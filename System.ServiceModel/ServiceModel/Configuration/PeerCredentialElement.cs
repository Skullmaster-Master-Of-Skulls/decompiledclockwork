using System;
using System.Configuration;
using System.ServiceModel.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000668 RID: 1640
	public sealed class PeerCredentialElement : ConfigurationElement
	{
		// Token: 0x17000FAF RID: 4015
		// (get) Token: 0x06003F09 RID: 16137 RVA: 0x000EF834 File Offset: 0x000EDA34
		[ConfigurationProperty("certificate")]
		public X509PeerCertificateElement Certificate
		{
			get
			{
				return (X509PeerCertificateElement)base["certificate"];
			}
		}

		// Token: 0x17000FB0 RID: 4016
		// (get) Token: 0x06003F0A RID: 16138 RVA: 0x000EF846 File Offset: 0x000EDA46
		[ConfigurationProperty("peerAuthentication")]
		public X509PeerCertificateAuthenticationElement PeerAuthentication
		{
			get
			{
				return (X509PeerCertificateAuthenticationElement)base["peerAuthentication"];
			}
		}

		// Token: 0x17000FB1 RID: 4017
		// (get) Token: 0x06003F0B RID: 16139 RVA: 0x000EF858 File Offset: 0x000EDA58
		[ConfigurationProperty("messageSenderAuthentication")]
		public X509PeerCertificateAuthenticationElement MessageSenderAuthentication
		{
			get
			{
				return (X509PeerCertificateAuthenticationElement)base["messageSenderAuthentication"];
			}
		}

		// Token: 0x06003F0C RID: 16140 RVA: 0x000EF86C File Offset: 0x000EDA6C
		public void Copy(PeerCredentialElement from)
		{
			if (this.IsReadOnly())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigReadOnly")));
			}
			if (from == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("from");
			}
			this.Certificate.Copy(from.Certificate);
			this.PeerAuthentication.Copy(from.PeerAuthentication);
			this.MessageSenderAuthentication.Copy(from.MessageSenderAuthentication);
		}

		// Token: 0x06003F0D RID: 16141 RVA: 0x000EF8E4 File Offset: 0x000EDAE4
		internal void ApplyConfiguration(PeerCredential creds)
		{
			if (creds == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("creds");
			}
			PropertyInformationCollection propertyInformationCollection = base.ElementInformation.Properties;
			if (propertyInformationCollection["certificate"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.Certificate.ApplyConfiguration(creds);
			}
			if (propertyInformationCollection["peerAuthentication"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.PeerAuthentication.ApplyConfiguration(creds.PeerAuthentication);
			}
			if (propertyInformationCollection["messageSenderAuthentication"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.MessageSenderAuthentication.ApplyConfiguration(creds.MessageSenderAuthentication);
			}
		}

		// Token: 0x17000FB2 RID: 4018
		// (get) Token: 0x06003F0E RID: 16142 RVA: 0x000EF974 File Offset: 0x000EDB74
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("certificate", typeof(X509PeerCertificateElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("peerAuthentication", typeof(X509PeerCertificateAuthenticationElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("messageSenderAuthentication", typeof(X509PeerCertificateAuthenticationElement), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002CAF RID: 11439
		private ConfigurationPropertyCollection properties;
	}
}
