using System;
using System.ComponentModel;
using System.ServiceModel.Description;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009E6 RID: 2534
	[__DynamicallyInvokable]
	public sealed class BinaryMessageEncodingBindingElement : MessageEncodingBindingElement, IWsdlExportExtension, IPolicyExportExtension
	{
		// Token: 0x06006421 RID: 25633 RVA: 0x00175F5C File Offset: 0x0017415C
		[__DynamicallyInvokable]
		public BinaryMessageEncodingBindingElement()
		{
			this.maxReadPoolSize = 64;
			this.maxWritePoolSize = 16;
			this.readerQuotas = new XmlDictionaryReaderQuotas();
			EncoderDefaults.ReaderQuotas.CopyTo(this.readerQuotas);
			this.maxSessionSize = 2048;
			this.binaryVersion = BinaryEncoderDefaults.BinaryVersion;
			this.messageVersion = MessageVersion.CreateVersion(BinaryEncoderDefaults.EnvelopeVersion);
			this.compressionFormat = CompressionFormat.None;
		}

		// Token: 0x06006422 RID: 25634 RVA: 0x00175FC8 File Offset: 0x001741C8
		private BinaryMessageEncodingBindingElement(BinaryMessageEncodingBindingElement elementToBeCloned) : base(elementToBeCloned)
		{
			this.maxReadPoolSize = elementToBeCloned.maxReadPoolSize;
			this.maxWritePoolSize = elementToBeCloned.maxWritePoolSize;
			this.readerQuotas = new XmlDictionaryReaderQuotas();
			elementToBeCloned.readerQuotas.CopyTo(this.readerQuotas);
			this.MaxSessionSize = elementToBeCloned.MaxSessionSize;
			this.BinaryVersion = elementToBeCloned.BinaryVersion;
			this.messageVersion = elementToBeCloned.messageVersion;
			this.CompressionFormat = elementToBeCloned.CompressionFormat;
			this.maxReceivedMessageSize = elementToBeCloned.maxReceivedMessageSize;
		}

		// Token: 0x1700182E RID: 6190
		// (get) Token: 0x06006423 RID: 25635 RVA: 0x0017604C File Offset: 0x0017424C
		// (set) Token: 0x06006424 RID: 25636 RVA: 0x00176054 File Offset: 0x00174254
		[DefaultValue(CompressionFormat.None)]
		[__DynamicallyInvokable]
		public CompressionFormat CompressionFormat
		{
			[__DynamicallyInvokable]
			get
			{
				return this.compressionFormat;
			}
			[__DynamicallyInvokable]
			set
			{
				this.compressionFormat = value;
			}
		}

		// Token: 0x1700182F RID: 6191
		// (get) Token: 0x06006425 RID: 25637 RVA: 0x0017605D File Offset: 0x0017425D
		// (set) Token: 0x06006426 RID: 25638 RVA: 0x00176065 File Offset: 0x00174265
		private BinaryVersion BinaryVersion
		{
			get
			{
				return this.binaryVersion;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("value"));
				}
				this.binaryVersion = value;
			}
		}

		// Token: 0x17001830 RID: 6192
		// (get) Token: 0x06006427 RID: 25639 RVA: 0x00176086 File Offset: 0x00174286
		// (set) Token: 0x06006428 RID: 25640 RVA: 0x00176090 File Offset: 0x00174290
		[__DynamicallyInvokable]
		public override MessageVersion MessageVersion
		{
			[__DynamicallyInvokable]
			get
			{
				return this.messageVersion;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				if (value.Envelope != BinaryEncoderDefaults.EnvelopeVersion)
				{
					string @string = SR.GetString("UnsupportedEnvelopeVersion", new object[]
					{
						base.GetType().FullName,
						BinaryEncoderDefaults.EnvelopeVersion,
						value.Envelope
					});
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(@string));
				}
				this.messageVersion = MessageVersion.CreateVersion(BinaryEncoderDefaults.EnvelopeVersion, value.Addressing);
			}
		}

		// Token: 0x17001831 RID: 6193
		// (get) Token: 0x06006429 RID: 25641 RVA: 0x00176114 File Offset: 0x00174314
		// (set) Token: 0x0600642A RID: 25642 RVA: 0x0017611C File Offset: 0x0017431C
		[DefaultValue(64)]
		public int MaxReadPoolSize
		{
			get
			{
				return this.maxReadPoolSize;
			}
			set
			{
				if (value <= 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBePositive")));
				}
				this.maxReadPoolSize = value;
			}
		}

		// Token: 0x17001832 RID: 6194
		// (get) Token: 0x0600642B RID: 25643 RVA: 0x0017614E File Offset: 0x0017434E
		// (set) Token: 0x0600642C RID: 25644 RVA: 0x00176156 File Offset: 0x00174356
		[DefaultValue(16)]
		public int MaxWritePoolSize
		{
			get
			{
				return this.maxWritePoolSize;
			}
			set
			{
				if (value <= 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBePositive")));
				}
				this.maxWritePoolSize = value;
			}
		}

		// Token: 0x17001833 RID: 6195
		// (get) Token: 0x0600642D RID: 25645 RVA: 0x00176188 File Offset: 0x00174388
		// (set) Token: 0x0600642E RID: 25646 RVA: 0x00176190 File Offset: 0x00174390
		[__DynamicallyInvokable]
		public XmlDictionaryReaderQuotas ReaderQuotas
		{
			[__DynamicallyInvokable]
			get
			{
				return this.readerQuotas;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				value.CopyTo(this.readerQuotas);
			}
		}

		// Token: 0x17001834 RID: 6196
		// (get) Token: 0x0600642F RID: 25647 RVA: 0x001761B1 File Offset: 0x001743B1
		// (set) Token: 0x06006430 RID: 25648 RVA: 0x001761B9 File Offset: 0x001743B9
		[DefaultValue(2048)]
		[__DynamicallyInvokable]
		public int MaxSessionSize
		{
			[__DynamicallyInvokable]
			get
			{
				return this.maxSessionSize;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBeNonNegative")));
				}
				this.maxSessionSize = value;
			}
		}

		// Token: 0x06006431 RID: 25649 RVA: 0x001761EC File Offset: 0x001743EC
		private void VerifyCompression(BindingContext context)
		{
			if (this.compressionFormat != CompressionFormat.None)
			{
				ITransportCompressionSupport innerProperty = context.GetInnerProperty<ITransportCompressionSupport>();
				if (innerProperty == null || !innerProperty.IsCompressionFormatSupported(this.compressionFormat))
				{
					throw FxTrace.Exception.AsError(new NotSupportedException(SR.GetString("TransportDoesNotSupportCompression", new object[]
					{
						this.compressionFormat.ToString(),
						base.GetType().Name,
						CompressionFormat.None.ToString()
					})));
				}
			}
		}

		// Token: 0x06006432 RID: 25650 RVA: 0x00176270 File Offset: 0x00174470
		private void SetMaxReceivedMessageSizeFromTransport(BindingContext context)
		{
			TransportBindingElement transportBindingElement = context.Binding.Elements.Find<TransportBindingElement>();
			if (transportBindingElement != null)
			{
				this.maxReceivedMessageSize = transportBindingElement.MaxReceivedMessageSize;
			}
		}

		// Token: 0x06006433 RID: 25651 RVA: 0x0017629D File Offset: 0x0017449D
		[__DynamicallyInvokable]
		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
		{
			this.VerifyCompression(context);
			this.SetMaxReceivedMessageSizeFromTransport(context);
			return base.InternalBuildChannelFactory<TChannel>(context);
		}

		// Token: 0x06006434 RID: 25652 RVA: 0x001762B4 File Offset: 0x001744B4
		public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
		{
			this.VerifyCompression(context);
			this.SetMaxReceivedMessageSizeFromTransport(context);
			return base.InternalBuildChannelListener<TChannel>(context);
		}

		// Token: 0x06006435 RID: 25653 RVA: 0x001762CB File Offset: 0x001744CB
		public override bool CanBuildChannelListener<TChannel>(BindingContext context)
		{
			return base.InternalCanBuildChannelListener<TChannel>(context);
		}

		// Token: 0x06006436 RID: 25654 RVA: 0x001762D4 File Offset: 0x001744D4
		[__DynamicallyInvokable]
		public override BindingElement Clone()
		{
			return new BinaryMessageEncodingBindingElement(this);
		}

		// Token: 0x06006437 RID: 25655 RVA: 0x001762DC File Offset: 0x001744DC
		[__DynamicallyInvokable]
		public override MessageEncoderFactory CreateMessageEncoderFactory()
		{
			return new BinaryMessageEncoderFactory(this.MessageVersion, this.MaxReadPoolSize, this.MaxWritePoolSize, this.MaxSessionSize, this.ReaderQuotas, this.maxReceivedMessageSize, this.BinaryVersion, this.CompressionFormat);
		}

		// Token: 0x06006438 RID: 25656 RVA: 0x00176314 File Offset: 0x00174514
		[__DynamicallyInvokable]
		public override T GetProperty<T>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (typeof(T) == typeof(XmlDictionaryReaderQuotas))
			{
				return (T)((object)this.readerQuotas);
			}
			return base.GetProperty<T>(context);
		}

		// Token: 0x06006439 RID: 25657 RVA: 0x00176364 File Offset: 0x00174564
		void IPolicyExportExtension.ExportPolicy(MetadataExporter exporter, PolicyConversionContext policyContext)
		{
			if (policyContext == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("policyContext");
			}
			XmlDocument xmlDocument = new XmlDocument();
			policyContext.GetBindingAssertions().Add(xmlDocument.CreateElement("msb", "BinaryEncoding", "http://schemas.microsoft.com/ws/06/2004/mspolicy/netbinary1"));
		}

		// Token: 0x0600643A RID: 25658 RVA: 0x001763AA File Offset: 0x001745AA
		void IWsdlExportExtension.ExportContract(WsdlExporter exporter, WsdlContractConversionContext context)
		{
		}

		// Token: 0x0600643B RID: 25659 RVA: 0x001763AC File Offset: 0x001745AC
		void IWsdlExportExtension.ExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			SoapHelper.SetSoapVersion(context, exporter, MessageVersion.Soap12WSAddressing10.Envelope);
		}

		// Token: 0x0600643C RID: 25660 RVA: 0x001763D4 File Offset: 0x001745D4
		internal override bool IsMatch(BindingElement b)
		{
			if (!base.IsMatch(b))
			{
				return false;
			}
			BinaryMessageEncodingBindingElement binaryMessageEncodingBindingElement = b as BinaryMessageEncodingBindingElement;
			return binaryMessageEncodingBindingElement != null && this.maxReadPoolSize == binaryMessageEncodingBindingElement.MaxReadPoolSize && this.maxWritePoolSize == binaryMessageEncodingBindingElement.MaxWritePoolSize && this.readerQuotas.MaxStringContentLength == binaryMessageEncodingBindingElement.ReaderQuotas.MaxStringContentLength && this.readerQuotas.MaxArrayLength == binaryMessageEncodingBindingElement.ReaderQuotas.MaxArrayLength && this.readerQuotas.MaxBytesPerRead == binaryMessageEncodingBindingElement.ReaderQuotas.MaxBytesPerRead && this.readerQuotas.MaxDepth == binaryMessageEncodingBindingElement.ReaderQuotas.MaxDepth && this.readerQuotas.MaxNameTableCharCount == binaryMessageEncodingBindingElement.ReaderQuotas.MaxNameTableCharCount && this.MaxSessionSize == binaryMessageEncodingBindingElement.MaxSessionSize && this.CompressionFormat == binaryMessageEncodingBindingElement.CompressionFormat;
		}

		// Token: 0x0600643D RID: 25661 RVA: 0x001764BB File Offset: 0x001746BB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeReaderQuotas()
		{
			return !EncoderDefaults.IsDefaultReaderQuotas(this.ReaderQuotas);
		}

		// Token: 0x0600643E RID: 25662 RVA: 0x001764CB File Offset: 0x001746CB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeMessageVersion()
		{
			return !this.messageVersion.IsMatch(MessageVersion.Default);
		}

		// Token: 0x040039BB RID: 14779
		private int maxReadPoolSize;

		// Token: 0x040039BC RID: 14780
		private int maxWritePoolSize;

		// Token: 0x040039BD RID: 14781
		private XmlDictionaryReaderQuotas readerQuotas;

		// Token: 0x040039BE RID: 14782
		private int maxSessionSize;

		// Token: 0x040039BF RID: 14783
		private BinaryVersion binaryVersion;

		// Token: 0x040039C0 RID: 14784
		private MessageVersion messageVersion;

		// Token: 0x040039C1 RID: 14785
		private CompressionFormat compressionFormat;

		// Token: 0x040039C2 RID: 14786
		private long maxReceivedMessageSize;
	}
}
