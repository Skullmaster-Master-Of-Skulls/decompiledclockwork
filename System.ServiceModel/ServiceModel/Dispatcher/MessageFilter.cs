using System;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200046E RID: 1134
	[DataContract]
	[KnownType(typeof(XPathMessageFilter))]
	[KnownType(typeof(ActionMessageFilter))]
	[KnownType(typeof(MatchAllMessageFilter))]
	[KnownType(typeof(MatchNoneMessageFilter))]
	public abstract class MessageFilter
	{
		// Token: 0x06002BF9 RID: 11257 RVA: 0x000AC4AA File Offset: 0x000AA6AA
		protected internal virtual IMessageFilterTable<FilterData> CreateFilterTable<FilterData>()
		{
			return null;
		}

		// Token: 0x06002BFA RID: 11258
		public abstract bool Match(MessageBuffer buffer);

		// Token: 0x06002BFB RID: 11259
		public abstract bool Match(Message message);
	}
}
