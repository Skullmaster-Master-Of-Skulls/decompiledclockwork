using System;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Asn1.Cms;

namespace Org.BouncyCastle.Asn1.Tsp
{
	// Token: 0x02000447 RID: 1095
	public class TimeStampResp : Asn1Encodable
	{
		// Token: 0x0600250E RID: 9486 RVA: 0x000E10D8 File Offset: 0x000E00D8
		public static TimeStampResp GetInstance(object o)
		{
			if (o == null || o is TimeStampResp)
			{
				return (TimeStampResp)o;
			}
			if (o is Asn1Sequence)
			{
				return new TimeStampResp((Asn1Sequence)o);
			}
			throw new ArgumentException("Unknown object in 'TimeStampResp' factory: " + o.GetType().FullName);
		}

		// Token: 0x0600250F RID: 9487 RVA: 0x000E1125 File Offset: 0x000E0125
		private TimeStampResp(Asn1Sequence seq)
		{
			this.pkiStatusInfo = PkiStatusInfo.GetInstance(seq[0]);
			if (seq.Count > 1)
			{
				this.timeStampToken = ContentInfo.GetInstance(seq[1]);
			}
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x000E115A File Offset: 0x000E015A
		public TimeStampResp(PkiStatusInfo pkiStatusInfo, ContentInfo timeStampToken)
		{
			this.pkiStatusInfo = pkiStatusInfo;
			this.timeStampToken = timeStampToken;
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06002511 RID: 9489 RVA: 0x000E1170 File Offset: 0x000E0170
		public PkiStatusInfo Status
		{
			get
			{
				return this.pkiStatusInfo;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06002512 RID: 9490 RVA: 0x000E1178 File Offset: 0x000E0178
		public ContentInfo TimeStampToken
		{
			get
			{
				return this.timeStampToken;
			}
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x000E1180 File Offset: 0x000E0180
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.pkiStatusInfo
			});
			if (this.timeStampToken != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.timeStampToken
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040019DF RID: 6623
		private readonly PkiStatusInfo pkiStatusInfo;

		// Token: 0x040019E0 RID: 6624
		private readonly ContentInfo timeStampToken;
	}
}
