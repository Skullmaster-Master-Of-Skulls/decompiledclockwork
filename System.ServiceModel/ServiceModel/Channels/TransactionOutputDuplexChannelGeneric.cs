using System;
using System.ServiceModel.Description;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A67 RID: 2663
	internal class TransactionOutputDuplexChannelGeneric<TChannel> : TransactionDuplexChannelGeneric<TChannel> where TChannel : class, IDuplexChannel
	{
		// Token: 0x06006920 RID: 26912 RVA: 0x00188B9A File Offset: 0x00186D9A
		public TransactionOutputDuplexChannelGeneric(ChannelManagerBase channelManager, TChannel innerChannel) : base(channelManager, innerChannel, MessageDirection.Output)
		{
		}
	}
}
