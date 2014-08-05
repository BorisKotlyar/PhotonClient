using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using UnityEngine;


public class ViewController : IViewController
{

    private readonly View _controlledView;
    private readonly byte _subOperationCode;
    public View ControllerdView { get { return _controlledView; } }

    private readonly Dictionary<byte, IPhotonOperationHandler> _operationHandlers = new Dictionary<byte, IPhotonOperationHandler>();
    private readonly Dictionary<byte, IPhotonEventHandler> _eventHandlers = new Dictionary<byte, IPhotonEventHandler>();

    public ViewController(View controlledView, byte subOperationCode = 0)
    {
        _controlledView = controlledView;
        _subOperationCode = subOperationCode;
        if (PhotonEngine.Instance == null)
        {
            Application.LoadLevel(0);
        }
        else
        {
            PhotonEngine.Instance.Controller = this;
        }
    }

    public Dictionary<byte, IPhotonOperationHandler> OperationHandlers
    {
        get { return _operationHandlers; }
    }

    public Dictionary<byte, IPhotonEventHandler> EventHandlers
    {
        get { return _eventHandlers; }
    }


    #region Implementation of IViewController

    public bool isConnected { get { return PhotonEngine.Instance.State is Connected; } }

    public void ApplicationQuit()
    {
        PhotonEngine.Instance.Disconnect();
    }

    public void Connect()
    {
        if (!isConnected)
        {
            PhotonEngine.Instance.Initialize();
        }
    }

    public void SendOperation(OperationRequest request, bool sendRelieble, byte channelId, bool encrypt)
    {
        PhotonEngine.Instance.SendOp(request, sendRelieble, channelId, encrypt);
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        _controlledView.LogDebug(string.Format("{0} - {1}", level, message));
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
        IPhotonOperationHandler handler;
        if (operationResponse.Parameters.ContainsKey(_subOperationCode) &&
            OperationHandlers.TryGetValue(
                Convert.ToByte(operationResponse.Parameters[_subOperationCode]),
                out handler))
        {
            handler.HandleResponse(operationResponse);
        }
        else
        {
            OnUnexpectedOperationResponse(operationResponse);
        }

    }

    public void OnEvent(EventData eventData)
    {
        IPhotonEventHandler handler;
        if (EventHandlers.TryGetValue(eventData.Code, out handler))
        {
            handler.HandleEvent(eventData);
        }
        else
        {
            OnUnexpectionEvent(eventData);
        }
    }

    public void OnUnexpectionEvent(EventData eventData)
    {
        _controlledView.LogError(string.Format("Unexpected Event {0}", eventData.Code));
    }

    public void OnUnexpectedOperationResponse(OperationResponse operationResponse)
    {
        _controlledView.LogError(string.Format("Unexpected Operation Error {0} from operation {1}", operationResponse.ReturnCode, operationResponse.OperationCode));
    }

    public void OnUnexpectedStatusCode(StatusCode statusCode)
    {
        _controlledView.LogError(string.Format("Unexpected Status {0}", statusCode));
    }

    public void OnDisconnected(string message)
    {
        _controlledView.Disconnected(message);
    }

    #endregion
}
