using System;
using System.Collections;
using System.Reflection;

namespace a.b
{
	// Token: 0x0200033C RID: 828
	[DefaultMember("Item")]
	internal sealed class hn : ReadOnlyCollectionBase, b1
	{
		// Token: 0x06001E19 RID: 7705 RVA: 0x00081804 File Offset: 0x00080804
		public bf oq(int A_0)
		{
			return base.InnerList[A_0] as al;
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x00081818 File Offset: 0x00080818
		public bool or(string A_0)
		{
			using (IEnumerator enumerator = base.InnerList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((bf)enumerator.Current).be().Equals(A_0))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x00081880 File Offset: 0x00080880
		public void os(bf[] A_0, int A_1)
		{
			base.InnerList.CopyTo(A_0, A_1);
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x0008188F File Offset: 0x0008088F
		public void a(bf A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("item");
			}
			base.InnerList.Add(A_0);
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x000818AC File Offset: 0x000808AC
		public void a()
		{
			base.InnerList.Clear();
		}
	}
}
