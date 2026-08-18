using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000586 RID: 1414
	public interface IInteractiveChannelInitializer
	{
		// Token: 0x06003674 RID: 13940
		IAsyncResult BeginDisplayInitializationUI(IClientChannel channel, AsyncCallback callback, object state);

		// Token: 0x06003675 RID: 13941
		void EndDisplayInitializationUI(IAsyncResult result);
	}
}
