using System;
using System.IO;

namespace a.b
{
	// Token: 0x020003A3 RID: 931
	internal class fy
	{
		// Token: 0x060021A6 RID: 8614 RVA: 0x00089FF9 File Offset: 0x00088FF9
		public static f a(string A_0, params f6[] A_1)
		{
			return fy.a(new bu(A_0), A_1);
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x0008A007 File Offset: 0x00089007
		public static f a(TextReader A_0, params f6[] A_1)
		{
			return fy.a(new bu(A_0), A_1);
		}

		// Token: 0x060021A8 RID: 8616 RVA: 0x0008A015 File Offset: 0x00089015
		public static f a(Stream A_0, params f6[] A_1)
		{
			return fy.a(new bu(A_0), A_1);
		}

		// Token: 0x060021A9 RID: 8617 RVA: 0x0008A024 File Offset: 0x00089024
		public static f a(da A_0, params f6[] A_1)
		{
			b5 b = new b5();
			ax ax = new ax(new f6[]
			{
				b
			});
			if (A_1 != null)
			{
				foreach (f6 f in A_1)
				{
					if (f != null)
					{
						ax.go(f);
					}
				}
			}
			ax.gq(A_0);
			return b.a();
		}
	}
}
