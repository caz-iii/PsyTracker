using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;


namespace PsyTrackerApp
{
    public class Data
    {
        public Button selected = null;

        public Dictionary<string, Tuple<Item, Grid>> Items = new Dictionary<string, Tuple<Item, Grid>>();
        public Dictionary<string, Grid> WorldsTop = new Dictionary<string, Grid>();
        public Dictionary<string, WorldData> WorldsData = new Dictionary<string, WorldData>();
    }

    public class WorldData
    {
        public Grid top;
        public Button world;
        public WorldGrid worldGrid;

        public WorldData(Grid top, Button world, WorldGrid itemgrid)
        {
            top = Top;
            world = World;
            worldGrid = itemgrid;
        }
    }
}