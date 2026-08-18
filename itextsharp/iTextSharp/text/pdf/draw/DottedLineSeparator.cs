using System;

namespace iTextSharp.text.pdf.draw
{
	// Token: 0x020004EE RID: 1262
	public class DottedLineSeparator : LineSeparator
	{
		// Token: 0x06002B2F RID: 11055 RVA: 0x00105CCC File Offset: 0x00104CCC
		public override void Draw(PdfContentByte canvas, float llx, float lly, float urx, float ury, float y)
		{
			canvas.SaveState();
			canvas.SetLineWidth(this.lineWidth);
			canvas.SetLineCap(1);
			canvas.SetLineDash(0f, this.gap, this.gap / 2f);
			base.DrawLine(canvas, llx, urx, y);
			canvas.RestoreState();
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06002B30 RID: 11056 RVA: 0x00105D21 File Offset: 0x00104D21
		// (set) Token: 0x06002B31 RID: 11057 RVA: 0x00105D29 File Offset: 0x00104D29
		public float Gap
		{
			get
			{
				return this.gap;
			}
			set
			{
				this.gap = value;
			}
		}

		// Token: 0x04001DD3 RID: 7635
		protected float gap = 5f;
	}
}
