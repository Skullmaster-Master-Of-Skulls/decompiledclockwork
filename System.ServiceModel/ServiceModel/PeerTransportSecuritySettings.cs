using System;
using System.ComponentModel;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000173 RID: 371
	public sealed class PeerTransportSecuritySettings
	{
		// Token: 0x06000AF4 RID: 2804 RVA: 0x00028A0D File Offset: 0x00026C0D
		public PeerTransportSecuritySettings()
		{
			this.credentialType = PeerTransportCredentialType.Password;
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00028A1C File Offset: 0x00026C1C
		internal PeerTransportSecuritySettings(PeerTransportSecuritySettings other)
		{
			this.credentialType = other.credentialType;
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x00028A30 File Offset: 0x00026C30
		internal PeerTransportSecuritySettings(PeerTransportSecurityElement element)
		{
			this.credentialType = element.CredentialType;
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x00028A44 File Offset: 0x00026C44
		// (set) Token: 0x06000AF8 RID: 2808 RVA: 0x00028A4C File Offset: 0x00026C4C
		public PeerTransportCredentialType CredentialType
		{
			get
			{
				return this.credentialType;
			}
			set
			{
				if (!PeerTransportCredentialTypeHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("value", (int)value, typeof(PeerTransportCredentialType)));
				}
				this.credentialType = value;
			}
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x00028A80 File Offset: 0x00026C80
		internal void OnImportPolicy(MetadataImporter importer, PolicyConversionContext context)
		{
			XmlElement xmlElement = PolicyConversionContext.FindAssertion(context.GetBindingAssertions(), "PeerTransportCredentialType", "http://schemas.microsoft.com/soap/peer", true);
			PeerTransportCredentialType peerTransportCredentialType = PeerTransportCredentialType.Password;
			if (xmlElement != null)
			{
				string innerText = xmlElement.InnerText;
				if (!(innerText == "PeerTransportCredentialTypePassword"))
				{
					if (innerText == "PeerTransportCredentialTypeCertificate")
					{
						peerTransportCredentialType = PeerTransportCredentialType.Certificate;
					}
				}
				else
				{
					peerTransportCredentialType = PeerTransportCredentialType.Password;
				}
			}
			this.CredentialType = peerTransportCredentialType;
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00028ADC File Offset: 0x00026CDC
		internal void OnExportPolicy(MetadataExporter exporter, PolicyConversionContext context)
		{
			PeerTransportCredentialType peerTransportCredentialType = this.CredentialType;
			string innerText;
			if (peerTransportCredentialType != PeerTransportCredentialType.Password)
			{
				if (peerTransportCredentialType != PeerTransportCredentialType.Certificate)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
				}
				innerText = "PeerTransportCredentialTypeCertificate";
			}
			else
			{
				innerText = "PeerTransportCredentialTypePassword";
			}
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement("pc", "PeerTransportCredentialType", "http://schemas.microsoft.com/soap/peer");
			xmlElement.InnerText = innerText;
			context.GetBindingAssertions().Add(xmlElement);
		}

		// Token: 0x04000BEB RID: 3051
		internal const PeerTransportCredentialType DefaultCredentialType = PeerTransportCredentialType.Password;

		// Token: 0x04000BEC RID: 3052
		private PeerTransportCredentialType credentialType;
	}
}
