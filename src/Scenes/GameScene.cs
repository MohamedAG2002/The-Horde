using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheHorde;

public class GameScene : IScene
{
    #region Fields
    public EntityManager Entities;
    public SpawnManager Spawner;
    public CollisionManager Collision;
    public ScoreManager Score;
    #endregion

    #region Constructor
    public GameScene()
    {
        Entities = new EntityManager();
        Spawner = new SpawnManager(Entities, new Vector2(64.0f, 0.0f));
        Collision = new CollisionManager();
        Score = new ScoreManager();
    }
    #endregion

    #region Methods
    public void Update(GameTime gameTime)
    {
        Entities.Update(gameTime);
        Spawner.Update();
        Score.Update();
    }
    
    public void Render(SpriteBatch spriteBatch)
    {
        // Entities render
        Entities.Render(spriteBatch);

        #region HUD render
        // Barricade health
        spriteBatch.DrawString(AssetManager.Instance().GetFont("MainFont"), "Barricade: " + Entities.BarricadeHealth, new Vector2(10.0f, 10.0f), Color.Black);

        // Score
        spriteBatch.DrawString(AssetManager.Instance().GetFont("MainFont"), "Score: " + Score.Score, new Vector2(Game1.ScreenWidth - 75.0f, 10.0f), Color.Black);
        #endregion
    }
    #endregion
}