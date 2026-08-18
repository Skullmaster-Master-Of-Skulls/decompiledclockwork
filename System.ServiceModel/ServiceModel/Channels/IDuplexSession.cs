using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000707 RID: 1799
	[__DynamicallyInvokable]
	public interface IDuplexSession : IInputSession, ISession, IOutputSession
	{
		// Token: 0x060044B6 RID: 17590
		[__DynamicallyInvokable]
		void CloseOutputSession();

		// Token: 0x060044B7 RID: 17591
		[__DynamicallyInvokable]
		void CloseOutputSession(TimeSpan timeout);

		// Token: 0x060044B8 RID: 17592
		[__DynamicallyInvokable]
		IAsyncResult BeginCloseOutputSession(AsyncCallback callback, object state);

		// Token: 0x060044B9 RID: 17593
		[__DynamicallyInvokable]
		IAsyncResult BeginCloseOutputSession(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x060044BA RID: 17594
		[__DynamicallyInvokable]
		void EndCloseOutputSession(IAsyncResult result);
	}
}
