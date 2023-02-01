using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheHorde;

// TO-DO Tomorrow
// Add the zombies
// Perhaps add more than one type of gun
// Add events
// Configure collisions
// Add a spawn manager

// PROBLEMS:
// The barricade_hit.mp3 does not allow the program to run when included in the project
// Find out a way to play all the audio from the audio manager only

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    #region Utility variables
    public static int ScreenWidth;
    public static int ScreenHeight;
    #endregion

    #region Managers
    public EntityManager EntityManager;
    public TileManager TileManager;
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
        ScreenWidth = 384;
        ScreenHeight = 512;

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

        // Entities init
        EntityManager = new EntityManager();

        // Tiles init
        TileManager = new TileManager();

        // Audio init
        AudioManager = new AudioManager(EntityManager);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        
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

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
