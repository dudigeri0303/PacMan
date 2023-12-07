using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PacMan.PacManGame;

namespace PacMan
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;

        public static string PathToTiles { get; set; } = $"C:\\Users\\Gergő\\source\\repos\\PacMan\\PacMan\\Map\\TileImages\\";
        public static int NumOfRows { get; } = 36;
        public static int NumOfCols { get; } = 28;
        public static int TileWidth { get; } = 24;
        public static int TileHeight { get; } = 24;
        public static int NumOfTiles { get; } = 3;

        private int windowWidth = 900;
        private int windowHeight = 950;

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

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            this._game.UpdateGame();

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
