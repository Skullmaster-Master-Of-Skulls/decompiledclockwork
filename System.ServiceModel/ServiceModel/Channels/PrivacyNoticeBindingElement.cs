using System;
using System.ServiceModel.Description;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008CB RID: 2251
	public sealed class PrivacyNoticeBindingElement : BindingElement, IPolicyExportExtension
	{
		// Token: 0x060055F0 RID: 22000 RVA: 0x0013A900 File Offset: 0x00138B00
		public PrivacyNoticeBindingElement()
		{
			this.url = null;
		}

		// Token: 0x060055F1 RID: 22001 RVA: 0x0013A90F File Offset: 0x00138B0F
		public PrivacyNoticeBindingElement(PrivacyNoticeBindingElement elementToBeCloned) : base(elementToBeCloned)
		{
			this.url = elementToBeCloned.url;
			this.version = elementToBeCloned.version;
		}

		// Token: 0x17001500 RID: 5376
		// (get) Token: 0x060055F2 RID: 22002 RVA: 0x0013A930 File Offset: 0x00138B30
		// (set) Token: 0x060055F3 RID: 22003 RVA: 0x0013A938 File Offset: 0x00138B38
		public Uri Url
		{
			get
			{
				return this.url;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.url = value;
			}
		}

		// Token: 0x17001501 RID: 5377
		// (get) Token: 0x060055F4 RID: 22004 RVA: 0x0013A95A File Offset: 0x00138B5A
		// (set) Token: 0x060055F5 RID: 22005 RVA: 0x0013A962 File Offset: 0x00138B62
		public int Version
		{
			get
			{
				return this.version;
			}
			set
			{
				if (value < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBePositive")));
				}
				this.version = value;
			}
		}

		// Token: 0x060055F6 RID: 22006 RVA: 0x0013A994 File Offset: 0x00138B94
		public override BindingElement Clone()
		{
			return new PrivacyNoticeBindingElement(this);
		}

		// Token: 0x060055F7 RID: 22007 RVA: 0x0013A99C File Offset: 0x00138B9C
		public override T GetProperty<T>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			return context.GetInnerProperty<T>();
		}

		// Token: 0x060055F8 RID: 22008 RVA: 0x0013A9B8 File Offset: 0x00138BB8
		void IPolicyExportExtension.ExportPolicy(MetadataExporter exporter, PolicyConversionContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (context.BindingElements != null)
			{
				PrivacyNoticeBindingElement privacyNoticeBindingElement = context.BindingElements.Find<PrivacyNoticeBindingElement>();
				if (privacyNoticeBindingElement != null)
				{
					XmlDocument xmlDocument = new XmlDocument();
					XmlElement xmlElement = xmlDocument.CreateElement("ic", "PrivacyNotice", "http://schemas.xmlsoap.org/ws/2005/05/identity");
					xmlElement.InnerText = privacyNoticeBindingElement.Url.ToString();
					xmlElement.SetAttribute("Version", "http://schemas.xmlsoap.org/ws/2005/05/identity", XmlConvert.ToString(privacyNoticeBindingElement.Version));
					context.GetBindingAssertions().Add(xmlElement);
				}
			}
		}

		// Token: 0x060055F9 RID: 22009 RVA: 0x0013AA44 File Offset: 0x00138C44
		internal override bool IsMatch(BindingElement b)
		{
			if (b == null)
			{
				return false;
			}
			PrivacyNoticeBindingElement privacyNoticeBindingElement = b as PrivacyNoticeBindingElement;
			return privacyNoticeBindingElement != null && this.url == privacyNoticeBindingElement.url && this.version == privacyNoticeBindingElement.version;
		}

		// Token: 0x04003513 RID: 13587
		private Uri url;

		// Token: 0x04003514 RID: 13588
		private int version;
	}
}
