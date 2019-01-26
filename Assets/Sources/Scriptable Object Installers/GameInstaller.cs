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
    }

    [System.Serializable]
    public class MouseCursor
    {
        public Texture2D RotateCursor; 
    }

    [SerializeField]
    private MouseInput m_MouseInput;

    [SerializeField]
    private MouseCursor m_MouseCursor;

    public override void InstallBindings()
    {
        Container.BindInstance(m_MouseInput);
        Container.BindInstance(m_MouseCursor);
    }
}