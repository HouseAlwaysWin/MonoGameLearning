
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using MonoGameLearning.Engine.Input;
using MonoGameLearning.States.Gameplay;

namespace MMonoGameLearning.States.Gameplay
{
    public class GameplayInputMapper : BaseInputMapper
    {
        public override IEnumerable<BaseInputCommand> GetKeyboardState(KeyboardState state)
        {
            var commands = new List<GameplayInputCommand>();

            if (state.IsKeyDown(Keys.Escape))
            {
                commands.Add(new GameplayInputCommand.GameExit());
            }
            else if (state.IsKeyDown(Keys.Left))
            {
                commands.Add(new GameplayInputCommand.PlayerMoveLeft());
            }
            else if (state.IsKeyDown(Keys.Right))
            {
                commands.Add(new GameplayInputCommand.PlayerMoveRight());
            }
            else if (state.IsKeyDown(Keys.Space))
            {
                commands.Add(new GameplayInputCommand.PlayerShoots());
            }
            else
            {
                commands.Add(new GameplayInputCommand.PlayerStopsMoving());
            }

            return commands;
        }
    }
}
