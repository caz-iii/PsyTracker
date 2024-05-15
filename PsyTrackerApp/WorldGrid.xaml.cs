using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PsyTrackerApp
{
    /// <summary>
    /// Interaction logic for WorldGrid.xaml
    /// </summary>
    public partial class WorldGrid : UniformGrid
    {

        MainWindow window = (MainWindow)App.Current.MainWindow;

        //real versions for itempool counts
        public static int Real_Marksmanship = 0;
        public static int Real_Levitation = 0;
        public static int Real_Shield = 0;
        public static int Real_TK = 0;
        public static int Real_Pyro = 0;
        public static int Real_Clairvoyance = 0;
        public static int Real_Invisibilty = 0;
        public static int Real_Confusion = 0;
        public static int Real_DuffelbagTag = 0;
        public static int Real_Duffelbag = 0;
        public static int Real_HatboxTag = 0;
        public static int Real_Hatbox = 0;
        public static int Real_PurseTag = 0;
        public static int Real_Purse = 0;
        public static int Real_SteamertrunkTag = 0;
        public static int Real_Steamertrunk = 0;
        public static int Real_SuitcaseTag = 0;
        public static int Real_Suitcase = 0;
        public static int Real_Candle = 0;

        // ghost versions for itempool counts
        public static int Ghost_Marksmanship = 0;
        public static int Ghost_Levitation = 0;
        public static int Ghost_Shield = 0;
        public static int Ghost_TK = 0;
        public static int Ghost_Pyro = 0;
        public static int Ghost_Clairvoyance = 0;
        public static int Ghost_Invisibilty = 0;
        public static int Ghost_Confusion = 0;
        public static int Ghost_DuffelbagTag = 0;
        public static int Ghost_Duffelbag = 0;
        public static int Ghost_HatboxTag = 0;
        public static int Ghost_Hatbox = 0;
        public static int Ghost_PurseTag = 0;
        public static int Ghost_Purse = 0;
        public static int Ghost_SteamertrunkTag = 0;
        public static int Ghost_Steamertrunk = 0;
        public static int Ghost_SuitcaseTag = 0;
        public static int Ghost_Suitcase = 0;
        public static int Ghost_Candle = 0;

        //amount of obtained ghost items
        public static int Ghost_Marksmanship_obtained = 0;
        public static int Ghost_Levitation_obtained = 0;
        public static int Ghost_Shield_obtained = 0;
        public static int Ghost_TK_obtained = 0;
        public static int Ghost_Pyro_obtained = 0;
        public static int Ghost_Clairvoyance_obtained = 0;
        public static int Ghost_Invisibilty_obtained = 0;
        public static int Ghost_Confusion_obtained = 0;
        public static int Ghost_DuffelbagTag_obtained = 0;
        public static int Ghost_Duffelbag_obtained = 0;
        public static int Ghost_HatboxTag_obtained = 0;
        public static int Ghost_Hatbox_obtained = 0;
        public static int Ghost_PurseTag_obtained = 0;
        public static int Ghost_Purse_obtained = 0;
        public static int Ghost_SteamertrunkTag_obtained = 0;
        public static int Ghost_Steamertrunk_obtained = 0;
        public static int Ghost_SuitcaseTag_obtained = 0;
        public static int Ghost_Suitcase_obtained = 0;
        public static int Ghost_Candle_obtained = 0;

        public static double universalOpacity = 0.5;

        public WorldGrid()
        {
            InitializeComponent();
        }

        public void HandleWorldGrid(Item button, bool add)
        {
            Data data = MainWindow.data;
            int addRemove = 1;

            if (add)
            {
                //Default should be children count so that items are always added to the end if no ghosts are found
                int firstGhost = Children.Count;

                //search for ghost items
                foreach (Item child in Children)
                {
                    if (child.Name.StartsWith("Ghost_"))
                    {
                        //when one is found get the index of it
                        firstGhost = Children.IndexOf(child);
                        break;
                    }
                }

                //add the item
                try
                {
                    Children.Insert(firstGhost, button);
                }
                catch (Exception)
                {
                    return;
                }
            }
            else
            {
                Children.Remove(button);
                addRemove = -1;
            }

            UpdateGhostObtained(button, addRemove);
            UpdateMulti(button, add);

            int gridremainder = 0;
            if (Children.Count % 5 != 0)
                gridremainder = 1;

            int gridnum = Math.Max((Children.Count / 5) + gridremainder, 1);
            Rows = gridnum;

            // default 1, add .5 for every row
            double length = 1 + ((Children.Count - 1) / 5) / 2.0;
            Grid outerGrid = (Parent as Grid).Parent as Grid;
            int row = (int)Parent.GetValue(Grid.RowProperty);
            outerGrid.RowDefinitions[row].Height = new GridLength(length, GridUnitType.Star);
            string worldName = Name.Substring(0, Name.Length - 4);

        }

        private void Item_Drop(Object sender, DragEventArgs e)
        {

        }
    }
}