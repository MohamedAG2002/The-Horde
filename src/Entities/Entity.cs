using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

namespace TheHorde;

public abstract class IEntity
{
    #region Fields
    public abstract Vector2 Position {get; set;}
    public abstract Texture2D Texture {get; set;}
    public abstract Rectangle Collider {get;}
    public abstract int MaxHealth {get; set;}
    public abstract int Health {get; set;}
    public abstract bool IsActive {get; set;}
    #endregion

    #region Methods
    public abstract void Update(GameTime gameTime);
    public abstract void CollisionUpdate(List<IEntity> entities);
    public abstract void Render(SpriteBatch spriteBatch);
    public abstract void TakeDamage(int damage);
    public abstract bool OnPixelCollision(IEntity entityA, IEntity entityB);
    #endregion
}

public class StaticEntity : IEntity
{
    #region Fields
    public override Vector2 Position { get; set; }
    public override Texture2D Texture { get; set; }
    public override Rectangle Collider 
    {
        get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height); }
    }
    public override int MaxHealth {get; set;}
    public override int Health {get; set;}
    public override bool IsActive { get; set; }
    #endregion

    #region Delegates
    // Collision delegates
    public delegate void BulletCollision(Bullet bullet, Zombie zombie);
    public delegate void BarricadeCollision(StaticEntity barricade, Zombie zombie);
    
    // Audio delegates 
    public delegate void BulletShotAudio(BulletType bulletType);
    public delegate void ZombieGrowlAudio(ZombieType zombieType);
    public delegate void ZombieDeathAudio();
    #endregion

    #region Constructor
    // Default constructor
    public StaticEntity()
    {
        Position = new Vector2(0.0f, 0.0f);
        Texture = null;
        MaxHealth = 0;
        Health = MaxHealth;
        IsActive = false;
    }

    public StaticEntity(Vector2 position, Texture2D texture, int maxHealth)
    {
        Position = position;
        Texture = texture;
        MaxHealth = maxHealth;
        Health = MaxHealth;
        IsActive = true;
    }
    #endregion

    #region Methods
    public override void Update(GameTime gameTime)
    {
        // Clamping the health to not go over the max or under 0
        if(Health > MaxHealth) Health = MaxHealth;
        else if(Health < 0) Health = 0;

        // Killing the entity once it is out of health
        if(Health == 0) IsActive = false;
    }

    public override void CollisionUpdate(List<IEntity> entities)
    {
        // Does nothing here   
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        if(IsActive)
            spriteBatch.Draw(Texture, Position, Color.White);
    }

    public override void TakeDamage(int damage)
    {
        // Taking damage only when there is health
        if(Health != 0) Health -= damage;
    }

    // Calculates pixel-perfect collisions
    public override bool OnPixelCollision(IEntity entityA, IEntity entityB)
    {
        // This code is provided by the user "pek" in 
        // https://gamedev.stackexchange.com/questions/15191/is-there-a-good-way-to-get-pixel-perfect-collision-detection-in-xna
        // All credits and thanks go to him

        // Getting the raw color data from the textures
        Color[] entityARawData = new Color[entityA.Texture.Width * entityA.Texture.Height];
        Texture.GetData<Color>(entityARawData);
        Color[] entityBRawData = new Color[entityB.Texture.Width * entityB.Texture.Height];
        entityB.Texture.GetData<Color>(entityBRawData);

        // Calculating the intersecting rectangle
        // Could be calculated with "if statements" as well, but this is more consice
        int intersectingRec1X = MathHelper.Max(entityA.Collider.X, entityB.Collider.X);
        int intersectingRec2X = MathHelper.Min(entityA.Collider.X + entityA.Collider.Width, entityB.Collider.X + entityB.Collider.Width);

        int intersectingRec1Y = MathHelper.Max(entityA.Collider.Y, entityB.Collider.Y);
        int intersectingRec2Y = MathHelper.Min(entityA.Collider.Y + entityA.Collider.Height, entityB.Collider.Y + entityB.Collider.Height);

        // Looping through each intersecting pixel
        for(int i = intersectingRec1Y; i < intersectingRec2Y; i++)
        {
            for(int j = intersectingRec1X; j < intersectingRec2X; j++)
            {
                // Get the color of the current pixel(for each entity)
                Color entityAPixelColor = entityARawData[(j - entityA.Collider.X) + (i - entityA.Collider.Y) * entityA.Texture.Width];
                Color entityBPixelColor = entityBRawData[(j - entityB.Collider.X) + (i - entityB.Collider.Y) * entityB.Texture.Width];

                // If both of the current colors' alpha channel is not 0(not transparent),
                // then there is a collision between the entities  
                if(entityAPixelColor.A != 0 && entityBPixelColor.A != 0)
                    return true;
            }
        }

        // No collisions occured
        return false;
    }
    #endregion
}

public class DynamicEntity : StaticEntity
{
    #region Variables
    public Vector2 Velocity {get; set;}
    public bool IsMoving {get; set;}
    #endregion

    #region Constructor
    public DynamicEntity(Vector2 position, Texture2D texture, int maxHealth)
        :base(position, texture, maxHealth)
    {
        Velocity = new Vector2(0.0f, 200.0f);
        IsMoving = true;
    }
    #endregion

    #region Methods
    public override void Update(GameTime gameTime)
    {
        if(IsMoving) Move(gameTime);

        // Clamping the position to the window's borders
        Position = new Vector2(MathHelper.Clamp(Position.X, -20.0f, Game1.ScreenWidth - Animation.SpriteWidth + 20.0f), Position.Y);

        base.Update(gameTime);
    }

    public virtual void Move(GameTime gameTime)
    {
        // Basic movements
        Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
    #endregion
}