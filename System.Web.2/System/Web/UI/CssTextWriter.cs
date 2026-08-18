using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x0200026C RID: 620
	internal sealed class CssTextWriter : TextWriter
	{
		// Token: 0x06001D68 RID: 7528 RVA: 0x0005FA24 File Offset: 0x0005DC24
		static CssTextWriter()
		{
			CssTextWriter.RegisterAttribute("background-color", HtmlTextWriterStyle.BackgroundColor);
			CssTextWriter.RegisterAttribute("background-image", HtmlTextWriterStyle.BackgroundImage, true, true);
			CssTextWriter.RegisterAttribute("border-collapse", HtmlTextWriterStyle.BorderCollapse);
			CssTextWriter.RegisterAttribute("border-color", HtmlTextWriterStyle.BorderColor);
			CssTextWriter.RegisterAttribute("border-style", HtmlTextWriterStyle.BorderStyle);
			CssTextWriter.RegisterAttribute("border-width", HtmlTextWriterStyle.BorderWidth);
			CssTextWriter.RegisterAttribute("color", HtmlTextWriterStyle.Color);
			CssTextWriter.RegisterAttribute("cursor", HtmlTextWriterStyle.Cursor);
			CssTextWriter.RegisterAttribute("direction", HtmlTextWriterStyle.Direction);
			CssTextWriter.RegisterAttribute("display", HtmlTextWriterStyle.Display);
			CssTextWriter.RegisterAttribute("filter", HtmlTextWriterStyle.Filter);
			CssTextWriter.RegisterAttribute("font-family", HtmlTextWriterStyle.FontFamily, true);
			CssTextWriter.RegisterAttribute("font-size", HtmlTextWriterStyle.FontSize);
			CssTextWriter.RegisterAttribute("font-style", HtmlTextWriterStyle.FontStyle);
			CssTextWriter.RegisterAttribute("font-variant", HtmlTextWriterStyle.FontVariant);
			CssTextWriter.RegisterAttribute("font-weight", HtmlTextWriterStyle.FontWeight);
			CssTextWriter.RegisterAttribute("height", HtmlTextWriterStyle.Height);
			CssTextWriter.RegisterAttribute("left", HtmlTextWriterStyle.Left);
			CssTextWriter.RegisterAttribute("list-style-image", HtmlTextWriterStyle.ListStyleImage, true, true);
			CssTextWriter.RegisterAttribute("list-style-type", HtmlTextWriterStyle.ListStyleType);
			CssTextWriter.RegisterAttribute("margin", HtmlTextWriterStyle.Margin);
			CssTextWriter.RegisterAttribute("margin-bottom", HtmlTextWriterStyle.MarginBottom);
			CssTextWriter.RegisterAttribute("margin-left", HtmlTextWriterStyle.MarginLeft);
			CssTextWriter.RegisterAttribute("margin-right", HtmlTextWriterStyle.MarginRight);
			CssTextWriter.RegisterAttribute("margin-top", HtmlTextWriterStyle.MarginTop);
			CssTextWriter.RegisterAttribute("overflow-x", HtmlTextWriterStyle.OverflowX);
			CssTextWriter.RegisterAttribute("overflow-y", HtmlTextWriterStyle.OverflowY);
			CssTextWriter.RegisterAttribute("overflow", HtmlTextWriterStyle.Overflow);
			CssTextWriter.RegisterAttribute("padding", HtmlTextWriterStyle.Padding);
			CssTextWriter.RegisterAttribute("padding-bottom", HtmlTextWriterStyle.PaddingBottom);
			CssTextWriter.RegisterAttribute("padding-left", HtmlTextWriterStyle.PaddingLeft);
			CssTextWriter.RegisterAttribute("padding-right", HtmlTextWriterStyle.PaddingRight);
			CssTextWriter.RegisterAttribute("padding-top", HtmlTextWriterStyle.PaddingTop);
			CssTextWriter.RegisterAttribute("position", HtmlTextWriterStyle.Position);
			CssTextWriter.RegisterAttribute("text-align", HtmlTextWriterStyle.TextAlign);
			CssTextWriter.RegisterAttribute("text-decoration", HtmlTextWriterStyle.TextDecoration);
			CssTextWriter.RegisterAttribute("text-overflow", HtmlTextWriterStyle.TextOverflow);
			CssTextWriter.RegisterAttribute("top", HtmlTextWriterStyle.Top);
			CssTextWriter.RegisterAttribute("vertical-align", HtmlTextWriterStyle.VerticalAlign);
			CssTextWriter.RegisterAttribute("visibility", HtmlTextWriterStyle.Visibility);
			CssTextWriter.RegisterAttribute("width", HtmlTextWriterStyle.Width);
			CssTextWriter.RegisterAttribute("white-space", HtmlTextWriterStyle.WhiteSpace);
			CssTextWriter.RegisterAttribute("z-index", HtmlTextWriterStyle.ZIndex);
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x0005FC49 File Offset: 0x0005DE49
		public CssTextWriter(TextWriter writer)
		{
			this._writer = writer;
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06001D6A RID: 7530 RVA: 0x0005FC58 File Offset: 0x0005DE58
		public override Encoding Encoding
		{
			get
			{
				return this._writer.Encoding;
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06001D6B RID: 7531 RVA: 0x0005FC65 File Offset: 0x0005DE65
		// (set) Token: 0x06001D6C RID: 7532 RVA: 0x0005FC72 File Offset: 0x0005DE72
		public override string NewLine
		{
			get
			{
				return this._writer.NewLine;
			}
			set
			{
				this._writer.NewLine = value;
			}
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x0005FC80 File Offset: 0x0005DE80
		public override void Close()
		{
			this._writer.Close();
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x0005FC8D File Offset: 0x0005DE8D
		public override void Flush()
		{
			this._writer.Flush();
		}

		// Token: 0x06001D6F RID: 7535 RVA: 0x0005FC9C File Offset: 0x0005DE9C
		public static HtmlTextWriterStyle GetStyleKey(string styleName)
		{
			if (!string.IsNullOrEmpty(styleName))
			{
				object obj = CssTextWriter.attrKeyLookupTable[styleName.ToLower(CultureInfo.InvariantCulture)];
				if (obj != null)
				{
					return (HtmlTextWriterStyle)obj;
				}
			}
			return (HtmlTextWriterStyle)(-1);
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x0005FCD2 File Offset: 0x0005DED2
		public static string GetStyleName(HtmlTextWriterStyle styleKey)
		{
			if (styleKey >= HtmlTextWriterStyle.BackgroundColor && styleKey < (HtmlTextWriterStyle)CssTextWriter.attrNameLookupArray.Length)
			{
				return CssTextWriter.attrNameLookupArray[(int)styleKey].name;
			}
			return string.Empty;
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x0005FCF8 File Offset: 0x0005DEF8
		public static bool IsStyleEncoded(HtmlTextWriterStyle styleKey)
		{
			return styleKey < HtmlTextWriterStyle.BackgroundColor || styleKey >= (HtmlTextWriterStyle)CssTextWriter.attrNameLookupArray.Length || CssTextWriter.attrNameLookupArray[(int)styleKey].encode;
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x0005FD1A File Offset: 0x0005DF1A
		internal static void RegisterAttribute(string name, HtmlTextWriterStyle key)
		{
			CssTextWriter.RegisterAttribute(name, key, false, false);
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x0005FD25 File Offset: 0x0005DF25
		internal static void RegisterAttribute(string name, HtmlTextWriterStyle key, bool encode)
		{
			CssTextWriter.RegisterAttribute(name, key, encode, false);
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x0005FD30 File Offset: 0x0005DF30
		internal static void RegisterAttribute(string name, HtmlTextWriterStyle key, bool encode, bool isUrl)
		{
			string key2 = name.ToLower(CultureInfo.InvariantCulture);
			CssTextWriter.attrKeyLookupTable.Add(key2, key);
			if (key < (HtmlTextWriterStyle)CssTextWriter.attrNameLookupArray.Length)
			{
				CssTextWriter.attrNameLookupArray[(int)key] = new CssTextWriter.AttributeInformation(name, encode, isUrl);
			}
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x0005FD77 File Offset: 0x0005DF77
		public override void Write(string s)
		{
			this._writer.Write(s);
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x0005FD85 File Offset: 0x0005DF85
		public override void Write(bool value)
		{
			this._writer.Write(value);
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x0005FD93 File Offset: 0x0005DF93
		public override void Write(char value)
		{
			this._writer.Write(value);
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x0005FDA1 File Offset: 0x0005DFA1
		public override void Write(char[] buffer)
		{
			this._writer.Write(buffer);
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x0005FDAF File Offset: 0x0005DFAF
		public override void Write(char[] buffer, int index, int count)
		{
			this._writer.Write(buffer, index, count);
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x0005FDBF File Offset: 0x0005DFBF
		public override void Write(double value)
		{
			this._writer.Write(value);
		}

		// Token: 0x06001D7B RID: 7547 RVA: 0x0005FDCD File Offset: 0x0005DFCD
		public override void Write(float value)
		{
			this._writer.Write(value);
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x0005FDDB File Offset: 0x0005DFDB
		public override void Write(int value)
		{
			this._writer.Write(value);
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x0005FDE9 File Offset: 0x0005DFE9
		public override void Write(long value)
		{
			this._writer.Write(value);
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x0005FDF7 File Offset: 0x0005DFF7
		public override void Write(object value)
		{
			this._writer.Write(value);
		}

		// Token: 0x06001D7F RID: 7551 RVA: 0x0005FE05 File Offset: 0x0005E005
		public override void Write(string format, object arg0)
		{
			this._writer.Write(format, arg0);
		}

		// Token: 0x06001D80 RID: 7552 RVA: 0x0005FE14 File Offset: 0x0005E014
		public override void Write(string format, object arg0, object arg1)
		{
			this._writer.Write(format, arg0, arg1);
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x0005FE24 File Offset: 0x0005E024
		public override void Write(string format, params object[] arg)
		{
			this._writer.Write(format, arg);
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x0005FE33 File Offset: 0x0005E033
		public void WriteAttribute(string name, string value)
		{
			CssTextWriter.WriteAttribute(this._writer, CssTextWriter.GetStyleKey(name), name, value);
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x0005FE48 File Offset: 0x0005E048
		public void WriteAttribute(HtmlTextWriterStyle key, string value)
		{
			CssTextWriter.WriteAttribute(this._writer, key, CssTextWriter.GetStyleName(key), value);
		}

		// Token: 0x06001D84 RID: 7556 RVA: 0x0005FE60 File Offset: 0x0005E060
		private static void WriteAttribute(TextWriter writer, HtmlTextWriterStyle key, string name, string value)
		{
			writer.Write(name);
			writer.Write(':');
			bool flag = false;
			if (key != (HtmlTextWriterStyle)(-1))
			{
				flag = CssTextWriter.attrNameLookupArray[(int)key].isUrl;
			}
			if (!flag)
			{
				writer.Write(value);
			}
			else
			{
				CssTextWriter.WriteUrlAttribute(writer, value);
			}
			writer.Write(';');
		}

		// Token: 0x06001D85 RID: 7557 RVA: 0x0005FEB0 File Offset: 0x0005E0B0
		internal static void WriteAttributes(TextWriter writer, RenderStyle[] styles, int count)
		{
			for (int i = 0; i < count; i++)
			{
				RenderStyle renderStyle = styles[i];
				CssTextWriter.WriteAttribute(writer, renderStyle.key, renderStyle.name, renderStyle.value);
			}
		}

		// Token: 0x06001D86 RID: 7558 RVA: 0x0005FEE9 File Offset: 0x0005E0E9
		public void WriteBeginCssRule(string selector)
		{
			this._writer.Write(selector);
			this._writer.Write(" { ");
		}

		// Token: 0x06001D87 RID: 7559 RVA: 0x0005FF07 File Offset: 0x0005E107
		public void WriteEndCssRule()
		{
			this._writer.WriteLine(" }");
		}

		// Token: 0x06001D88 RID: 7560 RVA: 0x0005FF19 File Offset: 0x0005E119
		public override void WriteLine(string s)
		{
			this._writer.WriteLine(s);
		}

		// Token: 0x06001D89 RID: 7561 RVA: 0x0005FF27 File Offset: 0x0005E127
		public override void WriteLine()
		{
			this._writer.WriteLine();
		}

		// Token: 0x06001D8A RID: 7562 RVA: 0x0005FF34 File Offset: 0x0005E134
		public override void WriteLine(bool value)
		{
			this._writer.WriteLine(value);
		}

		// Token: 0x06001D8B RID: 7563 RVA: 0x0005FF42 File Offset: 0x0005E142
		public override void WriteLine(char value)
		{
			this._writer.WriteLine(value);
		}

		// Token: 0x06001D8C RID: 7564 RVA: 0x0005FF50 File Offset: 0x0005E150
		public override void WriteLine(char[] buffer)
		{
			this._writer.WriteLine(buffer);
		}

		// Token: 0x06001D8D RID: 7565 RVA: 0x0005FF5E File Offset: 0x0005E15E
		public override void WriteLine(char[] buffer, int index, int count)
		{
			this._writer.WriteLine(buffer, index, count);
		}

		// Token: 0x06001D8E RID: 7566 RVA: 0x0005FF6E File Offset: 0x0005E16E
		public override void WriteLine(double value)
		{
			this._writer.WriteLine(value);
		}

		// Token: 0x06001D8F RID: 7567 RVA: 0x0005FF7C File Offset: 0x0005E17C
		public override void WriteLine(float value)
		{
			this._writer.WriteLine(value);
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x0005FF8A File Offset: 0x0005E18A
		public override void WriteLine(int value)
		{
			this._writer.WriteLine(value);
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x0005FF98 File Offset: 0x0005E198
		public override void WriteLine(long value)
		{
			this._writer.WriteLine(value);
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x0005FFA6 File Offset: 0x0005E1A6
		public override void WriteLine(object value)
		{
			this._writer.WriteLine(value);
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x0005FFB4 File Offset: 0x0005E1B4
		public override void WriteLine(string format, object arg0)
		{
			this._writer.WriteLine(format, arg0);
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x0005FFC3 File Offset: 0x0005E1C3
		public override void WriteLine(string format, object arg0, object arg1)
		{
			this._writer.WriteLine(format, arg0, arg1);
		}

		// Token: 0x06001D95 RID: 7573 RVA: 0x0005FFD3 File Offset: 0x0005E1D3
		public override void WriteLine(string format, params object[] arg)
		{
			this._writer.WriteLine(format, arg);
		}

		// Token: 0x06001D96 RID: 7574 RVA: 0x0005FFE2 File Offset: 0x0005E1E2
		public override void WriteLine(uint value)
		{
			this._writer.WriteLine(value);
		}

		// Token: 0x06001D97 RID: 7575 RVA: 0x0005FFF0 File Offset: 0x0005E1F0
		internal static void WriteUrlAttribute(TextWriter writer, string url)
		{
			string text = url;
			char[] array = new char[]
			{
				'\'',
				'"'
			};
			char? c = null;
			if (StringUtil.StringStartsWith(url, "url("))
			{
				int startIndex = 4;
				int num = url.Length - 4;
				if (StringUtil.StringEndsWith(url, ')'))
				{
					num--;
				}
				text = url.Substring(startIndex, num).Trim();
			}
			foreach (char c2 in array)
			{
				if (StringUtil.StringStartsWith(text, c2) && StringUtil.StringEndsWith(text, c2))
				{
					text = text.Trim(new char[]
					{
						c2
					});
					c = new char?(c2);
					break;
				}
			}
			writer.Write("url(");
			if (c != null)
			{
				writer.Write(c);
			}
			writer.Write(HttpUtility.UrlPathEncode(text));
			if (c != null)
			{
				writer.Write(c);
			}
			writer.Write(")");
		}

		// Token: 0x04001958 RID: 6488
		private TextWriter _writer;

		// Token: 0x04001959 RID: 6489
		private static Hashtable attrKeyLookupTable = new Hashtable(43);

		// Token: 0x0400195A RID: 6490
		private static CssTextWriter.AttributeInformation[] attrNameLookupArray = new CssTextWriter.AttributeInformation[43];

		// Token: 0x02000965 RID: 2405
		private struct AttributeInformation
		{
			// Token: 0x060069F4 RID: 27124 RVA: 0x00178C66 File Offset: 0x00176E66
			public AttributeInformation(string name, bool encode, bool isUrl)
			{
				this.name = name;
				this.encode = encode;
				this.isUrl = isUrl;
			}

			// Token: 0x04003841 RID: 14401
			public string name;

			// Token: 0x04003842 RID: 14402
			public bool isUrl;

			// Token: 0x04003843 RID: 14403
			public bool encode;
		}
	}
}
