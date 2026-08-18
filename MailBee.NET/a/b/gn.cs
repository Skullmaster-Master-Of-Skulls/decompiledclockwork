using System;
using System.Collections;

namespace a.b
{
	// Token: 0x0200032A RID: 810
	internal class gn
	{
		// Token: 0x06001D37 RID: 7479 RVA: 0x0007E805 File Offset: 0x0007D805
		private gn()
		{
		}

		// Token: 0x06001D38 RID: 7480 RVA: 0x0007E80D File Offset: 0x0007D80D
		public static dm a(Type A_0)
		{
			return gn.a(A_0.Name);
		}

		// Token: 0x06001D39 RID: 7481 RVA: 0x0007E81C File Offset: 0x0007D81C
		public static dm a(string A_0)
		{
			dm dm = null;
			if (gn.c == null && gn.c == null)
			{
				gn.c = gn.b.GetType().Name;
			}
			if (gn.c.Equals(gn.b.GetType().Name))
			{
				return gn.b;
			}
			if (gn.a.ContainsKey(A_0))
			{
				dm = (dm)gn.a[A_0];
			}
			else
			{
				try
				{
					dm = (Activator.CreateInstance(Type.GetType(gn.c)) as dm);
					dm.iu(A_0);
				}
				catch (Exception)
				{
					dm = gn.b;
				}
				gn.a[A_0] = dm;
			}
			return dm;
		}

		// Token: 0x0400137D RID: 4989
		private static Hashtable a = new Hashtable();

		// Token: 0x0400137E RID: 4990
		private static dm b = new d5();

		// Token: 0x0400137F RID: 4991
		private static string c = null;
	}
}
