using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public struct TextLoader
{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="fileName">ファイル名</param>
	/// <returns>読み込んだデータ</returns>
    static public TextAsset Load(string fileName = "")
	{
		// 読み込んだCSVを格納する変数
		TextAsset csvFile;
		// CSV読み込み
		csvFile = Resources.Load(fileName) as TextAsset;

		return csvFile;
	}

	/// <summary>
	/// CSVデータ読み込み
	/// </summary>
	/// <param name="filename">ファイル名</param>
	/// <returns>読み込んだデータ</returns>
	static public List<List<string>> CSVLoad(string filename = "")
	{
		var textAsset = Load(filename);
		StringReader reader = new StringReader(textAsset.text);
		//StringReader reader = new StringReader(name);
		List<List<string>> s = new List<List<string>>();
		s.Add(new List<string>());

		// 最後の行までループ
		while (reader.Peek() != -1)
		{
			// 一行読み込み
			string line = reader.ReadLine();
			// ,で区切って配列にしたものを追加
			s.Add(line.Split(',').ToList<string>());
		}

		return s;
	}
}
