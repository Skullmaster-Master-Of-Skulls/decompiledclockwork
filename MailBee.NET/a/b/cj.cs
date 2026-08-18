using System;
using System.Collections;
using System.Reflection;

namespace a.b
{
	// Token: 0x02000258 RID: 600
	[DefaultMember("Item")]
	internal class cj : CollectionBase
	{
		// Token: 0x0600148C RID: 5260 RVA: 0x0005FBED File Offset: 0x0005EBED
		public long a(int A_0)
		{
			return (long)base.List[A_0];
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x0005FC00 File Offset: 0x0005EC00
		public void a(int A_0, long A_1)
		{
			base.List[A_0] = A_1;
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x0005FC14 File Offset: 0x0005EC14
		public void a(long A_0)
		{
			base.List.Add(A_0);
		}
	}
}
