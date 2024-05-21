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
        // public static int Ghost_Marksmanship = 0;
        // public static int Ghost_Levitation = 0;
        // public static int Ghost_Shield = 0;
        // public static int Ghost_TK = 0;
        // public static int Ghost_Pyro = 0;
        // public static int Ghost_Clairvoyance = 0;
        // public static int Ghost_Invisibilty = 0;
        // public static int Ghost_Confusion = 0;
        // public static int Ghost_DuffelbagTag = 0;
        // public static int Ghost_Duffelbag = 0;
        // public static int Ghost_HatboxTag = 0;
        // public static int Ghost_Hatbox = 0;
        // public static int Ghost_PurseTag = 0;
        // public static int Ghost_Purse = 0;
        // public static int Ghost_SteamertrunkTag = 0;
        // public static int Ghost_Steamertrunk = 0;
        // public static int Ghost_SuitcaseTag = 0;
        // public static int Ghost_Suitcase = 0;
        // public static int Ghost_Candle = 0;

        //amount of obtained ghost items
        // public static int Ghost_Marksmanship_obtained = 0;
        // public static int Ghost_Levitation_obtained = 0;
        // public static int Ghost_Shield_obtained = 0;
        // public static int Ghost_TK_obtained = 0;
        // public static int Ghost_Pyro_obtained = 0;
        // public static int Ghost_Clairvoyance_obtained = 0;
        // public static int Ghost_Invisibilty_obtained = 0;
        // public static int Ghost_Confusion_obtained = 0;
        // public static int Ghost_DuffelbagTag_obtained = 0;
        // public static int Ghost_Duffelbag_obtained = 0;
        // public static int Ghost_HatboxTag_obtained = 0;
        // public static int Ghost_Hatbox_obtained = 0;
        // public static int Ghost_PurseTag_obtained = 0;
        // public static int Ghost_Purse_obtained = 0;
        // public static int Ghost_SteamertrunkTag_obtained = 0;
        // public static int Ghost_Steamertrunk_obtained = 0;
        // public static int Ghost_SuitcaseTag_obtained = 0;
        // public static int Ghost_Suitcase_obtained = 0;
        // public static int Ghost_Candle_obtained = 0;

        public static double universalOpacity = 0.5;

        public WorldGrid()
        {
            InitializeComponent();
        }

        public void Handle_WorldGrid(Item button, bool add)
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

            //UpdateGhostObtained(button, addRemove);
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
            if (e.Data.GetDataPresent(typeof(Item)))
            {
                Item item = e.Data.GetData(typeof(Item)) as Item;
            }
        }

        public void Add_Item(Item item)
        {
            //remove item from itempool
            Grid ItemRow = null;
            try
            {
                ItemRow = VisualTreeHelper.GetParent(item) as Grid;
            }
            catch
            {
                return;
            }

            if (ItemRow == null || ItemRow.Parent != window.ItemPool)
                return;

            ItemRow.Children.Remove(item);

            //add it to the world grid
            Handle_WorldGrid(item, true);

            //fix shadow opacity if needed
            Handle_Shadows(item, true);

            //Reset any obtained item to be normal transparency
            item.Opacity = 1.0;

            // update mouse actions
            item.MouseDoubleClick -= item.Item_Click;
            item.MouseMove -= item.Item_MouseMove;
            
            item.MouseDown -= item.Item_Return;
            item.MouseDown += item.Item_Return;
        }

        public void UpdateMulti(Item item, bool add)
        {
            //do nothing for ghost items
            if (item.Name.StartsWith("Ghost_"))
                return;

            int addRemove = 1;
            if (!add)
                addRemove = -1;

            char[] numbers = { '1', '2', '3', '4', '5' };
            string itemname = item.Name.TrimEnd(numbers);

            switch (itemname)
            {
                case "Marksmanship":
                    Real_Marksmanship += addRemove;
                    window.MarksmanshipCount.Text = (3 - Real_Marksmanship).ToString();
                    if (Real_Marksmanship == 3)
                    {
                        window.MarksmanshipCount.Fill = (SolidColorBrush)FindResource("Color_Black");
                        window.MarksmanshipCount.Stroke = (SolidColorBrush)FindResource("Color_Trans");
                    }
                    else
                    {
                        window.MarksmanshipCount.Fill = (LinearGradientBrush)FindResource("Color_Marksmanship");
                        window.MarksmanshipCount.Stroke = (SolidColorBrush)FindResource("Color_Black");
                    }
                    return;

                case "Levitation":
                    Real_Levitation += addRemove;
                    window.LevitationCount.Text = (3 - Real_Levitation).ToString();
                    if (Real_Levitation == 3)
                    {
                        window.LevitationCount.Fill = (SolidColorBrush)FindResource("Color_Black");
                        window.LevitationCount.Stroke = (SolidColorBrush)FindResource("Color_Trans");
                    }
                    else
                    {
                        window.LevitationCount.Fill = (LinearGradientBrush)FindResource("Color_Levitation");
                        window.LevitationCount.Stroke = (SolidColorBrush)FindResource("Color_Black");
                    }
                    return;

                case "Shield":
                    Real_Shield += addRemove;
                    window.ShieldCount.Text = (3 - Real_Shield).ToString();
                    if (Real_Shield == 3)
                    {
                        window.ShieldCount.Fill = (SolidColorBrush)FindResource("Color_Black");
                        window.ShieldCount.Stroke = (SolidColorBrush)FindResource("Color_Trans");
                    }
                    else
                    {
                        window.ShieldCount.Fill = (LinearGradientBrush)FindResource("Color_Shield");
                        window.ShieldCount.Stroke = (SolidColorBrush)FindResource("Color_Black");
                    }
                    return;

                case "TK":
                    Real_TK += addRemove;
                    window.TKCount.Text = (2 - Real_TK).ToString();
                    if (Real_TK == 2)
                    {
                        window.TKCount.Fill = (SolidColorBrush)FindResource("Color_Black");
                        window.TKCount.Stroke = (SolidColorBrush)FindResource("Color_Trans");
                    }
                    else
                    {
                        window.TKCount.Fill = (LinearGradientBrush)FindResource("Color_TK");
                        window.TKCount.Stroke = (SolidColorBrush)FindResource("Color_Black");
                    }
                    return;

                case "Pyro":
                    Real_Pyro += addRemove;
                    window.PyroCount.Text = (2 - Real_Pyro).ToString();
                    if (Real_Pyro == 2)
                    {
                        window.PyroCount.Fill = (SolidColorBrush)FindResource("Color_Black");
                        window.PyroCount.Stroke = (SolidColorBrush)FindResource("Color_Trans");
                    }
                    else
                    {
                        window.PyroCount.Fill = (LinearGradientBrush)FindResource("Color_Pyro");
                        window.PyroCount.Stroke = (SolidColorBrush)FindResource("Color_Black");
                    }
                    return;

                case "Clairvoyance":
                    Real_Clairvoyance += addRemove;
                    window.ClairvoyanceCount.Text = (2 - Real_Clairvoyance).ToString();
                    if (Real_Clairvoyance == 2)
                    {
                        window.ClairvoyanceCount.Fill = (SolidColorBrush)FindResource("Color_Black");
                        window.ClairvoyanceCount.Stroke = (SolidColorBrush)FindResource("Color_Trans");
                    }
                    else
                    {
                        window.ClairvoyanceCount.Fill = (LinearGradientBrush)FindResource("Color_Clairvoyance");
                        window.ClairvoyanceCount.Stroke = (SolidColorBrush)FindResource("Color_Black");
                    }
                    return;

                case "Invisibility":
                    Real_Invisibility += addRemove;
                    window.InvisibilityCount.Text = (2 - Real_Invisibility).ToString();
                    if (Real_Invisibility == 2)
                    {
                        window.InvisibilityCount.Fill = (SolidColorBrush)FindResource("Color_Black");
                        window.InvisibilityCount.Stroke = (SolidColorBrush)FindResource("Color_Trans");
                    }
                    else
                    {
                        window.InvisibilityCount.Fill = (LinearGradientBrush)FindResource("Color_Invisibility");
                        window.InvisibilityCount.Stroke = (SolidColorBrush)FindResource("Color_Black");
                    }
                    return;

                case "Confusion":
                    Real_Confusion += addRemove;
                    window.ConfusionCount.Text = (2 - Real_Confusion).ToString();
                    if (Real_Confusion == 2)
                    {
                        window.ConfusionCount.Fill = (SolidColorBrush)FindResource("Color_Black");
                        window.ConfusionCount.Stroke = (SolidColorBrush)FindResource("Color_Trans");
                    }
                    else
                    {
                        window.ConfusionCount.Fill = (LinearGradientBrush)FindResource("Color_Confusion");
                        window.ConfusionCount.Stroke = (SolidColorBrush)FindResource("Color_Black");
                    }
                    return;

                case "DuffleBagTag":
                    Real_DuffleBagTag += addRemove;
                    window.DuffleBagTagCount.Text = (10 - Real_DuffleBagTag).ToString();
                    if (Real_DuffleBagTag == 10)
                    {
                        window.DuffleBagTagCount.Fill = (SolidColorBrush)FindResource("Color_Black");
                        window.DuffleBagTagCount.Stroke = (SolidColorBrush)FindResource("Color_Trans");
                    }
                    else
                    {
                        window.DuffleBagTagCount.Fill = (LinearGradientBrush)FindResource("Color_DuffleBagTag");
                        window.DuffleBagTagCount.Stroke = (SolidColorBrush)FindResource("Color_Black");
                    }
                    return;

                case "DuffleBag":
                    Real_DuffleBag += addRemove;
                    window.DuffleBagCount.Text = (10 - Real_DuffleBag).ToString();
                    if (Real_DuffleBag == 10)
                    {
                        window.DuffleBagCount.Fill = (SolidColorBrush)FindResource("Color_Black");
                        window.DuffleBagCount.Stroke = (SolidColorBrush)FindResource("Color_Trans");
                    }
                    else
                    {
                        window.DuffleBagCount.Fill = (LinearGradientBrush)FindResource("Color_DuffleBag");
                        window.DuffleBagCount.Stroke = (SolidColorBrush)FindResource("Color_Black");
                    }
                    return;

                case "HatboxTag":
                    Real_HatboxTag += addRemove;
                    window.HatboxTagCount.Text = (10 - Real_HatboxTag).ToString();
                    if (Real_HatboxTag == 10)
                    {
                        window.HatboxTagCount.Fill = (SolidColorBrush)FindResource("Color_Black");
                        window.HatboxTagCount.Stroke = (SolidColorBrush)FindResource("Color_Trans");
                    }
                    else
                    {
                        window.HatboxTagCount.Fill = (LinearGradientBrush)FindResource("Color_HatboxTag");
                        window.HatboxTagCount.Stroke = (SolidColorBrush)FindResource("Color_Black");
                    }
                    return;

                case "Hatbox":
                    Real_Hatbox += addRemove;
                    window.HatboxCount.Text = (10 - Real_Hatbox).ToString();
                    if (Real_Hatbox == 10)
                    {
                        window.HatboxCount.Fill = (SolidColorBrush)FindResource("Color_Black");
                        window.HatboxCount.Stroke = (SolidColorBrush)FindResource("Color_Trans");
                    }
                    else
                    {
                        window.HatboxCount.Fill = (LinearGradientBrush)FindResource("Color_Hatbox");
                        window.HatboxCount.Stroke = (SolidColorBrush)FindResource("Color_Black");
                    }
                    return;

                case "PurseTag":
                    Real_PurseTag += addRemove;
                    window.HatboxCount.Text = (10 - Real_PurseTag).ToString();
                    if (Real_Hatbox == 10)
                    {
                        window.PurseTagCount.Fill = (SolidColorBrush)FindResource("Color_Black");
                        window.PurseTagCount.Stroke = (SolidColorBrush)FindResource("Color_Trans");
                    }
                    else
                    {
                        window.PurseTagCount.Fill = (LinearGradientBrush)FindResource("Color_PurseTag");
                        window.PurseTagCount.Stroke = (SolidColorBrush)FindResource("Color_Black");
                    }
                    return;

                case "Purse":
                    Real_Purse += addRemove;
                    window.PurseCount.Text = (10 - Real_Purse).ToString();
                    if (Real_Purse == 10)
                    {
                        window.PurseCount.Fill = (SolidColorBrush)FindResource("Color_Black");
                        window.PurseCount.Stroke = (SolidColorBrush)FindResource("Color_Trans");
                    }
                    else
                    {
                        window.PurseCount.Fill = (LinearGradientBrush)FindResource("Color_Purse");
                        window.PurseCount.Stroke = (SolidColorBrush)FindResource("Color_Black");
                    }
                    return;

                case "SteamerTrunkTag":
                    Real_SteamerTrunkTag += addRemove;
                    window.SteamerTrunkTagCount.Text = (10 - Real_SteamerTrunkTag).ToString();
                    if (Real_SteamerTrunkTag == 10)
                    {
                        window.SteamerTrunkTagCount.Fill = (SolidColorBrush)FindResource("Color_Black");
                        window.SteamerTrunkTagCount.Stroke = (SolidColorBrush)FindResource("Color_Trans");
                    }
                    else
                    {
                        window.SteamerTrunkTagCount.Fill = (LinearGradientBrush)FindResource("Color_SteamerTrunkTag");
                        window.SteamerTrunkTagCount.Stroke = (SolidColorBrush)FindResource("Color_Black");
                    }
                    return;

                case "Steamertrunk":

                    Real_SteamerTrunk += addRemove;
                    window.SteamerTrunkCount.Text = (10 - Real_SteamerTrunk).ToString();
                    if (Real_SteamerTrunk == 10)
                    {
                        window.SteamerTrunkCount.Fill = (SolidColorBrush)FindResource("Color_Black");
                        window.SteamerTrunkCount.Stroke = (SolidColorBrush)FindResource("Color_Trans");
                    }
                    else
                    {
                        window.SteamerTrunkCount.Fill = (LinearGradientBrush)FindResource("Color_SteamerTrunk");
                        window.SteamerTrunkCount.Stroke = (SolidColorBrush)FindResource("Color_Black");
                    }
                    return;

                case "SuitcaseTag":
                    Real_SuitcaseTag += addRemove;
                    window.SuitcaseTagCount.Text = (10 - Real_SuitcaseTag).ToString();
                    if (Real_SuitcaseTag == 10)
                    {
                        window.SuitcaseTagCount.Fill = (SolidColorBrush)FindResource("Color_Black");
                        window.SuitcaseTagCount.Stroke = (SolidColorBrush)FindResource("Color_Trans");
                    }
                    else
                    {
                        window.SuitcaseTagCount.Fill = (LinearGradientBrush)FindResource("Color_SuitcaseTag");
                        window.SuitcaseTagCount.Stroke = (SolidColorBrush)FindResource("Color_Black");
                    }
                    return;

                case "Suitcase":
                    Real_Suitcase += addRemove;
                    window.SuitcaseCount.Text = (10 - Real_Suitcase).ToString();
                    if (Real_Suitcase == 10)
                    {
                        window.SuitcaseCount.Fill = (SolidColorBrush)FindResource("Color_Black");
                        window.SuitcaseCount.Stroke = (SolidColorBrush)FindResource("Color_Trans");
                    }
                    else
                    {
                        window.SuitcaseCount.Fill = (LinearGradientBrush)FindResource("Color_Suitcase");
                        window.SuitcaseCount.Stroke = (SolidColorBrush)FindResource("Color_Black");
                    }
                    return;

                case "Candle":
                    Real_Candle += addRemove;
                    window.CandleCount.Text = (2 - Real_Candle).ToString();
                    if (Real_Candle == 2)
                    {
                        window.CandleCount.Fill = (SolidColorBrush)FindResource("Color_Black");
                        window.CandleCount.Stroke = (SolidColorBrush)FindResource("Color_Trans");
                    }
                    else
                    {
                        window.CandleCount.Fill = (LinearGradientBrush)FindResource("Color_Candle");
                        window.CandleCount.Stroke = (SolidColorBrush)FindResource("Color_Black");
                    }
                    return;

                default:
                    return;
            }
        }

        private int TableReturn(string nameButton)
        {
            Data data = MainWindow.data;
            string type = Codes.FindItemType(nameButton);

            return 0;

            
        }

        private void Handle_Shadows(Item item, bool add)
        {
            //don't hide shadows for the multi items
            if (Codes.FindItemType(item.Name) == "badge" || Codes.FindItemType(item.Name) == "baggage" || item.Name.StartsWith("Candle") || item.Name.StartsWith("Ghost_"))
            {
                return;
            }

            string shadowName = "S_" + item.Name;
            //ContentControl shadow = window.ItemPool.FindName(shadowName) as ContentControl;
            ContentControl shadow = null;

            foreach (Grid itemrow in window.ItemPool.Children)
            {
                shadow = itemrow.FindName(shadowName) as ContentControl;

                if (shadow != null)
                    break;
            }

            if (shadow == null)
                return;

            if (add)
            {
                shadow.Opacity = 1.0;
            }
            else
            {
                shadow.Opacity = 0.0;
            }
        }

    }
}