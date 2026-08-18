using System;
using System.ComponentModel;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000890 RID: 2192
	public sealed class OneWayBindingElement : BindingElement, IPolicyExportExtension
	{
		// Token: 0x06005342 RID: 21314 RVA: 0x00132CDD File Offset: 0x00130EDD
		public OneWayBindingElement()
		{
			this.channelPoolSettings = new ChannelPoolSettings();
			this.packetRoutable = false;
			this.maxAcceptedChannels = 10;
		}

		// Token: 0x06005343 RID: 21315 RVA: 0x00132CFF File Offset: 0x00130EFF
		private OneWayBindingElement(OneWayBindingElement elementToBeCloned) : base(elementToBeCloned)
		{
			this.channelPoolSettings = elementToBeCloned.ChannelPoolSettings.Clone();
			this.packetRoutable = elementToBeCloned.PacketRoutable;
			this.maxAcceptedChannels = elementToBeCloned.maxAcceptedChannels;
		}

		// Token: 0x17001484 RID: 5252
		// (get) Token: 0x06005344 RID: 21316 RVA: 0x00132D31 File Offset: 0x00130F31
		// (set) Token: 0x06005345 RID: 21317 RVA: 0x00132D39 File Offset: 0x00130F39
		public ChannelPoolSettings ChannelPoolSettings
		{
			get
			{
				return this.channelPoolSettings;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.channelPoolSettings = value;
			}
		}

		// Token: 0x17001485 RID: 5253
		// (get) Token: 0x06005346 RID: 21318 RVA: 0x00132D55 File Offset: 0x00130F55
		// (set) Token: 0x06005347 RID: 21319 RVA: 0x00132D5D File Offset: 0x00130F5D
		[DefaultValue(10)]
		public int MaxAcceptedChannels
		{
			get
			{
				return this.maxAcceptedChannels;
			}
			set
			{
				if (value <= 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBePositive")));
				}
				this.maxAcceptedChannels = value;
			}
		}

		// Token: 0x17001486 RID: 5254
		// (get) Token: 0x06005348 RID: 21320 RVA: 0x00132D8F File Offset: 0x00130F8F
		// (set) Token: 0x06005349 RID: 21321 RVA: 0x00132D97 File Offset: 0x00130F97
		[DefaultValue(false)]
		public bool PacketRoutable
		{
			get
			{
				return this.packetRoutable;
			}
			set
			{
				this.packetRoutable = value;
			}
		}

		// Token: 0x0600534A RID: 21322 RVA: 0x00132DA0 File Offset: 0x00130FA0
		public override BindingElement Clone()
		{
			return new OneWayBindingElement(this);
		}

		// Token: 0x0600534B RID: 21323 RVA: 0x00132DA8 File Offset: 0x00130FA8
		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (typeof(TChannel) != typeof(IOutputChannel))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("TChannel", SR.GetString("ChannelTypeNotSupported", new object[]
				{
					typeof(TChannel)
				}));
			}
			if (context.CanBuildInnerChannelFactory<IDuplexChannel>())
			{
				return (IChannelFactory<TChannel>)new DuplexOneWayChannelFactory(this, context);
			}
			if (context.CanBuildInnerChannelFactory<IDuplexSessionChannel>())
			{
				return (IChannelFactory<TChannel>)new DuplexSessionOneWayChannelFactory(this, context);
			}
			if (context.CanBuildInnerChannelFactory<IRequestChannel>())
			{
				return (IChannelFactory<TChannel>)new RequestOneWayChannelFactory(this, context);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("OneWayInternalTypeNotSupported", new object[]
			{
				context.Binding.Name
			})));
		}

		// Token: 0x0600534C RID: 21324 RVA: 0x00132E7C File Offset: 0x0013107C
		public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (typeof(TChannel) != typeof(IInputChannel))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("TChannel", SR.GetString("ChannelTypeNotSupported", new object[]
				{
					typeof(TChannel)
				}));
			}
			if (context.CanBuildInnerChannelListener<IDuplexChannel>())
			{
				return (IChannelListener<TChannel>)new DuplexOneWayChannelListener(this, context);
			}
			if (context.CanBuildInnerChannelListener<IDuplexSessionChannel>())
			{
				return (IChannelListener<TChannel>)new DuplexSessionOneWayChannelListener(this, context);
			}
			if (context.CanBuildInnerChannelListener<IReplyChannel>())
			{
				return (IChannelListener<TChannel>)new ReplyOneWayChannelListener(this, context);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("OneWayInternalTypeNotSupported", new object[]
			{
				context.Binding.Name
			})));
		}

		// Token: 0x0600534D RID: 21325 RVA: 0x00132F50 File Offset: 0x00131150
		public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			return !(typeof(TChannel) != typeof(IOutputChannel)) && (context.CanBuildInnerChannelFactory<IDuplexChannel>() || context.CanBuildInnerChannelFactory<IDuplexSessionChannel>() || context.CanBuildInnerChannelFactory<IRequestChannel>());
		}

		// Token: 0x0600534E RID: 21326 RVA: 0x00132FAC File Offset: 0x001311AC
		public override bool CanBuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			return !(typeof(TChannel) != typeof(IInputChannel)) && (context.CanBuildInnerChannelListener<IDuplexChannel>() || context.CanBuildInnerChannelListener<IDuplexSessionChannel>() || context.CanBuildInnerChannelListener<IReplyChannel>());
		}

		// Token: 0x17001487 RID: 5255
		// (get) Token: 0x0600534F RID: 21327 RVA: 0x00133008 File Offset: 0x00131208
		private static MessagePartSpecification OneWaySignedMessageParts
		{
			get
			{
				if (OneWayBindingElement.oneWaySignedMessageParts == null)
				{
					MessagePartSpecification messagePartSpecification = new MessagePartSpecification(new XmlQualifiedName[]
					{
						new XmlQualifiedName("PacketRoutable", "http://schemas.microsoft.com/ws/2005/05/routing")
					});
					messagePartSpecification.MakeReadOnly();
					OneWayBindingElement.oneWaySignedMessageParts = messagePartSpecification;
				}
				return OneWayBindingElement.oneWaySignedMessageParts;
			}
		}

		// Token: 0x06005350 RID: 21328 RVA: 0x0013304C File Offset: 0x0013124C
		public override T GetProperty<T>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (typeof(T) == typeof(ChannelProtectionRequirements))
			{
				ChannelProtectionRequirements channelProtectionRequirements = new ChannelProtectionRequirements();
				if (this.PacketRoutable)
				{
					channelProtectionRequirements.IncomingSignatureParts.AddParts(OneWayBindingElement.OneWaySignedMessageParts);
					channelProtectionRequirements.OutgoingSignatureParts.AddParts(OneWayBindingElement.OneWaySignedMessageParts);
				}
				ChannelProtectionRequirements innerProperty = context.GetInnerProperty<ChannelProtectionRequirements>();
				if (innerProperty != null)
				{
					channelProtectionRequirements.Add(innerProperty);
				}
				return (T)((object)channelProtectionRequirements);
			}
			return context.GetInnerProperty<T>();
		}

		// Token: 0x06005351 RID: 21329 RVA: 0x001330D4 File Offset: 0x001312D4
		internal override bool IsMatch(BindingElement b)
		{
			if (b == null)
			{
				return false;
			}
			OneWayBindingElement oneWayBindingElement = b as OneWayBindingElement;
			return oneWayBindingElement != null && this.channelPoolSettings.IsMatch(oneWayBindingElement.ChannelPoolSettings) && this.packetRoutable == oneWayBindingElement.PacketRoutable && this.maxAcceptedChannels == oneWayBindingElement.MaxAcceptedChannels;
		}

		// Token: 0x06005352 RID: 21330 RVA: 0x00133128 File Offset: 0x00131328
		void IPolicyExportExtension.ExportPolicy(MetadataExporter exporter, PolicyConversionContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (context.BindingElements != null)
			{
				OneWayBindingElement oneWayBindingElement = context.BindingElements.Find<OneWayBindingElement>();
				if (oneWayBindingElement != null)
				{
					XmlDocument xmlDocument = new XmlDocument();
					XmlElement xmlElement = xmlDocument.CreateElement("ow", "OneWay", "http://schemas.microsoft.com/ws/2005/05/routing/policy");
					if (oneWayBindingElement.PacketRoutable)
					{
						XmlElement newChild = xmlDocument.CreateElement("ow", "PacketRoutable", "http://schemas.microsoft.com/ws/2005/05/routing/policy");
						xmlElement.AppendChild(newChild);
					}
					context.GetBindingAssertions().Add(xmlElement);
				}
			}
		}

		// Token: 0x06005353 RID: 21331 RVA: 0x001331AD File Offset: 0x001313AD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeChannelPoolSettings()
		{
			return this.channelPoolSettings.InternalShouldSerialize();
		}

		// Token: 0x040032B7 RID: 12983
		private ChannelPoolSettings channelPoolSettings;

		// Token: 0x040032B8 RID: 12984
		private bool packetRoutable;

		// Token: 0x040032B9 RID: 12985
		private int maxAcceptedChannels;

		// Token: 0x040032BA RID: 12986
		private static MessagePartSpecification oneWaySignedMessageParts;
	}
}
