using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.IO;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002F5 RID: 757
	[Editor(typeof(ImageListImageEditor), typeof(UITypeEditor))]
	internal class ImageListImage
	{
		// Token: 0x06001E33 RID: 7731 RVA: 0x000B6E4F File Offset: 0x000B504F
		public ImageListImage(Bitmap image)
		{
			this.Image = image;
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x000B6E5E File Offset: 0x000B505E
		public ImageListImage(Bitmap image, string name)
		{
			this.Image = image;
			this.Name = name;
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001E35 RID: 7733 RVA: 0x000B6E74 File Offset: 0x000B5074
		// (set) Token: 0x06001E36 RID: 7734 RVA: 0x000B6E8A File Offset: 0x000B508A
		public string Name
		{
			get
			{
				if (this._name != null)
				{
					return this._name;
				}
				return "";
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001E37 RID: 7735 RVA: 0x000B6E93 File Offset: 0x000B5093
		// (set) Token: 0x06001E38 RID: 7736 RVA: 0x000B6E9B File Offset: 0x000B509B
		[Browsable(false)]
		public Bitmap Image
		{
			get
			{
				return this._image;
			}
			set
			{
				this._image = value;
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001E39 RID: 7737 RVA: 0x000B6EA4 File Offset: 0x000B50A4
		public float HorizontalResolution
		{
			get
			{
				return this._image.HorizontalResolution;
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001E3A RID: 7738 RVA: 0x000B6EB1 File Offset: 0x000B50B1
		public float VerticalResolution
		{
			get
			{
				return this._image.VerticalResolution;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001E3B RID: 7739 RVA: 0x000B6EBE File Offset: 0x000B50BE
		public PixelFormat PixelFormat
		{
			get
			{
				return this._image.PixelFormat;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06001E3C RID: 7740 RVA: 0x000B6ECB File Offset: 0x000B50CB
		public ImageFormat RawFormat
		{
			get
			{
				return this._image.RawFormat;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06001E3D RID: 7741 RVA: 0x000B6ED8 File Offset: 0x000B50D8
		public Size Size
		{
			get
			{
				return this._image.Size;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06001E3E RID: 7742 RVA: 0x000B6EE5 File Offset: 0x000B50E5
		public SizeF PhysicalDimension
		{
			get
			{
				return this._image.Size;
			}
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x000B6EF7 File Offset: 0x000B50F7
		public static ImageListImage ImageListImageFromStream(Stream stream, bool imageIsIcon)
		{
			if (imageIsIcon)
			{
				return new ImageListImage(new Icon(stream).ToBitmap());
			}
			return new ImageListImage((Bitmap)System.Drawing.Image.FromStream(stream));
		}

		// Token: 0x040017C6 RID: 6086
		private string _name;

		// Token: 0x040017C7 RID: 6087
		private Bitmap _image;
	}
}
