using System;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x02000056 RID: 86
	internal struct XPathNodeRef
	{
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600030E RID: 782 RVA: 0x0000C340 File Offset: 0x0000A540
		public static XPathNodeRef Null
		{
			get
			{
				return default(XPathNodeRef);
			}
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000C356 File Offset: 0x0000A556
		public XPathNodeRef(XPathNode[] page, int idx)
		{
			this.page = page;
			this.idx = idx;
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0000C366 File Offset: 0x0000A566
		public bool IsNull
		{
			get
			{
				return this.page == null;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000C371 File Offset: 0x0000A571
		public XPathNode[] Page
		{
			get
			{
				return this.page;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000312 RID: 786 RVA: 0x0000C379 File Offset: 0x0000A579
		public int Index
		{
			get
			{
				return this.idx;
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000C381 File Offset: 0x0000A581
		public override int GetHashCode()
		{
			return XPathNodeHelper.GetLocation(this.page, this.idx);
		}

		// Token: 0x0400013F RID: 319
		private XPathNode[] page;

		// Token: 0x04000140 RID: 320
		private int idx;
	}
}
