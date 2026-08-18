using System;
using System.ServiceModel.Description;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A6C RID: 2668
	internal class TransactionInputDuplexChannelGeneric<TChannel> : TransactionDuplexChannelGeneric<TChannel> where TChannel : class, IDuplexChannel
	{
		// Token: 0x0600694B RID: 26955 RVA: 0x00189101 File Offset: 0x00187301
		public TransactionInputDuplexChannelGeneric(ChannelManagerBase channelManager, TChannel innerChannel) : base(channelManager, innerChannel, MessageDirection.Input)
		{
		}
	}
}
