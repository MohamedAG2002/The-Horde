using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UI;

public class Text
{
    #region Fields
    public Vector2 Position {get; set;}
    public SpriteFont Font {get; set;}
    public string String {get; set;}
    public Color Color {get; set;}
    public Rectangle Bounds {get;}
    #endregion

    #region Constructor
    public Text(SpriteFont font, string text, Vector2 position, Color color)
    {
        Position = position;
        Font = font;
        String = text;
        Color = color;

        Vector2 textSize = Font.MeasureString(String); 
        Bounds = new Rectangle((int)Position.X, (int)Position.Y, (int)textSize.X, (int)textSize.Y);
    }
    #endregion

    #region Methods
    public void Render(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(Font, String, Position, Color);
    }
    #endregion
}  


