using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000506 RID: 1286
	[TypeConverter(typeof(UnitConverter))]
	[Serializable]
	public struct Unit
	{
		// Token: 0x060040DE RID: 16606 RVA: 0x000D4289 File Offset: 0x000D2489
		public Unit(int value)
		{
			if (value < -32768 || value > 32767)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			this.value = (double)value;
			this.type = UnitType.Pixel;
		}

		// Token: 0x060040DF RID: 16607 RVA: 0x000D42B5 File Offset: 0x000D24B5
		public Unit(double value)
		{
			if (value < -32768.0 || value > 32767.0)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			this.value = (double)((int)value);
			this.type = UnitType.Pixel;
		}

		// Token: 0x060040E0 RID: 16608 RVA: 0x000D42EC File Offset: 0x000D24EC
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

		// Token: 0x060040E1 RID: 16609 RVA: 0x000D4339 File Offset: 0x000D2539
		public Unit(string value)
		{
			this = new Unit(value, CultureInfo.CurrentCulture, UnitType.Pixel);
		}

		// Token: 0x060040E2 RID: 16610 RVA: 0x000D4348 File Offset: 0x000D2548
		public Unit(string value, CultureInfo culture)
		{
			this = new Unit(value, culture, UnitType.Pixel);
		}

		// Token: 0x060040E3 RID: 16611 RVA: 0x000D4354 File Offset: 0x000D2554
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

		// Token: 0x17001301 RID: 4865
		// (get) Token: 0x060040E4 RID: 16612 RVA: 0x000D44DC File Offset: 0x000D26DC
		public bool IsEmpty
		{
			get
			{
				return this.type == (UnitType)0;
			}
		}

		// Token: 0x17001302 RID: 4866
		// (get) Token: 0x060040E5 RID: 16613 RVA: 0x000D44E7 File Offset: 0x000D26E7
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

		// Token: 0x17001303 RID: 4867
		// (get) Token: 0x060040E6 RID: 16614 RVA: 0x000D44F9 File Offset: 0x000D26F9
		public double Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x060040E7 RID: 16615 RVA: 0x000D4504 File Offset: 0x000D2704
		public override int GetHashCode()
		{
			return HashCodeCombiner.CombineHashCodes(this.type.GetHashCode(), this.value.GetHashCode());
		}

		// Token: 0x060040E8 RID: 16616 RVA: 0x000D4538 File Offset: 0x000D2738
		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is Unit))
			{
				return false;
			}
			Unit unit = (Unit)obj;
			return unit.type == this.type && unit.value == this.value;
		}

		// Token: 0x060040E9 RID: 16617 RVA: 0x000D4578 File Offset: 0x000D2778
		public static bool operator ==(Unit left, Unit right)
		{
			return left.type == right.type && left.value == right.value;
		}

		// Token: 0x060040EA RID: 16618 RVA: 0x000D4598 File Offset: 0x000D2798
		public static bool operator !=(Unit left, Unit right)
		{
			return left.type != right.type || left.value != right.value;
		}

		// Token: 0x060040EB RID: 16619 RVA: 0x000D45BC File Offset: 0x000D27BC
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

		// Token: 0x060040EC RID: 16620 RVA: 0x000D4634 File Offset: 0x000D2834
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

		// Token: 0x060040ED RID: 16621 RVA: 0x000D46E0 File Offset: 0x000D28E0
		public static Unit Parse(string s)
		{
			return new Unit(s, CultureInfo.CurrentCulture);
		}

		// Token: 0x060040EE RID: 16622 RVA: 0x000D46ED File Offset: 0x000D28ED
		public static Unit Parse(string s, CultureInfo culture)
		{
			return new Unit(s, culture);
		}

		// Token: 0x060040EF RID: 16623 RVA: 0x000D46F6 File Offset: 0x000D28F6
		public static Unit Percentage(double n)
		{
			return new Unit(n, UnitType.Percentage);
		}

		// Token: 0x060040F0 RID: 16624 RVA: 0x000D46FF File Offset: 0x000D28FF
		public static Unit Pixel(int n)
		{
			return new Unit(n);
		}

		// Token: 0x060040F1 RID: 16625 RVA: 0x000D4707 File Offset: 0x000D2907
		public static Unit Point(int n)
		{
			return new Unit((double)n, UnitType.Point);
		}

		// Token: 0x060040F2 RID: 16626 RVA: 0x000D4711 File Offset: 0x000D2911
		public override string ToString()
		{
			return this.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x060040F3 RID: 16627 RVA: 0x000D471E File Offset: 0x000D291E
		public string ToString(CultureInfo culture)
		{
			return this.ToString(culture);
		}

		// Token: 0x060040F4 RID: 16628 RVA: 0x000D4728 File Offset: 0x000D2928
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

		// Token: 0x060040F5 RID: 16629 RVA: 0x000D4781 File Offset: 0x000D2981
		public static implicit operator Unit(int n)
		{
			return Unit.Pixel(n);
		}

		// Token: 0x040024D8 RID: 9432
		public static readonly Unit Empty;

		// Token: 0x040024D9 RID: 9433
		internal const int MaxValue = 32767;

		// Token: 0x040024DA RID: 9434
		internal const int MinValue = -32768;

		// Token: 0x040024DB RID: 9435
		private readonly UnitType type;

		// Token: 0x040024DC RID: 9436
		private readonly double value;
	}
}
