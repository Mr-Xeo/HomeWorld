using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameInstaller", menuName = "Installers/GameInstaller")]
public class GameInstaller : ScriptableObjectInstaller<GameInstaller>
{
    [System.Serializable]
    public class MouseInput
    {
        public string MouseClick;
    }

    [SerializeField]
    private MouseInput m_MouseInput;

    public override void InstallBindings()
    {
        Container.BindInstance(m_MouseInput);
    }
}