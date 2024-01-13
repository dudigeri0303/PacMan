using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PacMan.Map
{
    public class Map
    {
        private Tile[,] tiles;
        public Tile[,] Tiles { get { return tiles; } }

        private Pellet[,] pellets;
        public Pellet[,] Pellets { get { return pellets; } }

        private static Map instance;
        public static Map GetInstance() 
        {
            if (instance == null) 
            {
                instance = new Map();
            }
            return instance; 
        }
        private List<Tuple<int, int>> intersections;
        public List<Tuple<int, int>> Intersections { get { return intersections; } }

        private List<Tuple<int, int>> houseTiles;
        public List<Tuple<int, int>> HouseTiles { get { return houseTiles; } }
       
        private List<Tuple<int, int>> doorTiles;
        public List<Tuple<int, int>> DoorTiles { get { return doorTiles; } }

        private List<Tuple<int, int>> ghostStartIntersections;
        public List<Tuple<int, int>> GhostStartIntersections { get { return ghostStartIntersections; } }

        private Map()
        {
            this.tiles = new Tile[Game1.NumOfCols, Game1.NumOfRows];
            this.pellets = new Pellet[Game1.NumOfCols, Game1.NumOfRows];

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
            
            this.houseTiles = new List<Tuple<int, int>>()
            {
                Tuple.Create(11, 16), Tuple.Create(12, 16), Tuple.Create(13, 16), Tuple.Create(14, 16), Tuple.Create(15, 16), Tuple.Create(16, 16),
                Tuple.Create(11, 17), Tuple.Create(12, 17), Tuple.Create(13, 17), Tuple.Create(14, 17), Tuple.Create(15, 17), Tuple.Create(16, 17),
                Tuple.Create(11, 18), Tuple.Create(12, 18), Tuple.Create(13, 18), Tuple.Create(14, 18), Tuple.Create(15, 18), Tuple.Create(16, 18)
            };

            this.doorTiles = new List<Tuple<int, int>>()
            {
                Tuple.Create(13, 15), Tuple.Create(14, 15)
            };

            this.ghostStartIntersections = new List<Tuple<int, int>>()
            {
                Tuple.Create(13, 14), Tuple.Create(14, 14)
            };
            
            DeserializeAndFillTiles(Game1.PathToTileArray);
            DeserializeAndFillPellets(Game1.PathToPelletArray);
        }


        private void DeserializeAndFillTiles(string filePath)
        {
            int[,] deserialzedArray = JsonConvert.DeserializeObject <int[,]>(File.ReadAllText(filePath));

            for (int i = 0; i < Game1.NumOfCols; i++)
            {
                for (int j = 0; j < Game1.NumOfRows; j++)
                {
                    this.tiles[i, j] = new Tile(i * Game1.TileWidth, j * Game1.TileHeight, Game1.TileWidth, Game1.TileHeight, $"tile{deserialzedArray[i, j].ToString()}.png", i, j, Game1.PathToTileImages);
                }
            }
        }

        private void DeserializeAndFillPellets(string filePath) 
        {
            int[,] deserialzedArray = JsonConvert.DeserializeObject<int[,]>(File.ReadAllText(filePath));

            for (int i = 0; i < Game1.NumOfCols; i++)
            {
                for (int j = 0; j < Game1.NumOfRows; j++)
                {
                    if (deserialzedArray[i, j] == 25)
                    {
                        this.pellets[i, j] = new Pellet(i * Game1.TileWidth, j * Game1.TileHeight, Game1.TileWidth, Game1.TileHeight, $"pellet_test.png", i, j, Game1.PathToPelletImages);
                    }
                    else if (deserialzedArray[i, j] == 26) 
                    {
                        this.pellets[i, j] = new Pellet(i * Game1.TileWidth, j * Game1.TileHeight, Game1.TileWidth, Game1.TileHeight, $"pelletBooster_test.png", i, j, Game1.PathToPelletImages);
                    }
                    else
                    {
                        this.pellets[i, j] = new Pellet(i * Game1.TileWidth, j * Game1.TileHeight, Game1.TileWidth, Game1.TileHeight, $"empty_pellet.png", i, j, Game1.PathToPelletImages);
                    }
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
                    this.pellets[i, j].DrawTile();
                }
            }
        }

        public void ResetMap() 
        {
            instance = null;
        }
    }
}
