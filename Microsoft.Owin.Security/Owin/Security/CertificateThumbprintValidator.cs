using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Owin.Security
{
	// Token: 0x02000022 RID: 34
	public class CertificateThumbprintValidator : ICertificateValidator
	{
		// Token: 0x0600008B RID: 139 RVA: 0x0000412E File Offset: 0x0000232E
		public CertificateThumbprintValidator(IEnumerable<string> validThumbprints)
		{
			if (validThumbprints == null)
			{
				throw new ArgumentNullException("validThumbprints");
			}
			this._validCertificateThumbprints = new HashSet<string>(validThumbprints, StringComparer.OrdinalIgnoreCase);
			if (this._validCertificateThumbprints.Count == 0)
			{
				throw new ArgumentOutOfRangeException("validThumbprints");
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004170 File Offset: 0x00002370
		public bool Validate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			if (sslPolicyErrors != SslPolicyErrors.None)
			{
				return false;
			}
			if (chain == null)
			{
				throw new ArgumentNullException("chain");
			}
			if (chain.ChainElements.Count < 2)
			{
				return false;
			}
			foreach (X509ChainElement x509ChainElement in chain.ChainElements)
			{
				string thumbprint = x509ChainElement.Certificate.Thumbprint;
				if (thumbprint != null && this._validCertificateThumbprints.Contains(thumbprint))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000038 RID: 56
		private readonly HashSet<string> _validCertificateThumbprints;
	}
}
