using UnityEngine;

public class LevelSlot : MonoBehaviour
{
    private void OnBecameVisible()
    {
        Debug.Log("Became visible " + gameObject.name);
    }
}
