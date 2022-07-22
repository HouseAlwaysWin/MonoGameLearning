using System.Security.Authentication.ExtendedProtection;
using System;
using System.Collections.Generic;
using System.Linq;


using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLearning.Enum;
using MonoGameLearning.Engine.Objects;
using Microsoft.Xna.Framework;
using MonoGameLearning.Engine.Input;
using Microsoft.Xna.Framework.Audio;
using MonoGameLearning.Engine.Sound;

namespace MonoGameLearning.Engine.States
{
    public abstract class BaseGameState
    {
        private const string FallbackTexture = "png/Empty";
        private const string FallbackSong = "sounds/empty";
        protected bool _debug = true;
        private ContentManager _contentManager;
        protected int _viewportHeight;
        protected int _viewportWidth;
        protected SoundManager _soundManager = new SoundManager();
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
        public abstract void UpdateGameState(GameTime gameTime);
        public void Update(GameTime gameTime)
        {
            UpdateGameState(gameTime);
            _soundManager.PlaySoundtrack();
        }
        protected abstract void SetInputManager();

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

        protected SoundEffect LoadSound(string soundName)
        {
            return _contentManager.Load<SoundEffect>(soundName);
        }

        public abstract void HandleInput(GameTime gameTime);

        public event EventHandler<BaseGameState> OnStateSwitched;

        public event EventHandler<BaseGameStateEvent> OnEventNotification;


        protected void NotifyEvent(BaseGameStateEvent gameEvent)
        {
            OnEventNotification?.Invoke(this, gameEvent);

            foreach (var gameObject in _gameObjects)
            {
                gameObject.OnNotify(gameEvent);
            }
            _soundManager.OnNotify(gameEvent);
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
                if (_debug)
                {
                    gameObject.RenderBoundingBoxes(spriteBatch);
                }

                gameObject.Render(spriteBatch);
            }
        }
    }
}