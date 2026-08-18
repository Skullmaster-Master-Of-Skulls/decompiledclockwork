using System;
using System.Configuration;
using System.ServiceModel.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006AA RID: 1706
	public sealed class X509RecipientCertificateClientElement : ConfigurationElement
	{
		// Token: 0x170010F1 RID: 4337
		// (get) Token: 0x06004221 RID: 16929 RVA: 0x000FA850 File Offset: 0x000F8A50
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("defaultCertificate", typeof(X509DefaultServiceCertificateElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("scopedCertificates", typeof(X509ScopedServiceCertificateElementCollection), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("authentication", typeof(X509ServiceCertificateAuthenticationElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("sslCertificateAuthentication", typeof(X509ServiceCertificateAuthenticationElement), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x170010F2 RID: 4338
		// (get) Token: 0x06004223 RID: 16931 RVA: 0x000FA8FB File Offset: 0x000F8AFB
		[ConfigurationProperty("defaultCertificate")]
		public X509DefaultServiceCertificateElement DefaultCertificate
		{
			get
			{
				return (X509DefaultServiceCertificateElement)base["defaultCertificate"];
			}
		}

		// Token: 0x170010F3 RID: 4339
		// (get) Token: 0x06004224 RID: 16932 RVA: 0x000FA90D File Offset: 0x000F8B0D
		[ConfigurationProperty("scopedCertificates")]
		public X509ScopedServiceCertificateElementCollection ScopedCertificates
		{
			get
			{
				return (X509ScopedServiceCertificateElementCollection)base["scopedCertificates"];
			}
		}

		// Token: 0x170010F4 RID: 4340
		// (get) Token: 0x06004225 RID: 16933 RVA: 0x000FA91F File Offset: 0x000F8B1F
		[ConfigurationProperty("authentication")]
		public X509ServiceCertificateAuthenticationElement Authentication
		{
			get
			{
				return (X509ServiceCertificateAuthenticationElement)base["authentication"];
			}
		}

		// Token: 0x170010F5 RID: 4341
		// (get) Token: 0x06004226 RID: 16934 RVA: 0x000FA931 File Offset: 0x000F8B31
		[ConfigurationProperty("sslCertificateAuthentication")]
		public X509ServiceCertificateAuthenticationElement SslCertificateAuthentication
		{
			get
			{
				return (X509ServiceCertificateAuthenticationElement)base["sslCertificateAuthentication"];
			}
		}

		// Token: 0x06004227 RID: 16935 RVA: 0x000FA944 File Offset: 0x000F8B44
		public void Copy(X509RecipientCertificateClientElement from)
		{
			if (this.IsReadOnly())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigReadOnly")));
			}
			if (from == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("from");
			}
			this.DefaultCertificate.Copy(from.DefaultCertificate);
			X509ScopedServiceCertificateElementCollection scopedCertificates = from.ScopedCertificates;
			X509ScopedServiceCertificateElementCollection scopedCertificates2 = this.ScopedCertificates;
			scopedCertificates2.Clear();
			for (int i = 0; i < scopedCertificates.Count; i++)
			{
				scopedCertificates2.Add(scopedCertificates[i]);
			}
			this.Authentication.Copy(from.Authentication);
			this.SslCertificateAuthentication.Copy(from.SslCertificateAuthentication);
		}

		// Token: 0x06004228 RID: 16936 RVA: 0x000FA9EC File Offset: 0x000F8BEC
		internal void ApplyConfiguration(X509CertificateRecipientClientCredential cert)
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
			if (propertyInformationCollection["sslCertificateAuthentication"].ValueOrigin != PropertyValueOrigin.Default)
			{
				cert.SslCertificateAuthentication = new X509ServiceCertificateAuthentication();
				this.SslCertificateAuthentication.ApplyConfiguration(cert.SslCertificateAuthentication);
			}
			this.DefaultCertificate.ApplyConfiguration(cert);
			X509ScopedServiceCertificateElementCollection scopedCertificates = this.ScopedCertificates;
			for (int i = 0; i < scopedCertificates.Count; i++)
			{
				scopedCertificates[i].ApplyConfiguration(cert);
			}
		}

		// Token: 0x04002CF9 RID: 11513
		private ConfigurationPropertyCollection properties;
	}
}
