using Infrastructure.AssetManagement;
using Infrastructure.Services;
using Infrastructure.Services.StaticData;
using UI.Services;
using UI.Services.Factory;
using UI.Services.Windows;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, AllServices services)
        {
            _stateMachine = stateMachine;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {
            _services.Single<IStaticDataService>().LoadStaticData();
            _stateMachine.Enter<ChooseBoardState>();
        }

        public void Exit()
        {
            
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IAssets>(new AssetProvider());
            _services.RegisterSingle<IStaticDataService>(new StaticDataService());
            _services.RegisterSingle<IUIFactory>(new UIFactory(
                _services.Single<IAssets>(), 
                _services.Single<IStaticDataService>()));
            _services.RegisterSingle<IGameFactory>(new GameFactory(
                _services.Single<IAssets>(),
                _services.Single<IStaticDataService>()));
            _services.RegisterSingle<IWindowService>(new WindowService(_services.Single<IUIFactory>()));
        }
    }
}