using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Helpers.Resources;
using System.Web.UI.WebControls;
using Microsoft.Internal.Web.Utils;

namespace System.Web.Helpers
{
	// Token: 0x02000023 RID: 35
	public class WebImage
	{
		// Token: 0x060001B0 RID: 432 RVA: 0x00008910 File Offset: 0x00006B10
		public WebImage(byte[] content)
		{
			this._initialFormat = WebImage.ValidateImageContent(content, "content");
			this._currentFormat = this._initialFormat;
			this._content = (byte[])content.Clone();
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000896A File Offset: 0x00006B6A
		public WebImage(string filePath) : this(new HttpContextWrapper(HttpContext.Current), WebImage._defaultReadAction, filePath)
		{
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00008984 File Offset: 0x00006B84
		public WebImage(Stream imageStream)
		{
			if (imageStream.CanSeek)
			{
				imageStream.Seek(0L, SeekOrigin.Begin);
				this._content = new byte[imageStream.Length];
				using (BinaryReader binaryReader = new BinaryReader(imageStream))
				{
					binaryReader.Read(this._content, 0, (int)imageStream.Length);
					goto IL_107;
				}
			}
			List<byte[]> list = new List<byte[]>();
			int num = 0;
			using (BinaryReader binaryReader2 = new BinaryReader(imageStream))
			{
				int num2 = 51200;
				byte[] array;
				do
				{
					array = binaryReader2.ReadBytes(num2);
					num += array.Length;
					list.Add(array);
				}
				while (array.Length == num2);
			}
			this._content = new byte[num];
			int num3 = 0;
			foreach (byte[] array2 in list)
			{
				array2.CopyTo(this._content, num3);
				num3 += array2.Length;
			}
			IL_107:
			this._initialFormat = WebImage.ValidateImageContent(this._content, "imageStream");
			this._currentFormat = this._initialFormat;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00008AE4 File Offset: 0x00006CE4
		internal WebImage(HttpContextBase httpContext, Func<string, byte[]> readAction, string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "filePath");
			}
			this._fileName = filePath;
			this._content = readAction(VirtualPathUtil.MapPath(httpContext, filePath));
			this._initialFormat = WebImage.ValidateImageContent(this._content, "filePath");
			this._currentFormat = this._initialFormat;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00008B64 File Offset: 0x00006D64
		private WebImage(WebImage other)
		{
			this._content = (byte[])other._content.Clone();
			this._initialFormat = other._initialFormat;
			this._currentFormat = other._currentFormat;
			this._fileName = other._fileName;
			this._height = other._height;
			this._width = other._width;
			this._properties = ((other._properties != null) ? ((PropertyItem[])other._properties.Clone()) : null);
			this._transformations = new List<WebImage.ImageTransformation>(other._transformations);
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00008C14 File Offset: 0x00006E14
		public int Height
		{
			get
			{
				if (this._transformations.Count > 0 || this._height < 0)
				{
					this.ApplyTransformationsAndSetProperties();
				}
				return this._height;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00008C39 File Offset: 0x00006E39
		public int Width
		{
			get
			{
				if (this._transformations.Count > 0 || this._width < 0)
				{
					this.ApplyTransformationsAndSetProperties();
				}
				return this._width;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00008C5E File Offset: 0x00006E5E
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x00008C66 File Offset: 0x00006E66
		public string FileName
		{
			get
			{
				return this._fileName;
			}
			set
			{
				this._fileName = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00008C6F File Offset: 0x00006E6F
		public string ImageFormat
		{
			get
			{
				if (this._transformations.Any<WebImage.ImageTransformation>())
				{
					this.ApplyTransformationsAndSetProperties();
				}
				return ConversionUtil.ToString<ImageFormat>(this._currentFormat).ToLowerInvariant();
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00008C94 File Offset: 0x00006E94
		public static WebImage GetImageFromRequest(string postedFileName = null)
		{
			HttpRequestWrapper request = new HttpRequestWrapper(HttpContext.Current.Request);
			return WebImage.GetImageFromRequest(request, postedFileName);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00008CB8 File Offset: 0x00006EB8
		internal static WebImage GetImageFromRequest(HttpRequestBase request, string postedFileName = null)
		{
			if (request.Files == null || request.Files.Count == 0)
			{
				return null;
			}
			HttpPostedFileBase httpPostedFileBase = string.IsNullOrEmpty(postedFileName) ? request.Files[0] : request.Files[postedFileName];
			if (httpPostedFileBase == null || httpPostedFileBase.ContentLength < 1)
			{
				return null;
			}
			string mimeMapping = MimeMapping.GetMimeMapping(httpPostedFileBase.FileName);
			ImageFormat imageFormat;
			if (!ConversionUtil.TryFromStringToImageFormat(mimeMapping, out imageFormat))
			{
				return null;
			}
			return new WebImage(httpPostedFileBase.InputStream)
			{
				FileName = httpPostedFileBase.FileName
			};
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00008D3E File Offset: 0x00006F3E
		public WebImage Clone()
		{
			return new WebImage(this);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00008D48 File Offset: 0x00006F48
		public byte[] GetBytes(string requestedFormat = null)
		{
			if (this._transformations.Count > 0)
			{
				this.ApplyTransformationsAndSetProperties();
			}
			ImageFormat imageFormat = null;
			if (!string.IsNullOrEmpty(requestedFormat))
			{
				imageFormat = WebImage.GetImageFormat(requestedFormat);
			}
			imageFormat = (imageFormat ?? this._initialFormat);
			if (imageFormat.Equals(this._currentFormat))
			{
				return (byte[])this._content.Clone();
			}
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(this._content))
			{
				using (System.Drawing.Image image = System.Drawing.Image.FromStream(memoryStream))
				{
					if (this._properties != null)
					{
						WebImage.CopyMetadata(this._properties, image);
					}
					using (MemoryStream memoryStream2 = new MemoryStream())
					{
						image.Save(memoryStream2, imageFormat);
						result = memoryStream2.ToArray();
					}
				}
			}
			return result;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00008E2C File Offset: 0x0000702C
		public WebImage Resize(int width, int height, bool preserveAspectRatio = true, bool preventEnlarge = false)
		{
			if (width <= 0)
			{
				throw new ArgumentOutOfRangeException("width", string.Format(CultureInfo.InvariantCulture, CommonResources.Argument_Must_Be_GreaterThan, new object[]
				{
					0
				}));
			}
			if (height <= 0)
			{
				throw new ArgumentOutOfRangeException("height", string.Format(CultureInfo.InvariantCulture, CommonResources.Argument_Must_Be_GreaterThan, new object[]
				{
					0
				}));
			}
			WebImage.ResizeTransformation item = new WebImage.ResizeTransformation(height, width, preserveAspectRatio, preventEnlarge);
			this._transformations.Add(item);
			return this;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00008EB0 File Offset: 0x000070B0
		public WebImage Crop(int top = 0, int left = 0, int bottom = 0, int right = 0)
		{
			if (top < 0)
			{
				throw new ArgumentOutOfRangeException("top", string.Format(CultureInfo.InvariantCulture, CommonResources.Argument_Must_Be_GreaterThanOrEqualTo, new object[]
				{
					0
				}));
			}
			if (left < 0)
			{
				throw new ArgumentOutOfRangeException("left", string.Format(CultureInfo.InvariantCulture, CommonResources.Argument_Must_Be_GreaterThanOrEqualTo, new object[]
				{
					0
				}));
			}
			if (bottom < 0)
			{
				throw new ArgumentOutOfRangeException("bottom", string.Format(CultureInfo.InvariantCulture, CommonResources.Argument_Must_Be_GreaterThanOrEqualTo, new object[]
				{
					0
				}));
			}
			if (right < 0)
			{
				throw new ArgumentOutOfRangeException("right", string.Format(CultureInfo.InvariantCulture, CommonResources.Argument_Must_Be_GreaterThanOrEqualTo, new object[]
				{
					0
				}));
			}
			WebImage.CropTransformation item = new WebImage.CropTransformation(top, right, bottom, left);
			this._transformations.Add(item);
			return this;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00008F98 File Offset: 0x00007198
		public WebImage RotateLeft()
		{
			WebImage.ImageTransformation item = new WebImage.RotateTransformation(RotateFlipType.Rotate270FlipNone);
			this._transformations.Add(item);
			return this;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00008FBC File Offset: 0x000071BC
		public WebImage RotateRight()
		{
			WebImage.ImageTransformation item = new WebImage.RotateTransformation(RotateFlipType.Rotate90FlipNone);
			this._transformations.Add(item);
			return this;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00008FE0 File Offset: 0x000071E0
		public WebImage FlipVertical()
		{
			WebImage.ImageTransformation item = new WebImage.RotateTransformation(RotateFlipType.Rotate180FlipX);
			this._transformations.Add(item);
			return this;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00009004 File Offset: 0x00007204
		public WebImage FlipHorizontal()
		{
			WebImage.ImageTransformation item = new WebImage.RotateTransformation(RotateFlipType.RotateNoneFlipX);
			this._transformations.Add(item);
			return this;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00009028 File Offset: 0x00007228
		public WebImage AddTextWatermark(string text, string fontColor = "Black", int fontSize = 12, string fontStyle = "Regular", string fontFamily = "Microsoft Sans Serif", string horizontalAlign = "Right", string verticalAlign = "Bottom", int opacity = 100, int padding = 5)
		{
			if (string.IsNullOrEmpty(text))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "text");
			}
			Color color;
			if (!ConversionUtil.TryFromStringToColor(fontColor, out color))
			{
				throw new ArgumentException(HelpersResources.WebImage_IncorrectColorName);
			}
			if (opacity < 0 || opacity > 100)
			{
				throw new ArgumentOutOfRangeException("opacity", string.Format(CultureInfo.InvariantCulture, CommonResources.Argument_Must_Be_Between, new object[]
				{
					0,
					100
				}));
			}
			int alpha = 255 * opacity / 100;
			color = Color.FromArgb(alpha, color);
			if (fontSize <= 0)
			{
				throw new ArgumentOutOfRangeException("fontSize", string.Format(CultureInfo.InvariantCulture, CommonResources.Argument_Must_Be_GreaterThan, new object[]
				{
					0
				}));
			}
			FontStyle fontStyle2;
			if (!ConversionUtil.TryFromStringToEnum<FontStyle>(fontStyle, out fontStyle2))
			{
				throw new ArgumentException(HelpersResources.WebImage_IncorrectFontStyle);
			}
			FontFamily fontFamily2;
			if (!ConversionUtil.TryFromStringToFontFamily(fontFamily, out fontFamily2))
			{
				throw new ArgumentException(HelpersResources.WebImage_IncorrectFontFamily);
			}
			HorizontalAlign alignX = WebImage.ParseHorizontalAlign(horizontalAlign);
			VerticalAlign alignY = WebImage.ParseVerticalAlign(verticalAlign);
			if (padding < 0)
			{
				throw new ArgumentOutOfRangeException("padding", string.Format(CultureInfo.InvariantCulture, CommonResources.Argument_Must_Be_GreaterThanOrEqualTo, new object[]
				{
					0
				}));
			}
			WebImage.WatermarkTextTransformation item = new WebImage.WatermarkTextTransformation(text, color, fontSize, fontStyle2, fontFamily2, alignX, alignY, padding);
			this._transformations.Add(item);
			return this;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00009180 File Offset: 0x00007380
		public WebImage AddImageWatermark(WebImage watermarkImage, int width = 0, int height = 0, string horizontalAlign = "Right", string verticalAlign = "Bottom", int opacity = 100, int padding = 5)
		{
			if (watermarkImage == null)
			{
				throw new ArgumentNullException("watermarkImage");
			}
			if (width < 0)
			{
				throw new ArgumentOutOfRangeException("width", string.Format(CultureInfo.InvariantCulture, CommonResources.Argument_Must_Be_GreaterThanOrEqualTo, new object[]
				{
					0
				}));
			}
			if (height < 0)
			{
				throw new ArgumentOutOfRangeException("height", string.Format(CultureInfo.InvariantCulture, CommonResources.Argument_Must_Be_GreaterThanOrEqualTo, new object[]
				{
					0
				}));
			}
			if ((width == 0 && height > 0) || (width > 0 && height == 0))
			{
				throw new ArgumentException(HelpersResources.WebImage_IncorrectWidthAndHeight);
			}
			if (opacity < 0 || opacity > 100)
			{
				throw new ArgumentOutOfRangeException("opacity", string.Format(CultureInfo.InvariantCulture, CommonResources.Argument_Must_Be_Between, new object[]
				{
					0,
					100
				}));
			}
			HorizontalAlign horizontalAlign2 = WebImage.ParseHorizontalAlign(horizontalAlign);
			VerticalAlign verticalAlign2 = WebImage.ParseVerticalAlign(verticalAlign);
			if (padding < 0)
			{
				throw new ArgumentOutOfRangeException("padding", string.Format(CultureInfo.InvariantCulture, CommonResources.Argument_Must_Be_GreaterThanOrEqualTo, new object[]
				{
					0
				}));
			}
			WebImage.WatermarkImageTransformation item = new WebImage.WatermarkImageTransformation(watermarkImage.Clone(), width, height, horizontalAlign2, verticalAlign2, opacity, padding);
			this._transformations.Add(item);
			return this;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000092C0 File Offset: 0x000074C0
		public WebImage AddImageWatermark(string watermarkImageFilePath, int width = 0, int height = 0, string horizontalAlign = "Right", string verticalAlign = "Bottom", int opacity = 100, int padding = 5)
		{
			return this.AddImageWatermark(new HttpContextWrapper(HttpContext.Current), WebImage._defaultReadAction, watermarkImageFilePath, width, height, horizontalAlign, verticalAlign, opacity, padding);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000092ED File Offset: 0x000074ED
		internal WebImage AddImageWatermark(HttpContextBase httpContext, Func<string, byte[]> readAction, string watermarkImageFilePath, int width, int height, string horizontalAlign, string verticalAlign, int opacity, int padding)
		{
			return this.AddImageWatermark(new WebImage(httpContext, readAction, watermarkImageFilePath), width, height, horizontalAlign, verticalAlign, opacity, padding);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000930C File Offset: 0x0000750C
		public WebImage Write(string requestedFormat = null)
		{
			requestedFormat = (requestedFormat ?? this._initialFormat.ToString());
			byte[] bytes = this.GetBytes(requestedFormat);
			string contentType;
			if (requestedFormat.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
			{
				contentType = requestedFormat;
			}
			else
			{
				contentType = "image/" + requestedFormat;
			}
			HttpResponse response = HttpContext.Current.Response;
			response.ContentType = contentType;
			response.BinaryWrite(bytes);
			return this;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000936B File Offset: 0x0000756B
		public WebImage Save(string filePath = null, string imageFormat = null, bool forceCorrectExtension = true)
		{
			return this.Save(new HttpContextWrapper(HttpContext.Current), new Action<string, byte[]>(File.WriteAllBytes), filePath, imageFormat, forceCorrectExtension);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000938C File Offset: 0x0000758C
		internal WebImage Save(HttpContextBase context, Action<string, byte[]> saveAction, string filePath, string imageFormat, bool forceWellKnownExtension)
		{
			filePath = (filePath ?? this.FileName);
			if (string.IsNullOrEmpty(filePath))
			{
				throw new ArgumentNullException("filePath", CommonResources.Argument_Cannot_Be_Null_Or_Empty);
			}
			byte[] bytes = this.GetBytes(imageFormat);
			if (forceWellKnownExtension)
			{
				ImageFormat imageFormat2 = string.IsNullOrEmpty(imageFormat) ? this._initialFormat : WebImage.GetImageFormat(imageFormat);
				string text = Path.GetExtension(filePath).TrimStart(new char[]
				{
					'.'
				});
				ImageFormat imageFormat3;
				if (!ConversionUtil.TryFromStringToImageFormat(text, out imageFormat3) || !imageFormat3.Equals(imageFormat2))
				{
					text = imageFormat2.ToString().ToLowerInvariant();
					filePath = filePath + "." + text;
				}
			}
			saveAction(VirtualPathUtil.MapPath(context, filePath), bytes);
			this.FileName = filePath;
			return this;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00009444 File Offset: 0x00007644
		private static ImageFormat ValidateImageContent(byte[] content, string paramName)
		{
			ImageFormat result;
			try
			{
				using (MemoryStream memoryStream = new MemoryStream(content))
				{
					using (System.Drawing.Image image = System.Drawing.Image.FromStream(memoryStream, false))
					{
						ImageFormat rawFormat = image.RawFormat;
						ImageFormat imageFormat;
						if (!WebImage._imageFormatLookup.TryGetValue(rawFormat.Guid, out imageFormat))
						{
							imageFormat = rawFormat;
						}
						result = imageFormat;
					}
				}
			}
			catch (ArgumentException innerException)
			{
				throw new ArgumentException(HelpersResources.WebImage_InvalidImageContents, paramName, innerException);
			}
			return result;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000094D0 File Offset: 0x000076D0
		private static ImageFormat GetImageFormat(string format)
		{
			ImageFormat result;
			if (!ConversionUtil.TryFromStringToImageFormat(format, out result))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, HelpersResources.Image_IncorrectImageFormat, new object[]
				{
					format
				}), "format");
			}
			return result;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00009510 File Offset: 0x00007710
		private static HorizontalAlign ParseHorizontalAlign(string alignment)
		{
			HorizontalAlign horizontalAlign;
			bool flag = ConversionUtil.TryFromStringToEnum<HorizontalAlign>(alignment, out horizontalAlign);
			if (!flag || horizontalAlign == HorizontalAlign.Justify || horizontalAlign == HorizontalAlign.NotSet)
			{
				throw new ArgumentException(HelpersResources.WebImage_IncorrectHorizontalAlignment);
			}
			return horizontalAlign;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000953C File Offset: 0x0000773C
		private static VerticalAlign ParseVerticalAlign(string alignment)
		{
			VerticalAlign verticalAlign;
			bool flag = ConversionUtil.TryFromStringToEnum<VerticalAlign>(alignment, out verticalAlign);
			if (!flag || verticalAlign == VerticalAlign.NotSet)
			{
				throw new ArgumentException(HelpersResources.WebImage_IncorrectVerticalAlignment);
			}
			return verticalAlign;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00009564 File Offset: 0x00007764
		private void GetContentFromImageAndUpdateFormat(System.Drawing.Image image)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.MemoryBmp))
				{
					image.Save(memoryStream, this._currentFormat);
				}
				else
				{
					image.Save(memoryStream, image.RawFormat);
					this._currentFormat = image.RawFormat;
				}
				this._content = memoryStream.ToArray();
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000095DC File Offset: 0x000077DC
		private void ApplyTransformationsAndSetProperties()
		{
			MemoryStream memoryStream = null;
			System.Drawing.Image image = null;
			try
			{
				memoryStream = new MemoryStream(this._content);
				image = System.Drawing.Image.FromStream(memoryStream);
				if (this._properties == null)
				{
					this._properties = (image.PropertyItems ?? new PropertyItem[0]);
				}
				foreach (WebImage.ImageTransformation imageTransformation in this._transformations)
				{
					System.Drawing.Image image2 = imageTransformation.ApplyTransformation(image);
					if (image2 != image)
					{
						if (memoryStream != null)
						{
							memoryStream.Dispose();
							memoryStream = null;
						}
						image.Dispose();
						image = image2;
					}
				}
				if (this._transformations.Any<WebImage.ImageTransformation>())
				{
					this.GetContentFromImageAndUpdateFormat(image);
					this._transformations.Clear();
				}
				this._height = image.Size.Height;
				this._width = image.Size.Width;
			}
			finally
			{
				if (image != null)
				{
					image.Dispose();
				}
				if (memoryStream != null)
				{
					memoryStream.Dispose();
				}
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000096EC File Offset: 0x000078EC
		private static Bitmap GetBitmapFromImage(System.Drawing.Image image, int width, int height, bool preserveResolution = true)
		{
			bool flag = image.PixelFormat == PixelFormat.Format1bppIndexed || image.PixelFormat == PixelFormat.Format4bppIndexed || image.PixelFormat == PixelFormat.Format8bppIndexed || image.PixelFormat == PixelFormat.Indexed;
			Bitmap bitmap = flag ? new Bitmap(width, height) : new Bitmap(width, height, image.PixelFormat);
			if (preserveResolution)
			{
				bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
			}
			else
			{
				bitmap.SetResolution(96f, 96f);
			}
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				if (flag)
				{
					graphics.FillRectangle(Brushes.White, 0, 0, width, height);
				}
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.DrawImage(image, 0, 0, width, height);
			}
			return bitmap;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000097BC File Offset: 0x000079BC
		private static void CopyMetadata(PropertyItem[] properties, System.Drawing.Image target)
		{
			foreach (PropertyItem propertyItem in properties)
			{
				try
				{
					target.SetPropertyItem(propertyItem);
				}
				catch (ArgumentException)
				{
				}
			}
		}

		// Token: 0x0400008E RID: 142
		private const float FixedResolution = 96f;

		// Token: 0x0400008F RID: 143
		private static readonly IDictionary<Guid, ImageFormat> _imageFormatLookup = new ImageFormat[]
		{
			System.Drawing.Imaging.ImageFormat.Bmp,
			System.Drawing.Imaging.ImageFormat.Emf,
			System.Drawing.Imaging.ImageFormat.Exif,
			System.Drawing.Imaging.ImageFormat.Gif,
			System.Drawing.Imaging.ImageFormat.Icon,
			System.Drawing.Imaging.ImageFormat.Jpeg,
			System.Drawing.Imaging.ImageFormat.MemoryBmp,
			System.Drawing.Imaging.ImageFormat.Png,
			System.Drawing.Imaging.ImageFormat.Tiff,
			System.Drawing.Imaging.ImageFormat.Wmf
		}.ToDictionary((ImageFormat format) => format.Guid, (ImageFormat format) => format);

		// Token: 0x04000090 RID: 144
		private static readonly Func<string, byte[]> _defaultReadAction = new Func<string, byte[]>(File.ReadAllBytes);

		// Token: 0x04000091 RID: 145
		private readonly ImageFormat _initialFormat;

		// Token: 0x04000092 RID: 146
		private readonly List<WebImage.ImageTransformation> _transformations = new List<WebImage.ImageTransformation>();

		// Token: 0x04000093 RID: 147
		private ImageFormat _currentFormat;

		// Token: 0x04000094 RID: 148
		private byte[] _content;

		// Token: 0x04000095 RID: 149
		private string _fileName;

		// Token: 0x04000096 RID: 150
		private int _height = -1;

		// Token: 0x04000097 RID: 151
		private int _width = -1;

		// Token: 0x04000098 RID: 152
		private PropertyItem[] _properties;

		// Token: 0x02000024 RID: 36
		private abstract class ImageTransformation
		{
			// Token: 0x060001D6 RID: 470
			public abstract System.Drawing.Image ApplyTransformation(System.Drawing.Image image);
		}

		// Token: 0x02000025 RID: 37
		private class CropTransformation : WebImage.ImageTransformation
		{
			// Token: 0x060001D8 RID: 472 RVA: 0x000098CC File Offset: 0x00007ACC
			public CropTransformation(int top, int right, int bottom, int left)
			{
				this.Top = top;
				this.Right = right;
				this.Bottom = bottom;
				this.Left = left;
			}

			// Token: 0x17000079 RID: 121
			// (get) Token: 0x060001D9 RID: 473 RVA: 0x000098F1 File Offset: 0x00007AF1
			// (set) Token: 0x060001DA RID: 474 RVA: 0x000098F9 File Offset: 0x00007AF9
			public int Top { get; set; }

			// Token: 0x1700007A RID: 122
			// (get) Token: 0x060001DB RID: 475 RVA: 0x00009902 File Offset: 0x00007B02
			// (set) Token: 0x060001DC RID: 476 RVA: 0x0000990A File Offset: 0x00007B0A
			public int Right { get; set; }

			// Token: 0x1700007B RID: 123
			// (get) Token: 0x060001DD RID: 477 RVA: 0x00009913 File Offset: 0x00007B13
			// (set) Token: 0x060001DE RID: 478 RVA: 0x0000991B File Offset: 0x00007B1B
			public int Bottom { get; set; }

			// Token: 0x1700007C RID: 124
			// (get) Token: 0x060001DF RID: 479 RVA: 0x00009924 File Offset: 0x00007B24
			// (set) Token: 0x060001E0 RID: 480 RVA: 0x0000992C File Offset: 0x00007B2C
			public int Left { get; set; }

			// Token: 0x060001E1 RID: 481 RVA: 0x00009938 File Offset: 0x00007B38
			public override System.Drawing.Image ApplyTransformation(System.Drawing.Image image)
			{
				if (this.Top + this.Bottom > image.Height || this.Left + this.Right > image.Width)
				{
					return image;
				}
				int num = image.Width - (this.Left + this.Right);
				int num2 = image.Height - (this.Top + this.Bottom);
				RectangleF rect = new RectangleF((float)this.Left, (float)this.Top, (float)num, (float)num2);
				System.Drawing.Image result;
				using (Bitmap bitmapFromImage = WebImage.GetBitmapFromImage(image, image.Width, image.Height, true))
				{
					try
					{
						result = bitmapFromImage.Clone(rect, image.PixelFormat);
					}
					catch (OutOfMemoryException)
					{
						result = image;
					}
				}
				return result;
			}
		}

		// Token: 0x02000026 RID: 38
		private class ResizeTransformation : WebImage.ImageTransformation
		{
			// Token: 0x060001E2 RID: 482 RVA: 0x00009A08 File Offset: 0x00007C08
			public ResizeTransformation(int height, int width, bool preserveAspectRatio, bool preventEnlarge)
			{
				this.Height = height;
				this.Width = width;
				this.PreserveAspectRatio = preserveAspectRatio;
				this.PreventEnlarge = preventEnlarge;
			}

			// Token: 0x1700007D RID: 125
			// (get) Token: 0x060001E3 RID: 483 RVA: 0x00009A2D File Offset: 0x00007C2D
			// (set) Token: 0x060001E4 RID: 484 RVA: 0x00009A35 File Offset: 0x00007C35
			public int Height { get; set; }

			// Token: 0x1700007E RID: 126
			// (get) Token: 0x060001E5 RID: 485 RVA: 0x00009A3E File Offset: 0x00007C3E
			// (set) Token: 0x060001E6 RID: 486 RVA: 0x00009A46 File Offset: 0x00007C46
			public int Width { get; set; }

			// Token: 0x1700007F RID: 127
			// (get) Token: 0x060001E7 RID: 487 RVA: 0x00009A4F File Offset: 0x00007C4F
			// (set) Token: 0x060001E8 RID: 488 RVA: 0x00009A57 File Offset: 0x00007C57
			public bool PreserveAspectRatio { get; set; }

			// Token: 0x17000080 RID: 128
			// (get) Token: 0x060001E9 RID: 489 RVA: 0x00009A60 File Offset: 0x00007C60
			// (set) Token: 0x060001EA RID: 490 RVA: 0x00009A68 File Offset: 0x00007C68
			public bool PreventEnlarge { get; set; }

			// Token: 0x060001EB RID: 491 RVA: 0x00009A74 File Offset: 0x00007C74
			public override System.Drawing.Image ApplyTransformation(System.Drawing.Image image)
			{
				int num = this.Height;
				int num2 = this.Width;
				if (this.PreserveAspectRatio)
				{
					double num3 = (double)num * 100.0 / (double)image.Height;
					double num4 = (double)num2 * 100.0 / (double)image.Width;
					if (num3 > num4)
					{
						num = (int)Math.Round(num4 * (double)image.Height / 100.0);
					}
					else if (num3 < num4)
					{
						num2 = (int)Math.Round(num3 * (double)image.Width / 100.0);
					}
				}
				if (this.PreventEnlarge)
				{
					if (num > image.Height)
					{
						num = image.Height;
					}
					if (num2 > image.Width)
					{
						num2 = image.Width;
					}
				}
				if (image.Height == num && image.Width == num2)
				{
					return image;
				}
				return WebImage.GetBitmapFromImage(image, num2, num, true);
			}
		}

		// Token: 0x02000027 RID: 39
		private class RotateTransformation : WebImage.ImageTransformation
		{
			// Token: 0x060001EC RID: 492 RVA: 0x00009B44 File Offset: 0x00007D44
			public RotateTransformation(RotateFlipType direction)
			{
				this.Direction = direction;
			}

			// Token: 0x17000081 RID: 129
			// (get) Token: 0x060001ED RID: 493 RVA: 0x00009B53 File Offset: 0x00007D53
			// (set) Token: 0x060001EE RID: 494 RVA: 0x00009B5B File Offset: 0x00007D5B
			public RotateFlipType Direction { get; set; }

			// Token: 0x060001EF RID: 495 RVA: 0x00009B64 File Offset: 0x00007D64
			public override System.Drawing.Image ApplyTransformation(System.Drawing.Image image)
			{
				image.RotateFlip(this.Direction);
				return image;
			}
		}

		// Token: 0x02000028 RID: 40
		private abstract class WatermarkTransformation : WebImage.ImageTransformation
		{
			// Token: 0x060001F0 RID: 496 RVA: 0x00009B73 File Offset: 0x00007D73
			public WatermarkTransformation(HorizontalAlign alignX, VerticalAlign alignY, int padding)
			{
				this.HorizontalAlign = alignX;
				this.VerticalAlign = alignY;
				this.Padding = padding;
			}

			// Token: 0x17000082 RID: 130
			// (get) Token: 0x060001F1 RID: 497 RVA: 0x00009B90 File Offset: 0x00007D90
			// (set) Token: 0x060001F2 RID: 498 RVA: 0x00009B98 File Offset: 0x00007D98
			public HorizontalAlign HorizontalAlign { get; set; }

			// Token: 0x17000083 RID: 131
			// (get) Token: 0x060001F3 RID: 499 RVA: 0x00009BA1 File Offset: 0x00007DA1
			// (set) Token: 0x060001F4 RID: 500 RVA: 0x00009BA9 File Offset: 0x00007DA9
			public VerticalAlign VerticalAlign { get; set; }

			// Token: 0x17000084 RID: 132
			// (get) Token: 0x060001F5 RID: 501 RVA: 0x00009BB2 File Offset: 0x00007DB2
			// (set) Token: 0x060001F6 RID: 502 RVA: 0x00009BBA File Offset: 0x00007DBA
			public int Padding { get; set; }

			// Token: 0x060001F7 RID: 503 RVA: 0x00009BC4 File Offset: 0x00007DC4
			public Rectangle GetRectangleInsideImage(System.Drawing.Image image, int width, int height)
			{
				int x;
				switch (this.HorizontalAlign)
				{
				case HorizontalAlign.Left:
					x = this.Padding;
					goto IL_43;
				case HorizontalAlign.Right:
					x = image.Width - width - this.Padding;
					goto IL_43;
				}
				x = (image.Width - width) / 2;
				IL_43:
				int y;
				switch (this.VerticalAlign)
				{
				case VerticalAlign.Top:
					y = this.Padding;
					goto IL_86;
				case VerticalAlign.Bottom:
					y = image.Height - height - this.Padding;
					goto IL_86;
				}
				y = (image.Height - height) / 2;
				IL_86:
				return new Rectangle(x, y, width, height);
			}

			// Token: 0x060001F8 RID: 504 RVA: 0x00009C60 File Offset: 0x00007E60
			private static float[][] GetScalingMatrix(float alphaScaling)
			{
				if (alphaScaling == 1f)
				{
					return WebImage.WatermarkTransformation._identityScalingMatrix;
				}
				float[][] array = new float[5][];
				float[][] array2 = array;
				int num = 0;
				float[] array3 = new float[5];
				array3[0] = 1f;
				array2[num] = array3;
				float[][] array4 = array;
				int num2 = 1;
				float[] array5 = new float[5];
				array5[1] = 1f;
				array4[num2] = array5;
				float[][] array6 = array;
				int num3 = 2;
				float[] array7 = new float[5];
				array7[2] = 1f;
				array6[num3] = array7;
				float[][] array8 = array;
				int num4 = 3;
				float[] array9 = new float[5];
				array9[3] = alphaScaling;
				array8[num4] = array9;
				array[4] = new float[]
				{
					0f,
					0f,
					0f,
					0f,
					1f
				};
				return array;
			}

			// Token: 0x060001F9 RID: 505 RVA: 0x00009CEC File Offset: 0x00007EEC
			public static void AddWatermark(Graphics targetGraphics, System.Drawing.Image watermark, Rectangle rect, float alphaScaling)
			{
				float[][] scalingMatrix = WebImage.WatermarkTransformation.GetScalingMatrix(alphaScaling);
				ColorMatrix newColorMatrix = new ColorMatrix(scalingMatrix);
				using (ImageAttributes imageAttributes = new ImageAttributes())
				{
					imageAttributes.SetColorMatrix(newColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Default);
					targetGraphics.DrawImage(watermark, rect, 0, 0, watermark.Width, watermark.Height, GraphicsUnit.Pixel, imageAttributes);
				}
			}

			// Token: 0x060001FA RID: 506 RVA: 0x00009D4C File Offset: 0x00007F4C
			// Note: this type is marked as 'beforefieldinit'.
			static WatermarkTransformation()
			{
				float[][] array = new float[5][];
				float[][] array2 = array;
				int num = 0;
				float[] array3 = new float[5];
				array3[0] = 1f;
				array2[num] = array3;
				float[][] array4 = array;
				int num2 = 1;
				float[] array5 = new float[5];
				array5[1] = 1f;
				array4[num2] = array5;
				float[][] array6 = array;
				int num3 = 2;
				float[] array7 = new float[5];
				array7[2] = 1f;
				array6[num3] = array7;
				float[][] array8 = array;
				int num4 = 3;
				float[] array9 = new float[5];
				array9[3] = 1f;
				array8[num4] = array9;
				array[4] = new float[]
				{
					0f,
					0f,
					0f,
					0f,
					1f
				};
				WebImage.WatermarkTransformation._identityScalingMatrix = array;
			}

			// Token: 0x040000A4 RID: 164
			private static readonly float[][] _identityScalingMatrix;
		}

		// Token: 0x02000029 RID: 41
		private class WatermarkImageTransformation : WebImage.WatermarkTransformation
		{
			// Token: 0x060001FB RID: 507 RVA: 0x00009DCB File Offset: 0x00007FCB
			public WatermarkImageTransformation(WebImage image, int width, int height, HorizontalAlign horizontalAlign, VerticalAlign verticalAlign, int opacity, int padding) : base(horizontalAlign, verticalAlign, padding)
			{
				this.WatermarkImage = image;
				this.Width = width;
				this.Height = height;
				this.Opacity = opacity;
			}

			// Token: 0x17000085 RID: 133
			// (get) Token: 0x060001FC RID: 508 RVA: 0x00009DF6 File Offset: 0x00007FF6
			// (set) Token: 0x060001FD RID: 509 RVA: 0x00009DFE File Offset: 0x00007FFE
			public WebImage WatermarkImage { get; set; }

			// Token: 0x17000086 RID: 134
			// (get) Token: 0x060001FE RID: 510 RVA: 0x00009E07 File Offset: 0x00008007
			// (set) Token: 0x060001FF RID: 511 RVA: 0x00009E0F File Offset: 0x0000800F
			public int Width { get; set; }

			// Token: 0x17000087 RID: 135
			// (get) Token: 0x06000200 RID: 512 RVA: 0x00009E18 File Offset: 0x00008018
			// (set) Token: 0x06000201 RID: 513 RVA: 0x00009E20 File Offset: 0x00008020
			public int Height { get; set; }

			// Token: 0x17000088 RID: 136
			// (get) Token: 0x06000202 RID: 514 RVA: 0x00009E29 File Offset: 0x00008029
			// (set) Token: 0x06000203 RID: 515 RVA: 0x00009E31 File Offset: 0x00008031
			public int Opacity { get; set; }

			// Token: 0x06000204 RID: 516 RVA: 0x00009E3C File Offset: 0x0000803C
			public override System.Drawing.Image ApplyTransformation(System.Drawing.Image image)
			{
				if (this.Width == 0)
				{
					this.Width = this.WatermarkImage.Width;
					this.Height = this.WatermarkImage.Height;
				}
				if (base.Padding * 2 + this.Width >= image.Width || base.Padding * 2 + this.Height >= image.Height)
				{
					return image;
				}
				this.WatermarkImage.Resize(this.Width, this.Height, false, false);
				float alphaScaling = (float)this.Opacity / 100f;
				byte[] bytes = this.WatermarkImage.GetBytes(null);
				Rectangle rectangleInsideImage = base.GetRectangleInsideImage(image, this.Width, this.Height);
				using (Graphics graphics = Graphics.FromImage(image))
				{
					using (MemoryStream memoryStream = new MemoryStream(bytes))
					{
						using (System.Drawing.Image image2 = System.Drawing.Image.FromStream(memoryStream))
						{
							WebImage.WatermarkTransformation.AddWatermark(graphics, image2, rectangleInsideImage, alphaScaling);
						}
					}
				}
				return image;
			}
		}

		// Token: 0x0200002A RID: 42
		private class WatermarkTextTransformation : WebImage.WatermarkTransformation
		{
			// Token: 0x06000205 RID: 517 RVA: 0x00009F60 File Offset: 0x00008160
			public WatermarkTextTransformation(string text, Color fontColor, int fontSize, FontStyle fontStyle, FontFamily fontFamily, HorizontalAlign alignX, VerticalAlign alignY, int padding) : base(alignX, alignY, padding)
			{
				this.Text = text;
				this.FontColor = fontColor;
				this.FontSize = fontSize;
				this.FontStyle = fontStyle;
				this.FontFamily = fontFamily;
			}

			// Token: 0x17000089 RID: 137
			// (get) Token: 0x06000206 RID: 518 RVA: 0x00009F93 File Offset: 0x00008193
			// (set) Token: 0x06000207 RID: 519 RVA: 0x00009F9B File Offset: 0x0000819B
			public string Text { get; set; }

			// Token: 0x1700008A RID: 138
			// (get) Token: 0x06000208 RID: 520 RVA: 0x00009FA4 File Offset: 0x000081A4
			// (set) Token: 0x06000209 RID: 521 RVA: 0x00009FAC File Offset: 0x000081AC
			public Color FontColor { get; set; }

			// Token: 0x1700008B RID: 139
			// (get) Token: 0x0600020A RID: 522 RVA: 0x00009FB5 File Offset: 0x000081B5
			// (set) Token: 0x0600020B RID: 523 RVA: 0x00009FBD File Offset: 0x000081BD
			public int FontSize { get; set; }

			// Token: 0x1700008C RID: 140
			// (get) Token: 0x0600020C RID: 524 RVA: 0x00009FC6 File Offset: 0x000081C6
			// (set) Token: 0x0600020D RID: 525 RVA: 0x00009FCE File Offset: 0x000081CE
			public FontStyle FontStyle { get; set; }

			// Token: 0x1700008D RID: 141
			// (get) Token: 0x0600020E RID: 526 RVA: 0x00009FD7 File Offset: 0x000081D7
			// (set) Token: 0x0600020F RID: 527 RVA: 0x00009FDF File Offset: 0x000081DF
			public FontFamily FontFamily { get; set; }

			// Token: 0x06000210 RID: 528 RVA: 0x00009FE8 File Offset: 0x000081E8
			public override System.Drawing.Image ApplyTransformation(System.Drawing.Image image)
			{
				if (base.Padding * 2 >= image.Width || base.Padding * 2 >= image.Height)
				{
					return image;
				}
				SizeF sizeF;
				int bestFontSize;
				using (Bitmap bitmapFromImage = WebImage.GetBitmapFromImage(image, image.Width, image.Height, false))
				{
					using (Graphics graphics = Graphics.FromImage(bitmapFromImage))
					{
						bestFontSize = this.GetBestFontSize(image, graphics, out sizeF);
					}
				}
				int width = (int)Math.Ceiling((double)sizeF.Width);
				int height = (int)Math.Ceiling((double)sizeF.Height);
				Rectangle rectangleInsideImage = base.GetRectangleInsideImage(image, width, height);
				using (Bitmap bitmap = new Bitmap(width, height))
				{
					using (Graphics graphics2 = Graphics.FromImage(bitmap))
					{
						using (Font font = new Font(this.FontFamily, (float)bestFontSize, this.FontStyle))
						{
							using (Brush brush = new SolidBrush(this.FontColor))
							{
								graphics2.TextRenderingHint = TextRenderingHint.AntiAlias;
								graphics2.DrawString(this.Text, font, brush, new PointF(0f, 0f));
							}
						}
					}
					using (Graphics graphics3 = Graphics.FromImage(image))
					{
						WebImage.WatermarkTransformation.AddWatermark(graphics3, bitmap, rectangleInsideImage, 1f);
					}
				}
				return image;
			}

			// Token: 0x06000211 RID: 529 RVA: 0x0000A198 File Offset: 0x00008398
			private int GetBestFontSize(System.Drawing.Image image, Graphics graphics, out SizeF textArea)
			{
				SizeF sizeF = new SizeF((float)(image.Width - base.Padding * 2), (float)(image.Height - base.Padding * 2));
				int result = this.FontSize;
				textArea = sizeF;
				using (StringFormat stringFormat = new StringFormat(StringFormatFlags.MeasureTrailingSpaces | StringFormatFlags.NoClip))
				{
					for (int i = this.FontSize; i >= 2; i--)
					{
						int num = 0;
						int num2 = 0;
						using (Font font = new Font(this.FontFamily, (float)i, this.FontStyle))
						{
							textArea = graphics.MeasureString(this.Text, font, sizeF, stringFormat, out num, out num2);
						}
						if (num >= this.Text.Length && textArea.Width <= sizeF.Width && textArea.Height <= sizeF.Height)
						{
							return i;
						}
						result = i;
					}
				}
				return result;
			}
		}
	}
}
