using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : IUpdate
{
    private static PlayerManager Instance;
    private PlayerManager() { }
    public static PlayerManager instance { get { return Instance ?? (Instance = new PlayerManager()); } }

    public Player gPlayer;
    Vector2 vStartPos;


    public void Initialize()
    {
        Player();
    }

    public void PhysicsRefresh(float _fixedDeltaTime)
    {
        gPlayer.PhysicsRefresh(_fixedDeltaTime);
    }

    public void PostInitialize()
    {
        gPlayer.PostInitialize();
    }

    public void Refresh(float _deltaTime)
    {
        gPlayer.Refresh(_deltaTime);

    }

    private void Player()
    {
        vStartPos = new Vector2(5, 1);
        gPlayer = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Player"), vStartPos, Quaternion.identity).AddComponent<Player>();
        if (gPlayer) { gPlayer.bPlayerIsAlive = true; }
        else { gPlayer.bPlayerIsAlive = false; Debug.Log("P L A Y E R   D I E D"); }
    }
}
