using UnityEngine;
using System.Collections.Generic;

public class MaterialsRegistry
{
    private Dictionary<int, Material> m_materialsCache = new Dictionary<int, Material>();

    public Material GetMaterial(Material _materialPrototype, Renderer _renderer)
    {
        if (!m_materialsCache.ContainsKey(_renderer.material.GetInstanceID()))
        {
            Material clone = GameObject.Instantiate(_materialPrototype) as Material;
            m_materialsCache.Add(_renderer.material.GetInstanceID(), clone);

            clone.SetColor("_Color", _renderer.material.GetColor("_Color"));
            clone.SetTexture("_MainTex", _renderer.material.GetTexture("_MainTex"));
            clone.SetFloat("_Glossiness", _renderer.material.GetFloat("_Glossiness"));
            clone.SetFloat("_Metallic", _renderer.material.GetFloat("_Metallic"));
        }
        return m_materialsCache[_renderer.material.GetInstanceID()];
    }
}

