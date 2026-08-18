using System;
using System.ComponentModel;
using System.Net.Security;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008AE RID: 2222
	[__DynamicallyInvokable]
	public class WindowsStreamSecurityBindingElement : StreamUpgradeBindingElement, ITransportTokenAssertionProvider, IPolicyExportExtension
	{
		// Token: 0x060054B7 RID: 21687 RVA: 0x00137C62 File Offset: 0x00135E62
		[__DynamicallyInvokable]
		public WindowsStreamSecurityBindingElement()
		{
			this.protectionLevel = ProtectionLevel.EncryptAndSign;
		}

		// Token: 0x060054B8 RID: 21688 RVA: 0x00137C71 File Offset: 0x00135E71
		protected WindowsStreamSecurityBindingElement(WindowsStreamSecurityBindingElement elementToBeCloned) : base(elementToBeCloned)
		{
			this.protectionLevel = elementToBeCloned.protectionLevel;
		}

		// Token: 0x170014D4 RID: 5332
		// (get) Token: 0x060054B9 RID: 21689 RVA: 0x00137C86 File Offset: 0x00135E86
		// (set) Token: 0x060054BA RID: 21690 RVA: 0x00137C8E File Offset: 0x00135E8E
		[DefaultValue(ProtectionLevel.EncryptAndSign)]
		public ProtectionLevel ProtectionLevel
		{
			get
			{
				return this.protectionLevel;
			}
			set
			{
				ProtectionLevelHelper.Validate(value);
				this.protectionLevel = value;
			}
		}

		// Token: 0x060054BB RID: 21691 RVA: 0x00137C9D File Offset: 0x00135E9D
		[__DynamicallyInvokable]
		public override BindingElement Clone()
		{
			return new WindowsStreamSecurityBindingElement(this);
		}

		// Token: 0x060054BC RID: 21692 RVA: 0x00137CA5 File Offset: 0x00135EA5
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

		// Token: 0x060054BD RID: 21693 RVA: 0x00137CCC File Offset: 0x00135ECC
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

		// Token: 0x060054BE RID: 21694 RVA: 0x00137CF3 File Offset: 0x00135EF3
		public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			context.BindingParameters.Add(this);
			return context.BuildInnerChannelListener<TChannel>();
		}

		// Token: 0x060054BF RID: 21695 RVA: 0x00137D1A File Offset: 0x00135F1A
		public override bool CanBuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			context.BindingParameters.Add(this);
			return context.CanBuildInnerChannelListener<TChannel>();
		}

		// Token: 0x060054C0 RID: 21696 RVA: 0x00137D41 File Offset: 0x00135F41
		public override StreamUpgradeProvider BuildClientStreamUpgradeProvider(BindingContext context)
		{
			return new WindowsStreamSecurityUpgradeProvider(this, context, true);
		}

		// Token: 0x060054C1 RID: 21697 RVA: 0x00137D4B File Offset: 0x00135F4B
		public override StreamUpgradeProvider BuildServerStreamUpgradeProvider(BindingContext context)
		{
			return new WindowsStreamSecurityUpgradeProvider(this, context, false);
		}

		// Token: 0x060054C2 RID: 21698 RVA: 0x00137D58 File Offset: 0x00135F58
		[__DynamicallyInvokable]
		public override T GetProperty<T>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (typeof(T) == typeof(ISecurityCapabilities))
			{
				return (T)((object)new SecurityCapabilities(true, true, true, this.protectionLevel, this.protectionLevel));
			}
			if (typeof(T) == typeof(IdentityVerifier))
			{
				return (T)((object)IdentityVerifier.CreateDefault());
			}
			return context.GetInnerProperty<T>();
		}

		// Token: 0x060054C3 RID: 21699 RVA: 0x00137DDC File Offset: 0x00135FDC
		internal static void ImportPolicy(MetadataImporter importer, PolicyConversionContext policyContext)
		{
			XmlElement xmlElement = PolicyConversionContext.FindAssertion(policyContext.GetBindingAssertions(), "WindowsTransportSecurity", "http://schemas.microsoft.com/ws/2006/05/framing/policy", true);
			if (xmlElement != null)
			{
				WindowsStreamSecurityBindingElement windowsStreamSecurityBindingElement = new WindowsStreamSecurityBindingElement();
				XmlReader xmlReader = new XmlNodeReader(xmlElement);
				xmlReader.ReadStartElement();
				string value = null;
				if (xmlReader.IsStartElement("ProtectionLevel", "http://schemas.microsoft.com/ws/2006/05/framing/policy") && !xmlReader.IsEmptyElement)
				{
					value = xmlReader.ReadElementContentAsString();
				}
				if (string.IsNullOrEmpty(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ExpectedElementMissing", new object[]
					{
						"ProtectionLevel",
						"http://schemas.microsoft.com/ws/2006/05/framing/policy"
					})));
				}
				windowsStreamSecurityBindingElement.ProtectionLevel = (ProtectionLevel)Enum.Parse(typeof(ProtectionLevel), value);
				policyContext.BindingElements.Add(windowsStreamSecurityBindingElement);
			}
		}

		// Token: 0x060054C4 RID: 21700 RVA: 0x00137E9C File Offset: 0x0013609C
		public XmlElement GetTransportTokenAssertion()
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement("msf", "WindowsTransportSecurity", "http://schemas.microsoft.com/ws/2006/05/framing/policy");
			XmlElement xmlElement2 = xmlDocument.CreateElement("msf", "ProtectionLevel", "http://schemas.microsoft.com/ws/2006/05/framing/policy");
			xmlElement2.AppendChild(xmlDocument.CreateTextNode(this.ProtectionLevel.ToString()));
			xmlElement.AppendChild(xmlElement2);
			return xmlElement;
		}

		// Token: 0x060054C5 RID: 21701 RVA: 0x00137F05 File Offset: 0x00136105
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

		// Token: 0x060054C6 RID: 21702 RVA: 0x00137F34 File Offset: 0x00136134
		internal override bool IsMatch(BindingElement b)
		{
			if (b == null)
			{
				return false;
			}
			WindowsStreamSecurityBindingElement windowsStreamSecurityBindingElement = b as WindowsStreamSecurityBindingElement;
			return windowsStreamSecurityBindingElement != null && this.protectionLevel == windowsStreamSecurityBindingElement.protectionLevel;
		}

		// Token: 0x04003343 RID: 13123
		private ProtectionLevel protectionLevel;
	}
}
