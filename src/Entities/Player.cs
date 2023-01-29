using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheHorde;

public class Player : DynamicEntity
{
    #region Fields
    private int m_Direction {get; set;}
    private bool m_IsAbleToShot {get; set;}
    private const int MAX_COOLDOWN = 50; 
    private int m_ShotCooldown {get; set;}
    #endregion

    #region Constructor
    public Player(Vector2 position, Texture2D texture)
        :base(position, texture)
    {
        Velocity = new Vector2(200.0f, 0.0f);
        IsMoving = true;

        m_Direction = 0; // 0 = player facing left. 1 = player facing right
        m_IsAbleToShot = true;
        m_ShotCooldown = MAX_COOLDOWN;
    }
    #endregion

    #region Methods
    public override void Update(GameTime gameTime)
    {
        
        base.Update(gameTime);
    }

    public override void Move(GameTime gameTime)
    {
        // Moving to the left
        if(Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left))
        {
            Position -= Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            m_Direction = 0;
        }
        // Moving to the right
        else if(Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right))
        {
            Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            m_Direction = 1;
        }
    }
    #endregion
}