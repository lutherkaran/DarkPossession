using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessionController : MonoBehaviour
{
    public IPossessable controlledObj;

    public void possess(IPossessable possessable)
    {
        if (possessable != null)
        {
            if (controlledObj != null)
            {
                controlledObj.unpossess();
            }
            controlledObj = possessable;
            controlledObj.possess();
        }

    }
    public void Unpossess()
    {
        if (controlledObj != null)
        {
            controlledObj.unpossess();
        }
    }
}
