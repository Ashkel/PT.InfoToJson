using InfoToJson.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace InfoToJson.Engine
{
	public enum MonsterType
	{
		Normal,
		Mutant,
		Mechanic,
		Demon,
		Undead
	}

	public enum MonsterClass
	{
		Normal = 0,
		Boss = 1,
		Hammer = 200,
		Ghost = 300
	}

	public class Monster
	{
		public bool IsBoss { get; set; }

		public string Image { get; set; }

		public string KName { get; set; }

		public string Name { get; set; }
		
		public MonsterType Type { get; set; }

		public int Level { get; set; }

		public int Hp { get; set; }

		public int Experience { get; set; }

		private string _zhoonFile;


		private static Dictionary<string, MonsterType> MonsterTypes = new Dictionary<string, MonsterType>()
		{
			{"¾ðµ¥µå", MonsterType.Undead},
			{"¹ÂÅÏÆ®", MonsterType.Mutant},
			{"µð¸Õ", MonsterType.Demon},
			{"¸ÞÄ«´Ð", MonsterType.Mechanic}
		};


		public static Monster Load(string fileName)
		{
			int line = 0;
			string text = string.Empty;

			try
			{
				Monster monster = new Monster();

				using(var sr = new StreamReader(fileName, Encoding.Default))
				{
					while(!sr.EndOfStream)
					{
						++line;

						int position = 0;

						string buffer = sr.ReadLine();
						string keyword = Utils.GetWord(buffer, ref position);

						text = buffer;

						if(string.Compare(keyword, "*µÎ¸ñ") == 0)
						{
							monster.IsBoss = true;
						}
						else if(string.Compare(keyword, "*°è±Þ") == 0)
						{
							if(!monster.IsBoss)
							{
								var mc = (MonsterClass)Utils.ParseInteger(buffer, ref position);

								monster.IsBoss = (mc == MonsterClass.Boss);
							}
						}
						else if(string.Compare(keyword, "*¸ð¾çÆÄÀÏ") == 0)
						{
							string model = Utils.ParseString(buffer, ref position);

							FileInfo fi = new FileInfo(model);

							monster.Image = fi.Name.Replace(fi.Extension, ".gif");
						}
						else if(string.Compare(keyword, "*ÀÌ¸§") == 0)
						{
							monster.KName = Utils.ParseString(buffer, ref position);
						}
						else if(string.Compare(keyword, "*¸ó½ºÅÍÁ¾Á·") == 0)
						{
							string key = Utils.GetWord(buffer, ref position);

							monster.Type = MonsterType.Normal;

							if(MonsterTypes.ContainsKey(key))
							{
								monster.Type = MonsterTypes[key];
							}
						}
						else if(string.Compare(keyword, "*·¹º§") == 0)
						{
							monster.Level = Utils.ParseInteger(buffer, ref position);
						}
						else if(string.Compare(keyword, "*°æÇèÄ¡") == 0)
						{
							monster.Experience = Utils.ParseInteger(buffer, ref position);
						}
						else if(string.Compare(keyword, "*¿¬°áÆÄÀÏ") == 0)
						{
							monster._zhoonFile = Utils.ParseString(buffer, ref position);
						}
						else
						{
							var hp = new string[] { "*»ý¸í·Â", "*¶óÀÌÇÁ" };

							foreach(var key in hp)
							{
								if(string.Compare(keyword, key) == 0)
								{
									monster.Hp = Utils.ParseInteger(buffer, ref position);

									break;
								}
							}
						}
					}
				}

				if(!string.IsNullOrEmpty(monster._zhoonFile))
				{
					var folder = Path.GetDirectoryName(fileName);
					var zhoonFile = Path.Combine(folder, monster._zhoonFile);

					if(!File.Exists(zhoonFile))
						monster.Name = "None";

					using(var sr = new StreamReader(zhoonFile, Encoding.Default))
					{
						line = 0;

						while(!sr.EndOfStream)
						{
							++line;

							int position = 0;

							string buffer = sr.ReadLine();
							string keyword = Utils.GetWord(buffer, ref position);

							text = buffer;

							var names = new string[] { "*ÀÌ¸§", "*A_NAME", "*B_NAME", "*C_NAME", "*E_NAME", "*J_NAME", "*T_NAME", "*TH_NAME", "*V_NAME" };

							foreach(var key in names)
							{
								if(string.Compare(keyword, key, true) == 0)
								{
									monster.Name = Utils.ParseString(buffer, ref position);

									break;
								}
							}
						}
					}
				}

				return monster;
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
	}
}
