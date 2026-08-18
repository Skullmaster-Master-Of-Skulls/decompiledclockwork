using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.util;
using iTextSharp.text.html;
using iTextSharp.text.pdf;

namespace iTextSharp.text
{
	// Token: 0x020004F5 RID: 1269
	public class FontFactoryImp : IFontProvider
	{
		// Token: 0x06002B57 RID: 11095 RVA: 0x0010641C File Offset: 0x0010541C
		public FontFactoryImp()
		{
			this.trueTypeFonts.Add("Courier".ToLower(CultureInfo.InvariantCulture), "Courier");
			this.trueTypeFonts.Add("Courier-Bold".ToLower(CultureInfo.InvariantCulture), "Courier-Bold");
			this.trueTypeFonts.Add("Courier-Oblique".ToLower(CultureInfo.InvariantCulture), "Courier-Oblique");
			this.trueTypeFonts.Add("Courier-BoldOblique".ToLower(CultureInfo.InvariantCulture), "Courier-BoldOblique");
			this.trueTypeFonts.Add("Helvetica".ToLower(CultureInfo.InvariantCulture), "Helvetica");
			this.trueTypeFonts.Add("Helvetica-Bold".ToLower(CultureInfo.InvariantCulture), "Helvetica-Bold");
			this.trueTypeFonts.Add("Helvetica-Oblique".ToLower(CultureInfo.InvariantCulture), "Helvetica-Oblique");
			this.trueTypeFonts.Add("Helvetica-BoldOblique".ToLower(CultureInfo.InvariantCulture), "Helvetica-BoldOblique");
			this.trueTypeFonts.Add("Symbol".ToLower(CultureInfo.InvariantCulture), "Symbol");
			this.trueTypeFonts.Add("Times-Roman".ToLower(CultureInfo.InvariantCulture), "Times-Roman");
			this.trueTypeFonts.Add("Times-Bold".ToLower(CultureInfo.InvariantCulture), "Times-Bold");
			this.trueTypeFonts.Add("Times-Italic".ToLower(CultureInfo.InvariantCulture), "Times-Italic");
			this.trueTypeFonts.Add("Times-BoldItalic".ToLower(CultureInfo.InvariantCulture), "Times-BoldItalic");
			this.trueTypeFonts.Add("ZapfDingbats".ToLower(CultureInfo.InvariantCulture), "ZapfDingbats");
			List<string> list = new List<string>();
			list.Add("Courier");
			list.Add("Courier-Bold");
			list.Add("Courier-Oblique");
			list.Add("Courier-BoldOblique");
			this.fontFamilies["Courier".ToLower(CultureInfo.InvariantCulture)] = list;
			list = new List<string>();
			list.Add("Helvetica");
			list.Add("Helvetica-Bold");
			list.Add("Helvetica-Oblique");
			list.Add("Helvetica-BoldOblique");
			this.fontFamilies["Helvetica".ToLower(CultureInfo.InvariantCulture)] = list;
			list = new List<string>();
			list.Add("Symbol");
			this.fontFamilies["Symbol".ToLower(CultureInfo.InvariantCulture)] = list;
			list = new List<string>();
			list.Add("Times-Roman");
			list.Add("Times-Bold");
			list.Add("Times-Italic");
			list.Add("Times-BoldItalic");
			this.fontFamilies["Times".ToLower(CultureInfo.InvariantCulture)] = list;
			this.fontFamilies["Times-Roman".ToLower(CultureInfo.InvariantCulture)] = list;
			list = new List<string>();
			list.Add("ZapfDingbats");
			this.fontFamilies["ZapfDingbats".ToLower(CultureInfo.InvariantCulture)] = list;
		}

		// Token: 0x06002B58 RID: 11096 RVA: 0x0010675C File Offset: 0x0010575C
		public virtual Font GetFont(string fontname, string encoding, bool embedded, float size, int style, BaseColor color)
		{
			return this.GetFont(fontname, encoding, embedded, size, style, color, true);
		}

		// Token: 0x06002B59 RID: 11097 RVA: 0x00106770 File Offset: 0x00105770
		public virtual Font GetFont(string fontname, string encoding, bool embedded, float size, int style, BaseColor color, bool cached)
		{
			if (fontname == null)
			{
				return new Font(Font.FontFamily.UNDEFINED, size, style, color);
			}
			string key = fontname.ToLower(CultureInfo.InvariantCulture);
			List<string> list;
			this.fontFamilies.TryGetValue(key, out list);
			if (list != null)
			{
				int num = 0;
				bool flag = false;
				int num2 = (style == -1) ? 0 : style;
				foreach (string text in list)
				{
					string text2 = text.ToLower(CultureInfo.InvariantCulture);
					num = 0;
					if (text2.ToLower(CultureInfo.InvariantCulture).IndexOf("bold") != -1)
					{
						num |= 1;
					}
					if (text2.ToLower(CultureInfo.InvariantCulture).IndexOf("italic") != -1 || text2.ToLower(CultureInfo.InvariantCulture).IndexOf("oblique") != -1)
					{
						num |= 2;
					}
					if ((num2 & 3) == num)
					{
						fontname = text;
						flag = true;
						break;
					}
				}
				if (style != -1 && flag)
				{
					style &= ~num;
				}
			}
			BaseFont baseFont = null;
			try
			{
				try
				{
					baseFont = BaseFont.CreateFont(fontname, encoding, embedded, cached, null, null, true);
				}
				catch (DocumentException)
				{
				}
				if (baseFont == null)
				{
					this.trueTypeFonts.TryGetValue(fontname.ToLower(CultureInfo.InvariantCulture), out fontname);
					if (fontname == null)
					{
						return new Font(Font.FontFamily.UNDEFINED, size, style, color);
					}
					baseFont = BaseFont.CreateFont(fontname, encoding, embedded, cached, null, null);
				}
			}
			catch (DocumentException ex)
			{
				throw ex;
			}
			catch (IOException)
			{
				return new Font(Font.FontFamily.UNDEFINED, size, style, color);
			}
			catch
			{
				return new Font(Font.FontFamily.UNDEFINED, size, style, color);
			}
			return new Font(baseFont, size, style, color);
		}

		// Token: 0x06002B5A RID: 11098 RVA: 0x0010693C File Offset: 0x0010593C
		public virtual Font GetFont(Properties attributes)
		{
			string text = null;
			string encoding = this.defaultEncoding;
			bool embedded = this.defaultEmbedding;
			float size = -1f;
			int num = 0;
			BaseColor color = null;
			string text2 = attributes["style"];
			if (text2 != null && text2.Length > 0)
			{
				Properties properties = Markup.ParseAttributes(text2);
				if (properties.Count == 0)
				{
					attributes.Add("style", text2);
				}
				else
				{
					text = properties["font-family"];
					if (text != null)
					{
						while (text.IndexOf(',') != -1)
						{
							string text3 = text.Substring(0, text.IndexOf(','));
							if (this.IsRegistered(text3))
							{
								text = text3;
							}
							else
							{
								text = text.Substring(text.IndexOf(',') + 1);
							}
						}
					}
					if ((text2 = properties["font-size"]) != null)
					{
						size = Markup.ParseLength(text2);
					}
					if ((text2 = properties["font-weight"]) != null)
					{
						num |= Font.GetStyleValue(text2);
					}
					if ((text2 = properties["font-style"]) != null)
					{
						num |= Font.GetStyleValue(text2);
					}
					if ((text2 = properties["color"]) != null)
					{
						color = Markup.DecodeColor(text2);
					}
					attributes.AddAll(properties);
				}
			}
			if ((text2 = attributes["encoding"]) != null)
			{
				encoding = text2;
			}
			if ("true".Equals(attributes["embedded"]))
			{
				embedded = true;
			}
			if ((text2 = attributes["font"]) != null)
			{
				text = text2;
			}
			if ((text2 = attributes["size"]) != null)
			{
				size = float.Parse(text2, NumberFormatInfo.InvariantInfo);
			}
			if ((text2 = attributes["style"]) != null)
			{
				num |= Font.GetStyleValue(text2);
			}
			if ((text2 = attributes["fontstyle"]) != null)
			{
				num |= Font.GetStyleValue(text2);
			}
			string text4 = attributes["red"];
			string text5 = attributes["green"];
			string text6 = attributes["blue"];
			if (text4 != null || text5 != null || text6 != null)
			{
				int red = 0;
				int green = 0;
				int blue = 0;
				if (text4 != null)
				{
					red = int.Parse(text4);
				}
				if (text5 != null)
				{
					green = int.Parse(text5);
				}
				if (text6 != null)
				{
					blue = int.Parse(text6);
				}
				color = new BaseColor(red, green, blue);
			}
			else if ((text2 = attributes["color"]) != null)
			{
				color = Markup.DecodeColor(text2);
			}
			if (text == null)
			{
				return this.GetFont(null, encoding, embedded, size, num, color);
			}
			return this.GetFont(text, encoding, embedded, size, num, color);
		}

		// Token: 0x06002B5B RID: 11099 RVA: 0x00106BAC File Offset: 0x00105BAC
		public Font GetFont(string fontname, string encoding, bool embedded, float size, int style)
		{
			return this.GetFont(fontname, encoding, embedded, size, style, null);
		}

		// Token: 0x06002B5C RID: 11100 RVA: 0x00106BBC File Offset: 0x00105BBC
		public virtual Font GetFont(string fontname, string encoding, bool embedded, float size)
		{
			return this.GetFont(fontname, encoding, embedded, size, -1, null);
		}

		// Token: 0x06002B5D RID: 11101 RVA: 0x00106BCB File Offset: 0x00105BCB
		public virtual Font GetFont(string fontname, string encoding, bool embedded)
		{
			return this.GetFont(fontname, encoding, embedded, -1f, -1, null);
		}

		// Token: 0x06002B5E RID: 11102 RVA: 0x00106BDD File Offset: 0x00105BDD
		public virtual Font GetFont(string fontname, string encoding, float size, int style, BaseColor color)
		{
			return this.GetFont(fontname, encoding, this.defaultEmbedding, size, style, color);
		}

		// Token: 0x06002B5F RID: 11103 RVA: 0x00106BF2 File Offset: 0x00105BF2
		public virtual Font GetFont(string fontname, string encoding, float size, int style)
		{
			return this.GetFont(fontname, encoding, this.defaultEmbedding, size, style, null);
		}

		// Token: 0x06002B60 RID: 11104 RVA: 0x00106C06 File Offset: 0x00105C06
		public virtual Font GetFont(string fontname, string encoding, float size)
		{
			return this.GetFont(fontname, encoding, this.defaultEmbedding, size, -1, null);
		}

		// Token: 0x06002B61 RID: 11105 RVA: 0x00106C19 File Offset: 0x00105C19
		public virtual Font GetFont(string fontname, string encoding)
		{
			return this.GetFont(fontname, encoding, this.defaultEmbedding, -1f, -1, null);
		}

		// Token: 0x06002B62 RID: 11106 RVA: 0x00106C30 File Offset: 0x00105C30
		public virtual Font GetFont(string fontname, float size, int style, BaseColor color)
		{
			return this.GetFont(fontname, this.defaultEncoding, this.defaultEmbedding, size, style, color);
		}

		// Token: 0x06002B63 RID: 11107 RVA: 0x00106C49 File Offset: 0x00105C49
		public virtual Font GetFont(string fontname, float size, BaseColor color)
		{
			return this.GetFont(fontname, this.defaultEncoding, this.defaultEmbedding, size, -1, color);
		}

		// Token: 0x06002B64 RID: 11108 RVA: 0x00106C61 File Offset: 0x00105C61
		public virtual Font GetFont(string fontname, float size, int style)
		{
			return this.GetFont(fontname, this.defaultEncoding, this.defaultEmbedding, size, style, null);
		}

		// Token: 0x06002B65 RID: 11109 RVA: 0x00106C79 File Offset: 0x00105C79
		public virtual Font GetFont(string fontname, float size)
		{
			return this.GetFont(fontname, this.defaultEncoding, this.defaultEmbedding, size, -1, null);
		}

		// Token: 0x06002B66 RID: 11110 RVA: 0x00106C91 File Offset: 0x00105C91
		public virtual Font GetFont(string fontname)
		{
			return this.GetFont(fontname, this.defaultEncoding, this.defaultEmbedding, -1f, -1, null);
		}

		// Token: 0x06002B67 RID: 11111 RVA: 0x00106CB0 File Offset: 0x00105CB0
		public virtual void Register(Properties attributes)
		{
			string path = attributes.Remove("path");
			string alias = attributes.Remove("alias");
			this.Register(path, alias);
		}

		// Token: 0x06002B68 RID: 11112 RVA: 0x00106CE0 File Offset: 0x00105CE0
		public void RegisterFamily(string familyName, string fullName, string path)
		{
			if (path != null)
			{
				this.trueTypeFonts[fullName] = path;
			}
			List<string> list;
			this.fontFamilies.TryGetValue(familyName, out list);
			if (list == null)
			{
				list = new List<string>();
				list.Add(fullName);
				this.fontFamilies[familyName] = list;
				return;
			}
			int length = fullName.Length;
			bool flag = false;
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].Length >= length)
				{
					list.Insert(i, fullName);
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				list.Add(fullName);
			}
		}

		// Token: 0x06002B69 RID: 11113 RVA: 0x00106D69 File Offset: 0x00105D69
		public virtual void Register(string path)
		{
			this.Register(path, null);
		}

		// Token: 0x06002B6A RID: 11114 RVA: 0x00106D74 File Offset: 0x00105D74
		public virtual void Register(string path, string alias)
		{
			try
			{
				if (path.ToLower(CultureInfo.InvariantCulture).EndsWith(".ttf") || path.ToLower(CultureInfo.InvariantCulture).EndsWith(".otf") || path.ToLower(CultureInfo.InvariantCulture).IndexOf(".ttc,") > 0)
				{
					object[] allFontNames = BaseFont.GetAllFontNames(path, "Cp1252", null);
					this.trueTypeFonts[((string)allFontNames[0]).ToLower(CultureInfo.InvariantCulture)] = path;
					if (alias != null)
					{
						this.trueTypeFonts[alias.ToLower(CultureInfo.InvariantCulture)] = path;
					}
					string[][] array = (string[][])allFontNames[2];
					for (int i = 0; i < array.Length; i++)
					{
						this.trueTypeFonts[array[i][3].ToLower(CultureInfo.InvariantCulture)] = path;
					}
					string text = null;
					array = (string[][])allFontNames[1];
					for (int j = 0; j < FontFactoryImp.TTFamilyOrder.Length; j += 3)
					{
						foreach (string[] array3 in array)
						{
							if (FontFactoryImp.TTFamilyOrder[j].Equals(array3[0]) && FontFactoryImp.TTFamilyOrder[j + 1].Equals(array3[1]) && FontFactoryImp.TTFamilyOrder[j + 2].Equals(array3[2]))
							{
								text = array3[3].ToLower(CultureInfo.InvariantCulture);
								j = FontFactoryImp.TTFamilyOrder.Length;
								break;
							}
						}
					}
					if (text != null)
					{
						string value = "";
						array = (string[][])allFontNames[2];
						foreach (string[] array5 in array)
						{
							for (int m = 0; m < FontFactoryImp.TTFamilyOrder.Length; m += 3)
							{
								if (FontFactoryImp.TTFamilyOrder[m].Equals(array5[0]) && FontFactoryImp.TTFamilyOrder[m + 1].Equals(array5[1]) && FontFactoryImp.TTFamilyOrder[m + 2].Equals(array5[2]))
								{
									string text2 = array5[3];
									if (!text2.Equals(value))
									{
										value = text2;
										this.RegisterFamily(text, text2, null);
										break;
									}
								}
							}
						}
					}
				}
				else if (path.ToLower(CultureInfo.InvariantCulture).EndsWith(".ttc"))
				{
					string[] array6 = BaseFont.EnumerateTTCNames(path);
					for (int n = 0; n < array6.Length; n++)
					{
						this.Register(path + "," + n);
					}
				}
				else if (path.ToLower(CultureInfo.InvariantCulture).EndsWith(".afm") || path.ToLower(CultureInfo.InvariantCulture).EndsWith(".pfm"))
				{
					BaseFont baseFont = BaseFont.CreateFont(path, "Cp1252", false);
					string text3 = baseFont.FullFontName[0][3].ToLower(CultureInfo.InvariantCulture);
					string familyName = baseFont.FamilyFontName[0][3].ToLower(CultureInfo.InvariantCulture);
					string key = baseFont.PostscriptFontName.ToLower(CultureInfo.InvariantCulture);
					this.RegisterFamily(familyName, text3, null);
					this.trueTypeFonts[key] = path;
					this.trueTypeFonts[text3] = path;
				}
			}
			catch (DocumentException ex)
			{
				throw ex;
			}
			catch (IOException ex2)
			{
				throw ex2;
			}
		}

		// Token: 0x06002B6B RID: 11115 RVA: 0x001070C0 File Offset: 0x001060C0
		public virtual int RegisterDirectory(string dir)
		{
			return this.RegisterDirectory(dir, false);
		}

		// Token: 0x06002B6C RID: 11116 RVA: 0x001070CC File Offset: 0x001060CC
		public int RegisterDirectory(string dir, bool scanSubdirectories)
		{
			int num = 0;
			try
			{
				if (!Directory.Exists(dir))
				{
					return 0;
				}
				string[] files = Directory.GetFiles(dir);
				if (files == null)
				{
					return 0;
				}
				for (int i = 0; i < files.Length; i++)
				{
					try
					{
						if (Directory.Exists(files[i]))
						{
							if (scanSubdirectories)
							{
								num += this.RegisterDirectory(Path.GetFullPath(files[i]), true);
							}
						}
						else
						{
							string fullPath = Path.GetFullPath(files[i]);
							string value = (fullPath.Length < 4) ? null : fullPath.Substring(fullPath.Length - 4).ToLower(CultureInfo.InvariantCulture);
							if (".afm".Equals(value) || ".pfm".Equals(value))
							{
								string path = fullPath.Substring(0, fullPath.Length - 4) + ".pfb";
								if (File.Exists(path))
								{
									this.Register(fullPath, null);
									num++;
								}
							}
							else if (".ttf".Equals(value) || ".otf".Equals(value) || ".ttc".Equals(value))
							{
								this.Register(fullPath, null);
								num++;
							}
						}
					}
					catch
					{
					}
				}
			}
			catch
			{
			}
			return num;
		}

		// Token: 0x06002B6D RID: 11117 RVA: 0x00107230 File Offset: 0x00106230
		public virtual int RegisterDirectories()
		{
			string dir = Path.Combine(Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.System)), "Fonts");
			return this.RegisterDirectory(dir);
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06002B6E RID: 11118 RVA: 0x0010725B File Offset: 0x0010625B
		public virtual ICollection<string> RegisteredFonts
		{
			get
			{
				return this.trueTypeFonts.Keys;
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06002B6F RID: 11119 RVA: 0x00107268 File Offset: 0x00106268
		public virtual ICollection<string> RegisteredFamilies
		{
			get
			{
				return this.fontFamilies.Keys;
			}
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x00107275 File Offset: 0x00106275
		public virtual bool IsRegistered(string fontname)
		{
			return this.trueTypeFonts.ContainsKey(fontname.ToLower(CultureInfo.InvariantCulture));
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06002B71 RID: 11121 RVA: 0x0010728D File Offset: 0x0010628D
		// (set) Token: 0x06002B72 RID: 11122 RVA: 0x00107295 File Offset: 0x00106295
		public virtual string DefaultEncoding
		{
			get
			{
				return this.defaultEncoding;
			}
			set
			{
				this.defaultEncoding = value;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06002B73 RID: 11123 RVA: 0x0010729E File Offset: 0x0010629E
		// (set) Token: 0x06002B74 RID: 11124 RVA: 0x001072A6 File Offset: 0x001062A6
		public virtual bool DefaultEmbedding
		{
			get
			{
				return this.defaultEmbedding;
			}
			set
			{
				this.defaultEmbedding = value;
			}
		}

		// Token: 0x04001E23 RID: 7715
		private Dictionary<string, string> trueTypeFonts = new Dictionary<string, string>();

		// Token: 0x04001E24 RID: 7716
		private static string[] TTFamilyOrder = new string[]
		{
			"3",
			"1",
			"1033",
			"3",
			"0",
			"1033",
			"1",
			"0",
			"0",
			"0",
			"3",
			"0"
		};

		// Token: 0x04001E25 RID: 7717
		private Dictionary<string, List<string>> fontFamilies = new Dictionary<string, List<string>>();

		// Token: 0x04001E26 RID: 7718
		private string defaultEncoding = "Cp1252";

		// Token: 0x04001E27 RID: 7719
		private bool defaultEmbedding;
	}
}
