using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using PDollarGestureRecognizer;

public class Demo : MonoBehaviour {

	public Transform drawingPrefab;
	private List<Gesture> trainingSet = new List<Gesture>();
	private List<Point> points = new List<Point>();
	private int strokeId = -1;
	public Vector3 mousepos;
	public int vertexCount = 0;
	private List<LineRenderer> lines = new List<LineRenderer>();
	public LineRenderer currentLine;
	public bool onpoint = false;
	public Transform spawnpoint;
	private Vector3 spawnvec;
	private GameObject empty;
	public bool skip = false;
	public GameObject tester;
	Animator anim;

	//GUI
	private string message;
	private bool recognized;
	private string newGestureName = "";
	void Start () {
		anim = tester.gameObject.GetComponent<Animator>();
		empty = new GameObject();
		TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/");
		foreach (TextAsset gestureXml in gesturesXml)
			trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));
		string[] filePaths = Directory.GetFiles(Application.persistentDataPath, "*.xml");
		Debug.Log(Application.persistentDataPath);
		foreach (string filePath in filePaths)
			trainingSet.Add(GestureIO.ReadGestureFromFile(filePath));
		spawnvec = spawnpoint.GetComponent<Transform>().position;

	}

	void Update()
	{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
			if (hit.collider.gameObject.tag == "drawpad")
				{
				mousepos = hit.point;
				if (Input.GetMouseButtonDown(0))
					{
						if (recognized == true)
						{

						recognized = false;
						strokeId = -1;

						points.Clear();

						foreach (LineRenderer lineRenderer in lines)
							{
							lineRenderer.positionCount = 0;
							Destroy(lineRenderer.gameObject);
							}

						lines.Clear();
						}
					++strokeId;
					Transform tempLine = Instantiate(drawingPrefab, transform.position, transform.rotation) as Transform;
					currentLine = tempLine.GetComponent<LineRenderer>();
					lines.Add(currentLine);
					vertexCount = 0;
				}
				if (Input.GetMouseButton(0))
				{
					points.Add(new Point(mousepos.x, mousepos.y, strokeId));
					vertexCount++;
					currentLine.positionCount = vertexCount;
					currentLine.SetPosition(vertexCount - 1, mousepos);

				}

			}
			}
	}

		
		


	void OnGUI() {

		GUI.Label(new Rect(10, Screen.height - 40, 500, 50), message);

		if (GUI.Button(new Rect(Screen.width - 100, 10, 100, 30), "Recognize")) {

			recognized = true;

			Gesture candidate = new Gesture(points.ToArray());
			Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());
			
			message = gestureResult.GestureClass + " " + gestureResult.Score;
			if (gestureResult.GestureClass == "slash")
			{
				anim.SetTrigger("hit");
			}
			if ((gestureResult.GestureClass == "sword")|(gestureResult.GestureClass == "bow"))
			{
				var parObj = Instantiate(empty, spawnvec, transform.rotation);
				var rigid = parObj.AddComponent<Rigidbody>();
				foreach (LineRenderer lineRenderer in lines)
				{
					GameObject subObj = Instantiate(empty, spawnvec, transform.rotation);
					LineRenderer newline = subObj.AddComponent<LineRenderer>();
					var meshfilt = subObj.AddComponent<MeshFilter>();
					newline.positionCount = lineRenderer.positionCount;
					for (int i = 0; i < newline.positionCount; i++)
					{
						var pos = lineRenderer.GetPosition(i);
						newline.SetPosition(i, pos);
						if (skip == false)
						{
							SphereCollider newball = subObj.AddComponent<SphereCollider>();
							newball.center = pos;
							newball.radius = 0.1f;
							skip = true;
						}
						else if (skip == true)
						{
							skip = false;
						}
					}
					newline.startWidth = 0.1f;
					newline.endWidth = 0.1f;
					Mesh mesh = new Mesh();
					newline.BakeMesh(mesh, true);
					meshfilt.sharedMesh = mesh;
					var meshrend = subObj.AddComponent<MeshRenderer>();
					GameObject.Destroy(newline);
					subObj.transform.parent = parObj.transform;
				}
				parObj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
			}
			
		}

		GUI.Label(new Rect(Screen.width - 200, 150, 70, 30), "Add as: ");
		newGestureName = GUI.TextField(new Rect(Screen.width - 150, 150, 100, 30), newGestureName);

		if (GUI.Button(new Rect(Screen.width - 50, 150, 50, 30), "Add") && points.Count > 0 && newGestureName != "") {

			string fileName = String.Format("{0}/{1}-{2}.xml", Application.persistentDataPath, newGestureName, DateTime.Now.ToFileTime());

			#if !UNITY_WEBPLAYER
				GestureIO.WriteGesture(points.ToArray(), newGestureName, fileName);
			#endif

			trainingSet.Add(new Gesture(points.ToArray(), newGestureName));

			newGestureName = "";
		}
	}
}
