using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLearning.Objects;

namespace MonoGameLearning.States.Base
{
    public class SplashState : BaseGameState
    {
        public override void LoadContent()
        {
            // TODO: Add Content Loading

            AddGameObject(new SplashImage(LoadTexture("splash")));
        }

        public override void HandleInput()
        {
            var state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Enter))
            {

                SwitchState(new GameplayState());
            }

            // if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            //     Keyboard.GetState().IsKeyDown(Keys.Enter))
            // {
            // }
        }
    }
}