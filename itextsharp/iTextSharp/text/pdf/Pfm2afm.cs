using System;
using System.Globalization;
using System.IO;
using System.Text;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000525 RID: 1317
	public sealed class Pfm2afm
	{
		// Token: 0x06002CD6 RID: 11478 RVA: 0x00110A28 File Offset: 0x0010FA28
		private Pfm2afm(RandomAccessFileOrArray inp, Stream outp)
		{
			this.inp = inp;
			this.encoding = Encoding.GetEncoding(1252);
			this.outp = new StreamWriter(outp, this.encoding);
		}

		// Token: 0x06002CD7 RID: 11479 RVA: 0x00111524 File Offset: 0x00110524
		public static void Convert(RandomAccessFileOrArray inp, Stream outp)
		{
			Pfm2afm pfm2afm = new Pfm2afm(inp, outp);
			pfm2afm.Openpfm();
			pfm2afm.Putheader();
			pfm2afm.Putchartab();
			pfm2afm.Putkerntab();
			pfm2afm.Puttrailer();
			pfm2afm.outp.Flush();
		}

		// Token: 0x06002CD8 RID: 11480 RVA: 0x00111564 File Offset: 0x00110564
		private string ReadString(int n)
		{
			byte[] array = new byte[n];
			this.inp.ReadFully(array);
			int num = 0;
			while (num < array.Length && array[num] != 0)
			{
				num++;
			}
			return this.encoding.GetString(array, 0, num);
		}

		// Token: 0x06002CD9 RID: 11481 RVA: 0x001115A8 File Offset: 0x001105A8
		private string ReadString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (;;)
			{
				int num = this.inp.Read();
				if (num <= 0)
				{
					break;
				}
				stringBuilder.Append((char)num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002CDA RID: 11482 RVA: 0x001115DC File Offset: 0x001105DC
		private void Outval(int n)
		{
			this.outp.Write(' ');
			this.outp.Write(n);
		}

		// Token: 0x06002CDB RID: 11483 RVA: 0x001115F8 File Offset: 0x001105F8
		private void Outchar(int code, int width, string name)
		{
			this.outp.Write("C ");
			this.Outval(code);
			this.outp.Write(" ; WX ");
			this.Outval(width);
			if (name != null)
			{
				this.outp.Write(" ; N ");
				this.outp.Write(name);
			}
			this.outp.Write(" ;\n");
		}

		// Token: 0x06002CDC RID: 11484 RVA: 0x00111664 File Offset: 0x00110664
		private void Openpfm()
		{
			this.inp.Seek(0);
			this.vers = this.inp.ReadShortLE();
			this.h_len = this.inp.ReadIntLE();
			this.copyright = this.ReadString(60);
			this.type = this.inp.ReadShortLE();
			this.points = this.inp.ReadShortLE();
			this.verres = this.inp.ReadShortLE();
			this.horres = this.inp.ReadShortLE();
			this.ascent = this.inp.ReadShortLE();
			this.intleading = this.inp.ReadShortLE();
			this.extleading = this.inp.ReadShortLE();
			this.italic = (byte)this.inp.Read();
			this.uline = (byte)this.inp.Read();
			this.overs = (byte)this.inp.Read();
			this.weight = this.inp.ReadShortLE();
			this.charset = (byte)this.inp.Read();
			this.pixwidth = this.inp.ReadShortLE();
			this.pixheight = this.inp.ReadShortLE();
			this.kind = (byte)this.inp.Read();
			this.avgwidth = this.inp.ReadShortLE();
			this.maxwidth = this.inp.ReadShortLE();
			this.firstchar = this.inp.Read();
			this.lastchar = this.inp.Read();
			this.defchar = (byte)this.inp.Read();
			this.brkchar = (byte)this.inp.Read();
			this.widthby = this.inp.ReadShortLE();
			this.device = this.inp.ReadIntLE();
			this.face = this.inp.ReadIntLE();
			this.bits = this.inp.ReadIntLE();
			this.bitoff = this.inp.ReadIntLE();
			this.extlen = this.inp.ReadShortLE();
			this.psext = this.inp.ReadIntLE();
			this.chartab = this.inp.ReadIntLE();
			this.res1 = this.inp.ReadIntLE();
			this.kernpairs = this.inp.ReadIntLE();
			this.res2 = this.inp.ReadIntLE();
			this.fontname = this.inp.ReadIntLE();
			if (this.h_len != this.inp.Length || this.extlen != 30 || this.fontname < 75 || this.fontname > 512)
			{
				throw new IOException(MessageLocalization.GetComposedMessage("not.a.valid.pfm.file"));
			}
			this.inp.Seek(this.psext + 14);
			this.capheight = this.inp.ReadShortLE();
			this.xheight = this.inp.ReadShortLE();
			this.ascender = this.inp.ReadShortLE();
			this.descender = this.inp.ReadShortLE();
		}

		// Token: 0x06002CDD RID: 11485 RVA: 0x00111984 File Offset: 0x00110984
		private void Putheader()
		{
			this.outp.Write("StartFontMetrics 2.0\n");
			if (this.copyright.Length > 0)
			{
				this.outp.Write("Comment " + this.copyright + '\n');
			}
			this.outp.Write("FontName ");
			this.inp.Seek(this.fontname);
			string text = this.ReadString();
			this.outp.Write(text);
			this.outp.Write("\nEncodingScheme ");
			if (this.charset != 0)
			{
				this.outp.Write("FontSpecific\n");
			}
			else
			{
				this.outp.Write("AdobeStandardEncoding\n");
			}
			this.outp.Write("FullName " + text.Replace('-', ' '));
			if (this.face != 0)
			{
				this.inp.Seek(this.face);
				this.outp.Write("\nFamilyName " + this.ReadString());
			}
			this.outp.Write("\nWeight ");
			if (this.weight > 475 || text.ToLower(CultureInfo.InvariantCulture).IndexOf("bold") >= 0)
			{
				this.outp.Write("Bold");
			}
			else if ((this.weight < 325 && this.weight != 0) || text.ToLower(CultureInfo.InvariantCulture).IndexOf("light") >= 0)
			{
				this.outp.Write("Light");
			}
			else if (text.ToLower(CultureInfo.InvariantCulture).IndexOf("black") >= 0)
			{
				this.outp.Write("Black");
			}
			else
			{
				this.outp.Write("Medium");
			}
			this.outp.Write("\nItalicAngle ");
			if (this.italic != 0 || text.ToLower(CultureInfo.InvariantCulture).IndexOf("italic") >= 0)
			{
				this.outp.Write("-12.00");
			}
			else
			{
				this.outp.Write("0");
			}
			this.outp.Write("\nIsFixedPitch ");
			if ((this.kind & 1) == 0 || this.avgwidth == this.maxwidth)
			{
				this.outp.Write("true");
				this.isMono = true;
			}
			else
			{
				this.outp.Write("false");
				this.isMono = false;
			}
			this.outp.Write("\nFontBBox");
			if (this.isMono)
			{
				this.Outval(-20);
			}
			else
			{
				this.Outval(-100);
			}
			this.Outval((int)(-(int)(this.descender + 5)));
			this.Outval((int)(this.maxwidth + 10));
			this.Outval((int)(this.ascent + 5));
			this.outp.Write("\nCapHeight");
			this.Outval((int)this.capheight);
			this.outp.Write("\nXHeight");
			this.Outval((int)this.xheight);
			this.outp.Write("\nDescender");
			this.Outval((int)(-(int)this.descender));
			this.outp.Write("\nAscender");
			this.Outval((int)this.ascender);
			this.outp.Write('\n');
		}

		// Token: 0x06002CDE RID: 11486 RVA: 0x00111CD0 File Offset: 0x00110CD0
		private void Putchartab()
		{
			int num = this.lastchar - this.firstchar + 1;
			int[] array = new int[num];
			this.inp.Seek(this.chartab);
			for (int i = 0; i < num; i++)
			{
				array[i] = this.inp.ReadUnsignedShortLE();
			}
			int[] array2 = new int[256];
			if (this.charset == 0)
			{
				for (int j = this.firstchar; j <= this.lastchar; j++)
				{
					if (this.Win2PSStd[j] != 0)
					{
						array2[this.Win2PSStd[j]] = j;
					}
				}
			}
			this.outp.Write("StartCharMetrics");
			this.Outval(num);
			this.outp.Write('\n');
			if (this.charset != 0)
			{
				for (int k = this.firstchar; k <= this.lastchar; k++)
				{
					if (array[k - this.firstchar] != 0)
					{
						this.Outchar(k, array[k - this.firstchar], null);
					}
				}
			}
			else
			{
				for (int l = 0; l < 256; l++)
				{
					int num2 = array2[l];
					if (num2 != 0)
					{
						this.Outchar(l, array[num2 - this.firstchar], this.WinChars[num2]);
						array[num2 - this.firstchar] = 0;
					}
				}
				for (int m = this.firstchar; m <= this.lastchar; m++)
				{
					if (array[m - this.firstchar] != 0)
					{
						this.Outchar(-1, array[m - this.firstchar], this.WinChars[m]);
					}
				}
			}
			this.outp.Write("EndCharMetrics\n");
		}

		// Token: 0x06002CDF RID: 11487 RVA: 0x00111E68 File Offset: 0x00110E68
		private void Putkerntab()
		{
			if (this.kernpairs == 0)
			{
				return;
			}
			this.inp.Seek(this.kernpairs);
			int num = this.inp.ReadUnsignedShortLE();
			int num2 = 0;
			int[] array = new int[num * 3];
			int i = 0;
			while (i < array.Length)
			{
				array[i++] = this.inp.Read();
				array[i++] = this.inp.Read();
				if ((array[i++] = (int)this.inp.ReadShortLE()) != 0)
				{
					num2++;
				}
			}
			if (num2 == 0)
			{
				return;
			}
			this.outp.Write("StartKernData\nStartKernPairs");
			this.Outval(num2);
			this.outp.Write('\n');
			for (int j = 0; j < array.Length; j += 3)
			{
				if (array[j + 2] != 0)
				{
					this.outp.Write("KPX ");
					this.outp.Write(this.WinChars[array[j]]);
					this.outp.Write(' ');
					this.outp.Write(this.WinChars[array[j + 1]]);
					this.Outval(array[j + 2]);
					this.outp.Write('\n');
				}
			}
			this.outp.Write("EndKernPairs\nEndKernData\n");
		}

		// Token: 0x06002CE0 RID: 11488 RVA: 0x00111FA7 File Offset: 0x00110FA7
		private void Puttrailer()
		{
			this.outp.Write("EndFontMetrics\n");
		}

		// Token: 0x04001ED4 RID: 7892
		private RandomAccessFileOrArray inp;

		// Token: 0x04001ED5 RID: 7893
		private StreamWriter outp;

		// Token: 0x04001ED6 RID: 7894
		private Encoding encoding;

		// Token: 0x04001ED7 RID: 7895
		private short vers;

		// Token: 0x04001ED8 RID: 7896
		private int h_len;

		// Token: 0x04001ED9 RID: 7897
		private string copyright;

		// Token: 0x04001EDA RID: 7898
		private short type;

		// Token: 0x04001EDB RID: 7899
		private short points;

		// Token: 0x04001EDC RID: 7900
		private short verres;

		// Token: 0x04001EDD RID: 7901
		private short horres;

		// Token: 0x04001EDE RID: 7902
		private short ascent;

		// Token: 0x04001EDF RID: 7903
		private short intleading;

		// Token: 0x04001EE0 RID: 7904
		private short extleading;

		// Token: 0x04001EE1 RID: 7905
		private byte italic;

		// Token: 0x04001EE2 RID: 7906
		private byte uline;

		// Token: 0x04001EE3 RID: 7907
		private byte overs;

		// Token: 0x04001EE4 RID: 7908
		private short weight;

		// Token: 0x04001EE5 RID: 7909
		private byte charset;

		// Token: 0x04001EE6 RID: 7910
		private short pixwidth;

		// Token: 0x04001EE7 RID: 7911
		private short pixheight;

		// Token: 0x04001EE8 RID: 7912
		private byte kind;

		// Token: 0x04001EE9 RID: 7913
		private short avgwidth;

		// Token: 0x04001EEA RID: 7914
		private short maxwidth;

		// Token: 0x04001EEB RID: 7915
		private int firstchar;

		// Token: 0x04001EEC RID: 7916
		private int lastchar;

		// Token: 0x04001EED RID: 7917
		private byte defchar;

		// Token: 0x04001EEE RID: 7918
		private byte brkchar;

		// Token: 0x04001EEF RID: 7919
		private short widthby;

		// Token: 0x04001EF0 RID: 7920
		private int device;

		// Token: 0x04001EF1 RID: 7921
		private int face;

		// Token: 0x04001EF2 RID: 7922
		private int bits;

		// Token: 0x04001EF3 RID: 7923
		private int bitoff;

		// Token: 0x04001EF4 RID: 7924
		private short extlen;

		// Token: 0x04001EF5 RID: 7925
		private int psext;

		// Token: 0x04001EF6 RID: 7926
		private int chartab;

		// Token: 0x04001EF7 RID: 7927
		private int res1;

		// Token: 0x04001EF8 RID: 7928
		private int kernpairs;

		// Token: 0x04001EF9 RID: 7929
		private int res2;

		// Token: 0x04001EFA RID: 7930
		private int fontname;

		// Token: 0x04001EFB RID: 7931
		private short capheight;

		// Token: 0x04001EFC RID: 7932
		private short xheight;

		// Token: 0x04001EFD RID: 7933
		private short ascender;

		// Token: 0x04001EFE RID: 7934
		private short descender;

		// Token: 0x04001EFF RID: 7935
		private bool isMono;

		// Token: 0x04001F00 RID: 7936
		private int[] Win2PSStd = new int[]
		{
			0,
			0,
			0,
			0,
			197,
			198,
			199,
			0,
			202,
			0,
			205,
			206,
			207,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			32,
			33,
			34,
			35,
			36,
			37,
			38,
			169,
			40,
			41,
			42,
			43,
			44,
			45,
			46,
			47,
			48,
			49,
			50,
			51,
			52,
			53,
			54,
			55,
			56,
			57,
			58,
			59,
			60,
			61,
			62,
			63,
			64,
			65,
			66,
			67,
			68,
			69,
			70,
			71,
			72,
			73,
			74,
			75,
			76,
			77,
			78,
			79,
			80,
			81,
			82,
			83,
			84,
			85,
			86,
			87,
			88,
			89,
			90,
			91,
			92,
			93,
			94,
			95,
			193,
			97,
			98,
			99,
			100,
			101,
			102,
			103,
			104,
			105,
			106,
			107,
			108,
			109,
			110,
			111,
			112,
			113,
			114,
			115,
			116,
			117,
			118,
			119,
			120,
			121,
			122,
			123,
			124,
			125,
			126,
			127,
			128,
			0,
			184,
			166,
			185,
			188,
			178,
			179,
			195,
			189,
			0,
			172,
			234,
			0,
			0,
			0,
			0,
			96,
			0,
			170,
			186,
			183,
			177,
			208,
			196,
			0,
			0,
			173,
			250,
			0,
			0,
			0,
			0,
			161,
			162,
			163,
			168,
			165,
			0,
			167,
			200,
			0,
			227,
			171,
			0,
			0,
			0,
			197,
			0,
			0,
			0,
			0,
			194,
			0,
			182,
			180,
			203,
			0,
			235,
			187,
			0,
			0,
			0,
			191,
			0,
			0,
			0,
			0,
			0,
			0,
			225,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			233,
			0,
			0,
			0,
			0,
			0,
			0,
			251,
			0,
			0,
			0,
			0,
			0,
			0,
			241,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			249,
			0,
			0,
			0,
			0,
			0,
			0,
			0
		};

		// Token: 0x04001F01 RID: 7937
		private int[] WinClass = new int[]
		{
			0,
			0,
			0,
			0,
			2,
			2,
			2,
			0,
			2,
			0,
			2,
			2,
			2,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			2,
			0,
			0,
			2,
			0,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			0,
			0,
			0,
			0,
			3,
			3,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			0,
			0,
			2,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1
		};

		// Token: 0x04001F02 RID: 7938
		private string[] WinChars = new string[]
		{
			"W00",
			"W01",
			"W02",
			"W03",
			"macron",
			"breve",
			"dotaccent",
			"W07",
			"ring",
			"W09",
			"W0a",
			"W0b",
			"W0c",
			"W0d",
			"W0e",
			"W0f",
			"hungarumlaut",
			"ogonek",
			"caron",
			"W13",
			"W14",
			"W15",
			"W16",
			"W17",
			"W18",
			"W19",
			"W1a",
			"W1b",
			"W1c",
			"W1d",
			"W1e",
			"W1f",
			"space",
			"exclam",
			"quotedbl",
			"numbersign",
			"dollar",
			"percent",
			"ampersand",
			"quotesingle",
			"parenleft",
			"parenright",
			"asterisk",
			"plus",
			"comma",
			"hyphen",
			"period",
			"slash",
			"zero",
			"one",
			"two",
			"three",
			"four",
			"five",
			"six",
			"seven",
			"eight",
			"nine",
			"colon",
			"semicolon",
			"less",
			"equal",
			"greater",
			"question",
			"at",
			"A",
			"B",
			"C",
			"D",
			"E",
			"F",
			"G",
			"H",
			"I",
			"J",
			"K",
			"L",
			"M",
			"N",
			"O",
			"P",
			"Q",
			"R",
			"S",
			"T",
			"U",
			"V",
			"W",
			"X",
			"Y",
			"Z",
			"bracketleft",
			"backslash",
			"bracketright",
			"asciicircum",
			"underscore",
			"grave",
			"a",
			"b",
			"c",
			"d",
			"e",
			"f",
			"g",
			"h",
			"i",
			"j",
			"k",
			"l",
			"m",
			"n",
			"o",
			"p",
			"q",
			"r",
			"s",
			"t",
			"u",
			"v",
			"w",
			"x",
			"y",
			"z",
			"braceleft",
			"bar",
			"braceright",
			"asciitilde",
			"W7f",
			"euro",
			"W81",
			"quotesinglbase",
			"florin",
			"quotedblbase",
			"ellipsis",
			"dagger",
			"daggerdbl",
			"circumflex",
			"perthousand",
			"Scaron",
			"guilsinglleft",
			"OE",
			"W8d",
			"Zcaron",
			"W8f",
			"W90",
			"quoteleft",
			"quoteright",
			"quotedblleft",
			"quotedblright",
			"bullet",
			"endash",
			"emdash",
			"tilde",
			"trademark",
			"scaron",
			"guilsinglright",
			"oe",
			"W9d",
			"zcaron",
			"Ydieresis",
			"reqspace",
			"exclamdown",
			"cent",
			"sterling",
			"currency",
			"yen",
			"brokenbar",
			"section",
			"dieresis",
			"copyright",
			"ordfeminine",
			"guillemotleft",
			"logicalnot",
			"syllable",
			"registered",
			"macron",
			"degree",
			"plusminus",
			"twosuperior",
			"threesuperior",
			"acute",
			"mu",
			"paragraph",
			"periodcentered",
			"cedilla",
			"onesuperior",
			"ordmasculine",
			"guillemotright",
			"onequarter",
			"onehalf",
			"threequarters",
			"questiondown",
			"Agrave",
			"Aacute",
			"Acircumflex",
			"Atilde",
			"Adieresis",
			"Aring",
			"AE",
			"Ccedilla",
			"Egrave",
			"Eacute",
			"Ecircumflex",
			"Edieresis",
			"Igrave",
			"Iacute",
			"Icircumflex",
			"Idieresis",
			"Eth",
			"Ntilde",
			"Ograve",
			"Oacute",
			"Ocircumflex",
			"Otilde",
			"Odieresis",
			"multiply",
			"Oslash",
			"Ugrave",
			"Uacute",
			"Ucircumflex",
			"Udieresis",
			"Yacute",
			"Thorn",
			"germandbls",
			"agrave",
			"aacute",
			"acircumflex",
			"atilde",
			"adieresis",
			"aring",
			"ae",
			"ccedilla",
			"egrave",
			"eacute",
			"ecircumflex",
			"edieresis",
			"igrave",
			"iacute",
			"icircumflex",
			"idieresis",
			"eth",
			"ntilde",
			"ograve",
			"oacute",
			"ocircumflex",
			"otilde",
			"odieresis",
			"divide",
			"oslash",
			"ugrave",
			"uacute",
			"ucircumflex",
			"udieresis",
			"yacute",
			"thorn",
			"ydieresis"
		};
	}
}
