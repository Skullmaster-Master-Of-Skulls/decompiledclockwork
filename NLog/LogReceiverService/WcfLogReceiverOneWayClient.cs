using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace NLog.LogReceiverService
{
	// Token: 0x02000139 RID: 313
	public sealed class WcfLogReceiverOneWayClient : WcfLogReceiverClientBase<ILogReceiverOneWayClient>, ILogReceiverOneWayClient
	{
		// Token: 0x06000B09 RID: 2825 RVA: 0x00019930 File Offset: 0x00017B30
		public WcfLogReceiverOneWayClient()
		{
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00019938 File Offset: 0x00017B38
		public WcfLogReceiverOneWayClient(string endpointConfigurationName) : base(endpointConfigurationName)
		{
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00019941 File Offset: 0x00017B41
		public WcfLogReceiverOneWayClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0001994B File Offset: 0x00017B4B
		public WcfLogReceiverOneWayClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x00019955 File Offset: 0x00017B55
		public WcfLogReceiverOneWayClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
		{
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0001995F File Offset: 0x00017B5F
		public override IAsyncResult BeginProcessLogMessages(NLogEvents events, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginProcessLogMessages(events, callback, asyncState);
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0001996F File Offset: 0x00017B6F
		public override void EndProcessLogMessages(IAsyncResult result)
		{
			base.Channel.EndProcessLogMessages(result);
		}
	}
}
