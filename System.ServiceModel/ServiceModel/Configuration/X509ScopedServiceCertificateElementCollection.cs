using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006EE RID: 1774
	[ConfigurationCollection(typeof(X509ScopedServiceCertificateElement))]
	public sealed class X509ScopedServiceCertificateElementCollection : ServiceModelConfigurationElementCollection<X509ScopedServiceCertificateElement>
	{
		// Token: 0x0600441E RID: 17438 RVA: 0x0010146C File Offset: 0x000FF66C
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			X509ScopedServiceCertificateElement x509ScopedServiceCertificateElement = (X509ScopedServiceCertificateElement)element;
			return x509ScopedServiceCertificateElement.TargetUri;
		}
	}
}
