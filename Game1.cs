using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;

namespace TheHorde;

// TO-DO
// Add a scoring system
// Upgrade the visuals(health bar, particles, hit points, which weapon currently equipped, better font)
// Menus(main menu, pause menu, settings, help, game over)
// UI(buttons, checkboxes, sliders)

// PROBLEMS:
// The collisions are not so pixel perfect as you had thought
// Fuck collisions

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
    public TileManager TileManager;
    public EntityManager EntityManager;
    public SpawnManager SpawnManager;
    public CollisionManager CollisionManager;
    public AudioManager AudioManager;
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

        // Assets init
        AssetManager.Instance().LoadAssets(Content);

        // Tiles init
        TileManager = new TileManager();

        // Entities init
        EntityManager = new EntityManager(GraphicsDevice);

        // Audio init
        AudioManager = new AudioManager();

        // Spawner init
        SpawnManager = new SpawnManager(EntityManager, new Vector2(64.0f, 0.0f));

        // Collisions init
        CollisionManager = new CollisionManager();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        
        // Spawner init
        SpawnManager.Update();

        // Entities update
        EntityManager.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // Rendering stuff here
        _spriteBatch.Begin();

        // Tiles render
        TileManager.Render(_spriteBatch);

        // Entities render
        EntityManager.Render(_spriteBatch);

        // UI render
        _spriteBatch.DrawString(AssetManager.Instance().GetFont("MainFont"), "Barricade: " + EntityManager.Entities[1].Health, new Vector2(10, 10), Color.Black);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
