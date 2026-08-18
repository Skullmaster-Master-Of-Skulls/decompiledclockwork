using System;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Ocsp
{
	// Token: 0x02000113 RID: 275
	public class Req : X509ExtensionBase
	{
		// Token: 0x06000A74 RID: 2676 RVA: 0x000378BE File Offset: 0x000368BE
		public Req(Request req)
		{
			this.req = req;
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x000378CD File Offset: 0x000368CD
		public CertificateID GetCertID()
		{
			return new CertificateID(this.req.ReqCert);
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x000378DF File Offset: 0x000368DF
		public X509Extensions SingleRequestExtensions
		{
			get
			{
				return this.req.SingleRequestExtensions;
			}
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x000378EC File Offset: 0x000368EC
		protected override X509Extensions GetX509Extensions()
		{
			return this.SingleRequestExtensions;
		}

		// Token: 0x04000867 RID: 2151
		private Request req;
	}
}
