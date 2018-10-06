using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using XMasterTimeandKJV.Models;
using XMasterTimeandKJV.ViewModels;

namespace XMasterTimeandKJV.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkInstanceDetailPage : ContentPage
    {
        WorkInstanceDetailViewModel viewModel;

        public WorkInstanceDetailPage(WorkInstanceDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public WorkInstanceDetailPage()
        {
            InitializeComponent();

            var item = new WorkInstance
            {
                Date = DateTime.UtcNow.AddMonths(23),
                Comment = "This is Workinstabnce description."
            };

            viewModel = new WorkInstanceDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}