using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003FB RID: 1019
	[TypeConverter(typeof(FontUnitConverter))]
	[Serializable]
	public struct FontUnit
	{
		// Token: 0x06003112 RID: 12562 RVA: 0x0009FD2B File Offset: 0x0009DF2B
		public FontUnit(FontSize type)
		{
			if (type < FontSize.NotSet || type > FontSize.XXLarge)
			{
				throw new ArgumentOutOfRangeException("type");
			}
			this.type = type;
			if (this.type == FontSize.AsUnit)
			{
				this.value = Unit.Point(10);
				return;
			}
			this.value = Unit.Empty;
		}

		// Token: 0x06003113 RID: 12563 RVA: 0x0009FD6A File Offset: 0x0009DF6A
		public FontUnit(Unit value)
		{
			this.type = FontSize.NotSet;
			if (!value.IsEmpty)
			{
				this.type = FontSize.AsUnit;
				this.value = value;
				return;
			}
			this.value = Unit.Empty;
		}

		// Token: 0x06003114 RID: 12564 RVA: 0x0009FD96 File Offset: 0x0009DF96
		public FontUnit(int value)
		{
			this.type = FontSize.AsUnit;
			this.value = Unit.Point(value);
		}

		// Token: 0x06003115 RID: 12565 RVA: 0x0009FDAB File Offset: 0x0009DFAB
		public FontUnit(double value)
		{
			this = new FontUnit(new Unit(value, UnitType.Point));
		}

		// Token: 0x06003116 RID: 12566 RVA: 0x0009FDBA File Offset: 0x0009DFBA
		public FontUnit(double value, UnitType type)
		{
			this = new FontUnit(new Unit(value, type));
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x0009FDC9 File Offset: 0x0009DFC9
		public FontUnit(string value)
		{
			this = new FontUnit(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06003118 RID: 12568 RVA: 0x0009FDD8 File Offset: 0x0009DFD8
		public FontUnit(string value, CultureInfo culture)
		{
			this.type = FontSize.NotSet;
			this.value = Unit.Empty;
			if (!string.IsNullOrEmpty(value))
			{
				char c = char.ToLower(value[0], CultureInfo.InvariantCulture);
				if (c == 'x')
				{
					if (string.Equals(value, "xx-small", StringComparison.OrdinalIgnoreCase) || string.Equals(value, "xxsmall", StringComparison.OrdinalIgnoreCase))
					{
						this.type = FontSize.XXSmall;
						return;
					}
					if (string.Equals(value, "x-small", StringComparison.OrdinalIgnoreCase) || string.Equals(value, "xsmall", StringComparison.OrdinalIgnoreCase))
					{
						this.type = FontSize.XSmall;
						return;
					}
					if (string.Equals(value, "x-large", StringComparison.OrdinalIgnoreCase) || string.Equals(value, "xlarge", StringComparison.OrdinalIgnoreCase))
					{
						this.type = FontSize.XLarge;
						return;
					}
					if (string.Equals(value, "xx-large", StringComparison.OrdinalIgnoreCase) || string.Equals(value, "xxlarge", StringComparison.OrdinalIgnoreCase))
					{
						this.type = FontSize.XXLarge;
						return;
					}
				}
				else if (c == 's')
				{
					if (string.Equals(value, "small", StringComparison.OrdinalIgnoreCase))
					{
						this.type = FontSize.Small;
						return;
					}
					if (string.Equals(value, "smaller", StringComparison.OrdinalIgnoreCase))
					{
						this.type = FontSize.Smaller;
						return;
					}
				}
				else if (c == 'l')
				{
					if (string.Equals(value, "large", StringComparison.OrdinalIgnoreCase))
					{
						this.type = FontSize.Large;
						return;
					}
					if (string.Equals(value, "larger", StringComparison.OrdinalIgnoreCase))
					{
						this.type = FontSize.Larger;
						return;
					}
				}
				else if (c == 'm' && string.Equals(value, "medium", StringComparison.OrdinalIgnoreCase))
				{
					this.type = FontSize.Medium;
					return;
				}
				this.value = new Unit(value, culture, UnitType.Point);
				this.type = FontSize.AsUnit;
			}
		}

		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x06003119 RID: 12569 RVA: 0x0009FF43 File Offset: 0x0009E143
		public bool IsEmpty
		{
			get
			{
				return this.type == FontSize.NotSet;
			}
		}

		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x0600311A RID: 12570 RVA: 0x0009FF4E File Offset: 0x0009E14E
		public FontSize Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x0600311B RID: 12571 RVA: 0x0009FF56 File Offset: 0x0009E156
		public Unit Unit
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x0600311C RID: 12572 RVA: 0x0009FF60 File Offset: 0x0009E160
		public override int GetHashCode()
		{
			return HashCodeCombiner.CombineHashCodes(this.type.GetHashCode(), this.value.GetHashCode());
		}

		// Token: 0x0600311D RID: 12573 RVA: 0x0009FF9C File Offset: 0x0009E19C
		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is FontUnit))
			{
				return false;
			}
			FontUnit fontUnit = (FontUnit)obj;
			return fontUnit.type == this.type && fontUnit.value == this.value;
		}

		// Token: 0x0600311E RID: 12574 RVA: 0x0009FFE1 File Offset: 0x0009E1E1
		public static bool operator ==(FontUnit left, FontUnit right)
		{
			return left.type == right.type && left.value == right.value;
		}

		// Token: 0x0600311F RID: 12575 RVA: 0x000A0004 File Offset: 0x0009E204
		public static bool operator !=(FontUnit left, FontUnit right)
		{
			return left.type != right.type || left.value != right.value;
		}

		// Token: 0x06003120 RID: 12576 RVA: 0x000A0027 File Offset: 0x0009E227
		public static FontUnit Parse(string s)
		{
			return new FontUnit(s, CultureInfo.InvariantCulture);
		}

		// Token: 0x06003121 RID: 12577 RVA: 0x000A0034 File Offset: 0x0009E234
		public static FontUnit Parse(string s, CultureInfo culture)
		{
			return new FontUnit(s, culture);
		}

		// Token: 0x06003122 RID: 12578 RVA: 0x000A003D File Offset: 0x0009E23D
		public static FontUnit Point(int n)
		{
			return new FontUnit(n);
		}

		// Token: 0x06003123 RID: 12579 RVA: 0x000A0045 File Offset: 0x0009E245
		public override string ToString()
		{
			return this.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06003124 RID: 12580 RVA: 0x000A0052 File Offset: 0x0009E252
		public string ToString(CultureInfo culture)
		{
			return this.ToString(culture);
		}

		// Token: 0x06003125 RID: 12581 RVA: 0x000A005C File Offset: 0x0009E25C
		public string ToString(IFormatProvider formatProvider)
		{
			string empty = string.Empty;
			if (this.IsEmpty)
			{
				return empty;
			}
			FontSize fontSize = this.type;
			switch (fontSize)
			{
			case FontSize.AsUnit:
				return this.value.ToString(formatProvider);
			case FontSize.Smaller:
			case FontSize.Larger:
				break;
			case FontSize.XXSmall:
				return "XX-Small";
			case FontSize.XSmall:
				return "X-Small";
			default:
				if (fontSize == FontSize.XLarge)
				{
					return "X-Large";
				}
				if (fontSize == FontSize.XXLarge)
				{
					return "XX-Large";
				}
				break;
			}
			return PropertyConverter.EnumToString(typeof(FontSize), this.type);
		}

		// Token: 0x06003126 RID: 12582 RVA: 0x000A00F6 File Offset: 0x0009E2F6
		public static implicit operator FontUnit(int n)
		{
			return FontUnit.Point(n);
		}

		// Token: 0x040020B0 RID: 8368
		public static readonly FontUnit Empty = default(FontUnit);

		// Token: 0x040020B1 RID: 8369
		public static readonly FontUnit Smaller = new FontUnit(FontSize.Smaller);

		// Token: 0x040020B2 RID: 8370
		public static readonly FontUnit Larger = new FontUnit(FontSize.Larger);

		// Token: 0x040020B3 RID: 8371
		public static readonly FontUnit XXSmall = new FontUnit(FontSize.XXSmall);

		// Token: 0x040020B4 RID: 8372
		public static readonly FontUnit XSmall = new FontUnit(FontSize.XSmall);

		// Token: 0x040020B5 RID: 8373
		public static readonly FontUnit Small = new FontUnit(FontSize.Small);

		// Token: 0x040020B6 RID: 8374
		public static readonly FontUnit Medium = new FontUnit(FontSize.Medium);

		// Token: 0x040020B7 RID: 8375
		public static readonly FontUnit Large = new FontUnit(FontSize.Large);

		// Token: 0x040020B8 RID: 8376
		public static readonly FontUnit XLarge = new FontUnit(FontSize.XLarge);

		// Token: 0x040020B9 RID: 8377
		public static readonly FontUnit XXLarge = new FontUnit(FontSize.XXLarge);

		// Token: 0x040020BA RID: 8378
		private readonly FontSize type;

		// Token: 0x040020BB RID: 8379
		private readonly Unit value;
	}
}
