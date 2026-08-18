using System;
using System.Collections.Generic;

namespace iTextSharp.text.pdf
{
	// Token: 0x020003A5 RID: 933
	public class DocumentFont : BaseFont
	{
		// Token: 0x0600206F RID: 8303 RVA: 0x000C027C File Offset: 0x000BF27C
		internal DocumentFont(PRIndirectReference refFont)
		{
			this.encoding = "";
			this.fontSpecific = false;
			this.refFont = refFont;
			this.fontType = 4;
			this.font = (PdfDictionary)PdfReader.GetPdfObject(refFont);
			this.fontName = PdfName.DecodeName(this.font.GetAsName(PdfName.BASEFONT).ToString());
			PdfName asName = this.font.GetAsName(PdfName.SUBTYPE);
			if (PdfName.TYPE1.Equals(asName) || PdfName.TRUETYPE.Equals(asName))
			{
				this.DoType1TT();
				return;
			}
			for (int i = 0; i < DocumentFont.cjkNames.Length; i++)
			{
				if (this.fontName.StartsWith(DocumentFont.cjkNames[i]))
				{
					this.fontName = DocumentFont.cjkNames[i];
					this.cjkMirror = BaseFont.CreateFont(this.fontName, DocumentFont.cjkEncs[i], false);
					return;
				}
			}
			string text = PdfName.DecodeName(this.font.GetAsName(PdfName.ENCODING).ToString());
			for (int j = 0; j < DocumentFont.cjkEncs2.Length; j++)
			{
				if (text.StartsWith(DocumentFont.cjkEncs2[j]))
				{
					if (j > 3)
					{
						j -= 4;
					}
					this.cjkMirror = BaseFont.CreateFont(DocumentFont.cjkNames2[j], DocumentFont.cjkEncs2[j], false);
					return;
				}
			}
			if (PdfName.TYPE0.Equals(asName) && text.Equals("Identity-H"))
			{
				this.ProcessType0(this.font);
				this.isType0 = true;
			}
		}

		// Token: 0x06002070 RID: 8304 RVA: 0x000C0450 File Offset: 0x000BF450
		private void ProcessType0(PdfDictionary font)
		{
			PdfObject pdfObjectRelease = PdfReader.GetPdfObjectRelease(font.Get(PdfName.TOUNICODE));
			PdfArray pdfArray = (PdfArray)PdfReader.GetPdfObjectRelease(font.Get(PdfName.DESCENDANTFONTS));
			PdfDictionary pdfDictionary = (PdfDictionary)PdfReader.GetPdfObjectRelease(pdfArray[0]);
			PdfNumber pdfNumber = (PdfNumber)PdfReader.GetPdfObjectRelease(pdfDictionary.Get(PdfName.DW));
			int dw = 1000;
			if (pdfNumber != null)
			{
				dw = pdfNumber.IntValue;
			}
			IntHashtable widths = this.ReadWidths((PdfArray)PdfReader.GetPdfObjectRelease(pdfDictionary.Get(PdfName.W)));
			PdfDictionary fontDesc = (PdfDictionary)PdfReader.GetPdfObjectRelease(pdfDictionary.Get(PdfName.FONTDESCRIPTOR));
			this.FillFontDesc(fontDesc);
			if (pdfObjectRelease != null)
			{
				this.FillMetrics(PdfReader.GetStreamBytes((PRStream)pdfObjectRelease), widths, dw);
			}
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x000C0514 File Offset: 0x000BF514
		private IntHashtable ReadWidths(PdfArray ws)
		{
			IntHashtable intHashtable = new IntHashtable();
			if (ws == null)
			{
				return intHashtable;
			}
			for (int i = 0; i < ws.Size; i++)
			{
				int j = ((PdfNumber)PdfReader.GetPdfObjectRelease(ws[i])).IntValue;
				PdfObject pdfObjectRelease = PdfReader.GetPdfObjectRelease(ws[++i]);
				if (pdfObjectRelease.IsArray())
				{
					PdfArray pdfArray = (PdfArray)pdfObjectRelease;
					for (int k = 0; k < pdfArray.Size; k++)
					{
						int intValue = ((PdfNumber)PdfReader.GetPdfObjectRelease(pdfArray[k])).IntValue;
						intHashtable[j++] = intValue;
					}
				}
				else
				{
					int intValue2 = ((PdfNumber)pdfObjectRelease).IntValue;
					int intValue3 = ((PdfNumber)PdfReader.GetPdfObjectRelease(ws[++i])).IntValue;
					while (j <= intValue2)
					{
						intHashtable[j] = intValue3;
						j++;
					}
				}
			}
			return intHashtable;
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x000C05F8 File Offset: 0x000BF5F8
		private string DecodeString(PdfString ps)
		{
			if (ps.IsHexWriting())
			{
				return PdfEncodings.ConvertToString(ps.GetBytes(), "UnicodeBigUnmarked");
			}
			return ps.ToUnicodeString();
		}

		// Token: 0x06002073 RID: 8307 RVA: 0x000C061C File Offset: 0x000BF61C
		private void FillMetrics(byte[] touni, IntHashtable widths, int dw)
		{
			PdfContentParser pdfContentParser = new PdfContentParser(new PRTokeniser(touni));
			PdfObject pdfObject = null;
			PdfObject pdfObject2;
			while ((pdfObject2 = pdfContentParser.ReadPRObject()) != null)
			{
				if (pdfObject2.Type == 200)
				{
					if (pdfObject2.ToString().Equals("beginbfchar"))
					{
						int intValue = ((PdfNumber)pdfObject).IntValue;
						for (int i = 0; i < intValue; i++)
						{
							string text = this.DecodeString((PdfString)pdfContentParser.ReadPRObject());
							string text2 = this.DecodeString((PdfString)pdfContentParser.ReadPRObject());
							if (text2.Length == 1)
							{
								int num = (int)text[0];
								int key = (int)text2[text2.Length - 1];
								int num2 = dw;
								if (widths.ContainsKey(num))
								{
									num2 = widths[num];
								}
								this.metrics[key] = new int[]
								{
									num,
									num2
								};
							}
						}
					}
					else if (pdfObject2.ToString().Equals("beginbfrange"))
					{
						int intValue2 = ((PdfNumber)pdfObject).IntValue;
						for (int j = 0; j < intValue2; j++)
						{
							string text3 = this.DecodeString((PdfString)pdfContentParser.ReadPRObject());
							string text4 = this.DecodeString((PdfString)pdfContentParser.ReadPRObject());
							int k = (int)text3[0];
							int num3 = (int)text4[0];
							PdfObject pdfObject3 = pdfContentParser.ReadPRObject();
							if (pdfObject3.IsString())
							{
								string text5 = this.DecodeString((PdfString)pdfObject3);
								if (text5.Length == 1)
								{
									int num4 = (int)text5[text5.Length - 1];
									while (k <= num3)
									{
										int num5 = dw;
										if (widths.ContainsKey(k))
										{
											num5 = widths[k];
										}
										this.metrics[num4] = new int[]
										{
											k,
											num5
										};
										k++;
										num4++;
									}
								}
							}
							else
							{
								PdfArray pdfArray = (PdfArray)pdfObject3;
								int l = 0;
								while (l < pdfArray.Size)
								{
									string text6 = this.DecodeString(pdfArray.GetAsString(l));
									if (text6.Length == 1)
									{
										int key2 = (int)text6[text6.Length - 1];
										int num6 = dw;
										if (widths.ContainsKey(k))
										{
											num6 = widths[k];
										}
										this.metrics[key2] = new int[]
										{
											k,
											num6
										};
									}
									l++;
									k++;
								}
							}
						}
					}
				}
				else
				{
					pdfObject = pdfObject2;
				}
			}
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x000C08B8 File Offset: 0x000BF8B8
		private void DoType1TT()
		{
			PdfObject pdfObject = PdfReader.GetPdfObject(this.font.Get(PdfName.ENCODING));
			if (pdfObject == null)
			{
				this.FillEncoding(null);
			}
			else if (pdfObject.IsName())
			{
				this.FillEncoding((PdfName)pdfObject);
			}
			else
			{
				PdfDictionary pdfDictionary = (PdfDictionary)pdfObject;
				pdfObject = PdfReader.GetPdfObject(pdfDictionary.Get(PdfName.BASEENCODING));
				if (pdfObject == null)
				{
					this.FillEncoding(null);
				}
				else
				{
					this.FillEncoding((PdfName)pdfObject);
				}
				PdfArray asArray = pdfDictionary.GetAsArray(PdfName.DIFFERENCES);
				if (asArray != null)
				{
					this.diffmap = new IntHashtable();
					int num = 0;
					for (int i = 0; i < asArray.Size; i++)
					{
						PdfObject pdfObject2 = asArray[i];
						if (pdfObject2.IsNumber())
						{
							num = ((PdfNumber)pdfObject2).IntValue;
						}
						else
						{
							int[] array = GlyphList.NameToUnicode(PdfName.DecodeName(((PdfName)pdfObject2).ToString()));
							if (array != null && array.Length > 0)
							{
								this.uni2byte[array[0]] = num;
								this.diffmap[array[0]] = num;
							}
							num++;
						}
					}
				}
			}
			PdfArray asArray2 = this.font.GetAsArray(PdfName.WIDTHS);
			PdfNumber asNumber = this.font.GetAsNumber(PdfName.FIRSTCHAR);
			PdfNumber asNumber2 = this.font.GetAsNumber(PdfName.LASTCHAR);
			if (BaseFont.BuiltinFonts14.ContainsKey(this.fontName))
			{
				BaseFont baseFont = BaseFont.CreateFont(this.fontName, "Cp1252", false);
				int[] array2 = this.uni2byte.ToOrderedKeys();
				for (int j = 0; j < array2.Length; j++)
				{
					int num2 = this.uni2byte[array2[j]];
					this.widths[num2] = baseFont.GetRawWidth(num2, GlyphList.UnicodeToName(array2[j]));
				}
				if (this.diffmap != null)
				{
					array2 = this.diffmap.ToOrderedKeys();
					for (int k = 0; k < array2.Length; k++)
					{
						int num3 = this.diffmap[array2[k]];
						this.widths[num3] = baseFont.GetRawWidth(num3, GlyphList.UnicodeToName(array2[k]));
					}
					this.diffmap = null;
				}
				this.Ascender = baseFont.GetFontDescriptor(1, 1000f);
				this.CapHeight = baseFont.GetFontDescriptor(2, 1000f);
				this.Descender = baseFont.GetFontDescriptor(3, 1000f);
				this.ItalicAngle = baseFont.GetFontDescriptor(4, 1000f);
				this.llx = baseFont.GetFontDescriptor(5, 1000f);
				this.lly = baseFont.GetFontDescriptor(6, 1000f);
				this.urx = baseFont.GetFontDescriptor(7, 1000f);
				this.ury = baseFont.GetFontDescriptor(8, 1000f);
			}
			if (asNumber != null && asNumber2 != null && asArray2 != null)
			{
				int intValue = asNumber.IntValue;
				for (int l = 0; l < asArray2.Size; l++)
				{
					this.widths[intValue + l] = asArray2.GetAsNumber(l).IntValue;
				}
			}
			this.FillFontDesc(this.font.GetAsDict(PdfName.FONTDESCRIPTOR));
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x000C0BCC File Offset: 0x000BFBCC
		private void FillFontDesc(PdfDictionary fontDesc)
		{
			if (fontDesc == null)
			{
				return;
			}
			PdfNumber asNumber = fontDesc.GetAsNumber(PdfName.ASCENT);
			if (asNumber != null)
			{
				this.Ascender = asNumber.FloatValue;
			}
			asNumber = fontDesc.GetAsNumber(PdfName.CAPHEIGHT);
			if (asNumber != null)
			{
				this.CapHeight = asNumber.FloatValue;
			}
			asNumber = fontDesc.GetAsNumber(PdfName.DESCENT);
			if (asNumber != null)
			{
				this.Descender = asNumber.FloatValue;
			}
			asNumber = fontDesc.GetAsNumber(PdfName.ITALICANGLE);
			if (asNumber != null)
			{
				this.ItalicAngle = asNumber.FloatValue;
			}
			PdfArray asArray = fontDesc.GetAsArray(PdfName.FONTBBOX);
			if (asArray != null)
			{
				this.llx = asArray.GetAsNumber(0).FloatValue;
				this.lly = asArray.GetAsNumber(1).FloatValue;
				this.urx = asArray.GetAsNumber(2).FloatValue;
				this.ury = asArray.GetAsNumber(3).FloatValue;
				if (this.llx > this.urx)
				{
					float num = this.llx;
					this.llx = this.urx;
					this.urx = num;
				}
				if (this.lly > this.ury)
				{
					float num2 = this.lly;
					this.lly = this.ury;
					this.ury = num2;
				}
			}
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x000C0CF4 File Offset: 0x000BFCF4
		private void FillEncoding(PdfName encoding)
		{
			if (PdfName.MAC_ROMAN_ENCODING.Equals(encoding) || PdfName.WIN_ANSI_ENCODING.Equals(encoding))
			{
				byte[] array = new byte[256];
				for (int i = 0; i < 256; i++)
				{
					array[i] = (byte)i;
				}
				string encoding2 = "Cp1252";
				if (PdfName.MAC_ROMAN_ENCODING.Equals(encoding))
				{
					encoding2 = "MacRoman";
				}
				string text = PdfEncodings.ConvertToString(array, encoding2);
				char[] array2 = text.ToCharArray();
				for (int j = 0; j < 256; j++)
				{
					this.uni2byte[(int)array2[j]] = j;
				}
				return;
			}
			for (int k = 0; k < 256; k++)
			{
				this.uni2byte[DocumentFont.stdEnc[k]] = k;
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06002077 RID: 8311 RVA: 0x000C0DB4 File Offset: 0x000BFDB4
		public override string[][] FamilyFontName
		{
			get
			{
				return this.FullFontName;
			}
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x000C0DBC File Offset: 0x000BFDBC
		public override float GetFontDescriptor(int key, float fontSize)
		{
			if (this.cjkMirror != null)
			{
				return this.cjkMirror.GetFontDescriptor(key, fontSize);
			}
			switch (key)
			{
			case 1:
			case 9:
				return this.Ascender * fontSize / 1000f;
			case 2:
				return this.CapHeight * fontSize / 1000f;
			case 3:
			case 10:
				return this.Descender * fontSize / 1000f;
			case 4:
				return this.ItalicAngle;
			case 5:
				return this.llx * fontSize / 1000f;
			case 6:
				return this.lly * fontSize / 1000f;
			case 7:
				return this.urx * fontSize / 1000f;
			case 8:
				return this.ury * fontSize / 1000f;
			case 11:
				return 0f;
			case 12:
				return (this.urx - this.llx) * fontSize / 1000f;
			default:
				return 0f;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06002079 RID: 8313 RVA: 0x000C0EB0 File Offset: 0x000BFEB0
		public override string[][] FullFontName
		{
			get
			{
				return new string[][]
				{
					new string[]
					{
						"",
						"",
						"",
						this.fontName
					}
				};
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x0600207A RID: 8314 RVA: 0x000C0EF4 File Offset: 0x000BFEF4
		public override string[][] AllNameEntries
		{
			get
			{
				return new string[][]
				{
					new string[]
					{
						"4",
						"",
						"",
						"",
						this.fontName
					}
				};
			}
		}

		// Token: 0x0600207B RID: 8315 RVA: 0x000C0F3D File Offset: 0x000BFF3D
		public override int GetKerning(int char1, int char2)
		{
			return 0;
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x0600207C RID: 8316 RVA: 0x000C0F40 File Offset: 0x000BFF40
		// (set) Token: 0x0600207D RID: 8317 RVA: 0x000C0F48 File Offset: 0x000BFF48
		public override string PostscriptFontName
		{
			get
			{
				return this.fontName;
			}
			set
			{
			}
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x000C0F4A File Offset: 0x000BFF4A
		internal override int GetRawWidth(int c, string name)
		{
			return 0;
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x000C0F4D File Offset: 0x000BFF4D
		public override bool HasKernPairs()
		{
			return false;
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x000C0F50 File Offset: 0x000BFF50
		internal override void WriteFont(PdfWriter writer, PdfIndirectReference refi, object[] param)
		{
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x000C0F52 File Offset: 0x000BFF52
		public override PdfStream GetFullFontStream()
		{
			return null;
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x000C0F58 File Offset: 0x000BFF58
		public override int GetWidth(int char1)
		{
			if (this.cjkMirror != null)
			{
				return this.cjkMirror.GetWidth(char1);
			}
			if (!this.isType0)
			{
				return base.GetWidth(char1);
			}
			int[] array;
			this.metrics.TryGetValue(char1, out array);
			if (array != null)
			{
				return array[1];
			}
			return 0;
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x000C0FA4 File Offset: 0x000BFFA4
		public override int GetWidth(string text)
		{
			if (this.cjkMirror != null)
			{
				return this.cjkMirror.GetWidth(text);
			}
			if (this.isType0)
			{
				char[] array = text.ToCharArray();
				int num = array.Length;
				int num2 = 0;
				for (int i = 0; i < num; i++)
				{
					int[] array2;
					this.metrics.TryGetValue((int)array[i], out array2);
					if (array2 != null)
					{
						num2 += array2[1];
					}
				}
				return num2;
			}
			return base.GetWidth(text);
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x000C100C File Offset: 0x000C000C
		internal override byte[] ConvertToBytes(string text)
		{
			if (this.cjkMirror != null)
			{
				return PdfEncodings.ConvertToBytes(text, "UNICODEBIGUNMARKED");
			}
			if (this.isType0)
			{
				char[] array = text.ToCharArray();
				int num = array.Length;
				byte[] array2 = new byte[num * 2];
				int num2 = 0;
				for (int i = 0; i < num; i++)
				{
					int[] array3;
					this.metrics.TryGetValue((int)array[i], out array3);
					if (array3 != null)
					{
						int num3 = array3[0];
						array2[num2++] = (byte)(num3 / 256);
						array2[num2++] = (byte)num3;
					}
				}
				if (num2 == array2.Length)
				{
					return array2;
				}
				byte[] array4 = new byte[num2];
				Array.Copy(array2, 0, array4, 0, num2);
				return array4;
			}
			else
			{
				char[] array5 = text.ToCharArray();
				byte[] array6 = new byte[array5.Length];
				int num4 = 0;
				for (int j = 0; j < array5.Length; j++)
				{
					if (this.uni2byte.ContainsKey((int)array5[j]))
					{
						array6[num4++] = (byte)this.uni2byte[(int)array5[j]];
					}
				}
				if (num4 == array6.Length)
				{
					return array6;
				}
				byte[] array7 = new byte[num4];
				Array.Copy(array6, 0, array7, 0, num4);
				return array7;
			}
		}

		// Token: 0x06002085 RID: 8325 RVA: 0x000C1128 File Offset: 0x000C0128
		internal override byte[] ConvertToBytes(int char1)
		{
			if (this.cjkMirror != null)
			{
				return PdfEncodings.ConvertToBytes((char)char1, "UNICODEBIGUNMARKED");
			}
			if (this.isType0)
			{
				int[] array;
				this.metrics.TryGetValue(char1, out array);
				if (array != null)
				{
					int num = array[0];
					return new byte[]
					{
						(byte)(num / 256),
						(byte)num
					};
				}
				return new byte[0];
			}
			else
			{
				if (this.uni2byte.ContainsKey(char1))
				{
					return new byte[]
					{
						(byte)this.uni2byte[char1]
					};
				}
				return new byte[0];
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06002086 RID: 8326 RVA: 0x000C11B5 File Offset: 0x000C01B5
		internal PdfIndirectReference IndirectReference
		{
			get
			{
				return this.refFont;
			}
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x000C11BD File Offset: 0x000C01BD
		public override bool CharExists(int c)
		{
			if (this.cjkMirror != null)
			{
				return this.cjkMirror.CharExists(c);
			}
			if (this.isType0)
			{
				return this.metrics.ContainsKey(c);
			}
			return base.CharExists(c);
		}

		// Token: 0x06002088 RID: 8328 RVA: 0x000C11F0 File Offset: 0x000C01F0
		public override bool SetKerning(int char1, int char2, int kern)
		{
			return false;
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x000C11F3 File Offset: 0x000C01F3
		public override int[] GetCharBBox(int c)
		{
			return null;
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x000C11F6 File Offset: 0x000C01F6
		protected override int[] GetRawCharBBox(int c, string name)
		{
			return null;
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x0600208B RID: 8331 RVA: 0x000C11F9 File Offset: 0x000C01F9
		internal IntHashtable Uni2Byte
		{
			get
			{
				return this.uni2byte;
			}
		}

		// Token: 0x0400164B RID: 5707
		private Dictionary<int, int[]> metrics = new Dictionary<int, int[]>();

		// Token: 0x0400164C RID: 5708
		private string fontName;

		// Token: 0x0400164D RID: 5709
		private PRIndirectReference refFont;

		// Token: 0x0400164E RID: 5710
		private PdfDictionary font;

		// Token: 0x0400164F RID: 5711
		private IntHashtable uni2byte = new IntHashtable();

		// Token: 0x04001650 RID: 5712
		private IntHashtable diffmap;

		// Token: 0x04001651 RID: 5713
		private float Ascender = 800f;

		// Token: 0x04001652 RID: 5714
		private float CapHeight = 700f;

		// Token: 0x04001653 RID: 5715
		private float Descender = -200f;

		// Token: 0x04001654 RID: 5716
		private float ItalicAngle;

		// Token: 0x04001655 RID: 5717
		private float llx = -50f;

		// Token: 0x04001656 RID: 5718
		private float lly = -200f;

		// Token: 0x04001657 RID: 5719
		private float urx = 100f;

		// Token: 0x04001658 RID: 5720
		private float ury = 900f;

		// Token: 0x04001659 RID: 5721
		private bool isType0;

		// Token: 0x0400165A RID: 5722
		private BaseFont cjkMirror;

		// Token: 0x0400165B RID: 5723
		private static string[] cjkNames = new string[]
		{
			"HeiseiMin-W3",
			"HeiseiKakuGo-W5",
			"STSong-Light",
			"MHei-Medium",
			"MSung-Light",
			"HYGoThic-Medium",
			"HYSMyeongJo-Medium",
			"MSungStd-Light",
			"STSongStd-Light",
			"HYSMyeongJoStd-Medium",
			"KozMinPro-Regular"
		};

		// Token: 0x0400165C RID: 5724
		private static string[] cjkEncs = new string[]
		{
			"UniJIS-UCS2-H",
			"UniJIS-UCS2-H",
			"UniGB-UCS2-H",
			"UniCNS-UCS2-H",
			"UniCNS-UCS2-H",
			"UniKS-UCS2-H",
			"UniKS-UCS2-H",
			"UniCNS-UCS2-H",
			"UniGB-UCS2-H",
			"UniKS-UCS2-H",
			"UniJIS-UCS2-H"
		};

		// Token: 0x0400165D RID: 5725
		private static string[] cjkNames2 = new string[]
		{
			"MSungStd-Light",
			"STSongStd-Light",
			"HYSMyeongJoStd-Medium",
			"KozMinPro-Regular"
		};

		// Token: 0x0400165E RID: 5726
		private static string[] cjkEncs2 = new string[]
		{
			"UniCNS-UCS2-H",
			"UniGB-UCS2-H",
			"UniKS-UCS2-H",
			"UniJIS-UCS2-H",
			"UniCNS-UTF16-H",
			"UniGB-UTF16-H",
			"UniKS-UTF16-H",
			"UniJIS-UTF16-H"
		};

		// Token: 0x0400165F RID: 5727
		private static int[] stdEnc = new int[]
		{
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
			8217,
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
			8216,
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
			161,
			162,
			163,
			8260,
			165,
			402,
			167,
			164,
			39,
			8220,
			171,
			8249,
			8250,
			64257,
			64258,
			0,
			8211,
			8224,
			8225,
			183,
			0,
			182,
			8226,
			8218,
			8222,
			8221,
			187,
			8230,
			8240,
			0,
			191,
			0,
			96,
			180,
			710,
			732,
			175,
			728,
			729,
			168,
			0,
			730,
			184,
			0,
			733,
			731,
			711,
			8212,
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
			198,
			0,
			170,
			0,
			0,
			0,
			0,
			321,
			216,
			338,
			186,
			0,
			0,
			0,
			0,
			0,
			230,
			0,
			0,
			0,
			305,
			0,
			0,
			322,
			248,
			339,
			223,
			0,
			0,
			0,
			0
		};
	}
}
