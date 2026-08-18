using System;
using System.Collections;
using System.Reflection;

namespace a
{
	// Token: 0x020004BF RID: 1215
	[DefaultMember("Item")]
	internal class y : CollectionBase
	{
		// Token: 0x06002976 RID: 10614 RVA: 0x000C088A File Offset: 0x000BF88A
		public void a(bf A_0)
		{
			base.List.Add(A_0);
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x000C0899 File Offset: 0x000BF899
		public virtual bf b(int A_0)
		{
			return (bf)base.List[A_0];
		}

		// Token: 0x06002978 RID: 10616 RVA: 0x000C08AC File Offset: 0x000BF8AC
		public virtual void a(int A_0, bf A_1)
		{
			base.List[A_0] = A_1;
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x000C08BC File Offset: 0x000BF8BC
		public int d(int A_0)
		{
			if (A_0 >= base.List.Count || A_0 < 0)
			{
				throw new ArgumentException();
			}
			int num = -1;
			for (int i = 0; i < base.List.Count; i++)
			{
				if (this.b(i).i)
				{
					num++;
				}
				if (num == A_0)
				{
					return num;
				}
			}
			return -1;
		}

		// Token: 0x0600297A RID: 10618 RVA: 0x000C0914 File Offset: 0x000BF914
		public bf c(int A_0)
		{
			int num = this.d(A_0);
			if (num < 0)
			{
				return null;
			}
			return this.b(num);
		}

		// Token: 0x0600297B RID: 10619 RVA: 0x000C0938 File Offset: 0x000BF938
		public int a(int A_0)
		{
			if (A_0 > base.List.Count || A_0 < 0)
			{
				throw new ArgumentException();
			}
			int num = 0;
			for (int i = 0; i < A_0; i++)
			{
				if (this.b(i).i)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600297C RID: 10620 RVA: 0x000C097E File Offset: 0x000BF97E
		public int a()
		{
			return this.a(base.List.Count);
		}
	}
}
