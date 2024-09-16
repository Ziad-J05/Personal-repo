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
        private float gravity;
        private Texture2D shipTexture;
        private Texture2D particleTexture;
        private List<Particle> particles;
        private int trailLength;
        private int particleCount; // TODO: this isnt necessary since it isnt used after initialization

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
        public Spaceship(Vector2 pos, float gravity, Texture2D shipTexture, Texture2D particleTexture, int trailLength, int particleCount)
        {
            this.pos = pos;
            yVelocity = 0f;
            this.gravity = gravity;
            this.shipTexture = shipTexture;
            this.particleTexture = particleTexture;
            this.trailLength = trailLength;
            this.particleCount = particleCount;
            InitializeTrail();
        }

        // Methods

        /// <summary>
        /// Update position of the spaceship and its trail based on player input
        /// </summary>
        /// <param name="kb"></param>
        public void Move(KeyboardState kb)
        {
            UpdateParticles();
        }

        /// <summary>
        /// Draw the spaceship and its trail
        /// </summary>
        public void Draw()
        {

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
                Particle particle = particles[i];
                Particle next = particles[i + 1];

                particle.Y = next.Y;
            }

            // assign y-value of first particle to ship's y-value
            particles[0].Y = pos.Y;

            // rotate all particles
            foreach (Particle particle in particles)
            {
                particle.UpdateRot(3f);
            }
        }



        /// <summary>
        /// Instantiate every particle in the ship's trail and create a list to store them in
        /// </summary>
        private void InitializeTrail() 
        { 
        }
    }
}
