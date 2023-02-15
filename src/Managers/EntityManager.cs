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
    public int BarricadeHealth;
    #endregion

    #region Constructor
    public EntityManager()
    {
        /* Adding entities */
        // Player
        Entities.Add(new Player(new Vector2(128.0f, Game1.ScreenHeight - 100.0f), AssetManager.Instance().GetSprite("Player"), 100));
    
        BarricadeHealth = BARRICADE_HEALTH;

        // Subscribing to events(doing a collision event here since the health for the barricade is here. Very bad design)
        Zombie.BarricadeCollisionEvent += OnBarricadeCollision;
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

        // Updating the entities
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
        }
    }

    public void OnBarricadeCollision(Zombie zombie)
    {
        zombie.Velocity = new Vector2(0.0f, 0.0f);
        zombie.Anim.Stop();

        BarricadeHealth -= zombie.Damage;
    }
    #endregion
}