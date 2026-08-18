using System;
using System.Collections.ObjectModel;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000783 RID: 1923
	internal abstract class TransportChannelFactory<TChannel> : ChannelFactoryBase<TChannel>, ITransportFactorySettings, IDefaultCommunicationTimeouts
	{
		// Token: 0x0600493F RID: 18751 RVA: 0x0010DCCC File Offset: 0x0010BECC
		protected TransportChannelFactory(TransportBindingElement bindingElement, BindingContext context) : this(bindingElement, context, TransportDefaults.GetDefaultMessageEncoderFactory())
		{
		}

		// Token: 0x06004940 RID: 18752 RVA: 0x0010DCDC File Offset: 0x0010BEDC
		protected TransportChannelFactory(TransportBindingElement bindingElement, BindingContext context, MessageEncoderFactory defaultMessageEncoderFactory) : base(context.Binding)
		{
			this.manualAddressing = bindingElement.ManualAddressing;
			this.maxBufferPoolSize = bindingElement.MaxBufferPoolSize;
			this.maxReceivedMessageSize = bindingElement.MaxReceivedMessageSize;
			Collection<MessageEncodingBindingElement> collection = context.BindingParameters.FindAll<MessageEncodingBindingElement>();
			if (collection.Count > 1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MultipleMebesInParameters")));
			}
			if (collection.Count == 1)
			{
				this.messageEncoderFactory = collection[0].CreateMessageEncoderFactory();
				context.BindingParameters.Remove<MessageEncodingBindingElement>();
			}
			else
			{
				this.messageEncoderFactory = defaultMessageEncoderFactory;
			}
			if (this.messageEncoderFactory != null)
			{
				this.messageVersion = this.messageEncoderFactory.MessageVersion;
				return;
			}
			this.messageVersion = MessageVersion.None;
		}

		// Token: 0x17001260 RID: 4704
		// (get) Token: 0x06004941 RID: 18753 RVA: 0x0010DD9D File Offset: 0x0010BF9D
		public BufferManager BufferManager
		{
			get
			{
				return this.bufferManager;
			}
		}

		// Token: 0x17001261 RID: 4705
		// (get) Token: 0x06004942 RID: 18754 RVA: 0x0010DDA5 File Offset: 0x0010BFA5
		public long MaxBufferPoolSize
		{
			get
			{
				return this.maxBufferPoolSize;
			}
		}

		// Token: 0x17001262 RID: 4706
		// (get) Token: 0x06004943 RID: 18755 RVA: 0x0010DDAD File Offset: 0x0010BFAD
		public long MaxReceivedMessageSize
		{
			get
			{
				return this.maxReceivedMessageSize;
			}
		}

		// Token: 0x17001263 RID: 4707
		// (get) Token: 0x06004944 RID: 18756 RVA: 0x0010DDB5 File Offset: 0x0010BFB5
		public MessageEncoderFactory MessageEncoderFactory
		{
			get
			{
				return this.messageEncoderFactory;
			}
		}

		// Token: 0x17001264 RID: 4708
		// (get) Token: 0x06004945 RID: 18757 RVA: 0x0010DDBD File Offset: 0x0010BFBD
		public MessageVersion MessageVersion
		{
			get
			{
				return this.messageVersion;
			}
		}

		// Token: 0x17001265 RID: 4709
		// (get) Token: 0x06004946 RID: 18758 RVA: 0x0010DDC5 File Offset: 0x0010BFC5
		public bool ManualAddressing
		{
			get
			{
				return this.manualAddressing;
			}
		}

		// Token: 0x17001266 RID: 4710
		// (get) Token: 0x06004947 RID: 18759
		public abstract string Scheme { get; }

		// Token: 0x06004948 RID: 18760 RVA: 0x0010DDD0 File Offset: 0x0010BFD0
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(MessageVersion))
			{
				return (T)((object)this.MessageVersion);
			}
			if (typeof(T) == typeof(FaultConverter))
			{
				if (this.MessageEncoderFactory == null)
				{
					return default(T);
				}
				return this.MessageEncoderFactory.Encoder.GetProperty<T>();
			}
			else
			{
				if (typeof(T) == typeof(ITransportFactorySettings))
				{
					return (T)((object)this);
				}
				return base.GetProperty<T>();
			}
		}

		// Token: 0x06004949 RID: 18761 RVA: 0x0010DE6A File Offset: 0x0010C06A
		protected override void OnAbort()
		{
			this.OnCloseOrAbort();
			base.OnAbort();
		}

		// Token: 0x0600494A RID: 18762 RVA: 0x0010DE78 File Offset: 0x0010C078
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.OnCloseOrAbort();
			return base.OnBeginClose(timeout, callback, state);
		}

		// Token: 0x0600494B RID: 18763 RVA: 0x0010DE89 File Offset: 0x0010C089
		protected override void OnClose(TimeSpan timeout)
		{
			this.OnCloseOrAbort();
			base.OnClose(timeout);
		}

		// Token: 0x0600494C RID: 18764 RVA: 0x0010DE98 File Offset: 0x0010C098
		private void OnCloseOrAbort()
		{
			if (this.bufferManager != null)
			{
				this.bufferManager.Clear();
			}
		}

		// Token: 0x0600494D RID: 18765 RVA: 0x0010DEAD File Offset: 0x0010C0AD
		internal virtual int GetMaxBufferSize()
		{
			if (this.MaxReceivedMessageSize > 2147483647L)
			{
				return int.MaxValue;
			}
			return (int)this.MaxReceivedMessageSize;
		}

		// Token: 0x0600494E RID: 18766 RVA: 0x0010DECA File Offset: 0x0010C0CA
		protected override void OnOpening()
		{
			base.OnOpening();
			this.bufferManager = BufferManager.CreateBufferManager(this.MaxBufferPoolSize, this.GetMaxBufferSize());
		}

		// Token: 0x0600494F RID: 18767 RVA: 0x0010DEEC File Offset: 0x0010C0EC
		internal void ValidateScheme(Uri via)
		{
			if (via.Scheme != this.Scheme && string.Compare(via.Scheme, this.Scheme, StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("via", SR.GetString("InvalidUriScheme", new object[]
				{
					via.Scheme,
					this.Scheme
				}));
			}
		}

		// Token: 0x17001267 RID: 4711
		// (get) Token: 0x06004950 RID: 18768 RVA: 0x0010DF52 File Offset: 0x0010C152
		long ITransportFactorySettings.MaxReceivedMessageSize
		{
			get
			{
				return this.MaxReceivedMessageSize;
			}
		}

		// Token: 0x17001268 RID: 4712
		// (get) Token: 0x06004951 RID: 18769 RVA: 0x0010DF5A File Offset: 0x0010C15A
		BufferManager ITransportFactorySettings.BufferManager
		{
			get
			{
				return this.BufferManager;
			}
		}

		// Token: 0x17001269 RID: 4713
		// (get) Token: 0x06004952 RID: 18770 RVA: 0x0010DF62 File Offset: 0x0010C162
		bool ITransportFactorySettings.ManualAddressing
		{
			get
			{
				return this.ManualAddressing;
			}
		}

		// Token: 0x1700126A RID: 4714
		// (get) Token: 0x06004953 RID: 18771 RVA: 0x0010DF6A File Offset: 0x0010C16A
		MessageEncoderFactory ITransportFactorySettings.MessageEncoderFactory
		{
			get
			{
				return this.MessageEncoderFactory;
			}
		}

		// Token: 0x04002E21 RID: 11809
		private BufferManager bufferManager;

		// Token: 0x04002E22 RID: 11810
		private long maxBufferPoolSize;

		// Token: 0x04002E23 RID: 11811
		private long maxReceivedMessageSize;

		// Token: 0x04002E24 RID: 11812
		private MessageEncoderFactory messageEncoderFactory;

		// Token: 0x04002E25 RID: 11813
		private bool manualAddressing;

		// Token: 0x04002E26 RID: 11814
		private MessageVersion messageVersion;
	}
}
