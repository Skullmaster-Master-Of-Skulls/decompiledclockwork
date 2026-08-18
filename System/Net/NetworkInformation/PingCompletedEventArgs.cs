using System;
using System.ComponentModel;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000622 RID: 1570
	public class PingCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06003046 RID: 12358 RVA: 0x000D042C File Offset: 0x000CF42C
		internal PingCompletedEventArgs(PingReply reply, Exception error, bool cancelled, object userToken) : base(error, cancelled, userToken)
		{
			this.reply = reply;
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06003047 RID: 12359 RVA: 0x000D043F File Offset: 0x000CF43F
		public PingReply Reply
		{
			get
			{
				return this.reply;
			}
		}

		// Token: 0x04002DFD RID: 11773
		private PingReply reply;
	}
}
