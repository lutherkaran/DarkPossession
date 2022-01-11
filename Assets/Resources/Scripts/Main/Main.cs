using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : PossessionController
{
    public void Awake()
    {
        Gameflow.instance.Initialize();
    }

    public void Start()
    {
        Gameflow.instance.PostInitialize();

    }
    public void Update()
    {
        Gameflow.instance.Refresh(Time.deltaTime);

    }
    public void FixedUpdate()
    {
        Gameflow.instance.PhysicsRefresh(Time.fixedDeltaTime);
    }

}