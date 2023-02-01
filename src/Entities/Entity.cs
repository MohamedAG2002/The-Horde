using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
    public abstract void Render(SpriteBatch spriteBatch);
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

    public override void Render(SpriteBatch spriteBatch)
    {
        if(IsActive)
            spriteBatch.Draw(Texture, Position, Color.White);
    }
    #endregion
}

public class DynamicEntity : StaticEntity
{
    #region Variables
    public Vector2 Velocity {get; set;}
    public bool IsMoving {get; set;}
    public Animation Anim;
    #endregion

    #region Constructor
    public DynamicEntity(Vector2 position, Texture2D texture, int maxHealth)
        :base(position, texture, maxHealth)
    {
        Velocity = new Vector2(0.0f, 200.0f);
        IsMoving = true;
        Anim = new Animation(Texture, 4, 10);
    }
    #endregion

    #region Methods
    public override void Update(GameTime gameTime)
    {
        if(IsMoving)
        {
            Move(gameTime);
            Anim.Update();
        }

        // Clamping the position to the window's borders
        Position = new Vector2(MathHelper.Clamp(Position.X, -20.0f, Game1.ScreenWidth - Animation.SpriteWidth + 20.0f), Position.Y);

        base.Update(gameTime);
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        if(IsActive)
            Anim.Render(spriteBatch, Position);
    }

    public virtual void Move(GameTime gameTime)
    {
        // Basic movements
        Position += Velocity;
        Anim.Play();
    }
    #endregion
}