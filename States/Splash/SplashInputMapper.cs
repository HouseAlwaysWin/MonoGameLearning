using Microsoft.Xna.Framework.Input;
using MonoGameLearning.Engine.Input;
using System.Collections.Generic;

namespace MonoGameLearning.Splash.Gameplay
{
    public class SplashInputMapper : BaseInputMapper
    {
        public override IEnumerable<BaseInputCommand> GetKeyboardState(KeyboardState state)
        {
            var commands = new List<SplashInputCommand>();

            if (state.IsKeyDown(Keys.Enter))
            {
                commands.Add(new SplashInputCommand.GameSelect());
            }

            return commands;
        }
    }
}