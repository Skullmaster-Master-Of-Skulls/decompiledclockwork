using System;

namespace iTextSharp.text.pdf.parser
{
	// Token: 0x02000590 RID: 1424
	public class GraphicsState
	{
		// Token: 0x060030AC RID: 12460 RVA: 0x0012CB3C File Offset: 0x0012BB3C
		public GraphicsState()
		{
			this.ctm = new Matrix();
			this.characterSpacing = 0f;
			this.wordSpacing = 0f;
			this.horizontalScaling = 1f;
			this.leading = 0f;
			this.font = null;
			this.fontSize = 0f;
			this.renderMode = 0;
			this.rise = 0f;
			this.knockout = true;
		}

		// Token: 0x060030AD RID: 12461 RVA: 0x0012CBB4 File Offset: 0x0012BBB4
		public GraphicsState(GraphicsState source)
		{
			this.ctm = source.ctm;
			this.characterSpacing = source.characterSpacing;
			this.wordSpacing = source.wordSpacing;
			this.horizontalScaling = source.horizontalScaling;
			this.leading = source.leading;
			this.font = source.font;
			this.fontSize = source.fontSize;
			this.renderMode = source.renderMode;
			this.rise = source.rise;
			this.knockout = source.knockout;
		}

		// Token: 0x060030AE RID: 12462 RVA: 0x0012CC3F File Offset: 0x0012BC3F
		public Matrix GetCtm()
		{
			return this.ctm;
		}

		// Token: 0x060030AF RID: 12463 RVA: 0x0012CC47 File Offset: 0x0012BC47
		public float GetCharacterSpacing()
		{
			return this.characterSpacing;
		}

		// Token: 0x060030B0 RID: 12464 RVA: 0x0012CC4F File Offset: 0x0012BC4F
		public float GetWordSpacing()
		{
			return this.wordSpacing;
		}

		// Token: 0x060030B1 RID: 12465 RVA: 0x0012CC57 File Offset: 0x0012BC57
		public float GetHorizontalScaling()
		{
			return this.horizontalScaling;
		}

		// Token: 0x060030B2 RID: 12466 RVA: 0x0012CC5F File Offset: 0x0012BC5F
		public float GetLeading()
		{
			return this.leading;
		}

		// Token: 0x060030B3 RID: 12467 RVA: 0x0012CC67 File Offset: 0x0012BC67
		public CMapAwareDocumentFont GetFont()
		{
			return this.font;
		}

		// Token: 0x060030B4 RID: 12468 RVA: 0x0012CC6F File Offset: 0x0012BC6F
		public float GetFontSize()
		{
			return this.fontSize;
		}

		// Token: 0x060030B5 RID: 12469 RVA: 0x0012CC77 File Offset: 0x0012BC77
		public int GetRenderMode()
		{
			return this.renderMode;
		}

		// Token: 0x060030B6 RID: 12470 RVA: 0x0012CC7F File Offset: 0x0012BC7F
		public float GetRise()
		{
			return this.rise;
		}

		// Token: 0x060030B7 RID: 12471 RVA: 0x0012CC87 File Offset: 0x0012BC87
		public bool IsKnockout()
		{
			return this.knockout;
		}

		// Token: 0x0400216B RID: 8555
		internal Matrix ctm;

		// Token: 0x0400216C RID: 8556
		internal float characterSpacing;

		// Token: 0x0400216D RID: 8557
		internal float wordSpacing;

		// Token: 0x0400216E RID: 8558
		internal float horizontalScaling;

		// Token: 0x0400216F RID: 8559
		internal float leading;

		// Token: 0x04002170 RID: 8560
		internal CMapAwareDocumentFont font;

		// Token: 0x04002171 RID: 8561
		internal float fontSize;

		// Token: 0x04002172 RID: 8562
		internal int renderMode;

		// Token: 0x04002173 RID: 8563
		internal float rise;

		// Token: 0x04002174 RID: 8564
		internal bool knockout;
	}
}
