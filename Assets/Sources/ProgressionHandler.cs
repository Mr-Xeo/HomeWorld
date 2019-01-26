using UnityEngine;

using System.Collections.Generic;

using Unlock    = GameInstaller.Unlock;
using GameData  = GameInstaller.GameData;

public class ProgressionHandler
{
    private GameData m_GameData;

    public readonly List<string> UnlockedDrawings = new List<string>();

    private int m_DrawCount;
    private int m_UnlockIndex;

    private ProgressionHandler(GameData gameData)
    {
        m_GameData = gameData;

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
    }
}
