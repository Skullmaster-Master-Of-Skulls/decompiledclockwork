using System;
using System.Collections;
using System.Reflection;

namespace a.b
{
	// Token: 0x02000346 RID: 838
	[DefaultMember("Item")]
	internal sealed class go : ReadOnlyCollectionBase, g1
	{
		// Token: 0x06001E4F RID: 7759 RVA: 0x00081DBE File Offset: 0x00080DBE
		public e6 m1(int A_0)
		{
			return base.InnerList[A_0] as am;
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x00081DD1 File Offset: 0x00080DD1
		public void m2(e6[] A_0, int A_1)
		{
			base.InnerList.CopyTo(A_0, A_1);
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x00081DE0 File Offset: 0x00080DE0
		public void a(e6 A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("item");
			}
			base.InnerList.Add(A_0);
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x00081DFD File Offset: 0x00080DFD
		public void a()
		{
			base.InnerList.Clear();
		}

		// Token: 0x06001E53 RID: 7763 RVA: 0x00081E0A File Offset: 0x00080E0A
		public override string ToString()
		{
			return i2.a(this);
		}
	}
}
