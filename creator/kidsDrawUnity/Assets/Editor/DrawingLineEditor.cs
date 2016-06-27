using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(DrawingLine))]
public class DrawingLineEditor : Editor 
{
	DrawingLine drawingLineScript;

	public override void OnInspectorGUI()
	{
		DrawingLine drawingLineScript = (DrawingLine) target;

		DrawDefaultInspector();
		DrawExtraGUI(drawingLineScript);
	}



	void DrawExtraGUI(DrawingLine script)
	{

		if(GUILayout.Button("Create Points"))
		{
			script.createPoints();
		}
	}

}
