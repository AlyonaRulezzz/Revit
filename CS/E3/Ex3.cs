using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace E3
{
    [Transaction(TransactionMode.Manual)]
    public class Ex3 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Получение объектов приложения и документа
            UIApplication uiApp = commandData.Application;
            Document doc = uiApp.ActiveUIDocument.Document;

            string mes = "";

            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfCategory(BuiltInCategory.OST_Rooms);

            using (Transaction tran = new Transaction(doc))
            {
                tran.Start("tran1");
                foreach (Element elem in collector)
                {
                    if (elem is Room room)
                    {
                        if (room.Name.Contains("Помещение"))
                        {
                            mes += "Есть ошибки: " + room.Name + ", id = " + room.Id + "\n";
                        }
                    }
                }

                if (mes == "")
                {
                    mes += "Нет ошибок";
                }
                tran.Commit();
            }

            TaskDialog.Show("Задание 3", mes);

            return Result.Succeeded;
        }
    }
}
