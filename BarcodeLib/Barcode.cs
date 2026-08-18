using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using BarcodeLib.Symbologies;

namespace BarcodeLib
{
	// Token: 0x02000006 RID: 6
	public class Barcode : IDisposable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public Barcode()
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020F0 File Offset: 0x000002F0
		public Barcode(string data)
		{
			this.Raw_Data = data;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002198 File Offset: 0x00000398
		public Barcode(string data, TYPE iType)
		{
			this.Raw_Data = data;
			this.Encoded_Type = iType;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002244 File Offset: 0x00000444
		// (set) Token: 0x06000005 RID: 5 RVA: 0x0000224C File Offset: 0x0000044C
		public string RawData
		{
			get
			{
				return this.Raw_Data;
			}
			set
			{
				this.Raw_Data = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002255 File Offset: 0x00000455
		public string EncodedValue
		{
			get
			{
				return this.Encoded_Value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x0000225D File Offset: 0x0000045D
		public string Country_Assigning_Manufacturer_Code
		{
			get
			{
				return this._Country_Assigning_Manufacturer_Code;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000226E File Offset: 0x0000046E
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002265 File Offset: 0x00000465
		public TYPE EncodedType
		{
			get
			{
				return this.Encoded_Type;
			}
			set
			{
				this.Encoded_Type = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002276 File Offset: 0x00000476
		public Image EncodedImage
		{
			get
			{
				return this._Encoded_Image;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000227E File Offset: 0x0000047E
		// (set) Token: 0x0600000C RID: 12 RVA: 0x00002286 File Offset: 0x00000486
		public Color ForeColor
		{
			get
			{
				return this._ForeColor;
			}
			set
			{
				this._ForeColor = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000228F File Offset: 0x0000048F
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00002297 File Offset: 0x00000497
		public Color BackColor
		{
			get
			{
				return this._BackColor;
			}
			set
			{
				this._BackColor = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000022A0 File Offset: 0x000004A0
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000022A8 File Offset: 0x000004A8
		public Font LabelFont
		{
			get
			{
				return this._LabelFont;
			}
			set
			{
				this._LabelFont = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000022B1 File Offset: 0x000004B1
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000022B9 File Offset: 0x000004B9
		public LabelPositions LabelPosition
		{
			get
			{
				return this._LabelPosition;
			}
			set
			{
				this._LabelPosition = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000022C2 File Offset: 0x000004C2
		// (set) Token: 0x06000014 RID: 20 RVA: 0x000022CA File Offset: 0x000004CA
		public RotateFlipType RotateFlipType
		{
			get
			{
				return this._RotateFlipType;
			}
			set
			{
				this._RotateFlipType = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000022D3 File Offset: 0x000004D3
		// (set) Token: 0x06000016 RID: 22 RVA: 0x000022DB File Offset: 0x000004DB
		public int Width
		{
			get
			{
				return this._Width;
			}
			set
			{
				this._Width = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000022E4 File Offset: 0x000004E4
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000022EC File Offset: 0x000004EC
		public int Height
		{
			get
			{
				return this._Height;
			}
			set
			{
				this._Height = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000022F5 File Offset: 0x000004F5
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000022FD File Offset: 0x000004FD
		public int? BarWidth { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002306 File Offset: 0x00000506
		// (set) Token: 0x0600001C RID: 28 RVA: 0x0000230E File Offset: 0x0000050E
		public double? AspectRatio { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002317 File Offset: 0x00000517
		// (set) Token: 0x0600001E RID: 30 RVA: 0x0000231F File Offset: 0x0000051F
		public bool IncludeLabel { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002328 File Offset: 0x00000528
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002330 File Offset: 0x00000530
		public string AlternateLabel { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002339 File Offset: 0x00000539
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002341 File Offset: 0x00000541
		public double EncodingTime { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000234A File Offset: 0x0000054A
		public string XML
		{
			get
			{
				return this._XML;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002352 File Offset: 0x00000552
		// (set) Token: 0x06000025 RID: 37 RVA: 0x0000235A File Offset: 0x0000055A
		public ImageFormat ImageFormat
		{
			get
			{
				return this._ImageFormat;
			}
			set
			{
				this._ImageFormat = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002363 File Offset: 0x00000563
		public List<string> Errors
		{
			get
			{
				return this.ibarcode.Errors;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002370 File Offset: 0x00000570
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002378 File Offset: 0x00000578
		public AlignmentPositions Alignment { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002384 File Offset: 0x00000584
		public byte[] Encoded_Image_Bytes
		{
			get
			{
				if (this._Encoded_Image == null)
				{
					return null;
				}
				byte[] result;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					this._Encoded_Image.Save(memoryStream, this._ImageFormat);
					result = memoryStream.ToArray();
				}
				return result;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000023D8 File Offset: 0x000005D8
		public static Version Version
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Version;
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000023E9 File Offset: 0x000005E9
		public Image Encode(TYPE iType, string StringToEncode, int Width, int Height)
		{
			this.Width = Width;
			this.Height = Height;
			return this.Encode(iType, StringToEncode);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002402 File Offset: 0x00000602
		public Image Encode(TYPE iType, string StringToEncode, Color ForeColor, Color BackColor, int Width, int Height)
		{
			this.Width = Width;
			this.Height = Height;
			return this.Encode(iType, StringToEncode, ForeColor, BackColor);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000241F File Offset: 0x0000061F
		public Image Encode(TYPE iType, string StringToEncode, Color ForeColor, Color BackColor)
		{
			this.BackColor = BackColor;
			this.ForeColor = ForeColor;
			return this.Encode(iType, StringToEncode);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002438 File Offset: 0x00000638
		public Image Encode(TYPE iType, string StringToEncode)
		{
			this.Raw_Data = StringToEncode;
			return this.Encode(iType);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002448 File Offset: 0x00000648
		internal Image Encode(TYPE iType)
		{
			this.Encoded_Type = iType;
			return this.Encode();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002458 File Offset: 0x00000658
		internal Image Encode()
		{
			this.ibarcode.Errors.Clear();
			DateTime now = DateTime.Now;
			if (this.Raw_Data.Trim() == "")
			{
				throw new Exception("EENCODE-1: Input data not allowed to be blank.");
			}
			if (this.EncodedType == TYPE.UNSPECIFIED)
			{
				throw new Exception("EENCODE-2: Symbology type not allowed to be unspecified.");
			}
			this.Encoded_Value = "";
			this._Country_Assigning_Manufacturer_Code = "N/A";
			switch (this.Encoded_Type)
			{
			case TYPE.UPCA:
			case TYPE.UCC12:
				this.ibarcode = new UPCA(this.Raw_Data);
				break;
			case TYPE.UPCE:
				this.ibarcode = new UPCE(this.Raw_Data);
				break;
			case TYPE.UPC_SUPPLEMENTAL_2DIGIT:
				this.ibarcode = new UPCSupplement2(this.Raw_Data);
				break;
			case TYPE.UPC_SUPPLEMENTAL_5DIGIT:
				this.ibarcode = new UPCSupplement5(this.Raw_Data);
				break;
			case TYPE.EAN13:
			case TYPE.UCC13:
				this.ibarcode = new EAN13(this.Raw_Data);
				break;
			case TYPE.EAN8:
				this.ibarcode = new EAN8(this.Raw_Data);
				break;
			case TYPE.Interleaved2of5:
				this.ibarcode = new Interleaved2of5(this.Raw_Data);
				break;
			case TYPE.Standard2of5:
			case TYPE.Industrial2of5:
				this.ibarcode = new Standard2of5(this.Raw_Data);
				break;
			case TYPE.CODE39:
			case TYPE.LOGMARS:
				this.ibarcode = new Code39(this.Raw_Data);
				break;
			case TYPE.CODE39Extended:
				this.ibarcode = new Code39(this.Raw_Data, true);
				break;
			case TYPE.CODE39_Mod43:
				this.ibarcode = new Code39(this.Raw_Data, false, true);
				break;
			case TYPE.Codabar:
				this.ibarcode = new Codabar(this.Raw_Data);
				break;
			case TYPE.PostNet:
				this.ibarcode = new Postnet(this.Raw_Data);
				break;
			case TYPE.BOOKLAND:
			case TYPE.ISBN:
				this.ibarcode = new ISBN(this.Raw_Data);
				break;
			case TYPE.JAN13:
				this.ibarcode = new JAN13(this.Raw_Data);
				break;
			case TYPE.MSI_Mod10:
			case TYPE.MSI_2Mod10:
			case TYPE.MSI_Mod11:
			case TYPE.MSI_Mod11_Mod10:
			case TYPE.Modified_Plessey:
				this.ibarcode = new MSI(this.Raw_Data, this.Encoded_Type);
				break;
			case TYPE.CODE11:
			case TYPE.USD8:
				this.ibarcode = new Code11(this.Raw_Data);
				break;
			case TYPE.CODE128:
				this.ibarcode = new Code128(this.Raw_Data);
				break;
			case TYPE.CODE128A:
				this.ibarcode = new Code128(this.Raw_Data, Code128.TYPES.A);
				break;
			case TYPE.CODE128B:
				this.ibarcode = new Code128(this.Raw_Data, Code128.TYPES.B);
				break;
			case TYPE.CODE128C:
				this.ibarcode = new Code128(this.Raw_Data, Code128.TYPES.C);
				break;
			case TYPE.ITF14:
				this.ibarcode = new ITF14(this.Raw_Data);
				break;
			case TYPE.CODE93:
				this.ibarcode = new Code93(this.Raw_Data);
				break;
			case TYPE.TELEPEN:
				this.ibarcode = new Telepen(this.Raw_Data);
				break;
			case TYPE.FIM:
				this.ibarcode = new FIM(this.Raw_Data);
				break;
			case TYPE.PHARMACODE:
				this.ibarcode = new Pharmacode(this.Raw_Data);
				break;
			default:
				throw new Exception("EENCODE-2: Unsupported encoding type specified.");
			}
			this.Encoded_Value = this.ibarcode.Encoded_Value;
			this.Raw_Data = this.ibarcode.RawData;
			this._Encoded_Image = this.Generate_Image();
			this.EncodedImage.RotateFlip(this.RotateFlipType);
			this._XML = this.GetXML();
			this.EncodingTime = (DateTime.Now - now).TotalMilliseconds;
			return this.EncodedImage;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002814 File Offset: 0x00000A14
		private Bitmap Generate_Image()
		{
			if (this.Encoded_Value == "")
			{
				throw new Exception("EGENERATE_IMAGE-1: Must be encoded first.");
			}
			Bitmap bitmap = null;
			DateTime now = DateTime.Now;
			TYPE encoded_Type = this.Encoded_Type;
			if (encoded_Type == TYPE.ITF14)
			{
				if (this.BarWidth != null)
				{
					this.Width = (int)(1.362351611079706 * (double)this.Encoded_Value.Length * (double)this.BarWidth.Value + 1.0);
				}
				double num = (double)this.Width;
				double? aspectRatio = this.AspectRatio;
				this.Height = (((aspectRatio != null) ? new int?((int)(num / aspectRatio.GetValueOrDefault())) : null) ?? this.Height);
				int num2 = this.Height;
				if (this.IncludeLabel)
				{
					num2 -= this.LabelFont.Height;
				}
				bitmap = new Bitmap(this.Width, this.Height);
				int num3 = (int)((double)bitmap.Width / 12.05);
				int num4 = Convert.ToInt32((double)bitmap.Width * 0.05);
				int num5 = (bitmap.Width - num3 * 2 - num4 * 2) / this.Encoded_Value.Length;
				int num6 = (bitmap.Width - num3 * 2 - num4 * 2) % this.Encoded_Value.Length / 2;
				if (num5 <= 0 || num4 <= 0)
				{
					throw new Exception("EGENERATE_IMAGE-3: Image size specified not large enough to draw image. (Bar size determined to be less than 1 pixel or quiet zone determined to be less than 1 pixel)");
				}
				int i = 0;
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.Clear(this.BackColor);
					using (Pen pen = new Pen(this.ForeColor, (float)num5))
					{
						pen.Alignment = PenAlignment.Right;
						while (i < this.Encoded_Value.Length)
						{
							if (this.Encoded_Value[i] == '1')
							{
								graphics.DrawLine(pen, new Point(i * num5 + num6 + num3 + num4, 0), new Point(i * num5 + num6 + num3 + num4, this.Height));
							}
							i++;
						}
						pen.Width = (float)num2 / 8f;
						pen.Color = this.ForeColor;
						pen.Alignment = PenAlignment.Center;
						graphics.DrawLine(pen, new Point(0, 0), new Point(bitmap.Width, 0));
						graphics.DrawLine(pen, new Point(0, num2), new Point(bitmap.Width, num2));
						graphics.DrawLine(pen, new Point(0, 0), new Point(0, num2));
						graphics.DrawLine(pen, new Point(bitmap.Width, 0), new Point(bitmap.Width, num2));
					}
				}
				if (this.IncludeLabel)
				{
					this.Label_ITF14(bitmap);
				}
			}
			else
			{
				this.Width = ((this.BarWidth * this.Encoded_Value.Length) ?? this.Width);
				double num = (double)this.Width;
				double? aspectRatio = this.AspectRatio;
				this.Height = (((aspectRatio != null) ? new int?((int)(num / aspectRatio.GetValueOrDefault())) : null) ?? this.Height);
				int num7 = this.Height;
				if (this.IncludeLabel)
				{
					num7 -= this.LabelFont.Height;
				}
				bitmap = new Bitmap(this.Width, this.Height);
				int num8 = this.Width / this.Encoded_Value.Length;
				int num9 = 1;
				if (this.Encoded_Type == TYPE.PostNet)
				{
					num9 = 2;
				}
				int num10;
				switch (this.Alignment)
				{
				case AlignmentPositions.CENTER:
					num10 = this.Width % this.Encoded_Value.Length / 2;
					break;
				case AlignmentPositions.LEFT:
					num10 = 0;
					break;
				case AlignmentPositions.RIGHT:
					num10 = this.Width % this.Encoded_Value.Length;
					break;
				default:
					num10 = this.Width % this.Encoded_Value.Length / 2;
					break;
				}
				if (num8 <= 0)
				{
					throw new Exception("EGENERATE_IMAGE-2: Image size specified not large enough to draw image. (Bar size determined to be less than 1 pixel)");
				}
				int j = 0;
				int num11 = (int)((double)num8 * 0.5);
				using (Graphics graphics2 = Graphics.FromImage(bitmap))
				{
					graphics2.Clear(this.BackColor);
					using (new Pen(this.BackColor, (float)(num8 / num9)))
					{
						using (Pen pen3 = new Pen(this.ForeColor, (float)(num8 / num9)))
						{
							while (j < this.Encoded_Value.Length)
							{
								if (this.Encoded_Type == TYPE.PostNet)
								{
									if (this.Encoded_Value[j] == '0')
									{
										graphics2.DrawLine(pen3, new Point(j * num8 + num10 + num11, num7), new Point(j * num8 + num10 + num11, num7 / 2));
									}
									else
									{
										graphics2.DrawLine(pen3, new Point(j * num8 + num10 + num11, num7), new Point(j * num8 + num10 + num11, 0));
									}
								}
								else if (this.Encoded_Value[j] == '1')
								{
									graphics2.DrawLine(pen3, new Point(j * num8 + num10 + num11, 0), new Point(j * num8 + num10 + num11, num7));
								}
								j++;
							}
						}
					}
				}
				if (this.IncludeLabel)
				{
					this.Label_Generic(bitmap);
				}
			}
			this._Encoded_Image = bitmap;
			this.EncodingTime += (DateTime.Now - now).TotalMilliseconds;
			return bitmap;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002E74 File Offset: 0x00001074
		public byte[] GetImageData(SaveTypes savetype)
		{
			byte[] result = null;
			try
			{
				if (this._Encoded_Image != null)
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						this.SaveImage(memoryStream, savetype);
						result = memoryStream.ToArray();
						memoryStream.Flush();
						memoryStream.Close();
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception("EGETIMAGEDATA-1: Could not retrieve image data. " + ex.Message);
			}
			return result;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002EF0 File Offset: 0x000010F0
		public void SaveImage(string Filename, SaveTypes FileType)
		{
			try
			{
				if (this._Encoded_Image != null)
				{
					ImageFormat format;
					switch (FileType)
					{
					case SaveTypes.JPG:
						format = ImageFormat.Jpeg;
						break;
					case SaveTypes.BMP:
						format = ImageFormat.Bmp;
						break;
					case SaveTypes.PNG:
						format = ImageFormat.Png;
						break;
					case SaveTypes.GIF:
						format = ImageFormat.Gif;
						break;
					case SaveTypes.TIFF:
						format = ImageFormat.Tiff;
						break;
					default:
						format = this.ImageFormat;
						break;
					}
					((Bitmap)this._Encoded_Image).Save(Filename, format);
				}
			}
			catch (Exception ex)
			{
				throw new Exception("ESAVEIMAGE-1: Could not save image.\n\n=======================\n\n" + ex.Message);
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002F8C File Offset: 0x0000118C
		public void SaveImage(Stream stream, SaveTypes FileType)
		{
			try
			{
				if (this._Encoded_Image != null)
				{
					ImageFormat format;
					switch (FileType)
					{
					case SaveTypes.JPG:
						format = ImageFormat.Jpeg;
						break;
					case SaveTypes.BMP:
						format = ImageFormat.Bmp;
						break;
					case SaveTypes.PNG:
						format = ImageFormat.Png;
						break;
					case SaveTypes.GIF:
						format = ImageFormat.Gif;
						break;
					case SaveTypes.TIFF:
						format = ImageFormat.Tiff;
						break;
					default:
						format = this.ImageFormat;
						break;
					}
					((Bitmap)this._Encoded_Image).Save(stream, format);
				}
			}
			catch (Exception ex)
			{
				throw new Exception("ESAVEIMAGE-2: Could not save image.\n\n=======================\n\n" + ex.Message);
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003028 File Offset: 0x00001228
		public Barcode.ImageSize GetSizeOfImage(bool Metric)
		{
			double num = 0.0;
			double num2 = 0.0;
			if (this.EncodedImage != null && this.EncodedImage.Width > 0 && this.EncodedImage.Height > 0)
			{
				double num3 = 25.4;
				using (Graphics graphics = Graphics.FromImage(this.EncodedImage))
				{
					num = (double)((float)this.EncodedImage.Width / graphics.DpiX);
					num2 = (double)((float)this.EncodedImage.Height / graphics.DpiY);
					if (Metric)
					{
						num *= num3;
						num2 *= num3;
					}
				}
			}
			return new Barcode.ImageSize(num, num2, Metric);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000030DC File Offset: 0x000012DC
		private Image Label_ITF14(Image img)
		{
			try
			{
				Font labelFont = this.LabelFont;
				using (Graphics graphics = Graphics.FromImage(img))
				{
					graphics.DrawImage(img, 0f, 0f);
					graphics.SmoothingMode = SmoothingMode.HighQuality;
					graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
					graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
					graphics.CompositingQuality = CompositingQuality.HighQuality;
					graphics.FillRectangle(new SolidBrush(this.BackColor), new Rectangle(0, img.Height - (labelFont.Height - 2), img.Width, labelFont.Height));
					StringFormat stringFormat = new StringFormat();
					stringFormat.Alignment = StringAlignment.Center;
					graphics.DrawString((this.AlternateLabel == null) ? this.RawData : this.AlternateLabel, labelFont, new SolidBrush(this.ForeColor), (float)(img.Width / 2), (float)(img.Height - labelFont.Height + 1), stringFormat);
					graphics.DrawLine(new Pen(this.ForeColor, (float)img.Height / 16f)
					{
						Alignment = PenAlignment.Inset
					}, new Point(0, img.Height - labelFont.Height - 2), new Point(img.Width, img.Height - labelFont.Height - 2));
					graphics.Save();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("ELABEL_ITF14-1: " + ex.Message);
			}
			return img;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003264 File Offset: 0x00001464
		private Image Label_Generic(Image img)
		{
			try
			{
				Font labelFont = this.LabelFont;
				using (Graphics graphics = Graphics.FromImage(img))
				{
					graphics.DrawImage(img, 0f, 0f);
					graphics.SmoothingMode = SmoothingMode.HighQuality;
					graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
					graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
					graphics.CompositingQuality = CompositingQuality.HighQuality;
					graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
					StringFormat stringFormat = new StringFormat();
					stringFormat.Alignment = StringAlignment.Near;
					stringFormat.LineAlignment = StringAlignment.Near;
					int num = 0;
					switch (this.LabelPosition)
					{
					case LabelPositions.TOPLEFT:
					{
						int width = img.Width;
						num = 0;
						stringFormat.Alignment = StringAlignment.Near;
						break;
					}
					case LabelPositions.TOPCENTER:
					{
						int num2 = img.Width / 2;
						num = 0;
						stringFormat.Alignment = StringAlignment.Center;
						break;
					}
					case LabelPositions.TOPRIGHT:
					{
						int width2 = img.Width;
						num = 0;
						stringFormat.Alignment = StringAlignment.Far;
						break;
					}
					case LabelPositions.BOTTOMLEFT:
						num = img.Height - labelFont.Height;
						stringFormat.Alignment = StringAlignment.Near;
						break;
					case LabelPositions.BOTTOMCENTER:
					{
						int num3 = img.Width / 2;
						num = img.Height - labelFont.Height;
						stringFormat.Alignment = StringAlignment.Center;
						break;
					}
					case LabelPositions.BOTTOMRIGHT:
					{
						int width3 = img.Width;
						num = img.Height - labelFont.Height;
						stringFormat.Alignment = StringAlignment.Far;
						break;
					}
					}
					graphics.FillRectangle(new SolidBrush(this.BackColor), new RectangleF(0f, (float)num, (float)img.Width, (float)labelFont.Height));
					graphics.DrawString((this.AlternateLabel == null) ? this.RawData : this.AlternateLabel, labelFont, new SolidBrush(this.ForeColor), new RectangleF(0f, (float)num, (float)img.Width, (float)labelFont.Height), stringFormat);
					graphics.Save();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("ELABEL_GENERIC-1: " + ex.Message);
			}
			return img;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003458 File Offset: 0x00001658
		private Image Label_UPCA(Image img)
		{
			try
			{
				int num = this.Width / this.Encoded_Value.Length;
				int num2;
				switch (this.Alignment)
				{
				case AlignmentPositions.CENTER:
					num2 = this.Width % this.Encoded_Value.Length / 2;
					break;
				case AlignmentPositions.LEFT:
					num2 = 0;
					break;
				case AlignmentPositions.RIGHT:
					num2 = this.Width % this.Encoded_Value.Length;
					break;
				default:
					num2 = this.Width % this.Encoded_Value.Length / 2;
					break;
				}
				Font font = new Font("OCR A Extended", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
				using (Graphics graphics = Graphics.FromImage(img))
				{
					graphics.DrawImage(img, 0f, 0f);
					graphics.SmoothingMode = SmoothingMode.HighQuality;
					graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
					graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
					graphics.CompositingQuality = CompositingQuality.HighQuality;
					RectangleF rectangleF = new RectangleF((float)(num * 3 + num2), (float)(this.Height - (int)((double)this.Height * 0.1)), (float)(num * 43), (float)((int)((double)this.Height * 0.1)));
					graphics.FillRectangle(new SolidBrush(Color.Yellow), rectangleF.X, rectangleF.Y, rectangleF.Width, rectangleF.Height);
					graphics.DrawString(this.RawData.Substring(1, 5), font, new SolidBrush(this.ForeColor), rectangleF.X, rectangleF.Y);
					graphics.Save();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("ELABEL_UPCA-1: " + ex.Message);
			}
			return img;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000362C File Offset: 0x0000182C
		private string GetXML()
		{
			if (this.EncodedValue == "")
			{
				throw new Exception("EGETXML-1: Could not retrieve XML due to the barcode not being encoded first.  Please call Encode first.");
			}
			string result;
			try
			{
				using (BarcodeXML barcodeXML = new BarcodeXML())
				{
					BarcodeXML.BarcodeRow barcodeRow = barcodeXML.Barcode.NewBarcodeRow();
					barcodeRow.Type = this.EncodedType.ToString();
					barcodeRow.RawData = this.RawData;
					barcodeRow.EncodedValue = this.EncodedValue;
					barcodeRow.EncodingTime = this.EncodingTime;
					barcodeRow.IncludeLabel = this.IncludeLabel;
					barcodeRow.Forecolor = ColorTranslator.ToHtml(this.ForeColor);
					barcodeRow.Backcolor = ColorTranslator.ToHtml(this.BackColor);
					barcodeRow.CountryAssigningManufacturingCode = this.Country_Assigning_Manufacturer_Code;
					barcodeRow.ImageWidth = this.Width;
					barcodeRow.ImageHeight = this.Height;
					barcodeRow.RotateFlipType = this.RotateFlipType;
					barcodeRow.LabelPosition = (int)this.LabelPosition;
					barcodeRow.LabelFont = this.LabelFont.ToString();
					barcodeRow.ImageFormat = this.ImageFormat.ToString();
					barcodeRow.Alignment = (int)this.Alignment;
					using (MemoryStream memoryStream = new MemoryStream())
					{
						this.EncodedImage.Save(memoryStream, this.ImageFormat);
						barcodeRow.Image = Convert.ToBase64String(memoryStream.ToArray(), Base64FormattingOptions.None);
					}
					barcodeXML.Barcode.AddBarcodeRow(barcodeRow);
					StringWriter stringWriter = new StringWriter();
					barcodeXML.WriteXml(stringWriter, XmlWriteMode.WriteSchema);
					result = stringWriter.ToString();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("EGETXML-2: " + ex.Message);
			}
			return result;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003810 File Offset: 0x00001A10
		public static Image GetImageFromXML(BarcodeXML internalXML)
		{
			Image result;
			try
			{
				new byte[internalXML.Barcode[0].Image.Length];
				using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(internalXML.Barcode[0].Image)))
				{
					result = Image.FromStream(memoryStream);
				}
			}
			catch (Exception ex)
			{
				throw new Exception("EGETIMAGEFROMXML-1: " + ex.Message);
			}
			return result;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000389C File Offset: 0x00001A9C
		public static Image DoEncode(TYPE iType, string Data)
		{
			Image result;
			using (Barcode barcode = new Barcode())
			{
				result = barcode.Encode(iType, Data);
			}
			return result;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000038D8 File Offset: 0x00001AD8
		public static Image DoEncode(TYPE iType, string Data, ref string XML)
		{
			Image result;
			using (Barcode barcode = new Barcode())
			{
				Image image = barcode.Encode(iType, Data);
				XML = barcode.XML;
				result = image;
			}
			return result;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000391C File Offset: 0x00001B1C
		public static Image DoEncode(TYPE iType, string Data, bool IncludeLabel)
		{
			Image result;
			using (Barcode barcode = new Barcode())
			{
				barcode.IncludeLabel = IncludeLabel;
				result = barcode.Encode(iType, Data);
			}
			return result;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000395C File Offset: 0x00001B5C
		public static Image DoEncode(TYPE iType, string Data, bool IncludeLabel, int Width, int Height)
		{
			Image result;
			using (Barcode barcode = new Barcode())
			{
				barcode.IncludeLabel = IncludeLabel;
				result = barcode.Encode(iType, Data, Width, Height);
			}
			return result;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000039A0 File Offset: 0x00001BA0
		public static Image DoEncode(TYPE iType, string Data, bool IncludeLabel, Color DrawColor, Color BackColor)
		{
			Image result;
			using (Barcode barcode = new Barcode())
			{
				barcode.IncludeLabel = IncludeLabel;
				result = barcode.Encode(iType, Data, DrawColor, BackColor);
			}
			return result;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000039E4 File Offset: 0x00001BE4
		public static Image DoEncode(TYPE iType, string Data, bool IncludeLabel, Color DrawColor, Color BackColor, int Width, int Height)
		{
			Image result;
			using (Barcode barcode = new Barcode())
			{
				barcode.IncludeLabel = IncludeLabel;
				result = barcode.Encode(iType, Data, DrawColor, BackColor, Width, Height);
			}
			return result;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003A2C File Offset: 0x00001C2C
		public static Image DoEncode(TYPE iType, string Data, bool IncludeLabel, Color DrawColor, Color BackColor, int Width, int Height, ref string XML)
		{
			Image result;
			using (Barcode barcode = new Barcode())
			{
				barcode.IncludeLabel = IncludeLabel;
				Image image = barcode.Encode(iType, Data, DrawColor, BackColor, Width, Height);
				XML = barcode.XML;
				result = image;
			}
			return result;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003A7C File Offset: 0x00001C7C
		public void Dispose()
		{
		}

		// Token: 0x04000039 RID: 57
		private IBarcode ibarcode = new Blank();

		// Token: 0x0400003A RID: 58
		private string Raw_Data = "";

		// Token: 0x0400003B RID: 59
		private string Encoded_Value = "";

		// Token: 0x0400003C RID: 60
		private string _Country_Assigning_Manufacturer_Code = "N/A";

		// Token: 0x0400003D RID: 61
		private TYPE Encoded_Type;

		// Token: 0x0400003E RID: 62
		private Image _Encoded_Image;

		// Token: 0x0400003F RID: 63
		private Color _ForeColor = Color.Black;

		// Token: 0x04000040 RID: 64
		private Color _BackColor = Color.White;

		// Token: 0x04000041 RID: 65
		private int _Width = 300;

		// Token: 0x04000042 RID: 66
		private int _Height = 150;

		// Token: 0x04000043 RID: 67
		private string _XML = "";

		// Token: 0x04000044 RID: 68
		private ImageFormat _ImageFormat = ImageFormat.Jpeg;

		// Token: 0x04000045 RID: 69
		private Font _LabelFont = new Font("Microsoft Sans Serif", 10f, FontStyle.Bold);

		// Token: 0x04000046 RID: 70
		private LabelPositions _LabelPosition = LabelPositions.BOTTOMCENTER;

		// Token: 0x04000047 RID: 71
		private RotateFlipType _RotateFlipType;

		// Token: 0x02000021 RID: 33
		public class ImageSize
		{
			// Token: 0x060000BD RID: 189 RVA: 0x0000F524 File Offset: 0x0000D724
			public ImageSize(double width, double height, bool metric)
			{
				this.Width = width;
				this.Height = height;
				this.Metric = metric;
			}

			// Token: 0x17000037 RID: 55
			// (get) Token: 0x060000BE RID: 190 RVA: 0x0000F541 File Offset: 0x0000D741
			// (set) Token: 0x060000BF RID: 191 RVA: 0x0000F549 File Offset: 0x0000D749
			public double Width { get; set; }

			// Token: 0x17000038 RID: 56
			// (get) Token: 0x060000C0 RID: 192 RVA: 0x0000F552 File Offset: 0x0000D752
			// (set) Token: 0x060000C1 RID: 193 RVA: 0x0000F55A File Offset: 0x0000D75A
			public double Height { get; set; }

			// Token: 0x17000039 RID: 57
			// (get) Token: 0x060000C2 RID: 194 RVA: 0x0000F563 File Offset: 0x0000D763
			// (set) Token: 0x060000C3 RID: 195 RVA: 0x0000F56B File Offset: 0x0000D76B
			public bool Metric { get; set; }
		}
	}
}
