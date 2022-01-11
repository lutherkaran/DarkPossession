using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpdate
{
    void Initialize();
    void PostInitialize();
    void Refresh(float _deltaTime);
    void PhysicsRefresh(float _fixedDeltaTime);
}
