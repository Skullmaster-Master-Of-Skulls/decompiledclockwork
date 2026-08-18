using System;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x0200010D RID: 269
	internal sealed class XPathNodePageInfo
	{
		// Token: 0x0600107B RID: 4219 RVA: 0x0004B3B3 File Offset: 0x0004A3B3
		public XPathNodePageInfo(XPathNode[] pagePrev, int pageNum)
		{
			this.pagePrev = pagePrev;
			this.pageNum = pageNum;
			this.nodeCount = 1;
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x0600107C RID: 4220 RVA: 0x0004B3D0 File Offset: 0x0004A3D0
		public int PageNumber
		{
			get
			{
				return this.pageNum;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x0004B3D8 File Offset: 0x0004A3D8
		// (set) Token: 0x0600107E RID: 4222 RVA: 0x0004B3E0 File Offset: 0x0004A3E0
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

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x0600107F RID: 4223 RVA: 0x0004B3E9 File Offset: 0x0004A3E9
		public XPathNode[] PreviousPage
		{
			get
			{
				return this.pagePrev;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001080 RID: 4224 RVA: 0x0004B3F1 File Offset: 0x0004A3F1
		// (set) Token: 0x06001081 RID: 4225 RVA: 0x0004B3F9 File Offset: 0x0004A3F9
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

		// Token: 0x04000AB3 RID: 2739
		private int pageNum;

		// Token: 0x04000AB4 RID: 2740
		private int nodeCount;

		// Token: 0x04000AB5 RID: 2741
		private XPathNode[] pagePrev;

		// Token: 0x04000AB6 RID: 2742
		private XPathNode[] pageNext;
	}
}
