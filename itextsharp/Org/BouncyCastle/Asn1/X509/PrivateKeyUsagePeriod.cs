using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200009E RID: 158
	public class PrivateKeyUsagePeriod : Asn1Encodable
	{
		// Token: 0x06000502 RID: 1282 RVA: 0x0001B110 File Offset: 0x0001A110
		public static PrivateKeyUsagePeriod GetInstance(object obj)
		{
			if (obj is PrivateKeyUsagePeriod)
			{
				return (PrivateKeyUsagePeriod)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PrivateKeyUsagePeriod((Asn1Sequence)obj);
			}
			if (obj is X509Extension)
			{
				return PrivateKeyUsagePeriod.GetInstance(X509Extension.ConvertValueToObject((X509Extension)obj));
			}
			throw new ArgumentException("unknown object in GetInstance: " + obj.GetType().FullName, "obj");
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0001B178 File Offset: 0x0001A178
		private PrivateKeyUsagePeriod(Asn1Sequence seq)
		{
			foreach (object obj in seq)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
				if (asn1TaggedObject.TagNo == 0)
				{
					this._notBefore = DerGeneralizedTime.GetInstance(asn1TaggedObject, false);
				}
				else if (asn1TaggedObject.TagNo == 1)
				{
					this._notAfter = DerGeneralizedTime.GetInstance(asn1TaggedObject, false);
				}
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x0001B1F8 File Offset: 0x0001A1F8
		public DerGeneralizedTime NotBefore
		{
			get
			{
				return this._notBefore;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x0001B200 File Offset: 0x0001A200
		public DerGeneralizedTime NotAfter
		{
			get
			{
				return this._notAfter;
			}
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0001B208 File Offset: 0x0001A208
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			if (this._notBefore != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, this._notBefore)
				});
			}
			if (this._notAfter != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, this._notAfter)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000289 RID: 649
		private DerGeneralizedTime _notBefore;

		// Token: 0x0400028A RID: 650
		private DerGeneralizedTime _notAfter;
	}
}
