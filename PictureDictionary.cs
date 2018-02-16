using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace AutoClicker
{
    public class PictureDictionary
    {
        public static Dictionary<string, Bitmap> Dictionary = new Dictionary<string, Bitmap>
        {
            { "radioCopperRock", (Bitmap)Image.FromFile("Images/Mining/Swatches/CopperSwatch.png") },
            { "radioTinRock", (Bitmap)Image.FromFile("Images/Mining/Swatches/CopperSwatch.png") },
            { "radioClayRock", (Bitmap)Image.FromFile("Images/Mining/Swatches/CopperSwatch.png") },
            { "radioIronRock", (Bitmap)Image.FromFile("Images/Mining/Swatches/CopperSwatch.png") },
            { "radioSilverRock", (Bitmap)Image.FromFile("Images/Mining/Swatches/CopperSwatch.png") },
            { "radioCoalRock", (Bitmap)Image.FromFile("Images/Mining/Swatches/CopperSwatch.png") },
            { "radioGoldRock", (Bitmap)Image.FromFile("Images/Mining/Swatches/CopperSwatch.png") },
            { "radioGemRock", (Bitmap)Image.FromFile("Images/Mining/Swatches/CopperSwatch.png") },
            { "radioMithrilRock", (Bitmap)Image.FromFile("Images/Mining/Swatches/CopperSwatch.png") },
            { "radioAdamantiteRock", (Bitmap)Image.FromFile("Images/Mining/Swatches/CopperSwatch.png") },
            { "radioRuneRock", (Bitmap)Image.FromFile("Images/Mining/Swatches/CopperSwatch.png") },
            { "radioRegularTree", (Bitmap)Image.FromFile("Images/Woodcutting/Swatches/TreeSwatch.png") },
            { "radioOakTree", (Bitmap)Image.FromFile("Images/Woodcutting/Swatches/OakSwatch.png") },
            { "radioWillowTree", (Bitmap)Image.FromFile("Images/Woodcutting/Swatches/WillowSwatch.png") },
            { "radioMapleTree", (Bitmap)Image.FromFile("Images/Woodcutting/Swatches/MapleSwatch.png") },
            { "radioYewTree", (Bitmap)Image.FromFile("Images/Woodcutting/Swatches/YewSwatch.png") },
            { "radioMagicTree", (Bitmap)Image.FromFile("Images/Woodcutting/Swatches/MagicSwatch.png") },
            { "radiwoMagicTree", Properties.Resources.YewSwatch},
        };
    }
}