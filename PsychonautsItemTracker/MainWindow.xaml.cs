using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Linq;
using System.IO;
using Microsoft.Win32;
using System.Drawing;
using System.Windows.Documents;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace PsychonautsItemTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static Data data;

        public int collected;

        private int total = 0;

        public static int PointTotal = 0;

        public int DeathCounter = 0;


        public MainWindow()
        {
            InitializeComponent();
            InitData();
        }

        private void InitData()
        {
            data.VisitLocks.Add();

            data.WorldsData.Add();

            foreach (Grid itemrow in ItemPool.Children)
            {
                foreach (ContentControl item in itemrow.Children)
                {
                    if (item is Item)
                    {
                        if (!item.Name.StartsWith("Ghost_"))
                            data.Items.Add(item as Item);
                        else
                            data.GhostItems.Add(item.Name, item as Item);
                    }
                }
            }
        }

        /// 
        /// Input Handling
        /// 

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {

            BitmapImage BarW = data.VerticalBarW;
            BitmapImage BarY = data.VerticalBarY;
            Button button = sender as Button;

            if (e.ChangedButton == MouseButton.Left)
            {
                if (data.selected != null)
                {
                    data.WorldsData[data.selected.Name].selectedBar.Source = BarW;
                }

                data.selected = button;
                data.WorldsData[button.Name].selectedBar.Source = BarY;
            }
        }



        public void IncrementCollected()
        {
            ++collected;
            //i don't want to update this code every time a new IC is added.
            //just setting it to the max of 99 for now. if it breaks i know where to look
            if (collected > 99)
                collected = 99;
        }

        public void DecrementCollected()
        {
            --collected;
            if (collected < 0)
                collected = 0;
        }

        public void IncrementTotal()
        {
            ++total;
            if (total > 99)
                total = 99;

        }

        public void DecrementTotal()
        {
            --total;
            if (total < 0)
                total = 0;

        }

        private void ResetSize(object sender, RoutedEventArgs e)
        {
            Width = 570;
            Height = 880;
        }


    }
}