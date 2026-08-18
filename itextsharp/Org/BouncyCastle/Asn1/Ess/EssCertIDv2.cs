using System;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Ess
{
	// Token: 0x02000407 RID: 1031
	public class EssCertIDv2 : Asn1Encodable
	{
		// Token: 0x06002320 RID: 8992 RVA: 0x000D8A24 File Offset: 0x000D7A24
		public static EssCertIDv2 GetInstance(object o)
		{
			if (o == null || o is EssCertIDv2)
			{
				return (EssCertIDv2)o;
			}
			if (o is Asn1Sequence)
			{
				return new EssCertIDv2((Asn1Sequence)o);
			}
			throw new ArgumentException("unknown object in 'EssCertIDv2' factory : " + o.GetType().Name + ".");
		}

		// Token: 0x06002321 RID: 8993 RVA: 0x000D8A78 File Offset: 0x000D7A78
		private EssCertIDv2(Asn1Sequence seq)
		{
			if (seq.Count > 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			int num = 0;
			if (seq[0] is Asn1OctetString)
			{
				this.hashAlgorithm = EssCertIDv2.DefaultAlgID;
			}
			else
			{
				this.hashAlgorithm = AlgorithmIdentifier.GetInstance(seq[num++].ToAsn1Object());
			}
			this.certHash = Asn1OctetString.GetInstance(seq[num++].ToAsn1Object()).GetOctets();
			if (seq.Count > num)
			{
				this.issuerSerial = IssuerSerial.GetInstance(Asn1Sequence.GetInstance(seq[num].ToAsn1Object()));
			}
		}

		// Token: 0x06002322 RID: 8994 RVA: 0x000D8B31 File Offset: 0x000D7B31
		public EssCertIDv2(AlgorithmIdentifier algId, byte[] certHash) : this(algId, certHash, null)
		{
		}

		// Token: 0x06002323 RID: 8995 RVA: 0x000D8B3C File Offset: 0x000D7B3C
		public EssCertIDv2(AlgorithmIdentifier algId, byte[] certHash, IssuerSerial issuerSerial)
		{
			if (algId == null)
			{
				this.hashAlgorithm = EssCertIDv2.DefaultAlgID;
			}
			else
			{
				this.hashAlgorithm = algId;
			}
			this.certHash = certHash;
			this.issuerSerial = issuerSerial;
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06002324 RID: 8996 RVA: 0x000D8B69 File Offset: 0x000D7B69
		public AlgorithmIdentifier HashAlgorithm
		{
			get
			{
				return this.hashAlgorithm;
			}
		}

		// Token: 0x06002325 RID: 8997 RVA: 0x000D8B71 File Offset: 0x000D7B71
		public byte[] GetCertHash()
		{
			return Arrays.Clone(this.certHash);
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06002326 RID: 8998 RVA: 0x000D8B7E File Offset: 0x000D7B7E
		public IssuerSerial IssuerSerial
		{
			get
			{
				return this.issuerSerial;
			}
		}

		// Token: 0x06002327 RID: 8999 RVA: 0x000D8B88 File Offset: 0x000D7B88
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			if (!this.hashAlgorithm.Equals(EssCertIDv2.DefaultAlgID))
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.hashAlgorithm
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				new DerOctetString(this.certHash).ToAsn1Object()
			});
			if (this.issuerSerial != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.issuerSerial
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04001866 RID: 6246
		private readonly AlgorithmIdentifier hashAlgorithm;

		// Token: 0x04001867 RID: 6247
		private readonly byte[] certHash;

		// Token: 0x04001868 RID: 6248
		private readonly IssuerSerial issuerSerial;

		// Token: 0x04001869 RID: 6249
		private static readonly AlgorithmIdentifier DefaultAlgID = new AlgorithmIdentifier(NistObjectIdentifiers.IdSha256);
	}
}
