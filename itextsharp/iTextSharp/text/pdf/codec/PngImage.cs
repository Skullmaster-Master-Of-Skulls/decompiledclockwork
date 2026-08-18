using System;
using System.IO;
using System.Net;
using System.Text;
using System.util;
using System.util.zlib;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf.codec
{
	// Token: 0x020002D7 RID: 727
	public class PngImage
	{
		// Token: 0x06001B17 RID: 6935 RVA: 0x000A1570 File Offset: 0x000A0570
		private PngImage(Stream isp)
		{
			this.isp = isp;
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x000A15C0 File Offset: 0x000A05C0
		public static Image GetImage(Uri url)
		{
			Stream stream = null;
			Image result;
			try
			{
				stream = WebRequest.Create(url).GetResponse().GetResponseStream();
				Image image = PngImage.GetImage(stream);
				image.Url = url;
				result = image;
			}
			finally
			{
				if (stream != null)
				{
					stream.Close();
				}
			}
			return result;
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x000A1610 File Offset: 0x000A0610
		public static Image GetImage(Stream isp)
		{
			PngImage pngImage = new PngImage(isp);
			return pngImage.GetImage();
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x000A162A File Offset: 0x000A062A
		public static Image GetImage(string file)
		{
			return PngImage.GetImage(Utilities.ToURL(file));
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x000A1638 File Offset: 0x000A0638
		public static Image GetImage(byte[] data)
		{
			Stream stream = new MemoryStream(data);
			Image image = PngImage.GetImage(stream);
			image.OriginalData = data;
			return image;
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x000A165C File Offset: 0x000A065C
		private static bool CheckMarker(string s)
		{
			if (s.Length != 4)
			{
				return false;
			}
			for (int i = 0; i < 4; i++)
			{
				char c = s[i];
				if ((c < 'a' || c > 'z') && (c < 'A' || c > 'Z'))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x000A16A0 File Offset: 0x000A06A0
		private void ReadPng()
		{
			for (int i = 0; i < PngImage.PNGID.Length; i++)
			{
				if (PngImage.PNGID[i] != this.isp.ReadByte())
				{
					throw new IOException(MessageLocalization.GetComposedMessage("file.is.not.a.valid.png"));
				}
			}
			byte[] buffer = new byte[4096];
			for (;;)
			{
				int j = PngImage.GetInt(this.isp);
				string @string = PngImage.GetString(this.isp);
				if (j < 0 || !PngImage.CheckMarker(@string))
				{
					break;
				}
				if ("IDAT".Equals(@string))
				{
					while (j != 0)
					{
						int num = this.isp.Read(buffer, 0, Math.Min(j, 4096));
						if (num <= 0)
						{
							return;
						}
						this.idat.Write(buffer, 0, num);
						j -= num;
					}
				}
				else if ("tRNS".Equals(@string))
				{
					switch (this.colorType)
					{
					case 0:
						if (j >= 2)
						{
							j -= 2;
							int word = PngImage.GetWord(this.isp);
							if (this.bitDepth == 16)
							{
								this.transRedGray = word;
							}
							else
							{
								this.additional.Put(PdfName.MASK, new PdfLiteral(string.Concat(new object[]
								{
									"[",
									word,
									" ",
									word,
									"]"
								})));
							}
						}
						break;
					case 2:
						if (j >= 6)
						{
							j -= 6;
							int word2 = PngImage.GetWord(this.isp);
							int word3 = PngImage.GetWord(this.isp);
							int word4 = PngImage.GetWord(this.isp);
							if (this.bitDepth == 16)
							{
								this.transRedGray = word2;
								this.transGreen = word3;
								this.transBlue = word4;
							}
							else
							{
								this.additional.Put(PdfName.MASK, new PdfLiteral(string.Concat(new object[]
								{
									"[",
									word2,
									" ",
									word2,
									" ",
									word3,
									" ",
									word3,
									" ",
									word4,
									" ",
									word4,
									"]"
								})));
							}
						}
						break;
					case 3:
						if (j > 0)
						{
							this.trans = new byte[j];
							for (int k = 0; k < j; k++)
							{
								this.trans[k] = (byte)this.isp.ReadByte();
							}
							j = 0;
						}
						break;
					}
					Utilities.Skip(this.isp, j);
				}
				else if ("IHDR".Equals(@string))
				{
					this.width = PngImage.GetInt(this.isp);
					this.height = PngImage.GetInt(this.isp);
					this.bitDepth = this.isp.ReadByte();
					this.colorType = this.isp.ReadByte();
					this.compressionMethod = this.isp.ReadByte();
					this.filterMethod = this.isp.ReadByte();
					this.interlaceMethod = this.isp.ReadByte();
				}
				else if ("PLTE".Equals(@string))
				{
					if (this.colorType == 3)
					{
						PdfArray pdfArray = new PdfArray();
						pdfArray.Add(PdfName.INDEXED);
						pdfArray.Add(this.GetColorspace());
						pdfArray.Add(new PdfNumber(j / 3 - 1));
						ByteBuffer byteBuffer = new ByteBuffer();
						while (j-- > 0)
						{
							byteBuffer.Append_i(this.isp.ReadByte());
						}
						pdfArray.Add(new PdfString(this.colorTable = byteBuffer.ToByteArray()));
						this.additional.Put(PdfName.COLORSPACE, pdfArray);
					}
					else
					{
						Utilities.Skip(this.isp, j);
					}
				}
				else if ("pHYs".Equals(@string))
				{
					int @int = PngImage.GetInt(this.isp);
					int int2 = PngImage.GetInt(this.isp);
					int num2 = this.isp.ReadByte();
					if (num2 == 1)
					{
						this.dpiX = (int)((float)@int * 0.0254f + 0.5f);
						this.dpiY = (int)((float)int2 * 0.0254f + 0.5f);
					}
					else if (int2 != 0)
					{
						this.XYRatio = (float)@int / (float)int2;
					}
				}
				else if ("cHRM".Equals(@string))
				{
					this.xW = (float)PngImage.GetInt(this.isp) / 100000f;
					this.yW = (float)PngImage.GetInt(this.isp) / 100000f;
					this.xR = (float)PngImage.GetInt(this.isp) / 100000f;
					this.yR = (float)PngImage.GetInt(this.isp) / 100000f;
					this.xG = (float)PngImage.GetInt(this.isp) / 100000f;
					this.yG = (float)PngImage.GetInt(this.isp) / 100000f;
					this.xB = (float)PngImage.GetInt(this.isp) / 100000f;
					this.yB = (float)PngImage.GetInt(this.isp) / 100000f;
					this.hasCHRM = (Math.Abs(this.xW) >= 0.0001f && Math.Abs(this.yW) >= 0.0001f && Math.Abs(this.xR) >= 0.0001f && Math.Abs(this.yR) >= 0.0001f && Math.Abs(this.xG) >= 0.0001f && Math.Abs(this.yG) >= 0.0001f && Math.Abs(this.xB) >= 0.0001f && Math.Abs(this.yB) >= 0.0001f);
				}
				else if ("sRGB".Equals(@string))
				{
					int num3 = this.isp.ReadByte();
					this.intent = PngImage.intents[num3];
					this.gamma = 2.2f;
					this.xW = 0.3127f;
					this.yW = 0.329f;
					this.xR = 0.64f;
					this.yR = 0.33f;
					this.xG = 0.3f;
					this.yG = 0.6f;
					this.xB = 0.15f;
					this.yB = 0.06f;
					this.hasCHRM = true;
				}
				else if ("gAMA".Equals(@string))
				{
					int int3 = PngImage.GetInt(this.isp);
					if (int3 != 0)
					{
						this.gamma = 100000f / (float)int3;
						if (!this.hasCHRM)
						{
							this.xW = 0.3127f;
							this.yW = 0.329f;
							this.xR = 0.64f;
							this.yR = 0.33f;
							this.xG = 0.3f;
							this.yG = 0.6f;
							this.xB = 0.15f;
							this.yB = 0.06f;
							this.hasCHRM = true;
						}
					}
				}
				else
				{
					if ("iCCP".Equals(@string))
					{
						do
						{
							j--;
						}
						while (this.isp.ReadByte() != 0);
						this.isp.ReadByte();
						j--;
						byte[] array = new byte[j];
						int num4 = 0;
						while (j > 0)
						{
							int num5 = this.isp.Read(array, num4, j);
							if (num5 < 0)
							{
								goto Block_35;
							}
							num4 += num5;
							j -= num5;
						}
						byte[] data = PdfReader.FlateDecode(array, true);
						try
						{
							this.icc_profile = ICC_Profile.GetInstance(data);
							goto IL_7F6;
						}
						catch
						{
							this.icc_profile = null;
							goto IL_7F6;
						}
					}
					if ("IEND".Equals(@string))
					{
						return;
					}
					Utilities.Skip(this.isp, j);
				}
				IL_7F6:
				Utilities.Skip(this.isp, 4);
			}
			throw new IOException(MessageLocalization.GetComposedMessage("corrupted.png.file"));
			Block_35:
			throw new IOException(MessageLocalization.GetComposedMessage("premature.end.of.file"));
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x000A1EC4 File Offset: 0x000A0EC4
		private PdfObject GetColorspace()
		{
			if (this.icc_profile != null)
			{
				if ((this.colorType & 2) == 0)
				{
					return PdfName.DEVICEGRAY;
				}
				return PdfName.DEVICERGB;
			}
			else
			{
				if (this.gamma != 1f || this.hasCHRM)
				{
					PdfArray pdfArray = new PdfArray();
					PdfDictionary pdfDictionary = new PdfDictionary();
					if ((this.colorType & 2) == 0)
					{
						if (this.gamma == 1f)
						{
							return PdfName.DEVICEGRAY;
						}
						pdfArray.Add(PdfName.CALGRAY);
						pdfDictionary.Put(PdfName.GAMMA, new PdfNumber(this.gamma));
						pdfDictionary.Put(PdfName.WHITEPOINT, new PdfLiteral("[1 1 1]"));
						pdfArray.Add(pdfDictionary);
					}
					else
					{
						PdfObject value = new PdfLiteral("[1 1 1]");
						pdfArray.Add(PdfName.CALRGB);
						if (this.gamma != 1f)
						{
							PdfArray pdfArray2 = new PdfArray();
							PdfNumber obj = new PdfNumber(this.gamma);
							pdfArray2.Add(obj);
							pdfArray2.Add(obj);
							pdfArray2.Add(obj);
							pdfDictionary.Put(PdfName.GAMMA, pdfArray2);
						}
						if (this.hasCHRM)
						{
							float num = this.yW * ((this.xG - this.xB) * this.yR - (this.xR - this.xB) * this.yG + (this.xR - this.xG) * this.yB);
							float num2 = this.yR * ((this.xG - this.xB) * this.yW - (this.xW - this.xB) * this.yG + (this.xW - this.xG) * this.yB) / num;
							float num3 = num2 * this.xR / this.yR;
							float num4 = num2 * ((1f - this.xR) / this.yR - 1f);
							float num5 = -this.yG * ((this.xR - this.xB) * this.yW - (this.xW - this.xB) * this.yR + (this.xW - this.xR) * this.yB) / num;
							float num6 = num5 * this.xG / this.yG;
							float num7 = num5 * ((1f - this.xG) / this.yG - 1f);
							float num8 = this.yB * ((this.xR - this.xG) * this.yW - (this.xW - this.xG) * this.yW + (this.xW - this.xR) * this.yG) / num;
							float num9 = num8 * this.xB / this.yB;
							float num10 = num8 * ((1f - this.xB) / this.yB - 1f);
							float value2 = num3 + num6 + num9;
							float value3 = 1f;
							float value4 = num4 + num7 + num10;
							PdfArray pdfArray3 = new PdfArray();
							pdfArray3.Add(new PdfNumber(value2));
							pdfArray3.Add(new PdfNumber(value3));
							pdfArray3.Add(new PdfNumber(value4));
							value = pdfArray3;
							PdfArray pdfArray4 = new PdfArray();
							pdfArray4.Add(new PdfNumber(num3));
							pdfArray4.Add(new PdfNumber(num2));
							pdfArray4.Add(new PdfNumber(num4));
							pdfArray4.Add(new PdfNumber(num6));
							pdfArray4.Add(new PdfNumber(num5));
							pdfArray4.Add(new PdfNumber(num7));
							pdfArray4.Add(new PdfNumber(num9));
							pdfArray4.Add(new PdfNumber(num8));
							pdfArray4.Add(new PdfNumber(num10));
							pdfDictionary.Put(PdfName.MATRIX, pdfArray4);
						}
						pdfDictionary.Put(PdfName.WHITEPOINT, value);
						pdfArray.Add(pdfDictionary);
					}
					return pdfArray;
				}
				if ((this.colorType & 2) == 0)
				{
					return PdfName.DEVICEGRAY;
				}
				return PdfName.DEVICERGB;
			}
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x000A22A8 File Offset: 0x000A12A8
		private Image GetImage()
		{
			this.ReadPng();
			int num = 0;
			int num2 = 0;
			this.palShades = false;
			if (this.trans != null)
			{
				for (int i = 0; i < this.trans.Length; i++)
				{
					int num3 = (int)(this.trans[i] & byte.MaxValue);
					if (num3 == 0)
					{
						num++;
						num2 = i;
					}
					if (num3 != 0 && num3 != 255)
					{
						this.palShades = true;
						break;
					}
				}
			}
			if ((this.colorType & 4) != 0)
			{
				this.palShades = true;
			}
			this.genBWMask = (!this.palShades && (num > 1 || this.transRedGray >= 0));
			if (!this.palShades && !this.genBWMask && num == 1)
			{
				this.additional.Put(PdfName.MASK, new PdfLiteral(string.Concat(new object[]
				{
					"[",
					num2,
					" ",
					num2,
					"]"
				})));
			}
			bool flag = this.interlaceMethod == 1 || this.bitDepth == 16 || (this.colorType & 4) != 0 || this.palShades || this.genBWMask;
			switch (this.colorType)
			{
			case 0:
				this.inputBands = 1;
				break;
			case 2:
				this.inputBands = 3;
				break;
			case 3:
				this.inputBands = 1;
				break;
			case 4:
				this.inputBands = 2;
				break;
			case 6:
				this.inputBands = 4;
				break;
			}
			if (flag)
			{
				this.DecodeIdat();
			}
			int num4 = this.inputBands;
			if ((this.colorType & 4) != 0)
			{
				num4--;
			}
			int num5 = this.bitDepth;
			if (num5 == 16)
			{
				num5 = 8;
			}
			Image image;
			if (this.image != null)
			{
				if (this.colorType == 3)
				{
					image = new ImgRaw(this.width, this.height, num4, num5, this.image);
				}
				else
				{
					image = Image.GetInstance(this.width, this.height, num4, num5, this.image);
				}
			}
			else
			{
				image = new ImgRaw(this.width, this.height, num4, num5, this.idat.ToArray());
				image.Deflated = true;
				PdfDictionary pdfDictionary = new PdfDictionary();
				pdfDictionary.Put(PdfName.BITSPERCOMPONENT, new PdfNumber(this.bitDepth));
				pdfDictionary.Put(PdfName.PREDICTOR, new PdfNumber(15));
				pdfDictionary.Put(PdfName.COLUMNS, new PdfNumber(this.width));
				pdfDictionary.Put(PdfName.COLORS, new PdfNumber((this.colorType == 3 || (this.colorType & 2) == 0) ? 1 : 3));
				this.additional.Put(PdfName.DECODEPARMS, pdfDictionary);
			}
			if (this.additional.Get(PdfName.COLORSPACE) == null)
			{
				this.additional.Put(PdfName.COLORSPACE, this.GetColorspace());
			}
			if (this.intent != null)
			{
				this.additional.Put(PdfName.INTENT, this.intent);
			}
			if (this.additional.Size > 0)
			{
				image.Additional = this.additional;
			}
			if (this.icc_profile != null)
			{
				image.TagICC = this.icc_profile;
			}
			if (this.palShades)
			{
				Image instance = Image.GetInstance(this.width, this.height, 1, 8, this.smask);
				instance.MakeMask();
				image.ImageMask = instance;
			}
			if (this.genBWMask)
			{
				Image instance2 = Image.GetInstance(this.width, this.height, 1, 1, this.smask);
				instance2.MakeMask();
				image.ImageMask = instance2;
			}
			image.SetDpi(this.dpiX, this.dpiY);
			image.XYRatio = this.XYRatio;
			image.OriginalType = 2;
			return image;
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x000A266C File Offset: 0x000A166C
		private void DecodeIdat()
		{
			int num = this.bitDepth;
			if (num == 16)
			{
				num = 8;
			}
			int num2 = -1;
			this.bytesPerPixel = ((this.bitDepth == 16) ? 2 : 1);
			switch (this.colorType)
			{
			case 0:
				num2 = (num * this.width + 7) / 8 * this.height;
				break;
			case 2:
				num2 = this.width * 3 * this.height;
				this.bytesPerPixel *= 3;
				break;
			case 3:
				if (this.interlaceMethod == 1)
				{
					num2 = (num * this.width + 7) / 8 * this.height;
				}
				this.bytesPerPixel = 1;
				break;
			case 4:
				num2 = this.width * this.height;
				this.bytesPerPixel *= 2;
				break;
			case 6:
				num2 = this.width * 3 * this.height;
				this.bytesPerPixel *= 4;
				break;
			}
			if (num2 >= 0)
			{
				this.image = new byte[num2];
			}
			if (this.palShades)
			{
				this.smask = new byte[this.width * this.height];
			}
			else if (this.genBWMask)
			{
				this.smask = new byte[(this.width + 7) / 8 * this.height];
			}
			this.idat.Position = 0L;
			this.dataStream = new ZInflaterInputStream(this.idat);
			if (this.interlaceMethod != 1)
			{
				this.DecodePass(0, 0, 1, 1, this.width, this.height);
				return;
			}
			this.DecodePass(0, 0, 8, 8, (this.width + 7) / 8, (this.height + 7) / 8);
			this.DecodePass(4, 0, 8, 8, (this.width + 3) / 8, (this.height + 7) / 8);
			this.DecodePass(0, 4, 4, 8, (this.width + 3) / 4, (this.height + 3) / 8);
			this.DecodePass(2, 0, 4, 4, (this.width + 1) / 4, (this.height + 3) / 4);
			this.DecodePass(0, 2, 2, 4, (this.width + 1) / 2, (this.height + 1) / 4);
			this.DecodePass(1, 0, 2, 2, this.width / 2, (this.height + 1) / 2);
			this.DecodePass(0, 1, 1, 2, this.width, this.height / 2);
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x000A28C4 File Offset: 0x000A18C4
		private void DecodePass(int xOffset, int yOffset, int xStep, int yStep, int passWidth, int passHeight)
		{
			if (passWidth == 0 || passHeight == 0)
			{
				return;
			}
			int num = (this.inputBands * passWidth * this.bitDepth + 7) / 8;
			byte[] array = new byte[num];
			byte[] array2 = new byte[num];
			int i = 0;
			int num2 = yOffset;
			while (i < passHeight)
			{
				int num3 = 0;
				try
				{
					num3 = this.dataStream.ReadByte();
					PngImage.ReadFully(this.dataStream, array, 0, num);
				}
				catch
				{
				}
				switch (num3)
				{
				case 0:
					break;
				case 1:
					PngImage.DecodeSubFilter(array, num, this.bytesPerPixel);
					break;
				case 2:
					PngImage.DecodeUpFilter(array, array2, num);
					break;
				case 3:
					PngImage.DecodeAverageFilter(array, array2, num, this.bytesPerPixel);
					break;
				case 4:
					PngImage.DecodePaethFilter(array, array2, num, this.bytesPerPixel);
					break;
				default:
					throw new Exception(MessageLocalization.GetComposedMessage("png.filter.unknown"));
				}
				this.ProcessPixels(array, xOffset, xStep, num2, passWidth);
				byte[] array3 = array2;
				array2 = array;
				array = array3;
				i++;
				num2 += yStep;
			}
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x000A29CC File Offset: 0x000A19CC
		private void ProcessPixels(byte[] curr, int xOffset, int step, int y, int width)
		{
			int[] pixel = this.GetPixel(curr);
			int num = 0;
			switch (this.colorType)
			{
			case 0:
			case 3:
			case 4:
				num = 1;
				break;
			case 2:
			case 6:
				num = 3;
				break;
			}
			int num2;
			if (this.image != null)
			{
				num2 = xOffset;
				int bytesPerRow = (num * this.width * ((this.bitDepth == 16) ? 8 : this.bitDepth) + 7) / 8;
				for (int i = 0; i < width; i++)
				{
					PngImage.SetPixel(this.image, pixel, this.inputBands * i, num, num2, y, this.bitDepth, bytesPerRow);
					num2 += step;
				}
			}
			if (!this.palShades)
			{
				if (this.genBWMask)
				{
					switch (this.colorType)
					{
					case 0:
					{
						int bytesPerRow2 = (this.width + 7) / 8;
						int[] array = new int[1];
						num2 = xOffset;
						for (int i = 0; i < width; i++)
						{
							int num3 = pixel[i];
							array[0] = ((num3 == this.transRedGray) ? 1 : 0);
							PngImage.SetPixel(this.smask, array, 0, 1, num2, y, 1, bytesPerRow2);
							num2 += step;
						}
						return;
					}
					case 1:
						break;
					case 2:
					{
						int bytesPerRow3 = (this.width + 7) / 8;
						int[] array2 = new int[1];
						num2 = xOffset;
						for (int i = 0; i < width; i++)
						{
							int num4 = this.inputBands * i;
							array2[0] = ((pixel[num4] == this.transRedGray && pixel[num4 + 1] == this.transGreen && pixel[num4 + 2] == this.transBlue) ? 1 : 0);
							PngImage.SetPixel(this.smask, array2, 0, 1, num2, y, 1, bytesPerRow3);
							num2 += step;
						}
						break;
					}
					case 3:
					{
						int bytesPerRow4 = (this.width + 7) / 8;
						int[] array3 = new int[1];
						num2 = xOffset;
						for (int i = 0; i < width; i++)
						{
							int num5 = pixel[i];
							array3[0] = ((num5 < this.trans.Length && this.trans[num5] == 0) ? 1 : 0);
							PngImage.SetPixel(this.smask, array3, 0, 1, num2, y, 1, bytesPerRow4);
							num2 += step;
						}
						return;
					}
					default:
						return;
					}
				}
				return;
			}
			if ((this.colorType & 4) != 0)
			{
				if (this.bitDepth == 16)
				{
					for (int j = 0; j < width; j++)
					{
						int num6 = j * this.inputBands + num;
						pixel[num6] = Util.USR(pixel[num6], 8);
					}
				}
				int bytesPerRow5 = this.width;
				num2 = xOffset;
				for (int i = 0; i < width; i++)
				{
					PngImage.SetPixel(this.smask, pixel, this.inputBands * i + num, 1, num2, y, 8, bytesPerRow5);
					num2 += step;
				}
				return;
			}
			int bytesPerRow6 = this.width;
			int[] array4 = new int[1];
			num2 = xOffset;
			for (int i = 0; i < width; i++)
			{
				int num7 = pixel[i];
				if (num7 < this.trans.Length)
				{
					array4[0] = (int)this.trans[num7];
				}
				else
				{
					array4[0] = 255;
				}
				PngImage.SetPixel(this.smask, array4, 0, 1, num2, y, 8, bytesPerRow6);
				num2 += step;
			}
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x000A2CB0 File Offset: 0x000A1CB0
		private static int GetPixel(byte[] image, int x, int y, int bitDepth, int bytesPerRow)
		{
			if (bitDepth == 8)
			{
				int num = bytesPerRow * y + x;
				return (int)(image[num] & byte.MaxValue);
			}
			int num2 = bytesPerRow * y + x / (8 / bitDepth);
			int num3 = image[num2] >> 8 - bitDepth * (x % (8 / bitDepth)) - bitDepth;
			return num3 & (1 << bitDepth) - 1;
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x000A2CFC File Offset: 0x000A1CFC
		private static void SetPixel(byte[] image, int[] data, int offset, int size, int x, int y, int bitDepth, int bytesPerRow)
		{
			if (bitDepth == 8)
			{
				int num = bytesPerRow * y + size * x;
				for (int i = 0; i < size; i++)
				{
					image[num + i] = (byte)data[i + offset];
				}
				return;
			}
			if (bitDepth == 16)
			{
				int num2 = bytesPerRow * y + size * x;
				for (int j = 0; j < size; j++)
				{
					image[num2 + j] = (byte)(data[j + offset] >> 8);
				}
				return;
			}
			int num3 = bytesPerRow * y + x / (8 / bitDepth);
			int num4 = data[offset] << 8 - bitDepth * (x % (8 / bitDepth)) - bitDepth;
			int num5 = num3;
			image[num5] |= (byte)num4;
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x000A2D9C File Offset: 0x000A1D9C
		private int[] GetPixel(byte[] curr)
		{
			int num = this.bitDepth;
			if (num == 8)
			{
				int[] array = new int[curr.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = (int)(curr[i] & byte.MaxValue);
				}
				return array;
			}
			if (num != 16)
			{
				int[] array2 = new int[curr.Length * 8 / this.bitDepth];
				int num2 = 0;
				int num3 = 8 / this.bitDepth;
				int num4 = (1 << this.bitDepth) - 1;
				for (int j = 0; j < curr.Length; j++)
				{
					for (int k = num3 - 1; k >= 0; k--)
					{
						array2[num2++] = (Util.USR((int)curr[j], this.bitDepth * k) & num4);
					}
				}
				return array2;
			}
			int[] array3 = new int[curr.Length / 2];
			for (int l = 0; l < array3.Length; l++)
			{
				array3[l] = ((int)(curr[l * 2] & byte.MaxValue) << 8) + (int)(curr[l * 2 + 1] & byte.MaxValue);
			}
			return array3;
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x000A2E94 File Offset: 0x000A1E94
		private static void DecodeSubFilter(byte[] curr, int count, int bpp)
		{
			for (int i = bpp; i < count; i++)
			{
				int num = (int)(curr[i] & byte.MaxValue);
				num += (int)(curr[i - bpp] & byte.MaxValue);
				curr[i] = (byte)num;
			}
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x000A2ECC File Offset: 0x000A1ECC
		private static void DecodeUpFilter(byte[] curr, byte[] prev, int count)
		{
			for (int i = 0; i < count; i++)
			{
				int num = (int)(curr[i] & byte.MaxValue);
				int num2 = (int)(prev[i] & byte.MaxValue);
				curr[i] = (byte)(num + num2);
			}
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x000A2F00 File Offset: 0x000A1F00
		private static void DecodeAverageFilter(byte[] curr, byte[] prev, int count, int bpp)
		{
			for (int i = 0; i < bpp; i++)
			{
				int num = (int)(curr[i] & byte.MaxValue);
				int num2 = (int)(prev[i] & byte.MaxValue);
				curr[i] = (byte)(num + num2 / 2);
			}
			for (int j = bpp; j < count; j++)
			{
				int num = (int)(curr[j] & byte.MaxValue);
				int num3 = (int)(curr[j - bpp] & byte.MaxValue);
				int num2 = (int)(prev[j] & byte.MaxValue);
				curr[j] = (byte)(num + (num3 + num2) / 2);
			}
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x000A2F78 File Offset: 0x000A1F78
		private static int PaethPredictor(int a, int b, int c)
		{
			int num = a + b - c;
			int num2 = Math.Abs(num - a);
			int num3 = Math.Abs(num - b);
			int num4 = Math.Abs(num - c);
			if (num2 <= num3 && num2 <= num4)
			{
				return a;
			}
			if (num3 <= num4)
			{
				return b;
			}
			return c;
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x000A2FB8 File Offset: 0x000A1FB8
		private static void DecodePaethFilter(byte[] curr, byte[] prev, int count, int bpp)
		{
			for (int i = 0; i < bpp; i++)
			{
				int num = (int)(curr[i] & byte.MaxValue);
				int num2 = (int)(prev[i] & byte.MaxValue);
				curr[i] = (byte)(num + num2);
			}
			for (int j = bpp; j < count; j++)
			{
				int num = (int)(curr[j] & byte.MaxValue);
				int a = (int)(curr[j - bpp] & byte.MaxValue);
				int num2 = (int)(prev[j] & byte.MaxValue);
				int c = (int)(prev[j - bpp] & byte.MaxValue);
				curr[j] = (byte)(num + PngImage.PaethPredictor(a, num2, c));
			}
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x000A3042 File Offset: 0x000A2042
		public static int GetInt(Stream isp)
		{
			return (isp.ReadByte() << 24) + (isp.ReadByte() << 16) + (isp.ReadByte() << 8) + isp.ReadByte();
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x000A3067 File Offset: 0x000A2067
		public static int GetWord(Stream isp)
		{
			return (isp.ReadByte() << 8) + isp.ReadByte();
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x000A3078 File Offset: 0x000A2078
		public static string GetString(Stream isp)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < 4; i++)
			{
				stringBuilder.Append((char)isp.ReadByte());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x000A30AC File Offset: 0x000A20AC
		private static void ReadFully(Stream inp, byte[] b, int offset, int count)
		{
			while (count > 0)
			{
				int num = inp.Read(b, offset, count);
				if (num <= 0)
				{
					throw new IOException(MessageLocalization.GetComposedMessage("insufficient.data"));
				}
				count -= num;
				offset += num;
			}
		}

		// Token: 0x04001248 RID: 4680
		public const string IHDR = "IHDR";

		// Token: 0x04001249 RID: 4681
		public const string PLTE = "PLTE";

		// Token: 0x0400124A RID: 4682
		public const string IDAT = "IDAT";

		// Token: 0x0400124B RID: 4683
		public const string IEND = "IEND";

		// Token: 0x0400124C RID: 4684
		public const string tRNS = "tRNS";

		// Token: 0x0400124D RID: 4685
		public const string pHYs = "pHYs";

		// Token: 0x0400124E RID: 4686
		public const string gAMA = "gAMA";

		// Token: 0x0400124F RID: 4687
		public const string cHRM = "cHRM";

		// Token: 0x04001250 RID: 4688
		public const string sRGB = "sRGB";

		// Token: 0x04001251 RID: 4689
		public const string iCCP = "iCCP";

		// Token: 0x04001252 RID: 4690
		private const int TRANSFERSIZE = 4096;

		// Token: 0x04001253 RID: 4691
		private const int PNG_FILTER_NONE = 0;

		// Token: 0x04001254 RID: 4692
		private const int PNG_FILTER_SUB = 1;

		// Token: 0x04001255 RID: 4693
		private const int PNG_FILTER_UP = 2;

		// Token: 0x04001256 RID: 4694
		private const int PNG_FILTER_AVERAGE = 3;

		// Token: 0x04001257 RID: 4695
		private const int PNG_FILTER_PAETH = 4;

		// Token: 0x04001258 RID: 4696
		public static int[] PNGID = new int[]
		{
			137,
			80,
			78,
			71,
			13,
			10,
			26,
			10
		};

		// Token: 0x04001259 RID: 4697
		private static PdfName[] intents = new PdfName[]
		{
			PdfName.PERCEPTUAL,
			PdfName.RELATIVECOLORIMETRIC,
			PdfName.SATURATION,
			PdfName.ABSOLUTECOLORIMETRIC
		};

		// Token: 0x0400125A RID: 4698
		private Stream isp;

		// Token: 0x0400125B RID: 4699
		private Stream dataStream;

		// Token: 0x0400125C RID: 4700
		private int width;

		// Token: 0x0400125D RID: 4701
		private int height;

		// Token: 0x0400125E RID: 4702
		private int bitDepth;

		// Token: 0x0400125F RID: 4703
		private int colorType;

		// Token: 0x04001260 RID: 4704
		private int compressionMethod;

		// Token: 0x04001261 RID: 4705
		private int filterMethod;

		// Token: 0x04001262 RID: 4706
		private int interlaceMethod;

		// Token: 0x04001263 RID: 4707
		private PdfDictionary additional = new PdfDictionary();

		// Token: 0x04001264 RID: 4708
		private byte[] image;

		// Token: 0x04001265 RID: 4709
		private byte[] smask;

		// Token: 0x04001266 RID: 4710
		private byte[] trans;

		// Token: 0x04001267 RID: 4711
		private MemoryStream idat = new MemoryStream();

		// Token: 0x04001268 RID: 4712
		private int dpiX;

		// Token: 0x04001269 RID: 4713
		private int dpiY;

		// Token: 0x0400126A RID: 4714
		private float XYRatio;

		// Token: 0x0400126B RID: 4715
		private bool genBWMask;

		// Token: 0x0400126C RID: 4716
		private bool palShades;

		// Token: 0x0400126D RID: 4717
		private int transRedGray = -1;

		// Token: 0x0400126E RID: 4718
		private int transGreen = -1;

		// Token: 0x0400126F RID: 4719
		private int transBlue = -1;

		// Token: 0x04001270 RID: 4720
		private int inputBands;

		// Token: 0x04001271 RID: 4721
		private int bytesPerPixel;

		// Token: 0x04001272 RID: 4722
		private byte[] colorTable;

		// Token: 0x04001273 RID: 4723
		private float gamma = 1f;

		// Token: 0x04001274 RID: 4724
		private bool hasCHRM;

		// Token: 0x04001275 RID: 4725
		private float xW;

		// Token: 0x04001276 RID: 4726
		private float yW;

		// Token: 0x04001277 RID: 4727
		private float xR;

		// Token: 0x04001278 RID: 4728
		private float yR;

		// Token: 0x04001279 RID: 4729
		private float xG;

		// Token: 0x0400127A RID: 4730
		private float yG;

		// Token: 0x0400127B RID: 4731
		private float xB;

		// Token: 0x0400127C RID: 4732
		private float yB;

		// Token: 0x0400127D RID: 4733
		private PdfName intent;

		// Token: 0x0400127E RID: 4734
		private ICC_Profile icc_profile;
	}
}
