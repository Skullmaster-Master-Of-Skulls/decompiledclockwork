using System;
using System.util;

namespace iTextSharp.text.pdf.parser
{
	// Token: 0x02000219 RID: 537
	public class TextMarginFinder : IRenderListener
	{
		// Token: 0x060014F0 RID: 5360 RVA: 0x00076024 File Offset: 0x00075024
		public void RenderText(TextRenderInfo renderInfo)
		{
			if (this.textRectangle == null)
			{
				this.textRectangle = renderInfo.GetDescentLine().GetBoundingRectange();
			}
			else
			{
				this.textRectangle.Add(renderInfo.GetDescentLine().GetBoundingRectange());
			}
			this.textRectangle.Add(renderInfo.GetAscentLine().GetBoundingRectange());
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x00076078 File Offset: 0x00075078
		public float GetLlx()
		{
			return this.textRectangle.X;
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x00076085 File Offset: 0x00075085
		public float GetLly()
		{
			return this.textRectangle.Y;
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x00076092 File Offset: 0x00075092
		public float GetUrx()
		{
			return this.textRectangle.X + this.textRectangle.Width;
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x000760AB File Offset: 0x000750AB
		public float GetUry()
		{
			return this.textRectangle.Y + this.textRectangle.Height;
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x000760C4 File Offset: 0x000750C4
		public float GetWidth()
		{
			return this.textRectangle.Width;
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x000760D1 File Offset: 0x000750D1
		public float GetHeight()
		{
			return this.textRectangle.Height;
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x000760DE File Offset: 0x000750DE
		public void BeginTextBlock()
		{
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x000760E0 File Offset: 0x000750E0
		public void EndTextBlock()
		{
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x000760E2 File Offset: 0x000750E2
		public void RenderImage(ImageRenderInfo renderInfo)
		{
		}

		// Token: 0x04000E35 RID: 3637
		private RectangleJ textRectangle;
	}
}
