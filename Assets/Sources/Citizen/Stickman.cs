using UnityEngine;

using System.Collections;

public class Stickman : MonoBehaviour, ICitizen
{
    [SerializeField]
    private float m_Speed;

    public CitizenType Type { get { return CitizenType.STICKMAN; } }

    public CitizenState State { get; private set; }

    public Transform Transform { get { return transform; } }

    private Animator m_Animator;

    Vector3[] m_Directions = new Vector3[] { Vector3.left, Vector3.right };

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    public IEnumerator DoThings()
    {
        while (true)
        {

            switch(State)
            {
                case CitizenState.IDLE:
                    yield return DoIdle();
                    break;

                case CitizenState.WALKING:
                    yield return DoWalking();
                    break;
            }

            yield return null;
        }
    }

    private IEnumerator DoIdle()
    {
        m_Animator.SetBool("IsWalking", false);

        int waitDuration = Random.Range(0, 11);
        yield return new WaitForSeconds(waitDuration);

        State = CitizenState.WALKING;
    }

    private IEnumerator DoWalking()
    {
        m_Animator.SetBool("IsWalking", true);

        //move to random direction for 2 seconds
        Vector3 direction = m_Directions[Random.Range(0, m_Directions.Length)];

        float walkStart = Time.time;
        while (Time.time - walkStart < 2)
        {
            walkStart = Time.time;

            transform.Translate(direction * Time.deltaTime * m_Speed, Space.World);

            yield return new WaitForEndOfFrame();
        }

        State = CitizenState.IDLE;
    }

}
