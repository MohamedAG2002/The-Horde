using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheHorde;

public class OverScene : IScene
{
    #region Event related
    public delegate void SceneChange(SceneType sceneType);
    public static event SceneChange SceneChangeEvent;
    #endregion

    #region Fields
    
    #endregion

    #region Constructor
    
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