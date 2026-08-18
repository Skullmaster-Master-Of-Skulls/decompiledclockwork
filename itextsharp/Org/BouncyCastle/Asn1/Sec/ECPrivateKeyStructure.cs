using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.Sec
{
	// Token: 0x0200025B RID: 603
	public class ECPrivateKeyStructure : Asn1Encodable
	{
		// Token: 0x060016E1 RID: 5857 RVA: 0x000848E1 File Offset: 0x000838E1
		public ECPrivateKeyStructure(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			this.seq = seq;
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x00084900 File Offset: 0x00083900
		public ECPrivateKeyStructure(BigInteger key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.seq = new DerSequence(new Asn1Encodable[]
			{
				new DerInteger(1),
				new DerOctetString(key.ToByteArrayUnsigned())
			});
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x0008494B File Offset: 0x0008394B
		public ECPrivateKeyStructure(BigInteger key, Asn1Encodable parameters) : this(key, null, parameters)
		{
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x00084958 File Offset: 0x00083958
		public ECPrivateKeyStructure(BigInteger key, DerBitString publicKey, Asn1Encodable parameters)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				new DerInteger(1),
				new DerOctetString(key.ToByteArrayUnsigned())
			});
			if (parameters != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, parameters)
				});
			}
			if (publicKey != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 1, publicKey)
				});
			}
			this.seq = new DerSequence(asn1EncodableVector);
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x000849E4 File Offset: 0x000839E4
		public BigInteger GetKey()
		{
			Asn1OctetString asn1OctetString = (Asn1OctetString)this.seq[1];
			return new BigInteger(1, asn1OctetString.GetOctets());
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x00084A0F File Offset: 0x00083A0F
		public DerBitString GetPublicKey()
		{
			return (DerBitString)this.GetObjectInTag(1);
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x00084A1D File Offset: 0x00083A1D
		public Asn1Object GetParameters()
		{
			return this.GetObjectInTag(0);
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x00084A28 File Offset: 0x00083A28
		private Asn1Object GetObjectInTag(int tagNo)
		{
			foreach (object obj in this.seq)
			{
				Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
				Asn1Object asn1Object = asn1Encodable.ToAsn1Object();
				if (asn1Object is Asn1TaggedObject)
				{
					Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)asn1Object;
					if (asn1TaggedObject.TagNo == tagNo)
					{
						return asn1TaggedObject.GetObject();
					}
				}
			}
			return null;
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x00084AAC File Offset: 0x00083AAC
		public override Asn1Object ToAsn1Object()
		{
			return this.seq;
		}

		// Token: 0x04000FB7 RID: 4023
		private readonly Asn1Sequence seq;
	}
}
