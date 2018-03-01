using LiteDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HistoryViewer
{
    public class ViewModel : INotifyCollectionChanged
    {
        public ObservableCollection<OldData> ListPersons { get; set; }
        string dbPath = "History.db";

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public ICommand SaveCommand { get; set; }
        public ICommand RefreshCommand { get; set; }


        public ViewModel()
        {
            SaveCommand = new RelayCommand(() => ClickCommandHandle());
            RefreshCommand = new RelayCommand(() => RefreshCommandHandle());
            using (var db = new LiteDatabase(dbPath))
            {
                var persons = db.GetCollection<OldData>("History");
                ListPersons = new ObservableCollection<OldData>(persons.FindAll().ToList());
            }
        }

        private void RefreshCommandHandle()
        {
            using (var db = new LiteDatabase(dbPath))
            {
                LiteCollection<OldData> persons = db.GetCollection<OldData>("History");
                ListPersons.Clear();// = new ObservableCollection<Person>(persons.FindAll().ToList());
                foreach (OldData p in persons.FindAll().ToList())
                {
                    ListPersons.Add(p);
                }
            }
        }

        private void ClickCommandHandle()
        {
            using (var db = new LiteDatabase(dbPath))
            {
                var persons = db.GetCollection<OldData>("History");
                persons.Upsert(ListPersons);
            }
        }
    }

    public class OldData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string Date { get; set; }

        public string Shift { get; set; }
        public double AvailabilityRate { get; set; }
        public double Performance { get; set; }
    }
}
