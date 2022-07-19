using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLearning.Engine.States;
using MonoGameLearning.Enum;
using MonoGameLearning.States;
using MonoGameLearning.States.Gameplay;

namespace MonoGameLearning
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private BaseGameState _currentGameState;

        private RenderTarget2D _renderTarget;
        private Rectangle _renderScaleRectangle;

        private int _designedResolutionWidth;
        private int _designedResolutionHeight;

        private float _designedResolutionAspectRadio;

        private BaseGameState _firstGameState;

        public MainGame(int width, int height, BaseGameState firstGameState)
        {

            _graphics = new GraphicsDeviceManager(this);

            // _graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
            // IsMouseVisible = true;

            _firstGameState = firstGameState;
            _designedResolutionHeight = height;
            _designedResolutionWidth = width;
            _designedResolutionAspectRadio = width / (float)height;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = _designedResolutionWidth;
            _graphics.PreferredBackBufferHeight = _designedResolutionHeight;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();

            _renderTarget = new RenderTarget2D(
                graphicsDevice: _graphics.GraphicsDevice,
                width: _designedResolutionWidth,
                height: _designedResolutionHeight,
                mipMap: false,
                preferredFormat: SurfaceFormat.Color,
                preferredDepthFormat: DepthFormat.None,
                preferredMultiSampleCount: 0,
                usage: RenderTargetUsage.DiscardContents);

            _renderScaleRectangle = GetScaleRectangle();

            base.Initialize();
        }

        private Rectangle GetScaleRectangle()
        {
            var variance = 0.5;
            var actualAspectRatio = Window.ClientBounds.Width / (float)Window.ClientBounds.Height;
            Rectangle scaleRectangle;
            if (actualAspectRatio <= _designedResolutionAspectRadio)
            {
                var presentHeight = (int)(Window.ClientBounds.Width / _designedResolutionAspectRadio + variance);
                var barHeight = (Window.ClientBounds.Height - presentHeight) / 2;
                scaleRectangle = new Rectangle(0, barHeight, Window.ClientBounds.Width, presentHeight);
            }
            else
            {
                var presentWidth = (int)(Window.ClientBounds.Height * _designedResolutionAspectRadio + variance);
                var barWidth = (Window.ClientBounds.Width - presentWidth) / 2;
                scaleRectangle = new Rectangle(0, barWidth, Window.ClientBounds.Width, Window.ClientBounds.Height);
            }
            return scaleRectangle;
        }

        private void SwitchGameState(BaseGameState gameState)
        {
            if (_currentGameState != null)
            {
                _currentGameState.OnStateSwitched -= CurrentGameState_OnStateSwitched;
                _currentGameState.OnEventNotification -= _currentGameState_OnEventNotification;
                _currentGameState.UnloadContent();
            }

            _currentGameState = gameState;

            _currentGameState.Initialize(Content,
                        _graphics.GraphicsDevice.Viewport.Height,
                        _graphics.GraphicsDevice.Viewport.Width);

            _currentGameState.LoadContent();

            _currentGameState.OnStateSwitched += CurrentGameState_OnStateSwitched;
            _currentGameState.OnEventNotification += _currentGameState_OnEventNotification;
        }

        private void CurrentGameState_OnStateSwitched(object sender, BaseGameState e)
        {
            SwitchGameState(e);
        }

        private void _currentGameState_OnEventNotification(object sender, BaseGameStateEvent e)
        {
            switch (e)
            {
                case BaseGameStateEvent.GameQuit _:
                    Exit();
                    break;
            }
        }



        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            SwitchGameState(_firstGameState);
        }

        protected override void UnloadContent()
        {
            _currentGameState?.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            _currentGameState.HandleInput(gameTime);
            _currentGameState.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _currentGameState.Render(_spriteBatch);
            _spriteBatch.End();

            _graphics.GraphicsDevice.SetRenderTarget(null);
            _graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.Black, 1.0f, 0);

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);
            _spriteBatch.Draw(_renderTarget, _renderScaleRectangle, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
