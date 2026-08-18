using System;
using System.Configuration;
using System.ServiceModel.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006AB RID: 1707
	public sealed class X509InitiatorCertificateServiceElement : ConfigurationElement
	{
		// Token: 0x170010F6 RID: 4342
		// (get) Token: 0x06004229 RID: 16937 RVA: 0x000FAA9C File Offset: 0x000F8C9C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("certificate", typeof(X509ClientCertificateCredentialsElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("authentication", typeof(X509ClientCertificateAuthenticationElement), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x170010F7 RID: 4343
		// (get) Token: 0x0600422B RID: 16939 RVA: 0x000FAB08 File Offset: 0x000F8D08
		[ConfigurationProperty("certificate")]
		public X509ClientCertificateCredentialsElement Certificate
		{
			get
			{
				return (X509ClientCertificateCredentialsElement)base["certificate"];
			}
		}

		// Token: 0x170010F8 RID: 4344
		// (get) Token: 0x0600422C RID: 16940 RVA: 0x000FAB1A File Offset: 0x000F8D1A
		[ConfigurationProperty("authentication")]
		public X509ClientCertificateAuthenticationElement Authentication
		{
			get
			{
				return (X509ClientCertificateAuthenticationElement)base["authentication"];
			}
		}

		// Token: 0x0600422D RID: 16941 RVA: 0x000FAB2C File Offset: 0x000F8D2C
		public void Copy(X509InitiatorCertificateServiceElement from)
		{
			if (this.IsReadOnly())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigReadOnly")));
			}
			if (from == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("from");
			}
			this.Authentication.Copy(from.Authentication);
			this.Certificate.Copy(from.Certificate);
		}

		// Token: 0x0600422E RID: 16942 RVA: 0x000FAB90 File Offset: 0x000F8D90
		internal void ApplyConfiguration(X509CertificateInitiatorServiceCredential cert)
		{
			if (cert == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("cert");
			}
			PropertyInformationCollection propertyInformationCollection = base.ElementInformation.Properties;
			if (propertyInformationCollection["authentication"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.Authentication.ApplyConfiguration(cert.Authentication);
			}
			if (propertyInformationCollection["certificate"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.Certificate.ApplyConfiguration(cert);
			}
		}

		// Token: 0x04002CFA RID: 11514
		private ConfigurationPropertyCollection properties;
	}
}
