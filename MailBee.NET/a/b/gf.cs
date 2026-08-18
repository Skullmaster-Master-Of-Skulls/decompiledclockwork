using System;
using System.Collections;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x02000354 RID: 852
	internal abstract class gf : aj
	{
		// Token: 0x06001EF9 RID: 7929 RVA: 0x00084EC4 File Offset: 0x00083EC4
		protected gf(params g5[] A_0)
		{
			if (A_0 != null)
			{
				foreach (g5 a_ in A_0)
				{
					this.lg(a_);
				}
			}
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x00084F00 File Offset: 0x00083F00
		public void lg(g5 A_0)
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

		// Token: 0x06001EFB RID: 7931 RVA: 0x00084F40 File Offset: 0x00083F40
		public void lh(g5 A_0)
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

		// Token: 0x06001EFC RID: 7932 RVA: 0x00084F91 File Offset: 0x00083F91
		public void li(f A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("rtfDocument");
			}
			this.op(A_0);
		}

		// Token: 0x06001EFD RID: 7933
		protected abstract void op(f A_0);

		// Token: 0x06001EFE RID: 7934 RVA: 0x00084FA8 File Offset: 0x00083FA8
		protected void b()
		{
			if (this.b != null)
			{
				foreach (object obj in this.b)
				{
					((g5)obj).jn(this.a);
				}
			}
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x0008500C File Offset: 0x0008400C
		protected void a(string A_0)
		{
			if (this.b != null)
			{
				foreach (object obj in this.b)
				{
					((g5)obj).jo(this.a, A_0);
				}
			}
		}

		// Token: 0x06001F00 RID: 7936 RVA: 0x00085074 File Offset: 0x00084074
		protected void a(RtfVisualSpecialCharKind A_0)
		{
			if (this.b != null)
			{
				foreach (object obj in this.b)
				{
					((g5)obj).jp(this.a, A_0);
				}
			}
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x000850DC File Offset: 0x000840DC
		protected void a(RtfVisualBreakKind A_0)
		{
			if (this.b != null)
			{
				foreach (object obj in this.b)
				{
					((g5)obj).jq(this.a, A_0);
				}
			}
		}

		// Token: 0x06001F02 RID: 7938 RVA: 0x00085144 File Offset: 0x00084144
		protected void a(de A_0, int A_1, int A_2, int A_3, int A_4, int A_5, int A_6, string A_7)
		{
			if (this.b != null)
			{
				foreach (object obj in this.b)
				{
					((g5)obj).jr(this.a, A_0, A_1, A_2, A_3, A_4, A_5, A_6, A_7);
				}
			}
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x000851B8 File Offset: 0x000841B8
		protected void a()
		{
			if (this.b != null)
			{
				foreach (object obj in this.b)
				{
					((g5)obj).js(this.a);
				}
			}
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x0008521C File Offset: 0x0008421C
		protected fs c()
		{
			return this.a;
		}

		// Token: 0x0400141F RID: 5151
		private readonly fs a = new fs();

		// Token: 0x04001420 RID: 5152
		private ArrayList b;
	}
}
