using InfoToJson.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoToJson.Engine
{
	public abstract class GameInfo
	{
		#region Field

		protected string _fileName;

		#endregion

		#region Helper methods

		public void Parse(string fileName)
		{
			Process(fileName, null);
		}

		public void Save(string fileName, string saveAs)
		{
			Process(fileName, saveAs);
		}

		private void Process(string fileName, string saveToFile)
		{
			_fileName = fileName;

			int line = 0;
			string text = string.Empty;

			try
			{
				var sb = new StringBuilder();

				using(var sr = new StreamReader(fileName, Encoding.Default))
				{
					while(!sr.EndOfStream)
					{
						++line;

						string buffer = sr.ReadLine();

						//if(buffer[0] != '*')
						//{
						//	continue;
						//}

						text = buffer;

						if(string.IsNullOrEmpty(saveToFile))
						{
							ParseLine(buffer);
						}
						else
						{
							EditLine(buffer, ref sb);
						}
					}
				}

				if(!string.IsNullOrEmpty(saveToFile))
				{
					using(var sw = new StreamWriter(saveToFile, false, Encoding.Default))
					{
						sw.Write(sb);
					}
				}
			}
			catch(Exception ex)
			{
				using(var sw = new StreamWriter("error.log"))
				{
					var error = Utils.FormatErrorMessage(ex, fileName, line, text);

					sw.WriteLine(error);
				}
			}
		}

		protected abstract void ParseLine(string line);

		protected abstract void EditLine(string line, ref StringBuilder sb);

		#endregion
	}
}
