using System;
using System.Text;

namespace a.f
{
	// Token: 0x020000F6 RID: 246
	internal class l : n
	{
		// Token: 0x0600082C RID: 2092 RVA: 0x00025ABA File Offset: 0x00024ABA
		public static l a()
		{
			if (l.a == null)
			{
				l.a = new l();
			}
			return l.a;
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00025AD2 File Offset: 0x00024AD2
		public override int j9(string A_0, object A_1)
		{
			return 2;
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00025AD5 File Offset: 0x00024AD5
		public override object ka(string A_0, object A_1, Encoding A_2)
		{
			return A_1;
		}

		// Token: 0x04000564 RID: 1380
		private static l a;
	}
}
