﻿using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;

using Zenject;

public class DrawerHandler : MonoBehaviour
{
    public class PoolData
    {
        public GameObject Prefab;
        public SimplePooler<Drawing> Pooler;
    }

    [SerializeField]
    private Image[] m_ScrollAreas;

    [SerializeField]
    private GameObject m_DrawerPanel;

    [SerializeField]
    private InputField m_NameInputField;

    [SerializeField]
    private Transform m_DrawingParent;

    [SerializeField]
    private Transform m_DrawingsParent;

    [SerializeField]
    private DrawingSelectionPanel m_DrawingSelectionPanel;

    private DrawerSlot m_DrawerSlot;

    private GameObject m_PoolerPrefab;

    private Drawing m_Drawing;

    private Dictionary<string, PoolData> m_Poolers = new Dictionary<string, PoolData>();

    private string m_OriginalInputFieldText;

    private int m_SelectedDrawing;

    private ProgressionHandler m_ProgressionHandler;

    private void Awake()
    {
        m_OriginalInputFieldText = m_NameInputField.text;

        Drawing[] drawings = Resources.LoadAll<Drawing>("Drawings");
        for (int i = 0; i < drawings.Length; i++)
        {
            PoolData poolData = new PoolData() { Prefab = drawings[i].gameObject };
            m_PoolerPrefab = poolData.Prefab;
            
            poolData.Pooler = new SimplePooler<Drawing>(5, CreateDrawing, null, true);

            m_Poolers.Add(m_PoolerPrefab.name, poolData);
        }
    }

    [Inject]
    private void Init(ProgressionHandler progressionHandler)
    {
        m_ProgressionHandler = progressionHandler;
    }

    public void ToggleDrawer()
    {
        for (int i = 0; i < m_ScrollAreas.Length; i++)
        {
            Image currentImage = m_ScrollAreas[i];
            currentImage.enabled = !currentImage.enabled;
        }

        //enable drawer UI
        m_DrawerPanel.SetActive(!m_DrawerPanel.activeSelf);
    }

    public void InitSelectionPanel(DrawerSlot drawerSlot)
    {
        m_DrawerSlot = drawerSlot;

        m_DrawingSelectionPanel.InitDrawings(m_ProgressionHandler.UnlockedDrawings, m_Poolers);

        m_DrawingSelectionPanel.gameObject.SetActive(true);
    }

    private void InitDrawing(string drawingName)
    {
        //recover a drawing from the pooler and add it to the offset
        PoolData poolData = m_Poolers[drawingName];

        m_PoolerPrefab  = poolData.Prefab;
        m_Drawing = poolData.Pooler.BorrowOrCreateObject();

        m_Drawing.transform.SetParent(m_DrawingParent, false);
    }

    private Drawing CreateDrawing()
    {
        GameObject drawingGameObject = Instantiate(m_PoolerPrefab, m_DrawingsParent);

        return drawingGameObject.GetComponent<Drawing>();
    }

    public void OnTextEdit(string text)
    {
        if (!text.Equals(m_OriginalInputFieldText))
        {
            m_DrawerSlot.SetText(text);
        }
    }

    public void OnDrawingEnded()
    {
        m_DrawerSlot.SetDrawing(m_Drawing);

        m_ProgressionHandler.OnDrawingEnded();
    }

    public void OnDrawingSelected()
    {
        m_DrawingSelectionPanel.gameObject.SetActive(false);

        InitDrawing(m_DrawingSelectionPanel.SelectedDrawing);

        m_NameInputField.text = m_OriginalInputFieldText;
    }
}
