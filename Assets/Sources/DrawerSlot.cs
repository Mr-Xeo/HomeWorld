﻿using UnityEngine;

using TMPro;

using Zenject;

[RequireComponent(typeof(Collider2D))]
public class DrawerSlot : MonoBehaviour
{
    [SerializeField]
    private Color m_HoverColor;

    [SerializeField]
    private TextMeshProUGUI m_Text;

    [SerializeField]
    private SpriteRenderer m_SpriteRenderer;

    [SerializeField]
    private Transform m_DrawerSlot;

    private CameraTransitions m_CameraTransitions;

    private Color m_OriginalColor;

    private Collider2D m_Collider;

    AudioSource m_AudioSource;

    private void Awake()
    {
        m_OriginalColor = m_SpriteRenderer.color;

        m_Collider = GetComponent<Collider2D>();

        m_AudioSource = GetComponent<AudioSource>();
    }

    [Inject]
    public void Init(CameraTransitions cameraTransitions)
    {
        m_CameraTransitions = cameraTransitions;
    }

    public void SetText(string text)
    {
        m_Text.text = text;
    }

    public void SetDrawing(Drawing drawing)
    {
        //append drawing to drawer slot
        drawing.DisableDrawing(); //disable first to hide unkept (otherwise will teleport and trigger exit physics)
        drawing.transform.SetParent(m_DrawerSlot, false);

        //disable previous sprite and collider
        m_SpriteRenderer.enabled    = false;
        m_Collider.enabled          = false;
    }

    private void OnMouseEnter()
    {
        m_SpriteRenderer.color = m_HoverColor;
    }

    private void OnMouseExit()
    {
        m_SpriteRenderer.color = m_OriginalColor;
    }

    private void OnMouseUpAsButton()
    {
        m_SpriteRenderer.color = m_HoverColor;

        m_CameraTransitions.InitDrawerSlot(this);

        m_AudioSource.Play();
    }
}
