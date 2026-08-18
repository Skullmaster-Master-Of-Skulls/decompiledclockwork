using System;

namespace a.b
{
	// Token: 0x020002B8 RID: 696
	internal class c1 : EventArgs
	{
		// Token: 0x06001840 RID: 6208 RVA: 0x0006EA72 File Offset: 0x0006DA72
		public c1(string A_0, db A_1, eg A_2)
		{
			this.c = A_0;
			this.a = A_1;
			this.b = A_2;
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x0006EA8F File Offset: 0x0006DA8F
		public virtual db b()
		{
			return this.a;
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x0006EA97 File Offset: 0x0006DA97
		public virtual eg d()
		{
			return this.b;
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x0006EA9F File Offset: 0x0006DA9F
		public virtual az c()
		{
			return new az(this.b);
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x0006EAAC File Offset: 0x0006DAAC
		public virtual string a()
		{
			return this.c;
		}

		// Token: 0x04001227 RID: 4647
		private db a;

		// Token: 0x04001228 RID: 4648
		private eg b;

		// Token: 0x04001229 RID: 4649
		private string c;
	}
}
