using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E1
{
    [Transaction(TransactionMode.Manual)]
    public class ex1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Получение объектов приложения и документа
            UIApplication uiApp = commandData.Application;
            Document doc = uiApp.ActiveUIDocument.Document;

            string mes = "";
//            foreach (int item in walls_list)
//              mes += item + " ";

            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfCategory(BuiltInCategory.OST_Walls);

            Wall wall = null;
            foreach (Element elem in collector)
            {
                wall = elem as Wall;
                if (wall != null)
                {
                    mes += elem.Name + " = Name, " + elem.Id + " = Id, ";
                }
            }


            //List<int> walls_list = new List<int>((collector);
            /*
            List<int> walls_list = new List<int>();
            walls_list.Add(0);
            walls_list.Add(1);
            walls_list.Add(2);
            */

            TaskDialog.Show("Задание 1", mes);

            return Result.Succeeded;
        }
    }
}
