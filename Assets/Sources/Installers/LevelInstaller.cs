using UnityEngine;

using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject m_Level;

    [SerializeField]
    private CameraTransitions m_CameraTransitions;

    public override void InstallBindings()
    {
        Container.Bind<GameObject>().WithId("LD").FromInstance(m_Level).AsSingle();
        Container.Bind<CameraTransitions>().FromInstance(m_CameraTransitions).AsSingle();
    }
}