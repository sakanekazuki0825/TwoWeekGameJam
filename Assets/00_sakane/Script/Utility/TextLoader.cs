using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public struct TextLoader
{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="fileName">�t�@�C����</param>
	/// <returns>�ǂݍ��񂾃f�[�^</returns>
    static public TextAsset Load(string fileName = "")
	{
		// �ǂݍ���CSV���i�[����ϐ�
		TextAsset csvFile;
		// CSV�ǂݍ���
		csvFile = Resources.Load(fileName) as TextAsset;

		return csvFile;
	}

	/// <summary>
	/// CSV�f�[�^�ǂݍ���
	/// </summary>
	/// <param name="filename">�t�@�C����</param>
	/// <returns>�ǂݍ��񂾃f�[�^</returns>
	static public List<List<string>> CSVLoad(string filename = "")
	{
		var textAsset = Load(filename);
		StringReader reader = new StringReader(textAsset.text);
		//StringReader reader = new StringReader(name);
		List<List<string>> s = new List<List<string>>();
		s.Add(new List<string>());

		// �Ō�̍s�܂Ń��[�v
		while (reader.Peek() != -1)
		{
			// ��s�ǂݍ���
			string line = reader.ReadLine();
			// ,�ŋ�؂��Ĕz��ɂ������̂�ǉ�
			s.Add(line.Split(',').ToList<string>());
		}

		return s;
	}
}
