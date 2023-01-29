using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheHorde;

public abstract class IEntity
{
    #region Fields
    public abstract Vector2 Position {get; set;}
    public abstract Texture2D Texture {get; set;}
    public abstract Rectangle Collider {get;}
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
    public override bool IsActive { get; set; }
    #endregion

    #region Constructor
    // Default constructor
    public StaticEntity()
    {
        Position = new Vector2(0.0f, 0.0f);
        Texture = null;
        IsActive = false;
    }

    public StaticEntity(Vector2 position, Texture2D texture)
    {
        Position = position;
        Texture = texture;
        IsActive = true;
    }
    #endregion

    #region Methods
    public override void Update(GameTime gameTime)
    {
        // Update does nothing here...
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
    #endregion

    #region Constructor
    public DynamicEntity(Vector2 position, Texture2D texture)
        :base(position, texture)
    {
        Velocity = new Vector2(200.0f, 200.0f);
        IsMoving = true;
    }
    #endregion

    #region Methods
    public override void Update(GameTime gameTime)
    {
        if(IsMoving)
            Move(gameTime);
    }

    public virtual void Move(GameTime gameTime)
    {
        // Basic movements
        Position += Velocity;
    }
    #endregion
}