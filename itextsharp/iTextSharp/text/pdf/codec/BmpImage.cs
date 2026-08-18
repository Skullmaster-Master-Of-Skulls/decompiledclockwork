using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.util;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf.codec
{
	// Token: 0x02000534 RID: 1332
	public class BmpImage
	{
		// Token: 0x06002DC6 RID: 11718 RVA: 0x00118CDF File Offset: 0x00117CDF
		internal BmpImage(Stream isp, bool noHeader, int size)
		{
			this.bitmapFileSize = (long)size;
			this.bitmapOffset = 0L;
			this.Process(isp, noHeader);
		}

		// Token: 0x06002DC7 RID: 11719 RVA: 0x00118D0C File Offset: 0x00117D0C
		public static Image GetImage(Uri url)
		{
			Stream stream = null;
			Image result;
			try
			{
				stream = WebRequest.Create(url).GetResponse().GetResponseStream();
				Image image = BmpImage.GetImage(stream);
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

		// Token: 0x06002DC8 RID: 11720 RVA: 0x00118D5C File Offset: 0x00117D5C
		public static Image GetImage(Stream isp)
		{
			return BmpImage.GetImage(isp, false, 0);
		}

		// Token: 0x06002DC9 RID: 11721 RVA: 0x00118D68 File Offset: 0x00117D68
		public static Image GetImage(Stream isp, bool noHeader, int size)
		{
			BmpImage bmpImage = new BmpImage(isp, noHeader, size);
			Image image = bmpImage.GetImage();
			image.SetDpi((int)((double)bmpImage.xPelsPerMeter * 0.0254 + 0.5), (int)((double)bmpImage.yPelsPerMeter * 0.0254 + 0.5));
			image.OriginalType = 4;
			return image;
		}

		// Token: 0x06002DCA RID: 11722 RVA: 0x00118DCB File Offset: 0x00117DCB
		public static Image GetImage(string file)
		{
			return BmpImage.GetImage(Utilities.ToURL(file));
		}

		// Token: 0x06002DCB RID: 11723 RVA: 0x00118DD8 File Offset: 0x00117DD8
		public static Image GetImage(byte[] data)
		{
			Stream isp = new MemoryStream(data);
			Image image = BmpImage.GetImage(isp);
			image.OriginalData = data;
			return image;
		}

		// Token: 0x06002DCC RID: 11724 RVA: 0x00118DFC File Offset: 0x00117DFC
		protected void Process(Stream stream, bool noHeader)
		{
			if (noHeader || stream is BufferedStream)
			{
				this.inputStream = stream;
			}
			else
			{
				this.inputStream = new BufferedStream(stream);
			}
			if (!noHeader)
			{
				if (this.ReadUnsignedByte(this.inputStream) != 66 || this.ReadUnsignedByte(this.inputStream) != 77)
				{
					throw new Exception(MessageLocalization.GetComposedMessage("invalid.magic.value.for.bmp.file"));
				}
				this.bitmapFileSize = this.ReadDWord(this.inputStream);
				this.ReadWord(this.inputStream);
				this.ReadWord(this.inputStream);
				this.bitmapOffset = this.ReadDWord(this.inputStream);
			}
			long num = this.ReadDWord(this.inputStream);
			if (num == 12L)
			{
				this.width = this.ReadWord(this.inputStream);
				this.height = this.ReadWord(this.inputStream);
			}
			else
			{
				this.width = this.ReadLong(this.inputStream);
				this.height = this.ReadLong(this.inputStream);
			}
			int num2 = this.ReadWord(this.inputStream);
			this.bitsPerPixel = this.ReadWord(this.inputStream);
			this.properties["color_planes"] = num2;
			this.properties["bits_per_pixel"] = this.bitsPerPixel;
			this.numBands = 3;
			if (this.bitmapOffset == 0L)
			{
				this.bitmapOffset = num;
			}
			if (num == 12L)
			{
				this.properties["bmp_version"] = "BMP v. 2.x";
				if (this.bitsPerPixel == 1)
				{
					this.imageType = 0;
				}
				else if (this.bitsPerPixel == 4)
				{
					this.imageType = 1;
				}
				else if (this.bitsPerPixel == 8)
				{
					this.imageType = 2;
				}
				else if (this.bitsPerPixel == 24)
				{
					this.imageType = 3;
				}
				int num3 = (int)((this.bitmapOffset - 14L - num) / 3L);
				int num4 = num3 * 3;
				if (this.bitmapOffset == num)
				{
					switch (this.imageType)
					{
					case 0:
						num4 = 6;
						break;
					case 1:
						num4 = 48;
						break;
					case 2:
						num4 = 768;
						break;
					case 3:
						num4 = 0;
						break;
					}
					this.bitmapOffset = num + (long)num4;
				}
				this.ReadPalette(num4);
			}
			else
			{
				this.compression = this.ReadDWord(this.inputStream);
				this.imageSize = this.ReadDWord(this.inputStream);
				this.xPelsPerMeter = (long)this.ReadLong(this.inputStream);
				this.yPelsPerMeter = (long)this.ReadLong(this.inputStream);
				long num5 = this.ReadDWord(this.inputStream);
				long num6 = this.ReadDWord(this.inputStream);
				switch ((int)this.compression)
				{
				case 0:
					this.properties["compression"] = "BI_RGB";
					break;
				case 1:
					this.properties["compression"] = "BI_RLE8";
					break;
				case 2:
					this.properties["compression"] = "BI_RLE4";
					break;
				case 3:
					this.properties["compression"] = "BI_BITFIELDS";
					break;
				}
				this.properties["x_pixels_per_meter"] = this.xPelsPerMeter;
				this.properties["y_pixels_per_meter"] = this.yPelsPerMeter;
				this.properties["colors_used"] = num5;
				this.properties["colors_important"] = num6;
				if (num == 40L)
				{
					switch ((int)this.compression)
					{
					case 0:
					case 1:
					case 2:
					{
						if (this.bitsPerPixel == 1)
						{
							this.imageType = 4;
						}
						else if (this.bitsPerPixel == 4)
						{
							this.imageType = 5;
						}
						else if (this.bitsPerPixel == 8)
						{
							this.imageType = 6;
						}
						else if (this.bitsPerPixel == 24)
						{
							this.imageType = 7;
						}
						else if (this.bitsPerPixel == 16)
						{
							this.imageType = 8;
							this.redMask = 31744;
							this.greenMask = 992;
							this.blueMask = 31;
							this.properties["red_mask"] = this.redMask;
							this.properties["green_mask"] = this.greenMask;
							this.properties["blue_mask"] = this.blueMask;
						}
						else if (this.bitsPerPixel == 32)
						{
							this.imageType = 9;
							this.redMask = 16711680;
							this.greenMask = 65280;
							this.blueMask = 255;
							this.properties["red_mask"] = this.redMask;
							this.properties["green_mask"] = this.greenMask;
							this.properties["blue_mask"] = this.blueMask;
						}
						int num7 = (int)((this.bitmapOffset - 14L - num) / 4L);
						int num8 = num7 * 4;
						if (this.bitmapOffset == num)
						{
							switch (this.imageType)
							{
							case 4:
								num8 = (int)((num5 == 0L) ? 2L : num5) * 4;
								break;
							case 5:
								num8 = (int)((num5 == 0L) ? 16L : num5) * 4;
								break;
							case 6:
								num8 = (int)((num5 == 0L) ? 256L : num5) * 4;
								break;
							default:
								num8 = 0;
								break;
							}
							this.bitmapOffset = num + (long)num8;
						}
						this.ReadPalette(num8);
						this.properties["bmp_version"] = "BMP v. 3.x";
						break;
					}
					case 3:
						if (this.bitsPerPixel == 16)
						{
							this.imageType = 8;
						}
						else if (this.bitsPerPixel == 32)
						{
							this.imageType = 9;
						}
						this.redMask = (int)this.ReadDWord(this.inputStream);
						this.greenMask = (int)this.ReadDWord(this.inputStream);
						this.blueMask = (int)this.ReadDWord(this.inputStream);
						this.properties["red_mask"] = this.redMask;
						this.properties["green_mask"] = this.greenMask;
						this.properties["blue_mask"] = this.blueMask;
						if (num5 != 0L)
						{
							int num8 = (int)num5 * 4;
							this.ReadPalette(num8);
						}
						this.properties["bmp_version"] = "BMP v. 3.x NT";
						break;
					default:
						throw new Exception("Invalid compression specified in BMP file.");
					}
				}
				else
				{
					if (num != 108L)
					{
						this.properties["bmp_version"] = "BMP v. 5.x";
						throw new Exception("BMP version 5 not implemented yet.");
					}
					this.properties["bmp_version"] = "BMP v. 4.x";
					this.redMask = (int)this.ReadDWord(this.inputStream);
					this.greenMask = (int)this.ReadDWord(this.inputStream);
					this.blueMask = (int)this.ReadDWord(this.inputStream);
					this.alphaMask = (int)this.ReadDWord(this.inputStream);
					long num9 = this.ReadDWord(this.inputStream);
					int num10 = this.ReadLong(this.inputStream);
					int num11 = this.ReadLong(this.inputStream);
					int num12 = this.ReadLong(this.inputStream);
					int num13 = this.ReadLong(this.inputStream);
					int num14 = this.ReadLong(this.inputStream);
					int num15 = this.ReadLong(this.inputStream);
					int num16 = this.ReadLong(this.inputStream);
					int num17 = this.ReadLong(this.inputStream);
					int num18 = this.ReadLong(this.inputStream);
					long num19 = this.ReadDWord(this.inputStream);
					long num20 = this.ReadDWord(this.inputStream);
					long num21 = this.ReadDWord(this.inputStream);
					if (this.bitsPerPixel == 1)
					{
						this.imageType = 10;
					}
					else if (this.bitsPerPixel == 4)
					{
						this.imageType = 11;
					}
					else if (this.bitsPerPixel == 8)
					{
						this.imageType = 12;
					}
					else if (this.bitsPerPixel == 16)
					{
						this.imageType = 13;
						if ((int)this.compression == 0)
						{
							this.redMask = 31744;
							this.greenMask = 992;
							this.blueMask = 31;
						}
					}
					else if (this.bitsPerPixel == 24)
					{
						this.imageType = 14;
					}
					else if (this.bitsPerPixel == 32)
					{
						this.imageType = 15;
						if ((int)this.compression == 0)
						{
							this.redMask = 16711680;
							this.greenMask = 65280;
							this.blueMask = 255;
						}
					}
					this.properties["red_mask"] = this.redMask;
					this.properties["green_mask"] = this.greenMask;
					this.properties["blue_mask"] = this.blueMask;
					this.properties["alpha_mask"] = this.alphaMask;
					int num22 = (int)((this.bitmapOffset - 14L - num) / 4L);
					int num23 = num22 * 4;
					if (this.bitmapOffset == num)
					{
						switch (this.imageType)
						{
						case 10:
							num23 = (int)((num5 == 0L) ? 2L : num5) * 4;
							break;
						case 11:
							num23 = (int)((num5 == 0L) ? 16L : num5) * 4;
							break;
						case 12:
							num23 = (int)((num5 == 0L) ? 256L : num5) * 4;
							break;
						default:
							num23 = 0;
							break;
						}
						this.bitmapOffset = num + (long)num23;
					}
					this.ReadPalette(num23);
					switch ((int)num9)
					{
					case 0:
						this.properties["color_space"] = "LCS_CALIBRATED_RGB";
						this.properties["redX"] = num10;
						this.properties["redY"] = num11;
						this.properties["redZ"] = num12;
						this.properties["greenX"] = num13;
						this.properties["greenY"] = num14;
						this.properties["greenZ"] = num15;
						this.properties["blueX"] = num16;
						this.properties["blueY"] = num17;
						this.properties["blueZ"] = num18;
						this.properties["gamma_red"] = num19;
						this.properties["gamma_green"] = num20;
						this.properties["gamma_blue"] = num21;
						throw new Exception("Not implemented yet.");
					case 1:
						this.properties["color_space"] = "LCS_sRGB";
						break;
					case 2:
						this.properties["color_space"] = "LCS_CMYK";
						throw new Exception("Not implemented yet.");
					}
				}
			}
			if (this.height > 0)
			{
				this.isBottomUp = true;
			}
			else
			{
				this.isBottomUp = false;
				this.height = Math.Abs(this.height);
			}
			if (this.bitsPerPixel == 1 || this.bitsPerPixel == 4 || this.bitsPerPixel == 8)
			{
				this.numBands = 1;
				int num24;
				byte[] array;
				byte[] array2;
				byte[] array3;
				if (this.imageType == 0 || this.imageType == 1 || this.imageType == 2)
				{
					num24 = this.palette.Length / 3;
					if (num24 > 256)
					{
						num24 = 256;
					}
					array = new byte[num24];
					array2 = new byte[num24];
					array3 = new byte[num24];
					for (int i = 0; i < num24; i++)
					{
						int num25 = 3 * i;
						array3[i] = this.palette[num25];
						array2[i] = this.palette[num25 + 1];
						array[i] = this.palette[num25 + 2];
					}
					return;
				}
				num24 = this.palette.Length / 4;
				if (num24 > 256)
				{
					num24 = 256;
				}
				array = new byte[num24];
				array2 = new byte[num24];
				array3 = new byte[num24];
				for (int j = 0; j < num24; j++)
				{
					int num26 = 4 * j;
					array3[j] = this.palette[num26];
					array2[j] = this.palette[num26 + 1];
					array[j] = this.palette[num26 + 2];
				}
				return;
			}
			else
			{
				if (this.bitsPerPixel == 16)
				{
					this.numBands = 3;
					return;
				}
				if (this.bitsPerPixel == 32)
				{
					this.numBands = ((this.alphaMask == 0) ? 3 : 4);
					return;
				}
				this.numBands = 3;
				return;
			}
		}

		// Token: 0x06002DCD RID: 11725 RVA: 0x00119AC4 File Offset: 0x00118AC4
		private byte[] GetPalette(int group)
		{
			if (this.palette == null)
			{
				return null;
			}
			byte[] array = new byte[this.palette.Length / group * 3];
			int num = this.palette.Length / group;
			for (int i = 0; i < num; i++)
			{
				int num2 = i * group;
				int num3 = i * 3;
				array[num3 + 2] = this.palette[num2++];
				array[num3 + 1] = this.palette[num2++];
				array[num3] = this.palette[num2];
			}
			return array;
		}

		// Token: 0x06002DCE RID: 11726 RVA: 0x00119B40 File Offset: 0x00118B40
		private Image GetImage()
		{
			switch (this.imageType)
			{
			case 0:
				return this.Read1Bit(3);
			case 1:
				return this.Read4Bit(3);
			case 2:
				return this.Read8Bit(3);
			case 3:
			{
				byte[] array = new byte[this.width * this.height * 3];
				this.Read24Bit(array);
				return new ImgRaw(this.width, this.height, 3, 8, array);
			}
			case 4:
				return this.Read1Bit(4);
			case 5:
				switch ((int)this.compression)
				{
				case 0:
					return this.Read4Bit(4);
				case 2:
					return this.ReadRLE4();
				}
				throw new Exception("Invalid compression specified for BMP file.");
			case 6:
				switch ((int)this.compression)
				{
				case 0:
					return this.Read8Bit(4);
				case 1:
					return this.ReadRLE8();
				default:
					throw new Exception("Invalid compression specified for BMP file.");
				}
				break;
			case 7:
			{
				byte[] array = new byte[this.width * this.height * 3];
				this.Read24Bit(array);
				return new ImgRaw(this.width, this.height, 3, 8, array);
			}
			case 8:
				return this.Read1632Bit(false);
			case 9:
				return this.Read1632Bit(true);
			case 10:
				return this.Read1Bit(4);
			case 11:
				switch ((int)this.compression)
				{
				case 0:
					return this.Read4Bit(4);
				case 2:
					return this.ReadRLE4();
				}
				throw new Exception("Invalid compression specified for BMP file.");
			case 12:
				switch ((int)this.compression)
				{
				case 0:
					return this.Read8Bit(4);
				case 1:
					return this.ReadRLE8();
				default:
					throw new Exception("Invalid compression specified for BMP file.");
				}
				break;
			case 13:
				return this.Read1632Bit(false);
			case 14:
			{
				byte[] array = new byte[this.width * this.height * 3];
				this.Read24Bit(array);
				return new ImgRaw(this.width, this.height, 3, 8, array);
			}
			case 15:
				return this.Read1632Bit(true);
			default:
				return null;
			}
		}

		// Token: 0x06002DCF RID: 11727 RVA: 0x00119D54 File Offset: 0x00118D54
		private Image IndexedModel(byte[] bdata, int bpc, int paletteEntries)
		{
			Image image = new ImgRaw(this.width, this.height, 1, bpc, bdata);
			PdfArray pdfArray = new PdfArray();
			pdfArray.Add(PdfName.INDEXED);
			pdfArray.Add(PdfName.DEVICERGB);
			byte[] array = this.GetPalette(paletteEntries);
			int num = array.Length;
			pdfArray.Add(new PdfNumber(num / 3 - 1));
			pdfArray.Add(new PdfString(array));
			PdfDictionary pdfDictionary = new PdfDictionary();
			pdfDictionary.Put(PdfName.COLORSPACE, pdfArray);
			image.Additional = pdfDictionary;
			return image;
		}

		// Token: 0x06002DD0 RID: 11728 RVA: 0x00119DDC File Offset: 0x00118DDC
		private void ReadPalette(int sizeOfPalette)
		{
			if (sizeOfPalette == 0)
			{
				return;
			}
			this.palette = new byte[sizeOfPalette];
			int num;
			for (int i = 0; i < sizeOfPalette; i += num)
			{
				num = this.inputStream.Read(this.palette, i, sizeOfPalette - i);
				if (num <= 0)
				{
					throw new IOException(MessageLocalization.GetComposedMessage("incomplete.palette"));
				}
			}
			this.properties["palette"] = this.palette;
		}

		// Token: 0x06002DD1 RID: 11729 RVA: 0x00119E48 File Offset: 0x00118E48
		private Image Read1Bit(int paletteEntries)
		{
			byte[] array = new byte[(this.width + 7) / 8 * this.height];
			int num = 0;
			int num2 = (int)Math.Ceiling((double)this.width / 8.0);
			int num3 = num2 % 4;
			if (num3 != 0)
			{
				num = 4 - num3;
			}
			int num4 = (num2 + num) * this.height;
			byte[] array2 = new byte[num4];
			for (int i = 0; i < num4; i += this.inputStream.Read(array2, i, num4 - i))
			{
			}
			if (this.isBottomUp)
			{
				for (int j = 0; j < this.height; j++)
				{
					Array.Copy(array2, num4 - (j + 1) * (num2 + num), array, j * num2, num2);
				}
			}
			else
			{
				for (int k = 0; k < this.height; k++)
				{
					Array.Copy(array2, k * (num2 + num), array, k * num2, num2);
				}
			}
			return this.IndexedModel(array, 1, paletteEntries);
		}

		// Token: 0x06002DD2 RID: 11730 RVA: 0x00119F34 File Offset: 0x00118F34
		private Image Read4Bit(int paletteEntries)
		{
			byte[] array = new byte[(this.width + 1) / 2 * this.height];
			int num = 0;
			int num2 = (int)Math.Ceiling((double)this.width / 2.0);
			int num3 = num2 % 4;
			if (num3 != 0)
			{
				num = 4 - num3;
			}
			int num4 = (num2 + num) * this.height;
			byte[] array2 = new byte[num4];
			for (int i = 0; i < num4; i += this.inputStream.Read(array2, i, num4 - i))
			{
			}
			if (this.isBottomUp)
			{
				for (int j = 0; j < this.height; j++)
				{
					Array.Copy(array2, num4 - (j + 1) * (num2 + num), array, j * num2, num2);
				}
			}
			else
			{
				for (int k = 0; k < this.height; k++)
				{
					Array.Copy(array2, k * (num2 + num), array, k * num2, num2);
				}
			}
			return this.IndexedModel(array, 4, paletteEntries);
		}

		// Token: 0x06002DD3 RID: 11731 RVA: 0x0011A020 File Offset: 0x00119020
		private Image Read8Bit(int paletteEntries)
		{
			byte[] array = new byte[this.width * this.height];
			int num = 0;
			int num2 = this.width * 8;
			if (num2 % 32 != 0)
			{
				num = (num2 / 32 + 1) * 32 - num2;
				num = (int)Math.Ceiling((double)num / 8.0);
			}
			int num3 = (this.width + num) * this.height;
			byte[] array2 = new byte[num3];
			for (int i = 0; i < num3; i += this.inputStream.Read(array2, i, num3 - i))
			{
			}
			if (this.isBottomUp)
			{
				for (int j = 0; j < this.height; j++)
				{
					Array.Copy(array2, num3 - (j + 1) * (this.width + num), array, j * this.width, this.width);
				}
			}
			else
			{
				for (int k = 0; k < this.height; k++)
				{
					Array.Copy(array2, k * (this.width + num), array, k * this.width, this.width);
				}
			}
			return this.IndexedModel(array, 8, paletteEntries);
		}

		// Token: 0x06002DD4 RID: 11732 RVA: 0x0011A130 File Offset: 0x00119130
		private void Read24Bit(byte[] bdata)
		{
			int num = 0;
			int num2 = this.width * 24;
			if (num2 % 32 != 0)
			{
				num = (num2 / 32 + 1) * 32 - num2;
				num = (int)Math.Ceiling((double)num / 8.0);
			}
			int num3 = (this.width * 3 + 3) / 4 * 4 * this.height;
			byte[] array = new byte[num3];
			int num4;
			for (int i = 0; i < num3; i += num4)
			{
				num4 = this.inputStream.Read(array, i, num3 - i);
				if (num4 < 0)
				{
					break;
				}
			}
			int num5 = 0;
			int num7;
			if (this.isBottomUp)
			{
				int num6 = this.width * this.height * 3 - 1;
				num7 = -num;
				for (int j = 0; j < this.height; j++)
				{
					num5 = num6 - (j + 1) * this.width * 3 + 1;
					num7 += num;
					for (int k = 0; k < this.width; k++)
					{
						bdata[num5 + 2] = array[num7++];
						bdata[num5 + 1] = array[num7++];
						bdata[num5] = array[num7++];
						num5 += 3;
					}
				}
				return;
			}
			num7 = -num;
			for (int l = 0; l < this.height; l++)
			{
				num7 += num;
				for (int m = 0; m < this.width; m++)
				{
					bdata[num5 + 2] = array[num7++];
					bdata[num5 + 1] = array[num7++];
					bdata[num5] = array[num7++];
					num5 += 3;
				}
			}
		}

		// Token: 0x06002DD5 RID: 11733 RVA: 0x0011A2B8 File Offset: 0x001192B8
		private int FindMask(int mask)
		{
			int num = 0;
			while (num < 32 && (mask & 1) != 1)
			{
				mask = Util.USR(mask, 1);
				num++;
			}
			return mask;
		}

		// Token: 0x06002DD6 RID: 11734 RVA: 0x0011A2E4 File Offset: 0x001192E4
		private int FindShift(int mask)
		{
			int num = 0;
			while (num < 32 && (mask & 1) != 1)
			{
				mask = Util.USR(mask, 1);
				num++;
			}
			return num;
		}

		// Token: 0x06002DD7 RID: 11735 RVA: 0x0011A310 File Offset: 0x00119310
		private Image Read1632Bit(bool is32)
		{
			int num = this.FindMask(this.redMask);
			int op = this.FindShift(this.redMask);
			int num2 = num + 1;
			int num3 = this.FindMask(this.greenMask);
			int op2 = this.FindShift(this.greenMask);
			int num4 = num3 + 1;
			int num5 = this.FindMask(this.blueMask);
			int op3 = this.FindShift(this.blueMask);
			int num6 = num5 + 1;
			byte[] array = new byte[this.width * this.height * 3];
			int num7 = 0;
			if (!is32)
			{
				int num8 = this.width * 16;
				if (num8 % 32 != 0)
				{
					num7 = (num8 / 32 + 1) * 32 - num8;
					num7 = (int)Math.Ceiling((double)num7 / 8.0);
				}
			}
			if ((int)this.imageSize == 0)
			{
				int num9 = (int)(this.bitmapFileSize - this.bitmapOffset);
			}
			int num10 = 0;
			if (this.isBottomUp)
			{
				for (int i = this.height - 1; i >= 0; i--)
				{
					num10 = this.width * 3 * i;
					for (int j = 0; j < this.width; j++)
					{
						int op4;
						if (is32)
						{
							op4 = (int)this.ReadDWord(this.inputStream);
						}
						else
						{
							op4 = this.ReadWord(this.inputStream);
						}
						array[num10++] = (byte)((Util.USR(op4, op) & num) * 256 / num2);
						array[num10++] = (byte)((Util.USR(op4, op2) & num3) * 256 / num4);
						array[num10++] = (byte)((Util.USR(op4, op3) & num5) * 256 / num6);
					}
					for (int k = 0; k < num7; k++)
					{
						this.inputStream.ReadByte();
					}
				}
			}
			else
			{
				for (int l = 0; l < this.height; l++)
				{
					for (int m = 0; m < this.width; m++)
					{
						int op4;
						if (is32)
						{
							op4 = (int)this.ReadDWord(this.inputStream);
						}
						else
						{
							op4 = this.ReadWord(this.inputStream);
						}
						array[num10++] = (byte)((Util.USR(op4, op) & num) * 256 / num2);
						array[num10++] = (byte)((Util.USR(op4, op2) & num3) * 256 / num4);
						array[num10++] = (byte)((Util.USR(op4, op3) & num5) * 256 / num6);
					}
					for (int n = 0; n < num7; n++)
					{
						this.inputStream.ReadByte();
					}
				}
			}
			return new ImgRaw(this.width, this.height, 3, 8, array);
		}

		// Token: 0x06002DD8 RID: 11736 RVA: 0x0011A5C8 File Offset: 0x001195C8
		private Image ReadRLE8()
		{
			int num = (int)this.imageSize;
			if (num == 0)
			{
				num = (int)(this.bitmapFileSize - this.bitmapOffset);
			}
			byte[] array = new byte[num];
			for (int i = 0; i < num; i += this.inputStream.Read(array, i, num - i))
			{
			}
			byte[] array2 = this.DecodeRLE(true, array);
			num = this.width * this.height;
			if (this.isBottomUp)
			{
				byte[] array3 = new byte[array2.Length];
				int num2 = this.width;
				for (int j = 0; j < this.height; j++)
				{
					Array.Copy(array2, num - (j + 1) * num2, array3, j * num2, num2);
				}
				array2 = array3;
			}
			return this.IndexedModel(array2, 8, 4);
		}

		// Token: 0x06002DD9 RID: 11737 RVA: 0x0011A67C File Offset: 0x0011967C
		private Image ReadRLE4()
		{
			int num = (int)this.imageSize;
			if (num == 0)
			{
				num = (int)(this.bitmapFileSize - this.bitmapOffset);
			}
			byte[] array = new byte[num];
			for (int i = 0; i < num; i += this.inputStream.Read(array, i, num - i))
			{
			}
			byte[] array2 = this.DecodeRLE(false, array);
			if (this.isBottomUp)
			{
				byte[] array3 = array2;
				array2 = new byte[this.width * this.height];
				int num2 = 0;
				for (int j = this.height - 1; j >= 0; j--)
				{
					int num3 = j * this.width;
					int num4 = num2 + this.width;
					while (num2 != num4)
					{
						array2[num2++] = array3[num3++];
					}
				}
			}
			int num5 = (this.width + 1) / 2;
			byte[] array4 = new byte[num5 * this.height];
			int num6 = 0;
			int num7 = 0;
			for (int k = 0; k < this.height; k++)
			{
				for (int l = 0; l < this.width; l++)
				{
					if ((l & 1) == 0)
					{
						array4[num7 + l / 2] = (byte)(array2[num6++] << 4);
					}
					else
					{
						byte[] array5 = array4;
						int num8 = num7 + l / 2;
						array5[num8] |= (array2[num6++] & 15);
					}
				}
				num7 += num5;
			}
			return this.IndexedModel(array4, 4, 4);
		}

		// Token: 0x06002DDA RID: 11738 RVA: 0x0011A7DC File Offset: 0x001197DC
		private byte[] DecodeRLE(bool is8, byte[] values)
		{
			byte[] array = new byte[this.width * this.height];
			try
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				while (num4 < this.height && num < values.Length)
				{
					int num5 = (int)(values[num++] & byte.MaxValue);
					if (num5 != 0)
					{
						int num6 = (int)(values[num++] & byte.MaxValue);
						if (is8)
						{
							for (int num7 = num5; num7 != 0; num7--)
							{
								array[num3++] = (byte)num6;
							}
						}
						else
						{
							for (int i = 0; i < num5; i++)
							{
								array[num3++] = (byte)(((i & 1) == 1) ? (num6 & 15) : (num6 >> 4 & 15));
							}
						}
						num2 += num5;
					}
					else
					{
						num5 = (int)(values[num++] & byte.MaxValue);
						if (num5 == 1)
						{
							break;
						}
						switch (num5)
						{
						case 0:
							num2 = 0;
							num4++;
							num3 = num4 * this.width;
							continue;
						case 2:
							num2 += (int)(values[num++] & byte.MaxValue);
							num4 += (int)(values[num++] & byte.MaxValue);
							num3 = num4 * this.width + num2;
							continue;
						}
						if (is8)
						{
							for (int num8 = num5; num8 != 0; num8--)
							{
								array[num3++] = (values[num++] & byte.MaxValue);
							}
						}
						else
						{
							int num9 = 0;
							for (int j = 0; j < num5; j++)
							{
								if ((j & 1) == 0)
								{
									num9 = (int)(values[num++] & byte.MaxValue);
								}
								array[num3++] = (byte)(((j & 1) == 1) ? (num9 & 15) : (num9 >> 4 & 15));
							}
						}
						num2 += num5;
						if (is8)
						{
							if ((num5 & 1) == 1)
							{
								num++;
							}
						}
						else if ((num5 & 3) == 1 || (num5 & 3) == 2)
						{
							num++;
						}
					}
				}
			}
			catch
			{
			}
			return array;
		}

		// Token: 0x06002DDB RID: 11739 RVA: 0x0011A9CC File Offset: 0x001199CC
		private int ReadUnsignedByte(Stream stream)
		{
			return stream.ReadByte() & 255;
		}

		// Token: 0x06002DDC RID: 11740 RVA: 0x0011A9DC File Offset: 0x001199DC
		private int ReadUnsignedShort(Stream stream)
		{
			int num = this.ReadUnsignedByte(stream);
			int num2 = this.ReadUnsignedByte(stream);
			return (num2 << 8 | num) & 65535;
		}

		// Token: 0x06002DDD RID: 11741 RVA: 0x0011AA04 File Offset: 0x00119A04
		private int ReadShort(Stream stream)
		{
			int num = this.ReadUnsignedByte(stream);
			int num2 = this.ReadUnsignedByte(stream);
			return num2 << 8 | num;
		}

		// Token: 0x06002DDE RID: 11742 RVA: 0x0011AA26 File Offset: 0x00119A26
		private int ReadWord(Stream stream)
		{
			return this.ReadUnsignedShort(stream);
		}

		// Token: 0x06002DDF RID: 11743 RVA: 0x0011AA30 File Offset: 0x00119A30
		private long ReadUnsignedInt(Stream stream)
		{
			int num = this.ReadUnsignedByte(stream);
			int num2 = this.ReadUnsignedByte(stream);
			int num3 = this.ReadUnsignedByte(stream);
			int num4 = this.ReadUnsignedByte(stream);
			long num5 = (long)(num4 << 24 | num3 << 16 | num2 << 8 | num);
			return num5 & (long)((ulong)-1);
		}

		// Token: 0x06002DE0 RID: 11744 RVA: 0x0011AA74 File Offset: 0x00119A74
		private int ReadInt(Stream stream)
		{
			int num = this.ReadUnsignedByte(stream);
			int num2 = this.ReadUnsignedByte(stream);
			int num3 = this.ReadUnsignedByte(stream);
			int num4 = this.ReadUnsignedByte(stream);
			return num4 << 24 | num3 << 16 | num2 << 8 | num;
		}

		// Token: 0x06002DE1 RID: 11745 RVA: 0x0011AAB0 File Offset: 0x00119AB0
		private long ReadDWord(Stream stream)
		{
			return this.ReadUnsignedInt(stream);
		}

		// Token: 0x06002DE2 RID: 11746 RVA: 0x0011AAB9 File Offset: 0x00119AB9
		private int ReadLong(Stream stream)
		{
			return this.ReadInt(stream);
		}

		// Token: 0x04001F95 RID: 8085
		private const int VERSION_2_1_BIT = 0;

		// Token: 0x04001F96 RID: 8086
		private const int VERSION_2_4_BIT = 1;

		// Token: 0x04001F97 RID: 8087
		private const int VERSION_2_8_BIT = 2;

		// Token: 0x04001F98 RID: 8088
		private const int VERSION_2_24_BIT = 3;

		// Token: 0x04001F99 RID: 8089
		private const int VERSION_3_1_BIT = 4;

		// Token: 0x04001F9A RID: 8090
		private const int VERSION_3_4_BIT = 5;

		// Token: 0x04001F9B RID: 8091
		private const int VERSION_3_8_BIT = 6;

		// Token: 0x04001F9C RID: 8092
		private const int VERSION_3_24_BIT = 7;

		// Token: 0x04001F9D RID: 8093
		private const int VERSION_3_NT_16_BIT = 8;

		// Token: 0x04001F9E RID: 8094
		private const int VERSION_3_NT_32_BIT = 9;

		// Token: 0x04001F9F RID: 8095
		private const int VERSION_4_1_BIT = 10;

		// Token: 0x04001FA0 RID: 8096
		private const int VERSION_4_4_BIT = 11;

		// Token: 0x04001FA1 RID: 8097
		private const int VERSION_4_8_BIT = 12;

		// Token: 0x04001FA2 RID: 8098
		private const int VERSION_4_16_BIT = 13;

		// Token: 0x04001FA3 RID: 8099
		private const int VERSION_4_24_BIT = 14;

		// Token: 0x04001FA4 RID: 8100
		private const int VERSION_4_32_BIT = 15;

		// Token: 0x04001FA5 RID: 8101
		private const int LCS_CALIBRATED_RGB = 0;

		// Token: 0x04001FA6 RID: 8102
		private const int LCS_sRGB = 1;

		// Token: 0x04001FA7 RID: 8103
		private const int LCS_CMYK = 2;

		// Token: 0x04001FA8 RID: 8104
		private const int BI_RGB = 0;

		// Token: 0x04001FA9 RID: 8105
		private const int BI_RLE8 = 1;

		// Token: 0x04001FAA RID: 8106
		private const int BI_RLE4 = 2;

		// Token: 0x04001FAB RID: 8107
		private const int BI_BITFIELDS = 3;

		// Token: 0x04001FAC RID: 8108
		private Stream inputStream;

		// Token: 0x04001FAD RID: 8109
		private long bitmapFileSize;

		// Token: 0x04001FAE RID: 8110
		private long bitmapOffset;

		// Token: 0x04001FAF RID: 8111
		private long compression;

		// Token: 0x04001FB0 RID: 8112
		private long imageSize;

		// Token: 0x04001FB1 RID: 8113
		private byte[] palette;

		// Token: 0x04001FB2 RID: 8114
		private int imageType;

		// Token: 0x04001FB3 RID: 8115
		private int numBands;

		// Token: 0x04001FB4 RID: 8116
		private bool isBottomUp;

		// Token: 0x04001FB5 RID: 8117
		private int bitsPerPixel;

		// Token: 0x04001FB6 RID: 8118
		private int redMask;

		// Token: 0x04001FB7 RID: 8119
		private int greenMask;

		// Token: 0x04001FB8 RID: 8120
		private int blueMask;

		// Token: 0x04001FB9 RID: 8121
		private int alphaMask;

		// Token: 0x04001FBA RID: 8122
		public Dictionary<string, object> properties = new Dictionary<string, object>();

		// Token: 0x04001FBB RID: 8123
		private long xPelsPerMeter;

		// Token: 0x04001FBC RID: 8124
		private long yPelsPerMeter;

		// Token: 0x04001FBD RID: 8125
		private int width;

		// Token: 0x04001FBE RID: 8126
		private int height;
	}
}
