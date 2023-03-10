using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheHorde;

public class HelpScene : IScene
{
    #region Event related
    public delegate void SceneChange(SceneType sceneType);
    public static event SceneChange SceneChangeEvent;
    #endregion

    #region Fields
    private string m_Title, m_ToMenuText;
    private KeyboardState m_CurrentState, m_PreviousState;
    #endregion

    #region Constructor
    public HelpScene()
    {
        m_Title = "HELP";
        m_ToMenuText = "[M] MENU";

        m_CurrentState = Keyboard.GetState();
        m_PreviousState = m_CurrentState;
    }
    #endregion

    #region Methods
    public void Update(GameTime gameTime)
    {
        m_PreviousState = m_CurrentState;
        m_CurrentState = Keyboard.GetState();

        // From Help to Menu
        if(m_CurrentState.IsKeyDown(Keys.M) && m_PreviousState.IsKeyUp(Keys.M))
            SceneChangeEvent?.Invoke(SceneType.Menu);
    }

    public void Render(SpriteBatch spriteBatch)
    {
        SpriteFont largeFont = AssetManager.Instance().GetFont("Large");
        SpriteFont mediumFont = AssetManager.Instance().GetFont("Medium");
        SpriteFont smallFont = AssetManager.Instance().GetFont("Small");

        // Title
        spriteBatch.DrawString(largeFont, m_Title, new Vector2(Game1.CenterText(largeFont, m_Title).X, 10.0f), Color.CadetBlue);

        // Controls
        spriteBatch.DrawString(mediumFont, "CONTROLS:", new Vector2(20.0f, 150.0f), Color.Blue);

        // CONTROLS: Movement
        spriteBatch.DrawString(smallFont, "Use WASD or ARROWS to move the player", new Vector2(20.0f, 200.0f), Color.MediumPurple);
        
        // CONTROLS: Switching guns
        spriteBatch.DrawString(smallFont, "Use Q and E to switch between weapons", new Vector2(20.0f, 230.0f), Color.MediumPurple);

        // To menu text render
        spriteBatch.DrawString(mediumFont, m_ToMenuText, new Vector2(Game1.CenterText(mediumFont, m_ToMenuText).X, 300.0f), Color.CadetBlue);
    }
    #endregion
}