using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Text;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000EA7 RID: 3751
	public class AddTextOperation : ImageOperation, IImageOperation
	{
		// Token: 0x17002D3A RID: 11578
		// (get) Token: 0x06008F10 RID: 36624 RVA: 0x0020370B File Offset: 0x0020190B
		// (set) Token: 0x06008F11 RID: 36625 RVA: 0x00203713 File Offset: 0x00201913
		public Point Position { get; set; }

		// Token: 0x17002D3B RID: 11579
		// (get) Token: 0x06008F12 RID: 36626 RVA: 0x0020371C File Offset: 0x0020191C
		// (set) Token: 0x06008F13 RID: 36627 RVA: 0x00203724 File Offset: 0x00201924
		public ImageText Text { get; set; }

		// Token: 0x06008F14 RID: 36628 RVA: 0x0020372D File Offset: 0x0020192D
		public AddTextOperation(Point position, ImageText text) : this(position, text, -1)
		{
		}

		// Token: 0x06008F15 RID: 36629 RVA: 0x00203738 File Offset: 0x00201938
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public AddTextOperation(Point position, ImageText text, int index)
		{
			this.Position = position;
			this.Text = text;
			this.Index = index;
		}

		// Token: 0x06008F16 RID: 36630 RVA: 0x00203758 File Offset: 0x00201958
		public Image Apply(Image image)
		{
			Bitmap bitmap = new Bitmap(image);
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
				graphics.DrawString(this.Text.Value, new Font(this.Text.FontFamily, this.Text.Size, GraphicsUnit.Pixel), new SolidBrush(ColorTranslator.FromHtml(this.Text.Color)), this.Position);
			}
			return bitmap;
		}

		// Token: 0x17002D3C RID: 11580
		// (get) Token: 0x06008F17 RID: 36631 RVA: 0x002037E4 File Offset: 0x002019E4
		public string Name
		{
			get
			{
				return "AddText";
			}
		}
	}
}
