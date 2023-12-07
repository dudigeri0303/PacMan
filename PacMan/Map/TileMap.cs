using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PacMan.Map
{
    public class TileMap
    {
        private Tile[,] tiles;
        public Tile[,] Tiles { get { return tiles; } }

        public TileMap(string filePath)
        {
            tiles = new Tile[Game1.NumOfCols, Game1.NumOfRows];
            DeserializeAndFillTiles(filePath);
        }


        private void DeserializeAndFillTiles(string filePath)
        {
            int[,] deserialzedArray = JsonConvert.DeserializeObject <int[,]>(File.ReadAllText(filePath));

            for (int i = 0; i < Game1.NumOfCols; i++)
            {
                for (int j = 0; j < Game1.NumOfRows; j++)
                {
                    this.tiles[i, j] = new Tile(i * Game1.TileWidth, j * Game1.TileHeight, Game1.TileWidth, Game1.TileHeight, $"tile{deserialzedArray[i, j].ToString()}.png");
                }
            }

        }

        public void DrawTileMap()
        {
            for (int i = 0; i < Game1.NumOfCols; i++)
            {
                for (int j = 0; j < Game1.NumOfRows; j++)
                {
                    this.tiles[i, j].DrawTile();
                }
            }
        }
    }
}
