using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XMasterTimeandKJV.Contexts;
using XMasterTimeandKJV.Models;
using XMasterTimeandKJV.Views;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace XMasterTimeandKJV.ViewModels
{
    public class WorkInstanceViewModel : INotifyPropertyChanged
    {
        private DateTime _ClockIn;

        private DateTime _ClockOut;

        private HourlyRate _HourlyRate;

        public WorkInstanceContext workInstanceContext;//= new WorkInstanceContext();
        public DbSet<WorkInstance> DataStore;
        private void SetClockInOut(ref DateTime clockIn, ref DateTime clockOut, DateTime value)
        {
            clockIn = clockOut = value;
        }

        public float getTotalHours()
        {
            TimeSpan ts = ClockOut - ClockIn;
            return (float)ts.Hours + (ts.Minutes / 60f);
        }
        public override string ToString()
        {
            return $"\n{Oid.ToString()} -    {Date.ToString()}\nIN    {ClockIn.ToString()}\nOUT    {ClockOut.ToString()}\nHrs    { getTotalHours().ToString()}     Rate  ${HourlyRate.ToString()}";
        }

        public DateTime ClockIn
        {
            get
            {
                return _ClockIn;
            }
            set => SetClockInOut(ref _ClockIn, ref _ClockOut, value);
        }
        public DateTime ClockOut
        {
            get
            {
                return _ClockOut;
            }
            set
            {
                _ClockOut = value;
            }
        }
        public DateTime Date { get => _Date; set => SetProperty<DateTime>(ref _Date, value, "Date"); }
        public HourlyRate HourlyRate
        {
            get
            {
                return _HourlyRate;
            }
            set
            {
                _HourlyRate = value;
            }
        }
        public int Oid { get => _Oid; set => SetProperty<int>(ref _Oid, value, "Oid"); }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        private DateTime _Date;
        private int _Oid;

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string Title;
        #endregion
        public ObservableCollection<WorkInstance> WorkInstancesObservableCollection { get; set; }
        public Xamarin.Forms.Command LoadWorkInstancesCommand { get; set; }

        public WorkInstanceViewModel()
        {
            workInstanceContext = new WorkInstanceContext();
            Title = "Work Instances";
            WorkInstancesObservableCollection = new ObservableCollection<WorkInstance>();
            LoadWorkInstancesCommand = new Xamarin.Forms.Command(async () => await ExecuteLoadWorkInstancesCommand());

            MessagingCenter.Subscribe<NewWorkInstancePage, WorkInstance>(this, "AddWorkInstance", async (obj, WorkInstance) =>
            {
                var newWorkInstance = WorkInstance as WorkInstance;
                WorkInstancesObservableCollection.Add(newWorkInstance);
                await DataStore.AddAsync(newWorkInstance);
            });
        }

        async Task ExecuteLoadWorkInstancesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                WorkInstancesObservableCollection.Clear();
                var WorkInstances = await DataStore.ToArrayAsync<WorkInstance>();
                foreach (var WorkInstance in WorkInstances)
                {
                    WorkInstancesObservableCollection.Add(WorkInstance);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
