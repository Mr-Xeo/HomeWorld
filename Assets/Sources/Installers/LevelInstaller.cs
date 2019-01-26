using UnityEngine;

using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject m_Level;

    public override void InstallBindings()
    {
        Container.Bind<GameObject>().WithId("LD").FromInstance(m_Level).AsSingle();
    }
}