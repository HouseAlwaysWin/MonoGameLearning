using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLearning.Engine.Input;
using MonoGameLearning.Engine.States;
using MonoGameLearning.Objects;
using MonoGameLearning.States.Gameplay;

namespace MonoGameLearning.Splash.Gameplay
{
    public class SplashState : BaseGameState
    {
        public override void LoadContent()
        {
            // TODO: Add Content Loading

            AddGameObject(new SplashImage(LoadTexture("png/splash")));
        }

        public override void HandleInput(GameTime gameTime)
        {
            var state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Enter))
            {

                SwitchState(new GameplayState());
            }
        }

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new SplashInputMapper());
        }

        public override void UpdateGameState(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }
    }
}