using UnityEngine;
using System.Collections;

public class MouseGUI : MonoBehaviour {

	public Texture2D crosshairTexture;
	
	void Start ()
	{
		Cursor.visible = false;
	}
	
	void OnGUI ()
	{
		Vector3 mousePos1 = Input.mousePosition;
		Rect pos1 = new Rect (Screen.width * 0.5f, Screen.height * 0.5f, 
		                     crosshairTexture.width, crosshairTexture.height);
		GUI.DrawTexture (pos1, crosshairTexture);

		Vector3 mousePos = Input.mousePosition;
		Rect pos = new Rect (mousePos.x - crosshairTexture.width * 0.5f, Screen.height - mousePos.y - crosshairTexture.height * 0.5f, 
		                     crosshairTexture.width, crosshairTexture.height);
		GUI.DrawTexture (pos, crosshairTexture);



	}
}
