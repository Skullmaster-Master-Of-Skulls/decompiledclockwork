using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x020003CC RID: 972
	internal sealed class CssTextWriter : TextWriter
	{
		// Token: 0x06002F69 RID: 12137 RVA: 0x000D3658 File Offset: 0x000D2658
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

		// Token: 0x06002F6A RID: 12138 RVA: 0x000D387D File Offset: 0x000D287D
		public CssTextWriter(TextWriter writer)
		{
			this._writer = writer;
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06002F6B RID: 12139 RVA: 0x000D388C File Offset: 0x000D288C
		public override Encoding Encoding
		{
			get
			{
				return this._writer.Encoding;
			}
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x06002F6C RID: 12140 RVA: 0x000D3899 File Offset: 0x000D2899
		// (set) Token: 0x06002F6D RID: 12141 RVA: 0x000D38A6 File Offset: 0x000D28A6
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

		// Token: 0x06002F6E RID: 12142 RVA: 0x000D38B4 File Offset: 0x000D28B4
		public override void Close()
		{
			this._writer.Close();
		}

		// Token: 0x06002F6F RID: 12143 RVA: 0x000D38C1 File Offset: 0x000D28C1
		public override void Flush()
		{
			this._writer.Flush();
		}

		// Token: 0x06002F70 RID: 12144 RVA: 0x000D38D0 File Offset: 0x000D28D0
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

		// Token: 0x06002F71 RID: 12145 RVA: 0x000D3906 File Offset: 0x000D2906
		public static string GetStyleName(HtmlTextWriterStyle styleKey)
		{
			if (styleKey >= HtmlTextWriterStyle.BackgroundColor && styleKey < (HtmlTextWriterStyle)CssTextWriter.attrNameLookupArray.Length)
			{
				return CssTextWriter.attrNameLookupArray[(int)styleKey].name;
			}
			return string.Empty;
		}

		// Token: 0x06002F72 RID: 12146 RVA: 0x000D392C File Offset: 0x000D292C
		public static bool IsStyleEncoded(HtmlTextWriterStyle styleKey)
		{
			return styleKey < HtmlTextWriterStyle.BackgroundColor || styleKey >= (HtmlTextWriterStyle)CssTextWriter.attrNameLookupArray.Length || CssTextWriter.attrNameLookupArray[(int)styleKey].encode;
		}

		// Token: 0x06002F73 RID: 12147 RVA: 0x000D394E File Offset: 0x000D294E
		internal static void RegisterAttribute(string name, HtmlTextWriterStyle key)
		{
			CssTextWriter.RegisterAttribute(name, key, false, false);
		}

		// Token: 0x06002F74 RID: 12148 RVA: 0x000D3959 File Offset: 0x000D2959
		internal static void RegisterAttribute(string name, HtmlTextWriterStyle key, bool encode)
		{
			CssTextWriter.RegisterAttribute(name, key, encode, false);
		}

		// Token: 0x06002F75 RID: 12149 RVA: 0x000D3964 File Offset: 0x000D2964
		internal static void RegisterAttribute(string name, HtmlTextWriterStyle key, bool encode, bool isUrl)
		{
			string key2 = name.ToLower(CultureInfo.InvariantCulture);
			CssTextWriter.attrKeyLookupTable.Add(key2, key);
			if (key < (HtmlTextWriterStyle)CssTextWriter.attrNameLookupArray.Length)
			{
				CssTextWriter.attrNameLookupArray[(int)key] = new CssTextWriter.AttributeInformation(name, encode, isUrl);
			}
		}

		// Token: 0x06002F76 RID: 12150 RVA: 0x000D39B0 File Offset: 0x000D29B0
		public override void Write(string s)
		{
			this._writer.Write(s);
		}

		// Token: 0x06002F77 RID: 12151 RVA: 0x000D39BE File Offset: 0x000D29BE
		public override void Write(bool value)
		{
			this._writer.Write(value);
		}

		// Token: 0x06002F78 RID: 12152 RVA: 0x000D39CC File Offset: 0x000D29CC
		public override void Write(char value)
		{
			this._writer.Write(value);
		}

		// Token: 0x06002F79 RID: 12153 RVA: 0x000D39DA File Offset: 0x000D29DA
		public override void Write(char[] buffer)
		{
			this._writer.Write(buffer);
		}

		// Token: 0x06002F7A RID: 12154 RVA: 0x000D39E8 File Offset: 0x000D29E8
		public override void Write(char[] buffer, int index, int count)
		{
			this._writer.Write(buffer, index, count);
		}

		// Token: 0x06002F7B RID: 12155 RVA: 0x000D39F8 File Offset: 0x000D29F8
		public override void Write(double value)
		{
			this._writer.Write(value);
		}

		// Token: 0x06002F7C RID: 12156 RVA: 0x000D3A06 File Offset: 0x000D2A06
		public override void Write(float value)
		{
			this._writer.Write(value);
		}

		// Token: 0x06002F7D RID: 12157 RVA: 0x000D3A14 File Offset: 0x000D2A14
		public override void Write(int value)
		{
			this._writer.Write(value);
		}

		// Token: 0x06002F7E RID: 12158 RVA: 0x000D3A22 File Offset: 0x000D2A22
		public override void Write(long value)
		{
			this._writer.Write(value);
		}

		// Token: 0x06002F7F RID: 12159 RVA: 0x000D3A30 File Offset: 0x000D2A30
		public override void Write(object value)
		{
			this._writer.Write(value);
		}

		// Token: 0x06002F80 RID: 12160 RVA: 0x000D3A3E File Offset: 0x000D2A3E
		public override void Write(string format, object arg0)
		{
			this._writer.Write(format, arg0);
		}

		// Token: 0x06002F81 RID: 12161 RVA: 0x000D3A4D File Offset: 0x000D2A4D
		public override void Write(string format, object arg0, object arg1)
		{
			this._writer.Write(format, arg0, arg1);
		}

		// Token: 0x06002F82 RID: 12162 RVA: 0x000D3A5D File Offset: 0x000D2A5D
		public override void Write(string format, params object[] arg)
		{
			this._writer.Write(format, arg);
		}

		// Token: 0x06002F83 RID: 12163 RVA: 0x000D3A6C File Offset: 0x000D2A6C
		public void WriteAttribute(string name, string value)
		{
			CssTextWriter.WriteAttribute(this._writer, CssTextWriter.GetStyleKey(name), name, value);
		}

		// Token: 0x06002F84 RID: 12164 RVA: 0x000D3A81 File Offset: 0x000D2A81
		public void WriteAttribute(HtmlTextWriterStyle key, string value)
		{
			CssTextWriter.WriteAttribute(this._writer, key, CssTextWriter.GetStyleName(key), value);
		}

		// Token: 0x06002F85 RID: 12165 RVA: 0x000D3A98 File Offset: 0x000D2A98
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

		// Token: 0x06002F86 RID: 12166 RVA: 0x000D3AE8 File Offset: 0x000D2AE8
		internal static void WriteAttributes(TextWriter writer, RenderStyle[] styles, int count)
		{
			for (int i = 0; i < count; i++)
			{
				RenderStyle renderStyle = styles[i];
				CssTextWriter.WriteAttribute(writer, renderStyle.key, renderStyle.name, renderStyle.value);
			}
		}

		// Token: 0x06002F87 RID: 12167 RVA: 0x000D3B29 File Offset: 0x000D2B29
		public void WriteBeginCssRule(string selector)
		{
			this._writer.Write(selector);
			this._writer.Write(" { ");
		}

		// Token: 0x06002F88 RID: 12168 RVA: 0x000D3B47 File Offset: 0x000D2B47
		public void WriteEndCssRule()
		{
			this._writer.WriteLine(" }");
		}

		// Token: 0x06002F89 RID: 12169 RVA: 0x000D3B59 File Offset: 0x000D2B59
		public override void WriteLine(string s)
		{
			this._writer.WriteLine(s);
		}

		// Token: 0x06002F8A RID: 12170 RVA: 0x000D3B67 File Offset: 0x000D2B67
		public override void WriteLine()
		{
			this._writer.WriteLine();
		}

		// Token: 0x06002F8B RID: 12171 RVA: 0x000D3B74 File Offset: 0x000D2B74
		public override void WriteLine(bool value)
		{
			this._writer.WriteLine(value);
		}

		// Token: 0x06002F8C RID: 12172 RVA: 0x000D3B82 File Offset: 0x000D2B82
		public override void WriteLine(char value)
		{
			this._writer.WriteLine(value);
		}

		// Token: 0x06002F8D RID: 12173 RVA: 0x000D3B90 File Offset: 0x000D2B90
		public override void WriteLine(char[] buffer)
		{
			this._writer.WriteLine(buffer);
		}

		// Token: 0x06002F8E RID: 12174 RVA: 0x000D3B9E File Offset: 0x000D2B9E
		public override void WriteLine(char[] buffer, int index, int count)
		{
			this._writer.WriteLine(buffer, index, count);
		}

		// Token: 0x06002F8F RID: 12175 RVA: 0x000D3BAE File Offset: 0x000D2BAE
		public override void WriteLine(double value)
		{
			this._writer.WriteLine(value);
		}

		// Token: 0x06002F90 RID: 12176 RVA: 0x000D3BBC File Offset: 0x000D2BBC
		public override void WriteLine(float value)
		{
			this._writer.WriteLine(value);
		}

		// Token: 0x06002F91 RID: 12177 RVA: 0x000D3BCA File Offset: 0x000D2BCA
		public override void WriteLine(int value)
		{
			this._writer.WriteLine(value);
		}

		// Token: 0x06002F92 RID: 12178 RVA: 0x000D3BD8 File Offset: 0x000D2BD8
		public override void WriteLine(long value)
		{
			this._writer.WriteLine(value);
		}

		// Token: 0x06002F93 RID: 12179 RVA: 0x000D3BE6 File Offset: 0x000D2BE6
		public override void WriteLine(object value)
		{
			this._writer.WriteLine(value);
		}

		// Token: 0x06002F94 RID: 12180 RVA: 0x000D3BF4 File Offset: 0x000D2BF4
		public override void WriteLine(string format, object arg0)
		{
			this._writer.WriteLine(format, arg0);
		}

		// Token: 0x06002F95 RID: 12181 RVA: 0x000D3C03 File Offset: 0x000D2C03
		public override void WriteLine(string format, object arg0, object arg1)
		{
			this._writer.WriteLine(format, arg0, arg1);
		}

		// Token: 0x06002F96 RID: 12182 RVA: 0x000D3C13 File Offset: 0x000D2C13
		public override void WriteLine(string format, params object[] arg)
		{
			this._writer.WriteLine(format, arg);
		}

		// Token: 0x06002F97 RID: 12183 RVA: 0x000D3C22 File Offset: 0x000D2C22
		public override void WriteLine(uint value)
		{
			this._writer.WriteLine(value);
		}

		// Token: 0x06002F98 RID: 12184 RVA: 0x000D3C30 File Offset: 0x000D2C30
		private static void WriteUrlAttribute(TextWriter writer, string url)
		{
			string str = url;
			if (StringUtil.StringStartsWith(url, "url("))
			{
				int startIndex = 4;
				int num = url.Length - 4;
				if (StringUtil.StringEndsWith(url, ')'))
				{
					num--;
				}
				str = url.Substring(startIndex, num).Trim();
			}
			writer.Write("url(");
			writer.Write(HttpUtility.UrlPathEncode(str));
			writer.Write(")");
		}

		// Token: 0x040021E0 RID: 8672
		private TextWriter _writer;

		// Token: 0x040021E1 RID: 8673
		private static Hashtable attrKeyLookupTable = new Hashtable(43);

		// Token: 0x040021E2 RID: 8674
		private static CssTextWriter.AttributeInformation[] attrNameLookupArray = new CssTextWriter.AttributeInformation[43];

		// Token: 0x020003CD RID: 973
		private struct AttributeInformation
		{
			// Token: 0x06002F99 RID: 12185 RVA: 0x000D3C95 File Offset: 0x000D2C95
			public AttributeInformation(string name, bool encode, bool isUrl)
			{
				this.name = name;
				this.encode = encode;
				this.isUrl = isUrl;
			}

			// Token: 0x040021E3 RID: 8675
			public string name;

			// Token: 0x040021E4 RID: 8676
			public bool isUrl;

			// Token: 0x040021E5 RID: 8677
			public bool encode;
		}
	}
}
