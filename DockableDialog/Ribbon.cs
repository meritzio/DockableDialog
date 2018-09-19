using System;
using System.Reflection;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using DockableDialog.Forms;

namespace DockableDialog
{
    public class Ribbon : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication a)
        {
            a.CreateRibbonTab("Example");

            RibbonPanel AECPanelDebug = a.CreateRibbonPanel("Example", "AEC LABS");

            string path = Assembly.GetExecutingAssembly().Location;

            #region DockableWindow
            PushButtonData pushButtonRegisterDockableWindow = new PushButtonData("RegisterDockableWindow", "Register", path, "DockableDialog.RegisterDockableWindow");
            pushButtonRegisterDockableWindow.AvailabilityClassName = "DockableDialog.AvailabilityNoOpenDocument";
            RibbonItem ri1 = AECPanelDebug.AddItem(pushButtonRegisterDockableWindow);
            #endregion

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }
    }

    public class AvailabilityNoOpenDocument : IExternalCommandAvailability
    {
        public bool IsCommandAvailable(UIApplication a, CategorySet b)
        {
            if (a.ActiveUIDocument == null)
            {
                return true;
            }
            return false;
        }
    }

    [Transaction(TransactionMode.Manual)]
    public class RegisterDockableWindow : IExternalCommand
    {
        MainPage m_MyDockableWindow = null;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            DockablePaneProviderData data
              = new DockablePaneProviderData();

            MainPage MainDockableWindow = new MainPage();
            m_MyDockableWindow = MainDockableWindow;

            data.FrameworkElement = MainDockableWindow
              as System.Windows.FrameworkElement;

            data.InitialState = new DockablePaneState();

            data.InitialState.DockPosition
              = DockPosition.Tabbed;
            
            data.InitialState.TabBehind = DockablePanes
              .BuiltInDockablePanes.ProjectBrowser;

            DockablePaneId dpid = new DockablePaneId(
              new Guid("{D7C963CE-B7CA-426A-8D51-6E8254D21157}"));

            commandData.Application.RegisterDockablePane(
              dpid, "AEC Dockable Window", MainDockableWindow
              as IDockablePaneProvider);

            return Result.Succeeded;
        }
    }
}
