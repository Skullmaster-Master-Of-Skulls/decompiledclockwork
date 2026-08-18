using System;
using System.Reflection;

namespace a.b
{
	// Token: 0x02000377 RID: 887
	[DefaultMember("Item")]
	internal sealed class bc : b7, ee
	{
		// Token: 0x06002036 RID: 8246 RVA: 0x00086655 File Offset: 0x00085655
		public s dh(int A_0)
		{
			return base.InnerList[A_0] as s;
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x00086668 File Offset: 0x00085668
		public s di(string A_0)
		{
			if (A_0 != null)
			{
				foreach (object obj in base.InnerList)
				{
					s s = (s)obj;
					if (s.gu().Equals(A_0))
					{
						return s;
					}
				}
			}
			return null;
		}

		// Token: 0x06002038 RID: 8248 RVA: 0x000866D4 File Offset: 0x000856D4
		public void dj(s[] A_0, int A_1)
		{
			base.InnerList.CopyTo(A_0, A_1);
		}

		// Token: 0x06002039 RID: 8249 RVA: 0x000866E3 File Offset: 0x000856E3
		public void a(s A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("item");
			}
			base.InnerList.Add(A_0);
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x00086700 File Offset: 0x00085700
		public void a()
		{
			base.InnerList.Clear();
		}
	}
}
