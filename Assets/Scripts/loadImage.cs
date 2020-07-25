using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class loadImage : MonoBehaviour
{
    public Image playerImage;
    public Text errorMssg;

    public PlayerStats player = new PlayerStats();
    public ConsoleCommands output = new ConsoleCommands();

    public void reloadImage()
    {
        string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Divinity10/NarutoDnD/Game Saves");
        string imageLocation = Path.Combine(directory, player.playerName + ".jpg");

        if (File.Exists(imageLocation))
        {
            print("Image found!");
            playerImage.sprite = LoadNewSprite(imageLocation);
        }
        else
        {
            errorMssg.text = "There is no image for the character.  Using default. For help adding an image, type in the console \"help image\"";
            output.addOutput("There is no image for the character.  Using default. For help adding an image, type in the console \"help image\"");
        }
    }

    public static Sprite LoadNewSprite(string FilePath, float PixelsPerUnit = 100.0f, SpriteMeshType spriteType = SpriteMeshType.Tight)
    {

        // Load a PNG or JPG image from disk to a Texture2D, assign this texture to a new sprite and return its reference

        Texture2D SpriteTexture = LoadTexture(FilePath);
        Sprite NewSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0, 0), PixelsPerUnit, 0, spriteType);

        return NewSprite;
    }

    public static Sprite ConvertTextureToSprite(Texture2D texture, float PixelsPerUnit = 100.0f, SpriteMeshType spriteType = SpriteMeshType.Tight)
    {
        // Converts a Texture2D to a sprite, assign this texture to a new sprite and return its reference

        Sprite NewSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0), PixelsPerUnit, 0, spriteType);

        return NewSprite;
    }

    public static Texture2D LoadTexture(string FilePath)
    {

        // Load a PNG or JPG file from disk to a Texture2D
        // Returns null if load fails

        Texture2D Tex2D;
        byte[] FileData;

        if (File.Exists(FilePath))
        {
            FileData = File.ReadAllBytes(FilePath);
            Tex2D = new Texture2D(2, 2);           // Create new "empty" texture
            if (Tex2D.LoadImage(FileData))           // Load the imagedata into the texture (size is set automatically)
                return Tex2D;                 // If data = readable -> return texture
        }
        return null;                     // Return null if load failed
    }
}
