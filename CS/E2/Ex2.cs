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

            FilteredElementCollector roomTags = 
   new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_RoomTags).WhereElementIsNotElementType();
            FilteredElementCollector roomTagTypes = 
   new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_RoomTags).WhereElementIsElementType();

            Element NewType = roomTagTypes.ToElements()[1];

            foreach (Element elem in collector)
            {
            if (elem is Room room)
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
                }
            }

            TaskDialog.Show("Задание 2", mes);

            return Result.Succeeded;
        }
    }
}
