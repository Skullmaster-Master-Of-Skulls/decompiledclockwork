using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000488 RID: 1160
	public class ObjectDigestInfo : Asn1Encodable
	{
		// Token: 0x06002749 RID: 10057 RVA: 0x000ED680 File Offset: 0x000EC680
		public static ObjectDigestInfo GetInstance(object obj)
		{
			if (obj == null || obj is ObjectDigestInfo)
			{
				return (ObjectDigestInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new ObjectDigestInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x0600274A RID: 10058 RVA: 0x000ED6D2 File Offset: 0x000EC6D2
		public static ObjectDigestInfo GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return ObjectDigestInfo.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x0600274B RID: 10059 RVA: 0x000ED6E0 File Offset: 0x000EC6E0
		public ObjectDigestInfo(int digestedObjectType, string otherObjectTypeID, AlgorithmIdentifier digestAlgorithm, byte[] objectDigest)
		{
			this.digestedObjectType = new DerEnumerated(digestedObjectType);
			if (digestedObjectType == 2)
			{
				this.otherObjectTypeID = new DerObjectIdentifier(otherObjectTypeID);
			}
			this.digestAlgorithm = digestAlgorithm;
			this.objectDigest = new DerBitString(objectDigest);
		}

		// Token: 0x0600274C RID: 10060 RVA: 0x000ED718 File Offset: 0x000EC718
		private ObjectDigestInfo(Asn1Sequence seq)
		{
			if (seq.Count > 4 || seq.Count < 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.digestedObjectType = DerEnumerated.GetInstance(seq[0]);
			int num = 0;
			if (seq.Count == 4)
			{
				this.otherObjectTypeID = DerObjectIdentifier.GetInstance(seq[1]);
				num++;
			}
			this.digestAlgorithm = AlgorithmIdentifier.GetInstance(seq[1 + num]);
			this.objectDigest = DerBitString.GetInstance(seq[2 + num]);
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x0600274D RID: 10061 RVA: 0x000ED7B3 File Offset: 0x000EC7B3
		public DerEnumerated DigestedObjectType
		{
			get
			{
				return this.digestedObjectType;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x0600274E RID: 10062 RVA: 0x000ED7BB File Offset: 0x000EC7BB
		public DerObjectIdentifier OtherObjectTypeID
		{
			get
			{
				return this.otherObjectTypeID;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x0600274F RID: 10063 RVA: 0x000ED7C3 File Offset: 0x000EC7C3
		public AlgorithmIdentifier DigestAlgorithm
		{
			get
			{
				return this.digestAlgorithm;
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06002750 RID: 10064 RVA: 0x000ED7CB File Offset: 0x000EC7CB
		public DerBitString ObjectDigest
		{
			get
			{
				return this.objectDigest;
			}
		}

		// Token: 0x06002751 RID: 10065 RVA: 0x000ED7D4 File Offset: 0x000EC7D4
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.digestedObjectType
			});
			if (this.otherObjectTypeID != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.otherObjectTypeID
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.digestAlgorithm,
				this.objectDigest
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04001B16 RID: 6934
		public const int PublicKey = 0;

		// Token: 0x04001B17 RID: 6935
		public const int PublicKeyCert = 1;

		// Token: 0x04001B18 RID: 6936
		public const int OtherObjectDigest = 2;

		// Token: 0x04001B19 RID: 6937
		internal readonly DerEnumerated digestedObjectType;

		// Token: 0x04001B1A RID: 6938
		internal readonly DerObjectIdentifier otherObjectTypeID;

		// Token: 0x04001B1B RID: 6939
		internal readonly AlgorithmIdentifier digestAlgorithm;

		// Token: 0x04001B1C RID: 6940
		internal readonly DerBitString objectDigest;
	}
}
