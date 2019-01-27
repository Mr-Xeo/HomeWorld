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
        while (true)
        {
            switch(State)
            {
                case CitizenState.IDLE:
                    yield return DoIdle();
                    break;
            }

            yield return null;
        }
    }

    private IEnumerator DoIdle()
    {
        int waitDuration = Random.Range(2, 5);
        yield return new WaitForSeconds(waitDuration);
    }
}
