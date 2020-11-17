using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using PDollarGestureRecognizer;

public class Demo : MonoBehaviour {

	public Transform drawingPrefab;
	public Vector3 mousepos;
	public float checker;
	public string recogest;
	private List<Gesture> trainingSet = new List<Gesture>();
	private List<Point> drawpoints = new List<Point>();
	private List<Vector2> v_points = new List<Vector2>();
	private int strokeId = -1;
	private int vertexCount = 0;
	public List<LineRenderer> lines = new List<LineRenderer>();
	private LineRenderer currentLine;
	private GameObject empty;
	public Material metal;

	//GUI
	private string message;
	private bool recognized;
	private string newGestureName = "";
	void Start() 
	{
		empty = new GameObject();
		TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/");
		foreach (TextAsset gestureXml in gesturesXml)
			trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));
		string[] filePaths = Directory.GetFiles(Application.persistentDataPath, "*.xml");
		Debug.Log(Application.persistentDataPath);
		foreach (string filePath in filePaths)
			trainingSet.Add(GestureIO.ReadGestureFromFile(filePath));

	}

	void Update()
	{
		checker = checker + (10 * Time.deltaTime);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			if (hit.collider.gameObject.tag == "drawpad")
			{
				mousepos = hit.point;
				if (Input.GetMouseButtonDown(0))
				{
					if (recognized == false) {
						++strokeId;
						Transform tempLine = Instantiate(drawingPrefab, transform.position, transform.rotation) as Transform;
						currentLine = tempLine.GetComponent<LineRenderer>();
						lines.Add(currentLine);
						vertexCount = 0;
					}
				}
				if (Input.GetMouseButton(0))
				{
					drawpoints.Add(new Point(mousepos.x, mousepos.y, strokeId));
					v_points.Add(mousepos);
					vertexCount++;
					currentLine.positionCount = vertexCount;
					currentLine.SetPosition(vertexCount - 1, mousepos);
					checker = 0;
				}
			}
		}
		if ((checker > 20.0f)&&(currentLine!=null))
		{
			Checkdraw();
		}
		if ((checker > 21.0f) && (currentLine != null))
		{
			Clearandrun();
		}
	}
	public void Clearandrun()
	{
		recognized = false;
		strokeId = -1;

		drawpoints.Clear();
		v_points.Clear();

		foreach (LineRenderer lineRenderer in lines)
		{
			lineRenderer.positionCount = 0;
			Destroy(lineRenderer.gameObject);
		}

		lines.Clear();
		recogest = "";

	}
	public void Checkdraw()
	{
		recognized = true;

			Gesture candidate = new Gesture(drawpoints.ToArray());
			Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());
			recogest = gestureResult.GestureClass;
			Debug.Log(recogest);
	}
	public void instantobj(Vector3 spawnvec)
	{
		var parObj = Instantiate(empty, spawnvec, transform.rotation);
		var parrigid = parObj.AddComponent<Rigidbody2D>();
		parObj.transform.parent = GameObject.Find("instantiatedobjs").transform;
		parrigid.constraints = RigidbodyConstraints2D.FreezePositionX;
		parrigid.constraints = RigidbodyConstraints2D.FreezeRotation;
		parObj.transform.tag = "ground";
		foreach (LineRenderer lineRenderer in lines)
		{
			Vector3 defpos = new Vector3(0, 0, 0);
			GameObject subObj = Instantiate(empty, defpos, transform.rotation);
			GameObject colObj = Instantiate(empty, defpos, transform.rotation);
			LineRenderer newline = subObj.AddComponent<LineRenderer>();
			newline.positionCount = lineRenderer.positionCount;
			newline.startWidth = 0.1f;
			newline.endWidth = 0.1f;
			newline.material = metal;
			newline.startColor = Color.black;
			newline.endColor = Color.black;
			var col = colObj.AddComponent<EdgeCollider2D>();;
			for (int i = 0; i < newline.positionCount; i++)
			{
				var pos = lineRenderer.GetPosition(i);
				newline.SetPosition(i, pos);
			}
			var meshfilt = subObj.AddComponent<MeshFilter>();
			Mesh mesh = new Mesh();
			newline.BakeMesh(mesh, true);
			meshfilt.sharedMesh = mesh;
			var meshrend = subObj.AddComponent<MeshRenderer>();
			meshrend.material = metal;
			Destroy(newline);
			col.points = v_points.ToArray();
			subObj.transform.parent = parObj.transform;
			colObj.transform.parent = parObj.transform;
			colObj.transform.tag = "ground";
			defpos = colObj.transform.localPosition;
			defpos.z = defpos.z - defpos.z;
			colObj.transform.localPosition = defpos;
		}

	}
	public void getbombpos(GameObject bombobj)
	{
		var average = new Vector3();
		foreach (Point tempoint in drawpoints)
		{
			average.x += tempoint.X;
			average.y += tempoint.Y;
		}
		average.x /=  drawpoints.Count;
		average.y /=  drawpoints.Count;
		bombobj.GetComponent<bombscript>().bombinst(average);
	}
		
		

	void OnGUI() {

		//GUI.Label(new Rect(10, Screen.height - 40, 500, 50), message);

		//if (GUI.Button(new Rect(Screen.width - 100, 10, 100, 30), "Recognize")) {
		//}

		GUI.Label(new Rect(Screen.width - 200, 150, 70, 30), "Add as: ");
		newGestureName = GUI.TextField(new Rect(Screen.width - 150, 150, 100, 30), newGestureName);

		if (GUI.Button(new Rect(Screen.width - 50, 150, 50, 30), "Add") && drawpoints.Count > 0 && newGestureName != "") {

			string fileName = String.Format("{0}/{1}-{2}.xml", Application.persistentDataPath, newGestureName, DateTime.Now.ToFileTime());

#if !UNITY_WEBPLAYER
			GestureIO.WriteGesture(drawpoints.ToArray(), newGestureName, fileName);
#endif

			trainingSet.Add(new Gesture(drawpoints.ToArray(), newGestureName));

			newGestureName = "";
		}
	}
	
}
