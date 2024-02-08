using MapModel;
using Maze.Interfaces;
using Maze.Model;
using System.Diagnostics;

namespace MapEditor
{
    public partial class Editor : Form
    {
        private Button[,] buttons;
        private bool[,] map;
        private List<MapObject> mapObjects;
        bool isSelectMode = false;
        object selectedObject;

        public Editor()
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
                            // Add the event handlers
                            // Toggle the state of the button and the map cell
                            buttons[i, j].Click += (sender, e) =>
                            {
                                var (x, y) = (ValueTuple<int, int>)((Button)sender).Tag;

                                if (isSelectMode)
                                {
                                    if (selectedObject is Trap trap)
                                    {
                                        trap.TeleToX = x;
                                        trap.TeleToY = y;
                                        mapObjects.Add(trap);
                                    }

                                    isSelectMode = false;
                                    this.Cursor = Cursors.Default;
                                    RefreshButtonColors();
                                }
                                else
                                {
                                    // Toggle the state of the button and the map cell
                                    map[x, y] = !map[x, y];
                                    buttons[x, y].Text = map[x, y] ? " " : "#";
                                }
                            };
                            buttons[i, j].MouseHover += (sender, e) =>
                            {
                                var (x, y) = (ValueTuple<int, int>)((Button)sender).Tag;
                                FillInformationDescription(x, y);
                                selectedObject = mapObjects?.FirstOrDefault(o => o.X == x && o.Y == y);
                                if (selectedObject is Trap trap)
                                {
                                    buttons[trap.TeleToX, trap.TeleToY].BackColor = Color.Orange;
                                }
                            };
                            buttons[i, j].MouseLeave += (sender, e) =>
                            {
                                var (x, y) = (ValueTuple<int, int>)((Button)sender).Tag;
                                selectedObject = mapObjects?.FirstOrDefault(o => o.X == x && o.Y == y);
                                if (selectedObject is Trap trap)
                                {
                                    buttons[trap.TeleToX, trap.TeleToY].BackColor = SystemColors.ControlLightLight;
                                }
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

        /// <summary>
        /// save the map to the file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Open the overlay file and create the buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Refresh the colors of the buttons
        /// </summary>
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

        /// <summary>
        /// save the overlay to the file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Create a door on the map
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripCreateDoor_Click(object sender, EventArgs e)
        {
            var button = CreateObjectContextMenu.SourceControl;
            var (x, y) = (ValueTuple<int, int>)button.Tag;
            var door = new Door(x, y, true);
            mapObjects.Add(door);

            RefreshButtonColors();
        }

        private void teleportingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var button = CreateObjectContextMenu.SourceControl;
            var (x, y) = (ValueTuple<int, int>)button.Tag;
            var trap = new Trap();
            trap.X = x;
            trap.Y = y;
            trap.IsActive = true;

            selectedObject = trap;
            isSelectMode = true;
            this.Cursor = Cursors.Cross;
        }

        private void toolStripCreateItem_Click(object sender, EventArgs e)
        {
            var itemEditForm = new ItemEditForm();
            var result = itemEditForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                var button = CreateObjectContextMenu.SourceControl;
                var (x, y) = (ValueTuple<int, int>)button.Tag;
                var item = new Item
                {
                    X = x,
                    Y = y,
                    Name = itemEditForm.Name,
                    Description = itemEditForm.Description,
                    ItemType = itemEditForm.ItemType
                };
                mapObjects.Add(item);

                RefreshButtonColors();
            }
        }

        private void toolStripCreateFinish_Click(object sender, EventArgs e)
        {
            var finisheditForm = new FinishEditForm();
            finisheditForm.Treasures = mapObjects
                .Where(x => x is Item item && item.ItemType == ItemTypeEnum.Treasure)
                .Cast<Item>()
                .ToList();
            var result = finisheditForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                var button = CreateObjectContextMenu.SourceControl;
                var (x, y) = (ValueTuple<int, int>)button.Tag;
                var finish = new Finish();
                finish.X = x;
                finish.Y = y;

                var selectedItems = finisheditForm.Treasures
                    .Where(x => finisheditForm.SelectedTreasures.Contains(x.Name)).ToList();

                finish.FullfillmentConditionList = selectedItems;

                mapObjects.Add(finish);

                RefreshButtonColors();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var button = CreateObjectContextMenu.SourceControl;
            var (x, y) = (ValueTuple<int, int>)button.Tag;

            mapObjects.RemoveAll(o => o.X == x && o.Y == y);

            buttons[x, y].BackColor = SystemColors.ControlLightLight;
        }
    }
}                                     