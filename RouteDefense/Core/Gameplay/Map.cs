using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using RouteDefense.Models.GameObjects;

namespace RouteDefense.Core.Gameplay
{
    public class Map : Interfaces.IDrawable
    {
        private int tileWidth;
        private int tileHeight;
        private int mapRowCells;
        private int mapColumnCells;
        private Texture2D grassTexture;
        private Texture2D pathTexture;
        private Texture2D dirtTexture;
        private Tile[,] map;
        private List<PathGenerator.Direction> mobDirections;
        private Texture2D topLeftCorner;
        private Texture2D topBorder;
        private Texture2D topRightCorner;
        private Texture2D leftCorner;
        private Texture2D rightCorner;
        private Texture2D bottomLeftCorner;
        private Texture2D bottomRightCorner;
        private Texture2D bottomBorder;
        //private Bridge bridge;


        public Map(ContentManager contentManager, int tileWidth, int tileHeight, int rowCells = 16, int columnCells = 16)
        {
            this.TileWidth = tileWidth;
            this.TileHeight = tileHeight;
            this.MapRowCells = rowCells;
            this.MapColumnCells = columnCells;

            grassTexture = contentManager.Load<Texture2D>("Terrain/rpgTile039.png");
            pathTexture = contentManager.Load<Texture2D>("Terrain/rpgTile052.png");

            topLeftCorner = contentManager.Load<Texture2D>("Terrain/rpgTile000.png");
            topBorder = contentManager.Load<Texture2D>("Terrain/rpgTile001.png");
            topRightCorner = contentManager.Load<Texture2D>("Terrain/rpgTile002.png");
            leftCorner = contentManager.Load<Texture2D>("Terrain/rpgTile018.png");
            rightCorner = contentManager.Load<Texture2D>("Terrain/rpgTile020.png");
            bottomLeftCorner = contentManager.Load<Texture2D>("Terrain/rpgTile036.png");
            bottomRightCorner = contentManager.Load<Texture2D>("Terrain/rpgTile038.png");
            bottomBorder = contentManager.Load<Texture2D>("Terrain/rpgTile037.png");
            map = new Tile[this.MapRowCells, this.MapColumnCells];
            
            this.CreateMap(grassTexture);
            //bridge = new Bridge(contentManager, "bridge", new Rectangle(32, 32, 64, 96));
        }

        /*private void OnPress()
        {
            if (bridge.IsMoovable)
            {
                var bridgeCordinates = Mouse.GetState().Position;
                this.PlaceBridge(bridgeCordinates);
            }
        }*/

        public void Update()
        {
            /*if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                this.OnPress();
            }*/
        }

        public Tile[,] MapCells { get { return this.map; } }
        
        public int TileWidth
        {
            get { return this.tileWidth; }
            set
            {
                if (value > 64 || value < 1)
                {
                    throw new ArgumentOutOfRangeException("Tile width must be less then 64");
                }
                this.tileWidth = value;
            }
        }

        public int TileHeight
        {
            get { return this.tileHeight; }
            set
            {
                if (value > 64 || value < 1)
                {
                    throw new ArgumentOutOfRangeException("Tile height must be less then 64");
                }
                this.tileHeight = value;
            }
        }

        public int MapRowCells
        {
            get { return this.mapRowCells; }
            private set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException("Map width can not be negative or zero!");
                }
                this.mapRowCells = value;
            }
        }

        public int MapColumnCells
        {
            get { return this.mapColumnCells; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException("Map height can not be negative or zero!");
                }
                this.mapColumnCells = value;
            }
        }

        public int MapWidth 
        {
            get { return this.MapRowCells * this.TileWidth; } 
        }

        public int MapHeight
        {
            get { return this.MapColumnCells * this.TileHeight; }
        }

        public void CreateMap(Texture2D texture)
        {
            for (int row = 0; row < this.MapRowCells; row++)
            {
                for (int column = 0; column < this.MapColumnCells; column++)
                {
                    this.map[row, column] = new Tile("grass_tile",
                        new Rectangle(row * this.TileWidth, column * this.TileHeight,
                            TileWidth, TileHeight), texture);
                }
            }
            this.GenerateBoards();
            this.GeneratePath();
        }

        public Tile[] PathTiles
        { get; private set; }

        public void GeneratePath()
        {
            List<Tile> pathTiles = new List<Tile>();

            var path = PathGenerator.GeneratePath(this.MapRowCells, this.MapColumnCells);

            for (int i = 0; i < path.Count; i++)
            {
                this.map[path[i].Item1, path[i].Item2].TileTexture = pathTexture;
                this.map[path[i].Item1, path[i].Item2].TileType = Enumerations.TileType.Path;
                pathTiles.Add(this.map[path[i].Item1, path[i].Item2]);
            }
            this.PathTiles = pathTiles.ToArray();
        }

        public void GenerateBoards()
        {
            for (int row = 0; row < this.MapRowCells; row++)
            {
                for (int column = 0; column < this.MapColumnCells; column++)
                {
                    if (row == 0 || row == this.MapRowCells - 1 || column == 0 || column == this.MapColumnCells - 1)
                    {
                        if (row == 0 && column == 0)
                        {
                            this.map[row, column].TileTexture = topLeftCorner;
                        }
                        else if (row > 0 && row != this.MapRowCells - 1 && column == 0)
                        {
                            this.map[row, column].TileTexture = topBorder;
                        }
                        else if (row == this.MapRowCells - 1 && column == 0)
                        {
                            this.map[row, column].TileTexture = topRightCorner;
                        }
                        else if (row == 0 && column > 0 && column != this.MapColumnCells - 1)
                        {
                            this.map[row, column].TileTexture = leftCorner;
                        }
                        else if (row == this.mapRowCells - 1 && column > 0 && column != this.MapColumnCells - 1)
                        {
                            this.map[row, column].TileTexture = rightCorner;
                        }
                        else if (row == 0 && column == this.mapColumnCells - 1)
                        {
                            this.map[row, column].TileTexture = bottomLeftCorner;
                        }
                        else if (row == this.mapRowCells - 1 && column == this.mapColumnCells - 1)
                        {
                            this.map[row, column].TileTexture = bottomRightCorner;
                        }
                        else
                        {
                            this.map[row, column].TileTexture = bottomBorder;
                        }
                    }
                }
            }
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Begin();
            for (int row = 0; row < this.MapRowCells; row++)
            {
                for (int column = 0; column < this.MapColumnCells; column++)
                {
                    this.map[row, column].Draw(spriteBatch);
                }
            }
           // this.bridge.Draw(spriteBatch);
        }

        /*public void PlaceBridge(Point cordinates)
        {
            bridge.IsActive = true;
            Rectangle rect = new Rectangle(cordinates.X, cordinates.Y, bridge.Rectangle.Width, bridge.Rectangle.Height);
            bridge.Rectangle = rect;
            bridge.IsMoovable = false;
        }*/
    }
}
