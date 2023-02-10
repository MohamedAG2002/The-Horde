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

    private bool m_IsAnimating;
    private int m_CurrentFrame;
    private int m_Timer;
    private int m_AnimDirection;
    #endregion

    #region Constructor
    public Animation(Texture2D spriteSheet, int frames, int frameSpeed)
    {
        SpriteSheet = spriteSheet;
        
        Frames = frames;
        FrameSpeed = frameSpeed;

        SpriteWidth = SpriteSheet.Width / Frames;
        SpriteHeight = SpriteSheet.Height;

        m_IsAnimating = true;
        m_CurrentFrame = 0;
        m_Timer = 0;
        m_AnimDirection = 0;
    }
    #endregion

    #region Method
    public void Render(SpriteBatch spriteBatch, Vector2 position)
    {
        spriteBatch.Draw(SpriteSheet, 
                         new Rectangle((int)position.X, (int)position.Y, SpriteSheet.Width / 4, SpriteSheet.Height),
                         new Rectangle((SpriteSheet.Width / Frames) * m_CurrentFrame, 0, SpriteSheet.Width / 4, SpriteSheet.Height), 
                         Color.White);
    }

    // Plays an animation at a fixed speed
    public void Play()
    {
        // Switching the direction of the frames once it's on either side of the spritesheet
        if(m_CurrentFrame == Frames - 1) m_AnimDirection = -1;
        else if(m_CurrentFrame == 0) m_AnimDirection = 1;

        if(!m_IsAnimating) return;

        // Increasing the timer for the animations to progress
        m_Timer++;

        if(m_Timer >= FrameSpeed)
        {
            // Either increases or decreaces the current frame, depending on the direction(either goes back or forwards, basically)
            m_CurrentFrame += m_AnimDirection;

            m_Timer = 0;
        }
    }

    public void Stop()
    {
        m_IsAnimating = false;
    }
    #endregion
}