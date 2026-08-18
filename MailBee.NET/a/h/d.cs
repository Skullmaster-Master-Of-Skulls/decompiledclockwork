using System;
using System.Text;

namespace a.h
{
	// Token: 0x020001F8 RID: 504
	internal class d
	{
		// Token: 0x0600102C RID: 4140 RVA: 0x000448D8 File Offset: 0x000438D8
		public string b()
		{
			return this.a;
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x000448E0 File Offset: 0x000438E0
		public string d()
		{
			return this.c;
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x000448E8 File Offset: 0x000438E8
		public d()
		{
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x000448F0 File Offset: 0x000438F0
		public d(n A_0)
		{
			int a_ = (int)A_0.f();
			this.a = A_0.a(a_);
			int a_2 = (int)A_0.f();
			string text = A_0.a(a_2);
			int num = text.IndexOf(':');
			this.c = text.Substring(0, num);
			this.b = text.Substring(num + 1);
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x0004494C File Offset: 0x0004394C
		public d(string A_0, string A_1, string A_2)
		{
			this.a = A_0;
			this.c = A_1;
			this.b = A_2;
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x00044969 File Offset: 0x00043969
		public string c()
		{
			return this.b;
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x00044974 File Offset: 0x00043974
		public override string ToString()
		{
			return new StringBuilder().Append('"').Append(this.b()).Append("\" [").Append(this.d()).Append(':').Append(this.c()).Append(']').ToString();
		}

		// Token: 0x04000BE7 RID: 3047
		protected string a;

		// Token: 0x04000BE8 RID: 3048
		protected string b;

		// Token: 0x04000BE9 RID: 3049
		protected string c;
	}
}
