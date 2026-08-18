using System;

namespace System.Net.Sockets
{
	// Token: 0x02000377 RID: 887
	internal class ConnectAsyncResult : ContextAwareResult
	{
		// Token: 0x06002131 RID: 8497 RVA: 0x0009F39F File Offset: 0x0009D59F
		internal ConnectAsyncResult(object myObject, EndPoint endPoint, object myState, AsyncCallback myCallBack) : base(myObject, myState, myCallBack)
		{
			this.m_EndPoint = endPoint;
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06002132 RID: 8498 RVA: 0x0009F3B2 File Offset: 0x0009D5B2
		internal EndPoint RemoteEndPoint
		{
			get
			{
				return this.m_EndPoint;
			}
		}

		// Token: 0x04001E68 RID: 7784
		private EndPoint m_EndPoint;
	}
}
