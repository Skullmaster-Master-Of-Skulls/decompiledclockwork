using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x020001B2 RID: 434
	public class ResponseData : Asn1Encodable
	{
		// Token: 0x0600106E RID: 4206 RVA: 0x0005E67C File Offset: 0x0005D67C
		public static ResponseData GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return ResponseData.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x0005E68C File Offset: 0x0005D68C
		public static ResponseData GetInstance(object obj)
		{
			if (obj == null || obj is ResponseData)
			{
				return (ResponseData)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new ResponseData((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x0005E6DE File Offset: 0x0005D6DE
		public ResponseData(DerInteger version, ResponderID responderID, DerGeneralizedTime producedAt, Asn1Sequence responses, X509Extensions responseExtensions)
		{
			this.version = version;
			this.responderID = responderID;
			this.producedAt = producedAt;
			this.responses = responses;
			this.responseExtensions = responseExtensions;
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x0005E70B File Offset: 0x0005D70B
		public ResponseData(ResponderID responderID, DerGeneralizedTime producedAt, Asn1Sequence responses, X509Extensions responseExtensions) : this(ResponseData.V1, responderID, producedAt, responses, responseExtensions)
		{
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x0005E720 File Offset: 0x0005D720
		private ResponseData(Asn1Sequence seq)
		{
			int num = 0;
			Asn1Encodable asn1Encodable = seq[0];
			if (asn1Encodable is Asn1TaggedObject)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)asn1Encodable;
				if (asn1TaggedObject.TagNo == 0)
				{
					this.versionPresent = true;
					this.version = DerInteger.GetInstance(asn1TaggedObject, true);
					num++;
				}
				else
				{
					this.version = ResponseData.V1;
				}
			}
			else
			{
				this.version = ResponseData.V1;
			}
			this.responderID = ResponderID.GetInstance(seq[num++]);
			this.producedAt = (DerGeneralizedTime)seq[num++];
			this.responses = (Asn1Sequence)seq[num++];
			if (seq.Count > num)
			{
				this.responseExtensions = X509Extensions.GetInstance((Asn1TaggedObject)seq[num], true);
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06001073 RID: 4211 RVA: 0x0005E7E9 File Offset: 0x0005D7E9
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x0005E7F1 File Offset: 0x0005D7F1
		public ResponderID ResponderID
		{
			get
			{
				return this.responderID;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06001075 RID: 4213 RVA: 0x0005E7F9 File Offset: 0x0005D7F9
		public DerGeneralizedTime ProducedAt
		{
			get
			{
				return this.producedAt;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x0005E801 File Offset: 0x0005D801
		public Asn1Sequence Responses
		{
			get
			{
				return this.responses;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06001077 RID: 4215 RVA: 0x0005E809 File Offset: 0x0005D809
		public X509Extensions ResponseExtensions
		{
			get
			{
				return this.responseExtensions;
			}
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x0005E814 File Offset: 0x0005D814
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			if (this.versionPresent || !this.version.Equals(ResponseData.V1))
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.version)
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.responderID,
				this.producedAt,
				this.responses
			});
			if (this.responseExtensions != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 1, this.responseExtensions)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000C13 RID: 3091
		private static readonly DerInteger V1 = new DerInteger(0);

		// Token: 0x04000C14 RID: 3092
		private readonly bool versionPresent;

		// Token: 0x04000C15 RID: 3093
		private readonly DerInteger version;

		// Token: 0x04000C16 RID: 3094
		private readonly ResponderID responderID;

		// Token: 0x04000C17 RID: 3095
		private readonly DerGeneralizedTime producedAt;

		// Token: 0x04000C18 RID: 3096
		private readonly Asn1Sequence responses;

		// Token: 0x04000C19 RID: 3097
		private readonly X509Extensions responseExtensions;
	}
}
