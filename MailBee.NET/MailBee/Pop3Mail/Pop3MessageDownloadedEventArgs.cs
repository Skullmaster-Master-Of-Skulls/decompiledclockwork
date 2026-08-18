using System;
using a;
using MailBee.Mime;

namespace MailBee.Pop3Mail
{
	// Token: 0x0200057D RID: 1405
	public class Pop3MessageDownloadedEventArgs : CommonEventArgs
	{
		// Token: 0x06002F39 RID: 12089 RVA: 0x000DFE65 File Offset: 0x000DEE65
		internal Pop3MessageDownloadedEventArgs(int A_0, int A_1, MailMessage A_2, bc A_3) : base(A_3)
		{
			this.a = A_0;
			this.b = A_1;
			this.c = A_2;
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06002F3A RID: 12090 RVA: 0x000DFE84 File Offset: 0x000DEE84
		public int MessageNumber
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06002F3B RID: 12091 RVA: 0x000DFE8C File Offset: 0x000DEE8C
		public int DataLength
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06002F3C RID: 12092 RVA: 0x000DFE94 File Offset: 0x000DEE94
		// (set) Token: 0x06002F3D RID: 12093 RVA: 0x000DFE9C File Offset: 0x000DEE9C
		public MailMessage DownloadedMessage
		{
			get
			{
				return this.c;
			}
			set
			{
				this.c = value;
			}
		}

		// Token: 0x04001FFA RID: 8186
		private int a;

		// Token: 0x04001FFB RID: 8187
		private int b;

		// Token: 0x04001FFC RID: 8188
		private MailMessage c;
	}
}
