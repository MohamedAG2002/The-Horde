using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace TheHorde;

public class CollisionManager
{
    #region Fields
    private EntityManager m_EntityManager;
    #endregion

    #region Constructor
    public CollisionManager(EntityManager entityManager)
    {
        m_EntityManager = entityManager;
    
        // Subscribing to events
        m_EntityManager.EntityCollisionEvent += OnEntityCollision;
        m_EntityManager.BulletCollisionEvent += OnBulletCollision;
    }
    #endregion

    #region Methods
    public void OnEntityCollision(IEntity entity, int damage)
    {
        entity.TakeDamage(damage);
    }

    public void OnBulletCollision(Bullet bullet, IEntity entity)
    {
        entity.TakeDamage(bullet.Damage);
        bullet.Health = 0;
    }
    #endregion
}