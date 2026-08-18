using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000EA3 RID: 3747
	[Serializable]
	public class EditableImage : IDisposable
	{
		// Token: 0x06008EDB RID: 36571 RVA: 0x00202C55 File Offset: 0x00200E55
		public EditableImage(Stream stream) : this(EditableImage.TryLoadingImage(stream))
		{
		}

		// Token: 0x06008EDC RID: 36572 RVA: 0x00202C63 File Offset: 0x00200E63
		public EditableImage(string imagePath) : this(EditableImage.TryLoadingImage(imagePath))
		{
		}

		// Token: 0x06008EDD RID: 36573 RVA: 0x00202C71 File Offset: 0x00200E71
		public EditableImage(Image image) : this(image, GraphicsCoreManager.Default)
		{
		}

		// Token: 0x06008EDE RID: 36574 RVA: 0x00202C7F File Offset: 0x00200E7F
		public EditableImage(Image image, IGraphicsCore core)
		{
			this._image = image;
			this._graphics = (core ?? GraphicsCoreManager.Default);
			this.CalculateImageFormat();
		}

		// Token: 0x17002D33 RID: 11571
		// (get) Token: 0x06008EDF RID: 36575 RVA: 0x00202CA4 File Offset: 0x00200EA4
		public virtual int Width
		{
			get
			{
				return this._image.Width;
			}
		}

		// Token: 0x17002D34 RID: 11572
		// (get) Token: 0x06008EE0 RID: 36576 RVA: 0x00202CB1 File Offset: 0x00200EB1
		public virtual int Height
		{
			get
			{
				return this._image.Height;
			}
		}

		// Token: 0x17002D35 RID: 11573
		// (get) Token: 0x06008EE1 RID: 36577 RVA: 0x00202CBE File Offset: 0x00200EBE
		public virtual Size Size
		{
			get
			{
				return this._image.Size;
			}
		}

		// Token: 0x17002D36 RID: 11574
		// (get) Token: 0x06008EE2 RID: 36578 RVA: 0x00202CCB File Offset: 0x00200ECB
		public virtual Image Image
		{
			get
			{
				return this._image;
			}
		}

		// Token: 0x17002D37 RID: 11575
		// (get) Token: 0x06008EE3 RID: 36579 RVA: 0x00202CD3 File Offset: 0x00200ED3
		// (set) Token: 0x06008EE4 RID: 36580 RVA: 0x00202CDB File Offset: 0x00200EDB
		public virtual string Format { get; private set; }

		// Token: 0x17002D38 RID: 11576
		// (get) Token: 0x06008EE5 RID: 36581 RVA: 0x00202CE4 File Offset: 0x00200EE4
		// (set) Token: 0x06008EE6 RID: 36582 RVA: 0x00202D00 File Offset: 0x00200F00
		public virtual ImageFormat RawFormat
		{
			get
			{
				if (this._rawFormat == null)
				{
					this._rawFormat = this.ExtractRawImageFormat();
				}
				return this._rawFormat;
			}
			private set
			{
				this._rawFormat = value;
			}
		}

		// Token: 0x17002D39 RID: 11577
		// (get) Token: 0x06008EE7 RID: 36583 RVA: 0x00202D09 File Offset: 0x00200F09
		// (set) Token: 0x06008EE8 RID: 36584 RVA: 0x00202D11 File Offset: 0x00200F11
		public virtual bool IsDisposed { get; protected set; }

		// Token: 0x06008EE9 RID: 36585 RVA: 0x00202D1A File Offset: 0x00200F1A
		public void ChangeOpacity(double opacity)
		{
			this.ChangeImage(this._graphics.ChangeOpacity(this._image, opacity));
		}

		// Token: 0x06008EEA RID: 36586 RVA: 0x00202D34 File Offset: 0x00200F34
		public void Resize(Size size)
		{
			this.ChangeImage(this._graphics.Resize(this._image, size));
		}

		// Token: 0x06008EEB RID: 36587 RVA: 0x00202D4E File Offset: 0x00200F4E
		public void Resize(int width, int height)
		{
			this.ChangeImage(this._graphics.Resize(this._image, new Size(width, height)));
		}

		// Token: 0x06008EEC RID: 36588 RVA: 0x00202D6E File Offset: 0x00200F6E
		public void Flip(FlipDirection direction)
		{
			this.ChangeImage(this._graphics.Flip(this._image, direction));
		}

		// Token: 0x06008EED RID: 36589 RVA: 0x00202D88 File Offset: 0x00200F88
		public void Rotate(Rotation rotate)
		{
			this.ChangeImage(this._graphics.Rotate(this._image, rotate));
		}

		// Token: 0x06008EEE RID: 36590 RVA: 0x00202DA2 File Offset: 0x00200FA2
		public void Crop(Rectangle rectange)
		{
			this.ChangeImage(this._graphics.Crop(this._image, rectange));
		}

		// Token: 0x06008EEF RID: 36591 RVA: 0x00202DBC File Offset: 0x00200FBC
		public void AddText(Point position, ImageText text)
		{
			this.ChangeImage(this._graphics.AddText(this._image, position, text));
		}

		// Token: 0x06008EF0 RID: 36592 RVA: 0x00202DD7 File Offset: 0x00200FD7
		public void InsertImage(Point position, Image imgToInsert)
		{
			this.ChangeImage(this._graphics.InsertImage(this._image, position, imgToInsert));
		}

		// Token: 0x06008EF1 RID: 36593 RVA: 0x00202DF2 File Offset: 0x00200FF2
		public void ConvertTo(EditableFormat format)
		{
			this.ChangeImage(this._graphics.ConvertTo(this._image, format));
			this.CalculateImageFormat();
		}

		// Token: 0x06008EF2 RID: 36594 RVA: 0x00202E12 File Offset: 0x00201012
		public void CalculateImageFormat()
		{
			this.Format = this.ExtractImageFormat();
			this.RawFormat = this._image.RawFormat;
		}

		// Token: 0x06008EF3 RID: 36595 RVA: 0x00202E34 File Offset: 0x00201034
		public void ApplyImageOperations(IEnumerable<IImageOperation> operations)
		{
			foreach (IImageOperation imageOperation in operations)
			{
				using (Image image = this._image)
				{
					this._image = imageOperation.Apply(image);
					image.Dispose();
				}
			}
			this.FixGifColors();
		}

		// Token: 0x06008EF4 RID: 36596 RVA: 0x00202EB0 File Offset: 0x002010B0
		public void CopyToStream(Stream stream)
		{
			Guid guid = ImageFormat.MemoryBmp.Guid;
			ImageFormat format = (this.RawFormat.Guid == guid) ? ImageFormat.Bmp : this.RawFormat;
			this.Image.Save(stream, format);
		}

		// Token: 0x06008EF5 RID: 36597 RVA: 0x00202EF8 File Offset: 0x002010F8
		public void FixGifColors()
		{
			if (this.RawFormat.Equals(ImageFormat.Gif))
			{
				OctreeQuantizer octreeQuantizer = new OctreeQuantizer(255, 8);
				this._image = octreeQuantizer.Quantize(this._image);
			}
		}

		// Token: 0x06008EF6 RID: 36598 RVA: 0x00202F35 File Offset: 0x00201135
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06008EF7 RID: 36599 RVA: 0x00202F44 File Offset: 0x00201144
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.IsDisposed = true;
				this._image.Dispose();
			}
		}

		// Token: 0x06008EF8 RID: 36600 RVA: 0x00202F5C File Offset: 0x0020115C
		private static Image TryLoadingImage(string path)
		{
			Image result;
			try
			{
				result = EditableImage.LoadImage(path);
			}
			catch
			{
				throw new MissingEditableImageException("The source of the image seems to be invalid");
			}
			return result;
		}

		// Token: 0x06008EF9 RID: 36601 RVA: 0x00202F90 File Offset: 0x00201190
		private static Image TryLoadingImage(Stream stream)
		{
			Image result;
			try
			{
				result = new Bitmap(stream);
			}
			catch
			{
				throw new MissingEditableImageException("Image stream seems to be causing issues");
			}
			return result;
		}

		// Token: 0x06008EFA RID: 36602 RVA: 0x00202FC4 File Offset: 0x002011C4
		private static Stream GetFile(string physicalPath)
		{
			if (!File.Exists(physicalPath))
			{
				return null;
			}
			return File.OpenRead(physicalPath);
		}

		// Token: 0x06008EFB RID: 36603 RVA: 0x00202FD8 File Offset: 0x002011D8
		private static Image LoadImage(string originalImage)
		{
			Stream file = EditableImage.GetFile(originalImage);
			if (file == null || file.Length == 0L)
			{
				return null;
			}
			int i = (int)file.Length;
			int num = 0;
			byte[] array = new byte[file.Length];
			while (i > 0)
			{
				int num2 = file.Read(array, num, i);
				num += num2;
				i -= num2;
				if (num2 == 0)
				{
					break;
				}
			}
			if (i > 0)
			{
				return null;
			}
			MemoryStream memoryStream = new MemoryStream();
			memoryStream.Write(array, 0, array.Length);
			file.Close();
			return Image.FromStream(memoryStream);
		}

		// Token: 0x06008EFC RID: 36604 RVA: 0x00203057 File Offset: 0x00201257
		public void ReplaceImage(Image image)
		{
			this.ChangeImage(image);
		}

		// Token: 0x06008EFD RID: 36605 RVA: 0x00203060 File Offset: 0x00201260
		private void ChangeImage(Image image)
		{
			this._image.Dispose();
			this._image = image;
			this.FixGifColors();
		}

		// Token: 0x06008EFE RID: 36606 RVA: 0x0020307C File Offset: 0x0020127C
		private string ExtractImageFormat()
		{
			if (this._image.RawFormat.Guid == ImageFormat.Png.Guid)
			{
				return "png";
			}
			if (this._image.RawFormat.Guid == ImageFormat.Gif.Guid)
			{
				return "gif";
			}
			if (this._image.RawFormat.Guid == ImageFormat.Jpeg.Guid)
			{
				return "jpg";
			}
			if (!(this._image.RawFormat.Guid == ImageFormat.Bmp.Guid))
			{
				return "";
			}
			return "bmp";
		}

		// Token: 0x06008EFF RID: 36607 RVA: 0x0020312C File Offset: 0x0020132C
		private ImageFormat ExtractRawImageFormat()
		{
			string format;
			if ((format = this.Format) != null)
			{
				if (format == "png")
				{
					return ImageFormat.Png;
				}
				if (format == "gif")
				{
					return ImageFormat.Gif;
				}
				if (format == "jpg" || format == "jpeg")
				{
					return ImageFormat.Jpeg;
				}
				if (format == "bmp")
				{
					return ImageFormat.Bmp;
				}
			}
			return ImageFormat.Bmp;
		}

		// Token: 0x06008F00 RID: 36608 RVA: 0x002031A4 File Offset: 0x002013A4
		public EditableImage Clone()
		{
			if (this.IsDisposed)
			{
				throw new ObjectDisposedException("", "Cloning disposed images is not allowed.");
			}
			return new EditableImage((Image)this.Image.Clone(), this._graphics)
			{
				Format = this.Format,
				RawFormat = this.RawFormat
			};
		}

		// Token: 0x06008F01 RID: 36609 RVA: 0x00203200 File Offset: 0x00201400
		internal static bool CheckPixelFormat(Image original)
		{
			return original.PixelFormat != PixelFormat.Format8bppIndexed && original.PixelFormat != PixelFormat.Format1bppIndexed && original.PixelFormat != PixelFormat.Format4bppIndexed && original.PixelFormat != PixelFormat.Format16bppArgb1555 && original.PixelFormat != PixelFormat.Format16bppGrayScale && original.PixelFormat != PixelFormat.Undefined && original.PixelFormat != PixelFormat.Undefined && original.PixelFormat != PixelFormat.Indexed;
		}

		// Token: 0x040027B1 RID: 10161
		[NonSerialized]
		private ImageFormat _rawFormat;

		// Token: 0x040027B2 RID: 10162
		[NonSerialized]
		private readonly IGraphicsCore _graphics;

		// Token: 0x040027B3 RID: 10163
		private Image _image;
	}
}
