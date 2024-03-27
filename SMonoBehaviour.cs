using System;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class SMonoBehaviour : MonoBehaviour
{
    private const string ROOT_TAG = "Root";
    private static Dictionary<Type, Dictionary<int, List<SMonoBehaviour>>> shared = new();

    private Type _type = null;
    private GameObject _rootGameObject = null;
    private int _instanceID = -1;

    public static bool TryGetSharedComponent<T>(int instanceID, out T component) where T: SMonoBehaviour
    {
        component = GetSharedComponent<T>(instanceID);
        
        if(component == null) return false;
        else return true;
    } 
    public static T GetSharedComponent<T>(int instanceID) where T: SMonoBehaviour
    {
        try
        {
            return shared[typeof(T)][instanceID][0] as T;
        }
        catch (Exception)
        {
            return null;
        }
    } 
    public static List<T> GetSharedComponents<T>(int instanceID) where T: SMonoBehaviour 
    {
        try
        {
            return shared[typeof(T)][instanceID] as List<T>;
        }
        catch (Exception)
        {
            return null;   
        }
    } 
    private void InitRootGameObject()
    {
        GameObject currentObject = gameObject;
        while (currentObject != null)
        {
            if(currentObject.tag == ROOT_TAG)
            {
                _rootGameObject = currentObject;
                _instanceID = _rootGameObject.GetInstanceID();
                return;
            } else
            {
                currentObject = currentObject.transform.parent?.gameObject;
            }
        }

        Debug.LogError($"Root GameObject not found for [{this}]");
    }
    protected virtual void Awake() 
    {
        InitRootGameObject();
        AddComponent();
    }
    protected virtual void OnDestroy() 
    {
        RemoveComponent();
    }
    private void AddComponent()
    {
        _type = this.GetType();
        if(!shared.ContainsKey(_type))
        {
            shared.Add(_type, new Dictionary<int, List<SMonoBehaviour>>());
        }

        int id = _rootGameObject.GetInstanceID();

        if(!shared[_type].ContainsKey(id))
        {
            shared[_type].Add(id, new List<SMonoBehaviour>());
        }

        shared[_type][id].Add(this);
    }
    private void RemoveComponent()
    {
        shared[_type].Remove(_instanceID);
    }
}
