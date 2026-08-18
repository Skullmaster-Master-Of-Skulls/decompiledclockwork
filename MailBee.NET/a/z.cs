using System;
using System.Collections;
using System.Reflection;

namespace a
{
	// Token: 0x020004C0 RID: 1216
	[DefaultMember("Item")]
	internal class z : CollectionBase
	{
		// Token: 0x0600297E RID: 10622 RVA: 0x000C0999 File Offset: 0x000BF999
		public void a(at A_0)
		{
			base.List.Add(A_0);
		}

		// Token: 0x0600297F RID: 10623 RVA: 0x000C09A8 File Offset: 0x000BF9A8
		public virtual at a(int A_0)
		{
			return (at)base.List[A_0];
		}

		// Token: 0x06002980 RID: 10624 RVA: 0x000C09BB File Offset: 0x000BF9BB
		public virtual void a(int A_0, at A_1)
		{
			base.List[A_0] = A_1;
		}
	}
}
