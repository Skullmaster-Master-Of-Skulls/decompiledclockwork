using System;

namespace NLog.Internal.NetworkSenders
{
	// Token: 0x0200009C RID: 156
	internal interface INetworkSenderFactory
	{
		// Token: 0x06000505 RID: 1285
		NetworkSender Create(string url, int maxQueueSize);
	}
}
