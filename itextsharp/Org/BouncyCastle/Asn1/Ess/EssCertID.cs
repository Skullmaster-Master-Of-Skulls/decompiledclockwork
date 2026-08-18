using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Ess
{
	// Token: 0x0200057E RID: 1406
	public class EssCertID : Asn1Encodable
	{
		// Token: 0x06002FE3 RID: 12259 RVA: 0x00127A6C File Offset: 0x00126A6C
		public static EssCertID GetInstance(object o)
		{
			if (o == null || o is EssCertID)
			{
				return (EssCertID)o;
			}
			if (o is Asn1Sequence)
			{
				return new EssCertID((Asn1Sequence)o);
			}
			throw new ArgumentException("unknown object in 'EssCertID' factory : " + o.GetType().Name + ".");
		}

		// Token: 0x06002FE4 RID: 12260 RVA: 0x00127AC0 File Offset: 0x00126AC0
		public EssCertID(Asn1Sequence seq)
		{
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.certHash = Asn1OctetString.GetInstance(seq[0]);
			if (seq.Count > 1)
			{
				this.issuerSerial = IssuerSerial.GetInstance(seq[1]);
			}
		}

		// Token: 0x06002FE5 RID: 12261 RVA: 0x00127B2D File Offset: 0x00126B2D
		public EssCertID(byte[] hash)
		{
			this.certHash = new DerOctetString(hash);
		}

		// Token: 0x06002FE6 RID: 12262 RVA: 0x00127B41 File Offset: 0x00126B41
		public EssCertID(byte[] hash, IssuerSerial issuerSerial)
		{
			this.certHash = new DerOctetString(hash);
			this.issuerSerial = issuerSerial;
		}

		// Token: 0x06002FE7 RID: 12263 RVA: 0x00127B5C File Offset: 0x00126B5C
		public byte[] GetCertHash()
		{
			return this.certHash.GetOctets();
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06002FE8 RID: 12264 RVA: 0x00127B69 File Offset: 0x00126B69
		public IssuerSerial IssuerSerial
		{
			get
			{
				return this.issuerSerial;
			}
		}

		// Token: 0x06002FE9 RID: 12265 RVA: 0x00127B74 File Offset: 0x00126B74
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.certHash
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

		// Token: 0x040020E3 RID: 8419
		private Asn1OctetString certHash;

		// Token: 0x040020E4 RID: 8420
		private IssuerSerial issuerSerial;
	}
}
