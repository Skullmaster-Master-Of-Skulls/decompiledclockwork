using System;
using System.Security.Cryptography.X509Certificates;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000002 RID: 2
	// (Invoke) Token: 0x06000002 RID: 2
	public delegate bool CertificateValidationCallback(X509Certificate certificate, int[] certificateErrors);
}
