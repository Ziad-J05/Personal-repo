using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Spaceship_Demo
{
    internal class StarRenderer
    {
        private List<Star> stars;
        private Vector2 origin;
        private Rectangle screen;
        private Texture2D texture;

        public StarRenderer (Vector2 origin, int screenWidth, int screenHeight, Texture2D starTexture)
        {
            this.origin = origin;
            screen = new Rectangle(0, 0, screenWidth, screenHeight);
            stars = new List<Star>();
            texture = starTexture;
        }

        public void AddStars (int numStars)
        {
            Random rng = new Random();
            Vector2 direction;
            double theta;
            for (int i = 0; i < numStars; i++) 
            {
                // generate a random unit vector for each star
                theta = rng.NextDouble() * Math.PI * 2; // random from 0 to 2pi 
                direction = new Vector2((float)Math.Sin(theta), (float)Math.Cos(theta));

                stars.Add(new Star(origin, direction, 1f));
            }
        }

        public void DiscardStars()
        {
            Star star;
            for (int i = stars.Count - 1; i >= 0; i--)
            {
                star = stars[i];
                if (!screen.Contains(star.Pos.X, star.Pos.Y))
                {
                    stars.RemoveAt(i);
                }
            }
        }

        public void MoveStars()
        {
            foreach (Star star in stars)
            {
                star.Pos += star.Velocity;
                star.Velocity = Vector2.Multiply(star.Velocity, 1.1f);
            }
        }

        public void DrawStars(SpriteBatch sb) 
        { 
            foreach(Star star in stars)
            {
                sb.Draw(texture,
                    new Rectangle((int)star.Pos.X + texture.Width / 2, (int)star.Pos.Y + texture.Height / 2, texture.Width, texture.Height),
                    Color.White
                    );
            }
        }
    }
}
