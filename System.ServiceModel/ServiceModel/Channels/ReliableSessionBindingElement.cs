using System;
using System.ComponentModel;
using System.Runtime;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000980 RID: 2432
	public sealed class ReliableSessionBindingElement : BindingElement, IPolicyExportExtension
	{
		// Token: 0x06005E02 RID: 24066 RVA: 0x0015BB64 File Offset: 0x00159D64
		public ReliableSessionBindingElement()
		{
		}

		// Token: 0x06005E03 RID: 24067 RVA: 0x0015BBBC File Offset: 0x00159DBC
		internal ReliableSessionBindingElement(ReliableSessionBindingElement elementToBeCloned) : base(elementToBeCloned)
		{
			this.AcknowledgementInterval = elementToBeCloned.AcknowledgementInterval;
			this.FlowControlEnabled = elementToBeCloned.FlowControlEnabled;
			this.InactivityTimeout = elementToBeCloned.InactivityTimeout;
			this.MaxPendingChannels = elementToBeCloned.MaxPendingChannels;
			this.MaxRetryCount = elementToBeCloned.MaxRetryCount;
			this.MaxTransferWindowSize = elementToBeCloned.MaxTransferWindowSize;
			this.Ordered = elementToBeCloned.Ordered;
			this.ReliableMessagingVersion = elementToBeCloned.ReliableMessagingVersion;
			this.internalDuplexBindingElement = elementToBeCloned.internalDuplexBindingElement;
		}

		// Token: 0x06005E04 RID: 24068 RVA: 0x0015BC80 File Offset: 0x00159E80
		public ReliableSessionBindingElement(bool ordered)
		{
			this.ordered = ordered;
		}

		// Token: 0x1700168E RID: 5774
		// (get) Token: 0x06005E05 RID: 24069 RVA: 0x0015BCDE File Offset: 0x00159EDE
		// (set) Token: 0x06005E06 RID: 24070 RVA: 0x0015BCE8 File Offset: 0x00159EE8
		[DefaultValue(typeof(TimeSpan), "00:00:00.2")]
		public TimeSpan AcknowledgementInterval
		{
			get
			{
				return this.acknowledgementInterval;
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
				this.acknowledgementInterval = value;
			}
		}

		// Token: 0x1700168F RID: 5775
		// (get) Token: 0x06005E07 RID: 24071 RVA: 0x0015BD5B File Offset: 0x00159F5B
		// (set) Token: 0x06005E08 RID: 24072 RVA: 0x0015BD63 File Offset: 0x00159F63
		[DefaultValue(true)]
		public bool FlowControlEnabled
		{
			get
			{
				return this.flowControlEnabled;
			}
			set
			{
				this.flowControlEnabled = value;
			}
		}

		// Token: 0x17001690 RID: 5776
		// (get) Token: 0x06005E09 RID: 24073 RVA: 0x0015BD6C File Offset: 0x00159F6C
		// (set) Token: 0x06005E0A RID: 24074 RVA: 0x0015BD74 File Offset: 0x00159F74
		[DefaultValue(typeof(TimeSpan), "00:10:00")]
		public TimeSpan InactivityTimeout
		{
			get
			{
				return this.inactivityTimeout;
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
				this.inactivityTimeout = value;
			}
		}

		// Token: 0x17001691 RID: 5777
		// (get) Token: 0x06005E0B RID: 24075 RVA: 0x0015BDE7 File Offset: 0x00159FE7
		// (set) Token: 0x06005E0C RID: 24076 RVA: 0x0015BDF0 File Offset: 0x00159FF0
		[DefaultValue(4)]
		public int MaxPendingChannels
		{
			get
			{
				return this.maxPendingChannels;
			}
			set
			{
				if (value <= 0 || value > 16384)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBeInRange", new object[]
					{
						0,
						16384
					})));
				}
				this.maxPendingChannels = value;
			}
		}

		// Token: 0x17001692 RID: 5778
		// (get) Token: 0x06005E0D RID: 24077 RVA: 0x0015BE51 File Offset: 0x0015A051
		// (set) Token: 0x06005E0E RID: 24078 RVA: 0x0015BE59 File Offset: 0x0015A059
		[DefaultValue(8)]
		public int MaxRetryCount
		{
			get
			{
				return this.maxRetryCount;
			}
			set
			{
				if (value <= 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBePositive")));
				}
				this.maxRetryCount = value;
			}
		}

		// Token: 0x17001693 RID: 5779
		// (get) Token: 0x06005E0F RID: 24079 RVA: 0x0015BE8B File Offset: 0x0015A08B
		// (set) Token: 0x06005E10 RID: 24080 RVA: 0x0015BE94 File Offset: 0x0015A094
		[DefaultValue(8)]
		public int MaxTransferWindowSize
		{
			get
			{
				return this.maxTransferWindowSize;
			}
			set
			{
				if (value <= 0 || value > 4096)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBeInRange", new object[]
					{
						0,
						4096
					})));
				}
				this.maxTransferWindowSize = value;
			}
		}

		// Token: 0x17001694 RID: 5780
		// (get) Token: 0x06005E11 RID: 24081 RVA: 0x0015BEF5 File Offset: 0x0015A0F5
		// (set) Token: 0x06005E12 RID: 24082 RVA: 0x0015BEFD File Offset: 0x0015A0FD
		[DefaultValue(true)]
		public bool Ordered
		{
			get
			{
				return this.ordered;
			}
			set
			{
				this.ordered = value;
			}
		}

		// Token: 0x17001695 RID: 5781
		// (get) Token: 0x06005E13 RID: 24083 RVA: 0x0015BF06 File Offset: 0x0015A106
		// (set) Token: 0x06005E14 RID: 24084 RVA: 0x0015BF0E File Offset: 0x0015A10E
		[DefaultValue(typeof(ReliableMessagingVersion), "WSReliableMessagingFebruary2005")]
		public ReliableMessagingVersion ReliableMessagingVersion
		{
			get
			{
				return this.reliableMessagingVersion;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				if (!ReliableMessagingVersion.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.reliableMessagingVersion = value;
			}
		}

		// Token: 0x17001696 RID: 5782
		// (get) Token: 0x06005E15 RID: 24085 RVA: 0x0015BF48 File Offset: 0x0015A148
		private static MessagePartSpecification BodyOnly
		{
			get
			{
				if (ReliableSessionBindingElement.bodyOnly == null)
				{
					MessagePartSpecification messagePartSpecification = new MessagePartSpecification(true);
					messagePartSpecification.MakeReadOnly();
					ReliableSessionBindingElement.bodyOnly = messagePartSpecification;
				}
				return ReliableSessionBindingElement.bodyOnly;
			}
		}

		// Token: 0x06005E16 RID: 24086 RVA: 0x0015BF74 File Offset: 0x0015A174
		public override BindingElement Clone()
		{
			return new ReliableSessionBindingElement(this);
		}

		// Token: 0x06005E17 RID: 24087 RVA: 0x0015BF7C File Offset: 0x0015A17C
		public override T GetProperty<T>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (typeof(T) == typeof(ChannelProtectionRequirements))
			{
				ChannelProtectionRequirements protectionRequirements = this.GetProtectionRequirements();
				protectionRequirements.Add(context.GetInnerProperty<ChannelProtectionRequirements>() ?? new ChannelProtectionRequirements());
				return (T)((object)protectionRequirements);
			}
			if (typeof(T) == typeof(IBindingDeliveryCapabilities))
			{
				return (T)((object)new ReliableSessionBindingElement.BindingDeliveryCapabilitiesHelper(this, context.GetInnerProperty<IBindingDeliveryCapabilities>()));
			}
			return context.GetInnerProperty<T>();
		}

		// Token: 0x06005E18 RID: 24088 RVA: 0x0015C010 File Offset: 0x0015A210
		private ChannelProtectionRequirements GetProtectionRequirements()
		{
			ChannelProtectionRequirements channelProtectionRequirements = new ChannelProtectionRequirements();
			MessagePartSpecification signedReliabilityMessageParts = WsrmIndex.GetSignedReliabilityMessageParts(this.reliableMessagingVersion);
			channelProtectionRequirements.IncomingSignatureParts.AddParts(signedReliabilityMessageParts);
			channelProtectionRequirements.OutgoingSignatureParts.AddParts(signedReliabilityMessageParts);
			if (this.reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				ScopedMessagePartSpecification signaturePart = channelProtectionRequirements.IncomingSignatureParts;
				ScopedMessagePartSpecification encryptionPart = channelProtectionRequirements.IncomingEncryptionParts;
				ReliableSessionBindingElement.ProtectProtocolMessage(signaturePart, encryptionPart, "http://schemas.xmlsoap.org/ws/2005/02/rm/AckRequested");
				ReliableSessionBindingElement.ProtectProtocolMessage(signaturePart, encryptionPart, "http://schemas.xmlsoap.org/ws/2005/02/rm/CreateSequence");
				ReliableSessionBindingElement.ProtectProtocolMessage(signaturePart, encryptionPart, "http://schemas.xmlsoap.org/ws/2005/02/rm/SequenceAcknowledgement");
				ReliableSessionBindingElement.ProtectProtocolMessage(signaturePart, encryptionPart, "http://schemas.xmlsoap.org/ws/2005/02/rm/LastMessage");
				ReliableSessionBindingElement.ProtectProtocolMessage(signaturePart, encryptionPart, "http://schemas.xmlsoap.org/ws/2005/02/rm/TerminateSequence");
				signaturePart = channelProtectionRequirements.OutgoingSignatureParts;
				encryptionPart = channelProtectionRequirements.OutgoingEncryptionParts;
				ReliableSessionBindingElement.ProtectProtocolMessage(signaturePart, encryptionPart, "http://schemas.xmlsoap.org/ws/2005/02/rm/CreateSequenceResponse");
				ReliableSessionBindingElement.ProtectProtocolMessage(signaturePart, encryptionPart, "http://schemas.xmlsoap.org/ws/2005/02/rm/SequenceAcknowledgement");
				ReliableSessionBindingElement.ProtectProtocolMessage(signaturePart, encryptionPart, "http://schemas.xmlsoap.org/ws/2005/02/rm/LastMessage");
				ReliableSessionBindingElement.ProtectProtocolMessage(signaturePart, encryptionPart, "http://schemas.xmlsoap.org/ws/2005/02/rm/TerminateSequence");
			}
			else
			{
				if (this.reliableMessagingVersion != ReliableMessagingVersion.WSReliableMessaging11)
				{
					throw Fx.AssertAndThrow("Reliable messaging version not supported.");
				}
				ScopedMessagePartSpecification signaturePart2 = channelProtectionRequirements.IncomingSignatureParts;
				ScopedMessagePartSpecification encryptionPart2 = channelProtectionRequirements.IncomingEncryptionParts;
				ReliableSessionBindingElement.ProtectProtocolMessage(signaturePart2, encryptionPart2, "http://docs.oasis-open.org/ws-rx/wsrm/200702/AckRequested");
				ReliableSessionBindingElement.ProtectProtocolMessage(signaturePart2, encryptionPart2, "http://docs.oasis-open.org/ws-rx/wsrm/200702/CloseSequence");
				ReliableSessionBindingElement.ProtectProtocolMessage(signaturePart2, encryptionPart2, "http://docs.oasis-open.org/ws-rx/wsrm/200702/CloseSequenceResponse");
				ReliableSessionBindingElement.ProtectProtocolMessage(signaturePart2, encryptionPart2, "http://docs.oasis-open.org/ws-rx/wsrm/200702/CreateSequence");
				ReliableSessionBindingElement.ProtectProtocolMessage(signaturePart2, encryptionPart2, "http://docs.oasis-open.org/ws-rx/wsrm/200702/fault");
				ReliableSessionBindingElement.ProtectProtocolMessage(signaturePart2, encryptionPart2, "http://docs.oasis-open.org/ws-rx/wsrm/200702/SequenceAcknowledgement");
				ReliableSessionBindingElement.ProtectProtocolMessage(signaturePart2, encryptionPart2, "http://docs.oasis-open.org/ws-rx/wsrm/200702/TerminateSequence");
				ReliableSessionBindingElement.ProtectProtocolMessage(signaturePart2, encryptionPart2, "http://docs.oasis-open.org/ws-rx/wsrm/200702/TerminateSequenceResponse");
				signaturePart2 = channelProtectionRequirements.OutgoingSignatureParts;
				encryptionPart2 = channelProtectionRequirements.OutgoingEncryptionParts;
				ReliableSessionBindingElement.ProtectProtocolMessage(signaturePart2, encryptionPart2, "http://docs.oasis-open.org/ws-rx/wsrm/200702/AckRequested");
				ReliableSessionBindingElement.ProtectProtocolMessage(signaturePart2, encryptionPart2, "http://docs.oasis-open.org/ws-rx/wsrm/200702/CloseSequence");
				ReliableSessionBindingElement.ProtectProtocolMessage(signaturePart2, encryptionPart2, "http://docs.oasis-open.org/ws-rx/wsrm/200702/CloseSequenceResponse");
				ReliableSessionBindingElement.ProtectProtocolMessage(signaturePart2, encryptionPart2, "http://docs.oasis-open.org/ws-rx/wsrm/200702/CreateSequenceResponse");
				ReliableSessionBindingElement.ProtectProtocolMessage(signaturePart2, encryptionPart2, "http://docs.oasis-open.org/ws-rx/wsrm/200702/fault");
				ReliableSessionBindingElement.ProtectProtocolMessage(signaturePart2, encryptionPart2, "http://docs.oasis-open.org/ws-rx/wsrm/200702/SequenceAcknowledgement");
				ReliableSessionBindingElement.ProtectProtocolMessage(signaturePart2, encryptionPart2, "http://docs.oasis-open.org/ws-rx/wsrm/200702/TerminateSequence");
				ReliableSessionBindingElement.ProtectProtocolMessage(signaturePart2, encryptionPart2, "http://docs.oasis-open.org/ws-rx/wsrm/200702/TerminateSequenceResponse");
			}
			return channelProtectionRequirements;
		}

		// Token: 0x06005E19 RID: 24089 RVA: 0x0015C204 File Offset: 0x0015A404
		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			this.VerifyTransportMode(context);
			this.SetSecuritySettings(context);
			InternalDuplexBindingElement.AddDuplexFactorySupport(context, ref this.internalDuplexBindingElement);
			if (typeof(TChannel) == typeof(IOutputSessionChannel))
			{
				if (context.CanBuildInnerChannelFactory<IRequestSessionChannel>())
				{
					return (IChannelFactory<TChannel>)new ReliableChannelFactory<TChannel, IRequestSessionChannel>(this, context.BuildInnerChannelFactory<IRequestSessionChannel>(), context.Binding);
				}
				if (context.CanBuildInnerChannelFactory<IRequestChannel>())
				{
					return (IChannelFactory<TChannel>)new ReliableChannelFactory<TChannel, IRequestChannel>(this, context.BuildInnerChannelFactory<IRequestChannel>(), context.Binding);
				}
				if (context.CanBuildInnerChannelFactory<IDuplexSessionChannel>())
				{
					return (IChannelFactory<TChannel>)new ReliableChannelFactory<TChannel, IDuplexSessionChannel>(this, context.BuildInnerChannelFactory<IDuplexSessionChannel>(), context.Binding);
				}
				if (context.CanBuildInnerChannelFactory<IDuplexChannel>())
				{
					return (IChannelFactory<TChannel>)new ReliableChannelFactory<TChannel, IDuplexChannel>(this, context.BuildInnerChannelFactory<IDuplexChannel>(), context.Binding);
				}
			}
			else if (typeof(TChannel) == typeof(IDuplexSessionChannel))
			{
				if (context.CanBuildInnerChannelFactory<IDuplexSessionChannel>())
				{
					return (IChannelFactory<TChannel>)new ReliableChannelFactory<TChannel, IDuplexSessionChannel>(this, context.BuildInnerChannelFactory<IDuplexSessionChannel>(), context.Binding);
				}
				if (context.CanBuildInnerChannelFactory<IDuplexChannel>())
				{
					return (IChannelFactory<TChannel>)new ReliableChannelFactory<TChannel, IDuplexChannel>(this, context.BuildInnerChannelFactory<IDuplexChannel>(), context.Binding);
				}
			}
			else if (typeof(TChannel) == typeof(IRequestSessionChannel))
			{
				if (context.CanBuildInnerChannelFactory<IRequestSessionChannel>())
				{
					return (IChannelFactory<TChannel>)new ReliableChannelFactory<TChannel, IRequestSessionChannel>(this, context.BuildInnerChannelFactory<IRequestSessionChannel>(), context.Binding);
				}
				if (context.CanBuildInnerChannelFactory<IRequestChannel>())
				{
					return (IChannelFactory<TChannel>)new ReliableChannelFactory<TChannel, IRequestChannel>(this, context.BuildInnerChannelFactory<IRequestChannel>(), context.Binding);
				}
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("TChannel", SR.GetString("ChannelTypeNotSupported", new object[]
			{
				typeof(TChannel)
			}));
		}

		// Token: 0x06005E1A RID: 24090 RVA: 0x0015C3C4 File Offset: 0x0015A5C4
		public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			InternalDuplexBindingElement.AddDuplexFactorySupport(context, ref this.internalDuplexBindingElement);
			if (typeof(TChannel) == typeof(IOutputSessionChannel))
			{
				return context.CanBuildInnerChannelFactory<IRequestSessionChannel>() || context.CanBuildInnerChannelFactory<IRequestChannel>() || context.CanBuildInnerChannelFactory<IDuplexSessionChannel>() || context.CanBuildInnerChannelFactory<IDuplexChannel>();
			}
			if (typeof(TChannel) == typeof(IDuplexSessionChannel))
			{
				return context.CanBuildInnerChannelFactory<IDuplexSessionChannel>() || context.CanBuildInnerChannelFactory<IDuplexChannel>();
			}
			return typeof(TChannel) == typeof(IRequestSessionChannel) && (context.CanBuildInnerChannelFactory<IRequestSessionChannel>() || context.CanBuildInnerChannelFactory<IRequestChannel>());
		}

		// Token: 0x06005E1B RID: 24091 RVA: 0x0015C488 File Offset: 0x0015A688
		public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			this.VerifyTransportMode(context);
			this.SetSecuritySettings(context);
			IMessageFilterTable<EndpointAddress> localAddresses = context.BindingParameters.Find<IMessageFilterTable<EndpointAddress>>();
			InternalDuplexBindingElement.AddDuplexListenerSupport(context, ref this.internalDuplexBindingElement);
			if (typeof(TChannel) == typeof(IInputSessionChannel))
			{
				ReliableChannelListenerBase<IInputSessionChannel> reliableChannelListenerBase = null;
				if (context.CanBuildInnerChannelListener<IReplySessionChannel>())
				{
					reliableChannelListenerBase = new ReliableInputListenerOverReplySession(this, context);
				}
				else if (context.CanBuildInnerChannelListener<IReplyChannel>())
				{
					reliableChannelListenerBase = new ReliableInputListenerOverReply(this, context);
				}
				else if (context.CanBuildInnerChannelListener<IDuplexSessionChannel>())
				{
					reliableChannelListenerBase = new ReliableInputListenerOverDuplexSession(this, context);
				}
				else if (context.CanBuildInnerChannelListener<IDuplexChannel>())
				{
					reliableChannelListenerBase = new ReliableInputListenerOverDuplex(this, context);
				}
				if (reliableChannelListenerBase != null)
				{
					reliableChannelListenerBase.LocalAddresses = localAddresses;
					return (IChannelListener<TChannel>)reliableChannelListenerBase;
				}
			}
			else if (typeof(TChannel) == typeof(IDuplexSessionChannel))
			{
				ReliableChannelListenerBase<IDuplexSessionChannel> reliableChannelListenerBase2 = null;
				if (context.CanBuildInnerChannelListener<IDuplexSessionChannel>())
				{
					reliableChannelListenerBase2 = new ReliableDuplexListenerOverDuplexSession(this, context);
				}
				else if (context.CanBuildInnerChannelListener<IDuplexChannel>())
				{
					reliableChannelListenerBase2 = new ReliableDuplexListenerOverDuplex(this, context);
				}
				if (reliableChannelListenerBase2 != null)
				{
					reliableChannelListenerBase2.LocalAddresses = localAddresses;
					return (IChannelListener<TChannel>)reliableChannelListenerBase2;
				}
			}
			else if (typeof(TChannel) == typeof(IReplySessionChannel))
			{
				ReliableChannelListenerBase<IReplySessionChannel> reliableChannelListenerBase3 = null;
				if (context.CanBuildInnerChannelListener<IReplySessionChannel>())
				{
					reliableChannelListenerBase3 = new ReliableReplyListenerOverReplySession(this, context);
				}
				else if (context.CanBuildInnerChannelListener<IReplyChannel>())
				{
					reliableChannelListenerBase3 = new ReliableReplyListenerOverReply(this, context);
				}
				if (reliableChannelListenerBase3 != null)
				{
					reliableChannelListenerBase3.LocalAddresses = localAddresses;
					return (IChannelListener<TChannel>)reliableChannelListenerBase3;
				}
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("TChannel", SR.GetString("ChannelTypeNotSupported", new object[]
			{
				typeof(TChannel)
			}));
		}

		// Token: 0x06005E1C RID: 24092 RVA: 0x0015C614 File Offset: 0x0015A814
		public override bool CanBuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			InternalDuplexBindingElement.AddDuplexListenerSupport(context, ref this.internalDuplexBindingElement);
			if (typeof(TChannel) == typeof(IInputSessionChannel))
			{
				return context.CanBuildInnerChannelListener<IReplySessionChannel>() || context.CanBuildInnerChannelListener<IReplyChannel>() || context.CanBuildInnerChannelListener<IDuplexSessionChannel>() || context.CanBuildInnerChannelListener<IDuplexChannel>();
			}
			if (typeof(TChannel) == typeof(IDuplexSessionChannel))
			{
				return context.CanBuildInnerChannelListener<IDuplexSessionChannel>() || context.CanBuildInnerChannelListener<IDuplexChannel>();
			}
			return typeof(TChannel) == typeof(IReplySessionChannel) && (context.CanBuildInnerChannelListener<IReplySessionChannel>() || context.CanBuildInnerChannelListener<IReplyChannel>());
		}

		// Token: 0x06005E1D RID: 24093 RVA: 0x0015C6D8 File Offset: 0x0015A8D8
		internal override bool IsMatch(BindingElement b)
		{
			if (b == null)
			{
				return false;
			}
			ReliableSessionBindingElement reliableSessionBindingElement = b as ReliableSessionBindingElement;
			return reliableSessionBindingElement != null && !(this.acknowledgementInterval != reliableSessionBindingElement.acknowledgementInterval) && this.flowControlEnabled == reliableSessionBindingElement.flowControlEnabled && !(this.inactivityTimeout != reliableSessionBindingElement.inactivityTimeout) && this.maxPendingChannels == reliableSessionBindingElement.maxPendingChannels && this.maxRetryCount == reliableSessionBindingElement.maxRetryCount && this.maxTransferWindowSize == reliableSessionBindingElement.maxTransferWindowSize && this.ordered == reliableSessionBindingElement.ordered && this.reliableMessagingVersion == reliableSessionBindingElement.reliableMessagingVersion;
		}

		// Token: 0x06005E1E RID: 24094 RVA: 0x0015C781 File Offset: 0x0015A981
		private static void ProtectProtocolMessage(ScopedMessagePartSpecification signaturePart, ScopedMessagePartSpecification encryptionPart, string action)
		{
			signaturePart.AddParts(ReliableSessionBindingElement.BodyOnly, action);
			encryptionPart.AddParts(MessagePartSpecification.NoParts, action);
		}

		// Token: 0x06005E1F RID: 24095 RVA: 0x0015C79C File Offset: 0x0015A99C
		private void SetSecuritySettings(BindingContext context)
		{
			SecurityBindingElement securityBindingElement = context.RemainingBindingElements.Find<SecurityBindingElement>();
			if (securityBindingElement != null)
			{
				securityBindingElement.LocalServiceSettings.ReconnectTransportOnFailure = true;
			}
		}

		// Token: 0x06005E20 RID: 24096 RVA: 0x0015C7C4 File Offset: 0x0015A9C4
		private void VerifyTransportMode(BindingContext context)
		{
			TransportBindingElement transportBindingElement = context.RemainingBindingElements.Find<TransportBindingElement>();
			if (transportBindingElement != null && transportBindingElement.ManualAddressing)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ManualAddressingNotSupported")));
			}
			ConnectionOrientedTransportBindingElement connectionOrientedTransportBindingElement = transportBindingElement as ConnectionOrientedTransportBindingElement;
			HttpTransportBindingElement httpTransportBindingElement = transportBindingElement as HttpTransportBindingElement;
			TransferMode transferMode;
			if (connectionOrientedTransportBindingElement != null)
			{
				transferMode = connectionOrientedTransportBindingElement.TransferMode;
			}
			else if (httpTransportBindingElement != null)
			{
				transferMode = httpTransportBindingElement.TransferMode;
			}
			else
			{
				transferMode = TransferMode.Buffered;
			}
			if (transferMode != TransferMode.Buffered)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TransferModeNotSupported", new object[]
				{
					transferMode,
					base.GetType().Name
				})));
			}
		}

		// Token: 0x06005E21 RID: 24097 RVA: 0x0015C864 File Offset: 0x0015AA64
		void IPolicyExportExtension.ExportPolicy(MetadataExporter exporter, PolicyConversionContext context)
		{
			if (exporter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (context.BindingElements != null)
			{
				BindingElementCollection bindingElements = context.BindingElements;
				ReliableSessionBindingElement reliableSessionBindingElement = bindingElements.Find<ReliableSessionBindingElement>();
				if (reliableSessionBindingElement != null)
				{
					XmlElement item = reliableSessionBindingElement.CreateReliabilityAssertion(exporter.PolicyVersion, bindingElements);
					context.GetBindingAssertions().Add(item);
				}
			}
		}

		// Token: 0x06005E22 RID: 24098 RVA: 0x0015C8CC File Offset: 0x0015AACC
		private static XmlElement CreatePolicyElement(PolicyVersion policyVersion, XmlDocument doc)
		{
			string localName = "Policy";
			string @namespace = policyVersion.Namespace;
			string prefix = "wsp";
			return doc.CreateElement(prefix, localName, @namespace);
		}

		// Token: 0x06005E23 RID: 24099 RVA: 0x0015C8F8 File Offset: 0x0015AAF8
		private XmlElement CreateReliabilityAssertion(PolicyVersion policyVersion, BindingElementCollection bindingElements)
		{
			XmlDocument xmlDocument = new XmlDocument();
			string text;
			string text2;
			string prefix;
			string namespaceURI;
			if (this.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				text = "wsrm";
				text2 = "http://schemas.xmlsoap.org/ws/2005/02/rm/policy";
				prefix = text;
				namespaceURI = text2;
			}
			else
			{
				text = "wsrmp";
				text2 = "http://docs.oasis-open.org/ws-rx/wsrmp/200702";
				prefix = "netrmp";
				namespaceURI = "http://schemas.microsoft.com/ws-rx/wsrmp/200702";
			}
			XmlElement xmlElement = xmlDocument.CreateElement(text, "RMAssertion", text2);
			if (this.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11)
			{
				XmlElement xmlElement2 = ReliableSessionBindingElement.CreatePolicyElement(policyVersion, xmlDocument);
				if (ReliableSessionBindingElement.IsSecureConversationEnabled(bindingElements))
				{
					XmlElement newChild = xmlDocument.CreateElement(text, "SequenceSTR", text2);
					xmlElement2.AppendChild(newChild);
				}
				XmlElement xmlElement3 = xmlDocument.CreateElement(text, "DeliveryAssurance", text2);
				XmlElement xmlElement4 = ReliableSessionBindingElement.CreatePolicyElement(policyVersion, xmlDocument);
				XmlElement newChild2 = xmlDocument.CreateElement(text, "ExactlyOnce", text2);
				xmlElement4.AppendChild(newChild2);
				if (this.ordered)
				{
					XmlElement newChild3 = xmlDocument.CreateElement(text, "InOrder", text2);
					xmlElement4.AppendChild(newChild3);
				}
				xmlElement3.AppendChild(xmlElement4);
				xmlElement2.AppendChild(xmlElement3);
				xmlElement.AppendChild(xmlElement2);
			}
			XmlElement xmlElement5 = xmlDocument.CreateElement(prefix, "InactivityTimeout", namespaceURI);
			ReliableSessionBindingElement.WriteMillisecondsAttribute(xmlElement5, this.InactivityTimeout);
			xmlElement.AppendChild(xmlElement5);
			xmlElement5 = xmlDocument.CreateElement(prefix, "AcknowledgementInterval", namespaceURI);
			ReliableSessionBindingElement.WriteMillisecondsAttribute(xmlElement5, this.AcknowledgementInterval);
			xmlElement.AppendChild(xmlElement5);
			return xmlElement;
		}

		// Token: 0x06005E24 RID: 24100 RVA: 0x0015CA50 File Offset: 0x0015AC50
		private static bool IsSecureConversationEnabled(BindingElementCollection bindingElements)
		{
			bool flag = false;
			int i = 0;
			while (i < bindingElements.Count)
			{
				if (!flag)
				{
					ReliableSessionBindingElement reliableSessionBindingElement = bindingElements[i] as ReliableSessionBindingElement;
					flag = (reliableSessionBindingElement != null);
					i++;
				}
				else
				{
					SecurityBindingElement securityBindingElement = bindingElements[i] as SecurityBindingElement;
					if (securityBindingElement != null)
					{
						SecurityBindingElement securityBindingElement2;
						return SecurityBindingElement.IsSecureConversationBinding(securityBindingElement, true, out securityBindingElement2) || SecurityBindingElement.IsSecureConversationBinding(securityBindingElement, false, out securityBindingElement2);
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x06005E25 RID: 24101 RVA: 0x0015CAB0 File Offset: 0x0015ACB0
		private static void WriteMillisecondsAttribute(XmlElement childElement, TimeSpan timeSpan)
		{
			ulong value = Convert.ToUInt64(timeSpan.TotalMilliseconds);
			childElement.SetAttribute("Milliseconds", XmlConvert.ToString(value));
		}

		// Token: 0x040037D3 RID: 14291
		private TimeSpan acknowledgementInterval = ReliableSessionDefaults.AcknowledgementInterval;

		// Token: 0x040037D4 RID: 14292
		private bool flowControlEnabled = true;

		// Token: 0x040037D5 RID: 14293
		private TimeSpan inactivityTimeout = ReliableSessionDefaults.InactivityTimeout;

		// Token: 0x040037D6 RID: 14294
		private int maxPendingChannels = 4;

		// Token: 0x040037D7 RID: 14295
		private int maxRetryCount = 8;

		// Token: 0x040037D8 RID: 14296
		private int maxTransferWindowSize = 8;

		// Token: 0x040037D9 RID: 14297
		private bool ordered = true;

		// Token: 0x040037DA RID: 14298
		private ReliableMessagingVersion reliableMessagingVersion = ReliableMessagingVersion.Default;

		// Token: 0x040037DB RID: 14299
		private InternalDuplexBindingElement internalDuplexBindingElement;

		// Token: 0x040037DC RID: 14300
		private static MessagePartSpecification bodyOnly;

		// Token: 0x02000DF2 RID: 3570
		private class BindingDeliveryCapabilitiesHelper : IBindingDeliveryCapabilities
		{
			// Token: 0x060080ED RID: 33005 RVA: 0x001DEB9B File Offset: 0x001DCD9B
			internal BindingDeliveryCapabilitiesHelper(ReliableSessionBindingElement element, IBindingDeliveryCapabilities inner)
			{
				this.element = element;
				this.inner = inner;
			}

			// Token: 0x17001C80 RID: 7296
			// (get) Token: 0x060080EE RID: 33006 RVA: 0x001DEBB1 File Offset: 0x001DCDB1
			bool IBindingDeliveryCapabilities.AssuresOrderedDelivery
			{
				get
				{
					return this.element.Ordered;
				}
			}

			// Token: 0x17001C81 RID: 7297
			// (get) Token: 0x060080EF RID: 33007 RVA: 0x001DEBBE File Offset: 0x001DCDBE
			bool IBindingDeliveryCapabilities.QueuedDelivery
			{
				get
				{
					return this.inner != null && this.inner.QueuedDelivery;
				}
			}

			// Token: 0x04004988 RID: 18824
			private ReliableSessionBindingElement element;

			// Token: 0x04004989 RID: 18825
			private IBindingDeliveryCapabilities inner;
		}
	}
}
