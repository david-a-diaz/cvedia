using UnityEngine;
using System.Collections.Generic;

public class MaterialsRegistry
{
    private Dictionary<int, Material[]> m_materialsCache = new Dictionary<int, Material[]>();

    public Material[] GetMaterial(Material _materialPrototype, Renderer _renderer)
    {
        if (!m_materialsCache.ContainsKey(_renderer.GetInstanceID()))
        {
            Material clone = GameObject.Instantiate(_materialPrototype) as Material;

            clone.SetColor("_Color", _renderer.material.GetColor("_Color"));
            clone.SetTexture("_MainTex", _renderer.material.GetTexture("_MainTex"));
            clone.SetFloat("_Glossiness", _renderer.material.GetFloat("_Glossiness"));
            clone.SetFloat("_Metallic", _renderer.material.GetFloat("_Metallic"));

            Material[] clonedMaterials = new Material[_renderer.materials.Length];
            for (int i = 0; i < _renderer.materials.Length; i++)
            {
                clonedMaterials[i] = clone;
            }
            m_materialsCache.Add(_renderer.GetInstanceID(), clonedMaterials);
        }
        return m_materialsCache[_renderer.GetInstanceID()];
    }
}

