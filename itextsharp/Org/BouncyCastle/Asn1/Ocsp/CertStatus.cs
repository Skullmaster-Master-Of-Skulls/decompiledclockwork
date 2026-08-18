using System;

namespace Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x02000579 RID: 1401
	public class CertStatus : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06002FCC RID: 12236 RVA: 0x00127678 File Offset: 0x00126678
		public CertStatus()
		{
			this.tagNo = 0;
			this.value = DerNull.Instance;
		}

		// Token: 0x06002FCD RID: 12237 RVA: 0x00127692 File Offset: 0x00126692
		public CertStatus(RevokedInfo info)
		{
			this.tagNo = 1;
			this.value = info;
		}

		// Token: 0x06002FCE RID: 12238 RVA: 0x001276A8 File Offset: 0x001266A8
		public CertStatus(int tagNo, Asn1Encodable value)
		{
			this.tagNo = tagNo;
			this.value = value;
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x001276C0 File Offset: 0x001266C0
		public CertStatus(Asn1TaggedObject choice)
		{
			this.tagNo = choice.TagNo;
			switch (choice.TagNo)
			{
			case 0:
			case 2:
				this.value = DerNull.Instance;
				return;
			case 1:
				this.value = RevokedInfo.GetInstance(choice, false);
				return;
			default:
				return;
			}
		}

		// Token: 0x06002FD0 RID: 12240 RVA: 0x00127714 File Offset: 0x00126714
		public static CertStatus GetInstance(object obj)
		{
			if (obj == null || obj is CertStatus)
			{
				return (CertStatus)obj;
			}
			if (obj is Asn1TaggedObject)
			{
				return new CertStatus((Asn1TaggedObject)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06002FD1 RID: 12241 RVA: 0x00127766 File Offset: 0x00126766
		public int TagNo
		{
			get
			{
				return this.tagNo;
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06002FD2 RID: 12242 RVA: 0x0012776E File Offset: 0x0012676E
		public Asn1Encodable Status
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06002FD3 RID: 12243 RVA: 0x00127776 File Offset: 0x00126776
		public override Asn1Object ToAsn1Object()
		{
			return new DerTaggedObject(false, this.tagNo, this.value);
		}

		// Token: 0x040020D8 RID: 8408
		private readonly int tagNo;

		// Token: 0x040020D9 RID: 8409
		private readonly Asn1Encodable value;
	}
}
