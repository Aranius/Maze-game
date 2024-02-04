using MapModel;
using Maze.Model;

namespace MapEditor
{
    public partial class Form1 : Form
    {
        private Button[,] buttons;
        private bool[,] map;
        private List<MapObject> mapObjects;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Open the map file and create the buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void otvorMapuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = OpenMapDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    map = MapFileFunction.LoadMaze(OpenMapDialog.FileName);

                    // Create the buttons
                    buttons = new Button[map.GetLength(0), map.GetLength(1)];
                    for (int i = 0; i < map.GetLength(0); i++)
                    {
                        for (int j = 0; j < map.GetLength(1); j++)
                        {
                            buttons[i, j] = new Button
                            {
                                Width = 30,
                                Height = 30,
                                Left = j * 30,
                                Top = menuStrip1.Height + i * 30,
                                Text = map[i, j] ? " " : "#",
                                Tag = (i, j)
                            };
                            buttons[i, j].Click += (sender, e) =>
                            {
                                // Toggle the state of the button and the map cell
                                var (x, y) = (ValueTuple<int, int>)((Button)sender).Tag;
                                map[x, y] = !map[x, y];
                                buttons[x, y].Text = map[x, y] ? " " : "#";
                            };
                            buttons[i, j].MouseHover += (sender, e) =>
                            {
                                var (x, y) = (ValueTuple<int, int>)((Button)sender).Tag;
                                FillInformationDescription(x, y);
                            };
                            buttons[i, j].ContextMenuStrip = CreateObjectContextMenu;
                            Controls.Add(buttons[i, j]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }

        /// <summary>
        ///  Fill the description box with the information about the object on the map
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void FillInformationDescription(int x, int y)
        {
            if (mapObjects == null) return;

            var obj = mapObjects.FirstOrDefault(o => o.X == x && o.Y == y);
            if (obj != null)
            {
                descriptionBox.Text = obj.ToString();
            }
        }

        private void ulozMapuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = SaveMapDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    MapFileFunction.SaveMaze(map, SaveMapDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }

        private void otvorOverlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = OpenOverlayDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    mapObjects = MapFileFunction.LoadMapObjects(OpenOverlayDialog.FileName);

                    RefreshButtonColors();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }

        private void RefreshButtonColors()
        {
            foreach (var mapObject in mapObjects)
            {
                var color = Color.LightGray;

                switch (mapObject)
                {
                    case Item:
                        color = Color.Blue;
                        break;
                    case Trap:
                        color = Color.Red;
                        break;
                    case Door:
                        color = Color.Brown;
                        break;
                    case Finish:
                        color = Color.Green;
                        break;

                }

                buttons[mapObject.X, mapObject.Y].BackColor = color;
            }
        }

        private void ulozOverlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = saveOverlayDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    MapFileFunction.SaveMapObjects(mapObjects, saveOverlayDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }

        private void toolStripCreateDoor_Click(object sender, EventArgs e)
        {
            var button = CreateObjectContextMenu.SourceControl;
            var (x, y) = (ValueTuple<int, int>)button.Tag;
            var door = new Door(x, y, true);
            mapObjects.Add(door);

            RefreshButtonColors();
        }
    }
}