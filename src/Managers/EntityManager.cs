using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

namespace TheHorde;

public class EntityManager
{
    #region Consts
    private const int PLAYER_HEALTH = 100;
    private const int BARRICADE_HEALTH = 400;
    #endregion

    #region Fields
    public List<IEntity> Entities {get; private set;} = new List<IEntity>();
    private GraphicsDevice m_gd;
    #endregion

    #region Constructor
    public EntityManager(GraphicsDevice graphicsDevice)
    {
        m_gd = graphicsDevice;

        /* Adding entities */
        // Player
        Entities.Add(new Player(new Vector2(0.0f, Game1.ScreenHeight - 100.0f), AssetManager.Instance().GetSprite("Player"), 100));

        // Barricade(creates an invisible box that will act as the barricade)
        Entities.Add(new StaticEntity(new Vector2(0.0f, Game1.ScreenHeight - 140.0f), new Texture2D(graphicsDevice, Game1.ScreenWidth, 32), BARRICADE_HEALTH));
    }
    #endregion

    #region Methods
    public void Update(GameTime gameTime)
    {
        // Deleting the entity from the list if it's inactive
        for(int i = 0; i < Entities.Count; i++)
        {
            // Decreasing the "i" so not to skip over any entities
            if(!Entities[i].IsActive) 
            {
                Entities.RemoveAt(i);
                i--;
            }
        }

        foreach(var entity in Entities)
        {
            // Update for the collisions
            entity.CollisionUpdate(Entities);

            // Normal entity update
            entity.Update(gameTime);
        }
    }

    public void Render(SpriteBatch spriteBatch)
    {
        foreach(var entity in Entities)
        {
            entity.Render(spriteBatch);

            Texture2D texture = new Texture2D(m_gd, 1, 1);
            texture.SetData<Color>(new Color[] {Color.White});
            Rectangle rec = new Rectangle((int)entity.Position.X, (int)entity.Position.Y, entity.Texture.Width, entity.Texture.Height);

            spriteBatch.Draw(texture, rec, Color.Purple);
        }
    }
    #endregion
}