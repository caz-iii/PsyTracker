using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Linq;
using System.IO;
using Microsoft.Win32;
using System.Windows.Documents;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Windows.Forms;
using Button = System.Windows.Controls.Button;
//using hotkeys

namespace PsyTrackerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Data data;
        public int collected;
        private int total;

        public MainWindow()
        {
            InitializeComponent();
            InitData();

            //InitOptions();

            //OnReset(null, null);
        }

        private void InitData()
        {
            data = new Data();

            data.WorldsData.Add(new WorldData(WhisperingRockTop, WhisperingRock, null, null, WhisperingRockGrid, false, 0));
            data.WorldsData.Add(new WorldData(BasicBrainingTop, BasicBraining, null, null, BasicBrainingGrid, false, 0));
            data.WorldsData.Add(new WorldData(ShootingGalleryTop, ShootingGallery, null, null, ShootingGalleryGrid, false, 0));
            data.WorldsData.Add(new WorldData(DancePartyTop, DanceParty, null, null, DancePartyGrid, false, 0));
            data.WorldsData.Add(new WorldData(BrainTumblerTop, BrainTumbler, null, null, BrainTumblerGrid, false, 0));
            data.WorldsData.Add(new WorldData(LungfishopolisTop, Lungfishopolis, null, null, LungfishopolisGrid, false, 0));
            data.WorldsData.Add(new WorldData(FordTop, Ford, null, null, FordGrid, false, 0));
            data.WorldsData.Add(new WorldData(ThorneyTowersTop, ThorneyTowers, null, null, ThorneyTowersGrid, false, 0));
            data.WorldsData.Add(new WorldData(MilkmanConspiracyTop, MilkmanConspiracy, null, null, MilkmanConspiracyGrid, false, 0));
            data.WorldsData.Add(new WorldData(GloriasTheaterTop, GloriasTheater, null, null, GloriasTheaterGrid, false, 0));
            data.WorldsData.Add(new WorldData(WaterlooWorldTop, WaterlooWorld, null, null, WaterlooWorldGrid, false, 0));
            data.WorldsData.Add(new WorldData(BlackVelvetopiaTop, BlackVelvetopia, null, null, BlackVelvetopiaGrid, false, 0));
            data.WorldsData.Add(new WorldData(MeatCircusTop, MeatCircus, null, null, MeatCircusGrid, false, 0));

        }

        //private void InitOptions()


        private void Window_LocationChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.WindowY = RestoreBounds.Top;
            Properties.Settings.Default.WindowX = RestoreBounds.Left;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Properties.Settings.Default.Width = RestoreBounds.Width;
            Properties.Settings.Default.Height = RestoreBounds.Height;
        }

        private void ResetSize(object sender, RoutedEventArgs e)
        {
            Width = 570;
            Height = 880;

            gridWindow.Width = 500;
            gridWindow.Height = 680;
        }
    }
}