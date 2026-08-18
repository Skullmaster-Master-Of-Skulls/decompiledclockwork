using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x0200051A RID: 1306
	public class RevokedInfo : Asn1Encodable
	{
		// Token: 0x06002C9D RID: 11421 RVA: 0x0010F303 File Offset: 0x0010E303
		public static RevokedInfo GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return RevokedInfo.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06002C9E RID: 11422 RVA: 0x0010F314 File Offset: 0x0010E314
		public static RevokedInfo GetInstance(object obj)
		{
			if (obj == null || obj is RevokedInfo)
			{
				return (RevokedInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new RevokedInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06002C9F RID: 11423 RVA: 0x0010F366 File Offset: 0x0010E366
		public RevokedInfo(DerGeneralizedTime revocationTime) : this(revocationTime, null)
		{
		}

		// Token: 0x06002CA0 RID: 11424 RVA: 0x0010F370 File Offset: 0x0010E370
		public RevokedInfo(DerGeneralizedTime revocationTime, CrlReason revocationReason)
		{
			if (revocationTime == null)
			{
				throw new ArgumentNullException("revocationTime");
			}
			this.revocationTime = revocationTime;
			this.revocationReason = revocationReason;
		}

		// Token: 0x06002CA1 RID: 11425 RVA: 0x0010F394 File Offset: 0x0010E394
		private RevokedInfo(Asn1Sequence seq)
		{
			this.revocationTime = (DerGeneralizedTime)seq[0];
			if (seq.Count > 1)
			{
				this.revocationReason = new CrlReason(DerEnumerated.GetInstance((Asn1TaggedObject)seq[1], true));
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06002CA2 RID: 11426 RVA: 0x0010F3D4 File Offset: 0x0010E3D4
		public DerGeneralizedTime RevocationTime
		{
			get
			{
				return this.revocationTime;
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06002CA3 RID: 11427 RVA: 0x0010F3DC File Offset: 0x0010E3DC
		public CrlReason RevocationReason
		{
			get
			{
				return this.revocationReason;
			}
		}

		// Token: 0x06002CA4 RID: 11428 RVA: 0x0010F3E4 File Offset: 0x0010E3E4
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.revocationTime
			});
			if (this.revocationReason != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.revocationReason)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04001EAD RID: 7853
		private readonly DerGeneralizedTime revocationTime;

		// Token: 0x04001EAE RID: 7854
		private readonly CrlReason revocationReason;
	}
}
