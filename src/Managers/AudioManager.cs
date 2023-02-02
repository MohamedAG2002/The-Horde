using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace TheHorde;

public class AudioManager
{
    #region Fields
    private EntityManager m_EntityManager;
    private float m_Volume;
    #endregion

    #region Constructor
    public AudioManager(EntityManager entityManager)
    {
        m_EntityManager = entityManager;
        m_Volume = 100.0f;
    }
    #endregion

    #region Methods
    public void OnBulletShoot()
    {
        AssetManager.Instance().GetSound("Pistol").Play(m_Volume, 0.0f, 0.0f);
    }

    public void OnZombieGrowl(string zombieType)
    {
        if(zombieType == "Basic") 
            AssetManager.Instance().GetSound("BasicGrowl").Play(m_Volume, 0.0f, 0.0f);
        else if(zombieType == "Brute")
            AssetManager.Instance().GetSound("BruteGrowl").Play(m_Volume, 0.0f, 0.0f);
        else 
            AssetManager.Instance().GetSound("DenizenGrowl").Play(m_Volume, 2.0f, 0.0f);
    }

    public void OnBarricadeHit()
    {
        //AssetManager.Instance().GetSound("Barricade").Play(m_Volume, 0.0f, 0.0f);
    }
    #endregion
}