using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Reflection;
using iTextSharp.text.error_messages;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.codec;

namespace iTextSharp.text
{
	// Token: 0x020000F2 RID: 242
	public abstract class Image : Rectangle
	{
		// Token: 0x0600090D RID: 2317 RVA: 0x0003096C File Offset: 0x0002F96C
		public Image(Uri url) : base(0f, 0f)
		{
			this.url = url;
			this.alignment = 0;
			this.rotationRadians = 0f;
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x000309F0 File Offset: 0x0002F9F0
		public Image(Image image) : base(image)
		{
			this.type = image.type;
			this.url = image.url;
			this.alignment = image.alignment;
			this.alt = image.alt;
			this.absoluteX = image.absoluteX;
			this.absoluteY = image.absoluteY;
			this.plainWidth = image.plainWidth;
			this.plainHeight = image.plainHeight;
			this.scaledWidth = image.scaledWidth;
			this.scaledHeight = image.scaledHeight;
			this.rotationRadians = image.rotationRadians;
			this.indentationLeft = image.indentationLeft;
			this.indentationRight = image.indentationRight;
			this.colorspace = image.colorspace;
			this.rawData = image.rawData;
			this.template = image.template;
			this.bpc = image.bpc;
			this.transparency = image.transparency;
			this.mySerialId = image.mySerialId;
			this.invert = image.invert;
			this.dpiX = image.dpiX;
			this.dpiY = image.dpiY;
			this.mask = image.mask;
			this.imageMask = image.imageMask;
			this.interpolation = image.interpolation;
			this.annotation = image.annotation;
			this.profile = image.profile;
			this.deflated = image.deflated;
			this.additional = image.additional;
			this.smask = image.smask;
			this.XYRatio = image.XYRatio;
			this.originalData = image.originalData;
			this.originalType = image.originalType;
			this.spacingAfter = image.spacingAfter;
			this.spacingBefore = image.spacingBefore;
			this.widthPercentage = image.widthPercentage;
			this.layer = image.layer;
			this.initialRotation = image.initialRotation;
			this.directReference = image.directReference;
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00030C28 File Offset: 0x0002FC28
		public static Image GetInstance(Image image)
		{
			if (image == null)
			{
				return null;
			}
			return (Image)image.GetType().GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, new Type[]
			{
				typeof(Image)
			}, null).Invoke(new object[]
			{
				image
			});
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x00030C74 File Offset: 0x0002FC74
		public static Image GetInstance(Uri url)
		{
			Stream stream = null;
			Image result;
			try
			{
				WebRequest webRequest = WebRequest.Create(url);
				stream = webRequest.GetResponse().GetResponseStream();
				int num = stream.ReadByte();
				int num2 = stream.ReadByte();
				int num3 = stream.ReadByte();
				int num4 = stream.ReadByte();
				int num5 = stream.ReadByte();
				int num6 = stream.ReadByte();
				int num7 = stream.ReadByte();
				int num8 = stream.ReadByte();
				stream.Close();
				stream = null;
				if (num == 71 && num2 == 73 && num3 == 70)
				{
					GifImage gifImage = new GifImage(url);
					Image image = gifImage.GetImage(1);
					result = image;
				}
				else if (num == 255 && num2 == 216)
				{
					result = new Jpeg(url);
				}
				else if (num == 0 && num2 == 0 && num3 == 0 && num4 == 12)
				{
					result = new Jpeg2000(url);
				}
				else if (num == 255 && num2 == 79 && num3 == 255 && num4 == 81)
				{
					result = new Jpeg2000(url);
				}
				else if (num == PngImage.PNGID[0] && num2 == PngImage.PNGID[1] && num3 == PngImage.PNGID[2] && num4 == PngImage.PNGID[3])
				{
					Image image2 = PngImage.GetImage(url);
					result = image2;
				}
				else if (num == 215 && num2 == 205)
				{
					Image image3 = new ImgWMF(url);
					result = image3;
				}
				else
				{
					if (num != 66 || num2 != 77)
					{
						if ((num == 77 && num2 == 77 && num3 == 0 && num4 == 42) || (num == 73 && num2 == 73 && num3 == 42 && num4 == 0))
						{
							RandomAccessFileOrArray randomAccessFileOrArray = null;
							try
							{
								if (url.IsFile)
								{
									string localPath = url.LocalPath;
									randomAccessFileOrArray = new RandomAccessFileOrArray(localPath);
								}
								else
								{
									randomAccessFileOrArray = new RandomAccessFileOrArray(url);
								}
								Image tiffImage = TiffImage.GetTiffImage(randomAccessFileOrArray, 1);
								tiffImage.url = url;
								return tiffImage;
							}
							finally
							{
								if (randomAccessFileOrArray != null)
								{
									randomAccessFileOrArray.Close();
								}
							}
						}
						if (num == 151 && num2 == 74 && num3 == 66 && num4 == 50 && num5 == 13 && num6 == 10 && num7 == 26 && num8 == 10)
						{
							RandomAccessFileOrArray randomAccessFileOrArray2 = null;
							try
							{
								if (url.IsFile)
								{
									string localPath2 = url.LocalPath;
									randomAccessFileOrArray2 = new RandomAccessFileOrArray(localPath2);
								}
								else
								{
									randomAccessFileOrArray2 = new RandomAccessFileOrArray(url);
								}
								Image jbig2Image = JBIG2Image.GetJbig2Image(randomAccessFileOrArray2, 1);
								jbig2Image.url = url;
								return jbig2Image;
							}
							finally
							{
								if (randomAccessFileOrArray2 != null)
								{
									randomAccessFileOrArray2.Close();
								}
							}
						}
						throw new IOException(url.ToString() + " is not a recognized imageformat.");
					}
					Image image4 = BmpImage.GetImage(url);
					result = image4;
				}
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

		// Token: 0x06000911 RID: 2321 RVA: 0x00030F40 File Offset: 0x0002FF40
		public static Image GetInstance(Stream s)
		{
			byte[] imgb = RandomAccessFileOrArray.InputStreamToArray(s);
			s.Close();
			return Image.GetInstance(imgb);
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00030F60 File Offset: 0x0002FF60
		public static Image GetInstance(int width, int height, byte[] data, byte[] globals)
		{
			return new ImgJBIG2(width, height, data, globals);
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00030F78 File Offset: 0x0002FF78
		public static Image GetInstance(byte[] imgb)
		{
			int num = (int)imgb[0];
			int num2 = (int)imgb[1];
			int num3 = (int)imgb[2];
			int num4 = (int)imgb[3];
			if (num == 71 && num2 == 73 && num3 == 70)
			{
				GifImage gifImage = new GifImage(imgb);
				return gifImage.GetImage(1);
			}
			if (num == 255 && num2 == 216)
			{
				return new Jpeg(imgb);
			}
			if (num == 0 && num2 == 0 && num3 == 0 && num4 == 12)
			{
				return new Jpeg2000(imgb);
			}
			if (num == 255 && num2 == 79 && num3 == 255 && num4 == 81)
			{
				return new Jpeg2000(imgb);
			}
			if (num == PngImage.PNGID[0] && num2 == PngImage.PNGID[1] && num3 == PngImage.PNGID[2] && num4 == PngImage.PNGID[3])
			{
				return PngImage.GetImage(imgb);
			}
			if (num == 215 && num2 == 205)
			{
				return new ImgWMF(imgb);
			}
			if (num == 66 && num2 == 77)
			{
				return BmpImage.GetImage(imgb);
			}
			if ((num == 77 && num2 == 77 && num3 == 0 && num4 == 42) || (num == 73 && num2 == 73 && num3 == 42 && num4 == 0))
			{
				RandomAccessFileOrArray randomAccessFileOrArray = null;
				try
				{
					randomAccessFileOrArray = new RandomAccessFileOrArray(imgb);
					Image tiffImage = TiffImage.GetTiffImage(randomAccessFileOrArray, 1);
					if (tiffImage.OriginalData == null)
					{
						tiffImage.OriginalData = imgb;
					}
					return tiffImage;
				}
				finally
				{
					if (randomAccessFileOrArray != null)
					{
						randomAccessFileOrArray.Close();
					}
				}
			}
			throw new IOException(MessageLocalization.GetComposedMessage("the.byte.array.is.not.a.recognized.imageformat"));
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x000310D8 File Offset: 0x000300D8
		public static Image GetInstance(Image image, ImageFormat format)
		{
			MemoryStream memoryStream = new MemoryStream();
			image.Save(memoryStream, format);
			return Image.GetInstance(memoryStream.ToArray());
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00031100 File Offset: 0x00030100
		public static Image GetInstance(Image image, BaseColor color, bool forceBW)
		{
			Bitmap bitmap = (Bitmap)image;
			int width = bitmap.Width;
			int height = bitmap.Height;
			if (forceBW)
			{
				int num = width / 8 + (((width & 7) != 0) ? 1 : 0);
				byte[] array = new byte[num * height];
				int num2 = 0;
				int num3 = 1;
				if (color != null)
				{
					num3 = ((color.R + color.G + color.B < 384) ? 0 : 1);
				}
				int[] array2 = null;
				int num4 = 128;
				int num5 = 0;
				int num6 = 0;
				if (color != null)
				{
					for (int i = 0; i < height; i++)
					{
						for (int j = 0; j < width; j++)
						{
							int a = (int)bitmap.GetPixel(j, i).A;
							if (a < 250)
							{
								if (num3 == 1)
								{
									num6 |= num4;
								}
							}
							else if ((bitmap.GetPixel(j, i).ToArgb() & 2184) != 0)
							{
								num6 |= num4;
							}
							num4 >>= 1;
							if (num4 == 0 || num5 + 1 >= width)
							{
								array[num2++] = (byte)num6;
								num4 = 128;
								num6 = 0;
							}
							num5++;
							if (num5 >= width)
							{
								num5 = 0;
							}
						}
					}
				}
				else
				{
					for (int k = 0; k < height; k++)
					{
						for (int l = 0; l < width; l++)
						{
							if (array2 == null && bitmap.GetPixel(l, k).A == 0)
							{
								array2 = new int[2];
								array2[0] = (array2[1] = (((bitmap.GetPixel(l, k).ToArgb() & 2184) != 0) ? 1 : 0));
							}
							if ((bitmap.GetPixel(l, k).ToArgb() & 2184) != 0)
							{
								num6 |= num4;
							}
							num4 >>= 1;
							if (num4 == 0 || num5 + 1 >= width)
							{
								array[num2++] = (byte)num6;
								num4 = 128;
								num6 = 0;
							}
							num5++;
							if (num5 >= width)
							{
								num5 = 0;
							}
						}
					}
				}
				return Image.GetInstance(width, height, 1, 1, array, array2);
			}
			byte[] array3 = new byte[width * height * 3];
			byte[] array4 = null;
			int num7 = 0;
			int num8 = 255;
			int num9 = 255;
			int num10 = 255;
			if (color != null)
			{
				num8 = color.R;
				num9 = color.G;
				num10 = color.B;
			}
			int[] array5 = null;
			if (color != null)
			{
				for (int m = 0; m < height; m++)
				{
					for (int n = 0; n < width; n++)
					{
						int num11 = bitmap.GetPixel(n, m).ToArgb() >> 24 & 255;
						if (num11 < 250)
						{
							array3[num7++] = (byte)num8;
							array3[num7++] = (byte)num9;
							array3[num7++] = (byte)num10;
						}
						else
						{
							int num12 = bitmap.GetPixel(n, m).ToArgb();
							array3[num7++] = (byte)(num12 >> 16 & 255);
							array3[num7++] = (byte)(num12 >> 8 & 255);
							array3[num7++] = (byte)(num12 & 255);
						}
					}
				}
			}
			else
			{
				int num13 = 0;
				array4 = new byte[width * height];
				bool flag = false;
				int num14 = 0;
				for (int num15 = 0; num15 < height; num15++)
				{
					for (int num16 = 0; num16 < width; num16++)
					{
						int num12 = bitmap.GetPixel(num16, num15).ToArgb();
						byte b = array4[num14++] = (byte)(num12 >> 24 & 255);
						if (!flag)
						{
							if (b != 0 && b != 255)
							{
								flag = true;
							}
							else if (array5 == null)
							{
								if (b == 0)
								{
									num13 = (num12 & 16777215);
									array5 = new int[6];
									array5[0] = (array5[1] = (num13 >> 16 & 255));
									array5[2] = (array5[3] = (num13 >> 8 & 255));
									array5[4] = (array5[5] = (num13 & 255));
								}
							}
							else if ((num12 & 16777215) != num13)
							{
								flag = true;
							}
						}
						array3[num7++] = (byte)(num12 >> 16 & 255);
						array3[num7++] = (byte)(num12 >> 8 & 255);
						array3[num7++] = (byte)(num12 & 255);
					}
				}
				if (flag)
				{
					array5 = null;
				}
				else
				{
					array4 = null;
				}
			}
			Image instance = Image.GetInstance(width, height, 3, 8, array3, array5);
			if (array4 != null)
			{
				Image instance2 = Image.GetInstance(width, height, 1, 8, array4);
				instance2.MakeMask();
				instance.ImageMask = instance2;
			}
			return instance;
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x000315A2 File Offset: 0x000305A2
		public static Image GetInstance(Image image, BaseColor color)
		{
			return Image.GetInstance(image, color, false);
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x000315AC File Offset: 0x000305AC
		public static Image GetInstance(string filename)
		{
			return Image.GetInstance(Utilities.ToURL(filename));
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x000315B9 File Offset: 0x000305B9
		public static Image GetInstance(int width, int height, int components, int bpc, byte[] data)
		{
			return Image.GetInstance(width, height, components, bpc, data, null);
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x000315C8 File Offset: 0x000305C8
		public static Image GetInstance(PRIndirectReference iref)
		{
			PdfDictionary pdfDictionary = (PdfDictionary)PdfReader.GetPdfObjectRelease(iref);
			int intValue = ((PdfNumber)PdfReader.GetPdfObjectRelease(pdfDictionary.Get(PdfName.WIDTH))).IntValue;
			int intValue2 = ((PdfNumber)PdfReader.GetPdfObjectRelease(pdfDictionary.Get(PdfName.HEIGHT))).IntValue;
			Image image = null;
			PdfObject pdfObject = pdfDictionary.Get(PdfName.SMASK);
			if (pdfObject != null && pdfObject.IsIndirect())
			{
				image = Image.GetInstance((PRIndirectReference)pdfObject);
			}
			else
			{
				pdfObject = pdfDictionary.Get(PdfName.MASK);
				if (pdfObject != null && pdfObject.IsIndirect())
				{
					PdfObject pdfObjectRelease = PdfReader.GetPdfObjectRelease(pdfObject);
					if (pdfObjectRelease is PdfDictionary)
					{
						image = Image.GetInstance((PRIndirectReference)pdfObject);
					}
				}
			}
			return new ImgRaw(intValue, intValue2, 1, 1, null)
			{
				imageMask = image,
				directReference = iref
			};
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x00031699 File Offset: 0x00030699
		public static Image GetInstance(PdfTemplate template)
		{
			return new ImgTemplate(template);
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x000316A1 File Offset: 0x000306A1
		public static Image GetInstance(int width, int height, bool reverseBits, int typeCCITT, int parameters, byte[] data)
		{
			return Image.GetInstance(width, height, reverseBits, typeCCITT, parameters, data, null);
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x000316B4 File Offset: 0x000306B4
		public static Image GetInstance(int width, int height, bool reverseBits, int typeCCITT, int parameters, byte[] data, int[] transparency)
		{
			if (transparency != null && transparency.Length != 2)
			{
				throw new BadElementException(MessageLocalization.GetComposedMessage("transparency.length.must.be.equal.to.2.with.ccitt.images"));
			}
			return new ImgCCITT(width, height, reverseBits, typeCCITT, parameters, data)
			{
				transparency = transparency
			};
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x000316F4 File Offset: 0x000306F4
		public static Image GetInstance(int width, int height, int components, int bpc, byte[] data, int[] transparency)
		{
			if (transparency != null && transparency.Length != components * 2)
			{
				throw new BadElementException(MessageLocalization.GetComposedMessage("transparency.length.must.be.equal.to.componentes.2"));
			}
			if (components == 1 && bpc == 1)
			{
				byte[] data2 = CCITTG4Encoder.Compress(data, width, height);
				return Image.GetInstance(width, height, false, 256, 1, data2, transparency);
			}
			return new ImgRaw(width, height, components, bpc, data)
			{
				transparency = transparency
			};
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00031757 File Offset: 0x00030757
		public void SetAbsolutePosition(float absoluteX, float absoluteY)
		{
			this.absoluteX = absoluteX;
			this.absoluteY = absoluteY;
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x00031768 File Offset: 0x00030768
		public void ScaleAbsolute(float newWidth, float newHeight)
		{
			this.plainWidth = newWidth;
			this.plainHeight = newHeight;
			float[] matrix = this.Matrix;
			this.scaledWidth = matrix[6] - matrix[4];
			this.scaledHeight = matrix[7] - matrix[5];
			this.WidthPercentage = 0f;
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x000317B0 File Offset: 0x000307B0
		public void ScaleAbsoluteWidth(float newWidth)
		{
			this.plainWidth = newWidth;
			float[] matrix = this.Matrix;
			this.scaledWidth = matrix[6] - matrix[4];
			this.scaledHeight = matrix[7] - matrix[5];
			this.WidthPercentage = 0f;
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x000317F0 File Offset: 0x000307F0
		public void ScaleAbsoluteHeight(float newHeight)
		{
			this.plainHeight = newHeight;
			float[] matrix = this.Matrix;
			this.scaledWidth = matrix[6] - matrix[4];
			this.scaledHeight = matrix[7] - matrix[5];
			this.WidthPercentage = 0f;
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00031830 File Offset: 0x00030830
		public void ScalePercent(float percent)
		{
			this.ScalePercent(percent, percent);
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0003183C File Offset: 0x0003083C
		public void ScalePercent(float percentX, float percentY)
		{
			this.plainWidth = this.Width * percentX / 100f;
			this.plainHeight = base.Height * percentY / 100f;
			float[] matrix = this.Matrix;
			this.scaledWidth = matrix[6] - matrix[4];
			this.scaledHeight = matrix[7] - matrix[5];
			this.WidthPercentage = 0f;
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x000318A0 File Offset: 0x000308A0
		public void ScaleToFit(float fitWidth, float fitHeight)
		{
			this.ScalePercent(100f);
			float num = fitWidth * 100f / this.ScaledWidth;
			float num2 = fitHeight * 100f / this.ScaledHeight;
			this.ScalePercent((num < num2) ? num : num2);
			this.WidthPercentage = 0f;
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x000318F0 File Offset: 0x000308F0
		public float GetImageRotation()
		{
			float num = (float)((double)(this.rotationRadians - this.initialRotation) % 6.283185307179586);
			if (num < 0f)
			{
				num += 6.2831855f;
			}
			return num;
		}

		// Token: 0x170001D6 RID: 470
		// (set) Token: 0x06000926 RID: 2342 RVA: 0x00031928 File Offset: 0x00030928
		public new float Rotation
		{
			set
			{
				double num = 3.141592653589793;
				this.rotationRadians = (float)((double)(value + this.initialRotation) % (2.0 * num));
				if (this.rotationRadians < 0f)
				{
					this.rotationRadians += (float)(2.0 * num);
				}
				float[] matrix = this.Matrix;
				this.scaledWidth = matrix[6] - matrix[4];
				this.scaledHeight = matrix[7] - matrix[5];
			}
		}

		// Token: 0x170001D7 RID: 471
		// (set) Token: 0x06000927 RID: 2343 RVA: 0x000319A2 File Offset: 0x000309A2
		public float RotationDegrees
		{
			set
			{
				this.Rotation = value / 180f * 3.1415927f;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x000319B7 File Offset: 0x000309B7
		// (set) Token: 0x06000929 RID: 2345 RVA: 0x000319BF File Offset: 0x000309BF
		public Annotation Annotation
		{
			get
			{
				return this.annotation;
			}
			set
			{
				this.annotation = value;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x000319C8 File Offset: 0x000309C8
		public int Bpc
		{
			get
			{
				return this.bpc;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x000319D0 File Offset: 0x000309D0
		public byte[] RawData
		{
			get
			{
				return this.rawData;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x000319D8 File Offset: 0x000309D8
		// (set) Token: 0x0600092D RID: 2349 RVA: 0x000319E2 File Offset: 0x000309E2
		public PdfTemplate TemplateData
		{
			get
			{
				return this.template[0];
			}
			set
			{
				this.template[0] = value;
			}
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x000319ED File Offset: 0x000309ED
		public bool HasAbsolutePosition()
		{
			return !float.IsNaN(this.absoluteY);
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x000319FD File Offset: 0x000309FD
		public bool HasAbsoluteX()
		{
			return !float.IsNaN(this.absoluteX);
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x00031A0D File Offset: 0x00030A0D
		public float AbsoluteX
		{
			get
			{
				return this.absoluteX;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x00031A15 File Offset: 0x00030A15
		public float AbsoluteY
		{
			get
			{
				return this.absoluteY;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x00031A1D File Offset: 0x00030A1D
		public override int Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x00031A25 File Offset: 0x00030A25
		public override bool IsNestable()
		{
			return true;
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x00031A28 File Offset: 0x00030A28
		public bool IsJpeg()
		{
			return this.type == 32;
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x00031A34 File Offset: 0x00030A34
		public bool IsImgRaw()
		{
			return this.type == 34;
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x00031A40 File Offset: 0x00030A40
		public bool IsImgTemplate()
		{
			return this.type == 35;
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x00031A4C File Offset: 0x00030A4C
		// (set) Token: 0x06000938 RID: 2360 RVA: 0x00031A54 File Offset: 0x00030A54
		public Uri Url
		{
			get
			{
				return this.url;
			}
			set
			{
				this.url = value;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x00031A5D File Offset: 0x00030A5D
		// (set) Token: 0x0600093A RID: 2362 RVA: 0x00031A65 File Offset: 0x00030A65
		public int Alignment
		{
			get
			{
				return this.alignment;
			}
			set
			{
				this.alignment = value;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x00031A6E File Offset: 0x00030A6E
		// (set) Token: 0x0600093C RID: 2364 RVA: 0x00031A76 File Offset: 0x00030A76
		public string Alt
		{
			get
			{
				return this.alt;
			}
			set
			{
				this.alt = value;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x00031A7F File Offset: 0x00030A7F
		public float ScaledWidth
		{
			get
			{
				return this.scaledWidth;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x00031A87 File Offset: 0x00030A87
		public float ScaledHeight
		{
			get
			{
				return this.scaledHeight;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x00031A8F File Offset: 0x00030A8F
		public int Colorspace
		{
			get
			{
				return this.colorspace;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x00031A98 File Offset: 0x00030A98
		public float[] Matrix
		{
			get
			{
				float[] array = new float[8];
				float num = (float)Math.Cos((double)this.rotationRadians);
				float num2 = (float)Math.Sin((double)this.rotationRadians);
				array[0] = this.plainWidth * num;
				array[1] = this.plainWidth * num2;
				array[2] = -this.plainHeight * num2;
				array[3] = this.plainHeight * num;
				if ((double)this.rotationRadians < 1.5707963267948966)
				{
					array[4] = array[2];
					array[5] = 0f;
					array[6] = array[0];
					array[7] = array[1] + array[3];
				}
				else if ((double)this.rotationRadians < 3.141592653589793)
				{
					array[4] = array[0] + array[2];
					array[5] = array[3];
					array[6] = 0f;
					array[7] = array[1];
				}
				else if ((double)this.rotationRadians < 4.71238898038469)
				{
					array[4] = array[0];
					array[5] = array[1] + array[3];
					array[6] = array[2];
					array[7] = 0f;
				}
				else
				{
					array[4] = 0f;
					array[5] = array[1];
					array[6] = array[0] + array[2];
					array[7] = array[3];
				}
				return array;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x00031BAD File Offset: 0x00030BAD
		// (set) Token: 0x06000942 RID: 2370 RVA: 0x00031BB5 File Offset: 0x00030BB5
		public int[] Transparency
		{
			get
			{
				return this.transparency;
			}
			set
			{
				this.transparency = value;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x00031BBE File Offset: 0x00030BBE
		public float PlainWidth
		{
			get
			{
				return this.plainWidth;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x00031BC6 File Offset: 0x00030BC6
		public float PlainHeight
		{
			get
			{
				return this.plainHeight;
			}
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x00031BD0 File Offset: 0x00030BD0
		protected static long GetSerialId()
		{
			long result;
			lock (Image.serialId)
			{
				Image.serialId = (long)Image.serialId + 1L;
				result = (long)Image.serialId;
			}
			return result;
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x00031C28 File Offset: 0x00030C28
		public long MySerialId
		{
			get
			{
				return this.mySerialId;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x00031C30 File Offset: 0x00030C30
		public int DpiX
		{
			get
			{
				return this.dpiX;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x00031C38 File Offset: 0x00030C38
		public int DpiY
		{
			get
			{
				return this.dpiY;
			}
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00031C40 File Offset: 0x00030C40
		public void SetDpi(int dpiX, int dpiY)
		{
			this.dpiX = dpiX;
			this.dpiY = dpiY;
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00031C50 File Offset: 0x00030C50
		public bool IsMaskCandidate()
		{
			return (this.type == 34 && this.bpc > 255) || this.colorspace == 1;
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00031C74 File Offset: 0x00030C74
		public void MakeMask()
		{
			if (!this.IsMaskCandidate())
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("this.image.can.not.be.an.image.mask"));
			}
			this.mask = true;
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x00031C95 File Offset: 0x00030C95
		// (set) Token: 0x0600094D RID: 2381 RVA: 0x00031CA0 File Offset: 0x00030CA0
		public Image ImageMask
		{
			get
			{
				return this.imageMask;
			}
			set
			{
				if (this.mask)
				{
					throw new DocumentException(MessageLocalization.GetComposedMessage("an.image.mask.cannot.contain.another.image.mask"));
				}
				if (!value.mask)
				{
					throw new DocumentException(MessageLocalization.GetComposedMessage("the.image.mask.is.not.a.mask.did.you.do.makemask"));
				}
				this.imageMask = value;
				this.smask = (value.bpc > 1 && value.bpc <= 8);
			}
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00031D02 File Offset: 0x00030D02
		public bool IsMask()
		{
			return this.mask;
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x00031D13 File Offset: 0x00030D13
		// (set) Token: 0x0600094F RID: 2383 RVA: 0x00031D0A File Offset: 0x00030D0A
		public bool Inverted
		{
			get
			{
				return this.invert;
			}
			set
			{
				this.invert = value;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000952 RID: 2386 RVA: 0x00031D24 File Offset: 0x00030D24
		// (set) Token: 0x06000951 RID: 2385 RVA: 0x00031D1B File Offset: 0x00030D1B
		public bool Interpolation
		{
			get
			{
				return this.interpolation;
			}
			set
			{
				this.interpolation = value;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x00031D2C File Offset: 0x00030D2C
		// (set) Token: 0x06000954 RID: 2388 RVA: 0x00031D34 File Offset: 0x00030D34
		public ICC_Profile TagICC
		{
			get
			{
				return this.profile;
			}
			set
			{
				this.profile = value;
			}
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00031D3D File Offset: 0x00030D3D
		public bool HasICCProfile()
		{
			return this.profile != null;
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x00031D4B File Offset: 0x00030D4B
		// (set) Token: 0x06000957 RID: 2391 RVA: 0x00031D53 File Offset: 0x00030D53
		public bool Deflated
		{
			get
			{
				return this.deflated;
			}
			set
			{
				this.deflated = value;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x00031D5C File Offset: 0x00030D5C
		// (set) Token: 0x06000959 RID: 2393 RVA: 0x00031D64 File Offset: 0x00030D64
		public PdfDictionary Additional
		{
			get
			{
				return this.additional;
			}
			set
			{
				this.additional = value;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x00031D6D File Offset: 0x00030D6D
		// (set) Token: 0x0600095B RID: 2395 RVA: 0x00031D75 File Offset: 0x00030D75
		public bool Smask
		{
			get
			{
				return this.smask;
			}
			set
			{
				this.smask = value;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x00031D7E File Offset: 0x00030D7E
		// (set) Token: 0x0600095D RID: 2397 RVA: 0x00031D86 File Offset: 0x00030D86
		public float XYRatio
		{
			get
			{
				return this.xyRatio;
			}
			set
			{
				this.xyRatio = value;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x00031D8F File Offset: 0x00030D8F
		// (set) Token: 0x0600095F RID: 2399 RVA: 0x00031D97 File Offset: 0x00030D97
		public float IndentationLeft
		{
			get
			{
				return this.indentationLeft;
			}
			set
			{
				this.indentationLeft = value;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x00031DA0 File Offset: 0x00030DA0
		// (set) Token: 0x06000961 RID: 2401 RVA: 0x00031DA8 File Offset: 0x00030DA8
		public float IndentationRight
		{
			get
			{
				return this.indentationRight;
			}
			set
			{
				this.indentationRight = value;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x00031DB1 File Offset: 0x00030DB1
		// (set) Token: 0x06000963 RID: 2403 RVA: 0x00031DB9 File Offset: 0x00030DB9
		public int OriginalType
		{
			get
			{
				return this.originalType;
			}
			set
			{
				this.originalType = value;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x00031DC2 File Offset: 0x00030DC2
		// (set) Token: 0x06000965 RID: 2405 RVA: 0x00031DCA File Offset: 0x00030DCA
		public byte[] OriginalData
		{
			get
			{
				return this.originalData;
			}
			set
			{
				this.originalData = value;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x00031DD3 File Offset: 0x00030DD3
		// (set) Token: 0x06000967 RID: 2407 RVA: 0x00031DDB File Offset: 0x00030DDB
		public float SpacingBefore
		{
			get
			{
				return this.spacingBefore;
			}
			set
			{
				this.spacingBefore = value;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x00031DE4 File Offset: 0x00030DE4
		// (set) Token: 0x06000969 RID: 2409 RVA: 0x00031DEC File Offset: 0x00030DEC
		public float SpacingAfter
		{
			get
			{
				return this.spacingAfter;
			}
			set
			{
				this.spacingAfter = value;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x00031DF5 File Offset: 0x00030DF5
		// (set) Token: 0x0600096B RID: 2411 RVA: 0x00031DFD File Offset: 0x00030DFD
		public float WidthPercentage
		{
			get
			{
				return this.widthPercentage;
			}
			set
			{
				this.widthPercentage = value;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x00031E06 File Offset: 0x00030E06
		// (set) Token: 0x0600096D RID: 2413 RVA: 0x00031E0E File Offset: 0x00030E0E
		public IPdfOCG Layer
		{
			get
			{
				return this.layer;
			}
			set
			{
				this.layer = value;
			}
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x00031E18 File Offset: 0x00030E18
		private PdfObject SimplifyColorspace(PdfArray obj)
		{
			if (obj == null)
			{
				return obj;
			}
			PdfObject asName = obj.GetAsName(0);
			if (PdfName.CALGRAY.Equals(asName))
			{
				return PdfName.DEVICEGRAY;
			}
			if (PdfName.CALRGB.Equals(asName))
			{
				return PdfName.DEVICERGB;
			}
			return obj;
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00031E5C File Offset: 0x00030E5C
		public void SimplifyColorspace()
		{
			if (this.additional == null)
			{
				return;
			}
			PdfArray asArray = this.additional.GetAsArray(PdfName.COLORSPACE);
			if (asArray == null)
			{
				return;
			}
			PdfObject pdfObject = this.SimplifyColorspace(asArray);
			if (!pdfObject.IsName())
			{
				PdfName asName = asArray.GetAsName(0);
				if (PdfName.INDEXED.Equals(asName) && asArray.Size >= 2)
				{
					PdfArray asArray2 = asArray.GetAsArray(1);
					if (asArray2 != null)
					{
						asArray[1] = this.SimplifyColorspace(asArray2);
					}
				}
			}
			this.additional.Put(PdfName.COLORSPACE, asArray);
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x00031EDF File Offset: 0x00030EDF
		// (set) Token: 0x06000971 RID: 2417 RVA: 0x00031EE8 File Offset: 0x00030EE8
		public float InitialRotation
		{
			get
			{
				return this.initialRotation;
			}
			set
			{
				float rotation = this.rotationRadians - this.initialRotation;
				this.initialRotation = value;
				this.Rotation = rotation;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x00031F1A File Offset: 0x00030F1A
		// (set) Token: 0x06000972 RID: 2418 RVA: 0x00031F11 File Offset: 0x00030F11
		public PdfIndirectReference DirectReference
		{
			get
			{
				return this.directReference;
			}
			set
			{
				this.directReference = value;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x00031F3C File Offset: 0x00030F3C
		// (set) Token: 0x06000974 RID: 2420 RVA: 0x00031F22 File Offset: 0x00030F22
		public int CompressionLevel
		{
			get
			{
				return this.compressionLevel;
			}
			set
			{
				if (value < 0 || value > 9)
				{
					this.compressionLevel = -1;
					return;
				}
				this.compressionLevel = value;
			}
		}

		// Token: 0x040007A3 RID: 1955
		public const int DEFAULT = 0;

		// Token: 0x040007A4 RID: 1956
		public const int RIGHT_ALIGN = 2;

		// Token: 0x040007A5 RID: 1957
		public const int LEFT_ALIGN = 0;

		// Token: 0x040007A6 RID: 1958
		public const int MIDDLE_ALIGN = 1;

		// Token: 0x040007A7 RID: 1959
		public const int TEXTWRAP = 4;

		// Token: 0x040007A8 RID: 1960
		public const int UNDERLYING = 8;

		// Token: 0x040007A9 RID: 1961
		public const int AX = 0;

		// Token: 0x040007AA RID: 1962
		public const int AY = 1;

		// Token: 0x040007AB RID: 1963
		public const int BX = 2;

		// Token: 0x040007AC RID: 1964
		public const int BY = 3;

		// Token: 0x040007AD RID: 1965
		public const int CX = 4;

		// Token: 0x040007AE RID: 1966
		public const int CY = 5;

		// Token: 0x040007AF RID: 1967
		public const int DX = 6;

		// Token: 0x040007B0 RID: 1968
		public const int DY = 7;

		// Token: 0x040007B1 RID: 1969
		public const int ORIGINAL_NONE = 0;

		// Token: 0x040007B2 RID: 1970
		public const int ORIGINAL_JPEG = 1;

		// Token: 0x040007B3 RID: 1971
		public const int ORIGINAL_PNG = 2;

		// Token: 0x040007B4 RID: 1972
		public const int ORIGINAL_GIF = 3;

		// Token: 0x040007B5 RID: 1973
		public const int ORIGINAL_BMP = 4;

		// Token: 0x040007B6 RID: 1974
		public const int ORIGINAL_TIFF = 5;

		// Token: 0x040007B7 RID: 1975
		public const int ORIGINAL_WMF = 6;

		// Token: 0x040007B8 RID: 1976
		public const int ORIGINAL_JPEG2000 = 8;

		// Token: 0x040007B9 RID: 1977
		public const int ORIGINAL_JBIG2 = 9;

		// Token: 0x040007BA RID: 1978
		protected bool invert;

		// Token: 0x040007BB RID: 1979
		protected int type;

		// Token: 0x040007BC RID: 1980
		protected Uri url;

		// Token: 0x040007BD RID: 1981
		protected byte[] rawData;

		// Token: 0x040007BE RID: 1982
		protected PdfTemplate[] template = new PdfTemplate[1];

		// Token: 0x040007BF RID: 1983
		protected int alignment;

		// Token: 0x040007C0 RID: 1984
		protected string alt;

		// Token: 0x040007C1 RID: 1985
		protected float absoluteX = float.NaN;

		// Token: 0x040007C2 RID: 1986
		protected float absoluteY = float.NaN;

		// Token: 0x040007C3 RID: 1987
		protected float plainWidth;

		// Token: 0x040007C4 RID: 1988
		protected float plainHeight;

		// Token: 0x040007C5 RID: 1989
		protected float scaledWidth;

		// Token: 0x040007C6 RID: 1990
		protected float scaledHeight;

		// Token: 0x040007C7 RID: 1991
		protected int compressionLevel = -1;

		// Token: 0x040007C8 RID: 1992
		protected float rotationRadians;

		// Token: 0x040007C9 RID: 1993
		protected int colorspace = -1;

		// Token: 0x040007CA RID: 1994
		protected int bpc = 1;

		// Token: 0x040007CB RID: 1995
		protected int[] transparency;

		// Token: 0x040007CC RID: 1996
		protected float indentationLeft;

		// Token: 0x040007CD RID: 1997
		protected float indentationRight;

		// Token: 0x040007CE RID: 1998
		protected long mySerialId = Image.GetSerialId();

		// Token: 0x040007CF RID: 1999
		private static object serialId = 0L;

		// Token: 0x040007D0 RID: 2000
		protected int dpiX;

		// Token: 0x040007D1 RID: 2001
		protected int dpiY;

		// Token: 0x040007D2 RID: 2002
		protected bool mask;

		// Token: 0x040007D3 RID: 2003
		protected Image imageMask;

		// Token: 0x040007D4 RID: 2004
		protected bool interpolation;

		// Token: 0x040007D5 RID: 2005
		protected Annotation annotation;

		// Token: 0x040007D6 RID: 2006
		protected ICC_Profile profile;

		// Token: 0x040007D7 RID: 2007
		protected bool deflated;

		// Token: 0x040007D8 RID: 2008
		private PdfDictionary additional;

		// Token: 0x040007D9 RID: 2009
		private bool smask;

		// Token: 0x040007DA RID: 2010
		private float xyRatio;

		// Token: 0x040007DB RID: 2011
		protected int originalType;

		// Token: 0x040007DC RID: 2012
		protected byte[] originalData;

		// Token: 0x040007DD RID: 2013
		protected float spacingBefore;

		// Token: 0x040007DE RID: 2014
		protected float spacingAfter;

		// Token: 0x040007DF RID: 2015
		private float widthPercentage = 100f;

		// Token: 0x040007E0 RID: 2016
		protected IPdfOCG layer;

		// Token: 0x040007E1 RID: 2017
		private float initialRotation;

		// Token: 0x040007E2 RID: 2018
		private PdfIndirectReference directReference;
	}
}
