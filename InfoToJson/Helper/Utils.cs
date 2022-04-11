using InfoToJson.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace InfoToJson.Helper
{
	public static class Utils
	{
		#region Helper methods

		public static string GetWord(string line, ref int position)
		{
			var buffer = new StringBuilder();

			if(line != string.Empty)
			{
				while((position < line.Length) &&
					((line[position] == 32) ||
					(line[position] == 9)))
				{
					if((line[position] == '\r') ||
						(line[position] == '\n') ||
						(line[position] == 0))
					{
						break;
					}

					++position;
				}

				while((position < line.Length) &&
					((line[position] != 32) &&
					(line[position] != 9)))
				{
					if((line[position] == '\r') ||
						(line[position] == '\n') ||
						(line[position] == 0))
					{
						break;
					}

					buffer.Append(line[position]);

					++position;
				}
			}

			return buffer.ToString();
		}

		public static string GetString(string line, ref int position)
		{
			var buffer = new StringBuilder();

			if(line != string.Empty)
			{
				while((position < line.Length) &&
						(line[position] != 34))
				{
					if((line[position] == '\r') ||
						(line[position] == '\n') ||
						(line[position] == 0))
					{
						break;
					}

					++position;
				}

				++position;

				while((position < line.Length) &&
						(line[position] != 34))
				{
					if((line[position] == '\r') ||
						(line[position] == '\n') ||
						(line[position] == 0))
					{
						break;
					}

					buffer.Append(line[position]);

					++position;
				}

				++position;
			}

			return buffer.ToString();
		}

		public static string FormatErrorMessage(Exception exception, string fileName, int line, string text)
		{
			var sb = new StringBuilder();

			sb.AppendLine($"Exception: {exception.GetType()}");

			sb.AppendLine($" -> File: \"{fileName}\",");
			sb.AppendLine($" -> Line: \"{line}\",");

			if(!string.IsNullOrEmpty(text))
			{
				sb.AppendLine($" -> At: \"{text}\",");
			}

			sb.AppendLine($" -> Message: \"{exception.Message}\"");
			sb.AppendLine();

			return sb.ToString();
		}

		public static string FormatErrorMessage(Exception exception, string text)
		{
			var sb = new StringBuilder();

			sb.AppendLine($"Exception: {exception.GetType()}");

			if(!string.IsNullOrEmpty(text))
			{
				sb.AppendLine($" -> At: \"{text}\",");
			}

			sb.AppendLine($" -> Message: \"{exception.Message}\"");
			sb.AppendLine();

			return sb.ToString();
		}

		public static string EncodeString(string s, Encoding srcEncoding, Encoding dstEncoding)
		{
			var src = srcEncoding.GetBytes(s);
			var bytes = Encoding.Convert(srcEncoding, dstEncoding, src);

			char[] chars = new char[dstEncoding.GetCharCount(bytes, 0, bytes.Length)];

			dstEncoding.GetChars(bytes, 0, bytes.Length, chars, 0);

			return new string(chars);
		}

		public static string ComputeMD5(string file)
		{
			MD5 md5 = MD5.Create();

			string hash = "";

			using(var stream = File.OpenRead(file))
			{
				hash = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "");
			}

			return hash;
		}

		public static string ParseString(string buffer, ref int position)
		{
			int tmp = position;

			if(GetWord(buffer, ref tmp)[0] == 34)
			{
				return GetString(buffer, ref position);
			}

			return string.Empty;
		}

		public static int ParseInteger(string buffer, ref int position)
		{
			var str = GetWord(buffer, ref position);

			if(!string.IsNullOrEmpty(str))
			{
#if !DEBUG
				// fix "*ºí·°À²	 5%"
				if(str.EndsWith("%"))
				{
					str = str.Remove(str.Length - 1);
				}
#endif
				if(int.TryParse(str, out int result))
				{
					return result;
				}
			}

			return 0;
		}

		public static float ParseFloat(string buffer, ref int position)
		{
			var str = GetWord(buffer, ref position);

			if(!string.IsNullOrEmpty(str) &&
				float.TryParse(str, out float result))
			{
				return result;
			}

			return 0;
		}

		public static bool ParseBool(string buffer, ref int position, string condition = null)
		{
			if(!string.IsNullOrEmpty(condition))
			{
				var str = GetWord(buffer, ref position);

				if(string.Compare(str, condition) != 0)
				{
					return false;
				}
			}

			return true;
		}

		public static Range ParseRange(string buffer, ref int position)
		{
			int val1 = ParseInteger(buffer, ref position);
			int val2 = ParseInteger(buffer, ref position);

			return (val2 > val1) ?
						new Range(val1, val2) :
						new Range(val2, val1);
		}

		public static RangeF ParseRangeF(string buffer, ref int position)
		{
			float val1 = ParseFloat(buffer, ref position);
			float val2 = ParseFloat(buffer, ref position);

			return (val2 > val1) ?
						new RangeF(val1, val2) :
						new RangeF(val2, val1);
		}

		public static Loot? ParseLoot(string buffer, ref int position)
		{
			int temp = position;

			var str = GetWord(buffer, ref temp);

			if(str.CompareTo("Ä«¿îÅÍ") == 0)
			{
				return null;
			}

			temp = position;

			int rate = ParseInteger(buffer, ref temp);

			str = GetWord(buffer, ref temp);

			if(str.CompareTo("¾øÀ½") == 0)
			{
				return new Loot()
				{
					Rate = rate,
					Money = null,
					Coins = null,
					Items = null
				};
			}
			else if(str.CompareTo("µ·") == 0)
			{
				var money = ParseRange(buffer, ref temp);

				return new Loot()
				{
					Rate = rate,
					Money = money,
					Coins = null,
					Items = null
				};
			}
			else if(str.CompareTo("coins") == 0)
			{
				var coins = ParseRange(buffer, ref temp);

				return new Loot()
				{
					Rate = rate,
					Money = null,
					Coins = coins,
					Items = null
				};
			}
			else
			{
				var items = new List<string>();

				while(!string.IsNullOrEmpty(str))
				{
					items.Add(str);

					str = GetWord(buffer, ref temp);
				}

				return new Loot()
				{
					Rate = rate,
					Money = null,
					Coins = null,
					Items = items
				};
			}
		}

		#endregion
	}
}
