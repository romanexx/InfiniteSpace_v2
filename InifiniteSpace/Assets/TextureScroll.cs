using UnityEngine;
using System.Collections;

public class TextureScroll : MonoBehaviour {


	public Vector2 texScroll;
	MeshRenderer render;
	// Use this for initialization
	void Start () 
	{
		render = GetComponent<MeshRenderer>();

	}
	
	// Update is called once per frame
	void Update () 
	{
		//texScroll += texScroll  * Time.deltaTime;
		texScroll.x = Time.time * 0.1f;
		texScroll.y = Time.time * 0.1f;
		render.material.mainTextureOffset = texScroll;
	}
}
