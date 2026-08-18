using System;
using System.Reflection;

namespace a.b
{
	// Token: 0x02000396 RID: 918
	[DefaultMember("Item")]
	internal sealed class e0 : b7, a1
	{
		// Token: 0x06002103 RID: 8451 RVA: 0x00087EE0 File Offset: 0x00086EE0
		public f8 kb(int A_0)
		{
			return base.InnerList[A_0] as f8;
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x00087EF3 File Offset: 0x00086EF3
		public void kc(f8[] A_0, int A_1)
		{
			base.InnerList.CopyTo(A_0, A_1);
		}

		// Token: 0x06002105 RID: 8453 RVA: 0x00087F02 File Offset: 0x00086F02
		public void a(f8 A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("item");
			}
			base.InnerList.Add(A_0);
		}

		// Token: 0x06002106 RID: 8454 RVA: 0x00087F1F File Offset: 0x00086F1F
		public void a()
		{
			base.InnerList.Clear();
		}
	}
}
