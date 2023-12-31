﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace SuperTiled2Unity.Editor
{
    public class AnimationBuilder
    {
        private float m_Fps;
        private float m_Interval;
        private float m_Remainder;

        public AnimationBuilder(float fps, float interval)
        {
            m_Fps = fps;
            m_Interval = interval;
            Sprites = new List<Sprite>();
        }

        public List<Sprite> Sprites { get; private set; }

        public void AddFrames(Sprite sprite, float duration)
        {
            Assert.IsFalse(duration <= 0);

            // We may have time left over from the last time we added frames
            // This is time it will take to get to the next frame given our FPS

            if (m_Remainder >= duration)
            {
                // We will not be adding any frames but pay off duration
                m_Remainder -= duration;
                return;
            }
            else
            {
                // We will be adding some frames once we pay off debt from duration
                duration -= m_Remainder;

                float fNumFrames = duration * m_Fps;
                int iNumFrames = Mathf.CeilToInt(fNumFrames);

                Sprites.AddRange(Enumerable.Repeat(sprite, iNumFrames));

                // What duration is left over from the addition of last frame?
                float partial = iNumFrames - fNumFrames;
                m_Remainder = partial / m_Fps;
            }
        }

        public void HandleInterval()
        {
            return;
            var lastSprite = Sprites[Sprites.Count - 1];
            float fNumFrames = UnityEngine.Random.Range(0, m_Interval) * m_Fps;
            int iNumFrames = Mathf.CeilToInt(fNumFrames);

            Sprites.AddRange(Enumerable.Repeat(lastSprite, iNumFrames));
        }
    }
}
