using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheHorde;

public class Bullet : DynamicEntity
{
    #region Fields
    public int Damage;
    public int MaxDist;
    private const int MAX_LIFETIME = 30;
    private int m_LifeTime;
    private Vector2 m_OriginalPosition;
    #endregion

    #region Constructor
    public Bullet(Vector2 position, Texture2D texture, int health, int damage, int maxDist)
        :base(position, texture, health)
    {
        Velocity = new Vector2(0.0f, 200.0f);
        
        Damage = damage;

        // The max dustanc at which the bullet will be effective
        MaxDist = maxDist;

        m_OriginalPosition = position;
        m_LifeTime = MAX_LIFETIME;
    }
    #endregion

    #region Methods
    public override void Update(GameTime gameTime)
    {
        // Decreasing the damage once the bullet is out of it's effective range
        if(Vector2.Distance(Position, m_OriginalPosition) >= MaxDist)
            Damage /= 2;

        // Disabling the bullet once it's lifetime is 0
        if(m_LifeTime <= 0) Health = 0;
    }

    public override void Move(GameTime gameTime)
    {
        Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
    #endregion
}