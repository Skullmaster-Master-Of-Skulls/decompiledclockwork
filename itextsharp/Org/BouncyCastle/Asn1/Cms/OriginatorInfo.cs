using System;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x0200062F RID: 1583
	public class OriginatorInfo : Asn1Encodable
	{
		// Token: 0x0600359E RID: 13726 RVA: 0x0014C1F3 File Offset: 0x0014B1F3
		public OriginatorInfo(Asn1Set certs, Asn1Set crls)
		{
			this.certs = certs;
			this.crls = crls;
		}

		// Token: 0x0600359F RID: 13727 RVA: 0x0014C20C File Offset: 0x0014B20C
		public OriginatorInfo(Asn1Sequence seq)
		{
			switch (seq.Count)
			{
			case 0:
				return;
			case 1:
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)seq[0];
				switch (asn1TaggedObject.TagNo)
				{
				case 0:
					this.certs = Asn1Set.GetInstance(asn1TaggedObject, false);
					return;
				case 1:
					this.crls = Asn1Set.GetInstance(asn1TaggedObject, false);
					return;
				default:
					throw new ArgumentException("Bad tag in OriginatorInfo: " + asn1TaggedObject.TagNo);
				}
				break;
			}
			case 2:
				this.certs = Asn1Set.GetInstance((Asn1TaggedObject)seq[0], false);
				this.crls = Asn1Set.GetInstance((Asn1TaggedObject)seq[1], false);
				return;
			default:
				throw new ArgumentException("OriginatorInfo too big");
			}
		}

		// Token: 0x060035A0 RID: 13728 RVA: 0x0014C2D4 File Offset: 0x0014B2D4
		public static OriginatorInfo GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return OriginatorInfo.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x060035A1 RID: 13729 RVA: 0x0014C2E4 File Offset: 0x0014B2E4
		public static OriginatorInfo GetInstance(object obj)
		{
			if (obj == null || obj is OriginatorInfo)
			{
				return (OriginatorInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OriginatorInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid OriginatorInfo: " + obj.GetType().Name);
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x060035A2 RID: 13730 RVA: 0x0014C331 File Offset: 0x0014B331
		public Asn1Set Certificates
		{
			get
			{
				return this.certs;
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x060035A3 RID: 13731 RVA: 0x0014C339 File Offset: 0x0014B339
		public Asn1Set Crls
		{
			get
			{
				return this.crls;
			}
		}

		// Token: 0x060035A4 RID: 13732 RVA: 0x0014C344 File Offset: 0x0014B344
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			if (this.certs != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, this.certs)
				});
			}
			if (this.crls != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, this.crls)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040023E0 RID: 9184
		private Asn1Set certs;

		// Token: 0x040023E1 RID: 9185
		private Asn1Set crls;
	}
}
