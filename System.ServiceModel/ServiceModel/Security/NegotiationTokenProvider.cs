using System;
using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000302 RID: 770
	internal abstract class NegotiationTokenProvider<T> : IssuanceTokenProviderBase<T> where T : IssuanceTokenProviderState
	{
		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001A49 RID: 6729 RVA: 0x000628D0 File Offset: 0x00060AD0
		// (set) Token: 0x06001A4A RID: 6730 RVA: 0x000628D8 File Offset: 0x00060AD8
		public BindingContext IssuerBindingContext
		{
			get
			{
				return this.issuanceBindingContext;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.issuanceBindingContext = value.Clone();
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06001A4B RID: 6731 RVA: 0x00062904 File Offset: 0x00060B04
		public override XmlDictionaryString RequestSecurityTokenAction
		{
			get
			{
				return base.StandardsManager.TrustDriver.RequestSecurityTokenAction;
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001A4C RID: 6732 RVA: 0x00062916 File Offset: 0x00060B16
		public override XmlDictionaryString RequestSecurityTokenResponseAction
		{
			get
			{
				return base.StandardsManager.TrustDriver.RequestSecurityTokenResponseAction;
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001A4D RID: 6733 RVA: 0x00062928 File Offset: 0x00060B28
		protected override MessageVersion MessageVersion
		{
			get
			{
				return this.messageVersion;
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001A4E RID: 6734 RVA: 0x00062930 File Offset: 0x00060B30
		protected override bool RequiresManualReplyAddressing
		{
			get
			{
				base.ThrowIfCreated();
				return this.requiresManualReplyAddressing;
			}
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x00062940 File Offset: 0x00060B40
		public override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (this.rstChannelFactory != null)
			{
				this.rstChannelFactory.Close(timeout);
				this.rstChannelFactory = null;
			}
			base.OnClose(timeoutHelper.RemainingTime());
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x0006297D File Offset: 0x00060B7D
		public override void OnAbort()
		{
			if (this.rstChannelFactory != null)
			{
				this.rstChannelFactory.Abort();
				this.rstChannelFactory = null;
			}
			base.OnAbort();
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x000629A0 File Offset: 0x00060BA0
		public override void OnOpen(TimeSpan timeout)
		{
			if (this.IssuerBindingContext == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("IssuerBuildContextNotSet", new object[]
				{
					base.GetType()
				})));
			}
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.SetupRstChannelFactory();
			this.rstChannelFactory.Open(timeout);
			base.OnOpen(timeoutHelper.RemainingTime());
		}

		// Token: 0x06001A52 RID: 6738
		protected abstract IChannelFactory<IRequestChannel> GetNegotiationChannelFactory(IChannelFactory<IRequestChannel> transportChannelFactory, ChannelBuilder channelBuilder);

		// Token: 0x06001A53 RID: 6739 RVA: 0x00062A08 File Offset: 0x00060C08
		private void SetupRstChannelFactory()
		{
			ChannelBuilder channelBuilder = new ChannelBuilder(this.IssuerBindingContext.Clone(), true);
			IChannelFactory<IRequestChannel> transportChannelFactory;
			if (channelBuilder.CanBuildChannelFactory<IRequestChannel>())
			{
				transportChannelFactory = channelBuilder.BuildChannelFactory<IRequestChannel>();
				this.requiresManualReplyAddressing = true;
			}
			else
			{
				ServiceChannelFactory serviceChannelFactory = ServiceChannelFactory.BuildChannelFactory(channelBuilder, new ClientRuntime("RequestSecurityTokenContract", "http://tempuri.org/")
				{
					ValidateMustUnderstand = false
				});
				serviceChannelFactory.ClientRuntime.UseSynchronizationContext = false;
				serviceChannelFactory.ClientRuntime.AddTransactionFlowProperties = false;
				ClientOperation clientOperation = new ClientOperation(serviceChannelFactory.ClientRuntime, "RequestSecurityToken", this.RequestSecurityTokenAction.Value);
				clientOperation.Formatter = MessageOperationFormatter.Instance;
				serviceChannelFactory.ClientRuntime.Operations.Add(clientOperation);
				if (this.IsMultiLegNegotiation)
				{
					ClientOperation clientOperation2 = new ClientOperation(serviceChannelFactory.ClientRuntime, "RequestSecurityTokenResponse", this.RequestSecurityTokenResponseAction.Value);
					clientOperation2.Formatter = MessageOperationFormatter.Instance;
					serviceChannelFactory.ClientRuntime.Operations.Add(clientOperation2);
				}
				this.requiresManualReplyAddressing = false;
				transportChannelFactory = new SecuritySessionSecurityTokenProvider.RequestChannelFactory(serviceChannelFactory);
			}
			this.rstChannelFactory = this.GetNegotiationChannelFactory(transportChannelFactory, channelBuilder);
			this.messageVersion = channelBuilder.Binding.MessageVersion;
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x00062B26 File Offset: 0x00060D26
		protected override bool WillInitializeChannelFactoriesCompleteSynchronously(EndpointAddress target)
		{
			return true;
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x00062B29 File Offset: 0x00060D29
		protected override void InitializeChannelFactories(EndpointAddress target, TimeSpan timeout)
		{
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x00062B2B File Offset: 0x00060D2B
		protected override IAsyncResult BeginInitializeChannelFactories(EndpointAddress target, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x00062B35 File Offset: 0x00060D35
		protected override void EndInitializeChannelFactories(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x00062B3D File Offset: 0x00060D3D
		protected override IRequestChannel CreateClientChannel(EndpointAddress target, Uri via)
		{
			if (via != null)
			{
				return this.rstChannelFactory.CreateChannel(target, via);
			}
			return this.rstChannelFactory.CreateChannel(target);
		}

		// Token: 0x04001D14 RID: 7444
		private IChannelFactory<IRequestChannel> rstChannelFactory;

		// Token: 0x04001D15 RID: 7445
		private bool requiresManualReplyAddressing;

		// Token: 0x04001D16 RID: 7446
		private BindingContext issuanceBindingContext;

		// Token: 0x04001D17 RID: 7447
		private MessageVersion messageVersion;
	}
}
