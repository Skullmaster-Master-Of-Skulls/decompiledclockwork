using System;
using a;

namespace MailBee.ImapMail
{
	// Token: 0x02000188 RID: 392
	public class ImapMessageStatusEventArgs : CommonEventArgs
	{
		// Token: 0x06000E49 RID: 3657 RVA: 0x000358F1 File Offset: 0x000348F1
		internal ImapMessageStatusEventArgs(string A_0, int A_1, MessageFlagSet A_2, bc A_3) : base(A_3)
		{
			this.a = A_0;
			this.b = A_1;
			this.c = A_2;
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000E4A RID: 3658 RVA: 0x00035910 File Offset: 0x00034910
		public string StatusID
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000E4B RID: 3659 RVA: 0x00035918 File Offset: 0x00034918
		public int MessageCountOrIndex
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000E4C RID: 3660 RVA: 0x00035920 File Offset: 0x00034920
		public MessageFlagSet Flags
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x0400093B RID: 2363
		private string a;

		// Token: 0x0400093C RID: 2364
		private int b;

		// Token: 0x0400093D RID: 2365
		private MessageFlagSet c;
	}
}
