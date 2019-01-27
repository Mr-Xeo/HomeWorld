using UnityEngine;

public class EnableObject : MonoBehaviour
{
    [SerializeField]
    private GameObject m_ObjectToToggle;

    public void Toggle()
    {
        m_ObjectToToggle.SetActive(!m_ObjectToToggle.activeSelf);
    }
}
