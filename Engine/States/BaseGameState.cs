using System.Security.Authentication.ExtendedProtection;
using System;
using System.Collections.Generic;
using System.Linq;


using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLearning.Enum;
using MonoGameLearning.Objects.Base;
using Microsoft.Xna.Framework;
using MonoGameLearning.Engine.Input;

namespace MonoGameLearning.Engine.States
{
    public abstract class BaseGameState
    {
        private const string FallbackTexture = "Empty";
        private ContentManager _contentManager;
        protected int _viewportHeight;
        protected int _viewportWidth;
        private readonly List<BaseGameObject> _gameObjects = new List<BaseGameObject>();

        protected InputManager InputManager { get; set; }

        public void Initialize(ContentManager contentManager, int viewportHeight, int viewportWidth)
        {
            _contentManager = contentManager;
            _viewportHeight = viewportHeight;
            _viewportWidth = viewportWidth;
            SetInputManager();
        }

        public abstract void LoadContent();
        public virtual void Update(GameTime gameTime) { }
        public abstract void SetInputManager();

        public void UnloadContent()
        {
            _contentManager.Unload();
        }

        protected void RemoveGameObject(BaseGameObject gameObject)
        {
            _gameObjects.Remove(gameObject);
        }

        // public abstract void UnloadContent(ContentManager contentManager);
        protected Texture2D LoadTexture(string textureName)
        {
            var texture = _contentManager.Load<Texture2D>(textureName);
            return texture ?? _contentManager.Load<Texture2D>(FallbackTexture);
        }

        public abstract void HandleInput(GameTime gameTime);

        public event EventHandler<BaseGameState> OnStateSwitched;

        public event EventHandler<Events> OnEventNotification;


        protected void NotifyEvent(Events eventType, object argument = null)
        {
            OnEventNotification?.Invoke(this, eventType);

            foreach (var gameObject in _gameObjects)
            {
                gameObject.OnNotify(eventType);
            }
        }

        protected void SwitchState(BaseGameState gameState)
        {
            OnStateSwitched?.Invoke(this, gameState);
        }

        protected void AddGameObject(BaseGameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }

        public void Render(SpriteBatch spriteBatch)
        {
            foreach (var gameObject in _gameObjects.OrderBy(a => a.zIndex))
            {
                gameObject.Render(spriteBatch);
            }
        }
    }
}