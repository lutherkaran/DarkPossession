using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFeatures
{
    void Move(float _deltaTime);
    void Jump(float _fixedDeltaTime);
    void Attack();
}
