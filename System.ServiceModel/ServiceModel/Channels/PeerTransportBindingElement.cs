using System;
using System.Collections.ObjectModel;
using System.Net;
using System.ServiceModel.Description;
using System.ServiceModel.PeerResolvers;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A43 RID: 2627
	[Obsolete("PeerChannel feature is obsolete and will be removed in the future.", false)]
	public sealed class PeerTransportBindingElement : TransportBindingElement, IWsdlExportExtension, ITransportPolicyImport, IPolicyExportExtension
	{
		// Token: 0x060067F6 RID: 26614 RVA: 0x001840CA File Offset: 0x001822CA
		public PeerTransportBindingElement()
		{
			this.listenIPAddress = null;
			this.port = 0;
			if (PeerTransportDefaults.ResolverAvailable)
			{
				this.resolver = PeerTransportDefaults.CreateResolver();
			}
			this.peerSecurity = new PeerSecuritySettings();
		}

		// Token: 0x060067F7 RID: 26615 RVA: 0x00184100 File Offset: 0x00182300
		private PeerTransportBindingElement(PeerTransportBindingElement elementToBeCloned) : base(elementToBeCloned)
		{
			this.listenIPAddress = elementToBeCloned.listenIPAddress;
			this.port = elementToBeCloned.port;
			this.resolverSet = elementToBeCloned.resolverSet;
			this.resolver = elementToBeCloned.resolver;
			this.peerSecurity = new PeerSecuritySettings(elementToBeCloned.Security);
		}

		// Token: 0x170018E2 RID: 6370
		// (get) Token: 0x060067F8 RID: 26616 RVA: 0x00184155 File Offset: 0x00182355
		// (set) Token: 0x060067F9 RID: 26617 RVA: 0x0018415D File Offset: 0x0018235D
		public IPAddress ListenIPAddress
		{
			get
			{
				return this.listenIPAddress;
			}
			set
			{
				PeerValidateHelper.ValidateListenIPAddress(value);
				this.listenIPAddress = value;
			}
		}

		// Token: 0x170018E3 RID: 6371
		// (get) Token: 0x060067FA RID: 26618 RVA: 0x0018416C File Offset: 0x0018236C
		// (set) Token: 0x060067FB RID: 26619 RVA: 0x00184174 File Offset: 0x00182374
		public override long MaxReceivedMessageSize
		{
			get
			{
				return base.MaxReceivedMessageSize;
			}
			set
			{
				PeerValidateHelper.ValidateMaxMessageSize(value);
				base.MaxReceivedMessageSize = value;
			}
		}

		// Token: 0x170018E4 RID: 6372
		// (get) Token: 0x060067FC RID: 26620 RVA: 0x00184183 File Offset: 0x00182383
		// (set) Token: 0x060067FD RID: 26621 RVA: 0x0018418B File Offset: 0x0018238B
		public int Port
		{
			get
			{
				return this.port;
			}
			set
			{
				PeerValidateHelper.ValidatePort(value);
				this.port = value;
			}
		}

		// Token: 0x170018E5 RID: 6373
		// (get) Token: 0x060067FE RID: 26622 RVA: 0x0018419A File Offset: 0x0018239A
		// (set) Token: 0x060067FF RID: 26623 RVA: 0x001841A4 File Offset: 0x001823A4
		internal PeerResolver Resolver
		{
			get
			{
				return this.resolver;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				if (value.GetType() == PeerTransportDefaults.ResolverType)
				{
					if (!PeerTransportDefaults.ResolverInstalled)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("PeerPnrpNotInstalled"));
					}
					if (!PeerTransportDefaults.ResolverAvailable)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("PeerPnrpNotAvailable"));
					}
				}
				this.resolver = value;
				this.resolverSet = true;
			}
		}

		// Token: 0x170018E6 RID: 6374
		// (get) Token: 0x06006800 RID: 26624 RVA: 0x00184226 File Offset: 0x00182426
		public override string Scheme
		{
			get
			{
				return "net.p2p";
			}
		}

		// Token: 0x170018E7 RID: 6375
		// (get) Token: 0x06006801 RID: 26625 RVA: 0x0018422D File Offset: 0x0018242D
		public PeerSecuritySettings Security
		{
			get
			{
				return this.peerSecurity;
			}
		}

		// Token: 0x06006802 RID: 26626 RVA: 0x00184235 File Offset: 0x00182435
		void ITransportPolicyImport.ImportPolicy(MetadataImporter importer, PolicyConversionContext context)
		{
			if (importer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("importer");
			}
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			this.peerSecurity.OnImportPolicy(importer, context);
		}

		// Token: 0x06006803 RID: 26627 RVA: 0x0018426C File Offset: 0x0018246C
		void IPolicyExportExtension.ExportPolicy(MetadataExporter exporter, PolicyConversionContext context)
		{
			if (exporter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("exporter");
			}
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			this.peerSecurity.OnExportPolicy(exporter, context);
			bool flag;
			MessageEncodingBindingElement messageEncodingBindingElement = this.FindMessageEncodingBindingElement(context.BindingElements, out flag);
			if (flag && messageEncodingBindingElement is IPolicyExportExtension)
			{
				((IPolicyExportExtension)messageEncodingBindingElement).ExportPolicy(exporter, context);
			}
			WsdlExporter.WSAddressingHelper.AddWSAddressingAssertion(exporter, context, messageEncodingBindingElement.MessageVersion.Addressing);
		}

		// Token: 0x06006804 RID: 26628 RVA: 0x001842E5 File Offset: 0x001824E5
		void IWsdlExportExtension.ExportContract(WsdlExporter exporter, WsdlContractConversionContext context)
		{
		}

		// Token: 0x06006805 RID: 26629 RVA: 0x001842E8 File Offset: 0x001824E8
		void IWsdlExportExtension.ExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext endpointContext)
		{
			bool flag;
			MessageEncodingBindingElement messageEncodingBindingElement = this.FindMessageEncodingBindingElement(endpointContext, out flag);
			TransportBindingElement.ExportWsdlEndpoint(exporter, endpointContext, "http://schemas.microsoft.com/soap/peer", messageEncodingBindingElement.MessageVersion.Addressing);
		}

		// Token: 0x06006806 RID: 26630 RVA: 0x00184316 File Offset: 0x00182516
		internal void CreateDefaultResolver(PeerResolverSettings settings)
		{
			if (PeerTransportDefaults.ResolverAvailable)
			{
				this.resolver = new PnrpPeerResolver(settings.ReferralPolicy);
			}
		}

		// Token: 0x06006807 RID: 26631 RVA: 0x00184330 File Offset: 0x00182530
		private MessageEncodingBindingElement FindMessageEncodingBindingElement(BindingElementCollection bindingElements, out bool createdNew)
		{
			createdNew = false;
			MessageEncodingBindingElement messageEncodingBindingElement = bindingElements.Find<MessageEncodingBindingElement>();
			if (messageEncodingBindingElement == null)
			{
				createdNew = true;
				messageEncodingBindingElement = new BinaryMessageEncodingBindingElement();
			}
			return messageEncodingBindingElement;
		}

		// Token: 0x06006808 RID: 26632 RVA: 0x00184354 File Offset: 0x00182554
		private MessageEncodingBindingElement FindMessageEncodingBindingElement(WsdlEndpointConversionContext endpointContext, out bool createdNew)
		{
			BindingElementCollection bindingElements = endpointContext.Endpoint.Binding.CreateBindingElements();
			return this.FindMessageEncodingBindingElement(bindingElements, out createdNew);
		}

		// Token: 0x06006809 RID: 26633 RVA: 0x0018437C File Offset: 0x0018257C
		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("context"));
			}
			if (!this.CanBuildChannelFactory<TChannel>(context))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("TChannel", SR.GetString("ChannelTypeNotSupported", new object[]
				{
					typeof(TChannel)
				}));
			}
			if (this.ManualAddressing)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ManualAddressingNotSupported")));
			}
			PeerResolver peerResolver = this.GetResolver(context);
			return new PeerChannelFactory<TChannel>(this, context, peerResolver);
		}

		// Token: 0x0600680A RID: 26634 RVA: 0x0018440C File Offset: 0x0018260C
		public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			PeerResolver peerResolver = this.GetResolver(context);
			PeerChannelListenerBase peerChannelListenerBase;
			if (typeof(TChannel) == typeof(IInputChannel))
			{
				peerChannelListenerBase = new PeerInputChannelListener(this, context, peerResolver);
			}
			else
			{
				if (!(typeof(TChannel) == typeof(IDuplexChannel)))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("TChannel", SR.GetString("ChannelTypeNotSupported", new object[]
					{
						typeof(TChannel)
					}));
				}
				peerChannelListenerBase = new PeerDuplexChannelListener(this, context, peerResolver);
			}
			return (IChannelListener<TChannel>)peerChannelListenerBase;
		}

		// Token: 0x0600680B RID: 26635 RVA: 0x001844B5 File Offset: 0x001826B5
		public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
		{
			return typeof(TChannel) == typeof(IOutputChannel) || typeof(TChannel) == typeof(IDuplexChannel);
		}

		// Token: 0x0600680C RID: 26636 RVA: 0x001844ED File Offset: 0x001826ED
		public override bool CanBuildChannelListener<TChannel>(BindingContext context)
		{
			return typeof(TChannel) == typeof(IInputChannel) || typeof(TChannel) == typeof(IDuplexChannel);
		}

		// Token: 0x0600680D RID: 26637 RVA: 0x00184525 File Offset: 0x00182725
		public override BindingElement Clone()
		{
			return new PeerTransportBindingElement(this);
		}

		// Token: 0x0600680E RID: 26638 RVA: 0x00184530 File Offset: 0x00182730
		public override T GetProperty<T>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (typeof(T) == typeof(IBindingMulticastCapabilities))
			{
				return (T)((object)new PeerTransportBindingElement.BindingMulticastCapabilities());
			}
			if (typeof(T) == typeof(ISecurityCapabilities))
			{
				return (T)((object)new SecurityCapabilities(this.Security.SupportsAuthentication, this.Security.SupportsAuthentication, false, this.Security.SupportedProtectionLevel, this.Security.SupportedProtectionLevel));
			}
			if (typeof(T) == typeof(IBindingDeliveryCapabilities))
			{
				return (T)((object)new PeerTransportBindingElement.BindingDeliveryCapabilitiesHelper());
			}
			return base.GetProperty<T>(context);
		}

		// Token: 0x0600680F RID: 26639 RVA: 0x001845F8 File Offset: 0x001827F8
		private PeerResolver GetResolver(BindingContext context)
		{
			if (this.resolverSet)
			{
				return this.resolver;
			}
			Collection<PeerCustomResolverBindingElement> collection = context.BindingParameters.FindAll<PeerCustomResolverBindingElement>();
			if (collection.Count > 1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MultiplePeerCustomResolverBindingElementsInParameters")));
			}
			if (collection.Count == 1)
			{
				context.BindingParameters.Remove<PeerCustomResolverBindingElement>();
				return collection[0].CreatePeerResolver();
			}
			Collection<PeerResolverBindingElement> collection2 = context.BindingParameters.FindAll<PeerResolverBindingElement>();
			if (collection2.Count > 1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MultiplePeerResolverBindingElementsinParameters")));
			}
			if (collection2.Count != 0)
			{
				if (collection2[0].GetType() == PeerTransportDefaults.ResolverBindingElementType)
				{
					if (!PeerTransportDefaults.ResolverInstalled)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("PeerPnrpNotInstalled")));
					}
					if (!PeerTransportDefaults.ResolverAvailable)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("PeerPnrpNotAvailable")));
					}
				}
				context.BindingParameters.Remove<PeerResolverBindingElement>();
				return collection2[0].CreatePeerResolver();
			}
			if (this.resolver != null)
			{
				return this.resolver;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("PeerResolverBindingElementRequired", new object[]
			{
				context.Binding.Name
			})));
		}

		// Token: 0x04003BA9 RID: 15273
		private IPAddress listenIPAddress;

		// Token: 0x04003BAA RID: 15274
		private int port;

		// Token: 0x04003BAB RID: 15275
		private PeerResolver resolver;

		// Token: 0x04003BAC RID: 15276
		private bool resolverSet;

		// Token: 0x04003BAD RID: 15277
		private PeerSecuritySettings peerSecurity;

		// Token: 0x02000E79 RID: 3705
		private class BindingMulticastCapabilities : IBindingMulticastCapabilities
		{
			// Token: 0x17001D1D RID: 7453
			// (get) Token: 0x060083F4 RID: 33780 RVA: 0x001E80B5 File Offset: 0x001E62B5
			public bool IsMulticast
			{
				get
				{
					return true;
				}
			}
		}

		// Token: 0x02000E7A RID: 3706
		private class BindingDeliveryCapabilitiesHelper : IBindingDeliveryCapabilities
		{
			// Token: 0x060083F6 RID: 33782 RVA: 0x001E80C0 File Offset: 0x001E62C0
			internal BindingDeliveryCapabilitiesHelper()
			{
			}

			// Token: 0x17001D1E RID: 7454
			// (get) Token: 0x060083F7 RID: 33783 RVA: 0x001E80C8 File Offset: 0x001E62C8
			bool IBindingDeliveryCapabilities.AssuresOrderedDelivery
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001D1F RID: 7455
			// (get) Token: 0x060083F8 RID: 33784 RVA: 0x001E80CB File Offset: 0x001E62CB
			bool IBindingDeliveryCapabilities.QueuedDelivery
			{
				get
				{
					return false;
				}
			}
		}
	}
}
