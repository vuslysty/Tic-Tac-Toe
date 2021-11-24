﻿namespace Infrastructure.States
{
    public interface IPayloadedState<TPayLoad> : IExitableState
    {
        void Enter(TPayLoad payLoad);
    }
}