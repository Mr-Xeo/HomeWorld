using UnityEngine;

using Zenject;

[CreateAssetMenu(fileName = "GameInstaller", menuName = "Installers/GameInstaller")]
public class GameInstaller : ScriptableObjectInstaller<GameInstaller>
{
    [System.Serializable]
    public class MouseInput
    {
        public string MouseClick;
        public string MouseRightClick;
        public string ScaleButton;
    }

    [System.Serializable]
    public class MouseCursor
    {
        public Texture2D RotateCursor; 
        public Texture2D ScaleCursor;
    }

    [System.Serializable]
    public class GameData
    {
        public float ScrollSpeed;

        public Vector2 SpawnDuration;

        public string[]         StartDrawings;
        public CitizenType[]    StartCitizen;

        public Unlock[] Unlocks;

        public LDUnlock[] LDUnlocks;

        public CitizenUnlock[] CitizenUnlocks;
    }

    [System.Serializable]
    public class Unlock
    {
        public int     DrawingCount;
        public string  DrawingName;
    }

    [System.Serializable]
    public class LDUnlock
    {
        public int          DrawingCount;
        public LevelSlot    LDSlot;
    }

    [System.Serializable]
    public class CitizenUnlock
    {
        public int          DrawingCount;
        public CitizenType  Citizen;
    }

    [SerializeField]
    private MouseInput m_MouseInput;

    [SerializeField]
    private MouseCursor m_MouseCursor;

    [SerializeField]
    private GameData m_GameData;

    public override void InstallBindings()
    {
        Container.BindInstance(m_MouseInput);
        Container.BindInstance(m_MouseCursor);
        Container.BindInstance(m_GameData);
    }
}