using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.MsmqIntegration
{
	// Token: 0x020003AE RID: 942
	public sealed class MsmqIntegrationBindingElement : MsmqBindingElementBase
	{
		// Token: 0x06002345 RID: 9029 RVA: 0x0008116E File Offset: 0x0007F36E
		public MsmqIntegrationBindingElement()
		{
			this.serializationFormat = MsmqMessageSerializationFormat.Xml;
		}

		// Token: 0x06002346 RID: 9030 RVA: 0x0008117D File Offset: 0x0007F37D
		private MsmqIntegrationBindingElement(MsmqIntegrationBindingElement other) : base(other)
		{
			this.serializationFormat = other.serializationFormat;
			if (other.targetSerializationTypes != null)
			{
				this.targetSerializationTypes = (other.targetSerializationTypes.Clone() as Type[]);
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06002347 RID: 9031 RVA: 0x000811B0 File Offset: 0x0007F3B0
		public override string Scheme
		{
			get
			{
				return "msmq.formatname";
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06002348 RID: 9032 RVA: 0x000811B7 File Offset: 0x0007F3B7
		internal override MsmqUri.IAddressTranslator AddressTranslator
		{
			get
			{
				return MsmqUri.FormatNameAddressTranslator;
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06002349 RID: 9033 RVA: 0x000811BE File Offset: 0x0007F3BE
		// (set) Token: 0x0600234A RID: 9034 RVA: 0x000811C6 File Offset: 0x0007F3C6
		public MsmqMessageSerializationFormat SerializationFormat
		{
			get
			{
				return this.serializationFormat;
			}
			set
			{
				if (!MsmqMessageSerializationFormatHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.serializationFormat = value;
			}
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x0600234B RID: 9035 RVA: 0x000811EC File Offset: 0x0007F3EC
		// (set) Token: 0x0600234C RID: 9036 RVA: 0x00081208 File Offset: 0x0007F408
		public Type[] TargetSerializationTypes
		{
			get
			{
				if (this.targetSerializationTypes == null)
				{
					return null;
				}
				return this.targetSerializationTypes.Clone() as Type[];
			}
			set
			{
				if (value == null)
				{
					this.targetSerializationTypes = null;
					return;
				}
				this.targetSerializationTypes = (value.Clone() as Type[]);
			}
		}

		// Token: 0x0600234D RID: 9037 RVA: 0x00081226 File Offset: 0x0007F426
		public override BindingElement Clone()
		{
			return new MsmqIntegrationBindingElement(this);
		}

		// Token: 0x0600234E RID: 9038 RVA: 0x0008122E File Offset: 0x0007F42E
		public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
		{
			return typeof(TChannel) == typeof(IOutputChannel);
		}

		// Token: 0x0600234F RID: 9039 RVA: 0x00081249 File Offset: 0x0007F449
		public override bool CanBuildChannelListener<TChannel>(BindingContext context)
		{
			return typeof(TChannel) == typeof(IInputChannel);
		}

		// Token: 0x06002350 RID: 9040 RVA: 0x00081264 File Offset: 0x0007F464
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
			MsmqChannelFactoryBase<IOutputChannel> msmqChannelFactoryBase = new MsmqIntegrationChannelFactory(this, context);
			MsmqVerifier.VerifySender<IOutputChannel>(msmqChannelFactoryBase);
			return (IChannelFactory<TChannel>)msmqChannelFactoryBase;
		}

		// Token: 0x06002351 RID: 9041 RVA: 0x000812E0 File Offset: 0x0007F4E0
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
			MsmqIntegrationReceiveParameters receiveParameters = new MsmqIntegrationReceiveParameters(this);
			MsmqIntegrationChannelListener msmqIntegrationChannelListener = new MsmqIntegrationChannelListener(this, context, receiveParameters);
			MsmqVerifier.VerifyReceiver(receiveParameters, msmqIntegrationChannelListener.Uri);
			return (IChannelListener<TChannel>)msmqIntegrationChannelListener;
		}

		// Token: 0x06002352 RID: 9042 RVA: 0x0008136C File Offset: 0x0007F56C
		public override T GetProperty<T>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (typeof(T) == typeof(MessageVersion))
			{
				return (T)((object)MessageVersion.None);
			}
			return base.GetProperty<T>(context);
		}

		// Token: 0x04001FE2 RID: 8162
		private MsmqMessageSerializationFormat serializationFormat;

		// Token: 0x04001FE3 RID: 8163
		private Type[] targetSerializationTypes;
	}
}
