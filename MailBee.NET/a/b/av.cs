using System;

namespace a.b
{
	// Token: 0x020002BD RID: 701
	internal class av : EventArgs
	{
		// Token: 0x06001855 RID: 6229 RVA: 0x0006EC93 File Offset: 0x0006DC93
		public av(cm A_0, db A_1, string A_2, int A_3)
		{
			this.d = A_0;
			this.c = A_1;
			this.a = A_2;
			this.b = A_3;
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x0006ECB8 File Offset: 0x0006DCB8
		public virtual int d()
		{
			return this.b;
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x0006ECC0 File Offset: 0x0006DCC0
		public virtual string b()
		{
			return this.a;
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x0006ECC8 File Offset: 0x0006DCC8
		public virtual db a()
		{
			return this.c;
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x0006ECD0 File Offset: 0x0006DCD0
		public virtual cm c()
		{
			return this.d;
		}

		// Token: 0x04001231 RID: 4657
		private string a;

		// Token: 0x04001232 RID: 4658
		private int b;

		// Token: 0x04001233 RID: 4659
		private db c;

		// Token: 0x04001234 RID: 4660
		private cm d;
	}
}
