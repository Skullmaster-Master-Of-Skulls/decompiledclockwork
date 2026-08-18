using System;
using System.Text;

namespace iTextSharp.text.pdf.parser
{
	// Token: 0x0200063C RID: 1596
	public class SimpleTextExtractionStrategy : ITextExtractionStrategy, IRenderListener
	{
		// Token: 0x060035FE RID: 13822 RVA: 0x0014F5BD File Offset: 0x0014E5BD
		public void BeginTextBlock()
		{
		}

		// Token: 0x060035FF RID: 13823 RVA: 0x0014F5BF File Offset: 0x0014E5BF
		public void EndTextBlock()
		{
		}

		// Token: 0x06003600 RID: 13824 RVA: 0x0014F5C1 File Offset: 0x0014E5C1
		public string GetResultantText()
		{
			return this.result.ToString();
		}

		// Token: 0x06003601 RID: 13825 RVA: 0x0014F5D0 File Offset: 0x0014E5D0
		public void RenderText(TextRenderInfo renderInfo)
		{
			bool flag = this.result.Length == 0;
			bool flag2 = false;
			LineSegment baseline = renderInfo.GetBaseline();
			Vector startPoint = baseline.GetStartPoint();
			Vector endPoint = baseline.GetEndPoint();
			if (!flag)
			{
				Vector v = startPoint;
				Vector vector = this.lastStart;
				Vector vector2 = this.lastEnd;
				float num = vector2.Subtract(vector).Cross(vector.Subtract(v)).LengthSquared / vector2.Subtract(vector).LengthSquared;
				float num2 = 1f;
				if (num > num2)
				{
					flag2 = true;
				}
			}
			if (flag2)
			{
				this.result.Append('\n');
			}
			else if (!flag && this.result[this.result.Length - 1] != ' ' && renderInfo.GetText()[0] != ' ')
			{
				float length = this.lastEnd.Subtract(startPoint).Length;
				if (length > renderInfo.GetSingleSpaceWidth() / 2f)
				{
					this.result.Append(' ');
				}
			}
			this.result.Append(renderInfo.GetText());
			this.lastStart = startPoint;
			this.lastEnd = endPoint;
		}

		// Token: 0x06003602 RID: 13826 RVA: 0x0014F6EA File Offset: 0x0014E6EA
		public void RenderImage(ImageRenderInfo renderInfo)
		{
		}

		// Token: 0x04002449 RID: 9289
		private Vector lastStart;

		// Token: 0x0400244A RID: 9290
		private Vector lastEnd;

		// Token: 0x0400244B RID: 9291
		private StringBuilder result = new StringBuilder();
	}
}
