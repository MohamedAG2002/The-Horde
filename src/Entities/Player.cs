using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;

namespace TheHorde;

public class Player : DynamicEntity
{
    #region Consts
    private const int MAX_PISTOL_COOLDOWN = 30;
    private const int MAX_SHOTGUN_COOLDOWN = 80;
    #endregion

    #region Fields
    public List<Bullet> Ammo {get; private set;} = new List<Bullet>();
    public Animation Anim {get; private set;}
    private bool m_IsAbleToShoot;
    private int m_ShotCoolDown;
    private BulletType m_CurrentWeapon;
    #endregion

    #region Events
    public static event BulletShotAudio BulletShotAudioEvent;
    #endregion

    #region Constructor
    public Player(Vector2 position, Texture2D texture, int maxHealth)
        :base(position, texture, maxHealth)
    {
        Velocity = new Vector2(210.0f, 0.0f);
        IsMoving = true;
        Anim = new Animation(Texture, 4, 15);
    
        m_IsAbleToShoot = true;
        m_ShotCoolDown = MAX_PISTOL_COOLDOWN;
        m_CurrentWeapon = BulletType.Pistol;
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
            // Applying the cooldown depending on the weapon
            if(m_CurrentWeapon == BulletType.Pistol)
                m_ShotCoolDown = MAX_PISTOL_COOLDOWN;
            else 
                m_ShotCoolDown = MAX_SHOTGUN_COOLDOWN;


            m_IsAbleToShoot = true;
        }

        // Updating the bullets
        for(int i = 0; i < Ammo.Count; i++)
        {
            // Deleting the inactive bullets
            if(!Ammo[i].IsActive) 
            {
                Ammo.RemoveAt(i);
                i--;
            }
            // Update it otherwise
            else Ammo[i].Update(gameTime);
        }

        if(!IsMoving) return;

        Shoot();

        Anim.Update();

        // Switching between the weapon types
        if(Keyboard.GetState().IsKeyDown(Keys.Q))
            m_CurrentWeapon = BulletType.Pistol;
        else if(Keyboard.GetState().IsKeyDown(Keys.E))
            m_CurrentWeapon = BulletType.Shootgun;

        base.Update(gameTime);
    }

    public override void CollisionUpdate(List<IEntity> entities)
    {
        foreach(var bullet in Ammo)
        {
            bullet.CollisionUpdate(entities);
        }
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        if(IsActive)
            Anim.Render(spriteBatch, Position);
        
        // Rendering the bullets
        foreach(var bullet in Ammo)
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
        if(Keyboard.GetState().IsKeyDown(Keys.Space) && m_IsAbleToShoot)
        {
            // Shooting the pistol
            if(m_CurrentWeapon == BulletType.Pistol)
            {
                Ammo.Add(new Bullet(Position + new Vector2(27.0f, 0.0f), AssetManager.Instance().GetSprite("Bullet"), BulletType.Pistol));
                
                BulletShotAudioEvent?.Invoke(BulletType.Pistol);
            }
            // Shooting the shotgun
            else
            {
                // Shooting three bursts of the shotgun shell
                Ammo.Add(new Bullet(Position + new Vector2(15.0f, 0.0f), AssetManager.Instance().GetSprite("Shell"), BulletType.Shootgun));
                Ammo.Add(new Bullet(Position + new Vector2(27.0f, 0.0f), AssetManager.Instance().GetSprite("Shell"), BulletType.Shootgun));
                Ammo.Add(new Bullet(Position + new Vector2(39.0f, 0.0f), AssetManager.Instance().GetSprite("Shell"), BulletType.Shootgun));
                
                BulletShotAudioEvent?.Invoke(BulletType.Shootgun);
            }

            m_IsAbleToShoot = false;
        }
    }
    #endregion
}