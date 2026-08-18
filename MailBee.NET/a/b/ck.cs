using System;
using System.Collections;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x0200039B RID: 923
	internal abstract class ck : bd
	{
		// Token: 0x06002132 RID: 8498 RVA: 0x00088FD7 File Offset: 0x00087FD7
		protected ck()
		{
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x00088FE0 File Offset: 0x00087FE0
		protected ck(params f6[] A_0)
		{
			if (A_0 != null)
			{
				foreach (f6 a_ in A_0)
				{
					this.go(a_);
				}
			}
		}

		// Token: 0x06002134 RID: 8500 RVA: 0x00089011 File Offset: 0x00088011
		public bool gm()
		{
			return this.a;
		}

		// Token: 0x06002135 RID: 8501 RVA: 0x00089019 File Offset: 0x00088019
		public void gn(bool A_0)
		{
			this.a = A_0;
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x00089022 File Offset: 0x00088022
		public void go(f6 A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("listener");
			}
			if (this.b == null)
			{
				this.b = new ArrayList();
			}
			if (!this.b.Contains(A_0))
			{
				this.b.Add(A_0);
			}
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x00089060 File Offset: 0x00088060
		public void gp(f6 A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("listener");
			}
			if (this.b != null)
			{
				if (this.b.Contains(A_0))
				{
					this.b.Remove(A_0);
				}
				if (this.b.Count == 0)
				{
					this.b = null;
				}
			}
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x000890B1 File Offset: 0x000880B1
		public void gq(da A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("rtfTextSource");
			}
			this.bw(A_0);
		}

		// Token: 0x06002139 RID: 8505
		protected abstract void bw(da A_0);

		// Token: 0x0600213A RID: 8506 RVA: 0x000890C8 File Offset: 0x000880C8
		protected void d()
		{
			if (this.b != null)
			{
				foreach (object obj in this.b)
				{
					((f6)obj).bn();
				}
			}
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x00089128 File Offset: 0x00088128
		protected void f()
		{
			if (this.b != null)
			{
				foreach (object obj in this.b)
				{
					((f6)obj).bo();
				}
			}
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x00089188 File Offset: 0x00088188
		protected void b(c9 A_0)
		{
			if (this.b != null)
			{
				foreach (object obj in this.b)
				{
					((f6)obj).bp(A_0);
				}
			}
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x000891E8 File Offset: 0x000881E8
		protected void a(bp A_0)
		{
			if (this.b != null)
			{
				foreach (object obj in this.b)
				{
					((f6)obj).bq(A_0);
				}
			}
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x00089248 File Offset: 0x00088248
		protected void g()
		{
			if (this.b != null)
			{
				foreach (object obj in this.b)
				{
					((f6)obj).br();
				}
			}
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x000892A8 File Offset: 0x000882A8
		protected void e()
		{
			if (this.b != null)
			{
				foreach (object obj in this.b)
				{
					((f6)obj).bs();
				}
			}
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x00089308 File Offset: 0x00088308
		protected void a(RtfException A_0)
		{
			if (this.b != null)
			{
				foreach (object obj in this.b)
				{
					((f6)obj).bt(A_0);
				}
			}
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x00089368 File Offset: 0x00088368
		protected void c()
		{
			if (this.b != null)
			{
				foreach (object obj in this.b)
				{
					((f6)obj).bu();
				}
			}
		}

		// Token: 0x040014DC RID: 5340
		private bool a;

		// Token: 0x040014DD RID: 5341
		private ArrayList b;
	}
}
