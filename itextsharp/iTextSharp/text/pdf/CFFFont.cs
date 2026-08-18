using System;
using System.Collections.Generic;
using System.Text;

namespace iTextSharp.text.pdf
{
	// Token: 0x020003A7 RID: 935
	public class CFFFont
	{
		// Token: 0x06002095 RID: 8341 RVA: 0x000C19B8 File Offset: 0x000C09B8
		public string GetString(char sid)
		{
			if ((int)sid < CFFFont.standardStrings.Length)
			{
				return CFFFont.standardStrings[(int)sid];
			}
			if ((int)sid >= CFFFont.standardStrings.Length + (this.stringOffsets.Length - 1))
			{
				return null;
			}
			int num = (int)sid - CFFFont.standardStrings.Length;
			int position = this.GetPosition();
			this.Seek(this.stringOffsets[num]);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = this.stringOffsets[num]; i < this.stringOffsets[num + 1]; i++)
			{
				stringBuilder.Append(this.GetCard8());
			}
			this.Seek(position);
			return stringBuilder.ToString();
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x000C1A4C File Offset: 0x000C0A4C
		internal char GetCard8()
		{
			byte b = this.buf.ReadByte();
			return (char)(b & byte.MaxValue);
		}

		// Token: 0x06002097 RID: 8343 RVA: 0x000C1A6D File Offset: 0x000C0A6D
		internal char GetCard16()
		{
			return this.buf.ReadChar();
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x000C1A7C File Offset: 0x000C0A7C
		internal int GetOffset(int offSize)
		{
			int num = 0;
			for (int i = 0; i < offSize; i++)
			{
				num *= 256;
				num += (int)this.GetCard8();
			}
			return num;
		}

		// Token: 0x06002099 RID: 8345 RVA: 0x000C1AA9 File Offset: 0x000C0AA9
		internal void Seek(int offset)
		{
			this.buf.Seek(offset);
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x000C1AB7 File Offset: 0x000C0AB7
		internal short GetShort()
		{
			return this.buf.ReadShort();
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x000C1AC4 File Offset: 0x000C0AC4
		internal int GetInt()
		{
			return this.buf.ReadInt();
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x000C1AD1 File Offset: 0x000C0AD1
		internal int GetPosition()
		{
			return this.buf.FilePointer;
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x000C1AE0 File Offset: 0x000C0AE0
		internal int[] GetIndex(int nextIndexOffset)
		{
			this.Seek(nextIndexOffset);
			int card = (int)this.GetCard16();
			int[] array = new int[card + 1];
			if (card == 0)
			{
				array[0] = -1;
				nextIndexOffset += 2;
				return array;
			}
			int card2 = (int)this.GetCard8();
			for (int i = 0; i <= card; i++)
			{
				array[i] = nextIndexOffset + 2 + 1 + (card + 1) * card2 - 1 + this.GetOffset(card2);
			}
			return array;
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x000C1B40 File Offset: 0x000C0B40
		protected void GetDictItem()
		{
			for (int i = 0; i < this.arg_count; i++)
			{
				this.args[i] = null;
			}
			this.arg_count = 0;
			this.key = null;
			bool flag = false;
			while (!flag)
			{
				char card = this.GetCard8();
				if (card == '\u001d')
				{
					int @int = this.GetInt();
					this.args[this.arg_count] = @int;
					this.arg_count++;
				}
				else if (card == '\u001c')
				{
					short @short = this.GetShort();
					this.args[this.arg_count] = (int)@short;
					this.arg_count++;
				}
				else if (card >= ' ' && card <= 'ö')
				{
					sbyte b = (sbyte)(card - '\u008b');
					this.args[this.arg_count] = (int)b;
					this.arg_count++;
				}
				else if (card >= '÷' && card <= 'ú')
				{
					char card2 = this.GetCard8();
					short num = (short)((card - '÷') * 'Ā' + card2 + 'l');
					this.args[this.arg_count] = (int)num;
					this.arg_count++;
				}
				else if (card >= 'û' && card <= 'þ')
				{
					char card3 = this.GetCard8();
					short num2 = (short)(-(card - 'û') * 'Ā' - card3 - 'l');
					this.args[this.arg_count] = (int)num2;
					this.arg_count++;
				}
				else if (card == '\u001e')
				{
					string text = "";
					bool flag2 = false;
					char c = '\0';
					byte b2 = 0;
					int num3 = 0;
					while (!flag2)
					{
						if (b2 == 0)
						{
							c = this.GetCard8();
							b2 = 2;
						}
						if (b2 == 1)
						{
							num3 = (int)(c / '\u0010');
							b2 -= 1;
						}
						if (b2 == 2)
						{
							num3 = (int)(c % '\u0010');
							b2 -= 1;
						}
						switch (num3)
						{
						case 10:
							text += ".";
							continue;
						case 11:
							text += "E";
							continue;
						case 12:
							text += "E-";
							continue;
						case 14:
							text += "-";
							continue;
						case 15:
							flag2 = true;
							continue;
						}
						if (num3 >= 0 && num3 <= 9)
						{
							text += num3.ToString();
						}
						else
						{
							text = text + "<NIBBLE ERROR: " + num3.ToString() + ">";
							flag2 = true;
						}
					}
					this.args[this.arg_count] = text;
					this.arg_count++;
				}
				else if (card <= '\u0015')
				{
					flag = true;
					if (card != '\f')
					{
						this.key = CFFFont.operatorNames[(int)card];
					}
					else
					{
						this.key = CFFFont.operatorNames[(int)(' ' + this.GetCard8())];
					}
				}
			}
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x000C1E2C File Offset: 0x000C0E2C
		protected virtual CFFFont.RangeItem GetEntireIndexRange(int indexOffset)
		{
			this.Seek(indexOffset);
			int card = (int)this.GetCard16();
			if (card == 0)
			{
				return new CFFFont.RangeItem(this.buf, indexOffset, 2);
			}
			int card2 = (int)this.GetCard8();
			this.Seek(indexOffset + 2 + 1 + card * card2);
			int num = this.GetOffset(card2) - 1;
			return new CFFFont.RangeItem(this.buf, indexOffset, 3 + (card + 1) * card2 + num);
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x000C1E90 File Offset: 0x000C0E90
		public byte[] GetCID(string fontName)
		{
			int num = 0;
			while (num < this.fonts.Length && !fontName.Equals(this.fonts[num].name))
			{
				num++;
			}
			if (num == this.fonts.Length)
			{
				return null;
			}
			List<CFFFont.Item> list = new List<CFFFont.Item>();
			this.Seek(0);
			this.GetCard8();
			this.GetCard8();
			int card = (int)this.GetCard8();
			this.GetCard8();
			this.nextIndexOffset = card;
			list.Add(new CFFFont.RangeItem(this.buf, 0, card));
			int num2 = -1;
			int num3 = -1;
			if (!this.fonts[num].isCID)
			{
				this.Seek(this.fonts[num].charstringsOffset);
				num2 = (int)this.GetCard16();
				this.Seek(this.stringIndexOffset);
				num3 = (int)this.GetCard16() + CFFFont.standardStrings.Length;
			}
			list.Add(new CFFFont.UInt16Item('\u0001'));
			list.Add(new CFFFont.UInt8Item('\u0001'));
			list.Add(new CFFFont.UInt8Item('\u0001'));
			list.Add(new CFFFont.UInt8Item((char)(1 + this.fonts[num].name.Length)));
			list.Add(new CFFFont.StringItem(this.fonts[num].name));
			list.Add(new CFFFont.UInt16Item('\u0001'));
			list.Add(new CFFFont.UInt8Item('\u0002'));
			list.Add(new CFFFont.UInt16Item('\u0001'));
			CFFFont.OffsetItem offsetItem = new CFFFont.IndexOffsetItem(2);
			list.Add(offsetItem);
			CFFFont.IndexBaseItem indexBaseItem = new CFFFont.IndexBaseItem();
			list.Add(indexBaseItem);
			CFFFont.OffsetItem offsetItem2 = new CFFFont.DictOffsetItem();
			CFFFont.OffsetItem offsetItem3 = new CFFFont.DictOffsetItem();
			CFFFont.OffsetItem offsetItem4 = new CFFFont.DictOffsetItem();
			CFFFont.OffsetItem offsetItem5 = new CFFFont.DictOffsetItem();
			if (!this.fonts[num].isCID)
			{
				list.Add(new CFFFont.DictNumberItem(num3));
				list.Add(new CFFFont.DictNumberItem(num3 + 1));
				list.Add(new CFFFont.DictNumberItem(0));
				list.Add(new CFFFont.UInt8Item('\f'));
				list.Add(new CFFFont.UInt8Item('\u001e'));
				list.Add(new CFFFont.DictNumberItem(num2));
				list.Add(new CFFFont.UInt8Item('\f'));
				list.Add(new CFFFont.UInt8Item('"'));
			}
			list.Add(offsetItem4);
			list.Add(new CFFFont.UInt8Item('\f'));
			list.Add(new CFFFont.UInt8Item('$'));
			list.Add(offsetItem5);
			list.Add(new CFFFont.UInt8Item('\f'));
			list.Add(new CFFFont.UInt8Item('%'));
			list.Add(offsetItem2);
			list.Add(new CFFFont.UInt8Item('\u000f'));
			list.Add(offsetItem3);
			list.Add(new CFFFont.UInt8Item('\u0011'));
			this.Seek(this.topdictOffsets[num]);
			while (this.GetPosition() < this.topdictOffsets[num + 1])
			{
				int position = this.GetPosition();
				this.GetDictItem();
				int position2 = this.GetPosition();
				if (!(this.key == "Encoding") && !(this.key == "Private") && !(this.key == "FDSelect") && !(this.key == "FDArray") && !(this.key == "charset") && !(this.key == "CharStrings"))
				{
					list.Add(new CFFFont.RangeItem(this.buf, position, position2 - position));
				}
			}
			list.Add(new CFFFont.IndexMarkerItem(offsetItem, indexBaseItem));
			if (this.fonts[num].isCID)
			{
				list.Add(this.GetEntireIndexRange(this.stringIndexOffset));
			}
			else
			{
				string text = this.fonts[num].name + "-OneRange";
				if (text.Length > 127)
				{
					text = text.Substring(0, 127);
				}
				string text2 = "AdobeIdentity" + text;
				int num4 = this.stringOffsets[this.stringOffsets.Length - 1] - this.stringOffsets[0];
				int num5 = this.stringOffsets[0] - 1;
				byte b;
				if (num4 + text2.Length <= 255)
				{
					b = 1;
				}
				else if (num4 + text2.Length <= 65535)
				{
					b = 2;
				}
				else if (num4 + text2.Length <= 16777215)
				{
					b = 3;
				}
				else
				{
					b = 4;
				}
				list.Add(new CFFFont.UInt16Item((char)(this.stringOffsets.Length - 1 + 3)));
				list.Add(new CFFFont.UInt8Item((char)b));
				foreach (int num6 in this.stringOffsets)
				{
					list.Add(new CFFFont.IndexOffsetItem((int)b, num6 - num5));
				}
				int num7 = this.stringOffsets[this.stringOffsets.Length - 1] - num5;
				num7 += "Adobe".Length;
				list.Add(new CFFFont.IndexOffsetItem((int)b, num7));
				num7 += "Identity".Length;
				list.Add(new CFFFont.IndexOffsetItem((int)b, num7));
				num7 += text.Length;
				list.Add(new CFFFont.IndexOffsetItem((int)b, num7));
				list.Add(new CFFFont.RangeItem(this.buf, this.stringOffsets[0], num4));
				list.Add(new CFFFont.StringItem(text2));
			}
			list.Add(this.GetEntireIndexRange(this.gsubrIndexOffset));
			if (!this.fonts[num].isCID)
			{
				list.Add(new CFFFont.MarkerItem(offsetItem5));
				list.Add(new CFFFont.UInt8Item('\u0003'));
				list.Add(new CFFFont.UInt16Item('\u0001'));
				list.Add(new CFFFont.UInt16Item('\0'));
				list.Add(new CFFFont.UInt8Item('\0'));
				list.Add(new CFFFont.UInt16Item((char)num2));
				list.Add(new CFFFont.MarkerItem(offsetItem2));
				list.Add(new CFFFont.UInt8Item('\u0002'));
				list.Add(new CFFFont.UInt16Item('\u0001'));
				list.Add(new CFFFont.UInt16Item((char)(num2 - 1)));
				list.Add(new CFFFont.MarkerItem(offsetItem4));
				list.Add(new CFFFont.UInt16Item('\u0001'));
				list.Add(new CFFFont.UInt8Item('\u0001'));
				list.Add(new CFFFont.UInt8Item('\u0001'));
				CFFFont.OffsetItem offsetItem6 = new CFFFont.IndexOffsetItem(1);
				list.Add(offsetItem6);
				CFFFont.IndexBaseItem indexBaseItem2 = new CFFFont.IndexBaseItem();
				list.Add(indexBaseItem2);
				list.Add(new CFFFont.DictNumberItem(this.fonts[num].privateLength));
				CFFFont.OffsetItem offsetItem7 = new CFFFont.DictOffsetItem();
				list.Add(offsetItem7);
				list.Add(new CFFFont.UInt8Item('\u0012'));
				list.Add(new CFFFont.IndexMarkerItem(offsetItem6, indexBaseItem2));
				list.Add(new CFFFont.MarkerItem(offsetItem7));
				list.Add(new CFFFont.RangeItem(this.buf, this.fonts[num].privateOffset, this.fonts[num].privateLength));
				if (this.fonts[num].privateSubrs >= 0)
				{
					list.Add(this.GetEntireIndexRange(this.fonts[num].privateSubrs));
				}
			}
			list.Add(new CFFFont.MarkerItem(offsetItem3));
			list.Add(this.GetEntireIndexRange(this.fonts[num].charstringsOffset));
			int[] array2 = new int[]
			{
				0
			};
			foreach (CFFFont.Item item in list)
			{
				item.Increment(array2);
			}
			foreach (CFFFont.Item item2 in list)
			{
				item2.Xref();
			}
			int num8 = array2[0];
			byte[] array3 = new byte[num8];
			foreach (CFFFont.Item item3 in list)
			{
				item3.Emit(array3);
			}
			return array3;
		}

		// Token: 0x060020A1 RID: 8353 RVA: 0x000C2630 File Offset: 0x000C1630
		public bool IsCID(string fontName)
		{
			for (int i = 0; i < this.fonts.Length; i++)
			{
				if (fontName.Equals(this.fonts[i].name))
				{
					return this.fonts[i].isCID;
				}
			}
			return false;
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x000C2674 File Offset: 0x000C1674
		public bool Exists(string fontName)
		{
			for (int i = 0; i < this.fonts.Length; i++)
			{
				if (fontName.Equals(this.fonts[i].name))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x000C26AC File Offset: 0x000C16AC
		public string[] GetNames()
		{
			string[] array = new string[this.fonts.Length];
			for (int i = 0; i < this.fonts.Length; i++)
			{
				array[i] = this.fonts[i].name;
			}
			return array;
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x000C26EC File Offset: 0x000C16EC
		public CFFFont(RandomAccessFileOrArray inputbuffer)
		{
			this.buf = inputbuffer;
			this.Seek(0);
			this.GetCard8();
			this.GetCard8();
			int card = (int)this.GetCard8();
			this.offSize = (int)this.GetCard8();
			this.nameIndexOffset = card;
			this.nameOffsets = this.GetIndex(this.nameIndexOffset);
			this.topdictIndexOffset = this.nameOffsets[this.nameOffsets.Length - 1];
			this.topdictOffsets = this.GetIndex(this.topdictIndexOffset);
			this.stringIndexOffset = this.topdictOffsets[this.topdictOffsets.Length - 1];
			this.stringOffsets = this.GetIndex(this.stringIndexOffset);
			this.gsubrIndexOffset = this.stringOffsets[this.stringOffsets.Length - 1];
			this.gsubrOffsets = this.GetIndex(this.gsubrIndexOffset);
			this.fonts = new CFFFont.Font[this.nameOffsets.Length - 1];
			for (int i = 0; i < this.nameOffsets.Length - 1; i++)
			{
				this.fonts[i] = new CFFFont.Font();
				this.Seek(this.nameOffsets[i]);
				this.fonts[i].name = "";
				for (int j = this.nameOffsets[i]; j < this.nameOffsets[i + 1]; j++)
				{
					CFFFont.Font font = this.fonts[i];
					font.name += this.GetCard8();
				}
			}
			for (int k = 0; k < this.topdictOffsets.Length - 1; k++)
			{
				this.Seek(this.topdictOffsets[k]);
				while (this.GetPosition() < this.topdictOffsets[k + 1])
				{
					this.GetDictItem();
					if (this.key == "FullName")
					{
						this.fonts[k].fullName = this.GetString((char)((int)this.args[0]));
					}
					else if (this.key == "ROS")
					{
						this.fonts[k].isCID = true;
					}
					else if (this.key == "Private")
					{
						this.fonts[k].privateLength = (int)this.args[0];
						this.fonts[k].privateOffset = (int)this.args[1];
					}
					else if (this.key == "charset")
					{
						this.fonts[k].charsetOffset = (int)this.args[0];
					}
					else if (this.key == "Encoding")
					{
						this.fonts[k].encodingOffset = (int)this.args[0];
						this.ReadEncoding(this.fonts[k].encodingOffset);
					}
					else if (this.key == "CharStrings")
					{
						this.fonts[k].charstringsOffset = (int)this.args[0];
						int position = this.GetPosition();
						this.fonts[k].charstringsOffsets = this.GetIndex(this.fonts[k].charstringsOffset);
						this.Seek(position);
					}
					else if (this.key == "FDArray")
					{
						this.fonts[k].fdarrayOffset = (int)this.args[0];
					}
					else if (this.key == "FDSelect")
					{
						this.fonts[k].fdselectOffset = (int)this.args[0];
					}
					else if (this.key == "CharstringType")
					{
						this.fonts[k].CharstringType = (int)this.args[0];
					}
				}
				if (this.fonts[k].privateOffset >= 0)
				{
					this.Seek(this.fonts[k].privateOffset);
					while (this.GetPosition() < this.fonts[k].privateOffset + this.fonts[k].privateLength)
					{
						this.GetDictItem();
						if (this.key == "Subrs")
						{
							this.fonts[k].privateSubrs = (int)this.args[0] + this.fonts[k].privateOffset;
						}
					}
				}
				if (this.fonts[k].fdarrayOffset >= 0)
				{
					int[] index = this.GetIndex(this.fonts[k].fdarrayOffset);
					this.fonts[k].fdprivateOffsets = new int[index.Length - 1];
					this.fonts[k].fdprivateLengths = new int[index.Length - 1];
					for (int l = 0; l < index.Length - 1; l++)
					{
						this.Seek(index[l]);
						while (this.GetPosition() < index[l + 1])
						{
							this.GetDictItem();
						}
						if (this.key == "Private")
						{
							this.fonts[k].fdprivateLengths[l] = (int)this.args[0];
							this.fonts[k].fdprivateOffsets[l] = (int)this.args[1];
						}
					}
				}
			}
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x000C2C17 File Offset: 0x000C1C17
		internal void ReadEncoding(int nextIndexOffset)
		{
			this.Seek(nextIndexOffset);
			this.GetCard8();
		}

		// Token: 0x04001664 RID: 5732
		internal static string[] operatorNames = new string[]
		{
			"version",
			"Notice",
			"FullName",
			"FamilyName",
			"Weight",
			"FontBBox",
			"BlueValues",
			"OtherBlues",
			"FamilyBlues",
			"FamilyOtherBlues",
			"StdHW",
			"StdVW",
			"UNKNOWN_12",
			"UniqueID",
			"XUID",
			"charset",
			"Encoding",
			"CharStrings",
			"Private",
			"Subrs",
			"defaultWidthX",
			"nominalWidthX",
			"UNKNOWN_22",
			"UNKNOWN_23",
			"UNKNOWN_24",
			"UNKNOWN_25",
			"UNKNOWN_26",
			"UNKNOWN_27",
			"UNKNOWN_28",
			"UNKNOWN_29",
			"UNKNOWN_30",
			"UNKNOWN_31",
			"Copyright",
			"isFixedPitch",
			"ItalicAngle",
			"UnderlinePosition",
			"UnderlineThickness",
			"PaintType",
			"CharstringType",
			"FontMatrix",
			"StrokeWidth",
			"BlueScale",
			"BlueShift",
			"BlueFuzz",
			"StemSnapH",
			"StemSnapV",
			"ForceBold",
			"UNKNOWN_12_15",
			"UNKNOWN_12_16",
			"LanguageGroup",
			"ExpansionFactor",
			"initialRandomSeed",
			"SyntheticBase",
			"PostScript",
			"BaseFontName",
			"BaseFontBlend",
			"UNKNOWN_12_24",
			"UNKNOWN_12_25",
			"UNKNOWN_12_26",
			"UNKNOWN_12_27",
			"UNKNOWN_12_28",
			"UNKNOWN_12_29",
			"ROS",
			"CIDFontVersion",
			"CIDFontRevision",
			"CIDFontType",
			"CIDCount",
			"UIDBase",
			"FDArray",
			"FDSelect",
			"FontName"
		};

		// Token: 0x04001665 RID: 5733
		internal static string[] standardStrings = new string[]
		{
			".notdef",
			"space",
			"exclam",
			"quotedbl",
			"numbersign",
			"dollar",
			"percent",
			"ampersand",
			"quoteright",
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
			"quoteleft",
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
			"exclamdown",
			"cent",
			"sterling",
			"fraction",
			"yen",
			"florin",
			"section",
			"currency",
			"quotesingle",
			"quotedblleft",
			"guillemotleft",
			"guilsinglleft",
			"guilsinglright",
			"fi",
			"fl",
			"endash",
			"dagger",
			"daggerdbl",
			"periodcentered",
			"paragraph",
			"bullet",
			"quotesinglbase",
			"quotedblbase",
			"quotedblright",
			"guillemotright",
			"ellipsis",
			"perthousand",
			"questiondown",
			"grave",
			"acute",
			"circumflex",
			"tilde",
			"macron",
			"breve",
			"dotaccent",
			"dieresis",
			"ring",
			"cedilla",
			"hungarumlaut",
			"ogonek",
			"caron",
			"emdash",
			"AE",
			"ordfeminine",
			"Lslash",
			"Oslash",
			"OE",
			"ordmasculine",
			"ae",
			"dotlessi",
			"lslash",
			"oslash",
			"oe",
			"germandbls",
			"onesuperior",
			"logicalnot",
			"mu",
			"trademark",
			"Eth",
			"onehalf",
			"plusminus",
			"Thorn",
			"onequarter",
			"divide",
			"brokenbar",
			"degree",
			"thorn",
			"threequarters",
			"twosuperior",
			"registered",
			"minus",
			"eth",
			"multiply",
			"threesuperior",
			"copyright",
			"Aacute",
			"Acircumflex",
			"Adieresis",
			"Agrave",
			"Aring",
			"Atilde",
			"Ccedilla",
			"Eacute",
			"Ecircumflex",
			"Edieresis",
			"Egrave",
			"Iacute",
			"Icircumflex",
			"Idieresis",
			"Igrave",
			"Ntilde",
			"Oacute",
			"Ocircumflex",
			"Odieresis",
			"Ograve",
			"Otilde",
			"Scaron",
			"Uacute",
			"Ucircumflex",
			"Udieresis",
			"Ugrave",
			"Yacute",
			"Ydieresis",
			"Zcaron",
			"aacute",
			"acircumflex",
			"adieresis",
			"agrave",
			"aring",
			"atilde",
			"ccedilla",
			"eacute",
			"ecircumflex",
			"edieresis",
			"egrave",
			"iacute",
			"icircumflex",
			"idieresis",
			"igrave",
			"ntilde",
			"oacute",
			"ocircumflex",
			"odieresis",
			"ograve",
			"otilde",
			"scaron",
			"uacute",
			"ucircumflex",
			"udieresis",
			"ugrave",
			"yacute",
			"ydieresis",
			"zcaron",
			"exclamsmall",
			"Hungarumlautsmall",
			"dollaroldstyle",
			"dollarsuperior",
			"ampersandsmall",
			"Acutesmall",
			"parenleftsuperior",
			"parenrightsuperior",
			"twodotenleader",
			"onedotenleader",
			"zerooldstyle",
			"oneoldstyle",
			"twooldstyle",
			"threeoldstyle",
			"fouroldstyle",
			"fiveoldstyle",
			"sixoldstyle",
			"sevenoldstyle",
			"eightoldstyle",
			"nineoldstyle",
			"commasuperior",
			"threequartersemdash",
			"periodsuperior",
			"questionsmall",
			"asuperior",
			"bsuperior",
			"centsuperior",
			"dsuperior",
			"esuperior",
			"isuperior",
			"lsuperior",
			"msuperior",
			"nsuperior",
			"osuperior",
			"rsuperior",
			"ssuperior",
			"tsuperior",
			"ff",
			"ffi",
			"ffl",
			"parenleftinferior",
			"parenrightinferior",
			"Circumflexsmall",
			"hyphensuperior",
			"Gravesmall",
			"Asmall",
			"Bsmall",
			"Csmall",
			"Dsmall",
			"Esmall",
			"Fsmall",
			"Gsmall",
			"Hsmall",
			"Ismall",
			"Jsmall",
			"Ksmall",
			"Lsmall",
			"Msmall",
			"Nsmall",
			"Osmall",
			"Psmall",
			"Qsmall",
			"Rsmall",
			"Ssmall",
			"Tsmall",
			"Usmall",
			"Vsmall",
			"Wsmall",
			"Xsmall",
			"Ysmall",
			"Zsmall",
			"colonmonetary",
			"onefitted",
			"rupiah",
			"Tildesmall",
			"exclamdownsmall",
			"centoldstyle",
			"Lslashsmall",
			"Scaronsmall",
			"Zcaronsmall",
			"Dieresissmall",
			"Brevesmall",
			"Caronsmall",
			"Dotaccentsmall",
			"Macronsmall",
			"figuredash",
			"hypheninferior",
			"Ogoneksmall",
			"Ringsmall",
			"Cedillasmall",
			"questiondownsmall",
			"oneeighth",
			"threeeighths",
			"fiveeighths",
			"seveneighths",
			"onethird",
			"twothirds",
			"zerosuperior",
			"foursuperior",
			"fivesuperior",
			"sixsuperior",
			"sevensuperior",
			"eightsuperior",
			"ninesuperior",
			"zeroinferior",
			"oneinferior",
			"twoinferior",
			"threeinferior",
			"fourinferior",
			"fiveinferior",
			"sixinferior",
			"seveninferior",
			"eightinferior",
			"nineinferior",
			"centinferior",
			"dollarinferior",
			"periodinferior",
			"commainferior",
			"Agravesmall",
			"Aacutesmall",
			"Acircumflexsmall",
			"Atildesmall",
			"Adieresissmall",
			"Aringsmall",
			"AEsmall",
			"Ccedillasmall",
			"Egravesmall",
			"Eacutesmall",
			"Ecircumflexsmall",
			"Edieresissmall",
			"Igravesmall",
			"Iacutesmall",
			"Icircumflexsmall",
			"Idieresissmall",
			"Ethsmall",
			"Ntildesmall",
			"Ogravesmall",
			"Oacutesmall",
			"Ocircumflexsmall",
			"Otildesmall",
			"Odieresissmall",
			"OEsmall",
			"Oslashsmall",
			"Ugravesmall",
			"Uacutesmall",
			"Ucircumflexsmall",
			"Udieresissmall",
			"Yacutesmall",
			"Thornsmall",
			"Ydieresissmall",
			"001.000",
			"001.001",
			"001.002",
			"001.003",
			"Black",
			"Bold",
			"Book",
			"Light",
			"Medium",
			"Regular",
			"Roman",
			"Semibold"
		};

		// Token: 0x04001666 RID: 5734
		internal int nextIndexOffset;

		// Token: 0x04001667 RID: 5735
		protected string key;

		// Token: 0x04001668 RID: 5736
		protected object[] args = new object[48];

		// Token: 0x04001669 RID: 5737
		protected int arg_count;

		// Token: 0x0400166A RID: 5738
		protected RandomAccessFileOrArray buf;

		// Token: 0x0400166B RID: 5739
		private int offSize;

		// Token: 0x0400166C RID: 5740
		protected int nameIndexOffset;

		// Token: 0x0400166D RID: 5741
		protected int topdictIndexOffset;

		// Token: 0x0400166E RID: 5742
		protected int stringIndexOffset;

		// Token: 0x0400166F RID: 5743
		protected int gsubrIndexOffset;

		// Token: 0x04001670 RID: 5744
		protected int[] nameOffsets;

		// Token: 0x04001671 RID: 5745
		protected int[] topdictOffsets;

		// Token: 0x04001672 RID: 5746
		protected int[] stringOffsets;

		// Token: 0x04001673 RID: 5747
		protected int[] gsubrOffsets;

		// Token: 0x04001674 RID: 5748
		protected CFFFont.Font[] fonts;

		// Token: 0x020003A8 RID: 936
		protected internal abstract class Item
		{
			// Token: 0x060020A7 RID: 8359 RVA: 0x000C3F95 File Offset: 0x000C2F95
			public virtual void Increment(int[] currentOffset)
			{
				this.myOffset = currentOffset[0];
			}

			// Token: 0x060020A8 RID: 8360 RVA: 0x000C3FA0 File Offset: 0x000C2FA0
			public virtual void Emit(byte[] buffer)
			{
			}

			// Token: 0x060020A9 RID: 8361 RVA: 0x000C3FA2 File Offset: 0x000C2FA2
			public virtual void Xref()
			{
			}

			// Token: 0x04001675 RID: 5749
			protected internal int myOffset = -1;
		}

		// Token: 0x020003A9 RID: 937
		protected internal abstract class OffsetItem : CFFFont.Item
		{
			// Token: 0x060020AB RID: 8363 RVA: 0x000C3FB3 File Offset: 0x000C2FB3
			public void Set(int offset)
			{
				this.value = offset;
			}

			// Token: 0x04001676 RID: 5750
			public int value;
		}

		// Token: 0x020003AA RID: 938
		protected internal class RangeItem : CFFFont.Item
		{
			// Token: 0x060020AD RID: 8365 RVA: 0x000C3FC4 File Offset: 0x000C2FC4
			public RangeItem(RandomAccessFileOrArray buf, int offset, int length)
			{
				this.offset = offset;
				this.length = length;
				this.buf = buf;
			}

			// Token: 0x060020AE RID: 8366 RVA: 0x000C3FE1 File Offset: 0x000C2FE1
			public override void Increment(int[] currentOffset)
			{
				base.Increment(currentOffset);
				currentOffset[0] += this.length;
			}

			// Token: 0x060020AF RID: 8367 RVA: 0x000C4004 File Offset: 0x000C3004
			public override void Emit(byte[] buffer)
			{
				this.buf.Seek(this.offset);
				for (int i = this.myOffset; i < this.myOffset + this.length; i++)
				{
					buffer[i] = this.buf.ReadByte();
				}
			}

			// Token: 0x04001677 RID: 5751
			public int offset;

			// Token: 0x04001678 RID: 5752
			public int length;

			// Token: 0x04001679 RID: 5753
			private RandomAccessFileOrArray buf;
		}

		// Token: 0x020003AB RID: 939
		protected internal class IndexOffsetItem : CFFFont.OffsetItem
		{
			// Token: 0x060020B0 RID: 8368 RVA: 0x000C404D File Offset: 0x000C304D
			public IndexOffsetItem(int size, int value)
			{
				this.size = size;
				this.value = value;
			}

			// Token: 0x060020B1 RID: 8369 RVA: 0x000C4063 File Offset: 0x000C3063
			public IndexOffsetItem(int size)
			{
				this.size = size;
			}

			// Token: 0x060020B2 RID: 8370 RVA: 0x000C4072 File Offset: 0x000C3072
			public override void Increment(int[] currentOffset)
			{
				base.Increment(currentOffset);
				currentOffset[0] += this.size;
			}

			// Token: 0x060020B3 RID: 8371 RVA: 0x000C4094 File Offset: 0x000C3094
			public override void Emit(byte[] buffer)
			{
				int num = 0;
				switch (this.size)
				{
				case 1:
					goto IL_7B;
				case 2:
					goto IL_5E;
				case 3:
					break;
				case 4:
					buffer[this.myOffset + num] = (byte)(this.value >> 24 & 255);
					num++;
					break;
				default:
					return;
				}
				buffer[this.myOffset + num] = (byte)(this.value >> 16 & 255);
				num++;
				IL_5E:
				buffer[this.myOffset + num] = (byte)(this.value >> 8 & 255);
				num++;
				IL_7B:
				buffer[this.myOffset + num] = (byte)(this.value & 255);
				num++;
			}

			// Token: 0x0400167A RID: 5754
			public int size;
		}

		// Token: 0x020003AC RID: 940
		protected internal class IndexBaseItem : CFFFont.Item
		{
		}

		// Token: 0x020003AD RID: 941
		protected internal class IndexMarkerItem : CFFFont.Item
		{
			// Token: 0x060020B5 RID: 8373 RVA: 0x000C413F File Offset: 0x000C313F
			public IndexMarkerItem(CFFFont.OffsetItem offItem, CFFFont.IndexBaseItem indexBase)
			{
				this.offItem = offItem;
				this.indexBase = indexBase;
			}

			// Token: 0x060020B6 RID: 8374 RVA: 0x000C4155 File Offset: 0x000C3155
			public override void Xref()
			{
				this.offItem.Set(this.myOffset - this.indexBase.myOffset + 1);
			}

			// Token: 0x0400167B RID: 5755
			private CFFFont.OffsetItem offItem;

			// Token: 0x0400167C RID: 5756
			private CFFFont.IndexBaseItem indexBase;
		}

		// Token: 0x020003AE RID: 942
		protected internal class SubrMarkerItem : CFFFont.Item
		{
			// Token: 0x060020B7 RID: 8375 RVA: 0x000C4176 File Offset: 0x000C3176
			public SubrMarkerItem(CFFFont.OffsetItem offItem, CFFFont.IndexBaseItem indexBase)
			{
				this.offItem = offItem;
				this.indexBase = indexBase;
			}

			// Token: 0x060020B8 RID: 8376 RVA: 0x000C418C File Offset: 0x000C318C
			public override void Xref()
			{
				this.offItem.Set(this.myOffset - this.indexBase.myOffset);
			}

			// Token: 0x0400167D RID: 5757
			private CFFFont.OffsetItem offItem;

			// Token: 0x0400167E RID: 5758
			private CFFFont.IndexBaseItem indexBase;
		}

		// Token: 0x020003AF RID: 943
		protected internal class DictOffsetItem : CFFFont.OffsetItem
		{
			// Token: 0x060020B9 RID: 8377 RVA: 0x000C41AB File Offset: 0x000C31AB
			public DictOffsetItem()
			{
				this.size = 5;
			}

			// Token: 0x060020BA RID: 8378 RVA: 0x000C41BA File Offset: 0x000C31BA
			public override void Increment(int[] currentOffset)
			{
				base.Increment(currentOffset);
				currentOffset[0] += this.size;
			}

			// Token: 0x060020BB RID: 8379 RVA: 0x000C41DC File Offset: 0x000C31DC
			public override void Emit(byte[] buffer)
			{
				if (this.size == 5)
				{
					buffer[this.myOffset] = 29;
					buffer[this.myOffset + 1] = (byte)(this.value >> 24 & 255);
					buffer[this.myOffset + 2] = (byte)(this.value >> 16 & 255);
					buffer[this.myOffset + 3] = (byte)(this.value >> 8 & 255);
					buffer[this.myOffset + 4] = (byte)(this.value & 255);
				}
			}

			// Token: 0x0400167F RID: 5759
			public int size;
		}

		// Token: 0x020003B0 RID: 944
		protected internal class UInt24Item : CFFFont.Item
		{
			// Token: 0x060020BC RID: 8380 RVA: 0x000C4260 File Offset: 0x000C3260
			public UInt24Item(int value)
			{
				this.value = value;
			}

			// Token: 0x060020BD RID: 8381 RVA: 0x000C426F File Offset: 0x000C326F
			public override void Increment(int[] currentOffset)
			{
				base.Increment(currentOffset);
				currentOffset[0] += 3;
			}

			// Token: 0x060020BE RID: 8382 RVA: 0x000C428C File Offset: 0x000C328C
			public override void Emit(byte[] buffer)
			{
				buffer[this.myOffset] = (byte)(this.value >> 16 & 255);
				buffer[this.myOffset + 1] = (byte)(this.value >> 8 & 255);
				buffer[this.myOffset + 2] = (byte)(this.value & 255);
			}

			// Token: 0x04001680 RID: 5760
			public int value;
		}

		// Token: 0x020003B1 RID: 945
		protected internal class UInt32Item : CFFFont.Item
		{
			// Token: 0x060020BF RID: 8383 RVA: 0x000C42E1 File Offset: 0x000C32E1
			public UInt32Item(int value)
			{
				this.value = value;
			}

			// Token: 0x060020C0 RID: 8384 RVA: 0x000C42F0 File Offset: 0x000C32F0
			public override void Increment(int[] currentOffset)
			{
				base.Increment(currentOffset);
				currentOffset[0] += 4;
			}

			// Token: 0x060020C1 RID: 8385 RVA: 0x000C4310 File Offset: 0x000C3310
			public override void Emit(byte[] buffer)
			{
				buffer[this.myOffset] = (byte)(this.value >> 24 & 255);
				buffer[this.myOffset + 1] = (byte)(this.value >> 16 & 255);
				buffer[this.myOffset + 2] = (byte)(this.value >> 8 & 255);
				buffer[this.myOffset + 3] = (byte)(this.value & 255);
			}

			// Token: 0x04001681 RID: 5761
			public int value;
		}

		// Token: 0x020003B2 RID: 946
		protected internal class UInt16Item : CFFFont.Item
		{
			// Token: 0x060020C2 RID: 8386 RVA: 0x000C437F File Offset: 0x000C337F
			public UInt16Item(char value)
			{
				this.value = value;
			}

			// Token: 0x060020C3 RID: 8387 RVA: 0x000C438E File Offset: 0x000C338E
			public override void Increment(int[] currentOffset)
			{
				base.Increment(currentOffset);
				currentOffset[0] += 2;
			}

			// Token: 0x060020C4 RID: 8388 RVA: 0x000C43AB File Offset: 0x000C33AB
			public override void Emit(byte[] buffer)
			{
				buffer[this.myOffset] = (byte)(this.value >> 8 & 'ÿ');
				buffer[this.myOffset + 1] = (byte)(this.value & 'ÿ');
			}

			// Token: 0x04001682 RID: 5762
			public char value;
		}

		// Token: 0x020003B3 RID: 947
		protected internal class UInt8Item : CFFFont.Item
		{
			// Token: 0x060020C5 RID: 8389 RVA: 0x000C43DB File Offset: 0x000C33DB
			public UInt8Item(char value)
			{
				this.value = value;
			}

			// Token: 0x060020C6 RID: 8390 RVA: 0x000C43EA File Offset: 0x000C33EA
			public override void Increment(int[] currentOffset)
			{
				base.Increment(currentOffset);
				currentOffset[0]++;
			}

			// Token: 0x060020C7 RID: 8391 RVA: 0x000C4407 File Offset: 0x000C3407
			public override void Emit(byte[] buffer)
			{
				buffer[this.myOffset] = (byte)(this.value & 'ÿ');
			}

			// Token: 0x04001683 RID: 5763
			public char value;
		}

		// Token: 0x020003B4 RID: 948
		protected internal class StringItem : CFFFont.Item
		{
			// Token: 0x060020C8 RID: 8392 RVA: 0x000C441E File Offset: 0x000C341E
			public StringItem(string s)
			{
				this.s = s;
			}

			// Token: 0x060020C9 RID: 8393 RVA: 0x000C442D File Offset: 0x000C342D
			public override void Increment(int[] currentOffset)
			{
				base.Increment(currentOffset);
				currentOffset[0] += this.s.Length;
			}

			// Token: 0x060020CA RID: 8394 RVA: 0x000C4454 File Offset: 0x000C3454
			public override void Emit(byte[] buffer)
			{
				for (int i = 0; i < this.s.Length; i++)
				{
					buffer[this.myOffset + i] = (byte)(this.s[i] & 'ÿ');
				}
			}

			// Token: 0x04001684 RID: 5764
			public string s;
		}

		// Token: 0x020003B5 RID: 949
		protected internal class DictNumberItem : CFFFont.Item
		{
			// Token: 0x060020CB RID: 8395 RVA: 0x000C4494 File Offset: 0x000C3494
			public DictNumberItem(int value)
			{
				this.value = value;
			}

			// Token: 0x060020CC RID: 8396 RVA: 0x000C44AA File Offset: 0x000C34AA
			public override void Increment(int[] currentOffset)
			{
				base.Increment(currentOffset);
				currentOffset[0] += this.size;
			}

			// Token: 0x060020CD RID: 8397 RVA: 0x000C44CC File Offset: 0x000C34CC
			public override void Emit(byte[] buffer)
			{
				if (this.size == 5)
				{
					buffer[this.myOffset] = 29;
					buffer[this.myOffset + 1] = (byte)(this.value >> 24 & 255);
					buffer[this.myOffset + 2] = (byte)(this.value >> 16 & 255);
					buffer[this.myOffset + 3] = (byte)(this.value >> 8 & 255);
					buffer[this.myOffset + 4] = (byte)(this.value & 255);
				}
			}

			// Token: 0x04001685 RID: 5765
			public int value;

			// Token: 0x04001686 RID: 5766
			public int size = 5;
		}

		// Token: 0x020003B6 RID: 950
		protected internal class MarkerItem : CFFFont.Item
		{
			// Token: 0x060020CE RID: 8398 RVA: 0x000C4550 File Offset: 0x000C3550
			public MarkerItem(CFFFont.OffsetItem pointerToMarker)
			{
				this.p = pointerToMarker;
			}

			// Token: 0x060020CF RID: 8399 RVA: 0x000C455F File Offset: 0x000C355F
			public override void Xref()
			{
				this.p.Set(this.myOffset);
			}

			// Token: 0x04001687 RID: 5767
			private CFFFont.OffsetItem p;
		}

		// Token: 0x020003B7 RID: 951
		protected internal class Font
		{
			// Token: 0x04001688 RID: 5768
			public string name;

			// Token: 0x04001689 RID: 5769
			public string fullName;

			// Token: 0x0400168A RID: 5770
			public bool isCID;

			// Token: 0x0400168B RID: 5771
			public int privateOffset = -1;

			// Token: 0x0400168C RID: 5772
			public int privateLength = -1;

			// Token: 0x0400168D RID: 5773
			public int privateSubrs = -1;

			// Token: 0x0400168E RID: 5774
			public int charstringsOffset = -1;

			// Token: 0x0400168F RID: 5775
			public int encodingOffset = -1;

			// Token: 0x04001690 RID: 5776
			public int charsetOffset = -1;

			// Token: 0x04001691 RID: 5777
			public int fdarrayOffset = -1;

			// Token: 0x04001692 RID: 5778
			public int fdselectOffset = -1;

			// Token: 0x04001693 RID: 5779
			public int[] fdprivateOffsets;

			// Token: 0x04001694 RID: 5780
			public int[] fdprivateLengths;

			// Token: 0x04001695 RID: 5781
			public int[] fdprivateSubrs;

			// Token: 0x04001696 RID: 5782
			public int nglyphs;

			// Token: 0x04001697 RID: 5783
			public int nstrings;

			// Token: 0x04001698 RID: 5784
			public int CharsetLength;

			// Token: 0x04001699 RID: 5785
			public int[] charstringsOffsets;

			// Token: 0x0400169A RID: 5786
			public int[] charset;

			// Token: 0x0400169B RID: 5787
			public int[] FDSelect;

			// Token: 0x0400169C RID: 5788
			public int FDSelectLength;

			// Token: 0x0400169D RID: 5789
			public int FDSelectFormat;

			// Token: 0x0400169E RID: 5790
			public int CharstringType = 2;

			// Token: 0x0400169F RID: 5791
			public int FDArrayCount;

			// Token: 0x040016A0 RID: 5792
			public int FDArrayOffsize;

			// Token: 0x040016A1 RID: 5793
			public int[] FDArrayOffsets;

			// Token: 0x040016A2 RID: 5794
			public int[] PrivateSubrsOffset;

			// Token: 0x040016A3 RID: 5795
			public int[][] PrivateSubrsOffsetsArray;

			// Token: 0x040016A4 RID: 5796
			public int[] SubrsOffsets;
		}
	}
}
