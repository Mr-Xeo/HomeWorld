using UnityEngine;

using Zenject;

using GameData = GameInstaller.GameData;

public class LevelScroll : MonoBehaviour
{
    public enum Direction
    {
        Left,
        Right
    }

    [SerializeField]
    private Direction m_Direction;

    private GameObject m_Level;

    private GameData m_GameData;

    private Vector3[] m_Directions = new Vector3[] { Vector3.right, Vector3.left };

    [Inject]
    private void Init([Inject(Id = "LD")] GameObject level, GameData gameData)
    {
        m_Level     = level;
        m_GameData  = gameData;
    }

    public void Update()
    {
        Vector3 direction = m_Directions[(int) m_Direction];
        m_Level.transform.Translate(direction * m_GameData.ScrollSpeed * Time.deltaTime, Space.World);
    }

    public void SetEnabled(bool isEnabled)
    {
        enabled = isEnabled;
    }
}
