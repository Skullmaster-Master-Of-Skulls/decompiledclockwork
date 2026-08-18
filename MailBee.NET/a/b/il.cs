using System;
using System.Collections;
using System.IO;

namespace a.b
{
	// Token: 0x02000301 RID: 769
	internal class il : af, cr
	{
		// Token: 0x06001B10 RID: 6928 RVA: 0x000765C8 File Offset: 0x000755C8
		public il(y A_0, IList A_1, hj A_2)
		{
			this.a = new ib(A_0);
			this.b = new ArrayList();
			this.d = A_2;
			foreach (object obj in A_1)
			{
				eg eg = (eg)obj;
				af[] array = eg.b();
				if (array.Length != 0)
				{
					eg.jm(this.a.a(array.Length));
					for (int i = 0; i < array.Length; i++)
					{
						this.b.Add(array[i]);
					}
				}
				else
				{
					eg.jm(-2);
				}
			}
			this.a.c();
			this.d.oo(this.b.Count);
			this.c = aw.a(A_0, this.b);
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x0007668F File Offset: 0x0007568F
		public int a()
		{
			return (this.c + 15) / 16;
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x0007669D File Offset: 0x0007569D
		public ib b()
		{
			return this.a;
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x000766A5 File Offset: 0x000756A5
		public int ap()
		{
			return this.c;
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x000766AD File Offset: 0x000756AD
		public void jm(int A_0)
		{
			this.d.c(A_0);
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x000766BC File Offset: 0x000756BC
		public void a3(Stream A_0)
		{
			foreach (object obj in this.b)
			{
				((af)obj).a3(A_0);
			}
		}

		// Token: 0x04001319 RID: 4889
		private ib a;

		// Token: 0x0400131A RID: 4890
		private IList b;

		// Token: 0x0400131B RID: 4891
		private int c;

		// Token: 0x0400131C RID: 4892
		private hj d;
	}
}
