using System;

namespace Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x0200025D RID: 605
	public class OcspResponse : Asn1Encodable
	{
		// Token: 0x060016F1 RID: 5873 RVA: 0x00084BD8 File Offset: 0x00083BD8
		public static OcspResponse GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return OcspResponse.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x00084BE8 File Offset: 0x00083BE8
		public static OcspResponse GetInstance(object obj)
		{
			if (obj == null || obj is OcspResponse)
			{
				return (OcspResponse)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OcspResponse((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x00084C3A File Offset: 0x00083C3A
		public OcspResponse(OcspResponseStatus responseStatus, ResponseBytes responseBytes)
		{
			if (responseStatus == null)
			{
				throw new ArgumentNullException("responseStatus");
			}
			this.responseStatus = responseStatus;
			this.responseBytes = responseBytes;
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x00084C5E File Offset: 0x00083C5E
		private OcspResponse(Asn1Sequence seq)
		{
			this.responseStatus = new OcspResponseStatus(DerEnumerated.GetInstance(seq[0]));
			if (seq.Count == 2)
			{
				this.responseBytes = ResponseBytes.GetInstance((Asn1TaggedObject)seq[1], true);
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x060016F5 RID: 5877 RVA: 0x00084C9E File Offset: 0x00083C9E
		public OcspResponseStatus ResponseStatus
		{
			get
			{
				return this.responseStatus;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x060016F6 RID: 5878 RVA: 0x00084CA6 File Offset: 0x00083CA6
		public ResponseBytes ResponseBytes
		{
			get
			{
				return this.responseBytes;
			}
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x00084CB0 File Offset: 0x00083CB0
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.responseStatus
			});
			if (this.responseBytes != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.responseBytes)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000FBA RID: 4026
		private readonly OcspResponseStatus responseStatus;

		// Token: 0x04000FBB RID: 4027
		private readonly ResponseBytes responseBytes;
	}
}
