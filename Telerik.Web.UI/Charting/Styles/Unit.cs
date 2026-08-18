using System;
using System.ComponentModel;
using System.Globalization;

namespace Telerik.Charting.Styles
{
	// Token: 0x02001800 RID: 6144
	[TypeConverter(typeof(UnitConverter))]
	[Serializable]
	public class Unit
	{
		// Token: 0x0600EECE RID: 61134 RVA: 0x00365F83 File Offset: 0x00364183
		public static bool operator !=(Unit left, Unit right)
		{
			return !(left == right);
		}

		// Token: 0x0600EECF RID: 61135 RVA: 0x00365F8F File Offset: 0x0036418F
		public static bool operator ==(Unit left, Unit right)
		{
			if (object.ReferenceEquals(left, null) == object.ReferenceEquals(right, null))
			{
				if (object.ReferenceEquals(left, null))
				{
					return true;
				}
				if (left.Type == right.Type)
				{
					return left.Value == right.Value;
				}
			}
			return false;
		}

		// Token: 0x0600EED0 RID: 61136 RVA: 0x00365FCA File Offset: 0x003641CA
		public static implicit operator Unit(float n)
		{
			return Unit.Pixel(n);
		}

		// Token: 0x0600EED1 RID: 61137 RVA: 0x00365FD2 File Offset: 0x003641D2
		public static Unit Parse(string s)
		{
			return new Unit(s, CultureInfo.CurrentCulture);
		}

		// Token: 0x0600EED2 RID: 61138 RVA: 0x00365FDF File Offset: 0x003641DF
		public static Unit Parse(string s, CultureInfo culture)
		{
			return new Unit(s, culture);
		}

		// Token: 0x0600EED3 RID: 61139 RVA: 0x00365FE8 File Offset: 0x003641E8
		public static Unit Pixel(float n)
		{
			return new Unit(n);
		}

		// Token: 0x0600EED4 RID: 61140 RVA: 0x00365FF0 File Offset: 0x003641F0
		public static Unit Percentage(double n)
		{
			return new Unit(n, UnitType.Percentage);
		}

		// Token: 0x0600EED5 RID: 61141 RVA: 0x00365FFC File Offset: 0x003641FC
		private static string GetStringFromType(UnitType type)
		{
			switch (type)
			{
			case UnitType.Pixel:
				return "px";
			case UnitType.Percentage:
				return "%";
			default:
				return string.Empty;
			}
		}

		// Token: 0x0600EED6 RID: 61142 RVA: 0x0036602E File Offset: 0x0036422E
		private static UnitType GetTypeFromString(string value)
		{
			if (value == null || value.Length <= 0)
			{
				return UnitType.Pixel;
			}
			if (value.Equals("px"))
			{
				return UnitType.Pixel;
			}
			if (value.Equals("%"))
			{
				return UnitType.Percentage;
			}
			return UnitType.Pixel;
		}

		// Token: 0x0600EED7 RID: 61143 RVA: 0x0036605D File Offset: 0x0036425D
		public Unit()
		{
			this.unitParentPixelValue = 0f;
			this.unitType = UnitType.Pixel;
		}

		// Token: 0x0600EED8 RID: 61144 RVA: 0x00366077 File Offset: 0x00364277
		public Unit(UnitType type) : this()
		{
			this.unitType = type;
		}

		// Token: 0x0600EED9 RID: 61145 RVA: 0x00366086 File Offset: 0x00364286
		public Unit(double value) : this()
		{
			this.unitValue = (float)value;
			this.unitType = UnitType.Pixel;
		}

		// Token: 0x0600EEDA RID: 61146 RVA: 0x0036609D File Offset: 0x0036429D
		public Unit(float value) : this()
		{
			this.unitValue = value;
			this.unitType = UnitType.Pixel;
		}

		// Token: 0x0600EEDB RID: 61147 RVA: 0x003660B3 File Offset: 0x003642B3
		public Unit(int value) : this()
		{
			this.unitValue = (float)value;
			this.unitType = UnitType.Pixel;
		}

		// Token: 0x0600EEDC RID: 61148 RVA: 0x003660CA File Offset: 0x003642CA
		public Unit(int value, UnitType type) : this(value)
		{
			this.unitType = type;
		}

		// Token: 0x0600EEDD RID: 61149 RVA: 0x003660DA File Offset: 0x003642DA
		public Unit(double value, UnitType type) : this(value)
		{
			this.unitType = type;
		}

		// Token: 0x0600EEDE RID: 61150 RVA: 0x003660EA File Offset: 0x003642EA
		public Unit(float value, UnitType type) : this(value)
		{
			this.unitType = type;
		}

		// Token: 0x0600EEDF RID: 61151 RVA: 0x003660FA File Offset: 0x003642FA
		public Unit(string value) : this(value, CultureInfo.CurrentCulture, UnitType.Pixel)
		{
		}

		// Token: 0x0600EEE0 RID: 61152 RVA: 0x00366109 File Offset: 0x00364309
		public Unit(string value, CultureInfo culture) : this(value, culture, UnitType.Pixel)
		{
		}

		// Token: 0x0600EEE1 RID: 61153 RVA: 0x00366114 File Offset: 0x00364314
		internal Unit(string value, CultureInfo culture, UnitType defaultType)
		{
			this.unitType = UnitType.Pixel;
			if (string.IsNullOrEmpty(value))
			{
				this.unitType = UnitType.Pixel;
				return;
			}
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			string text = value.Trim().ToLower(CultureInfo.InvariantCulture);
			int length = text.Length;
			int num = -1;
			for (int i = 0; i < length; i++)
			{
				char c = text[i];
				if ((c < '0' || c > '9') && c != '-' && c != '.' && c != ',')
				{
					break;
				}
				num = i;
			}
			if (num < length - 1)
			{
				this.unitType = Unit.GetTypeFromString(text.Substring(num + 1).Trim());
			}
			else
			{
				this.unitType = defaultType;
			}
			string text2 = text.Substring(0, num + 1);
			try
			{
				if (this.unitType == UnitType.Pixel)
				{
					this.unitValue = (float)((int)new Int32Converter().ConvertFromString(null, culture, text2));
				}
				else
				{
					this.unitValue = (float)new SingleConverter().ConvertFromString(null, culture, text2);
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600EEE2 RID: 61154 RVA: 0x00366220 File Offset: 0x00364420
		public override int GetHashCode()
		{
			return this.unitValue.GetHashCode() ^ this.unitType.GetHashCode();
		}

		// Token: 0x0600EEE3 RID: 61155 RVA: 0x00366240 File Offset: 0x00364440
		public override bool Equals(object obj)
		{
			if (obj != null)
			{
				Unit unit = obj as Unit;
				if (unit != null && unit.unitType == this.unitType && unit.unitValue == this.unitValue)
				{
					return true;
				}
			}
			return base.Equals(obj);
		}

		// Token: 0x0600EEE4 RID: 61156 RVA: 0x00366288 File Offset: 0x00364488
		internal void CalculatePixelValue(float from)
		{
			if (!float.IsNaN(from))
			{
				switch (this.unitType)
				{
				case UnitType.Pixel:
					this.unitPixelValue = this.unitValue;
					return;
				case UnitType.Percentage:
					this.unitParentPixelValue = from;
					this.unitPixelValue = from / 100f * this.unitValue;
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x0600EEE5 RID: 61157 RVA: 0x003662DD File Offset: 0x003644DD
		internal void CalculatePixelValue()
		{
			this.CalculatePixelValue(this.unitParentPixelValue);
		}

		// Token: 0x0600EEE6 RID: 61158 RVA: 0x003662EB File Offset: 0x003644EB
		internal Unit Clone()
		{
			return new Unit(this.unitValue, this.unitType);
		}

		// Token: 0x0600EEE7 RID: 61159 RVA: 0x003662FE File Offset: 0x003644FE
		public override string ToString()
		{
			return this.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x0600EEE8 RID: 61160 RVA: 0x0036630C File Offset: 0x0036450C
		public string ToString(CultureInfo culture)
		{
			if (this.IsEmpty)
			{
				return string.Empty;
			}
			string str;
			if (this.unitType == UnitType.Pixel)
			{
				str = ((int)this.unitValue).ToString(culture);
			}
			else
			{
				str = this.unitValue.ToString(culture);
			}
			return str + Unit.GetStringFromType(this.unitType);
		}

		// Token: 0x1700483D RID: 18493
		// (get) Token: 0x0600EEE9 RID: 61161 RVA: 0x00366361 File Offset: 0x00364561
		public bool IsEmpty
		{
			get
			{
				return this.unitType == (UnitType)0;
			}
		}

		// Token: 0x1700483E RID: 18494
		// (get) Token: 0x0600EEEA RID: 61162 RVA: 0x0036636C File Offset: 0x0036456C
		internal float PixelValue
		{
			get
			{
				this.CalculatePixelValue();
				return this.unitPixelValue;
			}
		}

		// Token: 0x1700483F RID: 18495
		// (get) Token: 0x0600EEEB RID: 61163 RVA: 0x0036637A File Offset: 0x0036457A
		// (set) Token: 0x0600EEEC RID: 61164 RVA: 0x00366382 File Offset: 0x00364582
		[DefaultValue(typeof(UnitType), "Pixel")]
		public UnitType Type
		{
			get
			{
				return this.unitType;
			}
			set
			{
				this.unitType = value;
			}
		}

		// Token: 0x17004840 RID: 18496
		// (get) Token: 0x0600EEED RID: 61165 RVA: 0x0036638B File Offset: 0x0036458B
		// (set) Token: 0x0600EEEE RID: 61166 RVA: 0x00366393 File Offset: 0x00364593
		[DefaultValue(0)]
		[NotifyParentProperty(true)]
		public float Value
		{
			get
			{
				return this.unitValue;
			}
			set
			{
				this.unitValue = value;
			}
		}

		// Token: 0x040044E5 RID: 17637
		private float unitValue;

		// Token: 0x040044E6 RID: 17638
		private float unitPixelValue;

		// Token: 0x040044E7 RID: 17639
		private float unitParentPixelValue;

		// Token: 0x040044E8 RID: 17640
		private UnitType unitType;

		// Token: 0x040044E9 RID: 17641
		public static readonly Unit Empty = new Unit();
	}
}
