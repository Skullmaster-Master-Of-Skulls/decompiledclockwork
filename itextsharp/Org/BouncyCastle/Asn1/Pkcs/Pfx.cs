using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020001AF RID: 431
	public class Pfx : Asn1Encodable
	{
		// Token: 0x0600105F RID: 4191 RVA: 0x0005E3C4 File Offset: 0x0005D3C4
		public Pfx(Asn1Sequence seq)
		{
			BigInteger value = ((DerInteger)seq[0]).Value;
			if (value.IntValue != 3)
			{
				throw new ArgumentException("wrong version for PFX PDU");
			}
			this.contentInfo = ContentInfo.GetInstance(seq[1]);
			if (seq.Count == 3)
			{
				this.macData = MacData.GetInstance(seq[2]);
			}
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0005E42A File Offset: 0x0005D42A
		public Pfx(ContentInfo contentInfo, MacData macData)
		{
			this.contentInfo = contentInfo;
			this.macData = macData;
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x0005E440 File Offset: 0x0005D440
		public ContentInfo AuthSafe
		{
			get
			{
				return this.contentInfo;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x0005E448 File Offset: 0x0005D448
		public MacData MacData
		{
			get
			{
				return this.macData;
			}
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0005E450 File Offset: 0x0005D450
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				new DerInteger(3),
				this.contentInfo
			});
			if (this.macData != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.macData
				});
			}
			return new BerSequence(asn1EncodableVector);
		}

		// Token: 0x04000C0D RID: 3085
		private ContentInfo contentInfo;

		// Token: 0x04000C0E RID: 3086
		private MacData macData;
	}
}
