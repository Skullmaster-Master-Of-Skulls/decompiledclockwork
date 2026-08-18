using System;

namespace Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x0200003B RID: 59
	public class OcspRequest : Asn1Encodable
	{
		// Token: 0x06000183 RID: 387 RVA: 0x00009390 File Offset: 0x00008390
		public static OcspRequest GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return OcspRequest.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000093A0 File Offset: 0x000083A0
		public static OcspRequest GetInstance(object obj)
		{
			if (obj == null || obj is OcspRequest)
			{
				return (OcspRequest)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OcspRequest((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000093F2 File Offset: 0x000083F2
		public OcspRequest(TbsRequest tbsRequest, Signature optionalSignature)
		{
			if (tbsRequest == null)
			{
				throw new ArgumentNullException("tbsRequest");
			}
			this.tbsRequest = tbsRequest;
			this.optionalSignature = optionalSignature;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00009416 File Offset: 0x00008416
		private OcspRequest(Asn1Sequence seq)
		{
			this.tbsRequest = TbsRequest.GetInstance(seq[0]);
			if (seq.Count == 2)
			{
				this.optionalSignature = Signature.GetInstance((Asn1TaggedObject)seq[1], true);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00009451 File Offset: 0x00008451
		public TbsRequest TbsRequest
		{
			get
			{
				return this.tbsRequest;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00009459 File Offset: 0x00008459
		public Signature OptionalSignature
		{
			get
			{
				return this.optionalSignature;
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00009464 File Offset: 0x00008464
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.tbsRequest
			});
			if (this.optionalSignature != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.optionalSignature)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040000B7 RID: 183
		private readonly TbsRequest tbsRequest;

		// Token: 0x040000B8 RID: 184
		private readonly Signature optionalSignature;
	}
}
