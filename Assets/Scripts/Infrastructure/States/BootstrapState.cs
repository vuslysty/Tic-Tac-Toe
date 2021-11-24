using Infrastructure.AssetManagement;
using Infrastructure.Services;
using UI.Services;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private GameStateMachine _stateMachine;
        private AllServices _services;

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
            _services.RegisterSingle<IUIFactory>(new UIFactory(_services.Single<IAssets>(), 
                _services.Single<IStaticDataService>()));
        }
    }
}