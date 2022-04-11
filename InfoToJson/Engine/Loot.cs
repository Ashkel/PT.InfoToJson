using InfoToJson.Helper;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace InfoToJson.Engine
{
	public struct Loot
	{
		#region Field/Properties

		/// <summary>
		/// Drop rate of items
		/// </summary>
		public int Rate;

		/// <summary>
		/// Range between min-max gold
		/// </summary>
		public Range? Money;

		/// <summary>
		/// Range between min-max gold
		/// </summary>
		public Range? Coins;

		/// <summary>
		/// List of items
		/// </summary>
		public List<string> Items;

		[JsonIgnore]
		public bool Nothing
		{
			get
			{
				return (this.Money == null) &&
						(this.Coins == null) &&
						(this.Items == null);
			}
		}

		#endregion
	}
}
