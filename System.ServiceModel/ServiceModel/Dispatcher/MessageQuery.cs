using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000472 RID: 1138
	public abstract class MessageQuery
	{
		// Token: 0x06002C46 RID: 11334 RVA: 0x000AD5F3 File Offset: 0x000AB7F3
		public virtual MessageQueryCollection CreateMessageQueryCollection()
		{
			return null;
		}

		// Token: 0x06002C47 RID: 11335
		public abstract TResult Evaluate<TResult>(Message message);

		// Token: 0x06002C48 RID: 11336
		public abstract TResult Evaluate<TResult>(MessageBuffer buffer);
	}
}
