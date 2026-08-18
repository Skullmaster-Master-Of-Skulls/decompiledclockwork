using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.ICore.MessagingQueue
{
	// Token: 0x0200005E RID: 94
	public interface IMessagingQueueManager<T> : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000294 RID: 660
		void SendMessage(T obj);

		// Token: 0x06000295 RID: 661
		T ReceiveMessage();
	}
}
