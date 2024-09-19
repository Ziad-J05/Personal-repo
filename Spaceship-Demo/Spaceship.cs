using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spaceship_Demo
{
    /// <summary>
    /// Creates a spaceship that moves based on player input
    /// </summary>
    internal class Spaceship
    {
        private Vector2 pos;
        private float yVelocity;
        private float drag;
        private Texture2D shipTexture;
        private Texture2D particleTexture;
        private List<Particle> trail1;
        private List<Particle> trail2;

        private bool moveDir; // true is up

        /// <summary>
        /// Position of the spaceship in 2D space
        /// </summary>
        public Vector2 Pos { get { return pos; } set { pos = value; } }

        // Constructor

        /// <summary>
        /// Create a new spaceship
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="shipTexture"></param>
        /// <param name="particleTexture"></param>
        /// <param name="trailLength"></param>
        /// <param name="particleCount"></param>
        public Spaceship(Vector2 pos, float drag, Texture2D shipTexture, Texture2D particleTexture, int trailLength, int particleCount)
        {
            this.pos = pos;
            yVelocity = 0f;
            this.drag = drag;
            this.shipTexture = shipTexture;
            this.particleTexture = particleTexture;
            moveDir = true;
            trail1 = new List<Particle>();
            trail2 = new List<Particle>();
            InitializeTrail(trail1, particleCount, trailLength, true);
            InitializeTrail(trail2, particleCount, trailLength, false);
        }

        // Methods

        /// <summary>
        /// Update position of the spaceship and its trail based on player input
        /// </summary>
        /// <param name="kb"></param>
        public void Move(KeyboardState kb)
        {
            // apply drag
            if (yVelocity > 0)
            {
                yVelocity -= drag;
            }
            else
            {
                yVelocity = 0f;
            }

            // reset ship velocity
            if (kb.IsKeyDown(Keys.W))
            {
                yVelocity = 5;
                moveDir = true;
            }
            if (kb.IsKeyDown(Keys.S))
            {
                yVelocity = 5;
                moveDir = false;
            }

            // apply velocity
            if (moveDir)
            {
                pos.Y -= yVelocity;
            }
            else
            {
                pos.Y += yVelocity;
            }

            // update trail position
            UpdateParticles();
        }

        /// <summary>
        /// Draw the spaceship and its trail
        /// </summary>
        public void Draw(SpriteBatch sb)
        {
            // draw ship
            sb.Draw(shipTexture, pos - new Vector2(0, shipTexture.Height/2), Color.White);

            // draw trail1 and trail2
            Particle particle;
            for (int i = 0; i < trail1.Count; i++)
            {
                particle = trail1[i];
                sb.Draw(
                    particleTexture,
                    new Rectangle((int)particle.X, (int)particle.Y, particleTexture.Width, particleTexture.Height),
                    null,
                    new Color(Color.White, (float)(trail1.Count - i)/(float)trail1.Count),
                    particle.Rot,
                    new Vector2 (particleTexture.Width/2, particleTexture.Height/2),
                    SpriteEffects.None,
                    (i/trail1.Count)
                    );

                particle = trail2[i];
                sb.Draw(
                    particleTexture,
                    new Rectangle((int)particle.X, (int)particle.Y, particleTexture.Width, particleTexture.Height),
                    null,
                    new Color(Color.White, (float)(trail2.Count - i) / (float)trail2.Count),
                    particle.Rot,
                    new Vector2(particleTexture.Width / 2, particleTexture.Height / 2),
                    SpriteEffects.FlipHorizontally,
                    (i / trail2.Count)
                    );
            }
        }

        // Helper Methods

        /// <summary>
        /// Updates the posittion and rotation of every particle in the trail
        /// </summary>
        private void UpdateParticles()
        {
            Particle particle;
            Particle next;

            // assign y-value of each particle to the one in front of it
            for (int i = trail1.Count - 1; i >= 0; i--) 
            {
                // rotate all particles
                trail1[i].UpdateRot(MathF.PI / 32);
                trail2[i].UpdateRot(MathF.PI / 32 * -1);

                if (i > 0)
                {
                    particle = trail1[i];
                    next = trail1[i - 1];

                    particle.Y = next.Y;

                    particle = trail2[i];
                    next = trail2[i - 1];

                    particle.Y = next.Y;
                }
                else
                {
                    // assign y-value of first particle to ship's y-value
                    trail1[i].Y = pos.Y;
                    trail2[i].Y = pos.Y;
                }
            }
        }

        /// <summary>
        /// Instantiate every particle in the ship's trail and create a list to store them in
        /// </summary>
        private void InitializeTrail(List<Particle> trail, int particleCount, int trailLength, bool isClockwise) 
        { 
            // define start and end points
            Vector2 initial = pos;
            Vector2 direction = new Vector2(-trailLength, 0);

            // set rotation direction
            float rotDirection;
            if (isClockwise) 
            {
                rotDirection = -1;
            }
            else
            {
                rotDirection = 1;
            }

            // create every particle in the trail
            for (float i = 0f; i < particleCount; i++)
            {
                // define position and rotation based on index
                trail.Add(new Particle( 
                    initial + (((i) / (particleCount - 1)) * direction), // P + tv, where t = i/(n-1)
                    MathF.PI * (i+1)/particleCount * rotDirection // pi radians * n/t
                    ));
            }
        }
    }
}
