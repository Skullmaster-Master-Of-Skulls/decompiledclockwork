using System;
using System.Diagnostics;
using System.Runtime.Diagnostics;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007A9 RID: 1961
	internal class ContextChannelFactory<TChannel> : LayeredChannelFactory<TChannel>
	{
		// Token: 0x06004A38 RID: 19000 RVA: 0x00110FA0 File Offset: 0x0010F1A0
		public ContextChannelFactory(BindingContext context, ContextExchangeMechanism contextExchangeMechanism, Uri callbackAddress, bool contextManagementEnabled) : base((context == null) ? null : context.Binding, (context == null) ? null : context.BuildInnerChannelFactory<TChannel>())
		{
			if (!ContextExchangeMechanismHelper.IsDefined(contextExchangeMechanism))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("contextExchangeMechanism"));
			}
			this.contextExchangeMechanism = contextExchangeMechanism;
			this.callbackAddress = callbackAddress;
			this.contextManagementEnabled = contextManagementEnabled;
		}

		// Token: 0x06004A39 RID: 19001 RVA: 0x00111000 File Offset: 0x0010F200
		protected override TChannel OnCreateChannel(EndpointAddress address, Uri via)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				string @string = SR.GetString("ContextChannelFactoryChannelCreatedDetail", new object[]
				{
					address,
					via
				});
				TraceUtility.TraceEvent(TraceEventType.Information, 983043, SR.GetString("TraceCodeContextChannelFactoryChannelCreated"), new StringTraceRecord("ChannelDetail", @string), this, null);
			}
			if (typeof(TChannel) == typeof(IOutputChannel))
			{
				return (TChannel)((object)new ContextOutputChannel(this, ((IChannelFactory<IOutputChannel>)base.InnerChannelFactory).CreateChannel(address, via), this.contextExchangeMechanism, this.callbackAddress, this.contextManagementEnabled));
			}
			if (typeof(TChannel) == typeof(IOutputSessionChannel))
			{
				return (TChannel)((object)new ContextOutputSessionChannel(this, ((IChannelFactory<IOutputSessionChannel>)base.InnerChannelFactory).CreateChannel(address, via), this.contextExchangeMechanism, this.callbackAddress, this.contextManagementEnabled));
			}
			if (typeof(TChannel) == typeof(IRequestChannel))
			{
				return (TChannel)((object)new ContextRequestChannel(this, ((IChannelFactory<IRequestChannel>)base.InnerChannelFactory).CreateChannel(address, via), this.contextExchangeMechanism, this.callbackAddress, this.contextManagementEnabled));
			}
			if (typeof(TChannel) == typeof(IRequestSessionChannel))
			{
				return (TChannel)((object)new ContextRequestSessionChannel(this, ((IChannelFactory<IRequestSessionChannel>)base.InnerChannelFactory).CreateChannel(address, via), this.contextExchangeMechanism, this.callbackAddress, this.contextManagementEnabled));
			}
			return (TChannel)((object)new ContextDuplexSessionChannel(this, ((IChannelFactory<IDuplexSessionChannel>)base.InnerChannelFactory).CreateChannel(address, via), this.contextExchangeMechanism, via, this.callbackAddress, this.contextManagementEnabled));
		}

		// Token: 0x04002F05 RID: 12037
		private ContextExchangeMechanism contextExchangeMechanism;

		// Token: 0x04002F06 RID: 12038
		private Uri callbackAddress;

		// Token: 0x04002F07 RID: 12039
		private bool contextManagementEnabled;
	}
}
