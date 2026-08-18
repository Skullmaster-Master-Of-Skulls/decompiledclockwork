using System;

namespace Org.BouncyCastle.Asn1.Cms.Ecc
{
	// Token: 0x02000412 RID: 1042
	public class MQVuserKeyingMaterial : Asn1Encodable
	{
		// Token: 0x06002377 RID: 9079 RVA: 0x000D9959 File Offset: 0x000D8959
		public MQVuserKeyingMaterial(OriginatorPublicKey ephemeralPublicKey, Asn1OctetString addedukm)
		{
			this.ephemeralPublicKey = ephemeralPublicKey;
			this.addedukm = addedukm;
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x000D996F File Offset: 0x000D896F
		private MQVuserKeyingMaterial(Asn1Sequence seq)
		{
			this.ephemeralPublicKey = OriginatorPublicKey.GetInstance(seq[0]);
			if (seq.Count > 1)
			{
				this.addedukm = Asn1OctetString.GetInstance((Asn1TaggedObject)seq[1], true);
			}
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x000D99AA File Offset: 0x000D89AA
		public static MQVuserKeyingMaterial GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return MQVuserKeyingMaterial.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x0600237A RID: 9082 RVA: 0x000D99B8 File Offset: 0x000D89B8
		public static MQVuserKeyingMaterial GetInstance(object obj)
		{
			if (obj == null || obj is MQVuserKeyingMaterial)
			{
				return (MQVuserKeyingMaterial)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new MQVuserKeyingMaterial((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid MQVuserKeyingMaterial: " + obj.GetType().Name);
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x0600237B RID: 9083 RVA: 0x000D9A05 File Offset: 0x000D8A05
		public OriginatorPublicKey EphemeralPublicKey
		{
			get
			{
				return this.ephemeralPublicKey;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x0600237C RID: 9084 RVA: 0x000D9A0D File Offset: 0x000D8A0D
		public Asn1OctetString AddedUkm
		{
			get
			{
				return this.addedukm;
			}
		}

		// Token: 0x0600237D RID: 9085 RVA: 0x000D9A18 File Offset: 0x000D8A18
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.ephemeralPublicKey
			});
			if (this.addedukm != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.addedukm)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04001881 RID: 6273
		private OriginatorPublicKey ephemeralPublicKey;

		// Token: 0x04001882 RID: 6274
		private Asn1OctetString addedukm;
	}
}
