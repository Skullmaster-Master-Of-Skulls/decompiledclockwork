using System;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x0200010B RID: 267
	internal struct XPathNodeRef
	{
		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x0004AB40 File Offset: 0x00049B40
		public static XPathNodeRef Null
		{
			get
			{
				return default(XPathNodeRef);
			}
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x0004AB56 File Offset: 0x00049B56
		public XPathNodeRef(XPathNode[] page, int idx)
		{
			this.page = page;
			this.idx = idx;
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06001060 RID: 4192 RVA: 0x0004AB66 File Offset: 0x00049B66
		public bool IsNull
		{
			get
			{
				return this.page == null;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x0004AB71 File Offset: 0x00049B71
		public XPathNode[] Page
		{
			get
			{
				return this.page;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x0004AB79 File Offset: 0x00049B79
		public int Index
		{
			get
			{
				return this.idx;
			}
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0004AB81 File Offset: 0x00049B81
		public override int GetHashCode()
		{
			return XPathNodeHelper.GetLocation(this.page, this.idx);
		}

		// Token: 0x04000AB1 RID: 2737
		private XPathNode[] page;

		// Token: 0x04000AB2 RID: 2738
		private int idx;
	}
}
