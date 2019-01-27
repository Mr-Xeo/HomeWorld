using UnityEngine;

using System.Collections.Generic;

using Unlock    = GameInstaller.Unlock;
using GameData  = GameInstaller.GameData;
using LDUnlock  = GameInstaller.LDUnlock;

public class ProgressionHandler
{
    private GameData m_GameData;

    private LevelHandler m_LevelHandler;

    public readonly List<string> UnlockedDrawings = new List<string>();

    private int m_DrawCount;
    private int m_UnlockIndex;
    private int m_LDUnlockIndex;

    private ProgressionHandler(GameData gameData, LevelHandler levelHandler)
    {
        m_GameData = gameData;

        m_LevelHandler = levelHandler;

        for (int i = 0; i < m_GameData.StartDrawings.Length; i++)
        {
            UnlockedDrawings.Add(m_GameData.StartDrawings[i]);
        }
    }

    public void OnDrawingEnded()
    {
        m_DrawCount++;

        //unlock new draw
        Unlock[] unlocks = m_GameData.Unlocks;
        for (int i = m_UnlockIndex; i < unlocks.Length; i++)
        {
            Unlock currentUnlock = unlocks[i];
            if (currentUnlock.DrawingCount <= m_DrawCount)
            {
                m_UnlockIndex = i;
                UnlockedDrawings.Add(currentUnlock.DrawingName);
            }
        }

        //unlock new LD Draw
        LDUnlock[] LDUnlocks = m_GameData.LDUnlocks;
        for (int i = m_LDUnlockIndex; i < LDUnlocks.Length; i++)
        {
            LDUnlock currentUnlock = LDUnlocks[i];
            if (currentUnlock.DrawingCount <= m_DrawCount)
            {
                m_LDUnlockIndex = i;
                m_LevelHandler.AddNewLD(currentUnlock.LDSlot);
            }
        }
    }
}
