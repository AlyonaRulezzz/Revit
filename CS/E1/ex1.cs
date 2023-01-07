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

            FilteredElementCollector collector_walls = new FilteredElementCollector(doc);
            collector_walls.OfCategory(BuiltInCategory.OST_Walls);
            Wall wall = null;

            FilteredElementCollector collector_rooms = new FilteredElementCollector(doc);
            collector_rooms.OfCategory(BuiltInCategory.OST_Rooms);
            Room room = null;

            IList<IList<BoundarySegment>> segments;

            SortedDictionary<string, string> rooms_with_walls = new SortedDictionary<string, string>();

            foreach (Element elem_r in collector_rooms)
            {
                room = elem_r as Room;
                if (room != null)
                {
                    segments = room.GetBoundarySegments(new SpatialElementBoundaryOptions());
                    //mes += elem.Name + ", " + elem.Id + "\n";
                    rooms_with_walls.Add(elem_r.Name, "");

                    /*foreach (Element elem_w in collector_walls)
                    {
                        wall = elem_w as Wall;
                        if (wall != null)
                        {*/
                            //rooms_with_walls[elem_r.Name] += " a";
                            //mes += elem.Name + ", " + elem.Id + "\n";

                            foreach (IList<BoundarySegment> segments_i in segments)
                            {
                                foreach (BoundarySegment seg in segments_i)
                                {
                                    /* if (curve.Equals(seg))
                                     {
                                        rooms_with_walls[elem_r.Name] += elem_w.Id + " ";
                                    }*/
                                    //Element e = seg.GetCurve();
                                    Wall wall_1 = room.Document.GetElement(seg.ElementId) as Wall;
                                    LocationCurve locationCurve = wall_1.Location as LocationCurve;
                                    Curve curve = locationCurve.Curve;
                                    rooms_with_walls[elem_r.Name] += wall_1.Name + " ";
                                }
                            }
                            
                        /*}
                    }*/

                }
            }

            foreach (string key in rooms_with_walls.Keys)
            {
                mes += key + ": " + rooms_with_walls[key] + " ------- из мапы\n";

            }

            TaskDialog.Show("Задание 1", mes);

            return Result.Succeeded;
        }
    }
}
