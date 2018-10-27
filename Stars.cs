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
    public class Stars : StoryboardObjectGenerator
    {
        private Vector2 Centre = new Vector2(320, 240);
        public override void Generate()
        {
		    GenerateEffect(199829, 210283);
		    GenerateEffect(547688, 564222);
            
        }
        private void GenerateEffect(int startTime, int endTime)
        {
            for(int i = startTime; i < endTime; i += 50)
            {
                var particleDuration = Random(5000, 10000);
                var radius = Random(50, 300);
                var angle = Random(0, Math.PI*2);
                var position = new Vector2(
                    (float)(320 + Math.Cos(angle) * radius),
                    (float)(240 + Math.Sin(angle) * radius)
                );
                var sprite = GetLayer("").CreateSprite("sb/s.png");
                sprite.Move(OsbEasing.OutSine, i, i + particleDuration, Centre, position);
                sprite.Fade(i, i + particleDuration, 1, 0);
                sprite.Scale(i, i + particleDuration, 0, 0.05);
            }
        }
    }
}
