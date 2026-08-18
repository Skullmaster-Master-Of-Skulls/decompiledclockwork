using System;
using System.ServiceModel.Activation;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000906 RID: 2310
	public sealed class MsmqTransportBindingElement : MsmqBindingElementBase
	{
		// Token: 0x0600581E RID: 22558 RVA: 0x00143DB6 File Offset: 0x00141FB6
		public MsmqTransportBindingElement()
		{
		}

		// Token: 0x0600581F RID: 22559 RVA: 0x00143DC5 File Offset: 0x00141FC5
		private MsmqTransportBindingElement(MsmqTransportBindingElement elementToBeCloned) : base(elementToBeCloned)
		{
			this.useActiveDirectory = elementToBeCloned.useActiveDirectory;
			this.maxPoolSize = elementToBeCloned.maxPoolSize;
			this.queueTransferProtocol = elementToBeCloned.queueTransferProtocol;
		}

		// Token: 0x17001578 RID: 5496
		// (get) Token: 0x06005820 RID: 22560 RVA: 0x00143DFC File Offset: 0x00141FFC
		internal override MsmqUri.IAddressTranslator AddressTranslator
		{
			get
			{
				QueueTransferProtocol queueTransferProtocol = this.queueTransferProtocol;
				if (queueTransferProtocol == QueueTransferProtocol.Srmp)
				{
					return MsmqUri.SrmpAddressTranslator;
				}
				if (queueTransferProtocol == QueueTransferProtocol.SrmpSecure)
				{
					return MsmqUri.SrmpsAddressTranslator;
				}
				if (!this.useActiveDirectory)
				{
					return MsmqUri.NetMsmqAddressTranslator;
				}
				return MsmqUri.ActiveDirectoryAddressTranslator;
			}
		}

		// Token: 0x17001579 RID: 5497
		// (get) Token: 0x06005821 RID: 22561 RVA: 0x00143E39 File Offset: 0x00142039
		// (set) Token: 0x06005822 RID: 22562 RVA: 0x00143E41 File Offset: 0x00142041
		public int MaxPoolSize
		{
			get
			{
				return this.maxPoolSize;
			}
			set
			{
				if (value < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("MsmqNonNegativeArgumentExpected")));
				}
				this.maxPoolSize = value;
			}
		}

		// Token: 0x1700157A RID: 5498
		// (get) Token: 0x06005823 RID: 22563 RVA: 0x00143E73 File Offset: 0x00142073
		// (set) Token: 0x06005824 RID: 22564 RVA: 0x00143E7B File Offset: 0x0014207B
		public QueueTransferProtocol QueueTransferProtocol
		{
			get
			{
				return this.queueTransferProtocol;
			}
			set
			{
				if (!QueueTransferProtocolHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.queueTransferProtocol = value;
			}
		}

		// Token: 0x1700157B RID: 5499
		// (get) Token: 0x06005825 RID: 22565 RVA: 0x00143EA1 File Offset: 0x001420A1
		public override string Scheme
		{
			get
			{
				return "net.msmq";
			}
		}

		// Token: 0x1700157C RID: 5500
		// (get) Token: 0x06005826 RID: 22566 RVA: 0x00143EA8 File Offset: 0x001420A8
		// (set) Token: 0x06005827 RID: 22567 RVA: 0x00143EB0 File Offset: 0x001420B0
		public bool UseActiveDirectory
		{
			get
			{
				return this.useActiveDirectory;
			}
			set
			{
				this.useActiveDirectory = value;
			}
		}

		// Token: 0x1700157D RID: 5501
		// (get) Token: 0x06005828 RID: 22568 RVA: 0x00143EB9 File Offset: 0x001420B9
		internal override string WsdlTransportUri
		{
			get
			{
				return "http://schemas.microsoft.com/soap/msmq";
			}
		}

		// Token: 0x06005829 RID: 22569 RVA: 0x00143EC0 File Offset: 0x001420C0
		public override BindingElement Clone()
		{
			return new MsmqTransportBindingElement(this);
		}

		// Token: 0x0600582A RID: 22570 RVA: 0x00143EC8 File Offset: 0x001420C8
		public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
		{
			return typeof(TChannel) == typeof(IOutputChannel) || typeof(TChannel) == typeof(IOutputSessionChannel);
		}

		// Token: 0x0600582B RID: 22571 RVA: 0x00143F00 File Offset: 0x00142100
		public override bool CanBuildChannelListener<TChannel>(BindingContext context)
		{
			return typeof(TChannel) == typeof(IInputChannel) || typeof(TChannel) == typeof(IInputSessionChannel);
		}

		// Token: 0x0600582C RID: 22572 RVA: 0x00143F38 File Offset: 0x00142138
		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (typeof(TChannel) == typeof(IOutputChannel))
			{
				MsmqChannelFactoryBase<IOutputChannel> msmqChannelFactoryBase = new MsmqOutputChannelFactory(this, context);
				MsmqVerifier.VerifySender<IOutputChannel>(msmqChannelFactoryBase);
				return (IChannelFactory<TChannel>)msmqChannelFactoryBase;
			}
			if (typeof(TChannel) == typeof(IOutputSessionChannel))
			{
				MsmqChannelFactoryBase<IOutputSessionChannel> msmqChannelFactoryBase2 = new MsmqOutputSessionChannelFactory(this, context);
				MsmqVerifier.VerifySender<IOutputSessionChannel>(msmqChannelFactoryBase2);
				return (IChannelFactory<TChannel>)msmqChannelFactoryBase2;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("TChannel", SR.GetString("ChannelTypeNotSupported", new object[]
			{
				typeof(TChannel)
			}));
		}

		// Token: 0x0600582D RID: 22573 RVA: 0x00143FE4 File Offset: 0x001421E4
		public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			MsmqTransportReceiveParameters receiveParameters = new MsmqTransportReceiveParameters(this, MsmqUri.NetMsmqAddressTranslator);
			TransportChannelListener transportChannelListener;
			if (typeof(TChannel) == typeof(IInputChannel))
			{
				transportChannelListener = new MsmqInputChannelListener(this, context, receiveParameters);
			}
			else
			{
				if (!(typeof(TChannel) == typeof(IInputSessionChannel)))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("TChannel", SR.GetString("ChannelTypeNotSupported", new object[]
					{
						typeof(TChannel)
					}));
				}
				transportChannelListener = new MsmqInputSessionChannelListener(this, context, receiveParameters);
			}
			AspNetEnvironment.Current.ApplyHostedContext(transportChannelListener, context);
			MsmqVerifier.VerifyReceiver(receiveParameters, transportChannelListener.Uri);
			return (IChannelListener<TChannel>)transportChannelListener;
		}

		// Token: 0x04003619 RID: 13849
		private int maxPoolSize = 8;

		// Token: 0x0400361A RID: 13850
		private bool useActiveDirectory;

		// Token: 0x0400361B RID: 13851
		private QueueTransferProtocol queueTransferProtocol;
	}
}
