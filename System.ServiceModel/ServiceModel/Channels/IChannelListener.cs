using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000704 RID: 1796
	public interface IChannelListener : ICommunicationObject
	{
		// Token: 0x170011B8 RID: 4536
		// (get) Token: 0x060044AC RID: 17580
		Uri Uri { get; }

		// Token: 0x060044AD RID: 17581
		T GetProperty<T>() where T : class;

		// Token: 0x060044AE RID: 17582
		bool WaitForChannel(TimeSpan timeout);

		// Token: 0x060044AF RID: 17583
		IAsyncResult BeginWaitForChannel(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x060044B0 RID: 17584
		bool EndWaitForChannel(IAsyncResult result);
	}
}
