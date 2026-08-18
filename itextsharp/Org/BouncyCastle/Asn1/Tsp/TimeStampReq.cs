using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Tsp
{
	// Token: 0x020002B6 RID: 694
	public class TimeStampReq : Asn1Encodable
	{
		// Token: 0x06001A38 RID: 6712 RVA: 0x0009B3C4 File Offset: 0x0009A3C4
		public static TimeStampReq GetInstance(object o)
		{
			if (o == null || o is TimeStampReq)
			{
				return (TimeStampReq)o;
			}
			if (o is Asn1Sequence)
			{
				return new TimeStampReq((Asn1Sequence)o);
			}
			throw new ArgumentException("Unknown object in 'TimeStampReq' factory: " + o.GetType().FullName);
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x0009B414 File Offset: 0x0009A414
		private TimeStampReq(Asn1Sequence seq)
		{
			int count = seq.Count;
			int num = 0;
			this.version = DerInteger.GetInstance(seq[num++]);
			this.messageImprint = MessageImprint.GetInstance(seq[num++]);
			for (int i = num; i < count; i++)
			{
				if (seq[i] is DerObjectIdentifier)
				{
					this.tsaPolicy = DerObjectIdentifier.GetInstance(seq[i]);
				}
				else if (seq[i] is DerInteger)
				{
					this.nonce = DerInteger.GetInstance(seq[i]);
				}
				else if (seq[i] is DerBoolean)
				{
					this.certReq = DerBoolean.GetInstance(seq[i]);
				}
				else if (seq[i] is Asn1TaggedObject)
				{
					Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)seq[i];
					if (asn1TaggedObject.TagNo == 0)
					{
						this.extensions = X509Extensions.GetInstance(asn1TaggedObject, false);
					}
				}
			}
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x0009B504 File Offset: 0x0009A504
		public TimeStampReq(MessageImprint messageImprint, DerObjectIdentifier tsaPolicy, DerInteger nonce, DerBoolean certReq, X509Extensions extensions)
		{
			this.version = new DerInteger(1);
			this.messageImprint = messageImprint;
			this.tsaPolicy = tsaPolicy;
			this.nonce = nonce;
			this.certReq = certReq;
			this.extensions = extensions;
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001A3B RID: 6715 RVA: 0x0009B53D File Offset: 0x0009A53D
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001A3C RID: 6716 RVA: 0x0009B545 File Offset: 0x0009A545
		public MessageImprint MessageImprint
		{
			get
			{
				return this.messageImprint;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001A3D RID: 6717 RVA: 0x0009B54D File Offset: 0x0009A54D
		public DerObjectIdentifier ReqPolicy
		{
			get
			{
				return this.tsaPolicy;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001A3E RID: 6718 RVA: 0x0009B555 File Offset: 0x0009A555
		public DerInteger Nonce
		{
			get
			{
				return this.nonce;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06001A3F RID: 6719 RVA: 0x0009B55D File Offset: 0x0009A55D
		public DerBoolean CertReq
		{
			get
			{
				return this.certReq;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001A40 RID: 6720 RVA: 0x0009B565 File Offset: 0x0009A565
		public X509Extensions Extensions
		{
			get
			{
				return this.extensions;
			}
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x0009B570 File Offset: 0x0009A570
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.messageImprint
			});
			if (this.tsaPolicy != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.tsaPolicy
				});
			}
			if (this.nonce != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.nonce
				});
			}
			if (this.certReq != null && this.certReq.IsTrue)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.certReq
				});
			}
			if (this.extensions != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, this.extensions)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04001177 RID: 4471
		private readonly DerInteger version;

		// Token: 0x04001178 RID: 4472
		private readonly MessageImprint messageImprint;

		// Token: 0x04001179 RID: 4473
		private readonly DerObjectIdentifier tsaPolicy;

		// Token: 0x0400117A RID: 4474
		private readonly DerInteger nonce;

		// Token: 0x0400117B RID: 4475
		private readonly DerBoolean certReq;

		// Token: 0x0400117C RID: 4476
		private readonly X509Extensions extensions;
	}
}
