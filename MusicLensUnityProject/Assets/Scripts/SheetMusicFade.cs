using UnityEngine;

public class SheetMusicFade : MonoBehaviour {


	public Texture2D texture;
	public bool getTextureManually = false;

	private void OnEnable(){
		if (texture == null) {
		}
		//FillTexture ();
	}

	private void Start() {
	}

	private void Update(){
		//if (transform.hasChanged) {
		//	transform.hasChanged = false;
		//	FillTexture ();
		//}
		//if (runThisScript) {

		if (getTextureManually) {
			texture = this.gameObject.GetComponent<Renderer> ().material.mainTexture as Texture2D;
			getTextureManually = false;
		}
		FillTexture ();
		//}

		//Vector3 thispos = transform.position;
		//thispos.x -= .01f;

		//this.transform.position = thispos;
	}

	public void FillTexture(){
		//if (texture.width != resolution) {
		//	texture.Resize (resolution, resolution);
		//}

		//Vector3 point00 = transform.TransformPoint(new Vector3(-0.5f,-0.5f));
		//Vector3 point10 = transform.TransformPoint(new Vector3( 0.5f,-0.5f));

		Vector3 point00 = transform.TransformPoint(new Vector3(0f,0f));
		Vector3 point10 = transform.TransformPoint(new Vector3( 1f,-1f));


		float stepSize = 1f / texture.width;
		for (int y = 0; y < texture.height; y++) {
			//Vector3 point0 = Vector3.Lerp(point00, point01, (y + 0.5f) * stepSize);
			//Vector3 point1 = Vector3.Lerp(point10, point11, (y + 0.5f) * stepSize);
			for (int x = 0; x < texture.width; x++) {
				Vector3 point = Vector3.Lerp(point00, point10, (x + 0.5f) * stepSize);
				Color pixColor = texture.GetPixel(x,y); 
				Color changeTo = new Color (pixColor.r, pixColor.g, pixColor.b, point.x * 2);
				texture.SetPixel(x,y, changeTo);

				//MeshRenderer rend = GetComponent<MeshRenderer> ();
				//rend.material.color = new Color (30f, 30f, 30f, .1f);
			}
		}
		texture.Apply ();
	}
}
