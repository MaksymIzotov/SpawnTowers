using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileColorChanger : MonoBehaviour
{
    private Renderer materialRenderer;
    private bool isActive;

    [SerializeField] private Color active;
    private Color mainColor;

    private void Start()
    {
        materialRenderer = GetComponent<Renderer>();
        mainColor = materialRenderer.material.color;
    }

    private void Update()
    {
        SetColor();

        isActive = false;
    }

    private void SetColor()
    {
        if (isActive)
            materialRenderer.material.color = active;
        else
            materialRenderer.material.color = mainColor;
    }

    public void SetActive()
    {
        isActive = true;
    }
}
