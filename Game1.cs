using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;

namespace TheHorde;

// TO-DO
// Scenes(main menu, pause menu, settings, help, game over)
// Upgrade the visuals(health bar, particles, hit points, which weapon currently equipped, better font)
// UI(buttons, checkboxes, sliders, texts)

// PROBLEMS:
// The collisions are not so pixel perfect as you had thought. FUCK COLLISIONS!

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    #region Utility variables
    public static int ScreenWidth;
    public static int ScreenHeight;
    public static Random Random;
    #endregion

    #region Managers
    public TileManager Tiles;
    public AudioManager Audio;
    public SceneManager Scenes;
    #endregion

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // Utility variables init
        ScreenWidth = 384;
        ScreenHeight = 512;
        Random = new Random();

        // Changing the game window size
        _graphics.PreferredBackBufferWidth = ScreenWidth;
        _graphics.PreferredBackBufferHeight = ScreenHeight;

        _graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        #region Managers init
        // Assets init
        AssetManager.Instance().LoadAssets(Content);

        // Tiles init
        Tiles = new TileManager();

        // Audio init
        Audio = new AudioManager();

        // Scenes init
        Scenes = new SceneManager();
        #endregion
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        #region Managers update
        // Scenes update
        Scenes.Update(gameTime);
        #endregion

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // Rendering stuff here
        _spriteBatch.Begin();

        #region Managers render
        // Tiles render
        Tiles.Render(_spriteBatch);

        // Scenes render
        Scenes.Render(_spriteBatch);
        #endregion

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}