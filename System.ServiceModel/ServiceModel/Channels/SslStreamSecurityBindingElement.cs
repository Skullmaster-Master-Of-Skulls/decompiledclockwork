using System;
using System.ComponentModel;
using System.Net.Security;
using System.Security.Authentication;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008A6 RID: 2214
	[__DynamicallyInvokable]
	public class SslStreamSecurityBindingElement : StreamUpgradeBindingElement, ITransportTokenAssertionProvider, IPolicyExportExtension
	{
		// Token: 0x0600545E RID: 21598 RVA: 0x00136B95 File Offset: 0x00134D95
		[__DynamicallyInvokable]
		public SslStreamSecurityBindingElement()
		{
			this.requireClientCertificate = false;
			this.sslProtocols = TransportDefaults.SslProtocols;
		}

		// Token: 0x0600545F RID: 21599 RVA: 0x00136BAF File Offset: 0x00134DAF
		protected SslStreamSecurityBindingElement(SslStreamSecurityBindingElement elementToBeCloned) : base(elementToBeCloned)
		{
			this.identityVerifier = elementToBeCloned.identityVerifier;
			this.requireClientCertificate = elementToBeCloned.requireClientCertificate;
			this.sslProtocols = elementToBeCloned.sslProtocols;
		}

		// Token: 0x170014C1 RID: 5313
		// (get) Token: 0x06005460 RID: 21600 RVA: 0x00136BDC File Offset: 0x00134DDC
		// (set) Token: 0x06005461 RID: 21601 RVA: 0x00136BF7 File Offset: 0x00134DF7
		public IdentityVerifier IdentityVerifier
		{
			get
			{
				if (this.identityVerifier == null)
				{
					this.identityVerifier = IdentityVerifier.CreateDefault();
				}
				return this.identityVerifier;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.identityVerifier = value;
			}
		}

		// Token: 0x170014C2 RID: 5314
		// (get) Token: 0x06005462 RID: 21602 RVA: 0x00136C13 File Offset: 0x00134E13
		// (set) Token: 0x06005463 RID: 21603 RVA: 0x00136C1B File Offset: 0x00134E1B
		[DefaultValue(false)]
		public bool RequireClientCertificate
		{
			get
			{
				return this.requireClientCertificate;
			}
			set
			{
				this.requireClientCertificate = value;
			}
		}

		// Token: 0x170014C3 RID: 5315
		// (get) Token: 0x06005464 RID: 21604 RVA: 0x00136C24 File Offset: 0x00134E24
		// (set) Token: 0x06005465 RID: 21605 RVA: 0x00136C2C File Offset: 0x00134E2C
		[DefaultValue(SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12)]
		public SslProtocols SslProtocols
		{
			get
			{
				return this.sslProtocols;
			}
			set
			{
				SslProtocolsHelper.Validate(value);
				this.sslProtocols = value;
			}
		}

		// Token: 0x06005466 RID: 21606 RVA: 0x00136C3B File Offset: 0x00134E3B
		[__DynamicallyInvokable]
		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			context.BindingParameters.Add(this);
			return context.BuildInnerChannelFactory<TChannel>();
		}

		// Token: 0x06005467 RID: 21607 RVA: 0x00136C62 File Offset: 0x00134E62
		[__DynamicallyInvokable]
		public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			context.BindingParameters.Add(this);
			return context.CanBuildInnerChannelFactory<TChannel>();
		}

		// Token: 0x06005468 RID: 21608 RVA: 0x00136C89 File Offset: 0x00134E89
		public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			context.BindingParameters.Add(this);
			return context.BuildInnerChannelListener<TChannel>();
		}

		// Token: 0x06005469 RID: 21609 RVA: 0x00136CB0 File Offset: 0x00134EB0
		public override bool CanBuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			context.BindingParameters.Add(this);
			return context.CanBuildInnerChannelListener<TChannel>();
		}

		// Token: 0x0600546A RID: 21610 RVA: 0x00136CD7 File Offset: 0x00134ED7
		[__DynamicallyInvokable]
		public override BindingElement Clone()
		{
			return new SslStreamSecurityBindingElement(this);
		}

		// Token: 0x0600546B RID: 21611 RVA: 0x00136CE0 File Offset: 0x00134EE0
		[__DynamicallyInvokable]
		public override T GetProperty<T>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (typeof(T) == typeof(ISecurityCapabilities))
			{
				return (T)((object)new SecurityCapabilities(this.RequireClientCertificate, true, this.RequireClientCertificate, ProtectionLevel.EncryptAndSign, ProtectionLevel.EncryptAndSign));
			}
			if (typeof(T) == typeof(IdentityVerifier))
			{
				return (T)((object)this.IdentityVerifier);
			}
			return context.GetInnerProperty<T>();
		}

		// Token: 0x0600546C RID: 21612 RVA: 0x00136D62 File Offset: 0x00134F62
		public override StreamUpgradeProvider BuildClientStreamUpgradeProvider(BindingContext context)
		{
			return SslStreamSecurityUpgradeProvider.CreateClientProvider(this, context);
		}

		// Token: 0x0600546D RID: 21613 RVA: 0x00136D6B File Offset: 0x00134F6B
		public override StreamUpgradeProvider BuildServerStreamUpgradeProvider(BindingContext context)
		{
			return SslStreamSecurityUpgradeProvider.CreateServerProvider(this, context);
		}

		// Token: 0x0600546E RID: 21614 RVA: 0x00136D74 File Offset: 0x00134F74
		internal static void ImportPolicy(MetadataImporter importer, PolicyConversionContext policyContext)
		{
			XmlElement xmlElement = PolicyConversionContext.FindAssertion(policyContext.GetBindingAssertions(), "SslTransportSecurity", "http://schemas.microsoft.com/ws/2006/05/framing/policy", true);
			if (xmlElement != null)
			{
				SslStreamSecurityBindingElement sslStreamSecurityBindingElement = new SslStreamSecurityBindingElement();
				XmlReader xmlReader = new XmlNodeReader(xmlElement);
				xmlReader.ReadStartElement();
				sslStreamSecurityBindingElement.RequireClientCertificate = xmlReader.IsStartElement("RequireClientCertificate", "http://schemas.microsoft.com/ws/2006/05/framing/policy");
				if (sslStreamSecurityBindingElement.RequireClientCertificate)
				{
					xmlReader.ReadElementString();
				}
				policyContext.BindingElements.Add(sslStreamSecurityBindingElement);
			}
		}

		// Token: 0x0600546F RID: 21615 RVA: 0x00136DE0 File Offset: 0x00134FE0
		public XmlElement GetTransportTokenAssertion()
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement("msf", "SslTransportSecurity", "http://schemas.microsoft.com/ws/2006/05/framing/policy");
			if (this.requireClientCertificate)
			{
				xmlElement.AppendChild(xmlDocument.CreateElement("msf", "RequireClientCertificate", "http://schemas.microsoft.com/ws/2006/05/framing/policy"));
			}
			return xmlElement;
		}

		// Token: 0x06005470 RID: 21616 RVA: 0x00136E2E File Offset: 0x0013502E
		void IPolicyExportExtension.ExportPolicy(MetadataExporter exporter, PolicyConversionContext context)
		{
			if (exporter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("exporter");
			}
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			SecurityBindingElement.ExportPolicyForTransportTokenAssertionProviders(exporter, context);
		}

		// Token: 0x06005471 RID: 21617 RVA: 0x00136E60 File Offset: 0x00135060
		internal override bool IsMatch(BindingElement b)
		{
			if (b == null)
			{
				return false;
			}
			SslStreamSecurityBindingElement sslStreamSecurityBindingElement = b as SslStreamSecurityBindingElement;
			return sslStreamSecurityBindingElement != null && this.requireClientCertificate == sslStreamSecurityBindingElement.requireClientCertificate && this.sslProtocols == sslStreamSecurityBindingElement.sslProtocols;
		}

		// Token: 0x06005472 RID: 21618 RVA: 0x00136E9C File Offset: 0x0013509C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeIdentityVerifier()
		{
			return this.IdentityVerifier != IdentityVerifier.CreateDefault();
		}

		// Token: 0x04003312 RID: 13074
		private IdentityVerifier identityVerifier;

		// Token: 0x04003313 RID: 13075
		private bool requireClientCertificate;

		// Token: 0x04003314 RID: 13076
		private SslProtocols sslProtocols;
	}
}
