using System;
using System.Windows;
using System.Windows.Controls;
using Autodesk.Revit.UI;

namespace DockableDialog.Forms
{
  public partial class MainPage : Page, Autodesk.Revit.UI.IDockablePaneProvider
  {
    public MainPage()
    {
      InitializeComponent();
    }

    public void SetupDockablePane( Autodesk.Revit.UI.DockablePaneProviderData data )
    {
      data.FrameworkElement = this as FrameworkElement;
      data.InitialState = new Autodesk.Revit.UI.DockablePaneState();
      data.InitialState.DockPosition = DockPosition.Tabbed;
      data.InitialState.TabBehind = Autodesk.Revit.UI.DockablePanes.BuiltInDockablePanes.ProjectBrowser;
    }
  }
}
