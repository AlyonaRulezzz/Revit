using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
[TransactionAttribute(TransactionMode.Manual)]
[RegenerationAttribute(RegenerationOption.Manual)]
public class Lab1PlaceGroup : IExternalCommand
{
    public Result Execute(
    ExternalCommandData commandData,
    ref string message,
    ElementSet elements)
    {
        //Получение объектов приложения и документа
        UIApplication uiApp = commandData.Application;
        Document doc = uiApp.ActiveUIDocument.Document;
        //Определение объекта-ссылки для занесения результата указания
        Reference pickedRef = null;
        //Указание группы
        Selection sel = uiApp.ActiveUIDocument.Selection;
        pickedRef = sel.PickObject(ObjectType.Element,
        "Выберите группу");

        //Element elem = pickedRef.Element;
        Element elem = doc.GetElement(pickedRef);

        Group group = elem as Group;
        //Указание точки
        XYZ point = sel.PickPoint("Укажите точку для размещения группы");
        //Размещение группы
        Transaction trans = new Transaction(doc);
        trans.Start("Lab");
        doc.Create.PlaceGroup(point, group.GroupType);
        trans.Commit();
        return Result.Succeeded;
    }
}
