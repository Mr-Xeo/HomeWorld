﻿using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider))]
public class Stick : MonoBehaviour
{
    [SerializeField]
    private Color m_HoverColor;

    /*[SerializeField]
    private GameObject m_RotationAnchorPrefab;*/

    private SpriteRenderer m_SpriteRenderer;

    private Color m_OriginalColor;

    private void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();

        m_OriginalColor = m_SpriteRenderer.color;

        //CreateRotationAnchors();
    }

    /*private void CreateRotationAnchors()
    {
        //create anchors relative to the 4 corners
        BoxCollider boxCollider = GetComponent<BoxCollider>();

        Bounds boxBounds = boxCollider.bounds;
        Vector3[] corners = new Vector3[] { new Vector3(boxBounds.min.x, boxBounds.min.y, 0), new Vector3(boxBounds.max.x, boxBounds.min.y, 0),
                                            new Vector3(boxBounds.min.x, boxBounds.max.y, 0), new Vector3(boxBounds.max.x, boxBounds.max.y, 0) };

        Vector3[] dirs = new Vector3[] {    new Vector3(-1, -1, 0).normalized, new Vector3(1, -1, 0).normalized,
                                            new Vector3(-1, 1, 0).normalized, new Vector3(1, 1, 0).normalized };

        float pixelOffset = 0.5f;
        for (int i = 0; i < corners.Length; i++)
        {
            Vector3 anchorCenter = corners[i] + pixelOffset * transform.InverseTransformDirection(dirs[i]);

            GameObject anchorGameObject = Object.Instantiate(m_RotationAnchorPrefab, anchorCenter, Quaternion.identity);
            //anchorGameObject.transform.SetParent(transform);
        }
    }*/

    public void OnRaycasterIn()
    {
        m_SpriteRenderer.color = m_HoverColor;
    }

    public void OnRaycasterOut()
    {
        m_SpriteRenderer.color = m_OriginalColor;
    }

    public void OnRaycasterPress(Vector2 mouseDelta)
    {
        transform.Translate(mouseDelta, Space.World);
    }

    public void OnRotate(Vector2 mouseDelta)
    {
        transform.Rotate(0, 0, -mouseDelta.x, Space.World);
    }

    public void OnRaycasterUp()
    {
        
    }
}
