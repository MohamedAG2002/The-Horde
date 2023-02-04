using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheHorde;

public class Bullet : DynamicEntity
{
    #region Fields
    public int Damage {get; set;}
    public int MaxDist {get; set;}
    private const int MAX_LIFETIME = 100;
    private int m_LifeTime;
    private Vector2 m_OriginalPosition;
    #endregion

    #region Events
    public static event BulletCollision BulletCollisionEvent;
    public static event ZombieDeathAudio ZombieDeathAudioEvent;
    #endregion

    #region Constructor
    public Bullet(Vector2 position, Texture2D texture, int health, int damage, int maxDist)
        :base(position, texture, health)
    {
        Velocity = new Vector2(0.0f, -300.0f);
        
        Damage = damage;

        // The max distanc at which the bullet will be effective
        MaxDist = maxDist;

        m_OriginalPosition = position;
        m_LifeTime = MAX_LIFETIME;
    }
    #endregion

    #region Methods
    public override void Update(GameTime gameTime)
    {
        // Decreasing the lifetime
        m_LifeTime--;

        // Decreasing the damage once the bullet is out of it's effective range
        if(Vector2.Distance(Position, m_OriginalPosition) >= MaxDist)
            Damage /= 2;

        // Disabling the bullet once it's lifetime is 0, or it is outside the screen's borders
        if(m_LifeTime <= 0 || Position.Y < 0) Health = 0;

        base.Update(gameTime);
    
    }

    public override void CollisionUpdate(List<IEntity> entities)
    {
        foreach(var entity in entities)
        {
            if(entity is Zombie)
            {
                if(Collider.Contains(entity.Collider))
                {
                    BulletCollisionEvent?.Invoke(this, entity as Zombie);
                    ZombieDeathAudioEvent?.Invoke();
                }
            }
        }
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        if(IsActive)
            spriteBatch.Draw(Texture, Position, Color.White);
    }

    public override void Move(GameTime gameTime)
    {
        Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
    #endregion
}