using System;
using System.Security.Cryptography.X509Certificates;

namespace System.Net.Security
{
	// Token: 0x02000596 RID: 1430
	// (Invoke) Token: 0x06002BEE RID: 11246
	internal delegate bool RemoteCertValidationCallback(string host, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors);
}
