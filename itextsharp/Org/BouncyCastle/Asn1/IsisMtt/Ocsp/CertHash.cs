using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.IsisMtt.Ocsp
{
	// Token: 0x020005BC RID: 1468
	public class CertHash : Asn1Encodable
	{
		// Token: 0x06003278 RID: 12920 RVA: 0x00139128 File Offset: 0x00138128
		public static CertHash GetInstance(object obj)
		{
			if (obj == null || obj is CertHash)
			{
				return (CertHash)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CertHash((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06003279 RID: 12921 RVA: 0x0013917C File Offset: 0x0013817C
		private CertHash(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.hashAlgorithm = AlgorithmIdentifier.GetInstance(seq[0]);
			this.certificateHash = Asn1OctetString.GetInstance(seq[1]).GetOctets();
		}

		// Token: 0x0600327A RID: 12922 RVA: 0x001391DC File Offset: 0x001381DC
		public CertHash(AlgorithmIdentifier hashAlgorithm, byte[] certificateHash)
		{
			if (hashAlgorithm == null)
			{
				throw new ArgumentNullException("hashAlgorithm");
			}
			if (certificateHash == null)
			{
				throw new ArgumentNullException("certificateHash");
			}
			this.hashAlgorithm = hashAlgorithm;
			this.certificateHash = (byte[])certificateHash.Clone();
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x0600327B RID: 12923 RVA: 0x00139218 File Offset: 0x00138218
		public AlgorithmIdentifier HashAlgorithm
		{
			get
			{
				return this.hashAlgorithm;
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x0600327C RID: 12924 RVA: 0x00139220 File Offset: 0x00138220
		public byte[] CertificateHash
		{
			get
			{
				return (byte[])this.certificateHash.Clone();
			}
		}

		// Token: 0x0600327D RID: 12925 RVA: 0x00139234 File Offset: 0x00138234
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.hashAlgorithm,
				new DerOctetString(this.certificateHash)
			});
		}

		// Token: 0x04002287 RID: 8839
		private readonly AlgorithmIdentifier hashAlgorithm;

		// Token: 0x04002288 RID: 8840
		private readonly byte[] certificateHash;
	}
}
