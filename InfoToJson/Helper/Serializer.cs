using Newtonsoft.Json;
using System;
using System.IO;
using System.Xml.Serialization;

namespace InfoToJson.Helper
{
	/// <summary>
	/// An static class which Serializes/Deserializes XML files.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public static class Serializer<T> where T : new()
	{
		#region Methods

		/// <summary>
		/// Serializes an object in an xml file.
		/// </summary>
		/// <param name="path">The path to the destination file.</param>
		/// <param name="obj">The object to be serialized.</param>
		public static void SaveAsXml(string path, T obj)
		{
			try
			{
				using(var sw = new StreamWriter(path))
				{
					var xs = new XmlSerializer(typeof(T));

					xs.Serialize(sw, obj);
				}
			}
			catch(Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Deserializes an xml file in an object.
		/// </summary>
		/// <param name="path">The path to the destination file.</param>
		/// <returns>The object deserialized from the file</returns>
		public static T LoadFromXml(string path)
		{
			try
			{
				if(File.Exists(path))
				{
					using(var sr = new StreamReader(path))
					{
						var xs = new XmlSerializer(typeof(T));

						T obj = (T)xs.Deserialize(sr);

						return obj;
					}
				}
			}
			catch(Exception)
			{
				throw;
			}

			SaveAsXml(path, new T());

			return new T();
		}

		/// <summary>
		/// Serializes an object in an json file.
		/// </summary>
		/// <param name="path">The path to the destination file.</param>
		/// <param name="obj">The object to be serialized.</param>
		/// <param name="indent">Indent output json text.</param>
		public static void SaveAsJson(string path, T obj, bool indent = false)
		{
			try
			{
				using(var sw = new StreamWriter(path))
				{
					string json = JsonConvert.SerializeObject(obj, indent ? Formatting.Indented : Formatting.None);

					sw.Write(json);
				}
			}
			catch(Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Deserializes an json file in an object.
		/// </summary>
		/// <param name="path">The path to the destination file.</param>
		/// <returns>The object deserialized from the file</returns>
		public static T LoadFromJson(string path)
		{
			try
			{
				if(File.Exists(path))
				{
					using(var sr = new StreamReader(path))
					{
						T obj = JsonConvert.DeserializeObject<T>(sr.ReadToEnd());

						return obj;
					}
				}
			}
			catch(Exception)
			{
				throw;
			}

			SaveAsJson(path, new T());

			return new T();
		}

		#endregion
	}
}
