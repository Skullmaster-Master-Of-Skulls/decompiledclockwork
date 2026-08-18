using System;
using System.Collections;

namespace a.b
{
	// Token: 0x020002BB RID: 699
	internal class fn
	{
		// Token: 0x0600184A RID: 6218 RVA: 0x0006EAB4 File Offset: 0x0006DAB4
		public fn()
		{
			this.a = new ArrayList();
			this.b = new Hashtable();
			this.c = new Hashtable();
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x0006EAE0 File Offset: 0x0006DAE0
		public void a(gr A_0, db A_1, string A_2)
		{
			if (!this.a.Contains(A_0))
			{
				ArrayList arrayList = (ArrayList)this.b[A_0];
				if (arrayList == null)
				{
					arrayList = new ArrayList();
					this.b[A_0] = arrayList;
				}
				ge ge = new ge(A_1, A_2);
				if (arrayList.Add(ge) >= 0)
				{
					ArrayList arrayList2 = (ArrayList)this.c[ge];
					if (arrayList2 == null)
					{
						arrayList2 = new ArrayList();
						this.c[ge] = arrayList2;
					}
					arrayList2.Add(A_0);
				}
			}
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x0006EB65 File Offset: 0x0006DB65
		public void b(gr A_0)
		{
			if (!this.a.Contains(A_0))
			{
				this.a(A_0);
				this.a.Add(A_0);
			}
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x0006EB8C File Offset: 0x0006DB8C
		public IEnumerator a(db A_0, string A_1)
		{
			ArrayList arrayList = new ArrayList(this.a);
			ArrayList arrayList2 = (ArrayList)this.c[new ge(A_0, A_1)];
			if (arrayList2 != null)
			{
				arrayList.AddRange(arrayList2);
			}
			return arrayList.GetEnumerator();
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x0006EBD0 File Offset: 0x0006DBD0
		private void a(gr A_0)
		{
			ArrayList arrayList = (ArrayList)this.b[A_0];
			if (arrayList != null)
			{
				this.b.Remove(A_0);
				foreach (object obj in arrayList)
				{
					this.a(A_0, (ge)obj);
				}
			}
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x0006EC21 File Offset: 0x0006DC21
		private void a(gr A_0, ge A_1)
		{
			ArrayList arrayList = (ArrayList)this.c[A_1];
			arrayList.Remove(A_0);
			if (arrayList.Count == 0)
			{
				this.c.Remove(A_1);
			}
		}

		// Token: 0x0400122A RID: 4650
		private ArrayList a;

		// Token: 0x0400122B RID: 4651
		private Hashtable b;

		// Token: 0x0400122C RID: 4652
		private Hashtable c;
	}
}
