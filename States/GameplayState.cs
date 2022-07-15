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
        private PlayerSprite _playerSprite;

        public override void LoadContent()
        {
            _playerSprite = new PlayerSprite(LoadTexture(PlayerFighter));

            AddGameObject(new TerrainBackground(LoadTexture(BackgroundTexture)));

            var playerXPos = _viewportWidth / 2 - _playerSprite.Width / 2;
            var playerYPos = _viewportHeight - _playerSprite.Height - 30;
            _playerSprite.Position = new Vector2(playerXPos, playerYPos);

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