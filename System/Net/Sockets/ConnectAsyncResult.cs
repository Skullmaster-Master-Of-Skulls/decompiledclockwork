using System;

namespace System.Net.Sockets
{
	// Token: 0x020005BD RID: 1469
	internal class ConnectAsyncResult : ContextAwareResult
	{
		// Token: 0x06002DEB RID: 11755 RVA: 0x000CA36B File Offset: 0x000C936B
		internal ConnectAsyncResult(object myObject, EndPoint endPoint, object myState, AsyncCallback myCallBack) : base(myObject, myState, myCallBack)
		{
			this.m_EndPoint = endPoint;
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06002DEC RID: 11756 RVA: 0x000CA37E File Offset: 0x000C937E
		internal EndPoint RemoteEndPoint
		{
			get
			{
				return this.m_EndPoint;
			}
		}

		// Token: 0x04002B59 RID: 11097
		private EndPoint m_EndPoint;
	}
}
