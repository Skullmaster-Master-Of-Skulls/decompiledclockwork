using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006ED RID: 1773
	[ConfigurationCollection(typeof(X509CertificateTrustedIssuerElement))]
	public sealed class X509CertificateTrustedIssuerElementCollection : ServiceModelConfigurationElementCollection<X509CertificateTrustedIssuerElement>
	{
		// Token: 0x0600441C RID: 17436 RVA: 0x0010145F File Offset: 0x000FF65F
		protected override object GetElementKey(ConfigurationElement element)
		{
			return element;
		}
	}
}
