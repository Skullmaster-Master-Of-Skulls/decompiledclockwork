using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x020000E7 RID: 231
	internal class TrueTypeFont : BaseFont
	{
		// Token: 0x060008AB RID: 2219 RVA: 0x0002D23D File Offset: 0x0002C23D
		protected TrueTypeFont()
		{
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0002D27C File Offset: 0x0002C27C
		internal TrueTypeFont(string ttFile, string enc, bool emb, byte[] ttfAfm, bool justNames, bool forceRead)
		{
			this.justNames = justNames;
			string baseName = iTextSharp.text.pdf.BaseFont.GetBaseName(ttFile);
			string ttcname = TrueTypeFont.GetTTCName(baseName);
			if (baseName.Length < ttFile.Length)
			{
				this.style = ttFile.Substring(baseName.Length);
			}
			this.encoding = enc;
			this.embedded = emb;
			this.fileName = ttcname;
			base.FontType = 1;
			this.ttcIndex = "";
			if (ttcname.Length < baseName.Length)
			{
				this.ttcIndex = baseName.Substring(ttcname.Length + 1);
			}
			if (!this.fileName.ToLower(CultureInfo.InvariantCulture).EndsWith(".ttf") && !this.fileName.ToLower(CultureInfo.InvariantCulture).EndsWith(".otf") && !this.fileName.ToLower(CultureInfo.InvariantCulture).EndsWith(".ttc"))
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("1.is.not.a.ttf.otf.or.ttc.font.file", this.fileName + this.style));
			}
			this.Process(ttfAfm, forceRead);
			if (!justNames && this.embedded && this.os_2.fsType == 2)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("1.cannot.be.embedded.due.to.licensing.restrictions", this.fileName + this.style));
			}
			if (!this.encoding.StartsWith("#"))
			{
				PdfEncodings.ConvertToBytes(" ", enc);
			}
			base.CreateEncoding();
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0002D424 File Offset: 0x0002C424
		protected static string GetTTCName(string name)
		{
			int num = name.ToLower(CultureInfo.InvariantCulture).IndexOf(".ttc,");
			if (num < 0)
			{
				return name;
			}
			return name.Substring(0, num + 4);
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0002D458 File Offset: 0x0002C458
		internal void FillTables()
		{
			int[] array;
			this.tables.TryGetValue("head", out array);
			if (array == null)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("table.1.does.not.exist.in.2", "head", this.fileName + this.style));
			}
			this.rf.Seek(array[0] + 16);
			this.head.flags = this.rf.ReadUnsignedShort();
			this.head.unitsPerEm = this.rf.ReadUnsignedShort();
			this.rf.SkipBytes(16);
			this.head.xMin = this.rf.ReadShort();
			this.head.yMin = this.rf.ReadShort();
			this.head.xMax = this.rf.ReadShort();
			this.head.yMax = this.rf.ReadShort();
			this.head.macStyle = this.rf.ReadUnsignedShort();
			this.tables.TryGetValue("hhea", out array);
			if (array == null)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("table.1.does.not.exist.in.2", "hhea", this.fileName + this.style));
			}
			this.rf.Seek(array[0] + 4);
			this.hhea.Ascender = this.rf.ReadShort();
			this.hhea.Descender = this.rf.ReadShort();
			this.hhea.LineGap = this.rf.ReadShort();
			this.hhea.advanceWidthMax = this.rf.ReadUnsignedShort();
			this.hhea.minLeftSideBearing = this.rf.ReadShort();
			this.hhea.minRightSideBearing = this.rf.ReadShort();
			this.hhea.xMaxExtent = this.rf.ReadShort();
			this.hhea.caretSlopeRise = this.rf.ReadShort();
			this.hhea.caretSlopeRun = this.rf.ReadShort();
			this.rf.SkipBytes(12);
			this.hhea.numberOfHMetrics = this.rf.ReadUnsignedShort();
			this.tables.TryGetValue("OS/2", out array);
			if (array == null)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("table.1.does.not.exist.in.2", "OS/2", this.fileName + this.style));
			}
			this.rf.Seek(array[0]);
			int num = this.rf.ReadUnsignedShort();
			this.os_2.xAvgCharWidth = this.rf.ReadShort();
			this.os_2.usWeightClass = this.rf.ReadUnsignedShort();
			this.os_2.usWidthClass = this.rf.ReadUnsignedShort();
			this.os_2.fsType = this.rf.ReadShort();
			this.os_2.ySubscriptXSize = this.rf.ReadShort();
			this.os_2.ySubscriptYSize = this.rf.ReadShort();
			this.os_2.ySubscriptXOffset = this.rf.ReadShort();
			this.os_2.ySubscriptYOffset = this.rf.ReadShort();
			this.os_2.ySuperscriptXSize = this.rf.ReadShort();
			this.os_2.ySuperscriptYSize = this.rf.ReadShort();
			this.os_2.ySuperscriptXOffset = this.rf.ReadShort();
			this.os_2.ySuperscriptYOffset = this.rf.ReadShort();
			this.os_2.yStrikeoutSize = this.rf.ReadShort();
			this.os_2.yStrikeoutPosition = this.rf.ReadShort();
			this.os_2.sFamilyClass = this.rf.ReadShort();
			this.rf.ReadFully(this.os_2.panose);
			this.rf.SkipBytes(16);
			this.rf.ReadFully(this.os_2.achVendID);
			this.os_2.fsSelection = this.rf.ReadUnsignedShort();
			this.os_2.usFirstCharIndex = this.rf.ReadUnsignedShort();
			this.os_2.usLastCharIndex = this.rf.ReadUnsignedShort();
			this.os_2.sTypoAscender = this.rf.ReadShort();
			this.os_2.sTypoDescender = this.rf.ReadShort();
			if (this.os_2.sTypoDescender > 0)
			{
				this.os_2.sTypoDescender = -this.os_2.sTypoDescender;
			}
			this.os_2.sTypoLineGap = this.rf.ReadShort();
			this.os_2.usWinAscent = this.rf.ReadUnsignedShort();
			this.os_2.usWinDescent = this.rf.ReadUnsignedShort();
			this.os_2.ulCodePageRange1 = 0;
			this.os_2.ulCodePageRange2 = 0;
			if (num > 0)
			{
				this.os_2.ulCodePageRange1 = this.rf.ReadInt();
				this.os_2.ulCodePageRange2 = this.rf.ReadInt();
			}
			if (num > 1)
			{
				this.rf.SkipBytes(2);
				this.os_2.sCapHeight = (int)this.rf.ReadShort();
			}
			else
			{
				this.os_2.sCapHeight = (int)(0.7 * (double)this.head.unitsPerEm);
			}
			this.tables.TryGetValue("post", out array);
			if (array == null)
			{
				this.italicAngle = -Math.Atan2((double)this.hhea.caretSlopeRun, (double)this.hhea.caretSlopeRise) * 180.0 / 3.141592653589793;
				return;
			}
			this.rf.Seek(array[0] + 4);
			short num2 = this.rf.ReadShort();
			int num3 = this.rf.ReadUnsignedShort();
			this.italicAngle = (double)num2 + (double)num3 / 16384.0;
			this.underlinePosition = (int)this.rf.ReadShort();
			this.underlineThickness = (int)this.rf.ReadShort();
			this.isFixedPitch = (this.rf.ReadInt() != 0);
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x0002DA98 File Offset: 0x0002CA98
		internal string BaseFont
		{
			get
			{
				int[] array;
				this.tables.TryGetValue("name", out array);
				if (array == null)
				{
					throw new DocumentException(MessageLocalization.GetComposedMessage("table.1.does.not.exist.in.2", "name", this.fileName + this.style));
				}
				this.rf.Seek(array[0] + 2);
				int num = this.rf.ReadUnsignedShort();
				int num2 = this.rf.ReadUnsignedShort();
				int i = 0;
				while (i < num)
				{
					int num3 = this.rf.ReadUnsignedShort();
					this.rf.ReadUnsignedShort();
					this.rf.ReadUnsignedShort();
					int num4 = this.rf.ReadUnsignedShort();
					int length = this.rf.ReadUnsignedShort();
					int num5 = this.rf.ReadUnsignedShort();
					if (num4 == 6)
					{
						this.rf.Seek(array[0] + num2 + num5);
						if (num3 == 0 || num3 == 3)
						{
							return this.ReadUnicodeString(length);
						}
						return this.ReadStandardString(length);
					}
					else
					{
						i++;
					}
				}
				FileInfo fileInfo = new FileInfo(this.fileName);
				return fileInfo.Name.Replace(' ', '-');
			}
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0002DBB8 File Offset: 0x0002CBB8
		internal string[][] GetNames(int id)
		{
			int[] array;
			this.tables.TryGetValue("name", out array);
			if (array == null)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("table.1.does.not.exist.in.2", "name", this.fileName + this.style));
			}
			this.rf.Seek(array[0] + 2);
			int num = this.rf.ReadUnsignedShort();
			int num2 = this.rf.ReadUnsignedShort();
			List<string[]> list = new List<string[]>();
			for (int i = 0; i < num; i++)
			{
				int num3 = this.rf.ReadUnsignedShort();
				int num4 = this.rf.ReadUnsignedShort();
				int num5 = this.rf.ReadUnsignedShort();
				int num6 = this.rf.ReadUnsignedShort();
				int length = this.rf.ReadUnsignedShort();
				int num7 = this.rf.ReadUnsignedShort();
				if (num6 == id)
				{
					int filePointer = this.rf.FilePointer;
					this.rf.Seek(array[0] + num2 + num7);
					string text;
					if (num3 == 0 || num3 == 3 || (num3 == 2 && num4 == 1))
					{
						text = this.ReadUnicodeString(length);
					}
					else
					{
						text = this.ReadStandardString(length);
					}
					list.Add(new string[]
					{
						num3.ToString(),
						num4.ToString(),
						num5.ToString(),
						text
					});
					this.rf.Seek(filePointer);
				}
			}
			string[][] array2 = new string[list.Count][];
			for (int j = 0; j < list.Count; j++)
			{
				array2[j] = list[j];
			}
			return array2;
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0002DD5C File Offset: 0x0002CD5C
		internal string[][] GetAllNames()
		{
			int[] array;
			this.tables.TryGetValue("name", out array);
			if (array == null)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("table.1.does.not.exist.in.2", "name", this.fileName + this.style));
			}
			this.rf.Seek(array[0] + 2);
			int num = this.rf.ReadUnsignedShort();
			int num2 = this.rf.ReadUnsignedShort();
			List<string[]> list = new List<string[]>();
			for (int i = 0; i < num; i++)
			{
				int num3 = this.rf.ReadUnsignedShort();
				int num4 = this.rf.ReadUnsignedShort();
				int num5 = this.rf.ReadUnsignedShort();
				int num6 = this.rf.ReadUnsignedShort();
				int length = this.rf.ReadUnsignedShort();
				int num7 = this.rf.ReadUnsignedShort();
				int filePointer = this.rf.FilePointer;
				this.rf.Seek(array[0] + num2 + num7);
				string text;
				if (num3 == 0 || num3 == 3 || (num3 == 2 && num4 == 1))
				{
					text = this.ReadUnicodeString(length);
				}
				else
				{
					text = this.ReadStandardString(length);
				}
				list.Add(new string[]
				{
					num6.ToString(),
					num3.ToString(),
					num4.ToString(),
					num5.ToString(),
					text
				});
				this.rf.Seek(filePointer);
			}
			string[][] array2 = new string[list.Count][];
			for (int j = 0; j < list.Count; j++)
			{
				array2[j] = list[j];
			}
			return array2;
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0002DF00 File Offset: 0x0002CF00
		internal void CheckCff()
		{
			int[] array;
			this.tables.TryGetValue("CFF ", out array);
			if (array != null)
			{
				this.cff = true;
				this.cffOffset = array[0];
				this.cffLength = array[1];
			}
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0002DF3C File Offset: 0x0002CF3C
		internal void Process(byte[] ttfAfm, bool preload)
		{
			this.tables = new Dictionary<string, int[]>();
			try
			{
				if (ttfAfm == null)
				{
					this.rf = new RandomAccessFileOrArray(this.fileName, preload);
				}
				else
				{
					this.rf = new RandomAccessFileOrArray(ttfAfm);
				}
				if (this.ttcIndex.Length > 0)
				{
					int num = int.Parse(this.ttcIndex);
					if (num < 0)
					{
						throw new DocumentException(MessageLocalization.GetComposedMessage("the.font.index.for.1.must.be.positive", this.fileName));
					}
					string text = this.ReadStandardString(4);
					if (!text.Equals("ttcf"))
					{
						throw new DocumentException(MessageLocalization.GetComposedMessage("1.is.not.a.valid.ttc.file", this.fileName));
					}
					this.rf.SkipBytes(4);
					int num2 = this.rf.ReadInt();
					if (num >= num2)
					{
						throw new DocumentException(MessageLocalization.GetComposedMessage("the.font.index.for.1.must.be.between.0.and.2.it.was.3", this.fileName, num2 - 1, num));
					}
					this.rf.SkipBytes(num * 4);
					this.directoryOffset = this.rf.ReadInt();
				}
				this.rf.Seek(this.directoryOffset);
				int num3 = this.rf.ReadInt();
				if (num3 != 65536 && num3 != 1330926671)
				{
					throw new DocumentException(MessageLocalization.GetComposedMessage("1.is.not.a.valid.ttf.or.otf.file", this.fileName));
				}
				int num4 = this.rf.ReadUnsignedShort();
				this.rf.SkipBytes(6);
				for (int i = 0; i < num4; i++)
				{
					string key = this.ReadStandardString(4);
					this.rf.SkipBytes(4);
					int[] value = new int[]
					{
						this.rf.ReadInt(),
						this.rf.ReadInt()
					};
					this.tables[key] = value;
				}
				this.CheckCff();
				this.fontName = this.BaseFont;
				this.fullName = this.GetNames(4);
				this.familyName = this.GetNames(1);
				this.allNameEntries = this.GetAllNames();
				if (!this.justNames)
				{
					this.FillTables();
					this.ReadGlyphWidths();
					this.ReadCMaps();
					this.ReadKerning();
					this.ReadBbox();
					this.GlyphWidths = null;
				}
			}
			finally
			{
				if (this.rf != null)
				{
					this.rf.Close();
					if (!this.embedded)
					{
						this.rf = null;
					}
				}
			}
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0002E19C File Offset: 0x0002D19C
		protected string ReadStandardString(int length)
		{
			byte[] array = new byte[length];
			this.rf.ReadFully(array);
			return System.Text.Encoding.GetEncoding(1252).GetString(array);
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0002E1CC File Offset: 0x0002D1CC
		protected string ReadUnicodeString(int length)
		{
			StringBuilder stringBuilder = new StringBuilder();
			length /= 2;
			for (int i = 0; i < length; i++)
			{
				stringBuilder.Append(this.rf.ReadChar());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0002E208 File Offset: 0x0002D208
		protected void ReadGlyphWidths()
		{
			int[] array;
			this.tables.TryGetValue("hmtx", out array);
			if (array == null)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("table.1.does.not.exist.in.2", "hmtx", this.fileName + this.style));
			}
			this.rf.Seek(array[0]);
			this.GlyphWidths = new int[this.hhea.numberOfHMetrics];
			for (int i = 0; i < this.hhea.numberOfHMetrics; i++)
			{
				this.GlyphWidths[i] = this.rf.ReadUnsignedShort() * 1000 / this.head.unitsPerEm;
				this.rf.ReadUnsignedShort();
			}
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0002E2BC File Offset: 0x0002D2BC
		protected int GetGlyphWidth(int glyph)
		{
			if (glyph >= this.GlyphWidths.Length)
			{
				glyph = this.GlyphWidths.Length - 1;
			}
			return this.GlyphWidths[glyph];
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0002E2E0 File Offset: 0x0002D2E0
		private void ReadBbox()
		{
			int[] array;
			this.tables.TryGetValue("head", out array);
			if (array == null)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("table.1.does.not.exist.in.2", "head", this.fileName + this.style));
			}
			this.rf.Seek(array[0] + TrueTypeFontSubSet.HEAD_LOCA_FORMAT_OFFSET);
			bool flag = this.rf.ReadUnsignedShort() == 0;
			this.tables.TryGetValue("loca", out array);
			if (array == null)
			{
				return;
			}
			this.rf.Seek(array[0]);
			int[] array2;
			if (flag)
			{
				int num = array[1] / 2;
				array2 = new int[num];
				for (int i = 0; i < num; i++)
				{
					array2[i] = this.rf.ReadUnsignedShort() * 2;
				}
			}
			else
			{
				int num2 = array[1] / 4;
				array2 = new int[num2];
				for (int j = 0; j < num2; j++)
				{
					array2[j] = this.rf.ReadInt();
				}
			}
			this.tables.TryGetValue("glyf", out array);
			if (array == null)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("table.1.does.not.exist.in.2", "glyf", this.fileName + this.style));
			}
			int num3 = array[0];
			this.bboxes = new int[array2.Length - 1][];
			for (int k = 0; k < array2.Length - 1; k++)
			{
				int num4 = array2[k];
				if (num4 != array2[k + 1])
				{
					this.rf.Seek(num3 + num4 + 2);
					this.bboxes[k] = new int[]
					{
						(int)(this.rf.ReadShort() * 1000) / this.head.unitsPerEm,
						(int)(this.rf.ReadShort() * 1000) / this.head.unitsPerEm,
						(int)(this.rf.ReadShort() * 1000) / this.head.unitsPerEm,
						(int)(this.rf.ReadShort() * 1000) / this.head.unitsPerEm
					};
				}
			}
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0002E4FC File Offset: 0x0002D4FC
		internal void ReadCMaps()
		{
			int[] array;
			this.tables.TryGetValue("cmap", out array);
			if (array == null)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("table.1.does.not.exist.in.2", "cmap", this.fileName + this.style));
			}
			this.rf.Seek(array[0]);
			this.rf.SkipBytes(2);
			int num = this.rf.ReadUnsignedShort();
			this.fontSpecific = false;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			for (int i = 0; i < num; i++)
			{
				int num6 = this.rf.ReadUnsignedShort();
				int num7 = this.rf.ReadUnsignedShort();
				int num8 = this.rf.ReadInt();
				if (num6 == 3 && num7 == 0)
				{
					this.fontSpecific = true;
					num4 = num8;
				}
				else if (num6 == 3 && num7 == 1)
				{
					num3 = num8;
				}
				else if (num6 == 3 && num7 == 10)
				{
					num5 = num8;
				}
				if (num6 == 1 && num7 == 0)
				{
					num2 = num8;
				}
			}
			if (num2 > 0)
			{
				this.rf.Seek(array[0] + num2);
				int num9 = this.rf.ReadUnsignedShort();
				int num10 = num9;
				if (num10 != 0)
				{
					switch (num10)
					{
					case 4:
						this.cmap10 = this.ReadFormat4();
						break;
					case 6:
						this.cmap10 = this.ReadFormat6();
						break;
					}
				}
				else
				{
					this.cmap10 = this.ReadFormat0();
				}
			}
			if (num3 > 0)
			{
				this.rf.Seek(array[0] + num3);
				int num11 = this.rf.ReadUnsignedShort();
				if (num11 == 4)
				{
					this.cmap31 = this.ReadFormat4();
				}
			}
			if (num4 > 0)
			{
				this.rf.Seek(array[0] + num4);
				int num12 = this.rf.ReadUnsignedShort();
				if (num12 == 4)
				{
					this.cmap10 = this.ReadFormat4();
				}
			}
			if (num5 > 0)
			{
				this.rf.Seek(array[0] + num5);
				int num13 = this.rf.ReadUnsignedShort();
				int num14 = num13;
				if (num14 == 0)
				{
					this.cmapExt = this.ReadFormat0();
					return;
				}
				switch (num14)
				{
				case 4:
					this.cmapExt = this.ReadFormat4();
					return;
				case 5:
					break;
				case 6:
					this.cmapExt = this.ReadFormat6();
					return;
				default:
					if (num14 != 12)
					{
						return;
					}
					this.cmapExt = this.ReadFormat12();
					break;
				}
			}
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0002E740 File Offset: 0x0002D740
		internal Dictionary<int, int[]> ReadFormat12()
		{
			Dictionary<int, int[]> dictionary = new Dictionary<int, int[]>();
			this.rf.SkipBytes(2);
			this.rf.ReadInt();
			this.rf.SkipBytes(4);
			int num = this.rf.ReadInt();
			for (int i = 0; i < num; i++)
			{
				int num2 = this.rf.ReadInt();
				int num3 = this.rf.ReadInt();
				int num4 = this.rf.ReadInt();
				for (int j = num2; j <= num3; j++)
				{
					int[] array = new int[2];
					array[0] = num4;
					array[1] = this.GetGlyphWidth(array[0]);
					dictionary[j] = array;
					num4++;
				}
			}
			return dictionary;
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0002E7F8 File Offset: 0x0002D7F8
		internal Dictionary<int, int[]> ReadFormat0()
		{
			Dictionary<int, int[]> dictionary = new Dictionary<int, int[]>();
			this.rf.SkipBytes(4);
			for (int i = 0; i < 256; i++)
			{
				int[] array = new int[2];
				array[0] = this.rf.ReadUnsignedByte();
				array[1] = this.GetGlyphWidth(array[0]);
				dictionary[i] = array;
			}
			return dictionary;
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0002E854 File Offset: 0x0002D854
		internal Dictionary<int, int[]> ReadFormat4()
		{
			Dictionary<int, int[]> dictionary = new Dictionary<int, int[]>();
			int num = this.rf.ReadUnsignedShort();
			this.rf.SkipBytes(2);
			int num2 = this.rf.ReadUnsignedShort() / 2;
			this.rf.SkipBytes(6);
			int[] array = new int[num2];
			for (int i = 0; i < num2; i++)
			{
				array[i] = this.rf.ReadUnsignedShort();
			}
			this.rf.SkipBytes(2);
			int[] array2 = new int[num2];
			for (int j = 0; j < num2; j++)
			{
				array2[j] = this.rf.ReadUnsignedShort();
			}
			int[] array3 = new int[num2];
			for (int k = 0; k < num2; k++)
			{
				array3[k] = this.rf.ReadUnsignedShort();
			}
			int[] array4 = new int[num2];
			for (int l = 0; l < num2; l++)
			{
				array4[l] = this.rf.ReadUnsignedShort();
			}
			int[] array5 = new int[num / 2 - 8 - num2 * 4];
			for (int m = 0; m < array5.Length; m++)
			{
				array5[m] = this.rf.ReadUnsignedShort();
			}
			for (int n = 0; n < num2; n++)
			{
				int num3 = array2[n];
				while (num3 <= array[n] && num3 != 65535)
				{
					int num4;
					if (array4[n] == 0)
					{
						num4 = (num3 + array3[n] & 65535);
						goto IL_177;
					}
					int num5 = n + array4[n] / 2 - num2 + num3 - array2[n];
					if (num5 < array5.Length)
					{
						num4 = (array5[num5] + array3[n] & 65535);
						goto IL_177;
					}
					IL_1C2:
					num3++;
					continue;
					IL_177:
					int[] array6 = new int[2];
					array6[0] = num4;
					array6[1] = this.GetGlyphWidth(array6[0]);
					dictionary[this.fontSpecific ? (((num3 & 65280) == 61440) ? (num3 & 255) : num3) : num3] = array6;
					goto IL_1C2;
				}
			}
			return dictionary;
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0002EA4C File Offset: 0x0002DA4C
		internal Dictionary<int, int[]> ReadFormat6()
		{
			Dictionary<int, int[]> dictionary = new Dictionary<int, int[]>();
			this.rf.SkipBytes(4);
			int num = this.rf.ReadUnsignedShort();
			int num2 = this.rf.ReadUnsignedShort();
			for (int i = 0; i < num2; i++)
			{
				int[] array = new int[2];
				array[0] = this.rf.ReadUnsignedShort();
				array[1] = this.GetGlyphWidth(array[0]);
				dictionary[i + num] = array;
			}
			return dictionary;
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0002EAC4 File Offset: 0x0002DAC4
		internal void ReadKerning()
		{
			int[] array;
			this.tables.TryGetValue("kern", out array);
			if (array == null)
			{
				return;
			}
			this.rf.Seek(array[0] + 2);
			int num = this.rf.ReadUnsignedShort();
			int num2 = array[0] + 4;
			int num3 = 0;
			for (int i = 0; i < num; i++)
			{
				num2 += num3;
				this.rf.Seek(num2);
				this.rf.SkipBytes(2);
				num3 = this.rf.ReadUnsignedShort();
				int num4 = this.rf.ReadUnsignedShort();
				if ((num4 & 65527) == 1)
				{
					int num5 = this.rf.ReadUnsignedShort();
					this.rf.SkipBytes(6);
					for (int j = 0; j < num5; j++)
					{
						int key = this.rf.ReadInt();
						int value = (int)(this.rf.ReadShort() * 1000) / this.head.unitsPerEm;
						this.kerning[key] = value;
					}
				}
			}
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0002EBCC File Offset: 0x0002DBCC
		public override int GetKerning(int char1, int char2)
		{
			int[] metricsTT = this.GetMetricsTT(char1);
			if (metricsTT == null)
			{
				return 0;
			}
			int num = metricsTT[0];
			metricsTT = this.GetMetricsTT(char2);
			if (metricsTT == null)
			{
				return 0;
			}
			int num2 = metricsTT[0];
			return this.kerning[(num << 16) + num2];
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0002EC0C File Offset: 0x0002DC0C
		internal override int GetRawWidth(int c, string name)
		{
			int[] metricsTT = this.GetMetricsTT(c);
			if (metricsTT == null)
			{
				return 0;
			}
			return metricsTT[1];
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0002EC2C File Offset: 0x0002DC2C
		protected PdfDictionary GetFontDescriptor(PdfIndirectReference fontStream, string subsetPrefix, PdfIndirectReference cidset)
		{
			PdfDictionary pdfDictionary = new PdfDictionary(PdfName.FONTDESCRIPTOR);
			pdfDictionary.Put(PdfName.ASCENT, new PdfNumber((int)(this.os_2.sTypoAscender * 1000) / this.head.unitsPerEm));
			pdfDictionary.Put(PdfName.CAPHEIGHT, new PdfNumber(this.os_2.sCapHeight * 1000 / this.head.unitsPerEm));
			pdfDictionary.Put(PdfName.DESCENT, new PdfNumber((int)(this.os_2.sTypoDescender * 1000) / this.head.unitsPerEm));
			pdfDictionary.Put(PdfName.FONTBBOX, new PdfRectangle((float)((int)(this.head.xMin * 1000) / this.head.unitsPerEm), (float)((int)(this.head.yMin * 1000) / this.head.unitsPerEm), (float)((int)(this.head.xMax * 1000) / this.head.unitsPerEm), (float)((int)(this.head.yMax * 1000) / this.head.unitsPerEm)));
			if (cidset != null)
			{
				pdfDictionary.Put(PdfName.CIDSET, cidset);
			}
			if (this.cff)
			{
				if (this.encoding.StartsWith("Identity-"))
				{
					pdfDictionary.Put(PdfName.FONTNAME, new PdfName(subsetPrefix + this.fontName + "-" + this.encoding));
				}
				else
				{
					pdfDictionary.Put(PdfName.FONTNAME, new PdfName(subsetPrefix + this.fontName + this.style));
				}
			}
			else
			{
				pdfDictionary.Put(PdfName.FONTNAME, new PdfName(subsetPrefix + this.fontName + this.style));
			}
			pdfDictionary.Put(PdfName.ITALICANGLE, new PdfNumber(this.italicAngle));
			pdfDictionary.Put(PdfName.STEMV, new PdfNumber(80));
			if (fontStream != null)
			{
				if (this.cff)
				{
					pdfDictionary.Put(PdfName.FONTFILE3, fontStream);
				}
				else
				{
					pdfDictionary.Put(PdfName.FONTFILE2, fontStream);
				}
			}
			int num = 0;
			if (this.isFixedPitch)
			{
				num |= 1;
			}
			num |= (this.fontSpecific ? 4 : 32);
			if ((this.head.macStyle & 2) != 0)
			{
				num |= 64;
			}
			if ((this.head.macStyle & 1) != 0)
			{
				num |= 262144;
			}
			pdfDictionary.Put(PdfName.FLAGS, new PdfNumber(num));
			return pdfDictionary;
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0002EE94 File Offset: 0x0002DE94
		protected PdfDictionary GetFontBaseType(PdfIndirectReference fontDescriptor, string subsetPrefix, int firstChar, int lastChar, byte[] shortTag)
		{
			PdfDictionary pdfDictionary = new PdfDictionary(PdfName.FONT);
			if (this.cff)
			{
				pdfDictionary.Put(PdfName.SUBTYPE, PdfName.TYPE1);
				pdfDictionary.Put(PdfName.BASEFONT, new PdfName(this.fontName + this.style));
			}
			else
			{
				pdfDictionary.Put(PdfName.SUBTYPE, PdfName.TRUETYPE);
				pdfDictionary.Put(PdfName.BASEFONT, new PdfName(subsetPrefix + this.fontName + this.style));
			}
			pdfDictionary.Put(PdfName.BASEFONT, new PdfName(subsetPrefix + this.fontName + this.style));
			if (!this.fontSpecific)
			{
				for (int i = firstChar; i <= lastChar; i++)
				{
					if (!this.differences[i].Equals(".notdef"))
					{
						firstChar = i;
						break;
					}
				}
				if (this.encoding.Equals("Cp1252") || this.encoding.Equals("MacRoman"))
				{
					pdfDictionary.Put(PdfName.ENCODING, this.encoding.Equals("Cp1252") ? PdfName.WIN_ANSI_ENCODING : PdfName.MAC_ROMAN_ENCODING);
				}
				else
				{
					PdfDictionary pdfDictionary2 = new PdfDictionary(PdfName.ENCODING);
					PdfArray pdfArray = new PdfArray();
					bool flag = true;
					for (int j = firstChar; j <= lastChar; j++)
					{
						if (shortTag[j] != 0)
						{
							if (flag)
							{
								pdfArray.Add(new PdfNumber(j));
								flag = false;
							}
							pdfArray.Add(new PdfName(this.differences[j]));
						}
						else
						{
							flag = true;
						}
					}
					pdfDictionary2.Put(PdfName.DIFFERENCES, pdfArray);
					pdfDictionary.Put(PdfName.ENCODING, pdfDictionary2);
				}
			}
			pdfDictionary.Put(PdfName.FIRSTCHAR, new PdfNumber(firstChar));
			pdfDictionary.Put(PdfName.LASTCHAR, new PdfNumber(lastChar));
			PdfArray pdfArray2 = new PdfArray();
			for (int k = firstChar; k <= lastChar; k++)
			{
				if (shortTag[k] == 0)
				{
					pdfArray2.Add(new PdfNumber(0));
				}
				else
				{
					pdfArray2.Add(new PdfNumber(this.widths[k]));
				}
			}
			pdfDictionary.Put(PdfName.WIDTHS, pdfArray2);
			if (fontDescriptor != null)
			{
				pdfDictionary.Put(PdfName.FONTDESCRIPTOR, fontDescriptor);
			}
			return pdfDictionary;
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0002F0B4 File Offset: 0x0002E0B4
		protected byte[] GetFullFont()
		{
			RandomAccessFileOrArray randomAccessFileOrArray = null;
			byte[] result;
			try
			{
				randomAccessFileOrArray = new RandomAccessFileOrArray(this.rf);
				randomAccessFileOrArray.ReOpen();
				byte[] array = new byte[randomAccessFileOrArray.Length];
				randomAccessFileOrArray.ReadFully(array);
				result = array;
			}
			finally
			{
				try
				{
					if (randomAccessFileOrArray != null)
					{
						randomAccessFileOrArray.Close();
					}
				}
				catch
				{
				}
			}
			return result;
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0002F118 File Offset: 0x0002E118
		protected static int[] CompactRanges(List<int[]> ranges)
		{
			List<int[]> list = new List<int[]>();
			for (int i = 0; i < ranges.Count; i++)
			{
				int[] array = ranges[i];
				for (int j = 0; j < array.Length; j += 2)
				{
					list.Add(new int[]
					{
						Math.Max(0, Math.Min(array[j], array[j + 1])),
						Math.Min(65535, Math.Max(array[j], array[j + 1]))
					});
				}
			}
			for (int k = 0; k < list.Count - 1; k++)
			{
				for (int l = k + 1; l < list.Count; l++)
				{
					int[] array2 = list[k];
					int[] array3 = list[l];
					if ((array2[0] >= array3[0] && array2[0] <= array3[1]) || (array2[1] >= array3[0] && array2[0] <= array3[1]))
					{
						array2[0] = Math.Min(array2[0], array3[0]);
						array2[1] = Math.Max(array2[1], array3[1]);
						list.RemoveAt(l);
						l--;
					}
				}
			}
			int[] array4 = new int[list.Count * 2];
			for (int m = 0; m < list.Count; m++)
			{
				int[] array5 = list[m];
				array4[m * 2] = array5[0];
				array4[m * 2 + 1] = array5[1];
			}
			return array4;
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0002F280 File Offset: 0x0002E280
		protected void AddRangeUni(Dictionary<int, int[]> longTag, bool includeMetrics, bool subsetp)
		{
			if (!subsetp && (this.subsetRanges != null || this.directoryOffset > 0))
			{
				int[] array = (this.subsetRanges == null && this.directoryOffset > 0) ? new int[]
				{
					0,
					65535
				} : TrueTypeFont.CompactRanges(this.subsetRanges);
				Dictionary<int, int[]> dictionary;
				if (!this.fontSpecific && this.cmap31 != null)
				{
					dictionary = this.cmap31;
				}
				else if (this.fontSpecific && this.cmap10 != null)
				{
					dictionary = this.cmap10;
				}
				else if (this.cmap31 != null)
				{
					dictionary = this.cmap31;
				}
				else
				{
					dictionary = this.cmap10;
				}
				foreach (KeyValuePair<int, int[]> keyValuePair in dictionary)
				{
					int[] value = keyValuePair.Value;
					int key = value[0];
					if (!longTag.ContainsKey(key))
					{
						int key2 = keyValuePair.Key;
						bool flag = true;
						for (int i = 0; i < array.Length; i += 2)
						{
							if (key2 >= array[i] && key2 <= array[i + 1])
							{
								flag = false;
								break;
							}
						}
						if (!flag)
						{
							longTag[key] = (includeMetrics ? new int[]
							{
								value[0],
								value[1],
								key2
							} : null);
						}
					}
				}
			}
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0002F3E0 File Offset: 0x0002E3E0
		internal override void WriteFont(PdfWriter writer, PdfIndirectReference piref, object[] parms)
		{
			int num = (int)parms[0];
			int num2 = (int)parms[1];
			byte[] array = (byte[])parms[2];
			bool flag = (bool)parms[3] && this.subset;
			if (!flag)
			{
				num = 0;
				num2 = array.Length - 1;
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = 1;
				}
			}
			PdfIndirectReference pdfIndirectReference = null;
			string subsetPrefix = "";
			PdfObject pdfObject;
			if (this.embedded)
			{
				if (this.cff)
				{
					pdfObject = new BaseFont.StreamFont(this.ReadCffFont(), "Type1C", this.compressionLevel);
					PdfIndirectObject pdfIndirectObject = writer.AddToBody(pdfObject);
					pdfIndirectReference = pdfIndirectObject.IndirectReference;
				}
				else
				{
					if (flag)
					{
						subsetPrefix = iTextSharp.text.pdf.BaseFont.CreateSubsetPrefix();
					}
					Dictionary<int, int[]> dictionary = new Dictionary<int, int[]>();
					for (int j = num; j <= num2; j++)
					{
						if (array[j] != 0)
						{
							int[] array2 = null;
							if (this.specialMap != null)
							{
								int[] array3 = GlyphList.NameToUnicode(this.differences[j]);
								if (array3 != null)
								{
									array2 = this.GetMetricsTT(array3[0]);
								}
							}
							else if (this.fontSpecific)
							{
								array2 = this.GetMetricsTT(j);
							}
							else
							{
								array2 = this.GetMetricsTT((int)this.unicodeDifferences[j]);
							}
							if (array2 != null)
							{
								dictionary[array2[0]] = null;
							}
						}
					}
					this.AddRangeUni(dictionary, false, flag);
					byte[] array4;
					if (flag || this.directoryOffset != 0 || this.subsetRanges != null)
					{
						TrueTypeFontSubSet trueTypeFontSubSet = new TrueTypeFontSubSet(this.fileName, new RandomAccessFileOrArray(this.rf), dictionary, this.directoryOffset, true, !flag);
						array4 = trueTypeFontSubSet.Process();
					}
					else
					{
						array4 = this.GetFullFont();
					}
					int[] lengths = new int[]
					{
						array4.Length
					};
					pdfObject = new BaseFont.StreamFont(array4, lengths, this.compressionLevel);
					PdfIndirectObject pdfIndirectObject = writer.AddToBody(pdfObject);
					pdfIndirectReference = pdfIndirectObject.IndirectReference;
				}
			}
			pdfObject = this.GetFontDescriptor(pdfIndirectReference, subsetPrefix, null);
			if (pdfObject != null)
			{
				PdfIndirectObject pdfIndirectObject = writer.AddToBody(pdfObject);
				pdfIndirectReference = pdfIndirectObject.IndirectReference;
			}
			pdfObject = this.GetFontBaseType(pdfIndirectReference, subsetPrefix, num, num2, array);
			writer.AddToBody(pdfObject, piref);
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0002F5E8 File Offset: 0x0002E5E8
		protected internal byte[] ReadCffFont()
		{
			RandomAccessFileOrArray randomAccessFileOrArray = new RandomAccessFileOrArray(this.rf);
			byte[] array = new byte[this.cffLength];
			try
			{
				randomAccessFileOrArray.ReOpen();
				randomAccessFileOrArray.Seek(this.cffOffset);
				randomAccessFileOrArray.ReadFully(array);
			}
			finally
			{
				try
				{
					randomAccessFileOrArray.Close();
				}
				catch
				{
				}
			}
			return array;
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0002F654 File Offset: 0x0002E654
		public override PdfStream GetFullFontStream()
		{
			if (this.cff)
			{
				return new BaseFont.StreamFont(this.ReadCffFont(), "Type1C", this.compressionLevel);
			}
			byte[] fullFont = this.GetFullFont();
			int[] lengths = new int[]
			{
				fullFont.Length
			};
			return new BaseFont.StreamFont(fullFont, lengths, this.compressionLevel);
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0002F6A4 File Offset: 0x0002E6A4
		public override float GetFontDescriptor(int key, float fontSize)
		{
			switch (key)
			{
			case 1:
				return (float)this.os_2.sTypoAscender * fontSize / (float)this.head.unitsPerEm;
			case 2:
				return (float)this.os_2.sCapHeight * fontSize / (float)this.head.unitsPerEm;
			case 3:
				return (float)this.os_2.sTypoDescender * fontSize / (float)this.head.unitsPerEm;
			case 4:
				return (float)this.italicAngle;
			case 5:
				return fontSize * (float)this.head.xMin / (float)this.head.unitsPerEm;
			case 6:
				return fontSize * (float)this.head.yMin / (float)this.head.unitsPerEm;
			case 7:
				return fontSize * (float)this.head.xMax / (float)this.head.unitsPerEm;
			case 8:
				return fontSize * (float)this.head.yMax / (float)this.head.unitsPerEm;
			case 9:
				return fontSize * (float)this.hhea.Ascender / (float)this.head.unitsPerEm;
			case 10:
				return fontSize * (float)this.hhea.Descender / (float)this.head.unitsPerEm;
			case 11:
				return fontSize * (float)this.hhea.LineGap / (float)this.head.unitsPerEm;
			case 12:
				return fontSize * (float)this.hhea.advanceWidthMax / (float)this.head.unitsPerEm;
			case 13:
				return (float)(this.underlinePosition - this.underlineThickness / 2) * fontSize / (float)this.head.unitsPerEm;
			case 14:
				return (float)this.underlineThickness * fontSize / (float)this.head.unitsPerEm;
			case 15:
				return (float)this.os_2.yStrikeoutPosition * fontSize / (float)this.head.unitsPerEm;
			case 16:
				return (float)this.os_2.yStrikeoutSize * fontSize / (float)this.head.unitsPerEm;
			case 17:
				return (float)this.os_2.ySubscriptYSize * fontSize / (float)this.head.unitsPerEm;
			case 18:
				return (float)(-(float)this.os_2.ySubscriptYOffset) * fontSize / (float)this.head.unitsPerEm;
			case 19:
				return (float)this.os_2.ySuperscriptYSize * fontSize / (float)this.head.unitsPerEm;
			case 20:
				return (float)this.os_2.ySuperscriptYOffset * fontSize / (float)this.head.unitsPerEm;
			case 21:
				return (float)this.os_2.usWeightClass;
			case 22:
				return (float)this.os_2.usWidthClass;
			default:
				return 0f;
			}
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0002F954 File Offset: 0x0002E954
		public virtual int[] GetMetricsTT(int c)
		{
			int[] result = null;
			if (this.cmapExt != null)
			{
				this.cmapExt.TryGetValue(c, out result);
			}
			else if (!this.fontSpecific && this.cmap31 != null)
			{
				this.cmap31.TryGetValue(c, out result);
			}
			else if (this.fontSpecific && this.cmap10 != null)
			{
				this.cmap10.TryGetValue(c, out result);
			}
			else if (this.cmap31 != null)
			{
				this.cmap31.TryGetValue(c, out result);
			}
			else if (this.cmap10 != null)
			{
				this.cmap10.TryGetValue(c, out result);
			}
			return result;
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x0002F9EF File Offset: 0x0002E9EF
		// (set) Token: 0x060008CC RID: 2252 RVA: 0x0002F9F7 File Offset: 0x0002E9F7
		public override string PostscriptFontName
		{
			get
			{
				return this.fontName;
			}
			set
			{
				this.fontName = value;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x0002FA00 File Offset: 0x0002EA00
		public override string[] CodePagesSupported
		{
			get
			{
				long num = ((long)this.os_2.ulCodePageRange2 << 32) + ((long)this.os_2.ulCodePageRange1 & (long)((ulong)-1));
				int num2 = 0;
				long num3 = 1L;
				for (int i = 0; i < 64; i++)
				{
					if ((num & num3) != 0L && TrueTypeFont.codePages[i] != null)
					{
						num2++;
					}
					num3 <<= 1;
				}
				string[] array = new string[num2];
				num2 = 0;
				num3 = 1L;
				for (int j = 0; j < 64; j++)
				{
					if ((num & num3) != 0L && TrueTypeFont.codePages[j] != null)
					{
						array[num2++] = TrueTypeFont.codePages[j];
					}
					num3 <<= 1;
				}
				return array;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x0002FA9C File Offset: 0x0002EA9C
		public override string[][] FullFontName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x0002FAA4 File Offset: 0x0002EAA4
		public override string[][] AllNameEntries
		{
			get
			{
				return this.allNameEntries;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x0002FAAC File Offset: 0x0002EAAC
		public override string[][] FamilyFontName
		{
			get
			{
				return this.familyName;
			}
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0002FAB4 File Offset: 0x0002EAB4
		public override bool HasKernPairs()
		{
			return this.kerning.Size > 0;
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0002FAC4 File Offset: 0x0002EAC4
		public override bool SetKerning(int char1, int char2, int kern)
		{
			int[] metricsTT = this.GetMetricsTT(char1);
			if (metricsTT == null)
			{
				return false;
			}
			int num = metricsTT[0];
			metricsTT = this.GetMetricsTT(char2);
			if (metricsTT == null)
			{
				return false;
			}
			int num2 = metricsTT[0];
			this.kerning[(num << 16) + num2] = kern;
			return true;
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0002FB08 File Offset: 0x0002EB08
		protected override int[] GetRawCharBBox(int c, string name)
		{
			Dictionary<int, int[]> dictionary;
			if (name == null || this.cmap31 == null)
			{
				dictionary = this.cmap10;
			}
			else
			{
				dictionary = this.cmap31;
			}
			if (dictionary == null)
			{
				return null;
			}
			int[] array;
			dictionary.TryGetValue(c, out array);
			if (array == null || this.bboxes == null)
			{
				return null;
			}
			return this.bboxes[array[0]];
		}

		// Token: 0x04000742 RID: 1858
		internal static string[] codePages = new string[]
		{
			"1252 Latin 1",
			"1250 Latin 2: Eastern Europe",
			"1251 Cyrillic",
			"1253 Greek",
			"1254 Turkish",
			"1255 Hebrew",
			"1256 Arabic",
			"1257 Windows Baltic",
			"1258 Vietnamese",
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			"874 Thai",
			"932 JIS/Japan",
			"936 Chinese: Simplified chars--PRC and Singapore",
			"949 Korean Wansung",
			"950 Chinese: Traditional chars--Taiwan and Hong Kong",
			"1361 Korean Johab",
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			"Macintosh Character Set (US Roman)",
			"OEM Character Set",
			"Symbol Character Set",
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			"869 IBM Greek",
			"866 MS-DOS Russian",
			"865 MS-DOS Nordic",
			"864 Arabic",
			"863 MS-DOS Canadian French",
			"862 Hebrew",
			"861 MS-DOS Icelandic",
			"860 MS-DOS Portuguese",
			"857 IBM Turkish",
			"855 IBM Cyrillic; primarily Russian",
			"852 Latin 2",
			"775 MS-DOS Baltic",
			"737 Greek; former 437 G",
			"708 Arabic; ASMO 708",
			"850 WE/Latin 1",
			"437 US"
		};

		// Token: 0x04000743 RID: 1859
		protected bool justNames;

		// Token: 0x04000744 RID: 1860
		protected Dictionary<string, int[]> tables;

		// Token: 0x04000745 RID: 1861
		protected RandomAccessFileOrArray rf;

		// Token: 0x04000746 RID: 1862
		protected string fileName;

		// Token: 0x04000747 RID: 1863
		protected bool cff;

		// Token: 0x04000748 RID: 1864
		protected int cffOffset;

		// Token: 0x04000749 RID: 1865
		protected int cffLength;

		// Token: 0x0400074A RID: 1866
		protected int directoryOffset;

		// Token: 0x0400074B RID: 1867
		protected string ttcIndex;

		// Token: 0x0400074C RID: 1868
		protected string style = "";

		// Token: 0x0400074D RID: 1869
		protected TrueTypeFont.FontHeader head = new TrueTypeFont.FontHeader();

		// Token: 0x0400074E RID: 1870
		protected TrueTypeFont.HorizontalHeader hhea = new TrueTypeFont.HorizontalHeader();

		// Token: 0x0400074F RID: 1871
		protected TrueTypeFont.WindowsMetrics os_2 = new TrueTypeFont.WindowsMetrics();

		// Token: 0x04000750 RID: 1872
		protected int[] GlyphWidths;

		// Token: 0x04000751 RID: 1873
		protected int[][] bboxes;

		// Token: 0x04000752 RID: 1874
		protected Dictionary<int, int[]> cmap10;

		// Token: 0x04000753 RID: 1875
		protected Dictionary<int, int[]> cmap31;

		// Token: 0x04000754 RID: 1876
		protected Dictionary<int, int[]> cmapExt;

		// Token: 0x04000755 RID: 1877
		protected IntHashtable kerning = new IntHashtable();

		// Token: 0x04000756 RID: 1878
		protected string fontName;

		// Token: 0x04000757 RID: 1879
		protected string[][] fullName;

		// Token: 0x04000758 RID: 1880
		protected string[][] allNameEntries;

		// Token: 0x04000759 RID: 1881
		protected string[][] familyName;

		// Token: 0x0400075A RID: 1882
		protected double italicAngle;

		// Token: 0x0400075B RID: 1883
		protected bool isFixedPitch;

		// Token: 0x0400075C RID: 1884
		protected int underlinePosition;

		// Token: 0x0400075D RID: 1885
		protected int underlineThickness;

		// Token: 0x020000E8 RID: 232
		protected class FontHeader
		{
			// Token: 0x0400075E RID: 1886
			internal int flags;

			// Token: 0x0400075F RID: 1887
			internal int unitsPerEm;

			// Token: 0x04000760 RID: 1888
			internal short xMin;

			// Token: 0x04000761 RID: 1889
			internal short yMin;

			// Token: 0x04000762 RID: 1890
			internal short xMax;

			// Token: 0x04000763 RID: 1891
			internal short yMax;

			// Token: 0x04000764 RID: 1892
			internal int macStyle;
		}

		// Token: 0x020000E9 RID: 233
		protected class HorizontalHeader
		{
			// Token: 0x04000765 RID: 1893
			internal short Ascender;

			// Token: 0x04000766 RID: 1894
			internal short Descender;

			// Token: 0x04000767 RID: 1895
			internal short LineGap;

			// Token: 0x04000768 RID: 1896
			internal int advanceWidthMax;

			// Token: 0x04000769 RID: 1897
			internal short minLeftSideBearing;

			// Token: 0x0400076A RID: 1898
			internal short minRightSideBearing;

			// Token: 0x0400076B RID: 1899
			internal short xMaxExtent;

			// Token: 0x0400076C RID: 1900
			internal short caretSlopeRise;

			// Token: 0x0400076D RID: 1901
			internal short caretSlopeRun;

			// Token: 0x0400076E RID: 1902
			internal int numberOfHMetrics;
		}

		// Token: 0x020000EA RID: 234
		protected class WindowsMetrics
		{
			// Token: 0x0400076F RID: 1903
			internal short xAvgCharWidth;

			// Token: 0x04000770 RID: 1904
			internal int usWeightClass;

			// Token: 0x04000771 RID: 1905
			internal int usWidthClass;

			// Token: 0x04000772 RID: 1906
			internal short fsType;

			// Token: 0x04000773 RID: 1907
			internal short ySubscriptXSize;

			// Token: 0x04000774 RID: 1908
			internal short ySubscriptYSize;

			// Token: 0x04000775 RID: 1909
			internal short ySubscriptXOffset;

			// Token: 0x04000776 RID: 1910
			internal short ySubscriptYOffset;

			// Token: 0x04000777 RID: 1911
			internal short ySuperscriptXSize;

			// Token: 0x04000778 RID: 1912
			internal short ySuperscriptYSize;

			// Token: 0x04000779 RID: 1913
			internal short ySuperscriptXOffset;

			// Token: 0x0400077A RID: 1914
			internal short ySuperscriptYOffset;

			// Token: 0x0400077B RID: 1915
			internal short yStrikeoutSize;

			// Token: 0x0400077C RID: 1916
			internal short yStrikeoutPosition;

			// Token: 0x0400077D RID: 1917
			internal short sFamilyClass;

			// Token: 0x0400077E RID: 1918
			internal byte[] panose = new byte[10];

			// Token: 0x0400077F RID: 1919
			internal byte[] achVendID = new byte[4];

			// Token: 0x04000780 RID: 1920
			internal int fsSelection;

			// Token: 0x04000781 RID: 1921
			internal int usFirstCharIndex;

			// Token: 0x04000782 RID: 1922
			internal int usLastCharIndex;

			// Token: 0x04000783 RID: 1923
			internal short sTypoAscender;

			// Token: 0x04000784 RID: 1924
			internal short sTypoDescender;

			// Token: 0x04000785 RID: 1925
			internal short sTypoLineGap;

			// Token: 0x04000786 RID: 1926
			internal int usWinAscent;

			// Token: 0x04000787 RID: 1927
			internal int usWinDescent;

			// Token: 0x04000788 RID: 1928
			internal int ulCodePageRange1;

			// Token: 0x04000789 RID: 1929
			internal int ulCodePageRange2;

			// Token: 0x0400078A RID: 1930
			internal int sCapHeight;
		}
	}
}
