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
            UpdateParticles(particles.Count);
        }

        /// <summary>
        /// Draw the spaceship and its trail
        /// </summary>
        public void Draw()
        {

        }

        // Helper Methods

        
        private Vector2 UpdateParticles(int particleIndex)
        {
            // recursive case: assign position of particle to the next one in line
            if (particleIndex > 0)
            {
                return UpdateParticles(particleIndex - 1);
            }
            // base case: assign position of particle to ship position
            else
            {
                return pos;
            }
            // so it compiles
            return Vector2.One;
        }

        /// <summary>
        /// Instantiate every particle in the ship's trail and create a list to store them in
        /// </summary>
        private void InitializeTrail() 
        { 
        }
    }
}
