using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
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
    private Rectangle barricadeRec;
    #endregion

    #region Constructor
    public EntityManager(GraphicsDevice graphicsDevice)
    {
        /* Adding entities */
        // Player
        Entities.Add(new Player(new Vector2(0.0f, Game1.ScreenHeight - 100.0f), AssetManager.Instance().GetSprite("Player"), 100));

        // Barricade(creates an invisible box that will act as the barricade)
        Entities.Add(new StaticEntity(new Vector2(0.0f, Game1.ScreenHeight - 128.0f), new Texture2D(graphicsDevice, 1, 1), BARRICADE_HEALTH));
        barricadeRec = new Rectangle((int)Entities[1].Position.X, (int)Entities[1].Position.Y, Game1.ScreenWidth, 32);
    }
    #endregion

    #region Methods
    public void Update(GameTime gameTime)
    {
        CollisionUpdate();

        for(int i = 0; i < Entities.Count; i++)
        {
            // Deleting the entity from the list if it's inactive
            if(!Entities[i].IsActive) Entities.RemoveAt(i);
            // Otherwise, update it as usual
            else Entities[i].Update(gameTime);
        }
    }

    public void CollisionUpdate()
    {
        foreach(var entity in Entities)
        {
            entity.CollisionUpdate(Entities);
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