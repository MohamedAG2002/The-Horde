using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;

namespace TheHorde;

public class Player : DynamicEntity
{
    #region Consts
    private const int MAX_COOLDOWN = 50;
    private const int PISTOL_MAX_DIST = 50;
    private const int PISTOL_DAMAGE = 20;
    #endregion

    #region Fields
    public List<Bullet> PistolAmmo {get; private set;} = new List<Bullet>();
    private bool m_IsAbleToShoot;
    private int m_ShotCoolDown;
    #endregion

    #region Constructor
    public Player(Vector2 position, Texture2D texture, int maxHealth)
        :base(position, texture, maxHealth)
    {
        Velocity = new Vector2(200.0f, 0.0f);
        IsMoving = true;
        Anim = new Animation(Texture, 4, 10);
    
        m_IsAbleToShoot = true;
        m_ShotCoolDown = MAX_COOLDOWN;
    }
    #endregion

    #region Methods
    public override void Update(GameTime gameTime)
    {
        // Start the ticking once the player has shot
        if(!m_IsAbleToShoot) m_ShotCoolDown--;

        // Enables the player to shoot once the cool down has reached 0
        if(m_ShotCoolDown <= 0)
        {
            m_ShotCoolDown = MAX_COOLDOWN;
            m_IsAbleToShoot = true;
        }

        if(m_IsAbleToShoot && IsMoving) Shoot();

        // Updating the bullets
        for(int i = 0; i < PistolAmmo.Count; i++)
        {
            // Deleting the inactive bullets
            if(!PistolAmmo[i].IsActive) PistolAmmo.RemoveAt(i);
            // Update it otherwise
            else PistolAmmo[i].Update(gameTime);
        }

        base.Update(gameTime);
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        base.Render(spriteBatch);
        
        // Rendering the bullets
        foreach(var bullet in PistolAmmo)
        {
            if(bullet.IsActive)
                bullet.Render(spriteBatch);
        }
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
        if(Keyboard.GetState().IsKeyDown(Keys.Space))
        {
            PistolAmmo.Add(new Bullet(Position + new Vector2(0.0f, 15.0f), AssetManager.Instance().GetSprite("Bullet"), 1, PISTOL_DAMAGE, PISTOL_MAX_DIST));
            AssetManager.Instance().GetSound("Pistol").Play();
            m_IsAbleToShoot = false;

            Console.WriteLine("SHOOT!" + " " + PistolAmmo.Count);
        }
    }
    #endregion
}