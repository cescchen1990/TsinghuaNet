﻿using System;
using System.Linq;
using Eto.Forms;
using Eto.Serialization.Xaml;
using TsinghuaNet.Models;

namespace TsinghuaNet.Eto.Views
{
    public class ConnectionDialog : Dialog
    {
        private ConnectionViewModel Model;

        public ConnectionDialog()
        {
            XamlReader.Load(this);
            Model = new ConnectionViewModel();
            DataContext = Model;
            FindChild<GridView>("ConnectionView").DataStore = Model.NetUsers;
            Model.RefreshNetUsers();
        }

        private async void DropSelection(object sender, EventArgs e)
        {
            var view = FindChild<GridView>("ConnectionView");
            await Model.DropAsync(view.SelectedItems.Select(user => ((NetUser)user).Address));
        }
    }
}