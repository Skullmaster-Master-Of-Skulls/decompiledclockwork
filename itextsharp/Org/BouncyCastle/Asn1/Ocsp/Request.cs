using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x0200025C RID: 604
	public class Request : Asn1Encodable
	{
		// Token: 0x060016EA RID: 5866 RVA: 0x00084AB4 File Offset: 0x00083AB4
		public static Request GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return Request.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x00084AC4 File Offset: 0x00083AC4
		public static Request GetInstance(object obj)
		{
			if (obj == null || obj is Request)
			{
				return (Request)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new Request((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x00084B16 File Offset: 0x00083B16
		public Request(CertID reqCert, X509Extensions singleRequestExtensions)
		{
			if (reqCert == null)
			{
				throw new ArgumentNullException("reqCert");
			}
			this.reqCert = reqCert;
			this.singleRequestExtensions = singleRequestExtensions;
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x00084B3A File Offset: 0x00083B3A
		private Request(Asn1Sequence seq)
		{
			this.reqCert = CertID.GetInstance(seq[0]);
			if (seq.Count == 2)
			{
				this.singleRequestExtensions = X509Extensions.GetInstance((Asn1TaggedObject)seq[1], true);
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060016EE RID: 5870 RVA: 0x00084B75 File Offset: 0x00083B75
		public CertID ReqCert
		{
			get
			{
				return this.reqCert;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060016EF RID: 5871 RVA: 0x00084B7D File Offset: 0x00083B7D
		public X509Extensions SingleRequestExtensions
		{
			get
			{
				return this.singleRequestExtensions;
			}
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x00084B88 File Offset: 0x00083B88
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.reqCert
			});
			if (this.singleRequestExtensions != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.singleRequestExtensions)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000FB8 RID: 4024
		private readonly CertID reqCert;

		// Token: 0x04000FB9 RID: 4025
		private readonly X509Extensions singleRequestExtensions;
	}
}
