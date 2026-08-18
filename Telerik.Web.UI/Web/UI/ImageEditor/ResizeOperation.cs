using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000EA9 RID: 3753
	public class ResizeOperation : ImageOperation, IImageOperation
	{
		// Token: 0x17002D3F RID: 11583
		// (get) Token: 0x06008F1E RID: 36638 RVA: 0x002038F7 File Offset: 0x00201AF7
		// (set) Token: 0x06008F1F RID: 36639 RVA: 0x002038FF File Offset: 0x00201AFF
		public Size Size { get; set; }

		// Token: 0x06008F20 RID: 36640 RVA: 0x00203908 File Offset: 0x00201B08
		public ResizeOperation(Size size) : this(size, -1)
		{
		}

		// Token: 0x06008F21 RID: 36641 RVA: 0x00203912 File Offset: 0x00201B12
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public ResizeOperation(Size size, int index)
		{
			this.Size = size;
			this.Index = index;
		}

		// Token: 0x06008F22 RID: 36642 RVA: 0x00203928 File Offset: 0x00201B28
		public Image Apply(Image original)
		{
			return this.Resize(original, this.Size, InterpolationMode.HighQualityBicubic);
		}

		// Token: 0x06008F23 RID: 36643 RVA: 0x00203938 File Offset: 0x00201B38
		public Image Resize(Image originalImg, Size newSize, InterpolationMode intMode)
		{
			int width = newSize.Width;
			int height = newSize.Height;
			if (width > 0 && height > 0 && (originalImg.Width != width || originalImg.Height != height))
			{
				Bitmap bitmap = EditableImage.CheckPixelFormat(originalImg) ? new Bitmap(width, height, originalImg.PixelFormat) : new Bitmap(width, height);
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.SmoothingMode = SmoothingMode.AntiAlias;
					graphics.InterpolationMode = intMode;
					graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
					if (intMode != InterpolationMode.NearestNeighbor)
					{
						RectangleF rect = new RectangleF(-0.5f, -0.5f, (float)(newSize.Width + 1), (float)(newSize.Height + 1));
						graphics.DrawImage(originalImg, rect);
					}
					else
					{
						graphics.DrawImage(originalImg, new Rectangle(new Point(0, 0), newSize), new Rectangle(new Point(0, 0), originalImg.Size), GraphicsUnit.Pixel);
					}
					originalImg = bitmap;
				}
				return bitmap;
			}
			return new Bitmap(originalImg);
		}

		// Token: 0x17002D40 RID: 11584
		// (get) Token: 0x06008F24 RID: 36644 RVA: 0x00203A38 File Offset: 0x00201C38
		public string Name
		{
			get
			{
				return "Resize";
			}
		}
	}
}
