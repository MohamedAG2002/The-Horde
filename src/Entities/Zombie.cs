using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

namespace TheHorde;

public class Zombie : DynamicEntity
{
    #region Consts
    private const int MAX_ATTACK_COOLDOWN = 200;
    #endregion

    #region Fields
    public readonly int MaxDamage;
    public int Damage {get; set;}
    public bool IsAbleToAttack {get; set;}
    public ZombieType Type {get; private set;}
    public Animation Anim {get; private set;}
    private int m_AttackCoolDown;
    #endregion

    #region Events
    // Collision events
    public static event BarricadeCollision BarricadeCollisionEvent;

    // Audio events
    public static event BarricadeHitAudio BarricadeHitAudioEvent;
    public static event ZombieGrowlAudio ZombieGrowlAudioEvent;
    public static event ZombieDeathAudio ZombieDeathAudioEvent;

    // Score events
    public static event ScoreIncrease ScoreIncreaseEvent;
    #endregion

    #region Constructor
    public Zombie(Vector2 position, Texture2D texture, int health, int damage, float speed)
        :base(position, texture, health)
    {
        Velocity = new Vector2(0.0f, speed);

        MaxDamage = damage;
        Damage = 0;
        IsAbleToAttack = true;

        // Determines which of the zombie types it is from the texture
        if(texture == AssetManager.Instance().GetSprite("BasicZombie"))
        {
            Type = ZombieType.Basic;
            Anim = new Animation(AssetManager.Instance().GetSprite("BasicZombie"), 4, 10);
        }
        else if(texture == AssetManager.Instance().GetSprite("BruteZombie"))
        {
            Type = ZombieType.Brute;
            Anim = new Animation(AssetManager.Instance().GetSprite("BruteZombie"), 4, 15);
        }
        else 
        {
            Type = ZombieType.Denizen;
            Anim = new Animation(AssetManager.Instance().GetSprite("DenizenZombie"), 4, 5);
        }

        m_AttackCoolDown = MAX_ATTACK_COOLDOWN;
    }
    #endregion

    #region Methods
    public override void Update(GameTime gameTime)
    {
        // Decrease the attack cooldown gradually
        m_AttackCoolDown--;

        if(IsAbleToAttack)
            Attack();

        base.Update(gameTime);

        // Allowing the zombie to attack once the cooldown has reached 0
        if(m_AttackCoolDown == 0)
        {
            m_AttackCoolDown = MAX_ATTACK_COOLDOWN;
            IsAbleToAttack = true;
        }   
        // Otherwise, make the zombie defenceless
        else Damage = 0;
    
        // Plays the approriate sound when the zombie dies
        if(Health == 0) 
        {
            ZombieDeathAudioEvent?.Invoke();
            ScoreIncreaseEvent?.Invoke(Type);
        }
    }

    public override void CollisionUpdate(List<IEntity> entities)
    {
        StaticEntity barricade = entities[1] as StaticEntity;

        // Collision: Zombie VS. Barricade 
        if(CollisionManager.OnPixelContains(this, barricade.Collider))
        {
            if(IsAbleToAttack)
            {
                BarricadeCollisionEvent?.Invoke(barricade, this);
                BarricadeHitAudioEvent?.Invoke();
            }
        }
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
        Damage = MaxDamage;
        IsAbleToAttack = false;
        
        // Plays the appropriate zombie sound depending on the type
        ZombieGrowlAudioEvent?.Invoke(Type);
    }
    #endregion
}