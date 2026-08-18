using System;
using System.ComponentModel;
using System.Security.Authentication.ExtendedProtection;
using System.ServiceModel.Activation;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008A8 RID: 2216
	[__DynamicallyInvokable]
	public class TcpTransportBindingElement : ConnectionOrientedTransportBindingElement
	{
		// Token: 0x0600547F RID: 21631 RVA: 0x001370D5 File Offset: 0x001352D5
		[__DynamicallyInvokable]
		public TcpTransportBindingElement()
		{
			this.listenBacklog = TcpTransportDefaults.GetListenBacklog();
			this.portSharingEnabled = false;
			this.teredoEnabled = false;
			this.connectionPoolSettings = new TcpConnectionPoolSettings();
			this.extendedProtectionPolicy = ChannelBindingUtility.DefaultPolicy;
		}

		// Token: 0x06005480 RID: 21632 RVA: 0x0013710C File Offset: 0x0013530C
		[__DynamicallyInvokable]
		protected TcpTransportBindingElement(TcpTransportBindingElement elementToBeCloned) : base(elementToBeCloned)
		{
			this.listenBacklog = elementToBeCloned.listenBacklog;
			this.isListenBacklogSet = elementToBeCloned.isListenBacklogSet;
			this.portSharingEnabled = elementToBeCloned.portSharingEnabled;
			this.teredoEnabled = elementToBeCloned.teredoEnabled;
			this.connectionPoolSettings = elementToBeCloned.connectionPoolSettings.Clone();
			this.extendedProtectionPolicy = elementToBeCloned.ExtendedProtectionPolicy;
		}

		// Token: 0x170014C8 RID: 5320
		// (get) Token: 0x06005481 RID: 21633 RVA: 0x0013716D File Offset: 0x0013536D
		[__DynamicallyInvokable]
		public TcpConnectionPoolSettings ConnectionPoolSettings
		{
			[__DynamicallyInvokable]
			get
			{
				return this.connectionPoolSettings;
			}
		}

		// Token: 0x170014C9 RID: 5321
		// (get) Token: 0x06005482 RID: 21634 RVA: 0x00137175 File Offset: 0x00135375
		// (set) Token: 0x06005483 RID: 21635 RVA: 0x0013717D File Offset: 0x0013537D
		public int ListenBacklog
		{
			get
			{
				return this.listenBacklog;
			}
			set
			{
				if (value <= 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("ValueMustBePositive")));
				}
				this.listenBacklog = value;
				this.isListenBacklogSet = true;
			}
		}

		// Token: 0x170014CA RID: 5322
		// (get) Token: 0x06005484 RID: 21636 RVA: 0x001371B0 File Offset: 0x001353B0
		internal bool IsListenBacklogSet
		{
			get
			{
				return this.isListenBacklogSet;
			}
		}

		// Token: 0x170014CB RID: 5323
		// (get) Token: 0x06005485 RID: 21637 RVA: 0x001371B8 File Offset: 0x001353B8
		// (set) Token: 0x06005486 RID: 21638 RVA: 0x001371C0 File Offset: 0x001353C0
		[DefaultValue(false)]
		public bool PortSharingEnabled
		{
			get
			{
				return this.portSharingEnabled;
			}
			set
			{
				this.portSharingEnabled = value;
			}
		}

		// Token: 0x170014CC RID: 5324
		// (get) Token: 0x06005487 RID: 21639 RVA: 0x001371C9 File Offset: 0x001353C9
		[__DynamicallyInvokable]
		public override string Scheme
		{
			[__DynamicallyInvokable]
			get
			{
				return "net.tcp";
			}
		}

		// Token: 0x170014CD RID: 5325
		// (get) Token: 0x06005488 RID: 21640 RVA: 0x001371D0 File Offset: 0x001353D0
		// (set) Token: 0x06005489 RID: 21641 RVA: 0x001371D8 File Offset: 0x001353D8
		[DefaultValue(false)]
		public bool TeredoEnabled
		{
			get
			{
				return this.teredoEnabled;
			}
			set
			{
				this.teredoEnabled = value;
			}
		}

		// Token: 0x170014CE RID: 5326
		// (get) Token: 0x0600548A RID: 21642 RVA: 0x001371E1 File Offset: 0x001353E1
		// (set) Token: 0x0600548B RID: 21643 RVA: 0x001371EC File Offset: 0x001353EC
		public ExtendedProtectionPolicy ExtendedProtectionPolicy
		{
			get
			{
				return this.extendedProtectionPolicy;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				if (value.PolicyEnforcement == PolicyEnforcement.Always && !ExtendedProtectionPolicy.OSSupportsExtendedProtection)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new PlatformNotSupportedException(SR.GetString("ExtendedProtectionNotSupported")));
				}
				this.extendedProtectionPolicy = value;
			}
		}

		// Token: 0x170014CF RID: 5327
		// (get) Token: 0x0600548C RID: 21644 RVA: 0x0013723D File Offset: 0x0013543D
		internal override string WsdlTransportUri
		{
			get
			{
				return "http://schemas.microsoft.com/soap/tcp";
			}
		}

		// Token: 0x0600548D RID: 21645 RVA: 0x00137244 File Offset: 0x00135444
		[__DynamicallyInvokable]
		public override BindingElement Clone()
		{
			return new TcpTransportBindingElement(this);
		}

		// Token: 0x0600548E RID: 21646 RVA: 0x0013724C File Offset: 0x0013544C
		[__DynamicallyInvokable]
		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (!this.CanBuildChannelFactory<TChannel>(context))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("TChannel", SR.GetString("ChannelTypeNotSupported", new object[]
				{
					typeof(TChannel)
				}));
			}
			return (IChannelFactory<TChannel>)new TcpChannelFactory<TChannel>(this, context);
		}

		// Token: 0x0600548F RID: 21647 RVA: 0x001372B0 File Offset: 0x001354B0
		public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (!this.CanBuildChannelListener<TChannel>(context))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("TChannel", SR.GetString("ChannelTypeNotSupported", new object[]
				{
					typeof(TChannel)
				}));
			}
			TcpChannelListener tcpChannelListener;
			if (typeof(TChannel) == typeof(IReplyChannel))
			{
				tcpChannelListener = new TcpReplyChannelListener(this, context);
			}
			else
			{
				if (!(typeof(TChannel) == typeof(IDuplexSessionChannel)))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("TChannel", SR.GetString("ChannelTypeNotSupported", new object[]
					{
						typeof(TChannel)
					}));
				}
				tcpChannelListener = new TcpDuplexChannelListener(this, context);
			}
			AspNetEnvironment.Current.ApplyHostedContext(tcpChannelListener, context);
			return (IChannelListener<TChannel>)tcpChannelListener;
		}

		// Token: 0x06005490 RID: 21648 RVA: 0x00137390 File Offset: 0x00135590
		[__DynamicallyInvokable]
		public override T GetProperty<T>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (typeof(T) == typeof(IBindingDeliveryCapabilities))
			{
				return (T)((object)new TcpTransportBindingElement.BindingDeliveryCapabilitiesHelper());
			}
			if (typeof(T) == typeof(ExtendedProtectionPolicy))
			{
				return (T)((object)this.ExtendedProtectionPolicy);
			}
			if (typeof(T) == typeof(ITransportCompressionSupport))
			{
				return (T)((object)new TcpTransportBindingElement.TransportCompressionSupportHelper());
			}
			return base.GetProperty<T>(context);
		}

		// Token: 0x06005491 RID: 21649 RVA: 0x0013742C File Offset: 0x0013562C
		internal override bool IsMatch(BindingElement b)
		{
			if (!base.IsMatch(b))
			{
				return false;
			}
			TcpTransportBindingElement tcpTransportBindingElement = b as TcpTransportBindingElement;
			return tcpTransportBindingElement != null && this.listenBacklog == tcpTransportBindingElement.listenBacklog && this.portSharingEnabled == tcpTransportBindingElement.portSharingEnabled && this.teredoEnabled == tcpTransportBindingElement.teredoEnabled && this.connectionPoolSettings.IsMatch(tcpTransportBindingElement.connectionPoolSettings) && ChannelBindingUtility.AreEqual(this.ExtendedProtectionPolicy, tcpTransportBindingElement.ExtendedProtectionPolicy);
		}

		// Token: 0x06005492 RID: 21650 RVA: 0x001374AB File Offset: 0x001356AB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeExtendedProtectionPolicy()
		{
			return !ChannelBindingUtility.AreEqual(this.ExtendedProtectionPolicy, ChannelBindingUtility.DefaultPolicy);
		}

		// Token: 0x06005493 RID: 21651 RVA: 0x001374C0 File Offset: 0x001356C0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeListenBacklog()
		{
			return this.isListenBacklogSet;
		}

		// Token: 0x04003319 RID: 13081
		private int listenBacklog;

		// Token: 0x0400331A RID: 13082
		private bool portSharingEnabled;

		// Token: 0x0400331B RID: 13083
		private bool teredoEnabled;

		// Token: 0x0400331C RID: 13084
		private TcpConnectionPoolSettings connectionPoolSettings;

		// Token: 0x0400331D RID: 13085
		private ExtendedProtectionPolicy extendedProtectionPolicy;

		// Token: 0x0400331E RID: 13086
		private bool isListenBacklogSet;

		// Token: 0x02000D7A RID: 3450
		private class BindingDeliveryCapabilitiesHelper : IBindingDeliveryCapabilities
		{
			// Token: 0x06007E69 RID: 32361 RVA: 0x001D79A7 File Offset: 0x001D5BA7
			internal BindingDeliveryCapabilitiesHelper()
			{
			}

			// Token: 0x17001C27 RID: 7207
			// (get) Token: 0x06007E6A RID: 32362 RVA: 0x001D79AF File Offset: 0x001D5BAF
			bool IBindingDeliveryCapabilities.AssuresOrderedDelivery
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001C28 RID: 7208
			// (get) Token: 0x06007E6B RID: 32363 RVA: 0x001D79B2 File Offset: 0x001D5BB2
			bool IBindingDeliveryCapabilities.QueuedDelivery
			{
				get
				{
					return false;
				}
			}
		}

		// Token: 0x02000D7B RID: 3451
		private class TransportCompressionSupportHelper : ITransportCompressionSupport
		{
			// Token: 0x06007E6C RID: 32364 RVA: 0x001D79B5 File Offset: 0x001D5BB5
			public bool IsCompressionFormatSupported(CompressionFormat compressionFormat)
			{
				return true;
			}
		}
	}
}
