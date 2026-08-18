using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020001FF RID: 511
	public class V2Form : Asn1Encodable
	{
		// Token: 0x060013BC RID: 5052 RVA: 0x00071FD7 File Offset: 0x00070FD7
		public static V2Form GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return V2Form.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x00071FE8 File Offset: 0x00070FE8
		public static V2Form GetInstance(object obj)
		{
			if (obj is V2Form)
			{
				return (V2Form)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new V2Form((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x00072037 File Offset: 0x00071037
		public V2Form(GeneralNames issuerName)
		{
			this.issuerName = issuerName;
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x00072048 File Offset: 0x00071048
		private V2Form(Asn1Sequence seq)
		{
			if (seq.Count > 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			int num = 0;
			if (!(seq[0] is Asn1TaggedObject))
			{
				num++;
				this.issuerName = GeneralNames.GetInstance(seq[0]);
			}
			for (int num2 = num; num2 != seq.Count; num2++)
			{
				Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(seq[num2]);
				if (instance.TagNo == 0)
				{
					this.baseCertificateID = IssuerSerial.GetInstance(instance, false);
				}
				else
				{
					if (instance.TagNo != 1)
					{
						throw new ArgumentException("Bad tag number: " + instance.TagNo);
					}
					this.objectDigestInfo = ObjectDigestInfo.GetInstance(instance, false);
				}
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x060013C0 RID: 5056 RVA: 0x0007210D File Offset: 0x0007110D
		public GeneralNames IssuerName
		{
			get
			{
				return this.issuerName;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x060013C1 RID: 5057 RVA: 0x00072115 File Offset: 0x00071115
		public IssuerSerial BaseCertificateID
		{
			get
			{
				return this.baseCertificateID;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x060013C2 RID: 5058 RVA: 0x0007211D File Offset: 0x0007111D
		public ObjectDigestInfo ObjectDigestInfo
		{
			get
			{
				return this.objectDigestInfo;
			}
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x00072128 File Offset: 0x00071128
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			if (this.issuerName != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.issuerName
				});
			}
			if (this.baseCertificateID != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, this.baseCertificateID)
				});
			}
			if (this.objectDigestInfo != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, this.objectDigestInfo)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000DB5 RID: 3509
		internal GeneralNames issuerName;

		// Token: 0x04000DB6 RID: 3510
		internal IssuerSerial baseCertificateID;

		// Token: 0x04000DB7 RID: 3511
		internal ObjectDigestInfo objectDigestInfo;
	}
}
