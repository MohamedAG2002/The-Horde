using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace TheHorde;

public class CollisionManager
{
    #region Constructor
    public CollisionManager()
    {
        // Subscribing to events
        Zombie.BarricadeCollisionEvent += OnBarricadeCollision;
        Bullet.BulletCollisionEvent += OnBulletCollision;
    }
    #endregion

    #region Methods
    public void OnBarricadeCollision(StaticEntity barricade, Zombie zombie)
    {
        zombie.Velocity = new Vector2(0.0f, 0.0f);
        zombie.Anim.Stop();
        
        barricade.TakeDamage(zombie.Damage);
    }

    public void OnBulletCollision(Bullet bullet, IEntity entity)
    {
        entity.TakeDamage(bullet.Damage);
        bullet.Health = 0;
    }
    #endregion
}