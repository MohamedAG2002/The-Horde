using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace TheHorde;

public class AudioManager
{
    #region Constructor
    public AudioManager()
    {
        Zombie.ZombieGrowlAudioEvent += OnZombieGrowl;
        Bullet.ZombieDeathAudioEvent += OnZombieDeath;
        Player.BulletShotAudioEvent += OnBulletShot;
    }
    #endregion

    #region Methods
    public void OnZombieDeath()
    {
        AssetManager.Instance().GetSound("ZombieDeath").Play();
    }

    public void OnZombieGrowl(ZombieType zombieType)
    {
        switch(zombieType)
        {
            case ZombieType.Basic:
                AssetManager.Instance().GetSound("BasicGrowl").Play();
                break;
            case ZombieType.Brute:
                AssetManager.Instance().GetSound("BruteGrowl").Play();
                break;
            case ZombieType.Denizen:
                AssetManager.Instance().GetSound("DenizenGrowl").Play();
                break;    
        }
    }

    public void OnBulletShot(BulletType bulletType)
    {
        AssetManager.Instance().GetSound("Pistol").Play();
    }
    #endregion
}