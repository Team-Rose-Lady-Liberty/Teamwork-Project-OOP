using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RoseLadyLibertyOOPProject.GameObjects.Map
{
    public class Map : Interfaces.IDrawable
    {

        private int tileWidth;
        private int tileHeight;
        private int mapWidth;
        private int mapHeight;
        private Texture2D grassTexture;
        private Texture2D pathTexture;
        private Tile[,] map;

        public Map(TheGame game, int tileWidth, int tileHeight, int height = 16, int width = 16)
        {
            this.TileWidth = tileWidth;
            this.TileHeight = tileHeight;
            this.MapHeight = height;
            this.MapWidth = width;
            grassTexture = game.Content.Load<Texture2D>("Terrain/grass");
            pathTexture = game.Content.Load<Texture2D>("Terrain/path");
            map = new Tile[this.MapWidth, this.MapHeight];
            this.CreateMap(grassTexture);
            

        }

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
        public int MapWidth
        {
            get { return this.mapWidth; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException("Map width can not be negative or zero!");
                }
                this.mapWidth = value;
            }
        }
        public int MapHeight
        {
            get { return this.mapHeight; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException("Map height can not be negative or zero!");
                }
                this.mapHeight = value;
            }
        }

        public void CreateMap(Texture2D texture)
        {
            for (int iX = 0; iX < this.MapWidth; iX++)
            {
                for (int iZ = 0; iZ < this.MapHeight; iZ++)
                {
                    this.map[iX, iZ] = new Tile("grass_tile", iX * this.TileWidth, iZ * this.TileHeight, texture);
                }
            }
            this.GeneratePath();
        }

        public void GeneratePath()
        {
            for (int iX = 0; iX < this.MapWidth; iX++)
            {
                this.map[iX, 2].TileTexture = pathTexture;
                this.map[iX, 2].IsPath = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            for (int iX = 0; iX < this.MapWidth; iX++)
            {
                for (int iZ = 0; iZ < this.MapHeight; iZ++)
                {
                    this.map[iX, iZ].Draw(spriteBatch, TileWidth, TileHeight);
                }
            }
            spriteBatch.End();
        }
    }
}
