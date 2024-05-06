using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PsyTrackerApp
{
    /// <summary>
    /// Interaction logic for WorldGrid.xaml
    /// </summary>
    public partial class WorldGrid : UniformGrid
    {
        public WorldGrid()
        {
            InitializeComponent();
        }

        public void HandleWorldGrid(Item button, bool add)
        {
            if (add)
                Children.Add(button);
            else
                Children.Remove(button);

            int gridremainder = 0;
            if (Children.Count % 5 != 0)
                gridremainder = 1;

            int gridnum = Math.Max((Children.Count / 5) + gridremainder, 1);

            Rows = gridnum;
            Height = Rows * 40;
        }

        private void Item_Drop(Object sender, DragEventArgs e)
        {
            //Data data = MainWindow.data;
            MainWindow window = ((MainWindow)Application.Current.MainWindow);
            UniformGrid grid = sender as UniformGrid;

            Item button = e.Data.GetData(typeof(Item)) as Item;

            // move item to world
            window.ItemPoolGrid.Children.Remove(button);
            grid.Children.Add(button);

            // update collection count
            //window.IncrementCollected();

            // update mouse actions
            button.MouseDoubleClick -= button.Item_Click;
            button.MouseDown += button.Item_Return;
            button.MouseMove -= button.Item_MouseMove;

            //if (isreport)
            //{
            //    button.MouseEnter += button.Report_Hover;
            //}
        }
    }
}