using System;
using a;

namespace MailBee.ImapMail
{
	// Token: 0x02000186 RID: 390
	public class ImapServerStatusEventArgs : CommonEventArgs
	{
		// Token: 0x06000E3F RID: 3647 RVA: 0x00035889 File Offset: 0x00034889
		internal ImapServerStatusEventArgs(string A_0, string A_1, string A_2, string A_3, bc A_4) : base(A_4)
		{
			this.a = A_0;
			this.b = A_1;
			this.c = A_2;
			this.d = A_3;
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000E40 RID: 3648 RVA: 0x000358B0 File Offset: 0x000348B0
		public string StatusID
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000E41 RID: 3649 RVA: 0x000358B8 File Offset: 0x000348B8
		public string OptionalData
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000E42 RID: 3650 RVA: 0x000358C0 File Offset: 0x000348C0
		public string HumanReadable
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000E43 RID: 3651 RVA: 0x000358C8 File Offset: 0x000348C8
		public string Details
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000E44 RID: 3652 RVA: 0x000358D0 File Offset: 0x000348D0
		public bool IsAlert
		{
			get
			{
				return this.b != null && this.b.ToUpper() == "ALERT";
			}
		}

		// Token: 0x04000937 RID: 2359
		private string a;

		// Token: 0x04000938 RID: 2360
		private string b;

		// Token: 0x04000939 RID: 2361
		private string c;

		// Token: 0x0400093A RID: 2362
		private string d;
	}
}
