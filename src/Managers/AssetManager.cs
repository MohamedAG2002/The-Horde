using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using System.Collections.Generic;

namespace TheHorde;

public sealed class AssetManager
{
    #region Fields
    private static readonly AssetManager m_Instance = new AssetManager();
    private Dictionary<string, Texture2D> m_SpriteDict = new Dictionary<string, Texture2D>();
    private Dictionary<string, Texture2D> m_TileDict = new Dictionary<string, Texture2D>();
    private Dictionary<string, SoundEffect> m_SoundDict = new Dictionary<string, SoundEffect>();
    private Dictionary<string, SpriteFont> m_FontDict = new Dictionary<string, SpriteFont>();
    #endregion

    #region Constructor
    private AssetManager()
    {}
    #endregion

    #region Methods
    public static AssetManager Instance()
    {
        return m_Instance;
    }

    // Loads all of the assets at once
    public void LoadAssets(ContentManager content)
    {
        LoadSprites(content);
        LoadTiles(content);
        LoadSounds(content);
        LoadFonts(content);
    }

    // Loads only the sprites
    public void LoadSprites(ContentManager content)
    {
        m_SpriteDict.Add("Player", content.Load<Texture2D>("Sprites/player"));
        m_SpriteDict.Add("BasicZombie", content.Load<Texture2D>("Sprites/basic_zombie"));
        m_SpriteDict.Add("DenizenZombie", content.Load<Texture2D>("Sprites/denizen_zombie"));
        m_SpriteDict.Add("BruteZombie", content.Load<Texture2D>("Sprites/brute_zombie"));
    }

    // Loads only the tiles
    public void LoadTiles(ContentManager content)
    {
        m_TileDict.Add("Box1", content.Load<Texture2D>("Tiles/box_1"));
        m_TileDict.Add("Box2", content.Load<Texture2D>("Tiles/box_2"));
        m_TileDict.Add("Flower1", content.Load<Texture2D>("Tiles/flower_1"));
        m_TileDict.Add("Flower2", content.Load<Texture2D>("Tiles/flower_2"));
        m_TileDict.Add("GrassPatch", content.Load<Texture2D>("Tiles/grass_patch"));
        m_TileDict.Add("Grass", content.Load<Texture2D>("Tiles/grass"));
        m_TileDict.Add("Rock", content.Load<Texture2D>("Tiles/rock"));
        m_TileDict.Add("WitheredBush", content.Load<Texture2D>("Tiles/withered_bush"));
        m_TileDict.Add("WitheredGrass", content.Load<Texture2D>("Tiles/withered_grass"));
    }

    // Loads only the sounds
    public void LoadSounds(ContentManager content)
    {
        m_SoundDict.Add("BasicGrowl", content.Load<SoundEffect>("Audio/basic_zombie"));
        m_SoundDict.Add("BruteGrowl", content.Load<SoundEffect>("Audio/brute_zombie"));
        m_SoundDict.Add("DenizenGrowl", content.Load<SoundEffect>("Audio/denizen_zombie"));
        m_SoundDict.Add("Pistol", content.Load<SoundEffect>("Audio/pistol"));
        m_SoundDict.Add("Shotgun", content.Load<SoundEffect>("Audio/shotgun"));
    }

    // Loads only the font
    public void LoadFonts(ContentManager content)
    {
        m_FontDict.Add("MainFont", content.Load<SpriteFont>("Font/font"));
    }

    // Returns a specific sprite
    public Texture2D GetSprite(string spriteName)
    {
        return m_SpriteDict[spriteName];
    }

    // Returns a specific tile
    public Texture2D GetTile(string tileName)
    {
        return m_TileDict[tileName];
    }

    // Returns a specific sound
    public SoundEffect GetSound(string soundName)
    {
        return m_SoundDict[soundName];
    }

    // Returns a specific font
    public SpriteFont GetFont(string fontName)
    {
        return m_FontDict[fontName];
    }
    #endregion
}