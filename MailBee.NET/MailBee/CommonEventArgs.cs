using System;
using a;

namespace MailBee
{
	// Token: 0x02000031 RID: 49
	public abstract class CommonEventArgs : EventArgs
	{
		// Token: 0x06000161 RID: 353 RVA: 0x00007D73 File Offset: 0x00006D73
		internal CommonEventArgs(bc A_0)
		{
			this.a = A_0;
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00007D82 File Offset: 0x00006D82
		public object State
		{
			get
			{
				return this.a.bi();
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00007D8F File Offset: 0x00006D8F
		internal bc Context
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x0400014E RID: 334
		private bc a;
	}
}
