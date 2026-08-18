using System;
using System.Text.RegularExpressions;
using MailBee.AddressCheck;

namespace a.n
{
	// Token: 0x02000206 RID: 518
	internal class a
	{
		// Token: 0x060010F3 RID: 4339 RVA: 0x000476B0 File Offset: 0x000466B0
		public a(AddressValidationLevel A_0, string A_1, int A_2, Regex A_3)
		{
			this.a = A_0;
			this.b = A_1;
			this.c = A_2;
			this.d = A_3;
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x000476D5 File Offset: 0x000466D5
		public AddressValidationLevel c()
		{
			return this.a;
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x000476DD File Offset: 0x000466DD
		public string a()
		{
			return this.b;
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x000476E5 File Offset: 0x000466E5
		public int d()
		{
			return this.c;
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x000476ED File Offset: 0x000466ED
		public Regex b()
		{
			return this.d;
		}

		// Token: 0x04000E5F RID: 3679
		private AddressValidationLevel a;

		// Token: 0x04000E60 RID: 3680
		private string b;

		// Token: 0x04000E61 RID: 3681
		private int c;

		// Token: 0x04000E62 RID: 3682
		private Regex d;
	}
}
