using System;
using System.ComponentModel;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.Text;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009E9 RID: 2537
	[__DynamicallyInvokable]
	public sealed class TextMessageEncodingBindingElement : MessageEncodingBindingElement, IWsdlExportExtension, IPolicyExportExtension
	{
		// Token: 0x06006469 RID: 25705 RVA: 0x00176AFE File Offset: 0x00174CFE
		[__DynamicallyInvokable]
		public TextMessageEncodingBindingElement() : this(MessageVersion.Default, TextEncoderDefaults.Encoding)
		{
		}

		// Token: 0x0600646A RID: 25706 RVA: 0x00176B10 File Offset: 0x00174D10
		[__DynamicallyInvokable]
		public TextMessageEncodingBindingElement(MessageVersion messageVersion, Encoding writeEncoding)
		{
			if (messageVersion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("messageVersion");
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
			this.messageVersion = messageVersion;
			this.writeEncoding = writeEncoding;
		}

		// Token: 0x0600646B RID: 25707 RVA: 0x00176B88 File Offset: 0x00174D88
		private TextMessageEncodingBindingElement(TextMessageEncodingBindingElement elementToBeCloned) : base(elementToBeCloned)
		{
			this.maxReadPoolSize = elementToBeCloned.maxReadPoolSize;
			this.maxWritePoolSize = elementToBeCloned.maxWritePoolSize;
			this.readerQuotas = new XmlDictionaryReaderQuotas();
			elementToBeCloned.readerQuotas.CopyTo(this.readerQuotas);
			this.writeEncoding = elementToBeCloned.writeEncoding;
			this.messageVersion = elementToBeCloned.messageVersion;
		}

		// Token: 0x1700183D RID: 6205
		// (get) Token: 0x0600646C RID: 25708 RVA: 0x00176BE8 File Offset: 0x00174DE8
		// (set) Token: 0x0600646D RID: 25709 RVA: 0x00176BF0 File Offset: 0x00174DF0
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

		// Token: 0x1700183E RID: 6206
		// (get) Token: 0x0600646E RID: 25710 RVA: 0x00176C22 File Offset: 0x00174E22
		// (set) Token: 0x0600646F RID: 25711 RVA: 0x00176C2A File Offset: 0x00174E2A
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

		// Token: 0x1700183F RID: 6207
		// (get) Token: 0x06006470 RID: 25712 RVA: 0x00176C5C File Offset: 0x00174E5C
		// (set) Token: 0x06006471 RID: 25713 RVA: 0x00176C64 File Offset: 0x00174E64
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

		// Token: 0x17001840 RID: 6208
		// (get) Token: 0x06006472 RID: 25714 RVA: 0x00176C85 File Offset: 0x00174E85
		// (set) Token: 0x06006473 RID: 25715 RVA: 0x00176C8D File Offset: 0x00174E8D
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
				this.messageVersion = value;
			}
		}

		// Token: 0x17001841 RID: 6209
		// (get) Token: 0x06006474 RID: 25716 RVA: 0x00176CA9 File Offset: 0x00174EA9
		// (set) Token: 0x06006475 RID: 25717 RVA: 0x00176CB1 File Offset: 0x00174EB1
		[TypeConverter(typeof(EncodingConverter))]
		[__DynamicallyInvokable]
		public Encoding WriteEncoding
		{
			[__DynamicallyInvokable]
			get
			{
				return this.writeEncoding;
			}
			[__DynamicallyInvokable]
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

		// Token: 0x06006476 RID: 25718 RVA: 0x00176CD3 File Offset: 0x00174ED3
		[__DynamicallyInvokable]
		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
		{
			return base.InternalBuildChannelFactory<TChannel>(context);
		}

		// Token: 0x06006477 RID: 25719 RVA: 0x00176CDC File Offset: 0x00174EDC
		public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
		{
			return base.InternalBuildChannelListener<TChannel>(context);
		}

		// Token: 0x06006478 RID: 25720 RVA: 0x00176CE5 File Offset: 0x00174EE5
		public override bool CanBuildChannelListener<TChannel>(BindingContext context)
		{
			return base.InternalCanBuildChannelListener<TChannel>(context);
		}

		// Token: 0x06006479 RID: 25721 RVA: 0x00176CEE File Offset: 0x00174EEE
		[__DynamicallyInvokable]
		public override BindingElement Clone()
		{
			return new TextMessageEncodingBindingElement(this);
		}

		// Token: 0x0600647A RID: 25722 RVA: 0x00176CF6 File Offset: 0x00174EF6
		[__DynamicallyInvokable]
		public override MessageEncoderFactory CreateMessageEncoderFactory()
		{
			return new TextMessageEncoderFactory(this.MessageVersion, this.WriteEncoding, this.MaxReadPoolSize, this.MaxWritePoolSize, this.ReaderQuotas);
		}

		// Token: 0x0600647B RID: 25723 RVA: 0x00176D1C File Offset: 0x00174F1C
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

		// Token: 0x0600647C RID: 25724 RVA: 0x00176D6A File Offset: 0x00174F6A
		void IPolicyExportExtension.ExportPolicy(MetadataExporter exporter, PolicyConversionContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
		}

		// Token: 0x0600647D RID: 25725 RVA: 0x00176D7F File Offset: 0x00174F7F
		void IWsdlExportExtension.ExportContract(WsdlExporter exporter, WsdlContractConversionContext context)
		{
		}

		// Token: 0x0600647E RID: 25726 RVA: 0x00176D81 File Offset: 0x00174F81
		void IWsdlExportExtension.ExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			SoapHelper.SetSoapVersion(context, exporter, this.messageVersion.Envelope);
		}

		// Token: 0x0600647F RID: 25727 RVA: 0x00176DA8 File Offset: 0x00174FA8
		internal override bool CheckEncodingVersion(EnvelopeVersion version)
		{
			return this.messageVersion.Envelope == version;
		}

		// Token: 0x06006480 RID: 25728 RVA: 0x00176DB8 File Offset: 0x00174FB8
		internal override bool IsMatch(BindingElement b)
		{
			if (!base.IsMatch(b))
			{
				return false;
			}
			TextMessageEncodingBindingElement textMessageEncodingBindingElement = b as TextMessageEncodingBindingElement;
			return textMessageEncodingBindingElement != null && this.maxReadPoolSize == textMessageEncodingBindingElement.MaxReadPoolSize && this.maxWritePoolSize == textMessageEncodingBindingElement.MaxWritePoolSize && this.readerQuotas.MaxStringContentLength == textMessageEncodingBindingElement.ReaderQuotas.MaxStringContentLength && this.readerQuotas.MaxArrayLength == textMessageEncodingBindingElement.ReaderQuotas.MaxArrayLength && this.readerQuotas.MaxBytesPerRead == textMessageEncodingBindingElement.ReaderQuotas.MaxBytesPerRead && this.readerQuotas.MaxDepth == textMessageEncodingBindingElement.ReaderQuotas.MaxDepth && this.readerQuotas.MaxNameTableCharCount == textMessageEncodingBindingElement.ReaderQuotas.MaxNameTableCharCount && !(this.WriteEncoding.EncodingName != textMessageEncodingBindingElement.WriteEncoding.EncodingName) && this.MessageVersion.IsMatch(textMessageEncodingBindingElement.MessageVersion);
		}

		// Token: 0x06006481 RID: 25729 RVA: 0x00176EB3 File Offset: 0x001750B3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeReaderQuotas()
		{
			return !EncoderDefaults.IsDefaultReaderQuotas(this.ReaderQuotas);
		}

		// Token: 0x06006482 RID: 25730 RVA: 0x00176EC3 File Offset: 0x001750C3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeWriteEncoding()
		{
			return this.WriteEncoding != TextEncoderDefaults.Encoding;
		}

		// Token: 0x040039C9 RID: 14793
		private int maxReadPoolSize;

		// Token: 0x040039CA RID: 14794
		private int maxWritePoolSize;

		// Token: 0x040039CB RID: 14795
		private XmlDictionaryReaderQuotas readerQuotas;

		// Token: 0x040039CC RID: 14796
		private MessageVersion messageVersion;

		// Token: 0x040039CD RID: 14797
		private Encoding writeEncoding;
	}
}
