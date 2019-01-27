using UnityEngine;

using System.Collections.Generic;

using Zenject;

public class LevelHandler
{
    private List<LevelSlot> m_LevelSlots = new List<LevelSlot>();
    public  List<LevelSlot> LevelSlots { get { return m_LevelSlots; } }

    private int m_VisibleSlotIndex;

    private Transform m_Level;

    private CameraTransitions m_CameraTransitions;

    private LevelHandler([Inject(Id = "LD")] GameObject level, CameraTransitions cameraTransitions)
    {
        LevelSlot[] levelSlots = level.GetComponentsInChildren<LevelSlot>();
        m_LevelSlots.AddRange(levelSlots);

        m_Level             = level.transform;
        m_CameraTransitions = cameraTransitions;
    }

    public void AddNewLD(LevelSlot levelSlotPrefab)
    {
        GameObject levelSlotObject = Object.Instantiate(levelSlotPrefab.gameObject, m_Level);

        LevelSlot levelSlot = levelSlotObject.GetComponent<LevelSlot>();
        levelSlot.Init(this);

        DrawerSlot[] drawerSlots = levelSlot.GetComponentsInChildren<DrawerSlot>();
        for (int i = 0; i < drawerSlots.Length; i++)
        {
            drawerSlots[i].Init(m_CameraTransitions);
        }

        levelSlot.SnapToRightTarget(m_LevelSlots[m_LevelSlots.Count - 1].RightAnchor);
        m_LevelSlots.Add(levelSlot);
    }

    public void OnLevelSlotBecameVisible(LevelSlot levelSlot)
    {
        //update visible level slot, if first or last cycle
        int levelSlotIndex = m_LevelSlots.IndexOf(levelSlot);

        bool isFirst = levelSlotIndex == 0;
        if (!isFirst && levelSlotIndex != m_LevelSlots.Count - 1)
        {
            return;
        }

        int removeIndex = 0, step = 0;
        if (levelSlotIndex == 0)
        {
            step            = 0;
            removeIndex     = m_LevelSlots.Count - 1;

            m_LevelSlots[m_LevelSlots.Count - 1].SnapToLeftTarget(levelSlot.LeftAnchor);
        }
        else
        {
            m_LevelSlots[0].SnapToRightTarget(levelSlot.RightAnchor);
        }

        //remove and add to end/first
        LevelSlot slotToShift = m_LevelSlots[removeIndex];

        m_LevelSlots.RemoveAt(removeIndex);
        levelSlotIndex += step;

        m_LevelSlots.Insert(levelSlotIndex, slotToShift);
    }
}
