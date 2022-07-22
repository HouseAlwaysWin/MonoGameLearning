using MonoGameLearning.Enum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLearning.Engine.States;
using System.Collections.Generic;
using System;

namespace MonoGameLearning.Engine.Objects
{
    public class BaseGameObject
    {
        protected Texture2D _texture;
        protected Texture2D _boundingBoxTexture;
        protected Vector2 _position = Vector2.One;

        protected List<BoundingBox> _boundingBoxes = new List<BoundingBox>();
        public List<BoundingBox> BoundingBoxes
        {
            get
            {
                return _boundingBoxes;
            }
        }
        public int zIndex;
        public event EventHandler<BaseGameStateEvent> OnObjectChanged;
        public int Width { get { return _texture.Width; } }
        public int Height { get { return _texture.Height; } }
        public bool Destroyed { get; private set; }

        public virtual Vector2 Position
        {
            get { return _position; }
            set
            {
                var deltaX = value.X - _position.X;
                var deltaY = value.Y - _position.Y;
                _position = value;

                foreach (var bb in _boundingBoxes)
                {
                    bb.Position = new Vector2(bb.Position.X + deltaX, bb.Position.Y + deltaY);
                }
            }
        }
        public virtual void OnNotify(BaseGameStateEvent eventType) { }

        public virtual void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);

        }

        public void RenderBoundingBoxes(SpriteBatch spriteBatch)
        {
            if (_boundingBoxTexture == null)
            {
                CreateBoundingBoxTexture(spriteBatch.GraphicsDevice);
            }

            foreach (var bb in _boundingBoxes)
            {
                spriteBatch.Draw(_boundingBoxTexture, bb.Rectangle, Color.Red);
            }
        }

        public void AddBoundingBox(BoundingBox bb)
        {
            _boundingBoxes.Add(bb);
        }

        private void CreateBoundingBoxTexture(GraphicsDevice graphicsDevice)
        {
            _boundingBoxTexture = new Texture2D(graphicsDevice, 1, 1);
            _boundingBoxTexture.SetData<Color>(new Color[] { Color.White });
        }

        public void SendEvent(BaseGameStateEvent e)
        {
            OnObjectChanged?.Invoke(this, e);
        }

        public void Destroy()
        {
            Destroyed = true;
        }

    }
}