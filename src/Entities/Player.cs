using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheHorde;

public class Player : DynamicEntity
{
    #region Fields
    
    #endregion

    #region Constructor
    public Player(Vector2 position, Texture2D texture, int maxHealth)
        :base(position, texture, maxHealth)
    {
        Velocity = new Vector2(200.0f, 0.0f);
        IsMoving = true;
        Anim = new Animation(Texture, 4, 10);
    }
    #endregion

    #region Methods
    public override void Update(GameTime gameTime)
    {
        // Shooting logic

        base.Update(gameTime);
    }

    public override void Move(GameTime gameTime)
    {
        // Moving to the left
        if(Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left))
        {
            Anim.Play();
            Position -= Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        // Moving to the right
        else if(Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right))
        {
            Anim.Play();
            Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }

    public void Shoot()
    {

    }
    #endregion
}