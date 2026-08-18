using System;
using System.Collections;
using System.Collections.Generic;

namespace iTextSharp.text.pdf.parser
{
	// Token: 0x020001CA RID: 458
	public class TextRenderInfo
	{
		// Token: 0x060011E5 RID: 4581 RVA: 0x000674B0 File Offset: 0x000664B0
		internal TextRenderInfo(string text, GraphicsState gs, Matrix textMatrix, ICollection markedContentInfo)
		{
			this.text = text;
			this.textToUserSpaceTransformMatrix = textMatrix.Multiply(gs.ctm);
			this.gs = gs;
			this.markedContentInfos = new List<MarkedContentInfo>();
			foreach (object obj in markedContentInfo)
			{
				MarkedContentInfo item = (MarkedContentInfo)obj;
				this.markedContentInfos.Add(item);
			}
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x0006753C File Offset: 0x0006653C
		public string GetText()
		{
			return this.text;
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x00067544 File Offset: 0x00066544
		public bool HasMcid(int mcid)
		{
			foreach (MarkedContentInfo markedContentInfo in this.markedContentInfos)
			{
				if (markedContentInfo.HasMcid() && markedContentInfo.GetMcid() == mcid)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x000675A4 File Offset: 0x000665A4
		internal float GetUnscaledWidth()
		{
			return this.GetStringWidth(this.text);
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x000675B2 File Offset: 0x000665B2
		public LineSegment GetBaseline()
		{
			return this.GetUnscaledBaselineWithOffset(0f).TransformBy(this.textToUserSpaceTransformMatrix);
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x000675CC File Offset: 0x000665CC
		public LineSegment GetAscentLine()
		{
			float fontDescriptor = this.gs.GetFont().GetFontDescriptor(1, this.gs.GetFontSize());
			return this.GetUnscaledBaselineWithOffset(fontDescriptor).TransformBy(this.textToUserSpaceTransformMatrix);
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x00067608 File Offset: 0x00066608
		public LineSegment GetDescentLine()
		{
			float fontDescriptor = this.gs.GetFont().GetFontDescriptor(3, this.gs.GetFontSize());
			return this.GetUnscaledBaselineWithOffset(fontDescriptor).TransformBy(this.textToUserSpaceTransformMatrix);
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x00067644 File Offset: 0x00066644
		private LineSegment GetUnscaledBaselineWithOffset(float yOffset)
		{
			return new LineSegment(new Vector(0f, yOffset, 1f), new Vector(this.GetUnscaledWidth(), yOffset, 1f));
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x0006766C File Offset: 0x0006666C
		public DocumentFont GetFont()
		{
			return this.gs.GetFont();
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x0006767C File Offset: 0x0006667C
		public float GetSingleSpaceWidth()
		{
			LineSegment lineSegment = new LineSegment(new Vector(0f, 0f, 1f), new Vector(this.GetUnscaledFontSpaceWidth(), 0f, 1f));
			LineSegment lineSegment2 = lineSegment.TransformBy(this.textToUserSpaceTransformMatrix);
			return lineSegment2.GetLength();
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x000676CB File Offset: 0x000666CB
		public int GetTextRenderMode()
		{
			return this.gs.renderMode;
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x000676D8 File Offset: 0x000666D8
		private float GetUnscaledFontSpaceWidth()
		{
			char @char = ' ';
			if (this.gs.font.GetWidth((int)@char) == 0)
			{
				@char = '\u00a0';
			}
			return this.GetStringWidth(@char.ToString());
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x00067710 File Offset: 0x00066710
		private float GetStringWidth(string str)
		{
			DocumentFont font = this.gs.font;
			char[] array = str.ToCharArray();
			float num = 0f;
			for (int i = 0; i < array.Length; i++)
			{
				float num2 = (float)font.GetWidth((int)array[i]) / 1000f;
				float num3 = (array[i] == ' ') ? this.gs.wordSpacing : 0f;
				num += (num2 * this.gs.fontSize + this.gs.characterSpacing + num3) * this.gs.horizontalScaling;
			}
			return num;
		}

		// Token: 0x04000C95 RID: 3221
		private string text;

		// Token: 0x04000C96 RID: 3222
		private Matrix textToUserSpaceTransformMatrix;

		// Token: 0x04000C97 RID: 3223
		private GraphicsState gs;

		// Token: 0x04000C98 RID: 3224
		private ICollection<MarkedContentInfo> markedContentInfos;
	}
}
