using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Owin.Security
{
	// Token: 0x02000021 RID: 33
	public interface ICertificateValidator
	{
		// Token: 0x0600008A RID: 138
		bool Validate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors);
	}
}
