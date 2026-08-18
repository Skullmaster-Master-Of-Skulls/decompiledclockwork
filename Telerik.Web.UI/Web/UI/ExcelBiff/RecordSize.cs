using System;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AD6 RID: 2774
	internal sealed class RecordSize
	{
		// Token: 0x17002250 RID: 8784
		// (get) Token: 0x060068A2 RID: 26786 RVA: 0x00188327 File Offset: 0x00186527
		// (set) Token: 0x060068A3 RID: 26787 RVA: 0x0018832F File Offset: 0x0018652F
		public int CharIndex
		{
			get
			{
				return this.charIndex;
			}
			set
			{
				this.charIndex = value;
			}
		}

		// Token: 0x17002251 RID: 8785
		// (get) Token: 0x060068A4 RID: 26788 RVA: 0x00188338 File Offset: 0x00186538
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x17002252 RID: 8786
		// (get) Token: 0x060068A5 RID: 26789 RVA: 0x00188340 File Offset: 0x00186540
		public int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x060068A6 RID: 26790 RVA: 0x00188348 File Offset: 0x00186548
		public RecordSize(int index, int length, int charIndex)
		{
			this.index = index;
			this.length = length;
			this.charIndex = charIndex;
		}

		// Token: 0x04001BC2 RID: 7106
		private int charIndex;

		// Token: 0x04001BC3 RID: 7107
		private int index;

		// Token: 0x04001BC4 RID: 7108
		private int length;
	}
}
