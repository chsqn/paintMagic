using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class ToolsEditor : MonoBehaviour 
{
	[MenuItem ("TOOLS/Replace Points")]
	public static void replacePoints()
	{
		//get the selected obj
		Transform[] selectedObjs = Selection.GetTransforms(SelectionMode.Unfiltered);

		foreach(Transform selectedObj in selectedObjs)
		{

			//selected drawing line
			DrawingLine drawLine = selectedObj.GetComponent<DrawingLine>();

			//get all the linepoints inside it
			LinePoint[] allLinePoints = selectedObj.GetComponentsInChildren<LinePoint>(true);

			print("size of all line POints are " + allLinePoints.Length);

			//step through all line points
			int counter = 0;
			foreach(LinePoint lp in allLinePoints)
			{
				GameObject newObj = PrefabUtility.InstantiatePrefab(Resources.Load("point", typeof(GameObject))) as GameObject;
				newObj.name = "point_" + (counter+1).ToString("00"); 

				//and replace the point with an instance from the prefab
				newObj.transform.SetParent(selectedObj);

				//set the position
				newObj.transform.position = lp.transform.position;

				//set the drawing line to the point
				LinePoint newLinePoint 		= newObj.GetComponent<LinePoint>();
				newLinePoint.mDrawingLine 	= drawLine;
				newLinePoint.ID 			= counter +1;
				newLinePoint.idText.text	= (counter + 1).ToString();

				//set the linepoint in the drawing line
				drawLine.allPoints[counter] = newLinePoint;

				counter ++;
			}

			//step through the original line points and destroy them
			foreach(LinePoint lp in allLinePoints)
			{
				//delete the lp
				DestroyImmediate(lp.gameObject);
			}

		}


	}
}


