using System;
using System.Collections;
using System.Text;

namespace a.b
{
	// Token: 0x0200033D RID: 829
	internal class i3
	{
		// Token: 0x06001E1F RID: 7711 RVA: 0x000818CC File Offset: 0x000808CC
		public int a()
		{
			return this.a.Count;
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x000818D9 File Offset: 0x000808D9
		public eb b()
		{
			return (eb)this.a.Peek();
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x000818EB File Offset: 0x000808EB
		public bool a(eb A_0)
		{
			return this.b() == A_0;
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x000818F6 File Offset: 0x000808F6
		public bool b(eb A_0)
		{
			return this.a.Contains(A_0);
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x00081909 File Offset: 0x00080909
		public void c(eb A_0)
		{
			this.a.Push(A_0);
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x0008191C File Offset: 0x0008091C
		public void c()
		{
			this.a.Pop();
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x0008192C File Offset: 0x0008092C
		public override string ToString()
		{
			if (this.a.Count == 0)
			{
				return base.ToString();
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (object obj in this.a)
			{
				if (!flag)
				{
					stringBuilder.Insert(0, " > ");
				}
				stringBuilder.Insert(0, obj.ToString());
				flag = false;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040013BA RID: 5050
		private readonly Stack a = new Stack();
	}
}
