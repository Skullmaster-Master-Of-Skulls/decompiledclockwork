using System;
using System.Collections.Generic;
using System.Reflection;

namespace a.b
{
	// Token: 0x0200031A RID: 794
	[DefaultMember("Item")]
	internal class dg<a>
	{
		// Token: 0x06001C67 RID: 7271 RVA: 0x0007C810 File Offset: 0x0007B810
		public dg() : this(dg<a>.c)
		{
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x0007C81D File Offset: 0x0007B81D
		public dg(int A_0)
		{
			this.a = new List<a>(A_0);
			this.b = new Dictionary<a, int>(A_0);
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x0007C840 File Offset: 0x0007B840
		public bool a(a A_0)
		{
			int count = this.a.Count;
			this.a.Add(A_0);
			if (this.b.ContainsKey(A_0))
			{
				this.b[A_0] = count;
			}
			else
			{
				this.b.Add(A_0, count);
			}
			return true;
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x0007C890 File Offset: 0x0007B890
		public int a()
		{
			return this.a.Count;
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x0007C89D File Offset: 0x0007B89D
		public a a(int A_0)
		{
			return this.a[A_0];
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x0007C8AB File Offset: 0x0007B8AB
		public int b(a A_0)
		{
			if (!this.b.ContainsKey(A_0))
			{
				return -1;
			}
			return this.b[A_0];
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x0007C8C9 File Offset: 0x0007B8C9
		public IEnumerator<a> b()
		{
			return this.a.GetEnumerator();
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x0007C8DB File Offset: 0x0007B8DB
		public void c()
		{
			this.a.Clear();
			this.b.Clear();
		}

		// Token: 0x0400135B RID: 4955
		private List<a> a;

		// Token: 0x0400135C RID: 4956
		private Dictionary<a, int> b;

		// Token: 0x0400135D RID: 4957
		private static int c = 10;
	}
}
