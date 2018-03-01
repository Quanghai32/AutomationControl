using LiteDB;
using LiveCharts;
using LiveCharts.Defaults;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismTest
{
    public class LiteDbHandle
    {
        static string DbPath = @"./LOG/History.db";

        private static LiteDbHandle instance;
        public static LiteDbHandle Instance
        {
            get
            {
                if (instance == null) instance = new LiteDbHandle();
                return instance;
            }
            set { instance = value; }
        }

        public void SaveHistory(KeyenceMachineViewModel mc)
        {
            using (LiteDatabase db = new LiteDatabase(DbPath))
            {
                var historys = db.GetCollection<History>("History");

                List<History> oldData = historys.Find(x => x.Date == mc.Date.ToString("yyyy-MMM-dd") &&
                                                      x.Shift == mc.Shift && x.Name == mc.Name  && 
                                                      x.Model==mc.ProductModel
                                                      ).ToList();
                if (oldData.Count != 0)
                {
                    oldData[0].Performance = Double.Parse(mc.Performance.MemoryValue);
                    oldData[0].AvailabilityRate = Double.Parse(mc.AvailabilityRate.MemoryValue);
                    historys.Update(oldData[0]);
                }
                else
                {
                    History newData = new History()
                    {
                        Name = mc.Name,
                        Model=mc.ProductModel,
                        Date = mc.Date.ToString("yyyy-MMM-dd"),
                        Shift = mc.Shift,
                        AvailabilityRate = Double.Parse(mc.AvailabilityRate.MemoryValue),
                        Performance = Double.Parse(mc.Performance.MemoryValue)
                    };
                    historys.Insert(newData);
                }
            }
        }

        public void LoadHistory
            (
            DateTime fromDate, DateTime toDate, string shift,
            ref ChartValues<ObservableValue> perValue,
            ref ChartValues<ObservableValue> avaiValue,
            ref List<string> xAxis
            )
        {
            perValue.Clear();
            avaiValue.Clear();
            xAxis.Clear();

            using (LiteDatabase db = new LiteDatabase(DbPath))
            {
                var historys = db.GetCollection<History>("History");
                //List<History> oldData = historys.FindAll().ToList();
                List<History> oldData = historys.Find(x => DateTime.ParseExact(x.Date,"yyyy-MMM-dd",CultureInfo.InvariantCulture) >= fromDate && DateTime.ParseExact(x.Date, "yyyy-MMM-dd", CultureInfo.InvariantCulture) <= toDate && x.Shift == shift).ToList();
                if (oldData.Count > 0)
                {
                    foreach (History o in oldData)
                    {
                        perValue.Add(new ObservableValue(o.Performance));
                        avaiValue.Add(new ObservableValue(o.AvailabilityRate));
                        xAxis.Add(o.Date);
                    }
                }
            }
            //using (LiteDatabase db = new LiteDatabase(DbPath))
            //{
            //    var historys = db.GetCollection<History>("History");

            //    List<History> oldData = historys.FindAll().ToList();
            //    oldData=null;
            //}
        }

    }
}
