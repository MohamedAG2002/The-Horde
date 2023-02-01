using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

namespace TheHorde;

public class EntityManager
{
    #region Fields
    public List<IEntity> Entities = new List<IEntity>();
    #endregion

    #region Constructor
    public EntityManager()
    {
        /* Adding entities */
        // Player
        Entities.Add(new Player(new Vector2(0.0f, Game1.ScreenHeight - 100.0f), AssetManager.Instance().GetSprite("Player"), 100));
    }
    #endregion

    #region Methods
    public void Update(GameTime gameTime)
    {
        for(int i = 0; i < Entities.Count; i++)
        {
            // Deleting the entity from the list if it's inactive
            if(!Entities[i].IsActive) Entities.RemoveAt(i);
            // Otherwise, update it as usual
            else Entities[i].Update(gameTime);
        }
    }

    public void Render(SpriteBatch spriteBatch)
    {
        foreach(var entity in Entities)
        {
            entity.Render(spriteBatch);
        }
    }
    #endregion
}