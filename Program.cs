using System;
using MonoGameLearning.States;
using MonoGameLearning.States.Gameplay;

namespace MonoGameLearning
{
    public static class Program
    {
        private const int WIDTH = 1280;
        private const int HEIGHT = 720;
        [STAThread]
        static void Main()
        {
            using (var game = new MainGame(1280, 720, new DevState()))
                game.Run();
        }
    }
}
