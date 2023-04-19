using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects")]
public class PanelBoxData : ScriptableObject
{
	[SerializeField]
	List<int> panelDataList = new List<int>();
	public List<int> PanelDataList { get => panelDataList; }

	// CSVÇ©ÇÁÉpÉlÉãÇì«Ç›çûÇ›
	[ContextMenu("CSVLoad")]
	public void LoadData()
	{
		panelDataList.Clear();
		var fileName = "";
		fileName = EditorUtility.OpenFilePanel("CSVLoad", "", "");
		fileName = fileName.Substring(fileName.LastIndexOf("Resources/"));
		fileName = fileName.Substring(0, fileName.LastIndexOf("."));
		fileName = fileName.Substring(fileName.IndexOf("/") + 1);
		var data = TextLoader.CSVLoad(fileName);

		foreach (var item in data)
		{
			foreach (var item2 in item)
			{
				panelDataList.Add(int.Parse(item2));
			}
		}
	}
}
