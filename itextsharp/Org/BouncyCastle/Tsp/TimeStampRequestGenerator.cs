using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Tsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Tsp
{
	// Token: 0x020003CD RID: 973
	public class TimeStampRequestGenerator
	{
		// Token: 0x060021D6 RID: 8662 RVA: 0x000CD40C File Offset: 0x000CC40C
		public void SetReqPolicy(string reqPolicy)
		{
			this.reqPolicy = new DerObjectIdentifier(reqPolicy);
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x000CD41A File Offset: 0x000CC41A
		public void SetCertReq(bool certReq)
		{
			this.certReq = DerBoolean.GetInstance(certReq);
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x000CD428 File Offset: 0x000CC428
		public void AddExtension(string oid, bool critical, Asn1Encodable value)
		{
			this.AddExtension(oid, critical, value.GetEncoded());
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x000CD438 File Offset: 0x000CC438
		public void AddExtension(string oid, bool critical, byte[] value)
		{
			DerObjectIdentifier derObjectIdentifier = new DerObjectIdentifier(oid);
			this.extensions[derObjectIdentifier] = new X509Extension(critical, new DerOctetString(value));
			this.extOrdering.Add(derObjectIdentifier);
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x000CD471 File Offset: 0x000CC471
		public TimeStampRequest Generate(string digestAlgorithm, byte[] digest)
		{
			return this.Generate(digestAlgorithm, digest, null);
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x000CD47C File Offset: 0x000CC47C
		public TimeStampRequest Generate(string digestAlgorithmOid, byte[] digest, BigInteger nonce)
		{
			if (digestAlgorithmOid == null)
			{
				throw new ArgumentException("No digest algorithm specified");
			}
			DerObjectIdentifier objectID = new DerObjectIdentifier(digestAlgorithmOid);
			AlgorithmIdentifier hashAlgorithm = new AlgorithmIdentifier(objectID, DerNull.Instance);
			MessageImprint messageImprint = new MessageImprint(hashAlgorithm, digest);
			X509Extensions x509Extensions = null;
			if (this.extOrdering.Count != 0)
			{
				x509Extensions = new X509Extensions(this.extOrdering, this.extensions);
			}
			DerInteger nonce2 = (nonce == null) ? null : new DerInteger(nonce);
			return new TimeStampRequest(new TimeStampReq(messageImprint, this.reqPolicy, nonce2, this.certReq, x509Extensions));
		}

		// Token: 0x0400174C RID: 5964
		private DerObjectIdentifier reqPolicy;

		// Token: 0x0400174D RID: 5965
		private DerBoolean certReq;

		// Token: 0x0400174E RID: 5966
		private Hashtable extensions = new Hashtable();

		// Token: 0x0400174F RID: 5967
		private ArrayList extOrdering = new ArrayList();
	}
}
