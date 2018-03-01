using LiteDB;
using System;
using System.Collections.Generic;
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
                var historys = db.GetCollection<OldData>("History");

                List<OldData> oldData = historys.Find(x => x.Date == mc.Date && x.Shift == mc.Shift).ToList();
                if (oldData.Count != 0)
                {
                    oldData[0].Performance = Double.Parse(mc.Performance.MemoryValue);
                    oldData[0].AvailabilityRate = Double.Parse(mc.AvailabilityRate.MemoryValue);
                    historys.Update(oldData[0]);
                }
                else
                {
                    OldData newData = new OldData()
                    {
                        Date = mc.Date,
                        Shift = mc.Shift,
                        AvailabilityRate = Double.Parse(mc.AvailabilityRate.MemoryValue),
                        Performance = Double.Parse(mc.Performance.MemoryValue)
                    };
                    historys.Insert(newData);
                }
            }
        }

    }
}
