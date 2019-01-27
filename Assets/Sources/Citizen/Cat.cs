using UnityEngine;

using System.Collections;

public class Cat : MonoBehaviour, ICitizen
{
    [SerializeField]
    private CitizenType m_Type;
    public CitizenType Type { get { return m_Type; } }

    public CitizenState State { get; private set; }

    public Transform Transform { get { return transform; } }

    public IEnumerator DoThings()
    {
        yield return null;
    }
}
