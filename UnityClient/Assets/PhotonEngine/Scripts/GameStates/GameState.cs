﻿using ExitGames.Client.Photon;


public class GameState : IGameState
{
    protected PhotonEngine _engine;

    protected GameState(PhotonEngine engine)
    {
        _engine = engine;
    }

    // do nothing
    public virtual void OnUpdate()
    {
    }

    // do nothing
    public virtual void SendOperation(OperationRequest request, bool sendRelieble, byte channelId, bool encrypt)
    {
    }
}

