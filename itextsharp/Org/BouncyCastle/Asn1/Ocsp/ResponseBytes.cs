using System;

namespace Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x020001B3 RID: 435
	public class ResponseBytes : Asn1Encodable
	{
		// Token: 0x0600107A RID: 4218 RVA: 0x0005E8C7 File Offset: 0x0005D8C7
		public static ResponseBytes GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return ResponseBytes.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x0005E8D8 File Offset: 0x0005D8D8
		public static ResponseBytes GetInstance(object obj)
		{
			if (obj == null || obj is ResponseBytes)
			{
				return (ResponseBytes)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new ResponseBytes((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x0005E92A File Offset: 0x0005D92A
		public ResponseBytes(DerObjectIdentifier responseType, Asn1OctetString response)
		{
			if (responseType == null)
			{
				throw new ArgumentNullException("responseType");
			}
			if (response == null)
			{
				throw new ArgumentNullException("response");
			}
			this.responseType = responseType;
			this.response = response;
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0005E95C File Offset: 0x0005D95C
		private ResponseBytes(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.responseType = DerObjectIdentifier.GetInstance(seq[0]);
			this.response = Asn1OctetString.GetInstance(seq[1]);
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x0005E9AC File Offset: 0x0005D9AC
		public DerObjectIdentifier ResponseType
		{
			get
			{
				return this.responseType;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x0600107F RID: 4223 RVA: 0x0005E9B4 File Offset: 0x0005D9B4
		public Asn1OctetString Response
		{
			get
			{
				return this.response;
			}
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x0005E9BC File Offset: 0x0005D9BC
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.responseType,
				this.response
			});
		}

		// Token: 0x04000C1A RID: 3098
		private readonly DerObjectIdentifier responseType;

		// Token: 0x04000C1B RID: 3099
		private readonly Asn1OctetString response;
	}
}
