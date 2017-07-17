using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTextureSetter : MonoBehaviour 
{
    public Camera TNECamera;
    public RenderTexture TNETexture;

	// Use this for initialization
	void Awake () 
    {
        TNETexture.width = Screen.width;
        TNETexture.height = Screen.height;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
