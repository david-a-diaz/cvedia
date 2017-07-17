using UnityEngine;

public class MaterialSwap : MonoBehaviour
{
    private Material m_originalMaterial;

    void Awake()
    {
        m_originalMaterial = GetComponent<Renderer>().material;
    }

    public void RestoreMaterial()
    {
        GetComponent<Renderer>().material = m_originalMaterial;
    }
}

