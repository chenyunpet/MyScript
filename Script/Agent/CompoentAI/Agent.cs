﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Agent:MonoBehaviour
{

    public bool debugGOAP = true;
    public bool IsPalyer = true;
    public Transform Transform;
    public GameObject GameObject;
    public CharacterController CharacterController;
    private Vector3 CollisionCenter;
    public AnimSet AnimSet;
    public BlackBoard BlackBoard = new BlackBoard();
    public Transform t;
    public WorldState WorldState;
    GOAPManager GOAPManager;
    private Hashtable m_Actions = new Hashtable();
    public GOAPAction GetAction(E_GOAPAction type) { return(GOAPAction)m_Actions[type]; }
    public GOAPGoal CurrentGOAPGoal { get { return GOAPManager.CurrentGoal; } }


    public int GetNumberOfActions()
    {
        return m_Actions.Count;
    }
  
    public GOAPGoal AddGoal(E_GOAPGoals newGoal)
    {
        GOAPManager.AddGoal(newGoal);
        return null;
    }
    public void InitializeGoap()
    {
        GOAPManager.Initialize();
    }
    public void AddAction(E_GOAPAction action)
    {
        m_Actions.Add(action, GOAPActionFactory.Create(action, this));
    }
    void Awake()
    {
        Transform = transform;
        GameObject = gameObject;
        AnimSet = GetComponent<AnimSet>();
        WorldState = new WorldState();
        GOAPManager = new GOAPManager(this);

        CharacterController = transform.GetComponent<CharacterController>();
        CollisionCenter = CharacterController.center;
        BlackBoard.Owner = this;
        BlackBoard.myGameObject = GameObject;
        AnimSet = GetComponent<AnimSet>();
        t = GameObject.Find("GameObject").transform;


    }
    public Vector3 Position { get { return Transform.position; } }
    void Start()
    {
        CharacterController.detectCollisions = true;
        CharacterController.center = CollisionCenter;
        RaycastHit hit;
        //if(Physics.Raycast(t.position+Vector3.up,-Vector3.up,out hit,5,1<<10)==false)
        //{
        //    Transform.position = t.position;
        //}
        //else
        //{
        //    Transform.position = hit.point;
        //}
        //Transform.rotation = t.rotation;
    }
    void LateUpdate()
    {
        if (IsPalyer)
        {
            UpdateAgent();
        }else
        {
            GOAPManager.FindCriticalGoal();
        }
    }
    void FixedUpdate()
    {
        if(IsPalyer)
        {
            return;
        }
        UpdateAgent();
        WorldState.SetWSProperty(E_PropKey.E_IDLING,GOAPManager.CurrentGoal == null);
    }
    void UpdateAgent()
    {
        //if (BlackBoard.DontUpdate==false)
        //    return;
        BlackBoard.Update();
        GOAPManager.UpdateCurrentGoal();
        GOAPManager.ManageGoals();

    }
    void ResetAgent()
    {
        WorldState.Reset();
        GOAPManager.Reset();
    }
}

