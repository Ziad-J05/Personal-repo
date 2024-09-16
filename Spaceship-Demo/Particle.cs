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
    /// Particle class for rendering the trail effect on the spaceship
    /// </summary>
    public class Particle
    {
        private Vector2 pos;
        private float rot;

        /// <summary>
        /// Position of particle in 2D space
        /// </summary>
        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }

        public float X
        {
            get { return pos.X; }
            set { pos.X = value; }
        }

        public float Y
        {
            get { return pos.Y; }
            set { pos.Y = value; }
        }

        public float Rot { get { return rot; } }

        // Constructors

        /// <summary>
        /// Creates a particle with no rotation
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Particle(float x, float y)
        {
            pos = new Vector2(x, y);
            rot = 0;
        }

        /// <summary>
        /// Creates a particle with no rotation
        /// </summary>
        /// <param name="pos"></param>
        public Particle(Vector2 pos)
        {
            this.pos = pos;
            rot = 0;
        }

        /// <summary>
        /// Creates a particle
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="rot"></param>
        public Particle(float x, float y, float rot)
        {
            pos = new Vector2(x, y);
            this.rot = rot;
        }

        /// <summary>
        /// Creates a particle
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="rot"></param>
        public Particle(Vector2 pos, float rot)
        {
            this.pos = pos;
            this.rot = rot;
        }

        // Methods

        /// <summary>
        /// Rotates the particle
        /// </summary>
        public void UpdateRot(float rotSpeed)
        {

        }
    }
}
