using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.util;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000320 RID: 800
	internal class Type1Font : BaseFont
	{
		// Token: 0x06001D09 RID: 7433 RVA: 0x000AC64C File Offset: 0x000AB64C
		internal Type1Font(string afmFile, string enc, bool emb, byte[] ttfAfm, byte[] pfb, bool forceRead)
		{
			if (emb && ttfAfm != null && pfb == null)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("two.byte.arrays.are.needed.if.the.type1.font.is.embedded"));
			}
			if (emb && ttfAfm != null)
			{
				this.pfb = pfb;
			}
			this.encoding = enc;
			this.embedded = emb;
			this.fileName = afmFile;
			base.FontType = 0;
			RandomAccessFileOrArray randomAccessFileOrArray = null;
			Stream stream = null;
			if (BaseFont.BuiltinFonts14.ContainsKey(afmFile))
			{
				this.embedded = false;
				this.builtinFont = true;
				byte[] array = new byte[1024];
				try
				{
					stream = BaseFont.GetResourceStream("iTextSharp.text.pdf.fonts." + afmFile + ".afm");
					if (stream == null)
					{
						string composedMessage = MessageLocalization.GetComposedMessage("1.not.found.as.resource", afmFile);
						Console.Error.WriteLine(composedMessage);
						throw new DocumentException(composedMessage);
					}
					MemoryStream memoryStream = new MemoryStream();
					for (;;)
					{
						int num = stream.Read(array, 0, array.Length);
						if (num == 0)
						{
							break;
						}
						memoryStream.Write(array, 0, num);
					}
					array = memoryStream.ToArray();
				}
				finally
				{
					if (stream != null)
					{
						try
						{
							stream.Close();
						}
						catch
						{
						}
					}
				}
				try
				{
					randomAccessFileOrArray = new RandomAccessFileOrArray(array);
					this.Process(randomAccessFileOrArray);
					goto IL_26B;
				}
				finally
				{
					if (randomAccessFileOrArray != null)
					{
						try
						{
							randomAccessFileOrArray.Close();
						}
						catch
						{
						}
					}
				}
			}
			if (afmFile.ToLower(CultureInfo.InvariantCulture).EndsWith(".afm"))
			{
				try
				{
					if (ttfAfm == null)
					{
						randomAccessFileOrArray = new RandomAccessFileOrArray(afmFile, forceRead);
					}
					else
					{
						randomAccessFileOrArray = new RandomAccessFileOrArray(ttfAfm);
					}
					this.Process(randomAccessFileOrArray);
					goto IL_26B;
				}
				finally
				{
					if (randomAccessFileOrArray != null)
					{
						try
						{
							randomAccessFileOrArray.Close();
						}
						catch
						{
						}
					}
				}
			}
			if (afmFile.ToLower(CultureInfo.InvariantCulture).EndsWith(".pfm"))
			{
				try
				{
					MemoryStream memoryStream2 = new MemoryStream();
					if (ttfAfm == null)
					{
						randomAccessFileOrArray = new RandomAccessFileOrArray(afmFile, forceRead);
					}
					else
					{
						randomAccessFileOrArray = new RandomAccessFileOrArray(ttfAfm);
					}
					Pfm2afm.Convert(randomAccessFileOrArray, memoryStream2);
					randomAccessFileOrArray.Close();
					randomAccessFileOrArray = new RandomAccessFileOrArray(memoryStream2.ToArray());
					this.Process(randomAccessFileOrArray);
					goto IL_26B;
				}
				finally
				{
					if (randomAccessFileOrArray != null)
					{
						try
						{
							randomAccessFileOrArray.Close();
						}
						catch
						{
						}
					}
				}
			}
			throw new DocumentException(MessageLocalization.GetComposedMessage("1.is.not.an.afm.or.pfm.font.file", afmFile));
			IL_26B:
			this.EncodingScheme = this.EncodingScheme.Trim();
			if (this.EncodingScheme.Equals("AdobeStandardEncoding") || this.EncodingScheme.Equals("StandardEncoding"))
			{
				this.fontSpecific = false;
			}
			if (!this.encoding.StartsWith("#"))
			{
				PdfEncodings.ConvertToBytes(" ", enc);
			}
			base.CreateEncoding();
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x000AC988 File Offset: 0x000AB988
		internal override int GetRawWidth(int c, string name)
		{
			object[] array;
			if (name == null)
			{
				this.CharMetrics.TryGetValue(c, out array);
			}
			else
			{
				if (name.Equals(".notdef"))
				{
					return 0;
				}
				this.CharMetrics.TryGetValue(name, out array);
			}
			if (array != null)
			{
				return (int)array[1];
			}
			return 0;
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x000AC9DC File Offset: 0x000AB9DC
		public override int GetKerning(int char1, int char2)
		{
			string text = GlyphList.UnicodeToName(char1);
			if (text == null)
			{
				return 0;
			}
			string text2 = GlyphList.UnicodeToName(char2);
			if (text2 == null)
			{
				return 0;
			}
			object[] array;
			this.KernPairs.TryGetValue(text, out array);
			if (array == null)
			{
				return 0;
			}
			for (int i = 0; i < array.Length; i += 2)
			{
				if (text2.Equals(array[i]))
				{
					return (int)array[i + 1];
				}
			}
			return 0;
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x000ACA3C File Offset: 0x000ABA3C
		public void Process(RandomAccessFileOrArray rf)
		{
			bool flag = false;
			string str;
			while ((str = rf.ReadLine()) != null)
			{
				StringTokenizer stringTokenizer = new StringTokenizer(str, " ,\n\r\t\f");
				if (stringTokenizer.HasMoreTokens())
				{
					string text = stringTokenizer.NextToken();
					if (text.Equals("FontName"))
					{
						this.FontName = stringTokenizer.NextToken("ÿ").Substring(1);
					}
					else if (text.Equals("FullName"))
					{
						this.FullName = stringTokenizer.NextToken("ÿ").Substring(1);
					}
					else if (text.Equals("FamilyName"))
					{
						this.FamilyName = stringTokenizer.NextToken("ÿ").Substring(1);
					}
					else if (text.Equals("Weight"))
					{
						this.Weight = stringTokenizer.NextToken("ÿ").Substring(1);
					}
					else if (text.Equals("ItalicAngle"))
					{
						this.ItalicAngle = float.Parse(stringTokenizer.NextToken(), NumberFormatInfo.InvariantInfo);
					}
					else if (text.Equals("IsFixedPitch"))
					{
						this.IsFixedPitch = stringTokenizer.NextToken().Equals("true");
					}
					else if (text.Equals("CharacterSet"))
					{
						this.CharacterSet = stringTokenizer.NextToken("ÿ").Substring(1);
					}
					else if (text.Equals("FontBBox"))
					{
						this.llx = (int)float.Parse(stringTokenizer.NextToken(), NumberFormatInfo.InvariantInfo);
						this.lly = (int)float.Parse(stringTokenizer.NextToken(), NumberFormatInfo.InvariantInfo);
						this.urx = (int)float.Parse(stringTokenizer.NextToken(), NumberFormatInfo.InvariantInfo);
						this.ury = (int)float.Parse(stringTokenizer.NextToken(), NumberFormatInfo.InvariantInfo);
					}
					else if (text.Equals("UnderlinePosition"))
					{
						this.UnderlinePosition = (int)float.Parse(stringTokenizer.NextToken(), NumberFormatInfo.InvariantInfo);
					}
					else if (text.Equals("UnderlineThickness"))
					{
						this.UnderlineThickness = (int)float.Parse(stringTokenizer.NextToken(), NumberFormatInfo.InvariantInfo);
					}
					else if (text.Equals("EncodingScheme"))
					{
						this.EncodingScheme = stringTokenizer.NextToken("ÿ").Substring(1);
					}
					else if (text.Equals("CapHeight"))
					{
						this.CapHeight = (int)float.Parse(stringTokenizer.NextToken(), NumberFormatInfo.InvariantInfo);
					}
					else if (text.Equals("XHeight"))
					{
						this.XHeight = (int)float.Parse(stringTokenizer.NextToken(), NumberFormatInfo.InvariantInfo);
					}
					else if (text.Equals("Ascender"))
					{
						this.Ascender = (int)float.Parse(stringTokenizer.NextToken(), NumberFormatInfo.InvariantInfo);
					}
					else if (text.Equals("Descender"))
					{
						this.Descender = (int)float.Parse(stringTokenizer.NextToken(), NumberFormatInfo.InvariantInfo);
					}
					else if (text.Equals("StdHW"))
					{
						this.StdHW = (int)float.Parse(stringTokenizer.NextToken(), NumberFormatInfo.InvariantInfo);
					}
					else if (text.Equals("StdVW"))
					{
						this.StdVW = (int)float.Parse(stringTokenizer.NextToken(), NumberFormatInfo.InvariantInfo);
					}
					else if (text.Equals("StartCharMetrics"))
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("missing.startcharmetrics.in.1", this.fileName));
			}
			while ((str = rf.ReadLine()) != null)
			{
				StringTokenizer stringTokenizer2 = new StringTokenizer(str);
				if (stringTokenizer2.HasMoreTokens())
				{
					string text2 = stringTokenizer2.NextToken();
					if (text2.Equals("EndCharMetrics"))
					{
						flag = false;
						break;
					}
					int num = -1;
					int num2 = 250;
					string text3 = "";
					int[] array = null;
					stringTokenizer2 = new StringTokenizer(str, ";");
					while (stringTokenizer2.HasMoreTokens())
					{
						StringTokenizer stringTokenizer3 = new StringTokenizer(stringTokenizer2.NextToken());
						if (stringTokenizer3.HasMoreTokens())
						{
							text2 = stringTokenizer3.NextToken();
							if (text2.Equals("C"))
							{
								num = int.Parse(stringTokenizer3.NextToken());
							}
							else if (text2.Equals("WX"))
							{
								num2 = (int)float.Parse(stringTokenizer3.NextToken(), NumberFormatInfo.InvariantInfo);
							}
							else if (text2.Equals("N"))
							{
								text3 = stringTokenizer3.NextToken();
							}
							else if (text2.Equals("B"))
							{
								array = new int[]
								{
									int.Parse(stringTokenizer3.NextToken()),
									int.Parse(stringTokenizer3.NextToken()),
									int.Parse(stringTokenizer3.NextToken()),
									int.Parse(stringTokenizer3.NextToken())
								};
							}
						}
					}
					object[] value = new object[]
					{
						num,
						num2,
						text3,
						array
					};
					if (num >= 0)
					{
						this.CharMetrics[num] = value;
					}
					this.CharMetrics[text3] = value;
				}
			}
			if (flag)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("missing.endcharmetrics.in.1", this.fileName));
			}
			if (!this.CharMetrics.ContainsKey("nonbreakingspace"))
			{
				object[] array2;
				this.CharMetrics.TryGetValue("space", out array2);
				if (array2 != null)
				{
					this.CharMetrics["nonbreakingspace"] = array2;
				}
			}
			while ((str = rf.ReadLine()) != null)
			{
				StringTokenizer stringTokenizer4 = new StringTokenizer(str);
				if (stringTokenizer4.HasMoreTokens())
				{
					string text4 = stringTokenizer4.NextToken();
					if (text4.Equals("EndFontMetrics"))
					{
						return;
					}
					if (text4.Equals("StartKernPairs"))
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("missing.endfontmetrics.in.1", this.fileName));
			}
			while ((str = rf.ReadLine()) != null)
			{
				StringTokenizer stringTokenizer5 = new StringTokenizer(str);
				if (stringTokenizer5.HasMoreTokens())
				{
					string text5 = stringTokenizer5.NextToken();
					if (text5.Equals("KPX"))
					{
						string key = stringTokenizer5.NextToken();
						string text6 = stringTokenizer5.NextToken();
						int num3 = (int)float.Parse(stringTokenizer5.NextToken(), NumberFormatInfo.InvariantInfo);
						object[] array3;
						this.KernPairs.TryGetValue(key, out array3);
						if (array3 == null)
						{
							this.KernPairs[key] = new object[]
							{
								text6,
								num3
							};
						}
						else
						{
							int num4 = array3.Length;
							object[] array4 = new object[num4 + 2];
							Array.Copy(array3, 0, array4, 0, num4);
							array4[num4] = text6;
							array4[num4 + 1] = num3;
							this.KernPairs[key] = array4;
						}
					}
					else if (text5.Equals("EndKernPairs"))
					{
						flag = false;
						break;
					}
				}
			}
			if (flag)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("missing.endkernpairs.in.1", this.fileName));
			}
			rf.Close();
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x000AD108 File Offset: 0x000AC108
		public override PdfStream GetFullFontStream()
		{
			if (this.builtinFont || !this.embedded)
			{
				return null;
			}
			RandomAccessFileOrArray randomAccessFileOrArray = null;
			PdfStream result;
			try
			{
				string text = this.fileName.Substring(0, this.fileName.Length - 3) + "pfb";
				if (this.pfb == null)
				{
					randomAccessFileOrArray = new RandomAccessFileOrArray(text, true);
				}
				else
				{
					randomAccessFileOrArray = new RandomAccessFileOrArray(this.pfb);
				}
				int length = randomAccessFileOrArray.Length;
				byte[] array = new byte[length - 18];
				int[] array2 = new int[3];
				int num = 0;
				for (int i = 0; i < 3; i++)
				{
					if (randomAccessFileOrArray.Read() != 128)
					{
						throw new DocumentException(MessageLocalization.GetComposedMessage("start.marker.missing.in.1", text));
					}
					if (randomAccessFileOrArray.Read() != Type1Font.PFB_TYPES[i])
					{
						throw new DocumentException(MessageLocalization.GetComposedMessage("incorrect.segment.type.in.1", text));
					}
					int num2 = randomAccessFileOrArray.Read();
					num2 += randomAccessFileOrArray.Read() << 8;
					num2 += randomAccessFileOrArray.Read() << 16;
					num2 += randomAccessFileOrArray.Read() << 24;
					array2[i] = num2;
					while (num2 != 0)
					{
						int num3 = randomAccessFileOrArray.Read(array, num, num2);
						if (num3 < 0)
						{
							throw new DocumentException(MessageLocalization.GetComposedMessage("premature.end.in.1", text));
						}
						num += num3;
						num2 -= num3;
					}
				}
				result = new BaseFont.StreamFont(array, array2, this.compressionLevel);
			}
			finally
			{
				if (randomAccessFileOrArray != null)
				{
					try
					{
						randomAccessFileOrArray.Close();
					}
					catch
					{
					}
				}
			}
			return result;
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x000AD2A4 File Offset: 0x000AC2A4
		public PdfDictionary GetFontDescriptor(PdfIndirectReference fontStream)
		{
			if (this.builtinFont)
			{
				return null;
			}
			PdfDictionary pdfDictionary = new PdfDictionary(PdfName.FONTDESCRIPTOR);
			pdfDictionary.Put(PdfName.ASCENT, new PdfNumber(this.Ascender));
			pdfDictionary.Put(PdfName.CAPHEIGHT, new PdfNumber(this.CapHeight));
			pdfDictionary.Put(PdfName.DESCENT, new PdfNumber(this.Descender));
			pdfDictionary.Put(PdfName.FONTBBOX, new PdfRectangle((float)this.llx, (float)this.lly, (float)this.urx, (float)this.ury));
			pdfDictionary.Put(PdfName.FONTNAME, new PdfName(this.FontName));
			pdfDictionary.Put(PdfName.ITALICANGLE, new PdfNumber(this.ItalicAngle));
			pdfDictionary.Put(PdfName.STEMV, new PdfNumber(this.StdVW));
			if (fontStream != null)
			{
				pdfDictionary.Put(PdfName.FONTFILE, fontStream);
			}
			int num = 0;
			if (this.IsFixedPitch)
			{
				num |= 1;
			}
			num |= (this.fontSpecific ? 4 : 32);
			if (this.ItalicAngle < 0f)
			{
				num |= 64;
			}
			if (this.FontName.IndexOf("Caps") >= 0 || this.FontName.EndsWith("SC"))
			{
				num |= 131072;
			}
			if (this.Weight.Equals("Bold"))
			{
				num |= 262144;
			}
			pdfDictionary.Put(PdfName.FLAGS, new PdfNumber(num));
			return pdfDictionary;
		}

		// Token: 0x06001D0F RID: 7439 RVA: 0x000AD410 File Offset: 0x000AC410
		private PdfDictionary GetFontBaseType(PdfIndirectReference fontDescriptor, int firstChar, int lastChar, byte[] shortTag)
		{
			PdfDictionary pdfDictionary = new PdfDictionary(PdfName.FONT);
			pdfDictionary.Put(PdfName.SUBTYPE, PdfName.TYPE1);
			pdfDictionary.Put(PdfName.BASEFONT, new PdfName(this.FontName));
			bool flag = this.encoding.Equals("Cp1252") || this.encoding.Equals("MacRoman");
			if (!this.fontSpecific || this.specialMap != null)
			{
				for (int i = firstChar; i <= lastChar; i++)
				{
					if (!this.differences[i].Equals(".notdef"))
					{
						firstChar = i;
						break;
					}
				}
				if (flag)
				{
					pdfDictionary.Put(PdfName.ENCODING, this.encoding.Equals("Cp1252") ? PdfName.WIN_ANSI_ENCODING : PdfName.MAC_ROMAN_ENCODING);
				}
				else
				{
					PdfDictionary pdfDictionary2 = new PdfDictionary(PdfName.ENCODING);
					PdfArray pdfArray = new PdfArray();
					bool flag2 = true;
					for (int j = firstChar; j <= lastChar; j++)
					{
						if (shortTag[j] != 0)
						{
							if (flag2)
							{
								pdfArray.Add(new PdfNumber(j));
								flag2 = false;
							}
							pdfArray.Add(new PdfName(this.differences[j]));
						}
						else
						{
							flag2 = true;
						}
					}
					pdfDictionary2.Put(PdfName.DIFFERENCES, pdfArray);
					pdfDictionary.Put(PdfName.ENCODING, pdfDictionary2);
				}
			}
			if (this.specialMap != null || this.forceWidthsOutput || !this.builtinFont || (!this.fontSpecific && !flag))
			{
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
			}
			if (!this.builtinFont && fontDescriptor != null)
			{
				pdfDictionary.Put(PdfName.FONTDESCRIPTOR, fontDescriptor);
			}
			return pdfDictionary;
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x000AD600 File Offset: 0x000AC600
		internal override void WriteFont(PdfWriter writer, PdfIndirectReference piref, object[] parms)
		{
			int firstChar = (int)parms[0];
			int lastChar = (int)parms[1];
			byte[] array = (byte[])parms[2];
			if (!(bool)parms[3] || !this.subset)
			{
				firstChar = 0;
				lastChar = array.Length - 1;
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = 1;
				}
			}
			PdfIndirectReference pdfIndirectReference = null;
			PdfObject pdfObject = this.GetFullFontStream();
			if (pdfObject != null)
			{
				PdfIndirectObject pdfIndirectObject = writer.AddToBody(pdfObject);
				pdfIndirectReference = pdfIndirectObject.IndirectReference;
			}
			pdfObject = this.GetFontDescriptor(pdfIndirectReference);
			if (pdfObject != null)
			{
				PdfIndirectObject pdfIndirectObject = writer.AddToBody(pdfObject);
				pdfIndirectReference = pdfIndirectObject.IndirectReference;
			}
			pdfObject = this.GetFontBaseType(pdfIndirectReference, firstChar, lastChar, array);
			writer.AddToBody(pdfObject, piref);
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x000AD6C0 File Offset: 0x000AC6C0
		public override float GetFontDescriptor(int key, float fontSize)
		{
			switch (key)
			{
			case 1:
			case 9:
				return (float)this.Ascender * fontSize / 1000f;
			case 2:
				return (float)this.CapHeight * fontSize / 1000f;
			case 3:
			case 10:
				return (float)this.Descender * fontSize / 1000f;
			case 4:
				return this.ItalicAngle;
			case 5:
				return (float)this.llx * fontSize / 1000f;
			case 6:
				return (float)this.lly * fontSize / 1000f;
			case 7:
				return (float)this.urx * fontSize / 1000f;
			case 8:
				return (float)this.ury * fontSize / 1000f;
			case 11:
				return 0f;
			case 12:
				return (float)(this.urx - this.llx) * fontSize / 1000f;
			case 13:
				return (float)this.UnderlinePosition * fontSize / 1000f;
			case 14:
				return (float)this.UnderlineThickness * fontSize / 1000f;
			default:
				return 0f;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001D12 RID: 7442 RVA: 0x000AD7CD File Offset: 0x000AC7CD
		// (set) Token: 0x06001D13 RID: 7443 RVA: 0x000AD7D5 File Offset: 0x000AC7D5
		public override string PostscriptFontName
		{
			get
			{
				return this.FontName;
			}
			set
			{
				this.FontName = value;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06001D14 RID: 7444 RVA: 0x000AD7E0 File Offset: 0x000AC7E0
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
						this.FullName
					}
				};
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001D15 RID: 7445 RVA: 0x000AD824 File Offset: 0x000AC824
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
						this.FullName
					}
				};
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001D16 RID: 7446 RVA: 0x000AD870 File Offset: 0x000AC870
		public override string[][] FamilyFontName
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
						this.FamilyName
					}
				};
			}
		}

		// Token: 0x06001D17 RID: 7447 RVA: 0x000AD8B1 File Offset: 0x000AC8B1
		public override bool HasKernPairs()
		{
			return this.KernPairs.Count > 0;
		}

		// Token: 0x06001D18 RID: 7448 RVA: 0x000AD8C4 File Offset: 0x000AC8C4
		public override bool SetKerning(int char1, int char2, int kern)
		{
			string text = GlyphList.UnicodeToName(char1);
			if (text == null)
			{
				return false;
			}
			string text2 = GlyphList.UnicodeToName(char2);
			if (text2 == null)
			{
				return false;
			}
			object[] array;
			this.KernPairs.TryGetValue(text, out array);
			if (array == null)
			{
				array = new object[]
				{
					text2,
					kern
				};
				this.KernPairs[text] = array;
				return true;
			}
			for (int i = 0; i < array.Length; i += 2)
			{
				if (text2.Equals(array[i]))
				{
					array[i + 1] = kern;
					return true;
				}
			}
			int num = array.Length;
			object[] array2 = new object[num + 2];
			Array.Copy(array, 0, array2, 0, num);
			array2[num] = text2;
			array2[num + 1] = kern;
			this.KernPairs[text] = array2;
			return true;
		}

		// Token: 0x06001D19 RID: 7449 RVA: 0x000AD988 File Offset: 0x000AC988
		protected override int[] GetRawCharBBox(int c, string name)
		{
			object[] array;
			if (name == null)
			{
				this.CharMetrics.TryGetValue(c, out array);
			}
			else
			{
				if (name.Equals(".notdef"))
				{
					return null;
				}
				this.CharMetrics.TryGetValue(name, out array);
			}
			if (array != null)
			{
				return (int[])array[3];
			}
			return null;
		}

		// Token: 0x040013F7 RID: 5111
		protected byte[] pfb;

		// Token: 0x040013F8 RID: 5112
		private string FontName;

		// Token: 0x040013F9 RID: 5113
		private string FullName;

		// Token: 0x040013FA RID: 5114
		private string FamilyName;

		// Token: 0x040013FB RID: 5115
		private string Weight = "";

		// Token: 0x040013FC RID: 5116
		private float ItalicAngle;

		// Token: 0x040013FD RID: 5117
		private bool IsFixedPitch;

		// Token: 0x040013FE RID: 5118
		private string CharacterSet;

		// Token: 0x040013FF RID: 5119
		private int llx = -50;

		// Token: 0x04001400 RID: 5120
		private int lly = -200;

		// Token: 0x04001401 RID: 5121
		private int urx = 1000;

		// Token: 0x04001402 RID: 5122
		private int ury = 900;

		// Token: 0x04001403 RID: 5123
		private int UnderlinePosition = -100;

		// Token: 0x04001404 RID: 5124
		private int UnderlineThickness = 50;

		// Token: 0x04001405 RID: 5125
		private string EncodingScheme = "FontSpecific";

		// Token: 0x04001406 RID: 5126
		private int CapHeight = 700;

		// Token: 0x04001407 RID: 5127
		private int XHeight = 480;

		// Token: 0x04001408 RID: 5128
		private int Ascender = 800;

		// Token: 0x04001409 RID: 5129
		private int Descender = -200;

		// Token: 0x0400140A RID: 5130
		private int StdHW;

		// Token: 0x0400140B RID: 5131
		private int StdVW = 80;

		// Token: 0x0400140C RID: 5132
		private Dictionary<object, object[]> CharMetrics = new Dictionary<object, object[]>();

		// Token: 0x0400140D RID: 5133
		private Dictionary<string, object[]> KernPairs = new Dictionary<string, object[]>();

		// Token: 0x0400140E RID: 5134
		private string fileName;

		// Token: 0x0400140F RID: 5135
		private bool builtinFont;

		// Token: 0x04001410 RID: 5136
		private static readonly int[] PFB_TYPES = new int[]
		{
			1,
			2,
			1
		};
	}
}
