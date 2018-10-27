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
using System.IO;
using System.Drawing;

namespace StorybrewScripts
{
    public class PartManager : StoryboardObjectGenerator
    {
        private Section[] BeatmapSection;
        private FontGenerator Font;
        private int[] Colors = {300, 35, 270, 230, 117, 0, 230, 120, 150, 340, 200, 0, 40, 30, 40, 120};
        public override void Generate()
        {
            Font = Generatefont();
            GenerateSections();
        }
        private void RenderSection(Section section)
        {
            float color = Random(0, 360);
            GenerateText(section, section.SongTitle, true, color);
            GenerateText(section, section.ArtistName, false, color);
            GenerateBackground(section);
        }
        private void GenerateBackground(Section section)
        {
            var duration = section.EndTime - section.StartTime;
            var sprite = GetLayer("Background").CreateSprite("sb/b/" + section.Background + ".jpg");
            sprite.Scale(OsbEasing.OutExpo, section.StartTime, section.StartTime + duration/2, 480.0/900, 480.0/1080);
            sprite.Fade(section.StartTime, section.StartTime + 1000, 0, 1);
            sprite.Fade(section.EndTime, section.EndTime + 1000, 1, 0);
            sprite.Additive(section.StartTime, section.EndTime);
        }
        private void GenerateText(Section section, string text, bool title, float color)
        {
            
            float letterX = 0;
            float letterY = title ? 380 : 355;
            float lineWidth = 0;
            float lineHeight = 0;
            float scale = title ? 0.3f : 0.2f;
            float positionX = -20;
            var delay = 0;
            foreach(var letter in text)
            {
                var texture = Font.GetTexture(letter.ToString());
                lineWidth += texture.BaseWidth * scale;
                lineHeight = Math.Max(lineHeight, texture.BaseHeight * scale);
            }
            letterX = positionX;
            letterY -= lineHeight/2 * scale;
            foreach(var letter in text)
            {
                var texture = Font.GetTexture(letter.ToString());
                if(!texture.IsEmpty)
                {
                    var position = new Vector2(letterX, letterY)
                        + texture.OffsetFor(OsbOrigin.Centre) * scale;

                    
                    var sprite = GetLayer("Credits").CreateSprite(texture.Path, OsbOrigin.Centre, position);
                    sprite.Fade(section.StartTime + delay, section.StartTime + delay + 300, 0, 0.9);
                    sprite.Fade(section.EndTime - 1000 + delay, section.EndTime + delay, 0.9, 0);
                    sprite.MoveX(OsbEasing.OutExpo, section.StartTime + delay, section.StartTime + delay + 1000, position.X + 100, position.X);
                    sprite.ScaleVec(section.StartTime + delay, section.StartTime + delay + 300, -scale, scale, scale, scale);

                    if(delay == 0)         
                        sprite.ColorHsb(section.StartTime, Colors[section.Background], 0.6, 0.6);
                        
                    
                    delay += 100;
                }
                letterX += texture.BaseWidth * scale;
            }

            var line = GetLayer("Credits").CreateSprite("sb/p.png", OsbOrigin.CentreRight, new Vector2(748, 420));
            line.ScaleVec(OsbEasing.OutExpo, section.StartTime, section.StartTime + 1000, 0, 2, 766, 2);
            line.ScaleVec(OsbEasing.OutExpo, section.EndTime - 1000, section.EndTime, 766, 2, 0, 2);
            line.MoveX(OsbEasing.OutExpo, section.EndTime - 1000, section.EndTime, 748, -48);
            line.Fade(section.StartTime, section.EndTime, 0.5, 0.5);
        }
        private FontGenerator Generatefont()
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
        private void GenerateSections()
        {       
            var lines = File.ReadAllLines(@"E:\Applications\Storybrew\projects\SwordArtOnline\BeatmapSections.txt");
            BeatmapSection = new Section[lines.Length];
            for(int i = 0; i < lines.Length; i++)
            {
                var values = lines[i].Split(';');
                BeatmapSection[i] = new Section(int.Parse(values[0]), int.Parse(values[1]), values[2], values[3], int.Parse(values[4]));
                RenderSection(BeatmapSection[i]);
            }
        }
        private class Section
        {
            public int StartTime {get;}
            public int EndTime{get;}
            public string SongTitle {get;}
            public string ArtistName {get;}
            public int Background {get;}
            public Section(int StartTime, int EndTime, string SongTitle, string ArtistName, int Background)
            {
                this.StartTime = StartTime;
                this.SongTitle = SongTitle;
                this.ArtistName = ArtistName;
                this.Background = Background;
                this.EndTime = EndTime;
            }
        }
    }
}