namespace Infrastructure.States
{
    public class GameLoopState : IPayloadedState<GameMode>
    {
        private readonly GameStateMachine _gameStateMachine;

        public GameLoopState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Exit()
        {
        }
        
        public void Enter(GameMode gameMode)
        {
        }
    }
}