using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;

using UI;

namespace TheHorde;

// TO-DO
// Upgrade the visuals(health bar, particles, hit points, which weapon currently equipped, better font)
// Menus(main menu, pause menu, settings, help, game over)
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
    public SpriteFont mainFont;
    #endregion

    Text BarricadeHealth, ScoreText;

    #region Managers
    public TileManager Tiles;
    public EntityManager Entities;
    public AudioManager Audio;
    public SpawnManager Spawner;
    public CollisionManager Collision;
    public ScoreManager Score;
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

        // Entities init
        Entities = new EntityManager(GraphicsDevice);

        // Audio init
        Audio = new AudioManager();

        // Spawner init
        Spawner = new SpawnManager(Entities, new Vector2(64.0f, 0.0f));

        // Collisions init
        Collision = new CollisionManager();

        // Score init
        Score = new ScoreManager();
        #endregion

        // Loading the font
        mainFont = AssetManager.Instance().GetFont("MainFont");

        BarricadeHealth = new Text(mainFont, "Barricade: ", new Vector2(10.0f, 10.0f), Color.Black);
        ScoreText = new Text(mainFont, "Score: ", new Vector2(Game1.ScreenWidth - 75.0f, 10.0f), Color.Black);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        
        #region Managers update
        // Spawner init
        Spawner.Update();

        // Entities update
        Entities.Update(gameTime);

        // Score update
        Score.Update();
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

        // Entities render
        Entities.Render(_spriteBatch);
        #endregion

        #region UI render
        // Barricade's health 
        BarricadeHealth.String = "Barricade: " + Entities.Entities[1].Health;
        BarricadeHealth.Render(_spriteBatch);

        // Score   
        string scoreText = "Score: " + Score.Score;
        ScoreText.String = scoreText;
        ScoreText.Render(_spriteBatch);
        #endregion

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}