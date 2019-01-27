using UnityEngine;

using System.Collections;

public enum CitizenState
{
    IDLE,
    WALKING
}

[System.Serializable]
public enum CitizenType
{
    STICKMAN,
    CAT,
    DOG,
    CAR,
    BIRD
}

public interface ICitizen
{
    Transform Transform { get; }

    CitizenType Type { get; }

    CitizenState State { get; }

    IEnumerator DoThings();
}
