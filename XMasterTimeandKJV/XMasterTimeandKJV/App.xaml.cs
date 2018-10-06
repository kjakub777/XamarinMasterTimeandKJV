using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XMasterTimeandKJV.Contexts;
using XMasterTimeandKJV.Models;
using XMasterTimeandKJV.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XMasterTimeandKJV
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();


            MainPage = new MainPage();
        }

        protected async override void OnStart()
        {

            var dbFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var fileName = "WorkInTest.db";
            var dbFullPath = Path.Combine(dbFolder, fileName);
            try
            {
                using (var db = new WorkInstanceContext(dbFullPath))
                {
                    await db.Database.MigrateAsync(); //We need to ensure the latest Migration was added. This is different than EnsureDatabaseCreated.


                    if (db.WorkInstances == null || await db.WorkInstances.CountAsync() < 3)
                    {
                        WorkInstance WorkInstanceGary = new WorkInstance() { Oid = 1, Date = DateTime.Now, HourlyRate = new HourlyRate(17.5f) };
                        WorkInstance WorkInstanceJack = new WorkInstance() { Oid = 2, Date = DateTime.UtcNow, HourlyRate = new HourlyRate(18.5f) };
                        WorkInstance WorkInstanceLuna = new WorkInstance() { Oid = 3, Date = DateTime.Now + TimeSpan.FromDays(987), HourlyRate = new HourlyRate(20) };

                        List<WorkInstance> WorkInstancesInTheHat = new List<WorkInstance>() { WorkInstanceGary, WorkInstanceJack, WorkInstanceLuna };

                        await db.WorkInstances.AddRangeAsync(WorkInstancesInTheHat);
                        await db.SaveChangesAsync();
                    }

                    var WorkInstancesInTheBag = await db.WorkInstances.ToListAsync();

                    foreach (var WorkInstance in WorkInstancesInTheBag)
                    {
                        Console.WriteLine($"{WorkInstance.Oid} - {WorkInstance.Date} - {WorkInstance.HourlyRate}" + System.Environment.NewLine);
                    }
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
