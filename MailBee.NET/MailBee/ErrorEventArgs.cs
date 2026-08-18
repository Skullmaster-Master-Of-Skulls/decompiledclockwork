using System;
using a;

namespace MailBee
{
	// Token: 0x02000033 RID: 51
	public class ErrorEventArgs : CommonEventArgs
	{
		// Token: 0x06000168 RID: 360 RVA: 0x00007D97 File Offset: 0x00006D97
		internal ErrorEventArgs(MailBeeException A_0, bool A_1, bc A_2) : base(A_2)
		{
			this.a = A_0;
			this.b = A_1;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00007DAE File Offset: 0x00006DAE
		public MailBeeException Reason
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00007DB6 File Offset: 0x00006DB6
		public bool IsFinalError
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x0400014F RID: 335
		private MailBeeException a;

		// Token: 0x04000150 RID: 336
		private bool b;
	}
}
