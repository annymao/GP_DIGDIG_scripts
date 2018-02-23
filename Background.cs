using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

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
	private float tUnit = 1f;
	private Vector2 tBack = new Vector2 (0, 0);
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
	// Use this for initialization

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

	}


	// Update is called once per frame
	void Update () {

	}
	/// <summary>
	/// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	/// </summary>
	void FixedUpdate()
	{
		if(update){
			BuildMesh();
			UpdateMesh();
			/*transform.GetComponent<PhotonView> ().RPC("BuildMesh",PhotonTargets.All);
			transform.GetComponent<PhotonView> ().RPC("UpdateMesh",PhotonTargets.All);*/
			update=false;
		}
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

		newVertices.Add( new Vector3 (x*10  , y *10 , 0 ));
		newVertices.Add( new Vector3 ((x + 1)*10 , y*10  , 0 ));
		newVertices.Add( new Vector3 ((x + 1)*10 ,( y-1)*10, 0 ));
		newVertices.Add( new Vector3 (x*10  ,( y-1)*10 , 0 ));

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
		blocks=new byte[block_x,block_y];
		for(int px=0;px<blocks.GetLength(0);px++){

			for(int py=1;py<blocks.GetLength(1);py++){
				blocks [px, py] = 1;

			}
		}
	}
	void BuildMesh(){
		for(int px=0;px<blocks.GetLength(0);px++){
			for(int py=0;py<blocks.GetLength(1);py++){
				if(blocks[px,py]!=0){
					if (blocks [px, py] == 1) {
						GenSquare (px, py, tBack);
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

}
