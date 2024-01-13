using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PacMan.PacManGame;
using System.Diagnostics;

namespace PacMan
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;

        public static string PathToTileImages { get; } = $"C:\\Users\\Gergő\\source\\repos\\PacMan\\PacMan\\Assets\\MapAssets\\TileImages\\";
        public static string PathToPelletImages { get; } = $"C:\\Users\\Gergő\\source\\repos\\PacMan\\PacMan\\Assets\\MapAssets\\PelletImages\\";
        public static string PathToTileArray { get; } = $"C:\\Users\\Gergő\\source\\repos\\PacMan\\PacMan\\Assets\\MapAssets\\Map.txt";
        public static string PathToPelletArray { get; } = $"C:\\Users\\Gergő\\source\\repos\\PacMan\\PacMan\\Assets\\MapAssets\\PelletMap.txt";
        public static string PathToGhostImages { get; } = $"C:\\Users\\Gergő\\source\\repos\\PacMan\\PacMan\\Assets\\EntityAssets\\GhostAssets\\";
        public static string PathToPlayerImages { get; } = "C:\\Users\\Gergő\\source\\repos\\PacMan\\PacMan\\Assets\\EntityAssets\\PlayerAssets\\";
        public static int NumOfRows { get; } = 36;
        public static int NumOfCols { get; } = 28;
        public static int TileWidth { get; } = 24;
        public static int TileHeight { get; } = 24;
        public static int NumOfTiles { get; } = 27;
        public static float TotalGameTime { get; set; }

        private int windowWidth = 672;
        private int windowHeight = 864;

        private GameBase _game;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferWidth = windowWidth;
            _graphics.PreferredBackBufferHeight = windowHeight;
            _graphics.ApplyChanges();
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            this._game = new GameBase();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            TotalGameTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            this._game.UpdateGame((float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            this._game.DrawGame();

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
