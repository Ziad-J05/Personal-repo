using System;
using System.Collections.Generic;
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
        private List<Particle> particles;

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
            InitializeTrail(particleCount, trailLength);
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

            // draw trail
            for(int i = 0; i < particles.Count; i++)
            {
                Particle particle = particles[i];
                sb.Draw(
                    particleTexture,
                    new Rectangle((int)particle.X, (int)particle.Y, particleTexture.Width, particleTexture.Height),
                    null,
                    new Color(Color.White, (float)(particles.Count - i)/(float)particles.Count),
                    particle.Rot,
                    new Vector2 (particleTexture.Width/2, particleTexture.Height/2),
                    SpriteEffects.None,
                    (i/particles.Count)
                    );
            }
        }

        // Helper Methods

        /// <summary>
        /// Updates the posittion and rotation of every particle in the trail
        /// </summary>
        private void UpdateParticles()
        {
            // assign y-value of each particle to the one in front of it
            for (int i = particles.Count - 1; i >= 0; i--) 
            {
                // rotate all particles
                particles[i].UpdateRot(MathF.PI / 32);

                if (i > 0)
                {
                    Particle particle = particles[i];
                    Particle next = particles[i - 1];

                    particle.Y = next.Y;
                }
                else
                {
                    // assign y-value of first particle to ship's y-value
                    particles[i].Y = pos.Y;
                }
            }
        }

        /// <summary>
        /// Instantiate every particle in the ship's trail and create a list to store them in
        /// </summary>
        private void InitializeTrail(int particleCount, int trailLength) 
        { 
            // define start and end points
            Vector2 initial = pos;
            Vector2 direction = new Vector2(-trailLength, 0);

            // create particle list and every particle in the trail
            particles = new List<Particle>(particleCount);
            for (float i = 0f; i < particleCount; i++)
            {
                // define position and rotation based on index
                particles.Add(new Particle( 
                    initial + (((i) / (particleCount - 1)) * direction), // P + tv, where t = i/(n-1)
                    MathF.PI * (i+1)/particleCount // pi radians * n/t
                    ));
            }
        }
    }
}
