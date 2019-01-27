using UnityEngine;

using System.Collections;
using System.Collections.Generic;

using Zenject;

using GameData = GameInstaller.GameData;

public class CitizenHandler
{
    public class PoolData
    {
        public GameObject Prefab;
        public SimplePooler<ICitizen> Pooler;
    }

    private Dictionary<CitizenType, PoolData> m_Poolers = new Dictionary<CitizenType, PoolData>();

    private AsyncProcessor m_AsyncProcessor;

    private LevelHandler m_LevelHandler;

    private GameObject m_PoolerPrefab;

    private Transform m_CitizenParent;

    private List<CitizenType> m_UnlockedCitizen = new List<CitizenType>();

    private GameData m_GameData;

    private CitizenHandler(AsyncProcessor asyncProcessor, [Inject(Id = "CitizenParent")] Transform citizenParent, GameData gameData)
    {
        m_AsyncProcessor = asyncProcessor;

        m_CitizenParent = citizenParent;

        m_GameData = gameData;

        GameObject[] citizen = Resources.LoadAll<GameObject>("Citizens");
        for (int i = 0; i < citizen.Length; i++)
        {
            GameObject currentCitizen = citizen[i];
          
            PoolData poolData = new PoolData() { Prefab = currentCitizen };
            m_PoolerPrefab = poolData.Prefab;

            poolData.Pooler = new SimplePooler<ICitizen>(10, CreateCitizen, null, true);

            ICitizen iCitizen = currentCitizen.GetComponent<ICitizen>();
            m_Poolers.Add(iCitizen.Type, poolData);
        }

        m_AsyncProcessor.StartCoroutine(SpawnCitizen());
    }

    public void Init(LevelHandler levelHandler)
    {
        m_LevelHandler = levelHandler;
    }

    private IEnumerator SpawnCitizen()
    {
        yield return new WaitForSeconds(5f);

        while (true)
        {
        
            //spawn at some random level slot
            if (m_UnlockedCitizen.Count > 0)
            {
                CitizenType citizen = m_UnlockedCitizen[Random.Range(0, m_UnlockedCitizen.Count)];
                SpawnCitizen(citizen);
            }

            int spawnDuration = Random.Range((int) m_GameData.SpawnDuration.x, (int) m_GameData.SpawnDuration.y);
            yield return new WaitForSeconds(spawnDuration);
        }
    }

    public void UnlockCitizen(CitizenType type)
    {
        m_UnlockedCitizen.Add(type);
    }

    private void SpawnCitizen(CitizenType type)
    {
        PoolData poolData = m_Poolers[type];

        m_PoolerPrefab  = poolData.Prefab;
        ICitizen citizen = poolData.Pooler.BorrowOrCreateObject();

        LevelSlot levelSlot     = GetLevelSlot();
        Transform spawnPoint    = levelSlot.GetRandomSpawnPoint();

        citizen.Transform.SetParent(spawnPoint, false);

        m_AsyncProcessor.StartCoroutine(citizen.DoThings());
    }

    private LevelSlot GetLevelSlot()
    {
        return m_LevelHandler.LevelSlots[Random.Range(0, m_LevelHandler.LevelSlots.Count)];
    }

    private ICitizen CreateCitizen()
    {
        GameObject drawingGameObject = Object.Instantiate(m_PoolerPrefab, m_CitizenParent);

        return drawingGameObject.GetComponent<ICitizen>();
    }
}
