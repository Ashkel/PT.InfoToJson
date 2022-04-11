using Newtonsoft.Json;
using System;

namespace InfoToJson.Helper
{
	public struct Range : IEquatable<Range>, IEquatable<RangeF>
	{
		#region Field/Properties

		/// <summary>
		/// The minimum value for the range, inclusively.
		/// </summary>
		public int Min
		{
			get
			{
				return _min;
			}
			set
			{
				_min = value;
			}
		}
		private int _min;

		/// <summary>
		/// The maximum value for the range, inclusively.
		/// </summary>
		public int Max
		{
			get
			{
				return _max;
			}
			set
			{
				_max = value;
			}
		}
		private int _max;

		/// <summary>
		/// Calculate the size of this range.
		/// </summary>
		[JsonIgnore]
		public int Size
		{
			get
			{
				return (Max - Min);
			}
		}

		/// <summary>
		/// Calculate the average value returned by this range.
		/// </summary>
		[JsonIgnore]
		public int Average
		{
			get
			{
				return Min + Size / 2;
			}
		}

		private static Random _random;

		#endregion

		#region Constructor(s)

		public Range(int min, int max)
		{
			if(min > max)
			{
				throw new ArgumentException("The Max could be greater than Min");
			}

			this._min = min;
			this._max = max;

			_random = null;
		}

		public Range(RangeF range)
			: this((int)range.Min, (int)range.Max)
		{
		}

		#endregion

		#region Overload

		public static Range Add(Range lValue, Range rValue)
		{
			return new Range(
						lValue.Min + rValue.Min,
						lValue.Max + rValue.Max);
		}

		public static Range Add(Range lValue, int rValue)
		{
			return new Range(
						lValue.Min + rValue,
						lValue.Max + rValue);
		}

		public static Range operator +(Range lValue, Range rValue)
		{
			return Add(lValue, rValue);
		}

		public static Range operator +(Range lValue, int rValue)
		{
			return Add(lValue, rValue);
		}

		public static Range Subtract(Range lValue, Range rValue)
		{
			return new Range(
						lValue.Min - rValue.Min,
						lValue.Max - rValue.Max);
		}

		public static Range Subtract(Range lValue, int rValue)
		{
			return new Range(
						lValue.Min - rValue,
						lValue.Max - rValue);
		}

		public static Range operator -(Range lValue, Range rValue)
		{
			return Subtract(lValue, rValue);
		}

		public static Range operator -(Range lValue, int rValue)
		{
			return Subtract(lValue, rValue);
		}

		public static Range Multiply(Range lValue, Range rValue)
		{
			return new Range(
						lValue.Min * rValue.Min,
						lValue.Max * rValue.Max);
		}

		public static Range Multiply(Range lValue, int rValue)
		{
			return new Range(
						lValue.Min * rValue,
						lValue.Max * rValue);
		}

		public static Range operator *(Range lValue, Range rValue)
		{
			return Multiply(lValue, rValue);
		}

		public static Range operator *(Range lValue, int rValue)
		{
			return Multiply(lValue, rValue);
		}

		public static Range Divide(Range lValue, Range rValue)
		{
			return new Range(
						lValue.Min / rValue.Min,
						lValue.Max / rValue.Max);
		}

		public static Range Divide(Range lValue, int rValue)
		{
			return new Range(
						lValue.Min / rValue,
						lValue.Max / rValue);
		}

		public static Range operator /(Range lValue, Range rValue)
		{
			return Divide(lValue, rValue);
		}

		public static Range operator /(Range lValue, int rValue)
		{
			return Divide(lValue, rValue);
		}

		public static bool operator ==(Range lValue, Range rValue)
		{
			return lValue.Equals(rValue);
		}

		public static bool operator !=(Range lValue, Range rValue)
		{
			return !(lValue == rValue);
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Generate a random value between the Min and Max, inclusively.
		/// </summary>
		public int Random()
		{
			if(_random == null)
			{
				_random = new Random();
			}

			lock(_random)
			{
				return _random.Next(this.Min, this.Max + 1);
			}
		}

		public override int GetHashCode()
		{
			int hash = base.GetHashCode();

			hash += (this.Min * 16) & 0xFF;
			hash += (this.Max * 17) & 0xFF;

			return hash;
		}

		public override bool Equals(object obj)
		{
			if(obj is Range)
			{
				return this.Equals((Range)obj);
			}
			else if(obj is RangeF)
			{
				return this.Equals((RangeF)obj);
			}

			return false;
		}

		public bool Equals(Range range)
		{
			return ((this.Min == range.Min) &&
					(this.Max == range.Max));
		}

		public bool Equals(RangeF range)
		{
			return ((this.Min == range.Min) &&
					(this.Max == range.Max));
		}

		public override string ToString()
		{
			//return $"Min: {Min}, Max: {Max}";
			return string.Format("Min: {0}, Max: {1}", this.Min, this.Max);
		}

		#endregion
	}

	public struct RangeF : IEquatable<RangeF>, IEquatable<Range>
	{
		#region Field/Properties

		/// <summary>
		/// The minimum value for the range, inclusively.
		/// </summary>
		public float Min
		{
			get
			{
				return _min;
			}
			set
			{
				_min = value;
			}
		}
		private float _min;

		/// <summary>
		/// The maximum value for the range, inclusively.
		/// </summary>
		public float Max
		{
			get
			{
				return _max;
			}
			set
			{
				_max = value;
			}
		}
		private float _max;

		/// <summary>
		/// Calculate the size of this range.
		/// </summary>
		[JsonIgnore]
		public float Size
		{
			get
			{
				return (Max - Min);
			}
		}

		/// <summary>
		/// Calculate the average value returned by this range.
		/// </summary>
		[JsonIgnore]
		public float Average
		{
			get
			{
				return Min + Size / 2;
			}
		}

		private static Random _random;

		#endregion

		#region Constructor(s)

		public RangeF(float min, float max)
		{
			if(min > max)
			{
				throw new ArgumentException("The Max could be greater than Min");
			}

			this._min = min;
			this._max = max;

			_random = null;
		}

		public RangeF(Range range)
			: this(range.Min, range.Max)
		{
		}

		#endregion

		#region Overload

		public static RangeF Add(RangeF lValue, RangeF rValue)
		{
			return new RangeF(
						lValue.Min + rValue.Min,
						lValue.Max + rValue.Max);
		}

		public static RangeF Add(RangeF lValue, float rValue)
		{
			return new RangeF(
						lValue.Min + rValue,
						lValue.Max + rValue);
		}

		public static RangeF operator +(RangeF lValue, RangeF rValue)
		{
			return Add(lValue, rValue);
		}

		public static RangeF operator +(RangeF lValue, float rValue)
		{
			return Add(lValue, rValue);
		}

		public static RangeF Subtract(RangeF lValue, RangeF rValue)
		{
			return new RangeF(
						lValue.Min - rValue.Min,
						lValue.Max - rValue.Max);
		}

		public static RangeF Subtract(RangeF lValue, float rValue)
		{
			return new RangeF(
						lValue.Min - rValue,
						lValue.Max - rValue);
		}

		public static RangeF operator -(RangeF lValue, RangeF rValue)
		{
			return Subtract(lValue, rValue);
		}

		public static RangeF operator -(RangeF lValue, float rValue)
		{
			return Subtract(lValue, rValue);
		}

		public static RangeF Multiply(RangeF lValue, RangeF rValue)
		{
			return new RangeF(
						lValue.Min * rValue.Min,
						lValue.Max * rValue.Max);
		}

		public static RangeF Multiply(RangeF lValue, float rValue)
		{
			return new RangeF(
						lValue.Min * rValue,
						lValue.Max * rValue);
		}

		public static RangeF operator *(RangeF lValue, RangeF rValue)
		{
			return Multiply(lValue, rValue);
		}

		public static RangeF operator *(RangeF lValue, float rValue)
		{
			return Multiply(lValue, rValue);
		}

		public static RangeF Divide(RangeF lValue, RangeF rValue)
		{
			return new RangeF(
						lValue.Min / rValue.Min,
						lValue.Max / rValue.Max);
		}

		public static RangeF Divide(RangeF lValue, float rValue)
		{
			return new RangeF(
						lValue.Min / rValue,
						lValue.Max / rValue);
		}

		public static RangeF operator /(RangeF lValue, RangeF rValue)
		{
			return Divide(lValue, rValue);
		}

		public static RangeF operator /(RangeF lValue, float rValue)
		{
			return Divide(lValue, rValue);
		}

		public static bool operator ==(RangeF lValue, RangeF rValue)
		{
			return lValue.Equals(rValue);
		}

		public static bool operator !=(RangeF lValue, RangeF rValue)
		{
			return !(lValue == rValue);
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Generate a random value between the Min and Max, inclusively.
		/// </summary>
		public float Random()
		{
			if(_random == null)
			{
				_random = new Random();
			}

			return (float)(_random.NextDouble() * (double)(this.Max - this.Min)) + this.Min;
		}

		public override int GetHashCode()
		{
			int hash = base.GetHashCode();

			hash += (int)(this.Min * 16) & 0xFF;
			hash += (int)(this.Max * 17) & 0xFF;

			return hash;
		}

		public override bool Equals(object obj)
		{
			if(obj is RangeF)
			{
				return this.Equals((RangeF)obj);
			}
			else if(obj is Range)
			{
				return this.Equals((Range)obj);
			}

			return false;
		}

		public bool Equals(RangeF range)
		{
			return ((this.Min == range.Min) &&
					(this.Max == range.Max));
		}

		public bool Equals(Range range)
		{
			return ((this.Min == range.Min) &&
					(this.Max == range.Max));
		}

		public override string ToString()
		{
			//return $"Min: {Min}, Max: {Max}";
			return string.Format("Min: {0}, Max: {1}", this.Min, this.Max);
		}

		#endregion
	}
}
