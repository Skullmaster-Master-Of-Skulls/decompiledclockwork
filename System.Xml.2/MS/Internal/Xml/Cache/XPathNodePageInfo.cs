using System;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x02000058 RID: 88
	internal sealed class XPathNodePageInfo
	{
		// Token: 0x0600032B RID: 811 RVA: 0x0000CBB3 File Offset: 0x0000ADB3
		public XPathNodePageInfo(XPathNode[] pagePrev, int pageNum)
		{
			this.pagePrev = pagePrev;
			this.pageNum = pageNum;
			this.nodeCount = 1;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000CBD0 File Offset: 0x0000ADD0
		public int PageNumber
		{
			get
			{
				return this.pageNum;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000CBD8 File Offset: 0x0000ADD8
		// (set) Token: 0x0600032E RID: 814 RVA: 0x0000CBE0 File Offset: 0x0000ADE0
		public int NodeCount
		{
			get
			{
				return this.nodeCount;
			}
			set
			{
				this.nodeCount = value;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000CBE9 File Offset: 0x0000ADE9
		public XPathNode[] PreviousPage
		{
			get
			{
				return this.pagePrev;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000CBF1 File Offset: 0x0000ADF1
		// (set) Token: 0x06000331 RID: 817 RVA: 0x0000CBF9 File Offset: 0x0000ADF9
		public XPathNode[] NextPage
		{
			get
			{
				return this.pageNext;
			}
			set
			{
				this.pageNext = value;
			}
		}

		// Token: 0x04000141 RID: 321
		private int pageNum;

		// Token: 0x04000142 RID: 322
		private int nodeCount;

		// Token: 0x04000143 RID: 323
		private XPathNode[] pagePrev;

		// Token: 0x04000144 RID: 324
		private XPathNode[] pageNext;
	}
}
