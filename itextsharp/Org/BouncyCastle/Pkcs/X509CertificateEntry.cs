using System;
using System.Collections;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Pkcs
{
	// Token: 0x020002E7 RID: 743
	public class X509CertificateEntry : Pkcs12Entry
	{
		// Token: 0x06001B82 RID: 7042 RVA: 0x000A5560 File Offset: 0x000A4560
		public X509CertificateEntry(X509Certificate cert) : base(new Hashtable())
		{
			this.cert = cert;
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x000A5574 File Offset: 0x000A4574
		public X509CertificateEntry(X509Certificate cert, Hashtable attributes) : base(attributes)
		{
			this.cert = cert;
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001B84 RID: 7044 RVA: 0x000A5584 File Offset: 0x000A4584
		public X509Certificate Certificate
		{
			get
			{
				return this.cert;
			}
		}

		// Token: 0x06001B85 RID: 7045 RVA: 0x000A558C File Offset: 0x000A458C
		public override bool Equals(object obj)
		{
			X509CertificateEntry x509CertificateEntry = obj as X509CertificateEntry;
			return x509CertificateEntry != null && this.cert.Equals(x509CertificateEntry.cert);
		}

		// Token: 0x06001B86 RID: 7046 RVA: 0x000A55B6 File Offset: 0x000A45B6
		public override int GetHashCode()
		{
			return ~this.cert.GetHashCode();
		}

		// Token: 0x040012F8 RID: 4856
		private readonly X509Certificate cert;
	}
}
