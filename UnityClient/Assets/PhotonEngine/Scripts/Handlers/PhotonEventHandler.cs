
using ExitGames.Client.Photon;

public abstract class PhotonEventHandler : IPhotonEventHandler
{
    protected readonly ViewController _controller;
    public abstract byte Code { get; }

    protected PhotonEventHandler(ViewController controller)
    {
        _controller = controller;
    }

    public delegate void BefpreEventRecieved();
    public BefpreEventRecieved beforeEventRecieved;

    public delegate void AfterEventRecieved();
    public AfterEventRecieved afterEventRecieved;

    public void HandleEvent(EventData eventData)
    {
        if (beforeEventRecieved != null)
        {
            beforeEventRecieved();
        }

        OnHandleEvent(eventData);

        if (afterEventRecieved != null)
        {
            afterEventRecieved();
        }
    }

    public abstract void OnHandleEvent(EventData eventData);
}