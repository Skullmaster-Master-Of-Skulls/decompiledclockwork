using System;
using System.Text;

namespace a.d
{
	// Token: 0x02000478 RID: 1144
	internal class j : at
	{
		// Token: 0x06002789 RID: 10121 RVA: 0x000B799D File Offset: 0x000B699D
		public j(byte[] A_0, Encoding A_1, string A_2, af A_3, string A_4, int A_5) : base(A_0, A_1, A_2, A_3, A_4)
		{
			this.a = A_5;
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x000B79B4 File Offset: 0x000B69B4
		public bool b()
		{
			return this.a / 100 == 4;
		}

		// Token: 0x04001B12 RID: 6930
		public readonly int a;
	}
}
