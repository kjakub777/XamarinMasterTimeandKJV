using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using XMasterTimeandKJV.Models;
using XMasterTimeandKJV.Views;
using XMasterTimeandKJV.ViewModels;

namespace XMasterTimeandKJV.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkInstancesPage : ContentPage
    {
        WorkInstanceViewModel viewModel;

        public WorkInstancesPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new WorkInstanceViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as WorkInstance;
            if (item == null)
                return;

            await Navigation.PushAsync(new WorkInstanceDetailPage(new WorkInstanceDetailViewModel(item)));

            // Manually deselect item.
            WorkInstancesListView.SelectedItem = null;
        }



        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.DataStore.Count() == 0)
                viewModel.LoadWorkInstancesCommand.Execute(null);
        }

        private async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewWorkInstancePage()));
        }
    }
}