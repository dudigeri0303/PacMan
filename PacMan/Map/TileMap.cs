﻿using System;
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
        private static TileMap instance;
        public static TileMap GetInstance() 
        {
            if (instance == null) 
            {
                instance = new TileMap($"C:\\Users\\hp\\Source\\Repos\\PacMan\\PacMan\\Assets\\MapAssets\\MapDummy.txt");
            }
            return instance; 
        }
        private List<Tuple<int, int>> intersections;
        public List<Tuple<int, int>> Intersections { get { return intersections; } }

        private TileMap(string filePath)
        {
            tiles = new Tile[Game1.NumOfCols, Game1.NumOfRows];
            this.intersections = new List<Tuple<int, int>>() 
            {
                Tuple.Create(1, 4), Tuple.Create(6, 4), Tuple.Create(12, 4), Tuple.Create(15, 4), Tuple.Create(21, 4),  Tuple.Create(26, 4), 
                Tuple.Create(1, 8), Tuple.Create(6, 8), Tuple.Create(9, 8), Tuple.Create(12, 8), Tuple.Create(15, 8), Tuple.Create(18, 8), Tuple.Create(21, 8), Tuple.Create(26, 8), 
                Tuple.Create(1, 11), Tuple.Create(6, 11), Tuple.Create(9, 11), Tuple.Create(12, 11), Tuple.Create(15, 11), Tuple.Create(18, 11), Tuple.Create(21, 11), Tuple.Create(26, 11),
                Tuple.Create(9, 14), Tuple.Create(12, 14), Tuple.Create(15, 14), Tuple.Create(18, 14),
                Tuple.Create(6, 17), Tuple.Create(9, 17), Tuple.Create(18, 17), Tuple.Create(21, 17),
                Tuple.Create(9, 20), Tuple.Create(18, 20),
                Tuple.Create(1, 23), Tuple.Create(6, 23), Tuple.Create(9, 23), Tuple.Create(12, 23), Tuple.Create(15, 23), Tuple.Create(18, 23), Tuple.Create(21, 23), Tuple.Create(26, 23),
                Tuple.Create(1, 26), Tuple.Create(3, 26), Tuple.Create(6, 26), Tuple.Create(9, 26), Tuple.Create(12, 26), Tuple.Create(15, 26), Tuple.Create(18, 26), Tuple.Create(21, 26), Tuple.Create(24, 26), Tuple.Create(26, 26),
                Tuple.Create(1, 29), Tuple.Create(3, 29), Tuple.Create(6, 29), Tuple.Create(9, 29), Tuple.Create(12, 29), Tuple.Create(15, 29), Tuple.Create(18, 29), Tuple.Create(21, 29), Tuple.Create(24, 29), Tuple.Create(26, 29),
                Tuple.Create(1, 32), Tuple.Create(12, 32), Tuple.Create(15, 32), Tuple.Create(26, 32)
            };
            DeserializeAndFillTiles(filePath);
        }


        private void DeserializeAndFillTiles(string filePath)
        {
            int[,] deserialzedArray = JsonConvert.DeserializeObject <int[,]>(File.ReadAllText(filePath));

            for (int i = 0; i < Game1.NumOfCols; i++)
            {
                for (int j = 0; j < Game1.NumOfRows; j++)
                {
                    this.tiles[i, j] = new Tile(i * Game1.TileWidth, j * Game1.TileHeight, Game1.TileWidth, Game1.TileHeight, $"tile{deserialzedArray[i, j].ToString()}.png", i, j);
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
