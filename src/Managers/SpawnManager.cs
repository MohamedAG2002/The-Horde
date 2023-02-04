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
    public Vector2 Position {get; set;}
    private EntityManager m_EntityManager;
    private int m_Timer;
    private int m_MaxTime;
    private int m_DifficultyTimer;
    private int m_SpawnCounter;
    #endregion

    #region Constructor
    public SpawnManager(EntityManager entityManager, Vector2 position)
    {
        Position = position;

        m_EntityManager = entityManager;

        m_Timer = 0;
        m_MaxTime = 150;
        m_DifficultyTimer = 0;
        m_SpawnCounter = 1;
    }
    #endregion

    #region Methods
    public void Update()
    {
        m_Timer++;

        // This timer will define the difficulty of the game.
        // Once this timer is passed a certain threshold, the zombies will begin to spawn more frequently.
        m_DifficultyTimer++;

        // The max time will decrease by 10 every 1000 ticks
        if(m_DifficultyTimer % 1000 == 0)
            m_MaxTime -= 10;

        if(m_Timer >= m_MaxTime)
        {
            // Adding a zombie
            if(m_SpawnCounter % 10 == 0) SpawnEntity(ZombieType.Brute);
            else if(m_SpawnCounter % 5 == 0) SpawnEntity(ZombieType.Denizen);
            else SpawnEntity(ZombieType.Basic);
            
            // Reseting the position randomly
            Position = new Vector2((float)Game1.Random.Next(64, Game1.ScreenWidth - 64), 0.0f);

            // Reseting the timer
            m_Timer = 0;

            // Everytime a zombie spawns this counter will go up.
            // This will help with selecting which zombie to spawn next turn.
            // For example, if every 5  there is a brute zombie, and every 3 there is a denizen zombie.
            m_SpawnCounter++;
        }
    }

    // Spawns an entity based on the given type
    public void SpawnEntity(ZombieType type)
    {
        switch(type)
        {
            case ZombieType.Basic:
                m_EntityManager.Entities.Add(new Zombie(Position, 
                                                        AssetManager.Instance().GetSprite("BasicZombie"),  
                                                        BASIC_HEALTH, 
                                                        BASIC_DAMAGE, 
                                                        BASIC_SPEED));
                break;
            case ZombieType.Brute:
                m_EntityManager.Entities.Add(new Zombie(Position, 
                                                        AssetManager.Instance().GetSprite("BruteZombie"),  
                                                        BRUTE_HEALTH, 
                                                        BRUTE_DAMAGE, 
                                                        BRUTE_SPEED));
                break;
            case ZombieType.Denizen:
                m_EntityManager.Entities.Add(new Zombie(Position, 
                                                        AssetManager.Instance().GetSprite("DenizenZombie"),  
                                                        DENIZEN_HEALTH, 
                                                        DENIZEN_DAMAGE, 
                                                        DENIZEN_SPEED));
                break;
        }
    }
    #endregion
}