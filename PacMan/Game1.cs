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

        public static string PathToTileImages { get; } = $"C:\\Users\\hp\\Source\\Repos\\PacMan\\PacMan\\Assets\\MapAssets\\TileImages\\";
        public static string PathToPelletImages { get; } = $"C:\\Users\\hp\\source\\repos\\PacMan\\PacMan\\Assets\\MapAssets\\PelletImages\\";
        public static string PathToTileArray { get; } = $"C:\\Users\\hp\\Source\\Repos\\PacMan\\PacMan\\Assets\\MapAssets\\MapDummy.txt";
        public static string PathToPelletArray { get; } = $"C:\\Users\\hp\\source\\repos\\PacMan\\PacMan\\Assets\\MapAssets\\PelletMap.txt";
        public static string PathToGhostImages { get; } = $"C:\\Users\\hp\\Source\\Repos\\PacMan\\PacMan\\Assets\\EntityAssets\\GhostAssets\\";
        public static int NumOfRows { get; } = 36;
        public static int NumOfCols { get; } = 28;
        public static int TileWidth { get; } = 24;
        public static int TileHeight { get; } = 24;
        public static int NumOfTiles { get; } = 26;

        private int windowWidth = 672;
        private int windowHeight = 864;
        private float timeElapsed;

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
            this.timeElapsed = 0;
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

            this.timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            this._game.UpdateGame(this.timeElapsed);

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
