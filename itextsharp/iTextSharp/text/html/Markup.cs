using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.util;

namespace iTextSharp.text.html
{
	// Token: 0x020004F4 RID: 1268
	public class Markup
	{
		// Token: 0x06002B50 RID: 11088 RVA: 0x0010609C File Offset: 0x0010509C
		static Markup()
		{
			Markup.sizes["xx-small"] = 4f;
			Markup.sizes["x-small"] = 6f;
			Markup.sizes["small"] = 8f;
			Markup.sizes["medium"] = 10f;
			Markup.sizes["large"] = 13f;
			Markup.sizes["x-large"] = 18f;
			Markup.sizes["xx-large"] = 26f;
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x0010613F File Offset: 0x0010513F
		public static float ParseLength(string str)
		{
			return Markup.ParseLength(str, 12f);
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x0010614C File Offset: 0x0010514C
		public static float ParseLength(string str, float actualFontSize)
		{
			if (str == null)
			{
				return 0f;
			}
			float result;
			if (Markup.sizes.TryGetValue(str.ToLowerInvariant(), out result))
			{
				return result;
			}
			int num = 0;
			int length = str.Length;
			for (bool flag = true; flag && num < length; flag = false)
			{
				switch (str[num])
				{
				case '+':
				case '-':
				case '.':
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
					num++;
					continue;
				}
			}
			if (num == 0)
			{
				return 0f;
			}
			if (num == length)
			{
				return float.Parse(str, NumberFormatInfo.InvariantInfo);
			}
			float num2 = float.Parse(str.Substring(0, num), NumberFormatInfo.InvariantInfo);
			str = str.Substring(num);
			if (str.StartsWith("in"))
			{
				return num2 * 72f;
			}
			if (str.StartsWith("cm"))
			{
				return num2 / 2.54f * 72f;
			}
			if (str.StartsWith("mm"))
			{
				return num2 / 25.4f * 72f;
			}
			if (str.StartsWith("pc"))
			{
				return num2 * 12f;
			}
			if (str.StartsWith("em"))
			{
				return num2 * actualFontSize;
			}
			if (str.StartsWith("ex"))
			{
				return num2 * actualFontSize / 2f;
			}
			return num2;
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x001062AC File Offset: 0x001052AC
		public static BaseColor DecodeColor(string s)
		{
			if (s == null)
			{
				return null;
			}
			s = s.ToLower(CultureInfo.InvariantCulture).Trim();
			try
			{
				return WebColors.GetRGBColor(s);
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x06002B54 RID: 11092 RVA: 0x001062F0 File Offset: 0x001052F0
		public static Properties ParseAttributes(string str)
		{
			Properties properties = new Properties();
			if (str == null)
			{
				return properties;
			}
			StringTokenizer stringTokenizer = new StringTokenizer(str, ";");
			while (stringTokenizer.HasMoreTokens())
			{
				StringTokenizer stringTokenizer2 = new StringTokenizer(stringTokenizer.NextToken(), ":");
				if (stringTokenizer2.HasMoreTokens())
				{
					string text = stringTokenizer2.NextToken().Trim().Trim();
					if (stringTokenizer2.HasMoreTokens())
					{
						string text2 = stringTokenizer2.NextToken().Trim();
						if (text2.StartsWith("\""))
						{
							text2 = text2.Substring(1);
						}
						if (text2.EndsWith("\""))
						{
							text2 = text2.Substring(0, text2.Length - 1);
						}
						properties.Add(text.ToLower(CultureInfo.InvariantCulture), text2);
					}
				}
			}
			return properties;
		}

		// Token: 0x06002B55 RID: 11093 RVA: 0x001063B0 File Offset: 0x001053B0
		public static string RemoveComment(string str, string startComment, string endComment)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			int length = endComment.Length;
			for (int i = str.IndexOf(startComment, num); i > -1; i = str.IndexOf(startComment, num))
			{
				stringBuilder.Append(str.Substring(num, i - num));
				num = str.IndexOf(endComment, i) + length;
			}
			stringBuilder.Append(str.Substring(num));
			return stringBuilder.ToString();
		}

		// Token: 0x04001DE0 RID: 7648
		public const string ITEXT_TAG = "tag";

		// Token: 0x04001DE1 RID: 7649
		public const string HTML_TAG_BODY = "body";

		// Token: 0x04001DE2 RID: 7650
		public const string HTML_TAG_DIV = "div";

		// Token: 0x04001DE3 RID: 7651
		public const string HTML_TAG_LINK = "link";

		// Token: 0x04001DE4 RID: 7652
		public const string HTML_TAG_SPAN = "span";

		// Token: 0x04001DE5 RID: 7653
		public const string HTML_ATTR_HEIGHT = "height";

		// Token: 0x04001DE6 RID: 7654
		public const string HTML_ATTR_HREF = "href";

		// Token: 0x04001DE7 RID: 7655
		public const string HTML_ATTR_REL = "rel";

		// Token: 0x04001DE8 RID: 7656
		public const string HTML_ATTR_STYLE = "style";

		// Token: 0x04001DE9 RID: 7657
		public const string HTML_ATTR_TYPE = "type";

		// Token: 0x04001DEA RID: 7658
		public const string HTML_ATTR_STYLESHEET = "stylesheet";

		// Token: 0x04001DEB RID: 7659
		public const string HTML_ATTR_WIDTH = "width";

		// Token: 0x04001DEC RID: 7660
		public const string HTML_ATTR_CSS_CLASS = "class";

		// Token: 0x04001DED RID: 7661
		public const string HTML_ATTR_CSS_ID = "id";

		// Token: 0x04001DEE RID: 7662
		public const string HTML_VALUE_JAVASCRIPT = "text/javascript";

		// Token: 0x04001DEF RID: 7663
		public const string HTML_VALUE_CSS = "text/css";

		// Token: 0x04001DF0 RID: 7664
		public const string CSS_KEY_BGCOLOR = "background-color";

		// Token: 0x04001DF1 RID: 7665
		public const string CSS_KEY_COLOR = "color";

		// Token: 0x04001DF2 RID: 7666
		public const string CSS_KEY_DISPLAY = "display";

		// Token: 0x04001DF3 RID: 7667
		public const string CSS_KEY_FONTFAMILY = "font-family";

		// Token: 0x04001DF4 RID: 7668
		public const string CSS_KEY_FONTSIZE = "font-size";

		// Token: 0x04001DF5 RID: 7669
		public const string CSS_KEY_FONTSTYLE = "font-style";

		// Token: 0x04001DF6 RID: 7670
		public const string CSS_KEY_FONTWEIGHT = "font-weight";

		// Token: 0x04001DF7 RID: 7671
		public const string CSS_KEY_LINEHEIGHT = "line-height";

		// Token: 0x04001DF8 RID: 7672
		public const string CSS_KEY_MARGIN = "margin";

		// Token: 0x04001DF9 RID: 7673
		public const string CSS_KEY_MARGINLEFT = "margin-left";

		// Token: 0x04001DFA RID: 7674
		public const string CSS_KEY_MARGINRIGHT = "margin-right";

		// Token: 0x04001DFB RID: 7675
		public const string CSS_KEY_MARGINTOP = "margin-top";

		// Token: 0x04001DFC RID: 7676
		public const string CSS_KEY_MARGINBOTTOM = "margin-bottom";

		// Token: 0x04001DFD RID: 7677
		public const string CSS_KEY_PADDING = "padding";

		// Token: 0x04001DFE RID: 7678
		public const string CSS_KEY_PADDINGLEFT = "padding-left";

		// Token: 0x04001DFF RID: 7679
		public const string CSS_KEY_PADDINGRIGHT = "padding-right";

		// Token: 0x04001E00 RID: 7680
		public const string CSS_KEY_PADDINGTOP = "padding-top";

		// Token: 0x04001E01 RID: 7681
		public const string CSS_KEY_PADDINGBOTTOM = "padding-bottom";

		// Token: 0x04001E02 RID: 7682
		public const string CSS_KEY_BORDERCOLOR = "border-color";

		// Token: 0x04001E03 RID: 7683
		public const string CSS_KEY_BORDERWIDTH = "border-width";

		// Token: 0x04001E04 RID: 7684
		public const string CSS_KEY_BORDERWIDTHLEFT = "border-left-width";

		// Token: 0x04001E05 RID: 7685
		public const string CSS_KEY_BORDERWIDTHRIGHT = "border-right-width";

		// Token: 0x04001E06 RID: 7686
		public const string CSS_KEY_BORDERWIDTHTOP = "border-top-width";

		// Token: 0x04001E07 RID: 7687
		public const string CSS_KEY_BORDERWIDTHBOTTOM = "border-bottom-width";

		// Token: 0x04001E08 RID: 7688
		public const string CSS_KEY_PAGE_BREAK_AFTER = "page-break-after";

		// Token: 0x04001E09 RID: 7689
		public const string CSS_KEY_PAGE_BREAK_BEFORE = "page-break-before";

		// Token: 0x04001E0A RID: 7690
		public const string CSS_KEY_TEXTALIGN = "text-align";

		// Token: 0x04001E0B RID: 7691
		public const string CSS_KEY_TEXTDECORATION = "text-decoration";

		// Token: 0x04001E0C RID: 7692
		public const string CSS_KEY_VERTICALALIGN = "vertical-align";

		// Token: 0x04001E0D RID: 7693
		public const string CSS_KEY_VISIBILITY = "visibility";

		// Token: 0x04001E0E RID: 7694
		public const string CSS_VALUE_ALWAYS = "always";

		// Token: 0x04001E0F RID: 7695
		public const string CSS_VALUE_BLOCK = "block";

		// Token: 0x04001E10 RID: 7696
		public const string CSS_VALUE_BOLD = "bold";

		// Token: 0x04001E11 RID: 7697
		public const string CSS_VALUE_HIDDEN = "hidden";

		// Token: 0x04001E12 RID: 7698
		public const string CSS_VALUE_INLINE = "inline";

		// Token: 0x04001E13 RID: 7699
		public const string CSS_VALUE_ITALIC = "italic";

		// Token: 0x04001E14 RID: 7700
		public const string CSS_VALUE_LINETHROUGH = "line-through";

		// Token: 0x04001E15 RID: 7701
		public const string CSS_VALUE_LISTITEM = "list-item";

		// Token: 0x04001E16 RID: 7702
		public const string CSS_VALUE_NONE = "none";

		// Token: 0x04001E17 RID: 7703
		public const string CSS_VALUE_NORMAL = "normal";

		// Token: 0x04001E18 RID: 7704
		public const string CSS_VALUE_OBLIQUE = "oblique";

		// Token: 0x04001E19 RID: 7705
		public const string CSS_VALUE_TABLE = "table";

		// Token: 0x04001E1A RID: 7706
		public const string CSS_VALUE_TABLEROW = "table-row";

		// Token: 0x04001E1B RID: 7707
		public const string CSS_VALUE_TABLECELL = "table-cell";

		// Token: 0x04001E1C RID: 7708
		public const string CSS_VALUE_TEXTALIGNLEFT = "left";

		// Token: 0x04001E1D RID: 7709
		public const string CSS_VALUE_TEXTALIGNRIGHT = "right";

		// Token: 0x04001E1E RID: 7710
		public const string CSS_VALUE_TEXTALIGNCENTER = "center";

		// Token: 0x04001E1F RID: 7711
		public const string CSS_VALUE_TEXTALIGNJUSTIFY = "justify";

		// Token: 0x04001E20 RID: 7712
		public const string CSS_VALUE_UNDERLINE = "underline";

		// Token: 0x04001E21 RID: 7713
		public const float DEFAULT_FONT_SIZE = 12f;

		// Token: 0x04001E22 RID: 7714
		private static Dictionary<string, float> sizes = new Dictionary<string, float>();
	}
}
