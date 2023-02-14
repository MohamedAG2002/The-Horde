using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheHorde;

public class SceneManager
{
    #region Fields
    public SceneType Type {get; set;}
    public IScene CurrentScene;
    private bool m_IsSceneChanged;
    #endregion

    #region Constructor
    public SceneManager()
    {
        Type = SceneType.Game;

        CurrentScene = new MainMenuScene();

        m_IsSceneChanged = false;

        // Subscribing to events
        MainMenuScene.SceneChangeEvent += OnSceneChange;
    }
    #endregion

    #region Methods
    public void Update(GameTime gameTime)
    {
        // Only updating the current scene
        CurrentScene.Update(gameTime);

        // Only loading the scene when there is a change in the state
        if(!m_IsSceneChanged) return;

        switch(Type)
        {
            case SceneType.Menu:
                CurrentScene = new MainMenuScene();
                break;
            case SceneType.Game:
                CurrentScene = new GameScene();
                break;
            case SceneType.Setting:
                CurrentScene = new SettingScene();
                break;
            case SceneType.Help:
                CurrentScene = new HelpScene();
                break;
            case SceneType.Over:
                CurrentScene = new OverScene();
                break;
        }

        // Making sure that the scene loads once not every frame
        m_IsSceneChanged = false;
    }
    
    public void Render(SpriteBatch spriteBatch)
    {
        // Only rendering the current scene
        CurrentScene.Render(spriteBatch);
    }

    public void OnSceneChange(SceneType sceneType)
    {
        Type = sceneType;

        m_IsSceneChanged = true;
    }
    #endregion
}