using ExitGames.Client.Photon;

public abstract class PhotonOperationHandler : IPhotonOperationHandler
{
    protected readonly ViewController _controller;
    public abstract byte Code { get; }

    protected PhotonOperationHandler(ViewController controller)
    {
        _controller = controller;
    }

    public delegate void BefpreResponseRecieved();
    public BefpreResponseRecieved beforeResponseRecieved;

    public delegate void AfterResponseRecieved();
    public AfterResponseRecieved afterResponseRecieved;

    public void HandleResponse(OperationResponse response)
    {
        if (beforeResponseRecieved != null)
        {
            beforeResponseRecieved();
        }

        OnHandleResponse(response);

        if (afterResponseRecieved != null)
        {
            afterResponseRecieved();
        }
    }

    public abstract void OnHandleResponse(OperationResponse response);
}

