using System;

namespace a.b
{
	// Token: 0x020003B7 RID: 951
	internal class ht
	{
		// Token: 0x06002251 RID: 8785 RVA: 0x0008C399 File Offset: 0x0008B399
		public ht(object A_0)
		{
			this.a(A_0);
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x0008C3AF File Offset: 0x0008B3AF
		public object a()
		{
			if (this.a)
			{
				return null;
			}
			return this.b;
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x0008C3C6 File Offset: 0x0008B3C6
		public void a(object A_0)
		{
			if (A_0 == null)
			{
				this.a = true;
				return;
			}
			this.a = false;
			this.b = (DateTime)A_0;
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x0008C3E6 File Offset: 0x0008B3E6
		public DateTime b()
		{
			return this.b;
		}

		// Token: 0x0400168E RID: 5774
		private bool a = true;

		// Token: 0x0400168F RID: 5775
		private DateTime b;
	}
}
