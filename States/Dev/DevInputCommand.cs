using MonoGameLearning.Engine.Input;

namespace MonoGameLearning.Input
{
    public class DevInputCommand : BaseInputCommand
    {
        // Out of Game Commands
        public class DevQuit : DevInputCommand { }
        public class DevShoot : DevInputCommand { }
    }
}
