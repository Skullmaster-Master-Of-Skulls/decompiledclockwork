using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.Web.Services.Description;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008A9 RID: 2217
	[__DynamicallyInvokable]
	public abstract class TransportBindingElement : BindingElement
	{
		// Token: 0x06005494 RID: 21652 RVA: 0x001374C8 File Offset: 0x001356C8
		[__DynamicallyInvokable]
		protected TransportBindingElement()
		{
			this.manualAddressing = false;
			this.maxBufferPoolSize = 524288L;
			this.maxReceivedMessageSize = 65536L;
		}

		// Token: 0x06005495 RID: 21653 RVA: 0x001374EF File Offset: 0x001356EF
		[__DynamicallyInvokable]
		protected TransportBindingElement(TransportBindingElement elementToBeCloned) : base(elementToBeCloned)
		{
			this.manualAddressing = elementToBeCloned.manualAddressing;
			this.maxBufferPoolSize = elementToBeCloned.maxBufferPoolSize;
			this.maxReceivedMessageSize = elementToBeCloned.maxReceivedMessageSize;
		}

		// Token: 0x170014D0 RID: 5328
		// (get) Token: 0x06005496 RID: 21654 RVA: 0x0013751C File Offset: 0x0013571C
		// (set) Token: 0x06005497 RID: 21655 RVA: 0x00137524 File Offset: 0x00135724
		[DefaultValue(false)]
		[__DynamicallyInvokable]
		public virtual bool ManualAddressing
		{
			[__DynamicallyInvokable]
			get
			{
				return this.manualAddressing;
			}
			[__DynamicallyInvokable]
			set
			{
				this.manualAddressing = value;
			}
		}

		// Token: 0x170014D1 RID: 5329
		// (get) Token: 0x06005498 RID: 21656 RVA: 0x0013752D File Offset: 0x0013572D
		// (set) Token: 0x06005499 RID: 21657 RVA: 0x00137535 File Offset: 0x00135735
		[DefaultValue(524288L)]
		public virtual long MaxBufferPoolSize
		{
			get
			{
				return this.maxBufferPoolSize;
			}
			set
			{
				if (value < 0L)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBeNonNegative")));
				}
				this.maxBufferPoolSize = value;
			}
		}

		// Token: 0x170014D2 RID: 5330
		// (get) Token: 0x0600549A RID: 21658 RVA: 0x00137568 File Offset: 0x00135768
		// (set) Token: 0x0600549B RID: 21659 RVA: 0x00137570 File Offset: 0x00135770
		[DefaultValue(65536L)]
		[__DynamicallyInvokable]
		public virtual long MaxReceivedMessageSize
		{
			[__DynamicallyInvokable]
			get
			{
				return this.maxReceivedMessageSize;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value <= 0L)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBePositive")));
				}
				this.maxReceivedMessageSize = value;
			}
		}

		// Token: 0x170014D3 RID: 5331
		// (get) Token: 0x0600549C RID: 21660
		[__DynamicallyInvokable]
		public abstract string Scheme { [__DynamicallyInvokable] get; }

		// Token: 0x0600549D RID: 21661 RVA: 0x001375A4 File Offset: 0x001357A4
		internal static IChannelFactory<TChannel> CreateChannelFactory<TChannel>(TransportBindingElement transport)
		{
			Binding binding = new CustomBinding(new BindingElement[]
			{
				transport
			});
			return binding.BuildChannelFactory<TChannel>(new object[0]);
		}

		// Token: 0x0600549E RID: 21662 RVA: 0x001375D0 File Offset: 0x001357D0
		internal static IChannelListener CreateChannelListener<TChannel>(TransportBindingElement transport) where TChannel : class, IChannel
		{
			Binding binding = new CustomBinding(new BindingElement[]
			{
				transport
			});
			return binding.BuildChannelListener<TChannel>(new object[0]);
		}

		// Token: 0x0600549F RID: 21663 RVA: 0x001375FC File Offset: 0x001357FC
		[__DynamicallyInvokable]
		public override T GetProperty<T>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (typeof(T) == typeof(ChannelProtectionRequirements))
			{
				ChannelProtectionRequirements protectionRequirements = this.GetProtectionRequirements(context);
				protectionRequirements.Add(context.GetInnerProperty<ChannelProtectionRequirements>() ?? new ChannelProtectionRequirements());
				return (T)((object)protectionRequirements);
			}
			Collection<BindingElement> collection = context.BindingParameters.FindAll<BindingElement>();
			T t = default(T);
			for (int i = 0; i < collection.Count; i++)
			{
				t = collection[i].GetIndividualProperty<T>();
				if (t != null)
				{
					return t;
				}
			}
			if (typeof(T) == typeof(MessageVersion))
			{
				return (T)((object)TransportDefaults.GetDefaultMessageEncoderFactory().MessageVersion);
			}
			if (typeof(T) == typeof(XmlDictionaryReaderQuotas))
			{
				return (T)((object)new XmlDictionaryReaderQuotas());
			}
			return default(T);
		}

		// Token: 0x060054A0 RID: 21664 RVA: 0x001376F4 File Offset: 0x001358F4
		private ChannelProtectionRequirements GetProtectionRequirements(AddressingVersion addressingVersion)
		{
			ChannelProtectionRequirements channelProtectionRequirements = new ChannelProtectionRequirements();
			channelProtectionRequirements.IncomingSignatureParts.AddParts(addressingVersion.SignedMessageParts);
			channelProtectionRequirements.OutgoingSignatureParts.AddParts(addressingVersion.SignedMessageParts);
			return channelProtectionRequirements;
		}

		// Token: 0x060054A1 RID: 21665 RVA: 0x0013772C File Offset: 0x0013592C
		internal ChannelProtectionRequirements GetProtectionRequirements(BindingContext context)
		{
			AddressingVersion addressingVersion = AddressingVersion.WSAddressing10;
			MessageEncodingBindingElement messageEncodingBindingElement = context.Binding.Elements.Find<MessageEncodingBindingElement>();
			if (messageEncodingBindingElement != null)
			{
				addressingVersion = messageEncodingBindingElement.MessageVersion.Addressing;
			}
			return this.GetProtectionRequirements(addressingVersion);
		}

		// Token: 0x060054A2 RID: 21666 RVA: 0x00137766 File Offset: 0x00135966
		internal static void ExportWsdlEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext endpointContext, string wsdlTransportUri, AddressingVersion addressingVersion)
		{
			TransportBindingElement.ExportWsdlEndpoint(exporter, endpointContext, wsdlTransportUri, endpointContext.Endpoint.Address, addressingVersion);
		}

		// Token: 0x060054A3 RID: 21667 RVA: 0x0013777C File Offset: 0x0013597C
		internal static void ExportWsdlEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext endpointContext, string wsdlTransportUri, EndpointAddress address, AddressingVersion addressingVersion)
		{
			if (exporter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (endpointContext == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpointContext");
			}
			BindingElementCollection bindingElementCollection = endpointContext.Endpoint.Binding.CreateBindingElements();
			if (wsdlTransportUri != null)
			{
				SoapBinding orCreateSoapBinding = SoapHelper.GetOrCreateSoapBinding(endpointContext, exporter);
				if (orCreateSoapBinding != null)
				{
					orCreateSoapBinding.Transport = wsdlTransportUri;
				}
			}
			if (endpointContext.WsdlPort != null)
			{
				WsdlExporter.WSAddressingHelper.AddAddressToWsdlPort(endpointContext.WsdlPort, address, addressingVersion);
			}
		}

		// Token: 0x060054A4 RID: 21668 RVA: 0x001377EC File Offset: 0x001359EC
		internal override bool IsMatch(BindingElement b)
		{
			if (b == null)
			{
				return false;
			}
			TransportBindingElement transportBindingElement = b as TransportBindingElement;
			return transportBindingElement != null && this.maxBufferPoolSize == transportBindingElement.MaxBufferPoolSize && this.maxReceivedMessageSize == transportBindingElement.MaxReceivedMessageSize;
		}

		// Token: 0x0400331F RID: 13087
		private bool manualAddressing;

		// Token: 0x04003320 RID: 13088
		private long maxBufferPoolSize;

		// Token: 0x04003321 RID: 13089
		private long maxReceivedMessageSize;
	}
}
