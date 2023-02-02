using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;

namespace TheHorde;

public class EntityManager
{
    #region Fields
    public List<IEntity> Entities {get; private set;} = new List<IEntity>();
    #endregion

    #region Delegates
    public delegate void EntityAudio();
    public delegate void ZombieAudio(string zombieType);
    public delegate void EntityCollision(IEntity entity);
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