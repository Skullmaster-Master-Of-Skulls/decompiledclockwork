using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000B9F RID: 2975
	public class InsertImageOperation : ImageOperation, IImageOperation
	{
		// Token: 0x170024BD RID: 9405
		// (get) Token: 0x06007050 RID: 28752 RVA: 0x001A382F File Offset: 0x001A1A2F
		// (set) Token: 0x06007051 RID: 28753 RVA: 0x001A3837 File Offset: 0x001A1A37
		public Point Position { get; set; }

		// Token: 0x170024BE RID: 9406
		// (get) Token: 0x06007052 RID: 28754 RVA: 0x001A3840 File Offset: 0x001A1A40
		// (set) Token: 0x06007053 RID: 28755 RVA: 0x001A3848 File Offset: 0x001A1A48
		public Image Image { get; set; }

		// Token: 0x06007054 RID: 28756 RVA: 0x001A3851 File Offset: 0x001A1A51
		public InsertImageOperation(Point position, EditableImage editableImage) : this(position, editableImage.Image)
		{
		}

		// Token: 0x06007055 RID: 28757 RVA: 0x001A3860 File Offset: 0x001A1A60
		public InsertImageOperation(Point position, Image image) : this(position, image, -1)
		{
		}

		// Token: 0x06007056 RID: 28758 RVA: 0x001A386B File Offset: 0x001A1A6B
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public InsertImageOperation(Point position, Image image, int index)
		{
			this.Position = position;
			this.Image = (image.Clone() as Image);
			this.Index = index;
		}

		// Token: 0x06007057 RID: 28759 RVA: 0x001A3894 File Offset: 0x001A1A94
		public Image Apply(Image image)
		{
			Image result;
			using (Bitmap bitmap = new Bitmap(image))
			{
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
					graphics.SmoothingMode = SmoothingMode.AntiAlias;
					graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
					graphics.CompositingQuality = CompositingQuality.HighQuality;
					graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
					using (Image image2 = new Bitmap(this.Image))
					{
						graphics.DrawImage(image2, new Rectangle(this.Position.X, this.Position.Y, image2.Width, image2.Height));
					}
					Image image3 = Image.FromHbitmap(bitmap.GetHbitmap());
					result = image3;
				}
			}
			return result;
		}

		// Token: 0x170024BF RID: 9407
		// (get) Token: 0x06007058 RID: 28760 RVA: 0x001A3970 File Offset: 0x001A1B70
		public string Name
		{
			get
			{
				return "InsertImage";
			}
		}
	}
}
