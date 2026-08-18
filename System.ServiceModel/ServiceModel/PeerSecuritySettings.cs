using System;
using System.ComponentModel;
using System.Net.Security;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000170 RID: 368
	public sealed class PeerSecuritySettings
	{
		// Token: 0x06000AE5 RID: 2789 RVA: 0x000287CA File Offset: 0x000269CA
		public PeerSecuritySettings()
		{
			this.mode = SecurityMode.Transport;
			this.transportSecurity = new PeerTransportSecuritySettings();
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x000287E4 File Offset: 0x000269E4
		internal PeerSecuritySettings(PeerSecuritySettings other)
		{
			this.mode = other.mode;
			this.transportSecurity = new PeerTransportSecuritySettings(other.transportSecurity);
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x00028809 File Offset: 0x00026A09
		internal PeerSecuritySettings(PeerSecurityElement element)
		{
			this.mode = element.Mode;
			this.transportSecurity = new PeerTransportSecuritySettings(element.Transport);
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x0002882E File Offset: 0x00026A2E
		// (set) Token: 0x06000AE9 RID: 2793 RVA: 0x00028836 File Offset: 0x00026A36
		public SecurityMode Mode
		{
			get
			{
				return this.mode;
			}
			set
			{
				if (!SecurityModeHelper.IsDefined(value))
				{
					PeerExceptionHelper.ThrowArgumentOutOfRange_InvalidSecurityMode((int)value);
				}
				this.mode = value;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x0002884D File Offset: 0x00026A4D
		// (set) Token: 0x06000AEB RID: 2795 RVA: 0x00028855 File Offset: 0x00026A55
		public PeerTransportSecuritySettings Transport
		{
			get
			{
				return this.transportSecurity;
			}
			set
			{
				this.transportSecurity = value;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x0002885E File Offset: 0x00026A5E
		internal bool SupportsAuthentication
		{
			get
			{
				return this.Mode == SecurityMode.Transport || this.Mode == SecurityMode.TransportWithMessageCredential;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x00028874 File Offset: 0x00026A74
		internal ProtectionLevel SupportedProtectionLevel
		{
			get
			{
				ProtectionLevel result = ProtectionLevel.None;
				if (this.Mode == SecurityMode.Message || this.Mode == SecurityMode.TransportWithMessageCredential)
				{
					result = ProtectionLevel.Sign;
				}
				return result;
			}
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00028898 File Offset: 0x00026A98
		internal void OnImportPolicy(MetadataImporter importer, PolicyConversionContext context)
		{
			XmlElement xmlElement = PolicyConversionContext.FindAssertion(context.GetBindingAssertions(), "PeerTransportSecurityMode", "http://schemas.microsoft.com/soap/peer", true);
			this.Mode = SecurityMode.Transport;
			if (xmlElement != null)
			{
				string innerText = xmlElement.InnerText;
				if (!(innerText == "PeerTransportSecurityModeNone"))
				{
					if (!(innerText == "PeerTransportSecurityModeTransport"))
					{
						if (!(innerText == "PeerTransportSecurityModeMessage"))
						{
							if (innerText == "PeerTransportSecurityModeTransportWithMessageCredential")
							{
								this.Mode = SecurityMode.TransportWithMessageCredential;
							}
						}
						else
						{
							this.Mode = SecurityMode.Message;
						}
					}
					else
					{
						this.Mode = SecurityMode.Transport;
					}
				}
				else
				{
					this.Mode = SecurityMode.None;
				}
			}
			this.transportSecurity.OnImportPolicy(importer, context);
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x00028934 File Offset: 0x00026B34
		internal void OnExportPolicy(MetadataExporter exporter, PolicyConversionContext context)
		{
			string innerText;
			switch (this.Mode)
			{
			case SecurityMode.None:
				innerText = "PeerTransportSecurityModeNone";
				break;
			case SecurityMode.Transport:
				innerText = "PeerTransportSecurityModeTransport";
				break;
			case SecurityMode.Message:
				innerText = "PeerTransportSecurityModeMessage";
				break;
			case SecurityMode.TransportWithMessageCredential:
				innerText = "PeerTransportSecurityModeTransportWithMessageCredential";
				break;
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement("pc", "PeerTransportSecurityMode", "http://schemas.microsoft.com/soap/peer");
			xmlElement.InnerText = innerText;
			context.GetBindingAssertions().Add(xmlElement);
			this.transportSecurity.OnExportPolicy(exporter, context);
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x000289D2 File Offset: 0x00026BD2
		internal bool InternalShouldSerialize()
		{
			return this.ShouldSerializeMode() || this.ShouldSerializeTransport();
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x000289E4 File Offset: 0x00026BE4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeMode()
		{
			return this.Mode != SecurityMode.Transport;
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x000289F2 File Offset: 0x00026BF2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeTransport()
		{
			return this.Transport.CredentialType > PeerTransportCredentialType.Password;
		}

		// Token: 0x04000BE5 RID: 3045
		internal const SecurityMode DefaultMode = SecurityMode.Transport;

		// Token: 0x04000BE6 RID: 3046
		private SecurityMode mode;

		// Token: 0x04000BE7 RID: 3047
		private PeerTransportSecuritySettings transportSecurity;
	}
}
