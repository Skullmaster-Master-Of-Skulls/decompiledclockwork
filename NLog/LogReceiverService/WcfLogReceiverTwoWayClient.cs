using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace NLog.LogReceiverService
{
	// Token: 0x0200013A RID: 314
	public sealed class WcfLogReceiverTwoWayClient : WcfLogReceiverClientBase<ILogReceiverTwoWayClient>, ILogReceiverTwoWayClient
	{
		// Token: 0x06000B10 RID: 2832 RVA: 0x0001997D File Offset: 0x00017B7D
		public WcfLogReceiverTwoWayClient()
		{
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00019985 File Offset: 0x00017B85
		public WcfLogReceiverTwoWayClient(string endpointConfigurationName) : base(endpointConfigurationName)
		{
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0001998E File Offset: 0x00017B8E
		public WcfLogReceiverTwoWayClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x00019998 File Offset: 0x00017B98
		public WcfLogReceiverTwoWayClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x000199A2 File Offset: 0x00017BA2
		public WcfLogReceiverTwoWayClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
		{
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x000199AC File Offset: 0x00017BAC
		public override IAsyncResult BeginProcessLogMessages(NLogEvents events, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginProcessLogMessages(events, callback, asyncState);
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x000199BC File Offset: 0x00017BBC
		public override void EndProcessLogMessages(IAsyncResult result)
		{
			base.Channel.EndProcessLogMessages(result);
		}
	}
}
