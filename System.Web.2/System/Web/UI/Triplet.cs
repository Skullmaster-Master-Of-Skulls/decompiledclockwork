using System;

namespace System.Web.UI
{
	// Token: 0x0200031B RID: 795
	[Serializable]
	public sealed class Triplet
	{
		// Token: 0x06002512 RID: 9490 RVA: 0x000030B5 File Offset: 0x000012B5
		public Triplet()
		{
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x0007A71B File Offset: 0x0007891B
		public Triplet(object x, object y)
		{
			this.First = x;
			this.Second = y;
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x0007A731 File Offset: 0x00078931
		public Triplet(object x, object y, object z)
		{
			this.First = x;
			this.Second = y;
			this.Third = z;
		}

		// Token: 0x04001D68 RID: 7528
		public object First;

		// Token: 0x04001D69 RID: 7529
		public object Second;

		// Token: 0x04001D6A RID: 7530
		public object Third;
	}
}
