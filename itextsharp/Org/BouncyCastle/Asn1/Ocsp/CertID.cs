using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x0200048B RID: 1163
	public class CertID : Asn1Encodable
	{
		// Token: 0x0600275F RID: 10079 RVA: 0x000EDADC File Offset: 0x000ECADC
		public static CertID GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return CertID.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x000EDAEC File Offset: 0x000ECAEC
		public static CertID GetInstance(object obj)
		{
			if (obj == null || obj is CertID)
			{
				return (CertID)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CertID((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x000EDB3E File Offset: 0x000ECB3E
		public CertID(AlgorithmIdentifier hashAlgorithm, Asn1OctetString issuerNameHash, Asn1OctetString issuerKeyHash, DerInteger serialNumber)
		{
			this.hashAlgorithm = hashAlgorithm;
			this.issuerNameHash = issuerNameHash;
			this.issuerKeyHash = issuerKeyHash;
			this.serialNumber = serialNumber;
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x000EDB64 File Offset: 0x000ECB64
		private CertID(Asn1Sequence seq)
		{
			if (seq.Count != 4)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.hashAlgorithm = AlgorithmIdentifier.GetInstance(seq[0]);
			this.issuerNameHash = Asn1OctetString.GetInstance(seq[1]);
			this.issuerKeyHash = Asn1OctetString.GetInstance(seq[2]);
			this.serialNumber = DerInteger.GetInstance(seq[3]);
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06002763 RID: 10083 RVA: 0x000EDBD8 File Offset: 0x000ECBD8
		public AlgorithmIdentifier HashAlgorithm
		{
			get
			{
				return this.hashAlgorithm;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06002764 RID: 10084 RVA: 0x000EDBE0 File Offset: 0x000ECBE0
		public Asn1OctetString IssuerNameHash
		{
			get
			{
				return this.issuerNameHash;
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06002765 RID: 10085 RVA: 0x000EDBE8 File Offset: 0x000ECBE8
		public Asn1OctetString IssuerKeyHash
		{
			get
			{
				return this.issuerKeyHash;
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06002766 RID: 10086 RVA: 0x000EDBF0 File Offset: 0x000ECBF0
		public DerInteger SerialNumber
		{
			get
			{
				return this.serialNumber;
			}
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x000EDBF8 File Offset: 0x000ECBF8
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.hashAlgorithm,
				this.issuerNameHash,
				this.issuerKeyHash,
				this.serialNumber
			});
		}

		// Token: 0x04001B21 RID: 6945
		private readonly AlgorithmIdentifier hashAlgorithm;

		// Token: 0x04001B22 RID: 6946
		private readonly Asn1OctetString issuerNameHash;

		// Token: 0x04001B23 RID: 6947
		private readonly Asn1OctetString issuerKeyHash;

		// Token: 0x04001B24 RID: 6948
		private readonly DerInteger serialNumber;
	}
}
