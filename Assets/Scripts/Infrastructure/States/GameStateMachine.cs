using System;
using System.Collections.Generic;
using Infrastructure.AssetManagement;
using Infrastructure.Services;
using Infrastructure.Services.StaticData;
using UI.Services;
using UI.Services.Windows;

namespace Infrastructure.States
{
    public class GameStateMachine
    {
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(AllServices services)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, services),
                [typeof(ChooseBoardState)] = new ChooseBoardState(this, services.Single<IWindowService>(), services.Single<IStaticDataService>()),
                [typeof(ChoosePlayModeState)] = new ChoosePlayModeState(this, services.Single<IWindowService>()),
                [typeof(GameLoopState)] = new GameLoopState(this, services.Single<IGameFactory>())
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayLoad>(TPayLoad payload) where TState : class, IPayloadedState<TPayLoad>
        {
            TState state = ChangeState<TState>();            
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            
            TState state = GetState<TState>();
            _activeState = state;
            
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}