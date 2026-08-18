using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime;
using System.ServiceModel.Description;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200089A RID: 2202
	[__DynamicallyInvokable]
	public abstract class ConnectionOrientedTransportBindingElement : TransportBindingElement, IWsdlExportExtension, IPolicyExportExtension, ITransportPolicyImport
	{
		// Token: 0x060053A0 RID: 21408 RVA: 0x00134258 File Offset: 0x00132458
		internal ConnectionOrientedTransportBindingElement()
		{
			this.connectionBufferSize = 8192;
			this.hostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
			this.channelInitializationTimeout = ConnectionOrientedTransportDefaults.ChannelInitializationTimeout;
			this.maxBufferSize = 65536;
			this.maxPendingConnections = ConnectionOrientedTransportDefaults.GetMaxPendingConnections();
			this.maxOutputDelay = ConnectionOrientedTransportDefaults.MaxOutputDelay;
			this.maxPendingAccepts = ConnectionOrientedTransportDefaults.GetMaxPendingAccepts();
			this.transferMode = TransferMode.Buffered;
		}

		// Token: 0x060053A1 RID: 21409 RVA: 0x001342BC File Offset: 0x001324BC
		internal ConnectionOrientedTransportBindingElement(ConnectionOrientedTransportBindingElement elementToBeCloned) : base(elementToBeCloned)
		{
			this.connectionBufferSize = elementToBeCloned.connectionBufferSize;
			this.exposeConnectionProperty = elementToBeCloned.exposeConnectionProperty;
			this.hostNameComparisonMode = elementToBeCloned.hostNameComparisonMode;
			this.inheritBaseAddressSettings = elementToBeCloned.InheritBaseAddressSettings;
			this.channelInitializationTimeout = elementToBeCloned.ChannelInitializationTimeout;
			this.maxBufferSize = elementToBeCloned.maxBufferSize;
			this.maxBufferSizeInitialized = elementToBeCloned.maxBufferSizeInitialized;
			this.maxPendingConnections = elementToBeCloned.maxPendingConnections;
			this.maxOutputDelay = elementToBeCloned.maxOutputDelay;
			this.maxPendingAccepts = elementToBeCloned.maxPendingAccepts;
			this.transferMode = elementToBeCloned.transferMode;
			this.isMaxPendingConnectionsSet = elementToBeCloned.isMaxPendingConnectionsSet;
			this.isMaxPendingAcceptsSet = elementToBeCloned.isMaxPendingAcceptsSet;
		}

		// Token: 0x1700148B RID: 5259
		// (get) Token: 0x060053A2 RID: 21410 RVA: 0x0013436C File Offset: 0x0013256C
		// (set) Token: 0x060053A3 RID: 21411 RVA: 0x00134374 File Offset: 0x00132574
		[DefaultValue(8192)]
		[__DynamicallyInvokable]
		public int ConnectionBufferSize
		{
			[__DynamicallyInvokable]
			get
			{
				return this.connectionBufferSize;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBeNonNegative")));
				}
				this.connectionBufferSize = value;
			}
		}

		// Token: 0x1700148C RID: 5260
		// (get) Token: 0x060053A4 RID: 21412 RVA: 0x001343A6 File Offset: 0x001325A6
		// (set) Token: 0x060053A5 RID: 21413 RVA: 0x001343AE File Offset: 0x001325AE
		internal bool ExposeConnectionProperty
		{
			get
			{
				return this.exposeConnectionProperty;
			}
			set
			{
				this.exposeConnectionProperty = value;
			}
		}

		// Token: 0x1700148D RID: 5261
		// (get) Token: 0x060053A6 RID: 21414 RVA: 0x001343B7 File Offset: 0x001325B7
		// (set) Token: 0x060053A7 RID: 21415 RVA: 0x001343BF File Offset: 0x001325BF
		[DefaultValue(HostNameComparisonMode.StrongWildcard)]
		public HostNameComparisonMode HostNameComparisonMode
		{
			get
			{
				return this.hostNameComparisonMode;
			}
			set
			{
				HostNameComparisonModeHelper.Validate(value);
				this.hostNameComparisonMode = value;
			}
		}

		// Token: 0x1700148E RID: 5262
		// (get) Token: 0x060053A8 RID: 21416 RVA: 0x001343D0 File Offset: 0x001325D0
		// (set) Token: 0x060053A9 RID: 21417 RVA: 0x0013440C File Offset: 0x0013260C
		[DefaultValue(65536)]
		[__DynamicallyInvokable]
		public int MaxBufferSize
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.maxBufferSizeInitialized || this.TransferMode != TransferMode.Buffered)
				{
					return this.maxBufferSize;
				}
				long maxReceivedMessageSize = this.MaxReceivedMessageSize;
				if (maxReceivedMessageSize > 2147483647L)
				{
					return int.MaxValue;
				}
				return (int)maxReceivedMessageSize;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value <= 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBePositive")));
				}
				this.maxBufferSizeInitialized = true;
				this.maxBufferSize = value;
			}
		}

		// Token: 0x1700148F RID: 5263
		// (get) Token: 0x060053AA RID: 21418 RVA: 0x00134445 File Offset: 0x00132645
		// (set) Token: 0x060053AB RID: 21419 RVA: 0x0013444D File Offset: 0x0013264D
		public int MaxPendingConnections
		{
			get
			{
				return this.maxPendingConnections;
			}
			set
			{
				if (value <= 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBePositive")));
				}
				this.maxPendingConnections = value;
				this.isMaxPendingConnectionsSet = true;
			}
		}

		// Token: 0x17001490 RID: 5264
		// (get) Token: 0x060053AC RID: 21420 RVA: 0x00134486 File Offset: 0x00132686
		internal bool IsMaxPendingConnectionsSet
		{
			get
			{
				return this.isMaxPendingConnectionsSet;
			}
		}

		// Token: 0x17001491 RID: 5265
		// (get) Token: 0x060053AD RID: 21421 RVA: 0x0013448E File Offset: 0x0013268E
		// (set) Token: 0x060053AE RID: 21422 RVA: 0x00134496 File Offset: 0x00132696
		internal bool InheritBaseAddressSettings
		{
			get
			{
				return this.inheritBaseAddressSettings;
			}
			set
			{
				this.inheritBaseAddressSettings = value;
			}
		}

		// Token: 0x17001492 RID: 5266
		// (get) Token: 0x060053AF RID: 21423 RVA: 0x0013449F File Offset: 0x0013269F
		// (set) Token: 0x060053B0 RID: 21424 RVA: 0x001344A8 File Offset: 0x001326A8
		[DefaultValue(typeof(TimeSpan), "00:00:30")]
		public TimeSpan ChannelInitializationTimeout
		{
			get
			{
				return this.channelInitializationTimeout;
			}
			set
			{
				if (value <= TimeSpan.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("TimeSpanMustbeGreaterThanTimeSpanZero")));
				}
				if (TimeoutHelper.IsTooLarge(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRangeTooBig")));
				}
				this.channelInitializationTimeout = value;
			}
		}

		// Token: 0x17001493 RID: 5267
		// (get) Token: 0x060053B1 RID: 21425 RVA: 0x0013451B File Offset: 0x0013271B
		// (set) Token: 0x060053B2 RID: 21426 RVA: 0x00134524 File Offset: 0x00132724
		[DefaultValue(typeof(TimeSpan), "00:00:00.2")]
		public TimeSpan MaxOutputDelay
		{
			get
			{
				return this.maxOutputDelay;
			}
			set
			{
				if (value < TimeSpan.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRange0")));
				}
				if (TimeoutHelper.IsTooLarge(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRangeTooBig")));
				}
				this.maxOutputDelay = value;
			}
		}

		// Token: 0x17001494 RID: 5268
		// (get) Token: 0x060053B3 RID: 21427 RVA: 0x00134597 File Offset: 0x00132797
		// (set) Token: 0x060053B4 RID: 21428 RVA: 0x0013459F File Offset: 0x0013279F
		public int MaxPendingAccepts
		{
			get
			{
				return this.maxPendingAccepts;
			}
			set
			{
				if (value <= 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBePositive")));
				}
				this.maxPendingAccepts = value;
				this.isMaxPendingAcceptsSet = true;
			}
		}

		// Token: 0x17001495 RID: 5269
		// (get) Token: 0x060053B5 RID: 21429 RVA: 0x001345D8 File Offset: 0x001327D8
		internal bool IsMaxPendingAcceptsSet
		{
			get
			{
				return this.isMaxPendingAcceptsSet;
			}
		}

		// Token: 0x17001496 RID: 5270
		// (get) Token: 0x060053B6 RID: 21430 RVA: 0x001345E0 File Offset: 0x001327E0
		// (set) Token: 0x060053B7 RID: 21431 RVA: 0x001345E8 File Offset: 0x001327E8
		[DefaultValue(TransferMode.Buffered)]
		[__DynamicallyInvokable]
		public TransferMode TransferMode
		{
			[__DynamicallyInvokable]
			get
			{
				return this.transferMode;
			}
			[__DynamicallyInvokable]
			set
			{
				TransferModeHelper.Validate(value);
				this.transferMode = value;
			}
		}

		// Token: 0x060053B8 RID: 21432 RVA: 0x001345F8 File Offset: 0x001327F8
		[__DynamicallyInvokable]
		public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (this.TransferMode == TransferMode.Buffered)
			{
				return typeof(TChannel) == typeof(IDuplexSessionChannel);
			}
			return typeof(TChannel) == typeof(IRequestChannel);
		}

		// Token: 0x060053B9 RID: 21433 RVA: 0x00134654 File Offset: 0x00132854
		public override bool CanBuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (this.TransferMode == TransferMode.Buffered)
			{
				return typeof(TChannel) == typeof(IDuplexSessionChannel);
			}
			return typeof(TChannel) == typeof(IReplyChannel);
		}

		// Token: 0x060053BA RID: 21434 RVA: 0x001346B0 File Offset: 0x001328B0
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
			ICollection<XmlElement> bindingAssertions = context.GetBindingAssertions();
			if (TransferModeHelper.IsRequestStreamed(this.TransferMode) || TransferModeHelper.IsResponseStreamed(this.TransferMode))
			{
				bindingAssertions.Add(new XmlDocument().CreateElement("msf", "Streamed", "http://schemas.microsoft.com/ws/2006/05/framing/policy"));
			}
			bool flag;
			MessageEncodingBindingElement messageEncodingBindingElement = this.FindMessageEncodingBindingElement(context.BindingElements, out flag);
			if (flag && messageEncodingBindingElement is IPolicyExportExtension)
			{
				messageEncodingBindingElement = new BinaryMessageEncodingBindingElement();
				((IPolicyExportExtension)messageEncodingBindingElement).ExportPolicy(exporter, context);
			}
			WsdlExporter.WSAddressingHelper.AddWSAddressingAssertion(exporter, context, messageEncodingBindingElement.MessageVersion.Addressing);
		}

		// Token: 0x060053BB RID: 21435 RVA: 0x00134762 File Offset: 0x00132962
		void IWsdlExportExtension.ExportContract(WsdlExporter exporter, WsdlContractConversionContext context)
		{
		}

		// Token: 0x17001497 RID: 5271
		// (get) Token: 0x060053BC RID: 21436
		internal abstract string WsdlTransportUri { get; }

		// Token: 0x060053BD RID: 21437 RVA: 0x00134764 File Offset: 0x00132964
		void IWsdlExportExtension.ExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext endpointContext)
		{
			bool flag;
			MessageEncodingBindingElement messageEncodingBindingElement = this.FindMessageEncodingBindingElement(endpointContext, out flag);
			TransportBindingElement.ExportWsdlEndpoint(exporter, endpointContext, this.WsdlTransportUri, messageEncodingBindingElement.MessageVersion.Addressing);
		}

		// Token: 0x060053BE RID: 21438 RVA: 0x00134793 File Offset: 0x00132993
		void ITransportPolicyImport.ImportPolicy(MetadataImporter importer, PolicyConversionContext policyContext)
		{
			if (PolicyConversionContext.FindAssertion(policyContext.GetBindingAssertions(), "Streamed", "http://schemas.microsoft.com/ws/2006/05/framing/policy", true) != null)
			{
				this.TransferMode = TransferMode.Streamed;
			}
			WindowsStreamSecurityBindingElement.ImportPolicy(importer, policyContext);
			SslStreamSecurityBindingElement.ImportPolicy(importer, policyContext);
		}

		// Token: 0x060053BF RID: 21439 RVA: 0x001347C4 File Offset: 0x001329C4
		[__DynamicallyInvokable]
		public override T GetProperty<T>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (typeof(T) == typeof(TransferMode))
			{
				return (T)((object)this.TransferMode);
			}
			return base.GetProperty<T>(context);
		}

		// Token: 0x060053C0 RID: 21440 RVA: 0x00134818 File Offset: 0x00132A18
		internal override bool IsMatch(BindingElement b)
		{
			if (!base.IsMatch(b))
			{
				return false;
			}
			ConnectionOrientedTransportBindingElement connectionOrientedTransportBindingElement = b as ConnectionOrientedTransportBindingElement;
			return connectionOrientedTransportBindingElement != null && this.connectionBufferSize == connectionOrientedTransportBindingElement.connectionBufferSize && this.hostNameComparisonMode == connectionOrientedTransportBindingElement.hostNameComparisonMode && this.inheritBaseAddressSettings == connectionOrientedTransportBindingElement.inheritBaseAddressSettings && !(this.channelInitializationTimeout != connectionOrientedTransportBindingElement.channelInitializationTimeout) && this.maxBufferSize == connectionOrientedTransportBindingElement.maxBufferSize && this.maxPendingConnections == connectionOrientedTransportBindingElement.maxPendingConnections && !(this.maxOutputDelay != connectionOrientedTransportBindingElement.maxOutputDelay) && this.maxPendingAccepts == connectionOrientedTransportBindingElement.maxPendingAccepts && this.transferMode == connectionOrientedTransportBindingElement.transferMode;
		}

		// Token: 0x060053C1 RID: 21441 RVA: 0x001348D8 File Offset: 0x00132AD8
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

		// Token: 0x060053C2 RID: 21442 RVA: 0x001348FC File Offset: 0x00132AFC
		private MessageEncodingBindingElement FindMessageEncodingBindingElement(WsdlEndpointConversionContext endpointContext, out bool createdNew)
		{
			BindingElementCollection bindingElements = endpointContext.Endpoint.Binding.CreateBindingElements();
			return this.FindMessageEncodingBindingElement(bindingElements, out createdNew);
		}

		// Token: 0x060053C3 RID: 21443 RVA: 0x00134922 File Offset: 0x00132B22
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeMaxPendingAccepts()
		{
			return this.isMaxPendingAcceptsSet;
		}

		// Token: 0x060053C4 RID: 21444 RVA: 0x0013492A File Offset: 0x00132B2A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeMaxPendingConnections()
		{
			return this.isMaxPendingConnectionsSet;
		}

		// Token: 0x040032DB RID: 13019
		private int connectionBufferSize;

		// Token: 0x040032DC RID: 13020
		private bool exposeConnectionProperty;

		// Token: 0x040032DD RID: 13021
		private HostNameComparisonMode hostNameComparisonMode;

		// Token: 0x040032DE RID: 13022
		private bool inheritBaseAddressSettings;

		// Token: 0x040032DF RID: 13023
		private TimeSpan channelInitializationTimeout;

		// Token: 0x040032E0 RID: 13024
		private int maxBufferSize;

		// Token: 0x040032E1 RID: 13025
		private bool maxBufferSizeInitialized;

		// Token: 0x040032E2 RID: 13026
		private int maxPendingConnections;

		// Token: 0x040032E3 RID: 13027
		private TimeSpan maxOutputDelay;

		// Token: 0x040032E4 RID: 13028
		private int maxPendingAccepts;

		// Token: 0x040032E5 RID: 13029
		private TransferMode transferMode;

		// Token: 0x040032E6 RID: 13030
		private bool isMaxPendingConnectionsSet;

		// Token: 0x040032E7 RID: 13031
		private bool isMaxPendingAcceptsSet;
	}
}
