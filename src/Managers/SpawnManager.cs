using Microsoft.Xna.Framework;

namespace TheHorde;

public class SpawnManager
{
    #region Conts
    // Basic zombie consts
    private const int BASIC_HEALTH = 40;
    private const int BASIC_DAMAGE = 15;
    private const float BASIC_SPEED = 75.0f;
    
    // Brute zombie consts
    private const int BRUTE_HEALTH = 100;
    private const int BRUTE_DAMAGE = 40;
    private const float BRUTE_SPEED = 45.0f;
    
    // Denizen zombie consts
    private const int DENIZEN_HEALTH = 20;
    private const int DENIZEN_DAMAGE = 8;
    private const float DENIZEN_SPEED = 125.0f;
    #endregion

    #region Fields
    private EntityManager m_EntityManager;
    private int m_Timer;
    private int m_MaxTime;
    private bool m_IsAbleToSpawn;
    #endregion

    #region Constructor
    public SpawnManager(EntityManager entityManager)
    {
        m_EntityManager = entityManager;
        
        m_Timer = 0;
        m_MaxTime = 100;
        m_IsAbleToSpawn = true;
    }
    #endregion

    #region Methods
    public void Update()
    {

    }
    #endregion
}