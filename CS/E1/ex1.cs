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
            foreach (Element elem in collector_walls)
            {
                wall = elem as Wall;
                if (wall != null)
                {
                    mes += elem.Name + ", " + elem.Id + "\n";
                }
            }

            SortedDictionary<string, string> rooms_with_walls =
            new SortedDictionary<string, string>();

            FilteredElementCollector collector_rooms = new FilteredElementCollector(doc);
            collector_rooms.OfCategory(BuiltInCategory.OST_Rooms);
            Room room = null;
            foreach (Element elem in collector_rooms)
            {
                room = elem as Room;
                if (room != null)
                {
                    //mes += elem.Name + ", " + elem.Id + "\n";
                    Getinfo_Room(room);
                    rooms_with_walls.Add(elem.Name, "");
                }
            }

            foreach (string key in rooms_with_walls.Keys)
            {
                mes += rooms_with_walls[key] + " ------- из мапы\n";
            }

            void Getinfo_Room(Room rms)
            {
                string m = "Room: ";

                //get the name of the room
                m += "\nRoom Name: " + rms.Name;

                //get the room position
                LocationPoint location = rms.Location as LocationPoint;
                XYZ point = location.Point;
                m += "\nRoom position: " + XYZToString(point);

                //get the room number
                m += "\nRoom number: " + rms.Number;

                IList<IList<Autodesk.Revit.DB.BoundarySegment>> segments = rms.GetBoundarySegments(new SpatialElementBoundaryOptions());
                if (null != segments)  //the room may not be bound
                {
                    foreach (IList<Autodesk.Revit.DB.BoundarySegment> segmentList in segments)
                    {
                        m += "\nBoundarySegment of the room: ";
                        foreach (Autodesk.Revit.DB.BoundarySegment boundarySegment in segmentList)
                        {
                            // Get curve start point
                            XYZ start = boundarySegment.GetCurve().GetEndPoint(0);
                            m += "\nCurve start point: " + XYZToString(start);
                            // Get curve end point
                            XYZ end = boundarySegment.GetCurve().GetEndPoint(1);
                            m += " Curve end point: " + XYZToString(end);

                            // Show the boundary elements
                            m += "\nBoundary element id: " + boundarySegment.ElementId.IntegerValue;
                        }
                    }
                }

                //TaskDialog.Show("Revit", m);
                mes += m + "\n\n\n";
            }

            // output the point's three coordinates
            string XYZToString(XYZ point)
            {
                return "(" + point.X + ", " + point.Y + ", " + point.Z + ")";
            }

            TaskDialog.Show("Задание 1", mes);

            return Result.Succeeded;
        }
    }
}
