using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using System.Collections.Generic;

namespace E1
{
    [Transaction(TransactionMode.Manual)]
    public class Ex1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Получение объектов приложения и документа
            UIApplication uiApp = commandData.Application;
            Document doc = uiApp.ActiveUIDocument.Document;

            string mes = "";

            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfCategory(BuiltInCategory.OST_Rooms);

            SortedDictionary<string, string> rooms_with_walls = new SortedDictionary<string, string>();

            foreach (Element elem in collector)
            {
                if (elem is Room room)
                {
                    IList<IList<BoundarySegment>> segments =
                                                    room.GetBoundarySegments(new SpatialElementBoundaryOptions());
                    rooms_with_walls.Add(elem.Id + ", " + elem.Name, "");

                    foreach (IList<BoundarySegment> segments_i in segments)
                    {
                        foreach (BoundarySegment seg in segments_i)
                        {
                            Wall wall = room.Document.GetElement(seg.ElementId) as Wall;
                            rooms_with_walls[elem.Id + ", " + elem.Name] += wall.Name + ", " + wall.Id + "\n\t\t";
                        }
                    }
                }
            }

            foreach (string key in rooms_with_walls.Keys)
            {
                mes += key + ":\n\t\t" + rooms_with_walls[key] + "\n";

            }

            TaskDialog.Show("Задание 1", mes);

            return Result.Succeeded;
        }
    }
}
