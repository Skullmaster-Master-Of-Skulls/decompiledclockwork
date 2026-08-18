using System;
using System.Reflection;

namespace a.b
{
	// Token: 0x0200037E RID: 894
	[DefaultMember("Item")]
	internal sealed class ao : b7, bi
	{
		// Token: 0x06002089 RID: 8329 RVA: 0x00087334 File Offset: 0x00086334
		public i1 bl(int A_0)
		{
			return base.InnerList[A_0] as i1;
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x00087347 File Offset: 0x00086347
		public void bm(i1[] A_0, int A_1)
		{
			base.InnerList.CopyTo(A_0, A_1);
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x00087356 File Offset: 0x00086356
		public void a(i1 A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("item");
			}
			base.InnerList.Add(A_0);
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x00087373 File Offset: 0x00086373
		public void a()
		{
			base.InnerList.Clear();
		}
	}
}
