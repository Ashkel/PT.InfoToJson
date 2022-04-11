using InfoToJson.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace InfoToJson.Engine
{
	public enum FieldType
	{
		Unknown,
		Town,
		Forest,
		Desert,
		Ruin,
		Dungeon,
		Iron,
		Room,
		Ice,
		Castle,
		Action,
		All
	}

	public class Field
	{
		public string FileName { get; set; }
		public string Name { get; set; }
		public FieldType Type { get; set; }

		public List<string> Monsters { get; } = new List<string>();
		public List<string> Bosses { get; } = new List<string>();

		public static Field Load(string fileName)
		{
			int line = 0;
			string text = string.Empty;

			try
			{
				Field field = new Field();

				field.FileName = new FileInfo(fileName).Name;
				field.Name = GetFieldName(field.FileName);
				field.Type = GetFieldType(field.FileName);

				using(var sr = new StreamReader(fileName, Encoding.Default))
				{
					while(!sr.EndOfStream)
					{
						++line;

						int position = 0;

						string buffer = sr.ReadLine();
						string keyword = Utils.GetWord(buffer, ref position);

						text = buffer;

						if((string.Compare(keyword, "*Ãâ¿¬ÀÚ") == 0) ||
							(string.Compare(keyword, "*ACTOR") == 0))
						{
							field.Monsters.Add(Utils.ParseString(buffer, ref position));
						}
						else if((string.Compare(keyword, "*Ãâ¿¬ÀÚµÎ¸ñ") == 0) ||
								(string.Compare(keyword, "*BOSS_ACTOR") == 0))
						{
							field.Bosses.Add(Utils.ParseString(buffer, ref position));
						}
					}
				}

				return field;
			}
			catch(Exception ex)
			{
				using(var sw = new StreamWriter(@".\Error.txt", true, Encoding.Default))
				{
					sw.Write(Utils.FormatErrorMessage(ex, fileName, line, text));
				}
			}

			return null;
		}

		private static string GetFieldName(string fileName)
		{
			foreach(var key in FieldConstants.FieldNames.Keys)
			{
				foreach(var value in FieldConstants.FieldNames[key])
				{
					if(string.Compare(value, fileName, true) == 0)
					{
						return key;
					}
				}
			}

			return string.Empty;
		}

		private static FieldType GetFieldType(string fileName)
		{
			foreach(var key in FieldConstants.FieldTypes.Keys)
			{
				foreach(var value in FieldConstants.FieldTypes[key])
				{
					if(string.Compare(value, fileName, true) == 0)
					{
						return key;
					}
				}
			}

			return FieldType.Unknown;
		}
	}

	public static class FieldConstants
	{
		public static Dictionary<FieldType, List<string>> FieldTypes = new Dictionary<FieldType, List<string>>()
		{
			{
				FieldType.Town,
				new List<string>()
				{
					"village-1.ase.spm", "pilai.ase.spm"
				}
			},
			{
				FieldType.Forest,
				new List<string>()
				{
					"fore-1.ase.spm", "fore-2.ase.spm", "fore-3.ase.spm",
					"forever-fall-01.ase.spm", "forever-fall-02.ase.spm",
					"forever-fall-03.ase.spm", "forever-fall-04.ase.spm",
				}
			},
		};

		public static Dictionary<string, List<string>> FieldNames = new Dictionary<string, List<string>>()
		{
			{ "Mata das Acacias", new List<string>() { "fore-3.ase.spm" } },
			{ "Floresta Bamboo", new List<string>() { "fore-2.ase.spm" } },
			{ "Jardim da Liberdade", new List<string>() { "fore-1.ase.spm" } },
		};
	}
}
