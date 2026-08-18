using System;
using MailBee;

namespace a
{
	// Token: 0x020004B6 RID: 1206
	internal class l : MailBeeException
	{
		// Token: 0x0600295D RID: 10589 RVA: 0x000C02B3 File Offset: 0x000BF2B3
		internal l(string A_0, int A_1, string A_2) : base(A_0, A_1)
		{
			this.a = A_2;
		}

		// Token: 0x0600295E RID: 10590 RVA: 0x000C02C4 File Offset: 0x000BF2C4
		internal l(int A_0, string A_1) : base(A_0)
		{
			this.a = A_1;
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x000C02D4 File Offset: 0x000BF2D4
		public string a()
		{
			return this.a;
		}

		// Token: 0x04001C1C RID: 7196
		private string a;
	}
}
