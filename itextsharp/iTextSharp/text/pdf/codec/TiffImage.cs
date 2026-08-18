using System;
using System.IO;
using System.util.zlib;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf.codec
{
	// Token: 0x020005F6 RID: 1526
	public class TiffImage
	{
		// Token: 0x060033E5 RID: 13285 RVA: 0x00140E81 File Offset: 0x0013FE81
		public static int GetNumberOfPages(RandomAccessFileOrArray s)
		{
			return TIFFDirectory.GetNumDirectories(s);
		}

		// Token: 0x060033E6 RID: 13286 RVA: 0x00140E8C File Offset: 0x0013FE8C
		private static int GetDpi(TIFFField fd, int resolutionUnit)
		{
			if (fd == null)
			{
				return 0;
			}
			long[] asRational = fd.GetAsRational(0);
			float num = (float)asRational[0] / (float)asRational[1];
			int result = 0;
			switch (resolutionUnit)
			{
			case 1:
			case 2:
				result = (int)((double)num + 0.5);
				break;
			case 3:
				result = (int)((double)num * 2.54 + 0.5);
				break;
			}
			return result;
		}

		// Token: 0x060033E7 RID: 13287 RVA: 0x00140EF3 File Offset: 0x0013FEF3
		public static Image GetTiffImage(RandomAccessFileOrArray s, int page)
		{
			return TiffImage.GetTiffImage(s, page, false);
		}

		// Token: 0x060033E8 RID: 13288 RVA: 0x00140F00 File Offset: 0x0013FF00
		public static Image GetTiffImage(RandomAccessFileOrArray s, int page, bool direct)
		{
			if (page < 1)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("the.page.number.must.be.gt.eq.1"));
			}
			TIFFDirectory tiffdirectory = new TIFFDirectory(s, page - 1);
			if (tiffdirectory.IsTagPresent(322))
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("tiles.are.not.supported"));
			}
			int num = (int)tiffdirectory.GetFieldAsLong(259);
			int num2 = num;
			switch (num2)
			{
			case 2:
			case 3:
			case 4:
				break;
			default:
				if (num2 != 32771)
				{
					return TiffImage.GetTiffImageColor(tiffdirectory, s);
				}
				break;
			}
			float num3 = 0f;
			if (tiffdirectory.IsTagPresent(274))
			{
				int num4 = (int)tiffdirectory.GetFieldAsLong(274);
				if (num4 == 3 || num4 == 4)
				{
					num3 = 3.1415927f;
				}
				else if (num4 == 5 || num4 == 8)
				{
					num3 = 1.5707964f;
				}
				else if (num4 == 6 || num4 == 7)
				{
					num3 = -1.5707964f;
				}
			}
			Image image = null;
			long num5 = 0L;
			long tiffT6Options = 0L;
			int fillOrder = 1;
			int num6 = (int)tiffdirectory.GetFieldAsLong(257);
			int num7 = (int)tiffdirectory.GetFieldAsLong(256);
			int num8 = 0;
			int num9 = 0;
			float xyratio = 0f;
			int num10 = 2;
			if (tiffdirectory.IsTagPresent(296))
			{
				num10 = (int)tiffdirectory.GetFieldAsLong(296);
			}
			num8 = TiffImage.GetDpi(tiffdirectory.GetField(282), num10);
			num9 = TiffImage.GetDpi(tiffdirectory.GetField(283), num10);
			if (num10 == 1)
			{
				if (num9 != 0)
				{
					xyratio = (float)num8 / (float)num9;
				}
				num8 = 0;
				num9 = 0;
			}
			int num11 = num6;
			if (tiffdirectory.IsTagPresent(278))
			{
				num11 = (int)tiffdirectory.GetFieldAsLong(278);
			}
			if (num11 <= 0 || num11 > num6)
			{
				num11 = num6;
			}
			long[] arrayLongShort = TiffImage.GetArrayLongShort(tiffdirectory, 273);
			long[] array = TiffImage.GetArrayLongShort(tiffdirectory, 279);
			if ((array == null || (array.Length == 1 && (array[0] == 0L || array[0] + arrayLongShort[0] > (long)s.Length))) && num6 == num11)
			{
				array = new long[]
				{
					(long)(s.Length - (int)arrayLongShort[0])
				};
			}
			TIFFField field = tiffdirectory.GetField(266);
			if (field != null)
			{
				fillOrder = field.GetAsInt(0);
			}
			int num12 = 0;
			if (tiffdirectory.IsTagPresent(262))
			{
				long fieldAsLong = tiffdirectory.GetFieldAsLong(262);
				if (fieldAsLong == 1L)
				{
					num12 |= 1;
				}
			}
			int typeCCITT = 0;
			int num13 = num;
			switch (num13)
			{
			case 2:
				break;
			case 3:
			{
				typeCCITT = 257;
				num12 |= 12;
				TIFFField field2 = tiffdirectory.GetField(292);
				if (field2 == null)
				{
					goto IL_2C8;
				}
				num5 = field2.GetAsLong(0);
				if ((num5 & 1L) != 0L)
				{
					typeCCITT = 258;
				}
				if ((num5 & 4L) != 0L)
				{
					num12 |= 2;
					goto IL_2C8;
				}
				goto IL_2C8;
			}
			case 4:
			{
				typeCCITT = 256;
				TIFFField field3 = tiffdirectory.GetField(293);
				if (field3 != null)
				{
					tiffT6Options = field3.GetAsLong(0);
					goto IL_2C8;
				}
				goto IL_2C8;
			}
			default:
				if (num13 != 32771)
				{
					goto IL_2C8;
				}
				break;
			}
			typeCCITT = 257;
			num12 |= 10;
			IL_2C8:
			if (direct && num11 == num6)
			{
				byte[] array2 = new byte[(int)array[0]];
				s.Seek(arrayLongShort[0]);
				s.ReadFully(array2);
				image = Image.GetInstance(num7, num6, false, typeCCITT, num12, array2);
				image.Inverted = true;
			}
			else
			{
				int num14 = num6;
				CCITTG4Encoder ccittg4Encoder = new CCITTG4Encoder(num7);
				int i = 0;
				while (i < arrayLongShort.Length)
				{
					byte[] array3 = new byte[(int)array[i]];
					s.Seek(arrayLongShort[i]);
					s.ReadFully(array3);
					int num15 = Math.Min(num11, num14);
					TIFFFaxDecoder tifffaxDecoder = new TIFFFaxDecoder(fillOrder, num7, num15);
					byte[] array4 = new byte[(num7 + 7) / 8 * num15];
					int num16 = num;
					switch (num16)
					{
					case 2:
						goto IL_38C;
					case 3:
						try
						{
							tifffaxDecoder.Decode2D(array4, array3, 0, num15, num5);
						}
						catch (Exception ex)
						{
							num5 ^= 4L;
							try
							{
								tifffaxDecoder.Decode2D(array4, array3, 0, num15, num5);
							}
							catch
							{
								throw ex;
							}
						}
						ccittg4Encoder.Fax4Encode(array4, num15);
						break;
					case 4:
						tifffaxDecoder.DecodeT6(array4, array3, 0, num15, tiffT6Options);
						ccittg4Encoder.Fax4Encode(array4, num15);
						break;
					default:
						if (num16 == 32771)
						{
							goto IL_38C;
						}
						break;
					}
					IL_402:
					num14 -= num11;
					i++;
					continue;
					IL_38C:
					tifffaxDecoder.Decode1D(array4, array3, 0, num15);
					ccittg4Encoder.Fax4Encode(array4, num15);
					goto IL_402;
				}
				byte[] data = ccittg4Encoder.Close();
				image = Image.GetInstance(num7, num6, false, 256, num12 & 1, data);
			}
			image.SetDpi(num8, num9);
			image.XYRatio = xyratio;
			if (tiffdirectory.IsTagPresent(34675))
			{
				try
				{
					TIFFField field4 = tiffdirectory.GetField(34675);
					ICC_Profile instance = ICC_Profile.GetInstance(field4.GetAsBytes());
					if (instance.NumComponents == 1)
					{
						image.TagICC = instance;
					}
				}
				catch
				{
				}
			}
			image.OriginalType = 5;
			if (num3 != 0f)
			{
				image.InitialRotation = num3;
			}
			return image;
		}

		// Token: 0x060033E9 RID: 13289 RVA: 0x001413E0 File Offset: 0x001403E0
		protected static Image GetTiffImageColor(TIFFDirectory dir, RandomAccessFileOrArray s)
		{
			int num = 1;
			TIFFLZWDecoder tifflzwdecoder = null;
			int num2 = (int)dir.GetFieldAsLong(259);
			int num3 = num2;
			switch (num3)
			{
			case 1:
			case 5:
			case 6:
			case 7:
			case 8:
				goto IL_65;
			case 2:
			case 3:
			case 4:
				break;
			default:
				if (num3 == 32773 || num3 == 32946)
				{
					goto IL_65;
				}
				break;
			}
			throw new ArgumentException(MessageLocalization.GetComposedMessage("the.compression.1.is.not.supported", num2));
			IL_65:
			int num4 = (int)dir.GetFieldAsLong(262);
			switch (num4)
			{
			case 0:
			case 1:
			case 2:
			case 3:
			case 5:
				break;
			default:
				if (num2 != 6 && num2 != 7)
				{
					throw new ArgumentException(MessageLocalization.GetComposedMessage("the.photometric.1.is.not.supported", num4));
				}
				break;
			}
			float num5 = 0f;
			if (dir.IsTagPresent(274))
			{
				int num6 = (int)dir.GetFieldAsLong(274);
				if (num6 == 3 || num6 == 4)
				{
					num5 = 3.1415927f;
				}
				else if (num6 == 5 || num6 == 8)
				{
					num5 = 1.5707964f;
				}
				else if (num6 == 6 || num6 == 7)
				{
					num5 = -1.5707964f;
				}
			}
			if (dir.IsTagPresent(284) && dir.GetFieldAsLong(284) == 2L)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("planar.images.are.not.supported"));
			}
			if (dir.IsTagPresent(338))
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("extra.samples.are.not.supported"));
			}
			int num7 = 1;
			if (dir.IsTagPresent(277))
			{
				num7 = (int)dir.GetFieldAsLong(277);
			}
			int num8 = 1;
			if (dir.IsTagPresent(258))
			{
				num8 = (int)dir.GetFieldAsLong(258);
			}
			int num9 = num8;
			switch (num9)
			{
			case 1:
			case 2:
			case 4:
				goto IL_1C9;
			case 3:
				break;
			default:
				if (num9 == 8)
				{
					goto IL_1C9;
				}
				break;
			}
			throw new ArgumentException(MessageLocalization.GetComposedMessage("bits.per.sample.1.is.not.supported", num8));
			IL_1C9:
			Image image = null;
			int num10 = (int)dir.GetFieldAsLong(257);
			int num11 = (int)dir.GetFieldAsLong(256);
			int resolutionUnit = 2;
			if (dir.IsTagPresent(296))
			{
				resolutionUnit = (int)dir.GetFieldAsLong(296);
			}
			int dpi = TiffImage.GetDpi(dir.GetField(282), resolutionUnit);
			int dpi2 = TiffImage.GetDpi(dir.GetField(283), resolutionUnit);
			int num12 = 1;
			TIFFField field = dir.GetField(266);
			if (field != null)
			{
				num12 = field.GetAsInt(0);
			}
			bool flag = num12 == 2;
			int num13 = num10;
			if (dir.IsTagPresent(278))
			{
				num13 = (int)dir.GetFieldAsLong(278);
			}
			if (num13 <= 0 || num13 > num10)
			{
				num13 = num10;
			}
			long[] arrayLongShort = TiffImage.GetArrayLongShort(dir, 273);
			long[] array = TiffImage.GetArrayLongShort(dir, 279);
			if ((array == null || (array.Length == 1 && (array[0] == 0L || array[0] + arrayLongShort[0] > (long)s.Length))) && num10 == num13)
			{
				array = new long[]
				{
					(long)(s.Length - (int)arrayLongShort[0])
				};
			}
			if (num2 == 5)
			{
				TIFFField field2 = dir.GetField(317);
				if (field2 != null)
				{
					num = field2.GetAsInt(0);
					if (num != 1 && num != 2)
					{
						throw new Exception(MessageLocalization.GetComposedMessage("illegal.value.for.predictor.in.tiff.file"));
					}
					if (num == 2 && num8 != 8)
					{
						throw new Exception(MessageLocalization.GetComposedMessage("1.bit.samples.are.not.supported.for.horizontal.differencing.predictor", num8));
					}
				}
				tifflzwdecoder = new TIFFLZWDecoder(num11, num, num7);
			}
			int num14 = num10;
			MemoryStream memoryStream = null;
			ZDeflaterOutputStream zdeflaterOutputStream = null;
			CCITTG4Encoder ccittg4Encoder = null;
			if (num8 == 1 && num7 == 1)
			{
				ccittg4Encoder = new CCITTG4Encoder(num11);
			}
			else
			{
				memoryStream = new MemoryStream();
				if (num2 != 6 && num2 != 7)
				{
					zdeflaterOutputStream = new ZDeflaterOutputStream(memoryStream);
				}
			}
			if (num2 == 6)
			{
				if (!dir.IsTagPresent(513))
				{
					throw new IOException(MessageLocalization.GetComposedMessage("missing.tag.s.for.ojpeg.compression"));
				}
				int num15 = (int)dir.GetFieldAsLong(513);
				int val = s.Length - num15;
				if (dir.IsTagPresent(514))
				{
					val = (int)dir.GetFieldAsLong(514) + (int)array[0];
				}
				byte[] array2 = new byte[Math.Min(val, s.Length - num15)];
				int num16 = s.FilePointer;
				num16 += num15;
				s.Seek(num16);
				s.ReadFully(array2);
				image = new Jpeg(array2);
			}
			else if (num2 == 7)
			{
				if (array.Length > 1)
				{
					throw new IOException(MessageLocalization.GetComposedMessage("compression.jpeg.is.only.supported.with.a.single.strip.this.image.has.1.strips", array.Length));
				}
				byte[] array3 = new byte[(int)array[0]];
				s.Seek(arrayLongShort[0]);
				s.ReadFully(array3);
				image = new Jpeg(array3);
			}
			else
			{
				for (int i = 0; i < arrayLongShort.Length; i++)
				{
					byte[] array4 = new byte[(int)array[i]];
					s.Seek(arrayLongShort[i]);
					s.ReadFully(array4);
					int num17 = Math.Min(num13, num14);
					byte[] array5 = null;
					if (num2 != 1)
					{
						array5 = new byte[(num11 * num8 * num7 + 7) / 8 * num17];
					}
					if (flag)
					{
						TIFFFaxDecoder.ReverseBits(array4);
					}
					int num18 = num2;
					if (num18 <= 5)
					{
						if (num18 != 1)
						{
							if (num18 == 5)
							{
								tifflzwdecoder.Decode(array4, array5, num17);
							}
						}
						else
						{
							array5 = array4;
						}
					}
					else
					{
						if (num18 != 8)
						{
							if (num18 == 32773)
							{
								TiffImage.DecodePackbits(array4, array5);
								goto IL_528;
							}
							if (num18 != 32946)
							{
								goto IL_528;
							}
						}
						TiffImage.Inflate(array4, array5);
					}
					IL_528:
					if (num8 == 1 && num7 == 1)
					{
						ccittg4Encoder.Fax4Encode(array5, num17);
					}
					else
					{
						zdeflaterOutputStream.Write(array5, 0, array5.Length);
					}
					num14 -= num13;
				}
				if (num8 == 1 && num7 == 1)
				{
					image = Image.GetInstance(num11, num10, false, 256, (num4 == 1) ? 1 : 0, ccittg4Encoder.Close());
				}
				else
				{
					zdeflaterOutputStream.Close();
					image = Image.GetInstance(num11, num10, num7, num8, memoryStream.ToArray());
					image.Deflated = true;
				}
			}
			image.SetDpi(dpi, dpi2);
			if (num2 != 6 && num2 != 7)
			{
				if (dir.IsTagPresent(34675))
				{
					try
					{
						TIFFField field3 = dir.GetField(34675);
						ICC_Profile instance = ICC_Profile.GetInstance(field3.GetAsBytes());
						if (num7 == instance.NumComponents)
						{
							image.TagICC = instance;
						}
					}
					catch
					{
					}
				}
				if (dir.IsTagPresent(320))
				{
					TIFFField field4 = dir.GetField(320);
					char[] asChars = field4.GetAsChars();
					byte[] array6 = new byte[asChars.Length];
					int num19 = asChars.Length / 3;
					int num20 = num19 * 2;
					for (int j = 0; j < num19; j++)
					{
						array6[j * 3] = (byte)(asChars[j] >> 8);
						array6[j * 3 + 1] = (byte)(asChars[j + num19] >> 8);
						array6[j * 3 + 2] = (byte)(asChars[j + num20] >> 8);
					}
					PdfArray pdfArray = new PdfArray();
					pdfArray.Add(PdfName.INDEXED);
					pdfArray.Add(PdfName.DEVICERGB);
					pdfArray.Add(new PdfNumber(num19 - 1));
					pdfArray.Add(new PdfString(array6));
					PdfDictionary pdfDictionary = new PdfDictionary();
					pdfDictionary.Put(PdfName.COLORSPACE, pdfArray);
					image.Additional = pdfDictionary;
				}
				image.OriginalType = 5;
			}
			if (num4 == 0)
			{
				image.Inverted = true;
			}
			if (num5 != 0f)
			{
				image.InitialRotation = num5;
			}
			return image;
		}

		// Token: 0x060033EA RID: 13290 RVA: 0x00141B1C File Offset: 0x00140B1C
		private static long[] GetArrayLongShort(TIFFDirectory dir, int tag)
		{
			TIFFField field = dir.GetField(tag);
			if (field == null)
			{
				return null;
			}
			long[] array;
			if (field.GetType() == 4)
			{
				array = field.GetAsLongs();
			}
			else
			{
				char[] asChars = field.GetAsChars();
				array = new long[asChars.Length];
				for (int i = 0; i < asChars.Length; i++)
				{
					array[i] = (long)((ulong)asChars[i]);
				}
			}
			return array;
		}

		// Token: 0x060033EB RID: 13291 RVA: 0x00141B70 File Offset: 0x00140B70
		public static void DecodePackbits(byte[] data, byte[] dst)
		{
			int num = 0;
			int i = 0;
			try
			{
				while (i < dst.Length)
				{
					sbyte b = (sbyte)data[num++];
					if (b >= 0 && b <= 127)
					{
						for (int j = 0; j < (int)(b + 1); j++)
						{
							dst[i++] = data[num++];
						}
					}
					else if (b <= -1 && b >= -127)
					{
						sbyte b2 = (sbyte)data[num++];
						for (int k = 0; k < (int)(-b + 1); k++)
						{
							dst[i++] = (byte)b2;
						}
					}
					else
					{
						num++;
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x060033EC RID: 13292 RVA: 0x00141C08 File Offset: 0x00140C08
		public static void Inflate(byte[] deflated, byte[] inflated)
		{
			byte[] array = PdfReader.FlateDecode(deflated);
			Array.Copy(array, 0, inflated, 0, Math.Min(array.Length, inflated.Length));
		}
	}
}
