using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000207 RID: 519
	public class AttCertValidityPeriod : Asn1Encodable
	{
		// Token: 0x060013F1 RID: 5105 RVA: 0x00072AB4 File Offset: 0x00071AB4
		public static AttCertValidityPeriod GetInstance(object obj)
		{
			if (obj is AttCertValidityPeriod || obj == null)
			{
				return (AttCertValidityPeriod)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new AttCertValidityPeriod((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x00072B06 File Offset: 0x00071B06
		public static AttCertValidityPeriod GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return AttCertValidityPeriod.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x00072B14 File Offset: 0x00071B14
		private AttCertValidityPeriod(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.notBeforeTime = DerGeneralizedTime.GetInstance(seq[0]);
			this.notAfterTime = DerGeneralizedTime.GetInstance(seq[1]);
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x00072B6F File Offset: 0x00071B6F
		public AttCertValidityPeriod(DerGeneralizedTime notBeforeTime, DerGeneralizedTime notAfterTime)
		{
			this.notBeforeTime = notBeforeTime;
			this.notAfterTime = notAfterTime;
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x060013F5 RID: 5109 RVA: 0x00072B85 File Offset: 0x00071B85
		public DerGeneralizedTime NotBeforeTime
		{
			get
			{
				return this.notBeforeTime;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x060013F6 RID: 5110 RVA: 0x00072B8D File Offset: 0x00071B8D
		public DerGeneralizedTime NotAfterTime
		{
			get
			{
				return this.notAfterTime;
			}
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x00072B98 File Offset: 0x00071B98
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.notBeforeTime,
				this.notAfterTime
			});
		}

		// Token: 0x04000DC9 RID: 3529
		private readonly DerGeneralizedTime notBeforeTime;

		// Token: 0x04000DCA RID: 3530
		private readonly DerGeneralizedTime notAfterTime;
	}
}
