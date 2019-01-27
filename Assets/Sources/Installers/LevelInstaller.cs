using UnityEngine;

using Zenject;

public class AsyncProcessor : MonoBehaviour {}

public class LevelInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject m_Level;

    [SerializeField]
    private CameraTransitions m_CameraTransitions;

    [SerializeField]
    private Transform m_CitizenParent;

    public override void InstallBindings()
    {
        Container.Bind<GameObject>().WithId("LD").FromInstance(m_Level).AsSingle();
        Container.Bind<Transform>().WithId("CitizenParent").FromInstance(m_CitizenParent).AsSingle();

        Container.Bind<CameraTransitions>().FromInstance(m_CameraTransitions).AsSingle();

        Container.Bind<LevelHandler>().AsSingle();
        Container.Bind<CitizenHandler>().AsSingle();
        Container.Bind<ProgressionHandler>().AsSingle();

        Container.Bind<AsyncProcessor>().FromNewComponentOnNewGameObject().UnderTransform(transform).AsSingle();
    }
}