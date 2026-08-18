using System;
using System.Collections;

namespace a.b
{
	// Token: 0x020003B4 RID: 948
	internal class f3
	{
		// Token: 0x06002244 RID: 8772 RVA: 0x0008C22C File Offset: 0x0008B22C
		public static int a(int A_0, object A_1)
		{
			int num = (A_1 != null) ? A_1.GetHashCode() : 0;
			if (A_0 != 0)
			{
				num += A_0 * 31;
			}
			return num;
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x0008C254 File Offset: 0x0008B254
		public static int a(int A_0, int A_1)
		{
			int num = A_1;
			if (A_0 != 0)
			{
				num += A_0 * 31;
			}
			return num;
		}

		// Token: 0x06002246 RID: 8774 RVA: 0x0008C270 File Offset: 0x0008B270
		public static int a(IEnumerable A_0)
		{
			int num = 1;
			if (A_0 == null)
			{
				throw new ArgumentNullException("enumerable");
			}
			foreach (object obj in A_0)
			{
				num = num * 31 + ((obj != null) ? obj.GetHashCode() : 0);
			}
			return num;
		}
	}
}
