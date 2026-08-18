using System;
using System.Collections.Generic;
using System.Text;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000589 RID: 1417
	internal class TrueTypeFontSubSet
	{
		// Token: 0x06003032 RID: 12338 RVA: 0x0012A2D0 File Offset: 0x001292D0
		internal TrueTypeFontSubSet(string fileName, RandomAccessFileOrArray rf, Dictionary<int, int[]> glyphsUsed, int directoryOffset, bool includeCmap, bool includeExtras)
		{
			this.fileName = fileName;
			this.rf = rf;
			this.glyphsUsed = glyphsUsed;
			this.includeCmap = includeCmap;
			this.includeExtras = includeExtras;
			this.directoryOffset = directoryOffset;
			this.glyphsInList = new List<int>(glyphsUsed.Keys);
		}

		// Token: 0x06003033 RID: 12339 RVA: 0x0012A324 File Offset: 0x00129324
		internal byte[] Process()
		{
			byte[] result;
			try
			{
				this.rf.ReOpen();
				this.CreateTableDirectory();
				this.ReadLoca();
				this.FlatGlyphs();
				this.CreateNewGlyphTables();
				this.LocaTobytes();
				this.AssembleFont();
				result = this.outFont;
			}
			finally
			{
				try
				{
					this.rf.Close();
				}
				catch
				{
				}
			}
			return result;
		}

		// Token: 0x06003034 RID: 12340 RVA: 0x0012A398 File Offset: 0x00129398
		protected void AssembleFont()
		{
			int num = 0;
			string[] array;
			if (this.includeExtras)
			{
				array = TrueTypeFontSubSet.tableNamesExtra;
			}
			else if (this.includeCmap)
			{
				array = TrueTypeFontSubSet.tableNamesCmap;
			}
			else
			{
				array = TrueTypeFontSubSet.tableNamesSimple;
			}
			int num2 = 2;
			foreach (string text in array)
			{
				if (!text.Equals("glyf") && !text.Equals("loca"))
				{
					int[] array2;
					this.tableDirectory.TryGetValue(text, out array2);
					if (array2 != null)
					{
						num2++;
						num += (array2[TrueTypeFontSubSet.TABLE_LENGTH] + 3 & -4);
					}
				}
			}
			num += this.newLocaTableOut.Length;
			num += this.newGlyfTable.Length;
			int num3 = 16 * num2 + 12;
			num += num3;
			this.outFont = new byte[num];
			this.fontPtr = 0;
			this.WriteFontInt(65536);
			this.WriteFontShort(num2);
			int num4 = TrueTypeFontSubSet.entrySelectors[num2];
			this.WriteFontShort((1 << num4) * 16);
			this.WriteFontShort(num4);
			this.WriteFontShort((num2 - (1 << num4)) * 16);
			foreach (string text2 in array)
			{
				int[] array2;
				this.tableDirectory.TryGetValue(text2, out array2);
				if (array2 != null)
				{
					this.WriteFontString(text2);
					int num5;
					if (text2.Equals("glyf"))
					{
						this.WriteFontInt(this.CalculateChecksum(this.newGlyfTable));
						num5 = this.glyfTableRealSize;
					}
					else if (text2.Equals("loca"))
					{
						this.WriteFontInt(this.CalculateChecksum(this.newLocaTableOut));
						num5 = this.locaTableRealSize;
					}
					else
					{
						this.WriteFontInt(array2[TrueTypeFontSubSet.TABLE_CHECKSUM]);
						num5 = array2[TrueTypeFontSubSet.TABLE_LENGTH];
					}
					this.WriteFontInt(num3);
					this.WriteFontInt(num5);
					num3 += (num5 + 3 & -4);
				}
			}
			foreach (string text3 in array)
			{
				int[] array2;
				this.tableDirectory.TryGetValue(text3, out array2);
				if (array2 != null)
				{
					if (text3.Equals("glyf"))
					{
						Array.Copy(this.newGlyfTable, 0, this.outFont, this.fontPtr, this.newGlyfTable.Length);
						this.fontPtr += this.newGlyfTable.Length;
						this.newGlyfTable = null;
					}
					else if (text3.Equals("loca"))
					{
						Array.Copy(this.newLocaTableOut, 0, this.outFont, this.fontPtr, this.newLocaTableOut.Length);
						this.fontPtr += this.newLocaTableOut.Length;
						this.newLocaTableOut = null;
					}
					else
					{
						this.rf.Seek(array2[TrueTypeFontSubSet.TABLE_OFFSET]);
						this.rf.ReadFully(this.outFont, this.fontPtr, array2[TrueTypeFontSubSet.TABLE_LENGTH]);
						this.fontPtr += (array2[TrueTypeFontSubSet.TABLE_LENGTH] + 3 & -4);
					}
				}
			}
		}

		// Token: 0x06003035 RID: 12341 RVA: 0x0012A684 File Offset: 0x00129684
		protected void CreateTableDirectory()
		{
			this.tableDirectory = new Dictionary<string, int[]>();
			this.rf.Seek(this.directoryOffset);
			int num = this.rf.ReadInt();
			if (num != 65536)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("1.is.not.a.true.type.file", this.fileName));
			}
			int num2 = this.rf.ReadUnsignedShort();
			this.rf.SkipBytes(6);
			for (int i = 0; i < num2; i++)
			{
				string key = this.ReadStandardString(4);
				int[] array = new int[3];
				array[TrueTypeFontSubSet.TABLE_CHECKSUM] = this.rf.ReadInt();
				array[TrueTypeFontSubSet.TABLE_OFFSET] = this.rf.ReadInt();
				array[TrueTypeFontSubSet.TABLE_LENGTH] = this.rf.ReadInt();
				this.tableDirectory[key] = array;
			}
		}

		// Token: 0x06003036 RID: 12342 RVA: 0x0012A754 File Offset: 0x00129754
		protected void ReadLoca()
		{
			int[] array;
			this.tableDirectory.TryGetValue("head", out array);
			if (array == null)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("table.1.does.not.exist.in.2", "head", this.fileName));
			}
			this.rf.Seek(array[TrueTypeFontSubSet.TABLE_OFFSET] + TrueTypeFontSubSet.HEAD_LOCA_FORMAT_OFFSET);
			this.locaShortTable = (this.rf.ReadUnsignedShort() == 0);
			this.tableDirectory.TryGetValue("loca", out array);
			if (array == null)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("table.1.does.not.exist.in.2", "loca", this.fileName));
			}
			this.rf.Seek(array[TrueTypeFontSubSet.TABLE_OFFSET]);
			if (this.locaShortTable)
			{
				int num = array[TrueTypeFontSubSet.TABLE_LENGTH] / 2;
				this.locaTable = new int[num];
				for (int i = 0; i < num; i++)
				{
					this.locaTable[i] = this.rf.ReadUnsignedShort() * 2;
				}
				return;
			}
			int num2 = array[TrueTypeFontSubSet.TABLE_LENGTH] / 4;
			this.locaTable = new int[num2];
			for (int j = 0; j < num2; j++)
			{
				this.locaTable[j] = this.rf.ReadInt();
			}
		}

		// Token: 0x06003037 RID: 12343 RVA: 0x0012A87C File Offset: 0x0012987C
		protected void CreateNewGlyphTables()
		{
			this.newLocaTable = new int[this.locaTable.Length];
			int[] array = new int[this.glyphsInList.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.glyphsInList[i];
			}
			Array.Sort<int>(array);
			int num = 0;
			foreach (int num2 in array)
			{
				num += this.locaTable[num2 + 1] - this.locaTable[num2];
			}
			this.glyfTableRealSize = num;
			num = (num + 3 & -4);
			this.newGlyfTable = new byte[num];
			int num3 = 0;
			int num4 = 0;
			for (int k = 0; k < this.newLocaTable.Length; k++)
			{
				this.newLocaTable[k] = num3;
				if (num4 < array.Length && array[num4] == k)
				{
					num4++;
					this.newLocaTable[k] = num3;
					int num5 = this.locaTable[k];
					int num6 = this.locaTable[k + 1] - num5;
					if (num6 > 0)
					{
						this.rf.Seek(this.tableGlyphOffset + num5);
						this.rf.ReadFully(this.newGlyfTable, num3, num6);
						num3 += num6;
					}
				}
			}
		}

		// Token: 0x06003038 RID: 12344 RVA: 0x0012A9B8 File Offset: 0x001299B8
		protected void LocaTobytes()
		{
			if (this.locaShortTable)
			{
				this.locaTableRealSize = this.newLocaTable.Length * 2;
			}
			else
			{
				this.locaTableRealSize = this.newLocaTable.Length * 4;
			}
			this.newLocaTableOut = new byte[this.locaTableRealSize + 3 & -4];
			this.outFont = this.newLocaTableOut;
			this.fontPtr = 0;
			for (int i = 0; i < this.newLocaTable.Length; i++)
			{
				if (this.locaShortTable)
				{
					this.WriteFontShort(this.newLocaTable[i] / 2);
				}
				else
				{
					this.WriteFontInt(this.newLocaTable[i]);
				}
			}
		}

		// Token: 0x06003039 RID: 12345 RVA: 0x0012AA54 File Offset: 0x00129A54
		protected void FlatGlyphs()
		{
			int[] array;
			this.tableDirectory.TryGetValue("glyf", out array);
			if (array == null)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("table.1.does.not.exist.in.2", "glyf", this.fileName));
			}
			int num = 0;
			if (!this.glyphsUsed.ContainsKey(num))
			{
				this.glyphsUsed[num] = null;
				this.glyphsInList.Add(num);
			}
			this.tableGlyphOffset = array[TrueTypeFontSubSet.TABLE_OFFSET];
			for (int i = 0; i < this.glyphsInList.Count; i++)
			{
				int glyph = this.glyphsInList[i];
				this.CheckGlyphComposite(glyph);
			}
		}

		// Token: 0x0600303A RID: 12346 RVA: 0x0012AAF4 File Offset: 0x00129AF4
		protected void CheckGlyphComposite(int glyph)
		{
			int num = this.locaTable[glyph];
			if (num == this.locaTable[glyph + 1])
			{
				return;
			}
			this.rf.Seek(this.tableGlyphOffset + num);
			int num2 = (int)this.rf.ReadShort();
			if (num2 >= 0)
			{
				return;
			}
			this.rf.SkipBytes(8);
			for (;;)
			{
				int num3 = this.rf.ReadUnsignedShort();
				int num4 = this.rf.ReadUnsignedShort();
				if (!this.glyphsUsed.ContainsKey(num4))
				{
					this.glyphsUsed[num4] = null;
					this.glyphsInList.Add(num4);
				}
				if ((num3 & TrueTypeFontSubSet.MORE_COMPONENTS) == 0)
				{
					break;
				}
				int num5;
				if ((num3 & TrueTypeFontSubSet.ARG_1_AND_2_ARE_WORDS) != 0)
				{
					num5 = 4;
				}
				else
				{
					num5 = 2;
				}
				if ((num3 & TrueTypeFontSubSet.WE_HAVE_A_SCALE) != 0)
				{
					num5 += 2;
				}
				else if ((num3 & TrueTypeFontSubSet.WE_HAVE_AN_X_AND_Y_SCALE) != 0)
				{
					num5 += 4;
				}
				if ((num3 & TrueTypeFontSubSet.WE_HAVE_A_TWO_BY_TWO) != 0)
				{
					num5 += 8;
				}
				this.rf.SkipBytes(num5);
			}
		}

		// Token: 0x0600303B RID: 12347 RVA: 0x0012ABE4 File Offset: 0x00129BE4
		protected string ReadStandardString(int length)
		{
			byte[] array = new byte[length];
			this.rf.ReadFully(array);
			return Encoding.GetEncoding(1252).GetString(array);
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x0012AC14 File Offset: 0x00129C14
		protected void WriteFontShort(int n)
		{
			this.outFont[this.fontPtr++] = (byte)(n >> 8);
			this.outFont[this.fontPtr++] = (byte)n;
		}

		// Token: 0x0600303D RID: 12349 RVA: 0x0012AC58 File Offset: 0x00129C58
		protected void WriteFontInt(int n)
		{
			this.outFont[this.fontPtr++] = (byte)(n >> 24);
			this.outFont[this.fontPtr++] = (byte)(n >> 16);
			this.outFont[this.fontPtr++] = (byte)(n >> 8);
			this.outFont[this.fontPtr++] = (byte)n;
		}

		// Token: 0x0600303E RID: 12350 RVA: 0x0012ACD8 File Offset: 0x00129CD8
		protected void WriteFontString(string s)
		{
			byte[] array = PdfEncodings.ConvertToBytes(s, "Cp1252");
			Array.Copy(array, 0, this.outFont, this.fontPtr, array.Length);
			this.fontPtr += array.Length;
		}

		// Token: 0x0600303F RID: 12351 RVA: 0x0012AD18 File Offset: 0x00129D18
		protected int CalculateChecksum(byte[] b)
		{
			int num = b.Length / 4;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			for (int i = 0; i < num; i++)
			{
				num5 += (int)(b[num6++] & byte.MaxValue);
				num4 += (int)(b[num6++] & byte.MaxValue);
				num3 += (int)(b[num6++] & byte.MaxValue);
				num2 += (int)(b[num6++] & byte.MaxValue);
			}
			return num2 + (num3 << 8) + (num4 << 16) + (num5 << 24);
		}

		// Token: 0x04002111 RID: 8465
		internal static string[] tableNamesSimple = new string[]
		{
			"cvt ",
			"fpgm",
			"glyf",
			"head",
			"hhea",
			"hmtx",
			"loca",
			"maxp",
			"prep"
		};

		// Token: 0x04002112 RID: 8466
		internal static string[] tableNamesCmap = new string[]
		{
			"cmap",
			"cvt ",
			"fpgm",
			"glyf",
			"head",
			"hhea",
			"hmtx",
			"loca",
			"maxp",
			"prep"
		};

		// Token: 0x04002113 RID: 8467
		internal static string[] tableNamesExtra = new string[]
		{
			"OS/2",
			"cmap",
			"cvt ",
			"fpgm",
			"glyf",
			"head",
			"hhea",
			"hmtx",
			"loca",
			"maxp",
			"name, prep"
		};

		// Token: 0x04002114 RID: 8468
		internal static int[] entrySelectors = new int[]
		{
			0,
			0,
			1,
			1,
			2,
			2,
			2,
			2,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			4,
			4,
			4,
			4,
			4
		};

		// Token: 0x04002115 RID: 8469
		internal static int TABLE_CHECKSUM = 0;

		// Token: 0x04002116 RID: 8470
		internal static int TABLE_OFFSET = 1;

		// Token: 0x04002117 RID: 8471
		internal static int TABLE_LENGTH = 2;

		// Token: 0x04002118 RID: 8472
		internal static int HEAD_LOCA_FORMAT_OFFSET = 51;

		// Token: 0x04002119 RID: 8473
		internal static int ARG_1_AND_2_ARE_WORDS = 1;

		// Token: 0x0400211A RID: 8474
		internal static int WE_HAVE_A_SCALE = 8;

		// Token: 0x0400211B RID: 8475
		internal static int MORE_COMPONENTS = 32;

		// Token: 0x0400211C RID: 8476
		internal static int WE_HAVE_AN_X_AND_Y_SCALE = 64;

		// Token: 0x0400211D RID: 8477
		internal static int WE_HAVE_A_TWO_BY_TWO = 128;

		// Token: 0x0400211E RID: 8478
		protected Dictionary<string, int[]> tableDirectory;

		// Token: 0x0400211F RID: 8479
		protected RandomAccessFileOrArray rf;

		// Token: 0x04002120 RID: 8480
		protected string fileName;

		// Token: 0x04002121 RID: 8481
		protected bool includeCmap;

		// Token: 0x04002122 RID: 8482
		protected bool includeExtras;

		// Token: 0x04002123 RID: 8483
		protected bool locaShortTable;

		// Token: 0x04002124 RID: 8484
		protected int[] locaTable;

		// Token: 0x04002125 RID: 8485
		protected Dictionary<int, int[]> glyphsUsed;

		// Token: 0x04002126 RID: 8486
		protected List<int> glyphsInList;

		// Token: 0x04002127 RID: 8487
		protected int tableGlyphOffset;

		// Token: 0x04002128 RID: 8488
		protected int[] newLocaTable;

		// Token: 0x04002129 RID: 8489
		protected byte[] newLocaTableOut;

		// Token: 0x0400212A RID: 8490
		protected byte[] newGlyfTable;

		// Token: 0x0400212B RID: 8491
		protected int glyfTableRealSize;

		// Token: 0x0400212C RID: 8492
		protected int locaTableRealSize;

		// Token: 0x0400212D RID: 8493
		protected byte[] outFont;

		// Token: 0x0400212E RID: 8494
		protected int fontPtr;

		// Token: 0x0400212F RID: 8495
		protected int directoryOffset;
	}
}
