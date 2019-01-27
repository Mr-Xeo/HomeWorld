using UnityEngine;

using Zenject;

public class LevelSlot : MonoBehaviour
{
    [SerializeField]
    private Transform   m_LeftAnchor;
    public  Vector3     LeftAnchor { get { return m_LeftAnchor.transform.position; } }

    [SerializeField]
    private Transform   m_RightAnchor;
    public  Vector3     RightAnchor { get { return m_RightAnchor.transform.position; } }

    [SerializeField]
    private Transform[] m_CitizenSpawnPoints;

    private LevelHandler m_LevelHandler;

    [Inject]
    public void Init(LevelHandler levelHandler)
    {
        m_LevelHandler = levelHandler;
    }

    private void OnBecameVisible()
    {
        m_LevelHandler.OnLevelSlotBecameVisible(this);
    }

    public void SnapToRightTarget(Vector3 rightAnchor)
    {
        transform.position = rightAnchor - m_LeftAnchor.localPosition;
    }

    public void SnapToLeftTarget(Vector3 leftAnchor)
    {
        transform.position = leftAnchor - m_RightAnchor.localPosition;
    }

    public Transform GetRandomSpawnPoint()
    {
        return m_CitizenSpawnPoints[Random.Range(0, m_CitizenSpawnPoints.Length)];
    }
}
