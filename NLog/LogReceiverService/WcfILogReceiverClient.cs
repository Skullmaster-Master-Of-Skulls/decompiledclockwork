using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace NLog.LogReceiverService
{
	// Token: 0x02000137 RID: 311
	[Obsolete("This may be removed in a future release.  Use WcfLogReceiverOneWayClient.")]
	public sealed class WcfILogReceiverClient : WcfLogReceiverClientBase<ILogReceiverClient>, ILogReceiverClient
	{
		// Token: 0x06000ACE RID: 2766 RVA: 0x0001955B File Offset: 0x0001775B
		public WcfILogReceiverClient()
		{
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00019563 File Offset: 0x00017763
		public WcfILogReceiverClient(string endpointConfigurationName) : base(endpointConfigurationName)
		{
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0001956C File Offset: 0x0001776C
		public WcfILogReceiverClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00019576 File Offset: 0x00017776
		public WcfILogReceiverClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00019580 File Offset: 0x00017780
		public WcfILogReceiverClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
		{
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0001958A File Offset: 0x0001778A
		public override IAsyncResult BeginProcessLogMessages(NLogEvents events, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginProcessLogMessages(events, callback, asyncState);
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0001959A File Offset: 0x0001779A
		public override void EndProcessLogMessages(IAsyncResult result)
		{
			base.Channel.EndProcessLogMessages(result);
		}
	}
}
