using System;

namespace iTextSharp.text
{
	// Token: 0x020000D1 RID: 209
	public interface IDocListener : IElementListener
	{
		// Token: 0x06000738 RID: 1848
		void Open();

		// Token: 0x06000739 RID: 1849
		void Close();

		// Token: 0x0600073A RID: 1850
		bool NewPage();

		// Token: 0x0600073B RID: 1851
		bool SetPageSize(Rectangle pageSize);

		// Token: 0x0600073C RID: 1852
		bool SetMargins(float marginLeft, float marginRight, float marginTop, float marginBottom);

		// Token: 0x0600073D RID: 1853
		bool SetMarginMirroring(bool marginMirroring);

		// Token: 0x0600073E RID: 1854
		bool SetMarginMirroringTopBottom(bool marginMirroringTopBottom);

		// Token: 0x1700017E RID: 382
		// (set) Token: 0x0600073F RID: 1855
		int PageCount { set; }

		// Token: 0x06000740 RID: 1856
		void ResetPageCount();
	}
}
