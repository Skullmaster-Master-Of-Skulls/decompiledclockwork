using System;

namespace System.ServiceModel
{
	// Token: 0x0200002E RID: 46
	[__DynamicallyInvokable]
	public interface ICommunicationObject
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000186 RID: 390
		[__DynamicallyInvokable]
		CommunicationState State { [__DynamicallyInvokable] get; }

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000187 RID: 391
		// (remove) Token: 0x06000188 RID: 392
		[__DynamicallyInvokable]
		event EventHandler Closed;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000189 RID: 393
		// (remove) Token: 0x0600018A RID: 394
		[__DynamicallyInvokable]
		event EventHandler Closing;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600018B RID: 395
		// (remove) Token: 0x0600018C RID: 396
		[__DynamicallyInvokable]
		event EventHandler Faulted;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600018D RID: 397
		// (remove) Token: 0x0600018E RID: 398
		[__DynamicallyInvokable]
		event EventHandler Opened;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600018F RID: 399
		// (remove) Token: 0x06000190 RID: 400
		[__DynamicallyInvokable]
		event EventHandler Opening;

		// Token: 0x06000191 RID: 401
		[__DynamicallyInvokable]
		void Abort();

		// Token: 0x06000192 RID: 402
		[__DynamicallyInvokable]
		void Close();

		// Token: 0x06000193 RID: 403
		[__DynamicallyInvokable]
		void Close(TimeSpan timeout);

		// Token: 0x06000194 RID: 404
		[__DynamicallyInvokable]
		IAsyncResult BeginClose(AsyncCallback callback, object state);

		// Token: 0x06000195 RID: 405
		[__DynamicallyInvokable]
		IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x06000196 RID: 406
		[__DynamicallyInvokable]
		void EndClose(IAsyncResult result);

		// Token: 0x06000197 RID: 407
		[__DynamicallyInvokable]
		void Open();

		// Token: 0x06000198 RID: 408
		[__DynamicallyInvokable]
		void Open(TimeSpan timeout);

		// Token: 0x06000199 RID: 409
		[__DynamicallyInvokable]
		IAsyncResult BeginOpen(AsyncCallback callback, object state);

		// Token: 0x0600019A RID: 410
		[__DynamicallyInvokable]
		IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x0600019B RID: 411
		[__DynamicallyInvokable]
		void EndOpen(IAsyncResult result);
	}
}
