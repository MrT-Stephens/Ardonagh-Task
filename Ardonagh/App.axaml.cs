using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Ardonagh.Common;
using Ardonagh.Interfaces;
using Ardonagh.Models;
using Ardonagh.Services;
using Avalonia.Markup.Xaml;
using Ardonagh.ViewModels;
using Ardonagh.Views;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using SukiUI.Dialogs;

namespace Ardonagh;

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            
            var services = new ServiceCollection();
            
            var views = ConfigureViews(services);
            var provider = ConfigureServices(services);
            
            DataTemplates.Add(new ViewLocator(views));

            desktop.MainWindow = views.CreateView<MainViewModel>(provider) as Window;
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
    
    private static IViewsCollection ConfigureViews(IServiceCollection services)
    {
        var views = new ViewsCollection()
            .AddView<MainWindow, MainViewModel>(services)
            .AddView<CustomerView, CustomerViewModel>(services);
        
        return views;
    }

    private static IServiceProvider ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<SukiDialogManager>();
        services.AddSingleton<ISaveLoadService<Customer>, SaveLoadService<Customer>>();

        return services.BuildServiceProvider();
    }
}