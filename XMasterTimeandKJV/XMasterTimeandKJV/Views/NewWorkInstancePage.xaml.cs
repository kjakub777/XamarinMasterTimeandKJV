﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using XMasterTimeandKJV.Models;

namespace XMasterTimeandKJV.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewWorkInstancePage : ContentPage
    {
        public Item Item { get; set; }

        public NewWorkInstancePage()
        {
            InitializeComponent();

            Item = new Item
            {
                Text = "Item name",
                Description = "This is an item description."
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopModalAsync();
        }
    }
}