using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace PsyTrackerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Button selected = null;

        List<Button> worlds = new List<Button>();

        List<Grid> grids = new List<Grid>();

        public MainWindow()
        {
            InitializeComponent();

            worlds.Add(WhisperingRockButton);
            worlds.Add(AsylumButton);
            worlds.Add(BasicBrainingButton);
            worlds.Add(ShootingGalleryButton);
            worlds.Add(DancePartyButton);
            worlds.Add(BrainTumblerButton);
            worlds.Add(LungfishopolisButton);
            worlds.Add(MilkManButton);
            worlds.Add(GloriaButton);
            worlds.Add(WaterlooButton);
            worlds.Add(EdgarButton);
            worlds.Add(KidCoachButton);
            worlds.Add(FordButton);

            grids.Add(WhisperingRockGrid);
            grids.Add(AsylumGrid);
            grids.Add(BasicBrainingGrid);
            grids.Add(ShootingGalleryGrid);
            grids.Add(DancePartyGrid);
            grids.Add(BrainTumblerGrid);
            grids.Add(LungfishopolisGrid);
            grids.Add(MilkManGrid);
            grids.Add(GloriaGrid);
            grids.Add(FredGrid);
            grids.Add(EdgarGrid);
            grids.Add(KidCoachGrid);
            grids.Add(FordGrid);

        }

        public void HandleWorldGrid(Grid grid, Button button, bool add)
        {
            if (add)
                grid.Children.Add(button);
            else
                grid.Children.Remove(button);

        }

        public void Item_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (selected != null)
            {
                

                for (int i = 0; i < worlds.Count; ++i)
                {
                    if (selected == worlds[i])
                    {
                        HandleWorldGrid(grids[i], button, true);
                        break;
                    }
                }

                button.Click -= Item_Click;
                button.Click += Item_Return;
            }
        }

        public void Item_Return(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            for (int i = 0; i < grids.Count; ++i)
            {
                if (grids[i] == button.Parent)
                {
                    HandleWorldGrid(grids[i], button, false);
                    break;
                }
            }


            button.Click -= Item_Return;
            button.Click += Item_Click;
        }
    }
}