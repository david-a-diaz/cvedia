using UnityEngine;

public class MaterialSwap : MonoBehaviour
{
    private Material[] m_originalMaterials;

    void Awake()
    {
        m_originalMaterials = GetComponent<Renderer>().materials;
    }

    public void RestoreMaterial()
    {
        GetComponent<Renderer>().materials = m_originalMaterials;
    }
}

