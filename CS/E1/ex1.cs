using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
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
            string mes = "Ola)";
            TaskDialog.Show("Задание 1", mes);

            return Result.Succeeded;
        }
    }
}
