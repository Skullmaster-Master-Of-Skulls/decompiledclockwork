using System;

namespace iTextSharp.text.pdf.draw
{
	// Token: 0x020003A3 RID: 931
	public class LineSeparator : VerticalPositionMark
	{
		// Token: 0x0600203E RID: 8254 RVA: 0x000BFB04 File Offset: 0x000BEB04
		public LineSeparator(float lineWidth, float percentage, BaseColor lineColor, int align, float offset)
		{
			this.lineWidth = lineWidth;
			this.percentage = percentage;
			this.lineColor = lineColor;
			this.alignment = align;
			this.offset = offset;
		}

		// Token: 0x0600203F RID: 8255 RVA: 0x000BFB59 File Offset: 0x000BEB59
		public LineSeparator()
		{
		}

		// Token: 0x06002040 RID: 8256 RVA: 0x000BFB7E File Offset: 0x000BEB7E
		public override void Draw(PdfContentByte canvas, float llx, float lly, float urx, float ury, float y)
		{
			canvas.SaveState();
			this.DrawLine(canvas, llx, urx, y);
			canvas.RestoreState();
		}

		// Token: 0x06002041 RID: 8257 RVA: 0x000BFB98 File Offset: 0x000BEB98
		public void DrawLine(PdfContentByte canvas, float leftX, float rightX, float y)
		{
			float num;
			if (this.Percentage < 0f)
			{
				num = -this.Percentage;
			}
			else
			{
				num = (rightX - leftX) * this.Percentage / 100f;
			}
			float num2;
			switch (this.Alignment)
			{
			case 0:
				num2 = 0f;
				goto IL_5F;
			case 2:
				num2 = rightX - leftX - num;
				goto IL_5F;
			}
			num2 = (rightX - leftX - num) / 2f;
			IL_5F:
			canvas.SetLineWidth(this.LineWidth);
			if (this.LineColor != null)
			{
				canvas.SetColorStroke(this.LineColor);
			}
			canvas.MoveTo(num2 + leftX, y + this.offset);
			canvas.LineTo(num2 + num + leftX, y + this.offset);
			canvas.Stroke();
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06002042 RID: 8258 RVA: 0x000BFC50 File Offset: 0x000BEC50
		// (set) Token: 0x06002043 RID: 8259 RVA: 0x000BFC58 File Offset: 0x000BEC58
		public float LineWidth
		{
			get
			{
				return this.lineWidth;
			}
			set
			{
				this.lineWidth = value;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06002044 RID: 8260 RVA: 0x000BFC61 File Offset: 0x000BEC61
		// (set) Token: 0x06002045 RID: 8261 RVA: 0x000BFC69 File Offset: 0x000BEC69
		public float Percentage
		{
			get
			{
				return this.percentage;
			}
			set
			{
				this.percentage = value;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06002046 RID: 8262 RVA: 0x000BFC72 File Offset: 0x000BEC72
		// (set) Token: 0x06002047 RID: 8263 RVA: 0x000BFC7A File Offset: 0x000BEC7A
		public BaseColor LineColor
		{
			get
			{
				return this.lineColor;
			}
			set
			{
				this.lineColor = value;
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06002048 RID: 8264 RVA: 0x000BFC83 File Offset: 0x000BEC83
		// (set) Token: 0x06002049 RID: 8265 RVA: 0x000BFC8B File Offset: 0x000BEC8B
		public int Alignment
		{
			get
			{
				return this.alignment;
			}
			set
			{
				this.alignment = value;
			}
		}

		// Token: 0x04001629 RID: 5673
		protected float lineWidth = 1f;

		// Token: 0x0400162A RID: 5674
		protected float percentage = 100f;

		// Token: 0x0400162B RID: 5675
		protected BaseColor lineColor;

		// Token: 0x0400162C RID: 5676
		protected int alignment = 1;
	}
}
