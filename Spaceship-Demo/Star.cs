using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spaceship_Demo
{
    internal class Star
    {
        private Vector2 pos;
        private Vector2 velocity;
        private float size;

        public Vector2 Pos 
        { 
            get { return pos; } 
            set { pos = value; }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public float Size
        {
            get { return size; }
            set { size = value; }
        }

        public Star (Vector2 pos)
        {
            this.pos = pos;
            velocity = Vector2.Zero;
            size = 1f;
        }

        public Star(Vector2 pos, Vector2 velocity, float size)
        {
            this.pos = pos;
            this.velocity = velocity;
            this.size = size;
        }
    }
}
