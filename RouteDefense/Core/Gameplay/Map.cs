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
        private Tile[,] map;
        private List<PathGenerator.Direction> mobDirections;

        private Dictionary<string, Texture2D> textures; 

        public Map(ContentManager contentManager, int tileWidth, int tileHeight, int rowCells = 16, int columnCells = 16)
        {
            this.TileWidth = tileWidth;
            this.TileHeight = tileHeight;
            this.MapRowCells = rowCells;
            this.MapColumnCells = columnCells;
            
            textures = new Dictionary<string, Texture2D>();

            LoadContent(contentManager);

            map = new Tile[this.MapRowCells, this.MapColumnCells];
            
            this.CreateMap(textures["grass"]);
        }

        private void LoadContent(ContentManager content)
        {
            textures.Add("grass", content.Load<Texture2D>("Terrain/rpgTile039.png"));
            textures.Add("path", content.Load<Texture2D>("Terrain/rpgTile052.png"));
            textures.Add("topLeftCorner", content.Load<Texture2D>("Terrain/rpgTile000.png"));
            textures.Add("topBorder", content.Load<Texture2D>("Terrain/rpgTile001.png"));
            textures.Add("topRightCorner", content.Load<Texture2D>("Terrain/rpgTile002.png"));
            textures.Add("leftCorner", content.Load<Texture2D>("Terrain/rpgTile018.png"));
            textures.Add("rightCorner", content.Load<Texture2D>("Terrain/rpgTile020.png"));
            textures.Add("bottomLeftCorner", content.Load<Texture2D>("Terrain/rpgTile036.png"));
            textures.Add("bottomRightCorner", content.Load<Texture2D>("Terrain/rpgTile038.png"));
            textures.Add("bottomBorder", content.Load<Texture2D>("Terrain/rpgTile037.png"));
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
                    this.map[row, column] = new Tile(new Rectangle(row * this.TileWidth, column * this.TileHeight,
                            TileWidth, TileHeight), texture);
                }
            }
            this.GenerateBoards();
            this.DrawPath();
        }

        public Tile[] PathTiles { get; private set; }

        public List<Tuple<int, int>> GeneratePath()
        {

            var path = PathGenerator.GeneratePath(this.MapRowCells, this.MapColumnCells);
            return path;
           
        }

        private void DrawPath()
        {
            List<Tile> pathTiles = new List<Tile>();
            var path = GeneratePath();

            for (int i = 0; i < path.Count; i++)
            {
                this.map[path[i].Item1, path[i].Item2].TileTexture = textures["path"];
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
                            this.map[row, column].TileTexture = textures["topLeftCorner"];
                        }
                        else if (row > 0 && row != this.MapRowCells - 1 && column == 0)
                        {
                            this.map[row, column].TileTexture = textures["topBorder"];
                        }
                        else if (row == this.MapRowCells - 1 && column == 0)
                        {
                            this.map[row, column].TileTexture = textures["topRightCorner"];
                        }
                        else if (row == 0 && column > 0 && column != this.MapColumnCells - 1)
                        {
                            this.map[row, column].TileTexture = textures["leftCorner"];
                        }
                        else if (row == this.mapRowCells - 1 && column > 0 && column != this.MapColumnCells - 1)
                        {
                            this.map[row, column].TileTexture = textures["rightCorner"];
                        }
                        else if (row == 0 && column == this.mapColumnCells - 1)
                        {
                            this.map[row, column].TileTexture = textures["bottomLeftCorner"];
                        }
                        else if (row == this.mapRowCells - 1 && column == this.mapColumnCells - 1)
                        {
                            this.map[row, column].TileTexture = textures["bottomRightCorner"];
                        }
                        else
                        {
                            this.map[row, column].TileTexture = textures["bottomBorder"];
                        }
                    }
                }
            }
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int row = 0; row < this.MapRowCells; row++)
            {
                for (int column = 0; column < this.MapColumnCells; column++)
                {
                    this.map[row, column].Draw(spriteBatch);
                }
            }
        }
    }
}
