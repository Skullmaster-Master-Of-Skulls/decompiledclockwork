using System;
using System.util;
using iTextSharp.text.pdf;

namespace iTextSharp.text
{
	// Token: 0x02000174 RID: 372
	public class Font : IComparable<Font>
	{
		// Token: 0x06000E5E RID: 3678 RVA: 0x00053114 File Offset: 0x00052114
		public Font(Font other)
		{
			this.color = other.color;
			this.family = other.family;
			this.size = other.size;
			this.style = other.style;
			this.baseFont = other.baseFont;
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x0005317C File Offset: 0x0005217C
		public Font(Font.FontFamily family, float size, int style, BaseColor color)
		{
			this.family = family;
			this.size = size;
			this.style = style;
			this.color = color;
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x000531BA File Offset: 0x000521BA
		public Font(BaseFont bf, float size, int style, BaseColor color)
		{
			this.baseFont = bf;
			this.size = size;
			this.style = style;
			this.color = color;
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x000531F8 File Offset: 0x000521F8
		public Font(BaseFont bf, float size, int style) : this(bf, size, style, null)
		{
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x00053204 File Offset: 0x00052204
		public Font(BaseFont bf, float size) : this(bf, size, -1, null)
		{
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x00053210 File Offset: 0x00052210
		public Font(BaseFont bf) : this(bf, -1f, -1, null)
		{
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x00053220 File Offset: 0x00052220
		public Font(Font.FontFamily family, float size, int style) : this(family, size, style, null)
		{
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x0005322C File Offset: 0x0005222C
		public Font(Font.FontFamily family, float size) : this(family, size, -1, null)
		{
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x00053238 File Offset: 0x00052238
		public Font(Font.FontFamily family) : this(family, -1f, -1, null)
		{
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x00053248 File Offset: 0x00052248
		public Font() : this(Font.FontFamily.UNDEFINED, -1f, -1, null)
		{
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x00053258 File Offset: 0x00052258
		public virtual int CompareTo(Font font)
		{
			if (font == null)
			{
				return -1;
			}
			int result;
			try
			{
				if (this.baseFont != null && !this.baseFont.Equals(font.BaseFont))
				{
					result = -2;
				}
				else if (this.family != font.Family)
				{
					result = 1;
				}
				else if (this.size != font.Size)
				{
					result = 2;
				}
				else if (this.style != font.Style)
				{
					result = 3;
				}
				else if (this.color == null)
				{
					if (font.Color == null)
					{
						result = 0;
					}
					else
					{
						result = 4;
					}
				}
				else if (font.Color == null)
				{
					result = 4;
				}
				else if (this.color.Equals(font.Color))
				{
					result = 0;
				}
				else
				{
					result = 4;
				}
			}
			catch
			{
				result = -3;
			}
			return result;
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x00053318 File Offset: 0x00052318
		public Font.FontFamily Family
		{
			get
			{
				return this.family;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x00053320 File Offset: 0x00052320
		public virtual string Familyname
		{
			get
			{
				string result = "unknown";
				switch (this.Family)
				{
				case Font.FontFamily.COURIER:
					return "Courier";
				case Font.FontFamily.HELVETICA:
					return "Helvetica";
				case Font.FontFamily.TIMES_ROMAN:
					return "Times-Roman";
				case Font.FontFamily.SYMBOL:
					return "Symbol";
				case Font.FontFamily.ZAPFDINGBATS:
					return "ZapfDingbats";
				default:
					if (this.baseFont != null)
					{
						string[][] familyFontName = this.baseFont.FamilyFontName;
						foreach (string[] array2 in familyFontName)
						{
							if ("0".Equals(array2[2]))
							{
								return array2[3];
							}
							if ("1033".Equals(array2[2]))
							{
								result = array2[3];
							}
							if ("".Equals(array2[2]))
							{
								result = array2[3];
							}
						}
					}
					return result;
				}
			}
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x000533E4 File Offset: 0x000523E4
		public virtual void SetFamily(string family)
		{
			this.family = Font.GetFamilyIndex(family);
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x000533F4 File Offset: 0x000523F4
		public static Font.FontFamily GetFamilyIndex(string family)
		{
			if (Util.EqualsIgnoreCase(family, "Courier"))
			{
				return Font.FontFamily.COURIER;
			}
			if (Util.EqualsIgnoreCase(family, "Helvetica"))
			{
				return Font.FontFamily.HELVETICA;
			}
			if (Util.EqualsIgnoreCase(family, "Times-Roman"))
			{
				return Font.FontFamily.TIMES_ROMAN;
			}
			if (Util.EqualsIgnoreCase(family, "Symbol"))
			{
				return Font.FontFamily.SYMBOL;
			}
			if (Util.EqualsIgnoreCase(family, "ZapfDingbats"))
			{
				return Font.FontFamily.ZAPFDINGBATS;
			}
			return Font.FontFamily.UNDEFINED;
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000E6D RID: 3693 RVA: 0x0005344D File Offset: 0x0005244D
		// (set) Token: 0x06000E6E RID: 3694 RVA: 0x00053455 File Offset: 0x00052455
		public virtual float Size
		{
			get
			{
				return this.size;
			}
			set
			{
				this.size = value;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000E6F RID: 3695 RVA: 0x00053460 File Offset: 0x00052460
		public float CalculatedSize
		{
			get
			{
				float num = this.size;
				if (num == -1f)
				{
					num = 12f;
				}
				return num;
			}
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x00053483 File Offset: 0x00052483
		public float GetCalculatedLeading(float linespacing)
		{
			return linespacing * this.CalculatedSize;
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x0005348D File Offset: 0x0005248D
		public int Style
		{
			get
			{
				return this.style;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000E72 RID: 3698 RVA: 0x00053498 File Offset: 0x00052498
		public int CalculatedStyle
		{
			get
			{
				int num = this.style;
				if (num == -1)
				{
					num = 0;
				}
				if (this.baseFont != null)
				{
					return num;
				}
				if (this.family == Font.FontFamily.SYMBOL || this.family == Font.FontFamily.ZAPFDINGBATS)
				{
					return num;
				}
				return num & -4;
			}
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x000534D4 File Offset: 0x000524D4
		public bool IsBold()
		{
			return this.style != -1 && (this.style & 1) == 1;
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x000534EC File Offset: 0x000524EC
		public bool IsItalic()
		{
			return this.style != -1 && (this.style & 2) == 2;
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x00053504 File Offset: 0x00052504
		public bool IsUnderlined()
		{
			return this.style != -1 && (this.style & 4) == 4;
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0005351C File Offset: 0x0005251C
		public bool IsStrikethru()
		{
			return this.style != -1 && (this.style & 8) == 8;
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x00053534 File Offset: 0x00052534
		public virtual void SetStyle(string style)
		{
			if (this.style == -1)
			{
				this.style = 0;
			}
			this.style |= Font.GetStyleValue(style);
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x00053559 File Offset: 0x00052559
		public virtual void SetStyle(int style)
		{
			this.style = style;
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x00053564 File Offset: 0x00052564
		public static int GetStyleValue(string style)
		{
			int num = 0;
			if (style.IndexOf("normal") != -1)
			{
				num = num;
			}
			if (style.IndexOf("bold") != -1)
			{
				num |= 1;
			}
			if (style.IndexOf("italic") != -1)
			{
				num |= 2;
			}
			if (style.IndexOf("oblique") != -1)
			{
				num |= 2;
			}
			if (style.IndexOf("underline") != -1)
			{
				num |= 4;
			}
			if (style.IndexOf("line-through") != -1)
			{
				num |= 8;
			}
			return num;
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000E7A RID: 3706 RVA: 0x000535DE File Offset: 0x000525DE
		// (set) Token: 0x06000E7B RID: 3707 RVA: 0x000535E6 File Offset: 0x000525E6
		public virtual BaseColor Color
		{
			get
			{
				return this.color;
			}
			set
			{
				this.color = value;
			}
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x000535EF File Offset: 0x000525EF
		public virtual void SetColor(int red, int green, int blue)
		{
			this.color = new BaseColor(red, green, blue);
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000E7D RID: 3709 RVA: 0x000535FF File Offset: 0x000525FF
		public BaseFont BaseFont
		{
			get
			{
				return this.baseFont;
			}
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x00053608 File Offset: 0x00052608
		public BaseFont GetCalculatedBaseFont(bool specialEncoding)
		{
			if (this.baseFont != null)
			{
				return this.baseFont;
			}
			int num = this.style;
			if (num == -1)
			{
				num = 0;
			}
			string encoding = "Cp1252";
			string name;
			switch (this.family)
			{
			case Font.FontFamily.COURIER:
				switch (num & 3)
				{
				case 1:
					name = "Courier-Bold";
					goto IL_132;
				case 2:
					name = "Courier-Oblique";
					goto IL_132;
				case 3:
					name = "Courier-BoldOblique";
					goto IL_132;
				default:
					name = "Courier";
					goto IL_132;
				}
				break;
			case Font.FontFamily.TIMES_ROMAN:
				switch (num & 3)
				{
				case 1:
					name = "Times-Bold";
					goto IL_132;
				case 2:
					name = "Times-Italic";
					goto IL_132;
				case 3:
					name = "Times-BoldItalic";
					goto IL_132;
				default:
					name = "Times-Roman";
					goto IL_132;
				}
				break;
			case Font.FontFamily.SYMBOL:
				name = "Symbol";
				if (specialEncoding)
				{
					encoding = "Symbol";
					goto IL_132;
				}
				goto IL_132;
			case Font.FontFamily.ZAPFDINGBATS:
				name = "ZapfDingbats";
				if (specialEncoding)
				{
					encoding = "ZapfDingbats";
					goto IL_132;
				}
				goto IL_132;
			}
			switch (num & 3)
			{
			case 1:
				name = "Helvetica-Bold";
				break;
			case 2:
				name = "Helvetica-Oblique";
				break;
			case 3:
				name = "Helvetica-BoldOblique";
				break;
			default:
				name = "Helvetica";
				break;
			}
			IL_132:
			return BaseFont.CreateFont(name, encoding, false);
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x00053751 File Offset: 0x00052751
		public virtual bool IsStandardFont()
		{
			return this.family == Font.FontFamily.UNDEFINED && this.size == -1f && this.style == -1 && this.color == null && this.baseFont == null;
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x00053788 File Offset: 0x00052788
		public virtual Font Difference(Font font)
		{
			if (font == null)
			{
				return this;
			}
			float num = font.size;
			if (num == -1f)
			{
				num = this.size;
			}
			int num2 = -1;
			int num3 = this.Style;
			int num4 = font.Style;
			if (num3 != -1 || num4 != -1)
			{
				if (num3 == -1)
				{
					num3 = 0;
				}
				if (num4 == -1)
				{
					num4 = 0;
				}
				num2 = (num3 | num4);
			}
			BaseColor baseColor = font.Color;
			if (baseColor == null)
			{
				baseColor = this.Color;
			}
			if (font.baseFont != null)
			{
				return new Font(font.BaseFont, num, num2, baseColor);
			}
			if (font.Family != Font.FontFamily.UNDEFINED)
			{
				return new Font(font.Family, num, num2, baseColor);
			}
			if (this.baseFont == null)
			{
				return new Font(this.Family, num, num2, baseColor);
			}
			if (num2 == num3)
			{
				return new Font(this.BaseFont, num, num2, baseColor);
			}
			return FontFactory.GetFont(this.Familyname, num, num2, baseColor);
		}

		// Token: 0x04000A75 RID: 2677
		public const int NORMAL = 0;

		// Token: 0x04000A76 RID: 2678
		public const int BOLD = 1;

		// Token: 0x04000A77 RID: 2679
		public const int ITALIC = 2;

		// Token: 0x04000A78 RID: 2680
		public const int UNDERLINE = 4;

		// Token: 0x04000A79 RID: 2681
		public const int STRIKETHRU = 8;

		// Token: 0x04000A7A RID: 2682
		public const int BOLDITALIC = 3;

		// Token: 0x04000A7B RID: 2683
		public const int UNDEFINED = -1;

		// Token: 0x04000A7C RID: 2684
		public const int DEFAULTSIZE = 12;

		// Token: 0x04000A7D RID: 2685
		private Font.FontFamily family = Font.FontFamily.UNDEFINED;

		// Token: 0x04000A7E RID: 2686
		private float size = -1f;

		// Token: 0x04000A7F RID: 2687
		private int style = -1;

		// Token: 0x04000A80 RID: 2688
		private BaseColor color;

		// Token: 0x04000A81 RID: 2689
		private BaseFont baseFont;

		// Token: 0x02000175 RID: 373
		public enum FontFamily
		{
			// Token: 0x04000A83 RID: 2691
			COURIER,
			// Token: 0x04000A84 RID: 2692
			HELVETICA,
			// Token: 0x04000A85 RID: 2693
			TIMES_ROMAN,
			// Token: 0x04000A86 RID: 2694
			SYMBOL,
			// Token: 0x04000A87 RID: 2695
			ZAPFDINGBATS,
			// Token: 0x04000A88 RID: 2696
			UNDEFINED = -1
		}
	}
}
