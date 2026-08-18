using System;
using System.Collections.Generic;
using System.IO;

namespace a.b
{
	// Token: 0x020002E9 RID: 745
	internal abstract class f2 : cr
	{
		// Token: 0x06001A52 RID: 6738 RVA: 0x0007409C File Offset: 0x0007309C
		public f2(c3 A_0)
		{
			this.a = A_0;
			this.b = new List<ed>();
			this.b(new hj());
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x000740C1 File Offset: 0x000730C1
		public f2(c3 A_0, List<ed> A_1)
		{
			this.a = A_0;
			this.b = A_1;
			this.a((g8)this.b[0]);
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x000740EE File Offset: 0x000730EE
		public void b(ed A_0)
		{
			this.b.Add(A_0);
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x000740FC File Offset: 0x000730FC
		public void a(ed A_0)
		{
			this.b.Remove(A_0);
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x0007410B File Offset: 0x0007310B
		public hj b()
		{
			return (hj)this.b[0];
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x00074120 File Offset: 0x00073120
		protected void a(g8 A_0)
		{
			try
			{
				int num = A_0.j();
				if (ed.a(num))
				{
					Stack<ed> stack = new Stack<ed>();
					stack.Push(this.b[num]);
					while (stack.Count != 0)
					{
						ed ed = stack.Pop();
						A_0.on(ed);
						if (ed.lj())
						{
							this.a((g8)ed);
						}
						num = ed.c();
						if (ed.a(num))
						{
							stack.Push(this.b[num]);
						}
						num = ed.e();
						if (ed.a(num))
						{
							stack.Push(this.b[num]);
						}
					}
				}
			}
			catch (IOException ex)
			{
				throw ex;
			}
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x000741DC File Offset: 0x000731DC
		public virtual int c()
		{
			return this.a.g();
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x000741E9 File Offset: 0x000731E9
		public virtual void jm(int A_0)
		{
			this.a.g(A_0);
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x000741F7 File Offset: 0x000731F7
		public virtual int ap()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040012CE RID: 4814
		protected c3 a;

		// Token: 0x040012CF RID: 4815
		protected List<ed> b;
	}
}
