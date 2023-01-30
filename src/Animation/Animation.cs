using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheHorde;

public class Animation
{
    #region Fields
    public Texture2D SpriteSheet {get; private set;}
    public int Frames {get; set;}
    public int FrameSpeed {get; set;}
    public static int SpriteWidth;
    public static int SpriteHeight;
    private int m_CurrentFrame;
    private int m_Timer;
    #endregion

    #region Constructor
    public Animation(Texture2D spriteSheet, int frames, int frameSpeed)
    {
        SpriteSheet = spriteSheet;
        
        Frames = frames;
        FrameSpeed = frameSpeed;

        SpriteWidth = SpriteSheet.Width / Frames;
        SpriteHeight = SpriteSheet.Height;

        m_CurrentFrame = 0;
    }
    #endregion

    #region Method
    public void Update()
    {
        // Stricting the current frame between 0 and 3
        if(m_CurrentFrame >= Frames - 1) m_CurrentFrame = 0;
    }

    public void Render(SpriteBatch spriteBatch, Vector2 position)
    {
        spriteBatch.Draw(SpriteSheet, position, new Rectangle(SpriteWidth * m_CurrentFrame, 0, SpriteWidth, SpriteHeight), Color.White);
    }

    // Plays an animation at a fixed speed
    public void Play()
    {
        m_Timer++;

        if(m_Timer >= FrameSpeed)
        {
            m_CurrentFrame++;
            m_Timer = 0;
        }
    }
    #endregion
}