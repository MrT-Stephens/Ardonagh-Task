using System.Collections.ObjectModel;
using System.Linq;
using Ardonagh.Interfaces;
using Ardonagh.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SukiUI.Dialogs;

namespace Ardonagh.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public SukiDialogManager DialogManager { get; }
    
    private readonly ISaveLoadService<Customer> _SaveLoadService;

    [ObservableProperty] private ObservableCollection<Customer> _Customers;
    [ObservableProperty] private Customer? _SelectedCustomer;

    public MainViewModel(SukiDialogManager dialogManager, ISaveLoadService<Customer> saveLoadService)
    {
        DialogManager = dialogManager;
        _SaveLoadService = saveLoadService;
        
        Customers = new ObservableCollection<Customer>(_SaveLoadService.Load());
    }

    [RelayCommand]
    private void AddButtonClicked()
    {
        DialogManager.CreateDialog()
            .WithViewModel(dialog => new CustomerViewModel(dialog, customer =>
            {
                Customers.Insert(0, customer);
                _SaveLoadService.Save(Customers.ToList());
            }))
            .Dismiss().ByClickingBackground()
            .TryShow();
    }

    [RelayCommand]
    private void EditButtonClicked()
    {
        if (SelectedCustomer == null) return;

        DialogManager.CreateDialog()
            .WithViewModel(dialog => new CustomerViewModel(dialog, customer =>
            {
                var index = Customers.IndexOf(SelectedCustomer);
                if (index >= 0) Customers[index] = customer;
                _SaveLoadService.Save(Customers.ToList());
            }, SelectedCustomer))
            .Dismiss().ByClickingBackground()
            .TryShow();
    }
}