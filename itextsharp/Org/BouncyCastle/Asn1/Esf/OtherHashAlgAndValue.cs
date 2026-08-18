using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x02000262 RID: 610
	public class OtherHashAlgAndValue : Asn1Encodable
	{
		// Token: 0x0600170F RID: 5903 RVA: 0x000851E8 File Offset: 0x000841E8
		public static OtherHashAlgAndValue GetInstance(object obj)
		{
			if (obj == null || obj is OtherHashAlgAndValue)
			{
				return (OtherHashAlgAndValue)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OtherHashAlgAndValue((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'OtherHashAlgAndValue' factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x0008523C File Offset: 0x0008423C
		private OtherHashAlgAndValue(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.hashAlgorithm = AlgorithmIdentifier.GetInstance(seq[0].ToAsn1Object());
			this.hashValue = (Asn1OctetString)seq[1].ToAsn1Object();
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x000852B4 File Offset: 0x000842B4
		public OtherHashAlgAndValue(AlgorithmIdentifier hashAlgorithm, byte[] hashValue)
		{
			if (hashAlgorithm == null)
			{
				throw new ArgumentNullException("hashAlgorithm");
			}
			if (hashValue == null)
			{
				throw new ArgumentNullException("hashValue");
			}
			this.hashAlgorithm = hashAlgorithm;
			this.hashValue = new DerOctetString(hashValue);
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x000852EB File Offset: 0x000842EB
		public OtherHashAlgAndValue(AlgorithmIdentifier hashAlgorithm, Asn1OctetString hashValue)
		{
			if (hashAlgorithm == null)
			{
				throw new ArgumentNullException("hashAlgorithm");
			}
			if (hashValue == null)
			{
				throw new ArgumentNullException("hashValue");
			}
			this.hashAlgorithm = hashAlgorithm;
			this.hashValue = hashValue;
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001713 RID: 5907 RVA: 0x0008531D File Offset: 0x0008431D
		public AlgorithmIdentifier HashAlgorithm
		{
			get
			{
				return this.hashAlgorithm;
			}
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x00085325 File Offset: 0x00084325
		public byte[] GetHashValue()
		{
			return this.hashValue.GetOctets();
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x00085334 File Offset: 0x00084334
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.hashAlgorithm,
				this.hashValue
			});
		}

		// Token: 0x04000FCB RID: 4043
		private readonly AlgorithmIdentifier hashAlgorithm;

		// Token: 0x04000FCC RID: 4044
		private readonly Asn1OctetString hashValue;
	}
}
