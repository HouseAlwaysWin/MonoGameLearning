using MonoGameLearning.Engine.Objects;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameLearning.Objects.Text
{
    public class GameOverText : BaseTextObject
    {
        public GameOverText(SpriteFont font)
        {
            _font = font;
            Text = "Game Over";
        }
    }
}
