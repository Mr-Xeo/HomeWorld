using UnityEngine;

using System.Collections;

public class Stickman : MonoBehaviour, ICitizen
{
    public CitizenType Type { get { return CitizenType.STICKMAN; } }

    public CitizenState State { get; private set; }

    public Transform Transform { get { return transform; } }

    public IEnumerator DoThings()
    {
        yield return null;
    }
}
