using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000EAC RID: 3756
	public class OpacityOperation : ImageOperation, IImageOperation
	{
		// Token: 0x17002D47 RID: 11591
		// (get) Token: 0x06008F34 RID: 36660 RVA: 0x00203AE4 File Offset: 0x00201CE4
		// (set) Token: 0x06008F35 RID: 36661 RVA: 0x00203AEC File Offset: 0x00201CEC
		public double Opacity { get; set; }

		// Token: 0x17002D48 RID: 11592
		// (get) Token: 0x06008F36 RID: 36662 RVA: 0x00203AF5 File Offset: 0x00201CF5
		public string Name
		{
			get
			{
				return "Opacity";
			}
		}

		// Token: 0x06008F37 RID: 36663 RVA: 0x00203AFC File Offset: 0x00201CFC
		public OpacityOperation(double opacity) : this(opacity, -1)
		{
		}

		// Token: 0x06008F38 RID: 36664 RVA: 0x00203B06 File Offset: 0x00201D06
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public OpacityOperation(double opacity, int index)
		{
			this.Opacity = opacity;
			this.Index = index;
		}

		// Token: 0x06008F39 RID: 36665 RVA: 0x00203B1C File Offset: 0x00201D1C
		public Image Apply(Image original)
		{
			Bitmap bitmap = new Bitmap(original.Width, original.Height);
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				ColorMatrix colorMatrix = new ColorMatrix();
				colorMatrix.Matrix33 = (float)this.Opacity;
				ImageAttributes imageAttributes = new ImageAttributes();
				imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
				graphics.DrawImage(original, new Rectangle(0, 0, bitmap.Width, bitmap.Height), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, imageAttributes);
			}
			return bitmap;
		}
	}
}
