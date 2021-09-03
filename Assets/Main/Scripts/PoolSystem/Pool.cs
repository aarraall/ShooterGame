using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pool
{
    PoolObject _objSample;
    List<PoolObject> poolObjects;
    string _name;
    Transform _parent;
    private int _increment;
    public string Name => _name;

    public Pool(PoolObject objSample, string name, Transform parent, int increment)
    {
        poolObjects = new List<PoolObject>();
        _objSample = objSample;
        _name = name;
        _parent = parent;
        _increment = increment;

        CreateObject();
    }
    private bool CheckAvailability
    {
        get
        {
            if (poolObjects.All(obj => obj.InUse))
            {
                CreateObject();
                return true;
            }
            else
                return true;
        }
    }

    private void CreateObject()
    {
        for (int i = 0; i < _increment; i++)
        {
            PoolObject tempObj = PoolObject.Instantiate(_objSample);
            tempObj.transform.SetParent(_parent);
            tempObj.Dismiss();
            poolObjects.Add(tempObj);
        }
    }

    public PoolObject GetObject(Transform _transform)
    {
        if (CheckAvailability)
        {
            PoolObject obj = poolObjects.First(obj => !obj.InUse);
            obj.transform.position = _transform.position;
            obj.transform.rotation = _transform.rotation;
            obj.Spawn();
            return obj;
        }
        else
            throw new System.Exception("Pool is not available");

    }

    public void Dismiss(PoolObject obj)
    {
        if (obj.InUse)
            obj.Dismiss();
    }

}
