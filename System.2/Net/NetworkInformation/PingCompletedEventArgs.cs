using System;
using System.ComponentModel;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002E9 RID: 745
	public class PingCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001A24 RID: 6692 RVA: 0x0007EDC0 File Offset: 0x0007CFC0
		internal PingCompletedEventArgs(PingReply reply, Exception error, bool cancelled, object userToken) : base(error, cancelled, userToken)
		{
			this.reply = reply;
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001A25 RID: 6693 RVA: 0x0007EDD3 File Offset: 0x0007CFD3
		public PingReply Reply
		{
			get
			{
				return this.reply;
			}
		}

		// Token: 0x04001A72 RID: 6770
		private PingReply reply;
	}
}
