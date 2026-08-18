using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000473 RID: 1139
	public abstract class MessageQueryCollection : Collection<MessageQuery>
	{
		// Token: 0x06002C4A RID: 11338
		public abstract IEnumerable<KeyValuePair<MessageQuery, TResult>> Evaluate<TResult>(Message message);

		// Token: 0x06002C4B RID: 11339
		public abstract IEnumerable<KeyValuePair<MessageQuery, TResult>> Evaluate<TResult>(MessageBuffer buffer);
	}
}
