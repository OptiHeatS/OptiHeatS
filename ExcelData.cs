using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Database;

namespace OptiHeatPro
{
    public class ExcelData
    {
        public String? TFrom { get; set; } // time from
        public String? TTo { get; set; } // time to
        public Double HDemand { get; set; } // heat demand
        public Double EPrice { get; set; } // electricity price
        
        public ExcelData(String? tFrom, String? tTo, Double hDemand, Double ePrice)
        {
            TFrom = tFrom;
            TTo = tTo;
            HDemand = hDemand;
            EPrice = ePrice;
        }
    }
}