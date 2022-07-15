using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using MonoGameLearning.Enum;
using MonoGameLearning.Objects;
using MonoGameLearning.States.Base;

namespace MonoGameLearning.States
{
    public class GameplayState : BaseGameState
    {

        private const string PlayerFighter = "fighter";
        private const string BackgroundTexture = "Barren";

        public override void LoadContent()
        {
            AddGameObject(new SplashImage(LoadTexture(BackgroundTexture)));
            AddGameObject(new PlayerSprite(LoadTexture(PlayerFighter)));
        }

        public override void HandleInput()
        {
            var state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Escape))
            {
                NotifyEvent(Events.GAME_QUIT);
            }
            // if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            //     Keyboard.GetState().IsKeyDown(Keys.Enter))
            // {
            //     NotifyEvent(Events.GAME_QUIT);
            // }
        }
    }
}