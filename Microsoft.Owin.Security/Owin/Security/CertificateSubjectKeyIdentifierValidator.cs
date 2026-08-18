using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Owin.Security
{
	// Token: 0x02000035 RID: 53
	public class CertificateSubjectKeyIdentifierValidator : ICertificateValidator
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x000048C6 File Offset: 0x00002AC6
		public CertificateSubjectKeyIdentifierValidator(IEnumerable<string> validSubjectKeyIdentifiers)
		{
			if (validSubjectKeyIdentifiers == null)
			{
				throw new ArgumentNullException("validSubjectKeyIdentifiers");
			}
			this._validSubjectKeyIdentifiers = new HashSet<string>(validSubjectKeyIdentifiers, StringComparer.OrdinalIgnoreCase);
			if (this._validSubjectKeyIdentifiers.Count == 0)
			{
				throw new ArgumentOutOfRangeException("validSubjectKeyIdentifiers");
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004908 File Offset: 0x00002B08
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
				string subjectKeyIdentifier = CertificateSubjectKeyIdentifierValidator.GetSubjectKeyIdentifier(x509ChainElement.Certificate);
				if (!string.IsNullOrWhiteSpace(subjectKeyIdentifier) && this._validSubjectKeyIdentifiers.Contains(subjectKeyIdentifier))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004980 File Offset: 0x00002B80
		private static string GetSubjectKeyIdentifier(X509Certificate2 certificate)
		{
			X509SubjectKeyIdentifierExtension x509SubjectKeyIdentifierExtension = certificate.Extensions["2.5.29.14"] as X509SubjectKeyIdentifierExtension;
			if (x509SubjectKeyIdentifierExtension != null)
			{
				return x509SubjectKeyIdentifierExtension.SubjectKeyIdentifier;
			}
			return null;
		}

		// Token: 0x04000054 RID: 84
		private readonly HashSet<string> _validSubjectKeyIdentifiers;
	}
}
