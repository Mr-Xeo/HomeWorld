using UnityEngine;

public class Drawing : MonoBehaviour
{
    private Stick[] m_Sticks;

    private void Awake()
    {
        m_Sticks = GetComponentsInChildren<Stick>();
    }

    public void DisableDrawing()
    {
        for (int i = 0; i < m_Sticks.Length; i++)
        {
            m_Sticks[i].DisableStick();
        }
    }
}
