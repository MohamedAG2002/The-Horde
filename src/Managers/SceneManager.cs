using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

namespace TheHorde;

public class SceneManager
{
    #region Fields
    public List<IScene> Scenes {get;} = new List<IScene>();
    public SceneType Type {get; set;}
    public IScene CurrentScene;
    #endregion

    #region Constructor
    public SceneManager()
    {
        Type = SceneType.Game;

        // Adding the scenes
        Scenes.Add(new MainMenuScene());
        Scenes.Add(new GameScene());
        Scenes.Add(new SettingScene());
        Scenes.Add(new HelpScene());
        Scenes.Add(new OverScene());

        // Setting the current scene
        CurrentScene = Scenes[1] as GameScene;
    }
    #endregion

    #region Methods
    public void Update(GameTime gameTime)
    {
        // Only updating the current scene
        CurrentScene.Update(gameTime);
    }
    
    public void Render(SpriteBatch spriteBatch)
    {
        // Only rendering the current scene
        CurrentScene.Render(spriteBatch);
    }
    #endregion
}