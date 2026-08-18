using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Ess
{
	// Token: 0x0200051D RID: 1309
	[Obsolete("Use version in Asn1.Esf instead")]
	public class OtherCertID : Asn1Encodable
	{
		// Token: 0x06002CA9 RID: 11433 RVA: 0x0010F4AC File Offset: 0x0010E4AC
		public static OtherCertID GetInstance(object o)
		{
			if (o == null || o is OtherCertID)
			{
				return (OtherCertID)o;
			}
			if (o is Asn1Sequence)
			{
				return new OtherCertID((Asn1Sequence)o);
			}
			throw new ArgumentException("unknown object in 'OtherCertID' factory : " + o.GetType().Name + ".");
		}

		// Token: 0x06002CAA RID: 11434 RVA: 0x0010F500 File Offset: 0x0010E500
		public OtherCertID(Asn1Sequence seq)
		{
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			if (seq[0].ToAsn1Object() is Asn1OctetString)
			{
				this.otherCertHash = Asn1OctetString.GetInstance(seq[0]);
			}
			else
			{
				this.otherCertHash = DigestInfo.GetInstance(seq[0]);
			}
			if (seq.Count > 1)
			{
				this.issuerSerial = IssuerSerial.GetInstance(Asn1Sequence.GetInstance(seq[1]));
			}
		}

		// Token: 0x06002CAB RID: 11435 RVA: 0x0010F599 File Offset: 0x0010E599
		public OtherCertID(AlgorithmIdentifier algId, byte[] digest)
		{
			this.otherCertHash = new DigestInfo(algId, digest);
		}

		// Token: 0x06002CAC RID: 11436 RVA: 0x0010F5AE File Offset: 0x0010E5AE
		public OtherCertID(AlgorithmIdentifier algId, byte[] digest, IssuerSerial issuerSerial)
		{
			this.otherCertHash = new DigestInfo(algId, digest);
			this.issuerSerial = issuerSerial;
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06002CAD RID: 11437 RVA: 0x0010F5CA File Offset: 0x0010E5CA
		public AlgorithmIdentifier AlgorithmHash
		{
			get
			{
				if (this.otherCertHash.ToAsn1Object() is Asn1OctetString)
				{
					return new AlgorithmIdentifier("1.3.14.3.2.26");
				}
				return DigestInfo.GetInstance(this.otherCertHash).AlgorithmID;
			}
		}

		// Token: 0x06002CAE RID: 11438 RVA: 0x0010F5F9 File Offset: 0x0010E5F9
		public byte[] GetCertHash()
		{
			if (this.otherCertHash.ToAsn1Object() is Asn1OctetString)
			{
				return ((Asn1OctetString)this.otherCertHash.ToAsn1Object()).GetOctets();
			}
			return DigestInfo.GetInstance(this.otherCertHash).GetDigest();
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06002CAF RID: 11439 RVA: 0x0010F633 File Offset: 0x0010E633
		public IssuerSerial IssuerSerial
		{
			get
			{
				return this.issuerSerial;
			}
		}

		// Token: 0x06002CB0 RID: 11440 RVA: 0x0010F63C File Offset: 0x0010E63C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.otherCertHash
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

		// Token: 0x04001EB3 RID: 7859
		private Asn1Encodable otherCertHash;

		// Token: 0x04001EB4 RID: 7860
		private IssuerSerial issuerSerial;
	}
}
