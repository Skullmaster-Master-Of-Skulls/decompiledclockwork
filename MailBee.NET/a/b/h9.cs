using System;
using System.Reflection;

namespace a.b
{
	// Token: 0x02000372 RID: 882
	[DefaultMember("Item")]
	internal sealed class h9 : b7, j
	{
		// Token: 0x06001FE3 RID: 8163 RVA: 0x00085F91 File Offset: 0x00084F91
		public gb o7(int A_0)
		{
			return base.InnerList[A_0] as gb;
		}

		// Token: 0x06001FE4 RID: 8164 RVA: 0x00085FA4 File Offset: 0x00084FA4
		public void o8(gb[] A_0, int A_1)
		{
			base.InnerList.CopyTo(A_0, A_1);
		}

		// Token: 0x06001FE5 RID: 8165 RVA: 0x00085FB3 File Offset: 0x00084FB3
		public void a(gb A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("item");
			}
			base.InnerList.Add(A_0);
		}

		// Token: 0x06001FE6 RID: 8166 RVA: 0x00085FD0 File Offset: 0x00084FD0
		public void a()
		{
			base.InnerList.Clear();
		}
	}
}
