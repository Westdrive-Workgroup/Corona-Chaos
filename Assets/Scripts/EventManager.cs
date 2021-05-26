using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    // Start is called before the first frame update

    public delegate int delegateName(int a);

    public delegate void delegateB(string text);
    public event delegateName OnIntUpdated;

    public UnityEvent OnSpacePressed;
    public UnityAction<int> myAction;
    //delegateName myDelegate;
    void Start()
    {
        OnIntUpdated += OnSomeEvent;
        OnIntUpdated += OnSomeEventB;
        OnIntUpdated += OnSomeEventA;
        myAction = OnSomeEventVoid;
        myAction?.Invoke(1000);
        Foo(LogToFile);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //delegateName myDelegate
            if (OnIntUpdated != null)
            {
                Debug.Log(OnIntUpdated.Invoke(100));
            }
            OnSpacePressed?.Invoke();
        }
    }

    void OnSomeEventVoid(int a)
    {
        Debug.Log("Event is called with an integer: " + (a +5));
        
    }
    int OnSomeEvent(int a)
    {
        Debug.Log("Event is called with an integer: " + (a +5));
        return a + 5;
    }
    int OnSomeEventA(int a)
    {

        Debug.Log("Event is called with an integer: " + (a - 5));
        return a - 5;
    }
    public int OnSomeEventB(int a)
    {
        Debug.Log("Event is called with an integer: " + (a +500));
        return a + 500;
    }

    void Log(string text)
    {
        Debug.Log(text);
    }

    void LogToFile(string text)
    {
        //writes to file
    }

    void Foo(delegateB logger)
    {
        //does something
        logger("I am done!");
    }
}
