using System;

namespace Telerik.Pdf
{
	// Token: 0x020015FD RID: 5629
	internal class BfEntry
	{
		// Token: 0x0600DB79 RID: 56185 RVA: 0x003003E9 File Offset: 0x002FE5E9
		public BfEntry(int startIndex, int unicodeValue)
		{
			this.startIndex = startIndex;
			this.endIndex = startIndex;
			this.unicodeValue = unicodeValue;
		}

		// Token: 0x0600DB7A RID: 56186 RVA: 0x00300406 File Offset: 0x002FE606
		public void IncrementEndIndex()
		{
			this.endIndex++;
		}

		// Token: 0x1700432C RID: 17196
		// (get) Token: 0x0600DB7B RID: 56187 RVA: 0x00300416 File Offset: 0x002FE616
		public int StartGlyphIndex
		{
			get
			{
				return this.startIndex;
			}
		}

		// Token: 0x1700432D RID: 17197
		// (get) Token: 0x0600DB7C RID: 56188 RVA: 0x0030041E File Offset: 0x002FE61E
		public int EndGlyphIndex
		{
			get
			{
				return this.endIndex;
			}
		}

		// Token: 0x1700432E RID: 17198
		// (get) Token: 0x0600DB7D RID: 56189 RVA: 0x00300426 File Offset: 0x002FE626
		public int UnicodeValue
		{
			get
			{
				return this.unicodeValue;
			}
		}

		// Token: 0x1700432F RID: 17199
		// (get) Token: 0x0600DB7E RID: 56190 RVA: 0x0030042E File Offset: 0x002FE62E
		public bool IsRange
		{
			get
			{
				return this.startIndex != this.endIndex;
			}
		}

		// Token: 0x17004330 RID: 17200
		// (get) Token: 0x0600DB7F RID: 56191 RVA: 0x00300441 File Offset: 0x002FE641
		public bool IsChar
		{
			get
			{
				return !this.IsRange;
			}
		}

		// Token: 0x04003D60 RID: 15712
		private int startIndex;

		// Token: 0x04003D61 RID: 15713
		private int endIndex;

		// Token: 0x04003D62 RID: 15714
		private int unicodeValue;
	}
}
