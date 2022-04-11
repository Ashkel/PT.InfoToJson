using InfoToJson.Engine;
using InfoToJson.Helper;
using System.Collections.Generic;
using System.IO;

namespace InfoToJson
{
	public class InfoSerializer
	{
		private string _serverPath;
		private bool _saveIndented;

		public List<Monster> Monsters
		{
			get => _monsters;
		}
		private List<Monster> _monsters = new List<Monster>();

		public List<Field> Fields
		{
			get => _fields;
		}
		private List<Field> _fields = new List<Field>();

		public string GameServer => Path.Combine(_serverPath, @"GameServer\");

		public InfoSerializer(string serverPath, bool saveIndented = false)
		{
			_serverPath = serverPath;
			_saveIndented = saveIndented;
		}

		public void Go()
		{
			LoadMonsters();
			LoadFields();

			Serializer<List<Monster>>.SaveAsJson(@".\Monsters.json", Monsters, _saveIndented);
			//Serializer<List<Monster>>.SaveAsXml(@".\Monsters.xml", Monsters);
			
			Serializer<List<Field>>.SaveAsJson(@".\Fields.json", Fields, _saveIndented);
			//Serializer<List<Field>>.SaveAsXml(@".\Fields.xml", Fields);

			System.Windows.Forms.MessageBox.Show("Process complete!", "Done!");
		}

		private void LoadMonsters()
		{
			var folder = Path.Combine(GameServer, "Monster");
			var files = Directory.GetFiles(folder, "*.inf");

			foreach(var file in files)
			{
				_monsters.Add(Monster.Load(file));
			}
		}

		private void LoadFields()
		{
			var folder = Path.Combine(GameServer, "Field");
			var files = Directory.GetFiles(folder, "*.spm");

			foreach(var file in files)
				_fields.Add(Field.Load(file));
		}
	}
}