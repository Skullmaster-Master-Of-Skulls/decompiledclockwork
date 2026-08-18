using System;
using System.Collections;

namespace a
{
	// Token: 0x0200049C RID: 1180
	internal class ar : IComparer
	{
		// Token: 0x06002855 RID: 10325 RVA: 0x000BC062 File Offset: 0x000BB062
		private ar()
		{
		}

		// Token: 0x06002856 RID: 10326 RVA: 0x000BC06A File Offset: 0x000BB06A
		int IComparer.a(object A_0, object A_1)
		{
			return ((ax)A_0).get_Priority() - ((ax)A_1).get_Priority();
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x000BC083 File Offset: 0x000BB083
		public static ar a()
		{
			if (ar.c == null)
			{
				ar.c = new ar();
			}
			return ar.c;
		}

		// Token: 0x04001B8F RID: 7055
		public const int a = 0;

		// Token: 0x04001B90 RID: 7056
		public const int b = 999;

		// Token: 0x04001B91 RID: 7057
		private static ar c;
	}
}
