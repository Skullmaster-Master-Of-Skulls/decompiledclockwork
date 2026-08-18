using System;
using System.Collections.Generic;
using System.Globalization;
using System.util;
using iTextSharp.text.pdf;

namespace iTextSharp.text.html.simpleparser
{
	// Token: 0x02000066 RID: 102
	public class FactoryProperties
	{
		// Token: 0x0600034E RID: 846 RVA: 0x00010EE4 File Offset: 0x0000FEE4
		public Chunk CreateChunk(string text, ChainedProperties props)
		{
			Font font = this.GetFont(props);
			float num = font.Size;
			num /= 2f;
			Chunk chunk = new Chunk(text, font);
			if (props.HasProperty("sub"))
			{
				chunk.SetTextRise(-num);
			}
			else if (props.HasProperty("sup"))
			{
				chunk.SetTextRise(num);
			}
			chunk.SetHyphenation(FactoryProperties.GetHyphenation(props));
			return chunk;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00010F4C File Offset: 0x0000FF4C
		private static void SetParagraphLeading(Paragraph p, string leading)
		{
			if (leading == null)
			{
				p.SetLeading(0f, 1.5f);
				return;
			}
			try
			{
				StringTokenizer stringTokenizer = new StringTokenizer(leading, " ,");
				string s = stringTokenizer.NextToken();
				float fixedLeading = float.Parse(s, NumberFormatInfo.InvariantInfo);
				if (!stringTokenizer.HasMoreTokens())
				{
					p.SetLeading(fixedLeading, 0f);
				}
				else
				{
					s = stringTokenizer.NextToken();
					float multipliedLeading = float.Parse(s, NumberFormatInfo.InvariantInfo);
					p.SetLeading(fixedLeading, multipliedLeading);
				}
			}
			catch
			{
				p.SetLeading(0f, 1.5f);
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00010FE4 File Offset: 0x0000FFE4
		public static void CreateParagraph(Paragraph p, ChainedProperties props)
		{
			string text = props["align"];
			if (text != null)
			{
				if (Util.EqualsIgnoreCase(text, "center"))
				{
					p.Alignment = 1;
				}
				else if (Util.EqualsIgnoreCase(text, "right"))
				{
					p.Alignment = 2;
				}
				else if (Util.EqualsIgnoreCase(text, "justify"))
				{
					p.Alignment = 3;
				}
			}
			p.Hyphenation = FactoryProperties.GetHyphenation(props);
			FactoryProperties.SetParagraphLeading(p, props["leading"]);
			text = props["before"];
			if (text != null)
			{
				try
				{
					p.SpacingBefore = float.Parse(text, NumberFormatInfo.InvariantInfo);
				}
				catch
				{
				}
			}
			text = props["after"];
			if (text != null)
			{
				try
				{
					p.SpacingAfter = float.Parse(text, NumberFormatInfo.InvariantInfo);
				}
				catch
				{
				}
			}
			text = props["extraparaspace"];
			if (text != null)
			{
				try
				{
					p.ExtraParagraphSpace = float.Parse(text, NumberFormatInfo.InvariantInfo);
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x000110F4 File Offset: 0x000100F4
		public static Paragraph CreateParagraph(ChainedProperties props)
		{
			Paragraph paragraph = new Paragraph();
			FactoryProperties.CreateParagraph(paragraph, props);
			return paragraph;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00011110 File Offset: 0x00010110
		public static ListItem CreateListItem(ChainedProperties props)
		{
			ListItem listItem = new ListItem();
			FactoryProperties.CreateParagraph(listItem, props);
			return listItem;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0001112C File Offset: 0x0001012C
		public Font GetFont(ChainedProperties props)
		{
			string text = props["face"];
			if (text != null)
			{
				StringTokenizer stringTokenizer = new StringTokenizer(text, ",");
				while (stringTokenizer.HasMoreTokens())
				{
					text = stringTokenizer.NextToken().Trim();
					if (text.StartsWith("\""))
					{
						text = text.Substring(1);
					}
					if (text.EndsWith("\""))
					{
						text = text.Substring(0, text.Length - 1);
					}
					if (this.fontImp.IsRegistered(text))
					{
						break;
					}
				}
			}
			int num = 0;
			if (props.HasProperty("i"))
			{
				num |= 2;
			}
			if (props.HasProperty("b"))
			{
				num |= 1;
			}
			if (props.HasProperty("u"))
			{
				num |= 4;
			}
			if (props.HasProperty("s"))
			{
				num |= 8;
			}
			string text2 = props["size"];
			float size = 12f;
			if (text2 != null)
			{
				size = float.Parse(text2, NumberFormatInfo.InvariantInfo);
			}
			BaseColor color = Markup.DecodeColor(props["color"]);
			string text3 = props["encoding"];
			if (text3 == null)
			{
				text3 = "Cp1252";
			}
			return this.fontImp.GetFont(text, text3, true, size, num, color);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00011251 File Offset: 0x00010251
		public static IHyphenationEvent GetHyphenation(ChainedProperties props)
		{
			return FactoryProperties.GetHyphenation(props["hyphenation"]);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00011263 File Offset: 0x00010263
		public static IHyphenationEvent GetHyphenation(Dictionary<string, string> props)
		{
			if (props.ContainsKey("hyphenation"))
			{
				return FactoryProperties.GetHyphenation(props["hyphenation"]);
			}
			return null;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00011284 File Offset: 0x00010284
		public static IHyphenationEvent GetHyphenation(string s)
		{
			if (s == null || s.Length == 0)
			{
				return null;
			}
			string lang = s;
			string text = null;
			int leftMin = 2;
			int rightMin = 2;
			int num = s.IndexOf('_');
			if (num == -1)
			{
				return new HyphenationAuto(lang, text, leftMin, rightMin);
			}
			lang = s.Substring(0, num);
			text = s.Substring(num + 1);
			num = text.IndexOf(',');
			if (num == -1)
			{
				return new HyphenationAuto(lang, text, leftMin, rightMin);
			}
			s = text.Substring(num + 1);
			text = text.Substring(0, num);
			num = s.IndexOf(',');
			if (num == -1)
			{
				leftMin = int.Parse(s);
			}
			else
			{
				leftMin = int.Parse(s.Substring(0, num));
				rightMin = int.Parse(s.Substring(num + 1));
			}
			return new HyphenationAuto(lang, text, leftMin, rightMin);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00011344 File Offset: 0x00010344
		public static void InsertStyle(Dictionary<string, string> h)
		{
			string str;
			if (!h.TryGetValue("style", out str))
			{
				return;
			}
			Properties properties = Markup.ParseAttributes(str);
			foreach (string text in properties.Keys)
			{
				if (text.Equals("font-family"))
				{
					h["face"] = properties[text];
				}
				else if (text.Equals("font-size"))
				{
					h["size"] = Markup.ParseLength(properties[text]).ToString(NumberFormatInfo.InvariantInfo) + "pt";
				}
				else if (text.Equals("font-style"))
				{
					string text2 = properties[text].Trim().ToLower(CultureInfo.InvariantCulture);
					if (text2.Equals("italic") || text2.Equals("oblique"))
					{
						h["i"] = null;
					}
				}
				else if (text.Equals("font-weight"))
				{
					string text3 = properties[text].Trim().ToLower(CultureInfo.InvariantCulture);
					if (text3.Equals("bold") || text3.Equals("700") || text3.Equals("800") || text3.Equals("900"))
					{
						h["b"] = null;
					}
				}
				else if (text.Equals("text-decoration"))
				{
					string text4 = properties[text].Trim().ToLower(CultureInfo.InvariantCulture);
					if (text4.Equals("underline"))
					{
						h["u"] = null;
					}
				}
				else if (text.Equals("color"))
				{
					BaseColor baseColor = Markup.DecodeColor(properties[text]);
					if (baseColor != null)
					{
						string value = "#" + (baseColor.ToArgb() & 16777215).ToString("X06", NumberFormatInfo.InvariantInfo);
						h["color"] = value;
					}
				}
				else if (text.Equals("line-height"))
				{
					string text5 = properties[text].Trim();
					float num = Markup.ParseLength(properties[text]);
					if (text5.EndsWith("%"))
					{
						h["leading"] = "0," + (num / 100f).ToString(NumberFormatInfo.InvariantInfo);
					}
					else if (Util.EqualsIgnoreCase("normal", text5))
					{
						h["leading"] = "0,1.5";
					}
					else
					{
						h["leading"] = num.ToString(NumberFormatInfo.InvariantInfo) + ",0";
					}
				}
				else if (text.Equals("text-align"))
				{
					string value2 = properties[text].Trim().ToLower(CultureInfo.InvariantCulture);
					h["align"] = value2;
				}
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00011668 File Offset: 0x00010668
		public static void InsertStyle(Dictionary<string, string> h, ChainedProperties cprops)
		{
			string str;
			if (!h.TryGetValue("style", out str))
			{
				return;
			}
			Properties properties = Markup.ParseAttributes(str);
			foreach (string text in properties.Keys)
			{
				if (text.Equals("font-family"))
				{
					h["face"] = properties[text];
				}
				else if (text.Equals("font-size"))
				{
					float num = Markup.ParseLength(cprops["size"], 12f);
					if (num <= 0f)
					{
						num = 12f;
					}
					h["size"] = Markup.ParseLength(properties[text], num).ToString(NumberFormatInfo.InvariantInfo) + "pt";
				}
				else if (text.Equals("font-style"))
				{
					string text2 = properties[text].Trim().ToLower(CultureInfo.InvariantCulture);
					if (text2.Equals("italic") || text2.Equals("oblique"))
					{
						h["i"] = null;
					}
				}
				else if (text.Equals("font-weight"))
				{
					string text3 = properties[text].Trim().ToLower(CultureInfo.InvariantCulture);
					if (text3.Equals("bold") || text3.Equals("700") || text3.Equals("800") || text3.Equals("900"))
					{
						h["b"] = null;
					}
				}
				else if (text.Equals("text-decoration"))
				{
					string text4 = properties[text].Trim().ToLower(CultureInfo.InvariantCulture);
					if (text4.Equals("underline"))
					{
						h["u"] = null;
					}
				}
				else if (text.Equals("color"))
				{
					BaseColor baseColor = Markup.DecodeColor(properties[text]);
					if (baseColor != null)
					{
						string value = "#" + (baseColor.ToArgb() & 16777215).ToString("X06", NumberFormatInfo.InvariantInfo);
						h["color"] = value;
					}
				}
				else if (text.Equals("line-height"))
				{
					string text5 = properties[text].Trim();
					float num2 = Markup.ParseLength(cprops["size"], 12f);
					if (num2 <= 0f)
					{
						num2 = 12f;
					}
					float num3 = Markup.ParseLength(properties[text], num2);
					if (text5.EndsWith("%"))
					{
						h["leading"] = "0," + (num3 / 100f).ToString(NumberFormatInfo.InvariantInfo);
					}
					else if (Util.EqualsIgnoreCase("normal", text5))
					{
						h["leading"] = "0,1.5";
					}
					else
					{
						h["leading"] = num3.ToString(NumberFormatInfo.InvariantInfo) + ",0";
					}
				}
				else if (text.Equals("text-align"))
				{
					string value2 = properties[text].Trim().ToLower(CultureInfo.InvariantCulture);
					h["align"] = value2;
				}
				else if (text.Equals("padding-left"))
				{
					string value3 = properties[text].Trim().ToLower(CultureInfo.InvariantCulture);
					h["indent"] = value3;
				}
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00011A14 File Offset: 0x00010A14
		// (set) Token: 0x0600035A RID: 858 RVA: 0x00011A1C File Offset: 0x00010A1C
		public IFontProvider FontImp
		{
			get
			{
				return this.fontImp;
			}
			set
			{
				this.fontImp = value;
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00011A28 File Offset: 0x00010A28
		static FactoryProperties()
		{
			FactoryProperties.followTags["i"] = "i";
			FactoryProperties.followTags["b"] = "b";
			FactoryProperties.followTags["u"] = "u";
			FactoryProperties.followTags["sub"] = "sub";
			FactoryProperties.followTags["sup"] = "sup";
			FactoryProperties.followTags["em"] = "i";
			FactoryProperties.followTags["strong"] = "b";
			FactoryProperties.followTags["s"] = "s";
			FactoryProperties.followTags["strike"] = "s";
		}

		// Token: 0x040001BE RID: 446
		private IFontProvider fontImp = FontFactory.FontImp;

		// Token: 0x040001BF RID: 447
		public static Dictionary<string, string> followTags = new Dictionary<string, string>();
	}
}
