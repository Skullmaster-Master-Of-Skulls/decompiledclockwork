using System;
using System.Reflection;

namespace a.b
{
	// Token: 0x0200037B RID: 891
	[DefaultMember("Item")]
	internal sealed class a9 : b7, gu
	{
		// Token: 0x06002074 RID: 8308 RVA: 0x0008718F File Offset: 0x0008618F
		public ej dd(int A_0)
		{
			return base.InnerList[A_0] as ej;
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x000871A2 File Offset: 0x000861A2
		public bool de(ej A_0)
		{
			return this.df(A_0) >= 0;
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x000871B4 File Offset: 0x000861B4
		public int df(ej A_0)
		{
			if (A_0 != null)
			{
				int count = this.Count;
				for (int i = 0; i < count; i++)
				{
					if (A_0.Equals(base.InnerList[i]))
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x000871EE File Offset: 0x000861EE
		public void dg(ej[] A_0, int A_1)
		{
			base.InnerList.CopyTo(A_0, A_1);
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x000871FD File Offset: 0x000861FD
		public void a(ej A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("item");
			}
			base.InnerList.Add(A_0);
		}

		// Token: 0x06002079 RID: 8313 RVA: 0x0008721A File Offset: 0x0008621A
		public void a()
		{
			base.InnerList.Clear();
		}
	}
}
