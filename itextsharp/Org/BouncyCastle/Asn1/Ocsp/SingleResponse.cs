using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x0200044D RID: 1101
	public class SingleResponse : Asn1Encodable
	{
		// Token: 0x06002537 RID: 9527 RVA: 0x000E1B8A File Offset: 0x000E0B8A
		public SingleResponse(CertID certID, CertStatus certStatus, DerGeneralizedTime thisUpdate, DerGeneralizedTime nextUpdate, X509Extensions singleExtensions)
		{
			this.certID = certID;
			this.certStatus = certStatus;
			this.thisUpdate = thisUpdate;
			this.nextUpdate = nextUpdate;
			this.singleExtensions = singleExtensions;
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x000E1BB8 File Offset: 0x000E0BB8
		public SingleResponse(Asn1Sequence seq)
		{
			this.certID = CertID.GetInstance(seq[0]);
			this.certStatus = CertStatus.GetInstance(seq[1]);
			this.thisUpdate = (DerGeneralizedTime)seq[2];
			if (seq.Count > 4)
			{
				this.nextUpdate = DerGeneralizedTime.GetInstance((Asn1TaggedObject)seq[3], true);
				this.singleExtensions = X509Extensions.GetInstance((Asn1TaggedObject)seq[4], true);
				return;
			}
			if (seq.Count > 3)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)seq[3];
				if (asn1TaggedObject.TagNo == 0)
				{
					this.nextUpdate = DerGeneralizedTime.GetInstance(asn1TaggedObject, true);
					return;
				}
				this.singleExtensions = X509Extensions.GetInstance(asn1TaggedObject, true);
			}
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x000E1C74 File Offset: 0x000E0C74
		public static SingleResponse GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return SingleResponse.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x000E1C84 File Offset: 0x000E0C84
		public static SingleResponse GetInstance(object obj)
		{
			if (obj == null || obj is SingleResponse)
			{
				return (SingleResponse)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new SingleResponse((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x0600253B RID: 9531 RVA: 0x000E1CD6 File Offset: 0x000E0CD6
		public CertID CertId
		{
			get
			{
				return this.certID;
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x0600253C RID: 9532 RVA: 0x000E1CDE File Offset: 0x000E0CDE
		public CertStatus CertStatus
		{
			get
			{
				return this.certStatus;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x0600253D RID: 9533 RVA: 0x000E1CE6 File Offset: 0x000E0CE6
		public DerGeneralizedTime ThisUpdate
		{
			get
			{
				return this.thisUpdate;
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x0600253E RID: 9534 RVA: 0x000E1CEE File Offset: 0x000E0CEE
		public DerGeneralizedTime NextUpdate
		{
			get
			{
				return this.nextUpdate;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x0600253F RID: 9535 RVA: 0x000E1CF6 File Offset: 0x000E0CF6
		public X509Extensions SingleExtensions
		{
			get
			{
				return this.singleExtensions;
			}
		}

		// Token: 0x06002540 RID: 9536 RVA: 0x000E1D00 File Offset: 0x000E0D00
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.certID,
				this.certStatus,
				this.thisUpdate
			});
			if (this.nextUpdate != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.nextUpdate)
				});
			}
			if (this.singleExtensions != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 1, this.singleExtensions)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04001A18 RID: 6680
		private readonly CertID certID;

		// Token: 0x04001A19 RID: 6681
		private readonly CertStatus certStatus;

		// Token: 0x04001A1A RID: 6682
		private readonly DerGeneralizedTime thisUpdate;

		// Token: 0x04001A1B RID: 6683
		private readonly DerGeneralizedTime nextUpdate;

		// Token: 0x04001A1C RID: 6684
		private readonly X509Extensions singleExtensions;
	}
}
