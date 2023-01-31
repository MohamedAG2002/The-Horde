using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

namespace TheHorde;

public class TileManager
{
    #region Fields
    private const int TILE_SIZE = 32;
    private List<Tile> m_Tiles = new List<Tile>();
    private List<Tile> m_Props = new List<Tile>();
    private Texture2D m_SrcTexture;
    private Color[] m_RawData;
    #endregion

    #region Constructor
    public TileManager()
    {
        // Setting the main texture

        // Setting the size of the array for the raw data
        m_RawData = new Color[m_SrcTexture.Width * m_SrcTexture.Height];

        // Getting the raw color data from the texture
        m_SrcTexture.GetData<Color>(m_RawData);

        // Adds the tiles to the list
        for(int i = 0; i < m_SrcTexture.Height; i++)
        {
            for(int j = 0; j < m_SrcTexture.Width; j++)
            {
                // Getting the color of the current pixel
                Color currentColor = m_RawData[i * m_SrcTexture.Width + j];

                /* Tiles */
                // Yellow = Withered grass
                // Green = Grass
                // Yellow green = Grass patch
                
                /* Props */
                // Brown = Box1
                // Rosy brown = Box2
                // Red = Flower1
                // Pink = Flower2
                // Grey = Rock
                // Black = Withered bush
            }
        }
    }
    #endregion

    #region Methods
    public void Render(SpriteBatch spriteBatch)
    {
        // SPawning tiles before the props
        foreach(var tile in m_Tiles)
        {
            tile.Render(spriteBatch);
        }

        foreach(var prop in m_Props)
        {
            prop.Render(spriteBatch);
        }
    }
    #endregion
}