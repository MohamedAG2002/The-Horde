using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheHorde;

public class Tile
{
    #region Fields  
    public Vector2 Position {get; set;}
    public Texture2D Texture {get; private set;}
    public bool IsSolid {get; private set;}
    #endregion

    #region Constructor
    public Tile(Vector2 position, Texture2D texture, bool isSolid)
    {
        Position = position;
        Texture = texture;
        IsSolid = isSolid;
    }
    #endregion

    #region Methods
    public void Render(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, Position, Color.White);
    }
    #endregion
}