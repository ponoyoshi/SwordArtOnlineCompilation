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

namespace StorybrewScripts
{
    public class GlobalScript : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            RemoveBackground();
		    Vignette(834, 782664);
        }
        private void Vignette(int startTime, int endTime)
        {
            var sprite = GetLayer("Vignette").CreateSprite("sb/v.png");
            sprite.Scale(startTime, endTime, 480.0/1080, 480.0/1080);
        }
        private void RemoveBackground()
        {
            var sprite = GetLayer("").CreateSprite("BG.jpg");
            sprite.Fade(0, 0);
        }
    }
}
