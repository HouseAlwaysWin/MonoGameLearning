using MonoGameLearning.Engine.Input;
using MonoGameLearning.Engine.States;
using MonoGameLearning.Input;
using MonoGameLearning.Engine.Objects;
using MonoGameLearning.States.Particles;
using Microsoft.Xna.Framework;
using MonoGameLearning.Objects;
using MonoGameLearning.States.Gameplay;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MonoGameLearning.States
{
    /// <summary>
    /// Used to test out new things, like particle engines and shooting missiles
    /// </summary>
    public class DevState : BaseGameState
    {
        private const string ExhaustTexture = "png/Cloud001";
        private const string MissileTexture = "png/Missile05";
        private const string PlayerFighter = "png/Fighter";
        private const string ChopperTexture = "png/chopper-44x99";
        private const string ExplosionTexture = "png/explosion";

        private ExhaustEmitter _exhaustEmitter;
        private ExplosionEmitter _explosionEmitter;
        private MissileSprite _missile;
        private PlayerSprite _player;
        private ChopperSprite _chopper;

        private Texture2D _chopperTexture;
        private Texture2D _explosionTexture;

        private ChopperGenerator _chopperGenerator;

        private List<ChopperSprite> _enemyList = new List<ChopperSprite>();
        private List<ExplosionEmitter> _explosionList = new List<ExplosionEmitter>();


        public override void LoadContent()
        {
            var exhaustPosition = new Vector2(_viewportWidth / 2, _viewportHeight / 2);
            _exhaustEmitter = new ExhaustEmitter(LoadTexture(ExhaustTexture), exhaustPosition);
            AddGameObject(_exhaustEmitter);

            var explostion = new Vector2(300, 200);
            _explosionTexture = LoadTexture(ExplosionTexture);
            _explosionEmitter = new ExplosionEmitter(_explosionTexture, explostion);
            AddGameObject(_explosionEmitter);

            _player = new PlayerSprite(LoadTexture(PlayerFighter));
            _player.Position = new Vector2(500, 500);
            // AddGameObject(_player);
            _chopperTexture = LoadTexture(ChopperTexture);

            _chopper = new ChopperSprite(_chopperTexture, new System.Collections.Generic.List<(int, Vector2)>());
            _chopper.Position = new Vector2(300, 100);
            _chopperGenerator = new ChopperGenerator(_chopperTexture, 4, AddChopper);
            _chopperGenerator.GenerateChoppers();
            // AddGameObject(_chopper);

        }

        private void AddChopper(ChopperSprite chopper)
        {
            chopper.OnObjectChanged += _chopperSprite_OnObjectChanged;
            _enemyList.Add(chopper);
            AddGameObject(chopper);
        }


        private void _chopperSprite_OnObjectChanged(object sender, BaseGameStateEvent e)
        {
            var chopper = (ChopperSprite)sender;
            switch (e)
            {
                case GameplayEvents.EnemyLostLife ge:
                    if (ge.CurrentLife <= 0)
                    {
                        AddExplosion(new Vector2(chopper.Position.X - 40, chopper.Position.Y - 40));
                        chopper.Destroy();
                    }
                    break;
            }
        }
        private void AddExplosion(Vector2 position)
        {
            var explosion = new ExplosionEmitter(_explosionTexture, position);
            AddGameObject(explosion);
            _explosionList.Add(explosion);
        }


        public override void HandleInput(GameTime gameTime)
        {
            InputManager.GetCommands(cmd =>
            {
                if (cmd is DevInputCommand.DevQuit)
                {
                    NotifyEvent(new BaseGameStateEvent.GameQuit());
                }

                if (cmd is DevInputCommand.DevShoot)
                {
                    _missile = new MissileSprite(LoadTexture(MissileTexture), LoadTexture(ExhaustTexture));
                    _missile.Position = new Vector2(_player.Position.X, _player.Position.Y - 25);
                    AddGameObject(_missile);
                }
            });
        }

        public override void UpdateGameState(GameTime gameTime)
        {
            // _exhaustEmitter.Position = new Vector2(_exhaustEmitter.Position.X, _exhaustEmitter.Position.Y - 3f);
            _exhaustEmitter.Update(gameTime);
            _explosionEmitter.Update(gameTime);

            if (_missile != null)
            {
                _missile.Update(gameTime);

                if (_missile.Position.Y < -100)
                {
                    RemoveGameObject(_missile);
                }
            }

            if (_exhaustEmitter.Position.Y < -200)
            {
                RemoveGameObject(_exhaustEmitter);
            }
        }

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new DevInputMapper());
        }
    }
}