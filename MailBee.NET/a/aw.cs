using System;
using System.Collections;
using System.Reflection;

namespace a
{
	// Token: 0x0200049A RID: 1178
	[DefaultMember("Item")]
	internal class aw : CollectionBase
	{
		// Token: 0x0600284D RID: 10317 RVA: 0x000BBED6 File Offset: 0x000BAED6
		public virtual bc a(int A_0)
		{
			return (bc)base.List[A_0];
		}

		// Token: 0x0600284E RID: 10318 RVA: 0x000BBEE9 File Offset: 0x000BAEE9
		public virtual void a(int A_0, bc A_1)
		{
			base.List[A_0] = A_1;
		}

		// Token: 0x0600284F RID: 10319 RVA: 0x000BBEF8 File Offset: 0x000BAEF8
		public virtual void a(bc A_0)
		{
			base.List.Add(A_0);
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x000BBF07 File Offset: 0x000BAF07
		public virtual void b(int A_0)
		{
			base.List.RemoveAt(A_0);
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x000BBF15 File Offset: 0x000BAF15
		public virtual void a()
		{
			base.List.Clear();
		}
	}
}
