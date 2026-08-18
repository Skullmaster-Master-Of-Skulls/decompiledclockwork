using System;
using System.ComponentModel;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.Text;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009E8 RID: 2536
	public sealed class MtomMessageEncodingBindingElement : MessageEncodingBindingElement, IWsdlExportExtension, IPolicyExportExtension
	{
		// Token: 0x0600644C RID: 25676 RVA: 0x00176613 File Offset: 0x00174813
		public MtomMessageEncodingBindingElement() : this(MessageVersion.Default, TextEncoderDefaults.Encoding)
		{
		}

		// Token: 0x0600644D RID: 25677 RVA: 0x00176628 File Offset: 0x00174828
		public MtomMessageEncodingBindingElement(MessageVersion messageVersion, Encoding writeEncoding)
		{
			if (messageVersion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("messageVersion");
			}
			if (messageVersion == MessageVersion.None)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("MtomEncoderBadMessageVersion", new object[]
				{
					messageVersion.ToString()
				}), "messageVersion"));
			}
			if (writeEncoding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writeEncoding");
			}
			TextEncoderDefaults.ValidateEncoding(writeEncoding);
			this.maxReadPoolSize = 64;
			this.maxWritePoolSize = 16;
			this.readerQuotas = new XmlDictionaryReaderQuotas();
			EncoderDefaults.ReaderQuotas.CopyTo(this.readerQuotas);
			this.maxBufferSize = 65536;
			this.messageVersion = messageVersion;
			this.writeEncoding = writeEncoding;
		}

		// Token: 0x0600644E RID: 25678 RVA: 0x001766E4 File Offset: 0x001748E4
		private MtomMessageEncodingBindingElement(MtomMessageEncodingBindingElement elementToBeCloned) : base(elementToBeCloned)
		{
			this.maxReadPoolSize = elementToBeCloned.maxReadPoolSize;
			this.maxWritePoolSize = elementToBeCloned.maxWritePoolSize;
			this.readerQuotas = new XmlDictionaryReaderQuotas();
			elementToBeCloned.readerQuotas.CopyTo(this.readerQuotas);
			this.maxBufferSize = elementToBeCloned.maxBufferSize;
			this.writeEncoding = elementToBeCloned.writeEncoding;
			this.messageVersion = elementToBeCloned.messageVersion;
		}

		// Token: 0x17001837 RID: 6199
		// (get) Token: 0x0600644F RID: 25679 RVA: 0x00176750 File Offset: 0x00174950
		// (set) Token: 0x06006450 RID: 25680 RVA: 0x00176758 File Offset: 0x00174958
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

		// Token: 0x17001838 RID: 6200
		// (get) Token: 0x06006451 RID: 25681 RVA: 0x0017678A File Offset: 0x0017498A
		// (set) Token: 0x06006452 RID: 25682 RVA: 0x00176792 File Offset: 0x00174992
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

		// Token: 0x17001839 RID: 6201
		// (get) Token: 0x06006453 RID: 25683 RVA: 0x001767C4 File Offset: 0x001749C4
		public XmlDictionaryReaderQuotas ReaderQuotas
		{
			get
			{
				return this.readerQuotas;
			}
		}

		// Token: 0x1700183A RID: 6202
		// (get) Token: 0x06006454 RID: 25684 RVA: 0x001767CC File Offset: 0x001749CC
		// (set) Token: 0x06006455 RID: 25685 RVA: 0x001767D4 File Offset: 0x001749D4
		[DefaultValue(65536)]
		public int MaxBufferSize
		{
			get
			{
				return this.maxBufferSize;
			}
			set
			{
				if (value <= 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBePositive")));
				}
				this.maxBufferSize = value;
			}
		}

		// Token: 0x1700183B RID: 6203
		// (get) Token: 0x06006456 RID: 25686 RVA: 0x00176806 File Offset: 0x00174A06
		// (set) Token: 0x06006457 RID: 25687 RVA: 0x0017680E File Offset: 0x00174A0E
		[TypeConverter(typeof(EncodingConverter))]
		public Encoding WriteEncoding
		{
			get
			{
				return this.writeEncoding;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				TextEncoderDefaults.ValidateEncoding(value);
				this.writeEncoding = value;
			}
		}

		// Token: 0x1700183C RID: 6204
		// (get) Token: 0x06006458 RID: 25688 RVA: 0x00176830 File Offset: 0x00174A30
		// (set) Token: 0x06006459 RID: 25689 RVA: 0x00176838 File Offset: 0x00174A38
		public override MessageVersion MessageVersion
		{
			get
			{
				return this.messageVersion;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				if (value == MessageVersion.None)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("MtomEncoderBadMessageVersion", new object[]
					{
						value.ToString()
					}), "value"));
				}
				this.messageVersion = value;
			}
		}

		// Token: 0x0600645A RID: 25690 RVA: 0x00176895 File Offset: 0x00174A95
		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
		{
			return base.InternalBuildChannelFactory<TChannel>(context);
		}

		// Token: 0x0600645B RID: 25691 RVA: 0x0017689E File Offset: 0x00174A9E
		public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
		{
			return base.InternalCanBuildChannelFactory<TChannel>(context);
		}

		// Token: 0x0600645C RID: 25692 RVA: 0x001768A7 File Offset: 0x00174AA7
		public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
		{
			return base.InternalBuildChannelListener<TChannel>(context);
		}

		// Token: 0x0600645D RID: 25693 RVA: 0x001768B0 File Offset: 0x00174AB0
		public override bool CanBuildChannelListener<TChannel>(BindingContext context)
		{
			return base.InternalCanBuildChannelListener<TChannel>(context);
		}

		// Token: 0x0600645E RID: 25694 RVA: 0x001768B9 File Offset: 0x00174AB9
		public override BindingElement Clone()
		{
			return new MtomMessageEncodingBindingElement(this);
		}

		// Token: 0x0600645F RID: 25695 RVA: 0x001768C1 File Offset: 0x00174AC1
		public override MessageEncoderFactory CreateMessageEncoderFactory()
		{
			return new MtomMessageEncoderFactory(this.MessageVersion, this.WriteEncoding, this.MaxReadPoolSize, this.MaxWritePoolSize, this.MaxBufferSize, this.ReaderQuotas);
		}

		// Token: 0x06006460 RID: 25696 RVA: 0x001768EC File Offset: 0x00174AEC
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

		// Token: 0x06006461 RID: 25697 RVA: 0x0017693C File Offset: 0x00174B3C
		void IPolicyExportExtension.ExportPolicy(MetadataExporter exporter, PolicyConversionContext policyContext)
		{
			if (policyContext == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("policyContext");
			}
			XmlDocument xmlDocument = new XmlDocument();
			policyContext.GetBindingAssertions().Add(xmlDocument.CreateElement("wsoma", "OptimizedMimeSerialization", "http://schemas.xmlsoap.org/ws/2004/09/policy/optimizedmimeserialization"));
		}

		// Token: 0x06006462 RID: 25698 RVA: 0x00176982 File Offset: 0x00174B82
		void IWsdlExportExtension.ExportContract(WsdlExporter exporter, WsdlContractConversionContext context)
		{
		}

		// Token: 0x06006463 RID: 25699 RVA: 0x00176984 File Offset: 0x00174B84
		void IWsdlExportExtension.ExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			SoapHelper.SetSoapVersion(context, exporter, this.messageVersion.Envelope);
		}

		// Token: 0x06006464 RID: 25700 RVA: 0x001769AB File Offset: 0x00174BAB
		internal override bool CheckEncodingVersion(EnvelopeVersion version)
		{
			return this.messageVersion.Envelope == version;
		}

		// Token: 0x06006465 RID: 25701 RVA: 0x001769BC File Offset: 0x00174BBC
		internal override bool IsMatch(BindingElement b)
		{
			if (!base.IsMatch(b))
			{
				return false;
			}
			MtomMessageEncodingBindingElement mtomMessageEncodingBindingElement = b as MtomMessageEncodingBindingElement;
			return mtomMessageEncodingBindingElement != null && this.maxReadPoolSize == mtomMessageEncodingBindingElement.MaxReadPoolSize && this.maxWritePoolSize == mtomMessageEncodingBindingElement.MaxWritePoolSize && this.readerQuotas.MaxStringContentLength == mtomMessageEncodingBindingElement.ReaderQuotas.MaxStringContentLength && this.readerQuotas.MaxArrayLength == mtomMessageEncodingBindingElement.ReaderQuotas.MaxArrayLength && this.readerQuotas.MaxBytesPerRead == mtomMessageEncodingBindingElement.ReaderQuotas.MaxBytesPerRead && this.readerQuotas.MaxDepth == mtomMessageEncodingBindingElement.ReaderQuotas.MaxDepth && this.readerQuotas.MaxNameTableCharCount == mtomMessageEncodingBindingElement.ReaderQuotas.MaxNameTableCharCount && this.maxBufferSize == mtomMessageEncodingBindingElement.MaxBufferSize && !(this.WriteEncoding.EncodingName != mtomMessageEncodingBindingElement.WriteEncoding.EncodingName) && this.MessageVersion.IsMatch(mtomMessageEncodingBindingElement.MessageVersion);
		}

		// Token: 0x06006466 RID: 25702 RVA: 0x00176AC7 File Offset: 0x00174CC7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeMessageVersion()
		{
			return !this.messageVersion.IsMatch(MessageVersion.Default);
		}

		// Token: 0x06006467 RID: 25703 RVA: 0x00176ADC File Offset: 0x00174CDC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeReaderQuotas()
		{
			return !EncoderDefaults.IsDefaultReaderQuotas(this.ReaderQuotas);
		}

		// Token: 0x06006468 RID: 25704 RVA: 0x00176AEC File Offset: 0x00174CEC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeWriteEncoding()
		{
			return this.WriteEncoding != TextEncoderDefaults.Encoding;
		}

		// Token: 0x040039C3 RID: 14787
		private int maxReadPoolSize;

		// Token: 0x040039C4 RID: 14788
		private int maxWritePoolSize;

		// Token: 0x040039C5 RID: 14789
		private XmlDictionaryReaderQuotas readerQuotas;

		// Token: 0x040039C6 RID: 14790
		private int maxBufferSize;

		// Token: 0x040039C7 RID: 14791
		private Encoding writeEncoding;

		// Token: 0x040039C8 RID: 14792
		private MessageVersion messageVersion;
	}
}
