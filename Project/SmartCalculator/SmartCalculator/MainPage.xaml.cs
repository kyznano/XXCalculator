using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SmartCalculator.Resources;
using SmartCalculator.View;
using SmartCalculator.Model;

namespace SmartCalculator
{
    public partial class MainPage : PhoneApplicationPage
    {
        //create screen
        private ScreenView screen;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            screen = new ScreenView();
            //add it to ScreenUI grid
            screenUI.Children.Add(screen);
            //Set data bingding to may ViewModel to handle something
            this.DataContext = App.ViewModel;
        }
       
    }
}