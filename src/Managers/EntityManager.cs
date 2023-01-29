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
    }
    #endregion

    #region Methods
    public void Update(GameTime gameTime)
    {
        foreach(var entity in Entities)
        {
            entity.Update(gameTime);
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