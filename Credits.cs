using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace StorybrewScripts
{
    public class Credits : StoryboardObjectGenerator
    {
        FontGenerator Font;
        public override void Generate()
        {
		    Font = GetFont();
            GenerateCredit(328816, 334430, "- BEATMAP -", 220, 0.1f);
            GenerateCredit(331623, 334430, "SOTARKS", 240, 0.3f);

            GenerateCredit(335833, 340044, "- MUSIC EDIT & MIX -", 220, 0.1f);
            GenerateCredit(337237, 340044, "SAKIZ", 240, 0.3f);

            GenerateCredit(341447, 348326, "- STORYBOARD -", 220, 0.1f);
            GenerateCredit(342851, 348326, "PONO", 240, 0.3f);
            
            
        }
        private FontGenerator GetFont()
        {
            var font = LoadFont("sb/t", new FontDescription()
            {
                FontPath = "Righteous-Regular.ttf",
                FontSize = 100,
                Color = Color4.White,
                FontStyle = FontStyle.Regular,
            });
            return font;
        }
        private void GenerateCredit(int startTime, int endTime, string title, float posY, float scale)
        {
            float letterX = 0;
            float lineWidth = 0;
            float lineHeight = 0;
            var delay = 0;
            foreach(var letter in title)
            {
                var texture = Font.GetTexture(letter.ToString());
                lineWidth += texture.BaseWidth * scale;
                lineHeight = Math.Max(lineHeight, texture.BaseHeight * scale);
            }
            letterX = 320 - lineWidth/2;
            posY -= lineHeight/2 * scale;
            foreach(var letter in title)
            {
                var texture = Font.GetTexture(letter.ToString());
                if(!texture.IsEmpty)
                {
                    var position = new Vector2(letterX, posY)
                        + texture.OffsetFor(OsbOrigin.Centre) * scale;
                    
                    var sprite = GetLayer("Credits").CreateSprite(texture.Path, OsbOrigin.Centre, position);
                    sprite.Scale(startTime, scale);
                    sprite.MoveY(OsbEasing.OutExpo, startTime + delay, startTime + delay + 1000, position.Y + 20, position.Y);
                    sprite.Fade(startTime + delay, startTime + delay + 1000, 0, 0.7);
                    sprite.Fade(endTime, endTime + 1000, 0.7, 0);
       
                    delay += 100;
                }
                letterX += texture.BaseWidth * scale;
            }
        }
    }
}
