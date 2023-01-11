using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;

namespace E2
{
    [Transaction(TransactionMode.Manual)]
    public class Ex2 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Получение объектов приложения и документа
            UIApplication uiApp = commandData.Application;
            Document doc = uiApp.ActiveUIDocument.Document;

            string mes = "Марки помещений изменены на АК Барс";

            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfCategory(BuiltInCategory.OST_Rooms);
            //collector.OfCategory(BuiltInCategory.OST_RoomTags);

            FilteredElementCollector roomTags = 
   new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_RoomTags).WhereElementIsNotElementType();
            FilteredElementCollector roomTagTypes = 
   new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_RoomTags).WhereElementIsElementType();

            Element NewType = roomTagTypes.ToElements()[1];

            /*foreach (RoomTag rt in roomTags.ToElements())
            {
                //if (rooms.Contains(rt.TaggedLocalRoomId))
                //{
                    rt.ChangeTypeId(NewType.Id);
                //}
            }*/


            foreach (Element elem in collector)
            {
            if (elem is Room room)
            //if (elem is RoomTag roomTag)
                {
                    using (Transaction tran = new Transaction(doc))
                    {
                        tran.Start("tran2");
                        foreach (RoomTag rt in roomTags.ToElements())
                        {
                            //if (rooms.Contains(rt.TaggedLocalRoomId))
                            //{
                            rt.ChangeTypeId(NewType.Id);
                            rt.RoomTagType.Name = "АК Барс";
                            //}
                        }

                        tran.Commit();
                    }

                    //if (roomTagType != null)  //36958 RoomTag
                    //ElementId newId = BuiltInParameter.;
                    //elem.ChangeTypeId(newId);
                    //elem.Name = "АК Барс";

                    //Parameter parameter = roomTag.LookupParameter("Тип");
                    //parameter.Set("АК Барс");
                    //RoomTag roomTag= (RoomTag) elem;
                    //roomTag.Name = "АК Барс";
                    //Wall wall = room.Document.GetElement(seg.ElementId) as Wall;

                    //foreach (Parameter parameter in elem.Parameters)
                    //{
                    //mes += parameter.Set("АК Барс") + "\n";
                    //}

                    //mes += elem.GetTypeId() + "   " + elem.GetType().Name + "  " + elem.Id + "\n";
                }
            }

            TaskDialog.Show("Задание 2", mes);

            return Result.Succeeded;
        }
    }
}
