using ExitGames.Client.Photon;


public interface IViewController
{
    bool isConnected { get; }

    void ApplicationQuit();
    void Connect();

    void SendOperation(OperationRequest request, bool sendRelieble, byte channelId, bool encrypt);


    #region Implementation of IPhotonPeerListener

    void DebugReturn(DebugLevel level, string message);
    void OnOperationResponse(OperationResponse operationResponse);
    void OnEvent(EventData eventData);

    void OnUnexpectionEvent(EventData eventData);
    void OnUnexpectedOperationResponse(OperationResponse operationResponse);
    void OnUnexpectedStatusCode(StatusCode statusCode);

    void OnDisconnected(string message);

    #endregion
}

