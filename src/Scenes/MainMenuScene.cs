using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GeonBit.UI;
using GeonBit.UI.Entities;

namespace TheHorde;

public class MainMenuScene : IScene
{
    #region Event related
    public delegate void SceneChange(SceneType sceneType);
    public static event SceneChange SceneChangeEvent;
    #endregion

    #region Fields
    private Header m_Title;
    private Panel m_ButtonsPanel; 
    private Button m_PlayButton, m_SettingsButton, m_HelpButton, m_ExitButton;
    #endregion

    #region Constructor
    public MainMenuScene()
    {
        m_Title = new Header("The Horde", Anchor.TopCenter);

        m_ButtonsPanel = new Panel(new Vector2(50, 200));

        UserInterface.Active.AddEntity(m_Title);
        UserInterface.Active.AddEntity(m_ButtonsPanel);
    }
    #endregion

    #region Methods
    public void Update(GameTime gameTime)
    {
        
    }
    
    public void Render(SpriteBatch spriteBatch)
    {

    }
    #endregion
}