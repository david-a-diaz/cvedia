using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTextureSetter : MonoBehaviour 
{
    public Camera TNECamera;
    public RenderTexture TNETexture;

    public Material ThermalMaterial;
    public Material EMMaterial;
    public Material NormalMaterial;

    private MaterialsRegistry m_materialRegistry = new MaterialsRegistry();

    private Dictionary<int,MaterialSwap> m_objectsWithMaterialChange = new Dictionary<int,MaterialSwap>();

	// Use this for initialization
	void Awake () 
    {
        TNETexture.width = Screen.width;
        TNETexture.height = Screen.height;
	}
	
	// Update is called once per frame
	void OnPreRender () 
    {
        SetMaterial("Thermal", ThermalMaterial);
        SetMaterial("EM", EMMaterial);
        SetMaterial("Normal", NormalMaterial);
	}

    private void SetMaterial(string _tag, Material _material)
    {
        GameObject[] _objects = GameObject.FindGameObjectsWithTag(_tag);
        for (int i = 0; i < _objects.Length; i++)
        {
            Renderer renderer = _objects[i].GetComponent<Renderer>();
            if (renderer != null)
            {
                if (_objects[i].GetComponent<MaterialSwap>() == null)
                {
                    _objects[i].AddComponent<MaterialSwap>();
                }

                m_objectsWithMaterialChange[_objects[i].GetInstanceID()] = _objects[i].GetComponent<MaterialSwap>();
                renderer.material = m_materialRegistry.GetMaterial(_material, renderer);
            }
        }
    }

    void OnPostRender()
    {
        foreach (MaterialSwap objectWithMaterialChange in m_objectsWithMaterialChange.Values)
        {
            objectWithMaterialChange.RestoreMaterial();
        }
    }
}
