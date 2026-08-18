using System;
using System.Text;

namespace a.a
{
	// Token: 0x020003E9 RID: 1001
	internal class j : at
	{
		// Token: 0x0600239E RID: 9118 RVA: 0x0009554C File Offset: 0x0009454C
		public j(byte[] A_0, Encoding A_1, string A_2, af A_3, string A_4, bool A_5, bool A_6) : base(A_0, A_1, A_2, A_3, A_4)
		{
			this.b = A_5;
			this.c = A_6;
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x0009556B File Offset: 0x0009456B
		public bool b()
		{
			return this.b;
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x00095573 File Offset: 0x00094573
		public bool c()
		{
			return this.c;
		}

		// Token: 0x04001786 RID: 6022
		private bool b;

		// Token: 0x04001787 RID: 6023
		private bool c;
	}
}
