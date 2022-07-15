using Microsoft.Xna.Framework.Graphics;
using MonoGameLearning.Objects.Base;

namespace MonoGameLearning.Objects
{
    public class PlayerSprite : BaseGameObject
    {
        public PlayerSprite(Texture2D texture)
        {
            _texture = texture;
        }
    }
}