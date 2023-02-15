using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheHorde;

public class MainMenuScene : IScene
{
    #region Event related
    public delegate void SceneChange(SceneType sceneType);
    public static event SceneChange SceneChangeEvent;
    #endregion

    #region Fields
    private string m_Title, m_PlayText, m_CreditsText, m_ExitText;
    #endregion

    #region Constructor
    public MainMenuScene()
    {
        m_Title = "The Horde";
        m_PlayText = "[ENTER] PLAY";
        m_CreditsText = "[C] CREDITS";
        m_ExitText = "[ESC] EXIT";
    }
    #endregion

    #region Methods
    public void Update(GameTime gameTime)
    {
        /* Transitioning from the menu to other scenes */
        // From Menu to Game
        if(Keyboard.GetState().IsKeyDown(Keys.Enter) && Keyboard.GetState().IsKeyUp(Keys.Enter))
            SceneChangeEvent?.Invoke(SceneType.Game);
        // From Menu to Credits
        else if(Keyboard.GetState().IsKeyDown(Keys.C) && Keyboard.GetState().IsKeyUp(Keys.C))
            SceneChangeEvent?.Invoke(SceneType.Credits);
    }
    
    public void Render(SpriteBatch spriteBatch)
    {
        SpriteFont largeFont = AssetManager.Instance().GetFont("Large");
        SpriteFont mediumFont = AssetManager.Instance().GetFont("Medium");

        // Title render
        spriteBatch.DrawString(largeFont, m_Title, new Vector2(Game1.CenterText(largeFont, m_Title).X, 10.0f), Color.Black);
    }
    #endregion
}