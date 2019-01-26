using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;

using PoolData = DrawerHandler.PoolData;

public class DrawingSelectionPanel : MonoBehaviour
{
    private struct DrawingSelection
    {
        public string Name;
        public Sprite Sprite;
    }

    [SerializeField]
    private Image m_DrawingImage;

    public string SelectedDrawing { get { return m_Drawings[m_SelectionIndex].Name; } }

    private int m_SelectionIndex;

    private List<DrawingSelection> m_Drawings = new List<DrawingSelection>();

    public void InitDrawings(List<string> drawings, Dictionary<string, PoolData> poolers)
    {
        m_Drawings.Clear();

        for (int i = 0; i < drawings.Count; i++)
        {
            GameObject drawingPrefab = poolers[drawings[i]].Prefab;

            Drawing drawing = poolers[drawings[i]].Prefab.GetComponent<Drawing>();

            DrawingSelection selection = new DrawingSelection(){ Name = drawingPrefab.name, Sprite = drawing.Sprite};
            m_Drawings.Add(selection);
        }

        m_SelectionIndex = 0;
        SetDrawingImage(m_SelectionIndex);
    }

    private void SetDrawingImage(int index)
    {
        m_DrawingImage.sprite = m_Drawings[index].Sprite;
    }

    public void OnNextImage()
    {
        m_SelectionIndex = (m_SelectionIndex + 1) % m_Drawings.Count;
        SetDrawingImage(m_SelectionIndex);
    }

    public void OnPrevImage()
    {
        m_SelectionIndex = m_SelectionIndex - 1;
        if (m_SelectionIndex < 0)
        {
            m_SelectionIndex = m_Drawings.Count - 1;
        }

        SetDrawingImage(m_SelectionIndex);
    }

    public void OnPanelClosed()
    {
        gameObject.SetActive(false);
    }
}
