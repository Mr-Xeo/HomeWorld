using System;
using System.Collections.Generic;

public class SimplePooler<T>
{
    private int m_MaxPooledObjects;
    private List<T> m_Pool;
    private Action<T> m_DeInit;
    private Func<T> m_Ctr;

    private bool m_CreateUpFront;

    public SimplePooler(int maxPooledObjects, Func<T> Ctr, Action<T> DeInit, bool createUpFront = true)
    {
        m_CreateUpFront = createUpFront;
        m_MaxPooledObjects = maxPooledObjects;
        if (createUpFront && maxPooledObjects > 0)
            m_Pool = new List<T>(maxPooledObjects);
        else
            m_Pool = new List<T>();

        m_DeInit = DeInit;
        m_Ctr = Ctr;

        //Fill pool
        if (createUpFront)
        {
            for (int i = 0; i < maxPooledObjects; ++i)
            {
                m_Pool.Add(Ctr());
            }
        }
    }

    public T BorrowOrCreateObject()
    {
        if (m_Pool.Count == 0)
        {
            if (m_CreateUpFront)
                UnityEngine.Debug.LogWarning("SimplePooler, Pool empty, creating new object. Consider using larger pool than: " + m_MaxPooledObjects);

            //raise max needed pool
            m_MaxPooledObjects++;

            return m_Ctr();
        }

        T res = m_Pool[m_Pool.Count - 1];
        m_Pool.RemoveAt(m_Pool.Count - 1);
        return res;
    }

    public void ReturnPooledObject(T obj)
    {
        if (obj == null)
        {
            UnityEngine.Debug.LogError("SimplePooler, Returned object was null. Ignoring");
            return;
        }
        if (m_MaxPooledObjects > 0 && m_Pool.Count >= m_MaxPooledObjects)
        {
            UnityEngine.Debug.LogWarning("SimplePooler, Reached max PoolSize. Dropping object.");
            return;
        }

        if(m_DeInit != null)
            m_DeInit(obj);

        m_Pool.Add(obj);
    }
}