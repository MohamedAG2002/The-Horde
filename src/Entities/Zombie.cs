using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheHorde;

public class Zombie : DynamicEntity
{
    #region Consts
    private const int MAX_ATTACK_COOLDOWN = 100;
    #endregion

    #region Fields
    public readonly int MaxDamage;
    public int Damage {get; set;}
    public bool IsAbleToAttack {get; set;}
    public Animation Anim {get; private set;}
    private int m_AttackCoolDown;
    #endregion

    #region Constructor
    public Zombie(Vector2 position, Texture2D texture, int health, int damage, float speed)
        :base(position, texture, health)
    {
        Velocity = new Vector2(0.0f, speed);

        MaxDamage = damage;
        Damage = MaxDamage;
        IsAbleToAttack = true;
        Anim = new Animation(Texture, 4, 10);

        m_AttackCoolDown = MAX_ATTACK_COOLDOWN;
    }
    #endregion

    #region Methods
    public override void Update(GameTime gameTime)
    {
        m_AttackCoolDown--;

        if(m_AttackCoolDown <= 0)
        {
            m_AttackCoolDown = MAX_ATTACK_COOLDOWN;
            IsAbleToAttack = true;
        }   
        else Damage = 0;

        if(IsMoving) 
        {
            Attack();
            Anim.Update();
        }

        base.Update(gameTime);
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        if(IsActive)
            Anim.Render(spriteBatch, Position);
    }

    public override void Move(GameTime gameTime)
    {
        Anim.Play();
        base.Move(gameTime);
    }

    public void Attack()
    {
        if(IsAbleToAttack)
        {
            Damage = MaxDamage;
            IsAbleToAttack = false;
        }
    }
    #endregion
}