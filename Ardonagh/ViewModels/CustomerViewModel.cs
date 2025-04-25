using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Ardonagh.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SukiUI.Dialogs;

namespace Ardonagh.ViewModels;

public partial class CustomerViewModel : ViewModelBase
{
    public CustomerViewModel(ISukiDialog dialog, Action<Customer> onSubmit)
    {
        _Dialog = dialog;
        _OnSubmit = onSubmit;

        Validate();
    }

    public CustomerViewModel(ISukiDialog dialog, Action<Customer> onSubmit, Customer customer)
    {
        _Dialog = dialog;
        _OnSubmit = onSubmit;
        Name = customer.Name;
        Age = customer.Age;
        PostCode = customer.PostCode;
        Height = customer.Height;
        
        Validate();
    }

    private readonly ISukiDialog _Dialog;

    private readonly Action<Customer> _OnSubmit;

    [ObservableProperty] private string _Name = string.Empty;

    [ObservableProperty] private string? _NameError;

    [ObservableProperty] private int? _Age;

    [ObservableProperty] private string? _AgeError;

    [ObservableProperty] private string _PostCode = string.Empty;

    [ObservableProperty] private string? _PostCodeError;

    [ObservableProperty] private double? _Height;

    [ObservableProperty] private string? _HeightError;

    [ObservableProperty] private bool _HasErrors;

    partial void OnNameChanged(string value) => ValidateProperty(nameof(Name));
    partial void OnAgeChanged(int? value) => ValidateProperty(nameof(Age));
    partial void OnPostCodeChanged(string value) => ValidateProperty(nameof(PostCode));
    partial void OnHeightChanged(double? value) => ValidateProperty(nameof(Height));

    private void ValidateProperty(string propertyName)
    {
        switch (propertyName)
        {
            case nameof(Name):
                if (string.IsNullOrWhiteSpace(Name) || Name.Length > 50)
                    NameError = "Name must be 1–50 characters.";
                else
                    NameError = null;
                break;

            case nameof(Age):
                if (Age is null or < 0 or > 110)
                    AgeError = "Age must be between 0 and 110.";
                else
                    AgeError = null;
                break;

            case nameof(PostCode):
                if (!PostCodeRegex().IsMatch(PostCode))
                    PostCodeError = "PostCode must include both letters and numbers.";
                else
                    PostCodeError = null;
                break;

            case nameof(Height):
                if (Height is null || (Height < 0 || Height > 2.50 || Math.Round(Height.Value, 2) != Height))
                    HeightError = "Height must be 0–2.50 with up to 2 decimal places.";
                else
                    HeightError = null;
                break;
        }

        HasErrors = !string.IsNullOrWhiteSpace(NameError) || !string.IsNullOrWhiteSpace(AgeError) ||
                    !string.IsNullOrWhiteSpace(PostCodeError) || !string.IsNullOrWhiteSpace(HeightError);
    }

    private void Validate()
    {
        ValidateProperty(nameof(Name));
        ValidateProperty(nameof(Age));
        ValidateProperty(nameof(PostCode));
        ValidateProperty(nameof(Height));
    }

    [RelayCommand]
    private void OnSubmit()
    {
        Validate();
        
        if (HasErrors)
        {
            return;
        }

        _OnSubmit.Invoke(new Customer(Name, Age!.Value, PostCode, Height!.Value));

        _Dialog.Dismiss();
    }

    [RelayCommand]
    private void OnCancel()
    {
        _Dialog.Dismiss();
    }

    [GeneratedRegex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]+$")]
    private static partial Regex PostCodeRegex();
}