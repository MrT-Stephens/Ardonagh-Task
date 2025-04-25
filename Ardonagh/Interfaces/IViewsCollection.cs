using System;
using System.Diagnostics.CodeAnalysis;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace Ardonagh.Interfaces;

/// <summary>
///     Interface for managing and resolving views associated with view models.
/// </summary>
public interface IViewsCollection
{
    IViewsCollection AddView<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
        TView,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
        TViewModel>(IServiceCollection services)
        where TView : ContentControl
        where TViewModel : ObservableObject;
    
    bool TryCreateView(IServiceProvider provider, Type viewModelType, [NotNullWhen(true)] out Control? view);
    
    bool TryCreateView(object? viewModel, [NotNullWhen(true)] out Control? view);

    Control CreateView<TViewModel>(IServiceProvider provider) where TViewModel : ObservableObject;
}