using System.Collections.Generic;
using Ardonagh.Interfaces;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Ardonagh.Common;

public class ViewLocator : IDataTemplate
{
    private readonly Dictionary<object, Control> _ControlCache = [];
    private readonly IViewsCollection _Views;

    public ViewLocator(IViewsCollection views)
    {
        _Views = views;
    }

    public Control Build(object? param)
    {
        if (param is null) return CreateText("Data is null.");

        if (_ControlCache.TryGetValue(param, out var control)) return control;

        if (_Views.TryCreateView(param, out var view))
        {
            _ControlCache.Add(param, view);

            return view;
        }

        return CreateText($"No View For {param.GetType().Name}.");
    }

    public bool Match(object? data)
    {
        return data is ObservableObject;
    }

    private static TextBlock CreateText(string text)
    {
        return new TextBlock { Text = text };
    }
}