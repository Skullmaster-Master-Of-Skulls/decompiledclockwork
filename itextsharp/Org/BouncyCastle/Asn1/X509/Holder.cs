using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000514 RID: 1300
	public class Holder : Asn1Encodable
	{
		// Token: 0x06002C71 RID: 11377 RVA: 0x0010EB60 File Offset: 0x0010DB60
		public static Holder GetInstance(object obj)
		{
			if (obj is Holder)
			{
				return (Holder)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new Holder((Asn1Sequence)obj);
			}
			if (obj is Asn1TaggedObject)
			{
				return new Holder((Asn1TaggedObject)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06002C72 RID: 11378 RVA: 0x0010EBC4 File Offset: 0x0010DBC4
		public Holder(Asn1TaggedObject tagObj)
		{
			switch (tagObj.TagNo)
			{
			case 0:
				this.baseCertificateID = IssuerSerial.GetInstance(tagObj, false);
				break;
			case 1:
				this.entityName = GeneralNames.GetInstance(tagObj, false);
				break;
			default:
				throw new ArgumentException("unknown tag in Holder");
			}
			this.version = 0;
		}

		// Token: 0x06002C73 RID: 11379 RVA: 0x0010EC20 File Offset: 0x0010DC20
		private Holder(Asn1Sequence seq)
		{
			if (seq.Count > 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			for (int num = 0; num != seq.Count; num++)
			{
				Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(seq[num]);
				switch (instance.TagNo)
				{
				case 0:
					this.baseCertificateID = IssuerSerial.GetInstance(instance, false);
					break;
				case 1:
					this.entityName = GeneralNames.GetInstance(instance, false);
					break;
				case 2:
					this.objectDigestInfo = ObjectDigestInfo.GetInstance(instance, false);
					break;
				default:
					throw new ArgumentException("unknown tag in Holder");
				}
			}
			this.version = 1;
		}

		// Token: 0x06002C74 RID: 11380 RVA: 0x0010ECCF File Offset: 0x0010DCCF
		public Holder(IssuerSerial baseCertificateID) : this(baseCertificateID, 1)
		{
		}

		// Token: 0x06002C75 RID: 11381 RVA: 0x0010ECD9 File Offset: 0x0010DCD9
		public Holder(IssuerSerial baseCertificateID, int version)
		{
			this.baseCertificateID = baseCertificateID;
			this.version = version;
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06002C76 RID: 11382 RVA: 0x0010ECEF File Offset: 0x0010DCEF
		public int Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x06002C77 RID: 11383 RVA: 0x0010ECF7 File Offset: 0x0010DCF7
		public Holder(GeneralNames entityName) : this(entityName, 1)
		{
		}

		// Token: 0x06002C78 RID: 11384 RVA: 0x0010ED01 File Offset: 0x0010DD01
		public Holder(GeneralNames entityName, int version)
		{
			this.entityName = entityName;
			this.version = version;
		}

		// Token: 0x06002C79 RID: 11385 RVA: 0x0010ED17 File Offset: 0x0010DD17
		public Holder(ObjectDigestInfo objectDigestInfo)
		{
			this.objectDigestInfo = objectDigestInfo;
			this.version = 1;
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06002C7A RID: 11386 RVA: 0x0010ED2D File Offset: 0x0010DD2D
		public IssuerSerial BaseCertificateID
		{
			get
			{
				return this.baseCertificateID;
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06002C7B RID: 11387 RVA: 0x0010ED35 File Offset: 0x0010DD35
		public GeneralNames EntityName
		{
			get
			{
				return this.entityName;
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06002C7C RID: 11388 RVA: 0x0010ED3D File Offset: 0x0010DD3D
		public ObjectDigestInfo ObjectDigestInfo
		{
			get
			{
				return this.objectDigestInfo;
			}
		}

		// Token: 0x06002C7D RID: 11389 RVA: 0x0010ED48 File Offset: 0x0010DD48
		public override Asn1Object ToAsn1Object()
		{
			if (this.version == 1)
			{
				Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
				if (this.baseCertificateID != null)
				{
					asn1EncodableVector.Add(new Asn1Encodable[]
					{
						new DerTaggedObject(false, 0, this.baseCertificateID)
					});
				}
				if (this.entityName != null)
				{
					asn1EncodableVector.Add(new Asn1Encodable[]
					{
						new DerTaggedObject(false, 1, this.entityName)
					});
				}
				if (this.objectDigestInfo != null)
				{
					asn1EncodableVector.Add(new Asn1Encodable[]
					{
						new DerTaggedObject(false, 2, this.objectDigestInfo)
					});
				}
				return new DerSequence(asn1EncodableVector);
			}
			if (this.entityName != null)
			{
				return new DerTaggedObject(false, 1, this.entityName);
			}
			return new DerTaggedObject(false, 0, this.baseCertificateID);
		}

		// Token: 0x04001E9F RID: 7839
		internal readonly IssuerSerial baseCertificateID;

		// Token: 0x04001EA0 RID: 7840
		internal readonly GeneralNames entityName;

		// Token: 0x04001EA1 RID: 7841
		internal readonly ObjectDigestInfo objectDigestInfo;

		// Token: 0x04001EA2 RID: 7842
		private readonly int version;
	}
}
