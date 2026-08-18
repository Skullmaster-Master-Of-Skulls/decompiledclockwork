using System;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x020009DE RID: 2526
	internal class CodeWordsBlockInfo
	{
		// Token: 0x060060B0 RID: 24752 RVA: 0x0012C7A0 File Offset: 0x0012A9A0
		public CodeWordsBlockInfo(int codeWordsPerBlock, int firstBlockCount, int firstDataCodeWords, int secondBlockCount, int secondBlockCodeWords)
		{
			this.CodeWordsPerBlock = codeWordsPerBlock;
			this.FirstBlockCount = firstBlockCount;
			this.FirstDataCodeWords = firstDataCodeWords;
			this.SecondBlockCount = secondBlockCount;
			this.SecondBlockCodeWords = secondBlockCodeWords;
		}

		// Token: 0x17001FCC RID: 8140
		// (get) Token: 0x060060B1 RID: 24753 RVA: 0x0012C7CD File Offset: 0x0012A9CD
		// (set) Token: 0x060060B2 RID: 24754 RVA: 0x0012C7D5 File Offset: 0x0012A9D5
		public int CodeWordsPerBlock
		{
			get
			{
				return this.codeWordsPerBlockL;
			}
			set
			{
				this.codeWordsPerBlockL = value;
			}
		}

		// Token: 0x17001FCD RID: 8141
		// (get) Token: 0x060060B3 RID: 24755 RVA: 0x0012C7DE File Offset: 0x0012A9DE
		// (set) Token: 0x060060B4 RID: 24756 RVA: 0x0012C7E6 File Offset: 0x0012A9E6
		public int FirstBlockCount
		{
			get
			{
				return this.firstBlockCountL;
			}
			set
			{
				this.firstBlockCountL = value;
			}
		}

		// Token: 0x17001FCE RID: 8142
		// (get) Token: 0x060060B5 RID: 24757 RVA: 0x0012C7EF File Offset: 0x0012A9EF
		// (set) Token: 0x060060B6 RID: 24758 RVA: 0x0012C7F7 File Offset: 0x0012A9F7
		public int FirstDataCodeWords
		{
			get
			{
				return this.firstDataCodeWordsL;
			}
			set
			{
				this.firstDataCodeWordsL = value;
			}
		}

		// Token: 0x17001FCF RID: 8143
		// (get) Token: 0x060060B7 RID: 24759 RVA: 0x0012C800 File Offset: 0x0012AA00
		// (set) Token: 0x060060B8 RID: 24760 RVA: 0x0012C808 File Offset: 0x0012AA08
		public int SecondBlockCount
		{
			get
			{
				return this.secondBlockCountL;
			}
			set
			{
				this.secondBlockCountL = value;
			}
		}

		// Token: 0x17001FD0 RID: 8144
		// (get) Token: 0x060060B9 RID: 24761 RVA: 0x0012C811 File Offset: 0x0012AA11
		// (set) Token: 0x060060BA RID: 24762 RVA: 0x0012C819 File Offset: 0x0012AA19
		public int SecondBlockCodeWords
		{
			get
			{
				return this.secondBlockCodeWordsL;
			}
			set
			{
				this.secondBlockCodeWordsL = value;
			}
		}

		// Token: 0x04001786 RID: 6022
		private int codeWordsPerBlockL;

		// Token: 0x04001787 RID: 6023
		private int firstBlockCountL;

		// Token: 0x04001788 RID: 6024
		private int firstDataCodeWordsL;

		// Token: 0x04001789 RID: 6025
		private int secondBlockCountL;

		// Token: 0x0400178A RID: 6026
		private int secondBlockCodeWordsL;
	}
}
