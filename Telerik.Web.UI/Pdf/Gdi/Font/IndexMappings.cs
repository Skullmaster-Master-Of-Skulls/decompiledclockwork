using System;
using System.Collections;

namespace Telerik.Pdf.Gdi.Font
{
	// Token: 0x02001613 RID: 5651
	public class IndexMappings
	{
		// Token: 0x0600DC1B RID: 56347 RVA: 0x00301E0B File Offset: 0x0030000B
		public IndexMappings()
		{
			this.glyphToSubset = new SortedList();
			this.subsetToGlyph = new SortedList();
		}

		// Token: 0x1700435B RID: 17243
		// (get) Token: 0x0600DC1C RID: 56348 RVA: 0x00301E29 File Offset: 0x00300029
		public int Count
		{
			get
			{
				return this.glyphToSubset.Count;
			}
		}

		// Token: 0x0600DC1D RID: 56349 RVA: 0x00301E36 File Offset: 0x00300036
		public bool HasMapping(int glyphIndex)
		{
			return this.glyphToSubset.Contains(glyphIndex);
		}

		// Token: 0x0600DC1E RID: 56350 RVA: 0x00301E4C File Offset: 0x0030004C
		public int Map(int glyphIndex)
		{
			int num;
			if (this.glyphToSubset.Contains(glyphIndex))
			{
				num = (int)this.glyphToSubset[glyphIndex];
			}
			else
			{
				num = this.glyphToSubset.Count;
				this.glyphToSubset.Add(glyphIndex, num);
				this.subsetToGlyph.Add(num, glyphIndex);
			}
			return num;
		}

		// Token: 0x0600DC1F RID: 56351 RVA: 0x00301EC4 File Offset: 0x003000C4
		public void Add(params int[] glyphIndices)
		{
			foreach (int glyphIndex in glyphIndices)
			{
				this.Map(glyphIndex);
			}
		}

		// Token: 0x0600DC20 RID: 56352 RVA: 0x00301EED File Offset: 0x003000ED
		public int GetSubsetIndex(int glyphIndex)
		{
			if (this.glyphToSubset.Contains(glyphIndex))
			{
				return (int)this.glyphToSubset[glyphIndex];
			}
			return -1;
		}

		// Token: 0x0600DC21 RID: 56353 RVA: 0x00301F1A File Offset: 0x0030011A
		public int GetGlyphIndex(int subsetIndex)
		{
			if (this.subsetToGlyph.Contains(subsetIndex))
			{
				return (int)this.subsetToGlyph[subsetIndex];
			}
			return -1;
		}

		// Token: 0x1700435C RID: 17244
		// (get) Token: 0x0600DC22 RID: 56354 RVA: 0x00301F47 File Offset: 0x00300147
		public IList GlyphIndices
		{
			get
			{
				return new ArrayList(this.glyphToSubset.Keys);
			}
		}

		// Token: 0x1700435D RID: 17245
		// (get) Token: 0x0600DC23 RID: 56355 RVA: 0x00301F59 File Offset: 0x00300159
		public IList SubsetIndices
		{
			get
			{
				return new ArrayList(this.subsetToGlyph.Keys);
			}
		}

		// Token: 0x04003D86 RID: 15750
		private SortedList glyphToSubset;

		// Token: 0x04003D87 RID: 15751
		private SortedList subsetToGlyph;
	}
}
