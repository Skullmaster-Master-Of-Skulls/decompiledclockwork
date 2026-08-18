using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000205 RID: 517
	public class IssuerSerial : Asn1Encodable
	{
		// Token: 0x060013DF RID: 5087 RVA: 0x00072798 File Offset: 0x00071798
		public static IssuerSerial GetInstance(object obj)
		{
			if (obj == null || obj is IssuerSerial)
			{
				return (IssuerSerial)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new IssuerSerial((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x000727EA File Offset: 0x000717EA
		public static IssuerSerial GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return IssuerSerial.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x000727F8 File Offset: 0x000717F8
		private IssuerSerial(Asn1Sequence seq)
		{
			if (seq.Count != 2 && seq.Count != 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.issuer = GeneralNames.GetInstance(seq[0]);
			this.serial = DerInteger.GetInstance(seq[1]);
			if (seq.Count == 3)
			{
				this.issuerUid = DerBitString.GetInstance(seq[2]);
			}
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x00072877 File Offset: 0x00071877
		public IssuerSerial(GeneralNames issuer, DerInteger serial)
		{
			this.issuer = issuer;
			this.serial = serial;
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x060013E3 RID: 5091 RVA: 0x0007288D File Offset: 0x0007188D
		public GeneralNames Issuer
		{
			get
			{
				return this.issuer;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x060013E4 RID: 5092 RVA: 0x00072895 File Offset: 0x00071895
		public DerInteger Serial
		{
			get
			{
				return this.serial;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x060013E5 RID: 5093 RVA: 0x0007289D File Offset: 0x0007189D
		public DerBitString IssuerUid
		{
			get
			{
				return this.issuerUid;
			}
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x000728A8 File Offset: 0x000718A8
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.issuer,
				this.serial
			});
			if (this.issuerUid != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.issuerUid
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000DC2 RID: 3522
		internal readonly GeneralNames issuer;

		// Token: 0x04000DC3 RID: 3523
		internal readonly DerInteger serial;

		// Token: 0x04000DC4 RID: 3524
		internal readonly DerBitString issuerUid;
	}
}
