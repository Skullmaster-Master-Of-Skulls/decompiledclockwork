using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000EA8 RID: 3752
	public class CropOperation : ImageOperation, IImageOperation
	{
		// Token: 0x17002D3D RID: 11581
		// (get) Token: 0x06008F18 RID: 36632 RVA: 0x002037EB File Offset: 0x002019EB
		// (set) Token: 0x06008F19 RID: 36633 RVA: 0x002037F3 File Offset: 0x002019F3
		public Rectangle Rectange { get; set; }

		// Token: 0x06008F1A RID: 36634 RVA: 0x002037FC File Offset: 0x002019FC
		public CropOperation(Rectangle rectangle) : this(rectangle, -1)
		{
		}

		// Token: 0x06008F1B RID: 36635 RVA: 0x00203806 File Offset: 0x00201A06
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public CropOperation(Rectangle rectangle, int index)
		{
			this.Rectange = rectangle;
			this.Index = index;
		}

		// Token: 0x06008F1C RID: 36636 RVA: 0x0020381C File Offset: 0x00201A1C
		public Image Apply(Image original)
		{
			Bitmap bitmap = EditableImage.CheckPixelFormat(original) ? new Bitmap(this.Rectange.Width, this.Rectange.Height, original.PixelFormat) : new Bitmap(this.Rectange.Width, this.Rectange.Height, PixelFormat.Format32bppPArgb);
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				graphics.SmoothingMode = SmoothingMode.AntiAlias;
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
				graphics.DrawImage(original, new Rectangle(0, 0, this.Rectange.Width, this.Rectange.Height), this.Rectange, GraphicsUnit.Pixel);
			}
			return bitmap;
		}

		// Token: 0x17002D3E RID: 11582
		// (get) Token: 0x06008F1D RID: 36637 RVA: 0x002038F0 File Offset: 0x00201AF0
		public string Name
		{
			get
			{
				return "Crop";
			}
		}
	}
}
