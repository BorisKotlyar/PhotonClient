using System;
using UnityEngine;
using System.Collections;

public class Login : View {

    public string ServerAddress;
    public string ApplicationName;

    public bool loggingIn = false;

    public override void Awake()
    {
        Controller = new LoginController(this);
        PhotonEngine.UseExistingOrCreateNewPhotonEngine(ServerAddress, ApplicationName);
    }

	// Use this for initialization
	void Start () {
        string[] argList = new string[0];
        if (Application.srcValue.Split(new[] {"?"}, StringSplitOptions.RemoveEmptyEntries).Length > 1)
        {
            argList = Application.srcValue.Split(new[] { "?" }, StringSplitOptions.RemoveEmptyEntries)[1].Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        }

        if (argList.Length == 2)
        {
            _controller.SendLogin(argList[0], argList[1]);
            loggingIn = true;
        }

        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private LoginController _controller;
    public override IViewController Controller { get { return (IViewController)_controller; } protected set { _controller = value as LoginController; } }
}
