using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTextureCreator : MonoBehaviour
{
    public Camera minimapCamera; // Die Kamera, die die Minimap rendert
    public int textureWidth = 512; // Die Breite der Render-Textur
    public int textureHeight = 512; // Die Höhe der Render-Textur
    public RenderTextureFormat textureFormat = RenderTextureFormat.ARGB32; // Das Format der Render-Textur
    public FilterMode filterMode = FilterMode.Bilinear; // Der Filtermodus der Render-Textur

    private void Start()
    {
        // Erstelle eine neue Render-Textur
        RenderTexture renderTexture = new RenderTexture(textureWidth, textureHeight, 0, textureFormat);
        renderTexture.filterMode = filterMode;

        // Weise der Kamera die Render-Textur als Ziel zu
        minimapCamera.targetTexture = renderTexture;

        // Erstelle ein UI-Element mit der Render-Textur als Material
        GameObject minimapUI = new GameObject("MinimapUI");
        minimapUI.transform.SetParent(transform);
        minimapUI.transform.localPosition = Vector3.zero;

        MeshRenderer renderer = minimapUI.AddComponent<MeshRenderer>();
        renderer.sharedMaterial = new Material(Shader.Find("Unlit/Texture"));

        // Weise der Materialtextur das Render-Textur als Haupttextur zu
        renderer.sharedMaterial.mainTexture = renderTexture;
    }
}


