using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour {

	public List<Vector3> newVertices = new List<Vector3>();

	// The triangles tell Unity how to build each section of the mesh joining
	// the vertices
	public List<int> newTriangles = new List<int>();

	// The UV list is unimportant right now but it tells Unity how the texture is
	// aligned on each polygon
	public List<Vector2> newUV = new List<Vector2>();

	public byte[,] blocks;
	public int block_x;
	public int block_y;
	// A mesh is made up of the vertices, triangles and UVs we are going to define,
	// after we make them up we'll save them as this mesh
	private Mesh mesh;
	public float tUnit = 0.4f;
	private Vector2 tDirt = new Vector2 (0, 0);
	private Vector2 tDirt1 = new Vector2 (0, 1);
	private Vector2 tDirt2 = new Vector2 (0, 2);
	private Vector2 tDirt3 = new Vector2 (0, 3);
	/*private Vector2 tStone = new Vector2 (0, 1);
	private Vector2 tGrass = new Vector2 (0, 2);*/
	private int squareCount;
	//MeshCollider

	public List<Vector3> colVertices = new List<Vector3>();
	public List<int> colTriangles = new List<int>();
	private int colCount;
	public float offset_x;
	public float offset_y;
	private MeshCollider col;
	public bool update=false;
	public bool refresh = false;
	float time = 0.5f;
	float lastspawn;
	// Use this for initialization
	void Awake(){
		blocks=new byte[155,128];
	}
	void Start () {

		mesh = GetComponent<MeshFilter> ().mesh;
		//col = GetComponent<MeshCollider> ();
		float x = transform.position.x;
		float y = transform.position.y;
		float z = transform.position.z;
		Debug.Log (gameObject.name);
		GenTerrain();
		BuildMesh ();
		UpdateMesh();
		Debug.Log("Start!");
	}


	// Update is called once per frame
	void Update () {
		if (lastspawn + time <= Time.time) {
			UpdateTerrain ();
			BuildMesh ();
			UpdateMesh ();
			lastspawn = Time.time;
		}

	}
	/// <summary>
	/// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	/// </summary>
	void FixedUpdate()
	{
		
	}
	//[PunRPC]
	void UpdateMesh () {


		mesh.Clear ();
		mesh.vertices = newVertices.ToArray();
		mesh.triangles = newTriangles.ToArray();
		mesh.uv = newUV.ToArray();
		;
		mesh.RecalculateNormals ();

		squareCount=0;
		newVertices.Clear();
		newTriangles.Clear();
		newUV.Clear();
		Mesh newMesh = new Mesh();
		newMesh.vertices = colVertices.ToArray();
		newMesh.triangles = colTriangles.ToArray();
		//col.sharedMesh= newMesh;

		colVertices.Clear();
		colTriangles.Clear();
		colCount=0;
	}
	void GenSquare(int x, int y, Vector2 texture){

		newVertices.Add( new Vector3 (x  , y  , 0 ));
		newVertices.Add( new Vector3 (x + 1 , y  , 0 ));
		newVertices.Add( new Vector3 (x + 1 , y-1 , 0 ));
		newVertices.Add( new Vector3 (x  , y-1 , 0 ));

		newTriangles.Add(squareCount*4);
		newTriangles.Add((squareCount*4)+1);
		newTriangles.Add((squareCount*4)+3);
		newTriangles.Add((squareCount*4)+1);
		newTriangles.Add((squareCount*4)+2);
		newTriangles.Add((squareCount*4)+3);

		newUV.Add(new Vector2 (tUnit * texture.x, tUnit * texture.y + tUnit));
		newUV.Add(new Vector2 (tUnit*texture.x+tUnit, tUnit*texture.y+tUnit));
		newUV.Add(new Vector2 (tUnit * texture.x + tUnit, tUnit * texture.y));
		newUV.Add(new Vector2 (tUnit * texture.x, tUnit * texture.y));

		squareCount++;
	}
	void GenTerrain(){
		GameObject terrain;
		//blocks [140, 96] = 1;
		if (offset_y == 0) {
			terrain = GameObject.Find ("terrain3(Clone)");
		} else {
			terrain = GameObject.Find ("terrain2(Clone)");
		}
		if (terrain != null) {
			for(int px=1;px<blocks.GetLength(0)-1;px++){

				for(int py=1;py<blocks.GetLength(1)-1;py++){
					if (terrain.GetComponent<PolyGen> ().blocks [px, py] == 0 && (blocks [px - 1, py] != 0 || blocks [px + 1, py] != 0 || blocks [px, py + 1] != 0)) {
						if(blocks [px, py] == 0)blocks [px, py] = 1;
						else if(blocks [px, py] == 1)blocks [px, py] = 2;
						else if(blocks [px, py] == 2)blocks [px, py] = 3;
						else if(blocks [px, py] == 3)blocks [px, py] = 4;
						else if(blocks [px, py] == 4)blocks [px, py] = 1;
					}
						

				}
			}
		}

	}
	void UpdateTerrain(){
		GameObject terrain;
		//blocks [140, 96] = 1;
		if (offset_y == 0) {
			terrain = GameObject.Find ("terrain3(Clone)");
		} else {
			terrain = GameObject.Find ("terrain2(Clone)");
		}
		if (terrain != null) {
			for(int px=1;px<blocks.GetLength(0)-1;px++){

				for(int py=1;py<blocks.GetLength(1)-1;py++){
					if (terrain.GetComponent<PolyGen> ().blocks [px, py] == 0 && (blocks [px - 1, py] != 0 || blocks [px + 1, py] != 0 || blocks [px, py + 1] != 0)) {
						if(blocks [px, py] == 0)blocks [px, py] = 1;
						else if(blocks [px, py] == 1)blocks [px, py] = 2;
						else if(blocks [px, py] == 2)blocks [px, py] = 3;
						else if(blocks [px, py] == 3)blocks [px, py] = 4;
						else if(blocks [px, py] == 4)blocks [px, py] = 1;
					}

				}
			}
		}
	}
	void ColliderTriangles(){
		colTriangles.Add(colCount*4);
		colTriangles.Add((colCount*4)+1);
		colTriangles.Add((colCount*4)+3);
		colTriangles.Add((colCount*4)+1);
		colTriangles.Add((colCount*4)+2);
		colTriangles.Add((colCount*4)+3);
	}
	void BuildMesh(){
		for(int px=0;px<blocks.GetLength(0);px++){
			for(int py=0;py<blocks.GetLength(1);py++){
				if(blocks[px,py]!=0){
					if (blocks [px, py] == 1) {
						GenSquare (px, py, tDirt);
					}else if (blocks [px, py] == 2) {
						GenSquare (px, py, tDirt1);
					} else if (blocks [px, py] == 3) {
						GenSquare (px, py, tDirt2);
					} else if (blocks [px, py] == 4) {
						GenSquare (px, py, tDirt3);
					} 
				}


			}
		}
	}
	byte Block (int x, int y){

		if(x==-1 || x==blocks.GetLength(0) ||   y==-1 || y==blocks.GetLength(1)){
			return (byte)1;
		}

		return blocks[x,y];
	}
	[PunRPC]
	public void SetInitialBlock(int x, int y){
		blocks [x, y] = 1; 
	}
}
