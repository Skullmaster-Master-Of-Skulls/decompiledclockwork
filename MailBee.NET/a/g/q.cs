using System;

namespace a.g
{
	// Token: 0x0200040A RID: 1034
	internal class q : m, ax
	{
		// Token: 0x0600244D RID: 9293 RVA: 0x0009A197 File Offset: 0x00099197
		public q(int A_0, string A_1)
		{
			this.a = A_0;
			this.b = A_1;
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x0009A1B8 File Offset: 0x000991B8
		public int get_Priority()
		{
			if (base.c())
			{
				return this.a;
			}
			return 999;
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x0009A1CE File Offset: 0x000991CE
		public void set_Priority(int value)
		{
			this.a = value;
		}

		// Token: 0x06002450 RID: 9296 RVA: 0x0009A1D7 File Offset: 0x000991D7
		public string a()
		{
			return this.b;
		}

		// Token: 0x06002451 RID: 9297 RVA: 0x0009A1DF File Offset: 0x000991DF
		public override h a5()
		{
			return h.o;
		}

		// Token: 0x04001818 RID: 6168
		private int a;

		// Token: 0x04001819 RID: 6169
		private new string b = string.Empty;
	}
}
