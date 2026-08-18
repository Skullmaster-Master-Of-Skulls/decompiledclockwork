using System;

namespace a.b
{
	// Token: 0x020003B5 RID: 949
	internal class bz
	{
		// Token: 0x06002248 RID: 8776 RVA: 0x0008C2E4 File Offset: 0x0008B2E4
		public bz(object A_0)
		{
			this.a(A_0);
		}

		// Token: 0x06002249 RID: 8777 RVA: 0x0008C2FA File Offset: 0x0008B2FA
		public object a()
		{
			if (this.a)
			{
				return null;
			}
			return this.b;
		}

		// Token: 0x0600224A RID: 8778 RVA: 0x0008C311 File Offset: 0x0008B311
		public void a(object A_0)
		{
			if (A_0 == null)
			{
				this.a = true;
				return;
			}
			this.a = false;
			this.b = (int)A_0;
		}

		// Token: 0x0600224B RID: 8779 RVA: 0x0008C331 File Offset: 0x0008B331
		public int b()
		{
			return this.b;
		}

		// Token: 0x0400168A RID: 5770
		private bool a = true;

		// Token: 0x0400168B RID: 5771
		private int b;
	}
}
