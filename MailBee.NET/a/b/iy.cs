using System;

namespace a.b
{
	// Token: 0x0200034E RID: 846
	internal class iy : cy
	{
		// Token: 0x06001EC3 RID: 7875 RVA: 0x00082B93 File Offset: 0x00081B93
		public iy(ie A_0)
		{
			this.a = A_0;
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x00082BA2 File Offset: 0x00081BA2
		public void pr(c9 A_0)
		{
			if (A_0 != null)
			{
				this.dz(A_0);
			}
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x00082BAE File Offset: 0x00081BAE
		protected virtual void dz(c9 A_0)
		{
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x00082BB0 File Offset: 0x00081BB0
		public void ps(f A_0)
		{
			if (A_0 != null)
			{
				if (this.a == ie.b)
				{
					this.c(A_0);
				}
				this.da(A_0);
				if (this.a == ie.c)
				{
					this.c(A_0);
				}
			}
		}

		// Token: 0x06001EC7 RID: 7879 RVA: 0x00082BDC File Offset: 0x00081BDC
		protected virtual void da(f A_0)
		{
		}

		// Token: 0x06001EC8 RID: 7880 RVA: 0x00082BE0 File Offset: 0x00081BE0
		protected void c(f A_0)
		{
			foreach (object obj in A_0.nt())
			{
				((f8)obj).q(this);
			}
		}

		// Token: 0x06001EC9 RID: 7881 RVA: 0x00082C38 File Offset: 0x00081C38
		public void pt(bp A_0)
		{
			if (A_0 != null)
			{
				this.ft(A_0);
			}
		}

		// Token: 0x06001ECA RID: 7882 RVA: 0x00082C44 File Offset: 0x00081C44
		protected virtual void ft(bp A_0)
		{
		}

		// Token: 0x04001404 RID: 5124
		private readonly ie a;
	}
}
