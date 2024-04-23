using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

namespace OptiHeatPro
{
    public class DataEntry
    {
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
        public double HeatDemand { get; set; }
        public double ElectricityPrice { get; set; }
    }

    public class HeatingData
    {
        public List<DataEntry>? WinterData { get; private set; }
        public List<DataEntry>? SummerData { get; private set; }

        public void Read()
        {
            WinterData = new List<DataEntry>();
            SummerData = new List<DataEntry>();

            using (var reader = new StreamReader(@"datacsv.csv"))
            {
                reader.ReadLine(); // Skip header
                reader.ReadLine(); // Skip sub-header

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    var values = line.Split(';');
#pragma warning restore CS8602 // Dereference of a possibly null reference.

                    StoreData(values);
                }
            }
        }

        private void StoreData(string[] values)
        {
            var format = "dd/MM/yyyy HH.mm";
        
            var winterEntry = new DataEntry
            {
                TimeFrom = DateTime.ParseExact(values[0], format, CultureInfo.InvariantCulture),
                TimeTo = DateTime.ParseExact(values[1], format, CultureInfo.InvariantCulture),
                HeatDemand = double.Parse(values[2].Replace(',', '.'), CultureInfo.InvariantCulture),
                ElectricityPrice = double.Parse(values[3].Replace(',', '.'), CultureInfo.InvariantCulture)
            };
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            WinterData.Add(winterEntry);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        
            var summerEntry = new DataEntry
            {
                TimeFrom = DateTime.ParseExact(values[5], format, CultureInfo.InvariantCulture),
                TimeTo = DateTime.ParseExact(values[6], format, CultureInfo.InvariantCulture),
                HeatDemand = double.Parse(values[7].Replace(',', '.'), CultureInfo.InvariantCulture),
                ElectricityPrice = double.Parse(values[8].Replace(',', '.'), CultureInfo.InvariantCulture)
            };
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            SummerData.Add(summerEntry);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        public void PrintData()
        {
            Console.WriteLine("Winter Data:");
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            foreach (var entry in WinterData)
            {
                Console.WriteLine($"TimeFrom: {entry.TimeFrom}, TimeTo: {entry.TimeTo}, HeatDemand: {entry.HeatDemand}, ElectricityPrice: {entry.ElectricityPrice}");
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            Console.WriteLine("Summer Data:");
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            foreach (var entry in SummerData)
            {
                Console.WriteLine($"TimeFrom: {entry.TimeFrom}, TimeTo: {entry.TimeTo}, HeatDemand: {entry.HeatDemand}, ElectricityPrice: {entry.ElectricityPrice}");
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
    }
}