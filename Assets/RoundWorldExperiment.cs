using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoundWorldExperiment : MonoBehaviour
{
	[Tooltip("Basis-Radius in meter.")]
	public int radius = 50;

	[Range(0, 20)]
	public int divisionPasses;

	public Platonic startSymmetry;
	private Platonic oldSymmetry;

	private List<Vector3> vertdata;
	private List<int> faceData;

	private Mesh worldMesh;

	// Use this for initialization
	void Start()
	{
		vertdata = new List<Vector3>();
		faceData = new List<int>();
		worldMesh = new Mesh();
		worldMesh.MarkDynamic();
		if (GetComponent<MeshFilter>() == null)
			gameObject.AddComponent<MeshFilter>();
		GetComponent<MeshFilter>().mesh = worldMesh;

		MeshRenderer m;
		if ((m = GetComponent<MeshRenderer>()) == null)
		{
			m = gameObject.AddComponent<MeshRenderer>();
			m.material = Resources.Load<Material>("Materials/Alpha");
			if (m.material == null)
			{
				m.material = new Material(Shader.Find("Standard"));
			}
		}

	

		if (worldMesh.vertices.Length == 0)
			createBasicGeometry();

	}


	[ExecuteInEditMode]
	void Update()
	{
		if (oldSymmetry != startSymmetry)
		{
			createBasicGeometry();
		}
	}


	private void createBasicGeometry()
	{

		createCube();
		ApplyNewMeshData();

		return;

		//switch (startSymmetry)
		//{

		//	case Platonic.Cube:
		//		{
		//			createCube();
		//			ApplyNewMeshData();

		//			return;
		//		}
		//	case Platonic.Octahedron:
		//		{

		//			return;
		//		}
		//	case Platonic.Icosahedron:
		//		{

		//			return;
		//		}
		//}
	}



	private void createCube()
	{
		vertdata.Clear();
		vertdata.Add(new Vector3(0.5f, 0.5f, -0.5f));		// 0		rechts oben hinten
		vertdata.Add(new Vector3(-0.5f, 0.5f, -0.5f));		// 1		links oben hinten
		vertdata.Add(new Vector3(-0.5f, -0.5f, -0.5f));		// 2		links unten hinten
		vertdata.Add(new Vector3(0.5f, -0.5f, -0.5f));		// 3		rechts unten hinten
		vertdata.Add(new Vector3(0.5f, 0.5f, 0.5f));		// 4		rechts oben vorne
		vertdata.Add(new Vector3(-0.5f, 0.5f, 0.5f));		// 5		links oben vorne
		vertdata.Add(new Vector3(-0.5f, -0.5f, 0.5f));		// 6		links unten vorne
		vertdata.Add(new Vector3(0.5f, -0.5f, 0.5f));		// 7		rechts unten vorne

		faceData.Clear();
		faceData.AddRange(new List<int>() { 2, 1, 0, 0, 3, 2 });
		faceData.AddRange(new List<int>() { 0, 7, 3, 7, 0, 4 });
		faceData.AddRange(new List<int>() { 0, 5, 4, 5, 0, 1 });
		faceData.AddRange(new List<int>() { 4, 6, 7, 6, 4, 5 });
		faceData.AddRange(new List<int>() { 2, 3, 7, 7, 6, 2 });
		faceData.AddRange(new List<int>() { 1, 6, 5, 6, 1, 2 });

		ApplyRadius();

	}


	private void ApplyNewMeshData()
	{
		worldMesh.Clear();
		worldMesh.vertices = vertdata.ToArray();
		worldMesh.triangles = faceData.ToArray();


	}

	private void ApplyRadius()
	{
		for (int i = 0; i < vertdata.Count; i++)
		{
			vertdata[i] = vertdata[i].normalized * radius;
		}

	}


	private void ApplySubdivision()
	{
		Debug.Log("Subdivision not yet implemented.");
	}

	public enum Platonic
	{
		Cube,
		Octahedron,
		Icosahedron

	}
}
