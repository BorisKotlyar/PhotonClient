﻿using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IGameState
{
    void OnUpdate();
    void SendOperation(OperationRequest request, bool sendRelieble, byte channelId, bool encrypt);
}

