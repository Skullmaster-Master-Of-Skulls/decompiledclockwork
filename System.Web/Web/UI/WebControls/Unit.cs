using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000678 RID: 1656
	[TypeConverter(typeof(UnitConverter))]
	[Serializable]
	public struct Unit
	{
		// Token: 0x060051A0 RID: 20896 RVA: 0x0014A200 File Offset: 0x00149200
		public Unit(int value)
		{
			if (value < -32768 || value > 32767)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			this.value = (double)value;
			this.type = UnitType.Pixel;
		}

		// Token: 0x060051A1 RID: 20897 RVA: 0x0014A22C File Offset: 0x0014922C
		public Unit(double value)
		{
			if (value < -32768.0 || value > 32767.0)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			this.value = (double)((int)value);
			this.type = UnitType.Pixel;
		}

		// Token: 0x060051A2 RID: 20898 RVA: 0x0014A264 File Offset: 0x00149264
		public Unit(double value, UnitType type)
		{
			if (value < -32768.0 || value > 32767.0)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			if (type == UnitType.Pixel)
			{
				this.value = (double)((int)value);
			}
			else
			{
				this.value = value;
			}
			this.type = type;
		}

		// Token: 0x060051A3 RID: 20899 RVA: 0x0014A2B1 File Offset: 0x001492B1
		public Unit(string value)
		{
			this = new Unit(value, CultureInfo.CurrentCulture, UnitType.Pixel);
		}

		// Token: 0x060051A4 RID: 20900 RVA: 0x0014A2C0 File Offset: 0x001492C0
		public Unit(string value, CultureInfo culture)
		{
			this = new Unit(value, culture, UnitType.Pixel);
		}

		// Token: 0x060051A5 RID: 20901 RVA: 0x0014A2CC File Offset: 0x001492CC
		internal Unit(string value, CultureInfo culture, UnitType defaultType)
		{
			if (string.IsNullOrEmpty(value))
			{
				this.value = 0.0;
				this.type = (UnitType)0;
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
			if (num == -1)
			{
				throw new FormatException(SR.GetString("UnitParseNoDigits", new object[]
				{
					value
				}));
			}
			if (num < length - 1)
			{
				this.type = Unit.GetTypeFromString(text.Substring(num + 1).Trim());
			}
			else
			{
				this.type = defaultType;
			}
			string text2 = text.Substring(0, num + 1);
			try
			{
				TypeConverter typeConverter = new SingleConverter();
				this.value = (double)((float)typeConverter.ConvertFromString(null, culture, text2));
				if (this.type == UnitType.Pixel)
				{
					this.value = (double)((int)this.value);
				}
			}
			catch
			{
				throw new FormatException(SR.GetString("UnitParseNumericPart", new object[]
				{
					value,
					text2,
					this.type.ToString("G")
				}));
			}
			if (this.value < -32768.0 || this.value > 32767.0)
			{
				throw new ArgumentOutOfRangeException("value");
			}
		}

		// Token: 0x170014CA RID: 5322
		// (get) Token: 0x060051A6 RID: 20902 RVA: 0x0014A45C File Offset: 0x0014945C
		public bool IsEmpty
		{
			get
			{
				return this.type == (UnitType)0;
			}
		}

		// Token: 0x170014CB RID: 5323
		// (get) Token: 0x060051A7 RID: 20903 RVA: 0x0014A467 File Offset: 0x00149467
		public UnitType Type
		{
			get
			{
				if (!this.IsEmpty)
				{
					return this.type;
				}
				return UnitType.Pixel;
			}
		}

		// Token: 0x170014CC RID: 5324
		// (get) Token: 0x060051A8 RID: 20904 RVA: 0x0014A479 File Offset: 0x00149479
		public double Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x060051A9 RID: 20905 RVA: 0x0014A484 File Offset: 0x00149484
		public override int GetHashCode()
		{
			return HashCodeCombiner.CombineHashCodes(this.type.GetHashCode(), this.value.GetHashCode());
		}

		// Token: 0x060051AA RID: 20906 RVA: 0x0014A4B4 File Offset: 0x001494B4
		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is Unit))
			{
				return false;
			}
			Unit unit = (Unit)obj;
			return unit.type == this.type && unit.value == this.value;
		}

		// Token: 0x060051AB RID: 20907 RVA: 0x0014A4F6 File Offset: 0x001494F6
		public static bool operator ==(Unit left, Unit right)
		{
			return left.type == right.type && left.value == right.value;
		}

		// Token: 0x060051AC RID: 20908 RVA: 0x0014A51A File Offset: 0x0014951A
		public static bool operator !=(Unit left, Unit right)
		{
			return left.type != right.type || left.value != right.value;
		}

		// Token: 0x060051AD RID: 20909 RVA: 0x0014A544 File Offset: 0x00149544
		private static string GetStringFromType(UnitType type)
		{
			switch (type)
			{
			case UnitType.Pixel:
				return "px";
			case UnitType.Point:
				return "pt";
			case UnitType.Pica:
				return "pc";
			case UnitType.Inch:
				return "in";
			case UnitType.Mm:
				return "mm";
			case UnitType.Cm:
				return "cm";
			case UnitType.Percentage:
				return "%";
			case UnitType.Em:
				return "em";
			case UnitType.Ex:
				return "ex";
			default:
				return string.Empty;
			}
		}

		// Token: 0x060051AE RID: 20910 RVA: 0x0014A5BC File Offset: 0x001495BC
		private static UnitType GetTypeFromString(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return UnitType.Pixel;
			}
			if (value.Equals("px"))
			{
				return UnitType.Pixel;
			}
			if (value.Equals("pt"))
			{
				return UnitType.Point;
			}
			if (value.Equals("%"))
			{
				return UnitType.Percentage;
			}
			if (value.Equals("pc"))
			{
				return UnitType.Pica;
			}
			if (value.Equals("in"))
			{
				return UnitType.Inch;
			}
			if (value.Equals("mm"))
			{
				return UnitType.Mm;
			}
			if (value.Equals("cm"))
			{
				return UnitType.Cm;
			}
			if (value.Equals("em"))
			{
				return UnitType.Em;
			}
			if (value.Equals("ex"))
			{
				return UnitType.Ex;
			}
			throw new ArgumentOutOfRangeException("value");
		}

		// Token: 0x060051AF RID: 20911 RVA: 0x0014A668 File Offset: 0x00149668
		public static Unit Parse(string s)
		{
			return new Unit(s, CultureInfo.CurrentCulture);
		}

		// Token: 0x060051B0 RID: 20912 RVA: 0x0014A675 File Offset: 0x00149675
		public static Unit Parse(string s, CultureInfo culture)
		{
			return new Unit(s, culture);
		}

		// Token: 0x060051B1 RID: 20913 RVA: 0x0014A67E File Offset: 0x0014967E
		public static Unit Percentage(double n)
		{
			return new Unit(n, UnitType.Percentage);
		}

		// Token: 0x060051B2 RID: 20914 RVA: 0x0014A687 File Offset: 0x00149687
		public static Unit Pixel(int n)
		{
			return new Unit(n);
		}

		// Token: 0x060051B3 RID: 20915 RVA: 0x0014A68F File Offset: 0x0014968F
		public static Unit Point(int n)
		{
			return new Unit((double)n, UnitType.Point);
		}

		// Token: 0x060051B4 RID: 20916 RVA: 0x0014A699 File Offset: 0x00149699
		public override string ToString()
		{
			return this.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x060051B5 RID: 20917 RVA: 0x0014A6A6 File Offset: 0x001496A6
		public string ToString(CultureInfo culture)
		{
			return this.ToString(culture);
		}

		// Token: 0x060051B6 RID: 20918 RVA: 0x0014A6B0 File Offset: 0x001496B0
		public string ToString(IFormatProvider formatProvider)
		{
			if (this.IsEmpty)
			{
				return string.Empty;
			}
			string str;
			if (this.type == UnitType.Pixel)
			{
				str = ((int)this.value).ToString(formatProvider);
			}
			else
			{
				str = ((float)this.value).ToString(formatProvider);
			}
			return str + Unit.GetStringFromType(this.type);
		}

		// Token: 0x060051B7 RID: 20919 RVA: 0x0014A709 File Offset: 0x00149709
		public static implicit operator Unit(int n)
		{
			return Unit.Pixel(n);
		}

		// Token: 0x04002DA5 RID: 11685
		internal const int MaxValue = 32767;

		// Token: 0x04002DA6 RID: 11686
		internal const int MinValue = -32768;

		// Token: 0x04002DA7 RID: 11687
		public static readonly Unit Empty = default(Unit);

		// Token: 0x04002DA8 RID: 11688
		private readonly UnitType type;

		// Token: 0x04002DA9 RID: 11689
		private readonly double value;
	}
}
