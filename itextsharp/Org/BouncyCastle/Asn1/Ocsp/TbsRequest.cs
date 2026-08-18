using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x020000A3 RID: 163
	public class TbsRequest : Asn1Encodable
	{
		// Token: 0x06000524 RID: 1316 RVA: 0x0001B80F File Offset: 0x0001A80F
		public static TbsRequest GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return TbsRequest.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0001B820 File Offset: 0x0001A820
		public static TbsRequest GetInstance(object obj)
		{
			if (obj == null || obj is TbsRequest)
			{
				return (TbsRequest)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new TbsRequest((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0001B872 File Offset: 0x0001A872
		public TbsRequest(GeneralName requestorName, Asn1Sequence requestList, X509Extensions requestExtensions)
		{
			this.version = TbsRequest.V1;
			this.requestorName = requestorName;
			this.requestList = requestList;
			this.requestExtensions = requestExtensions;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0001B89C File Offset: 0x0001A89C
		private TbsRequest(Asn1Sequence seq)
		{
			int num = 0;
			Asn1Encodable asn1Encodable = seq[0];
			if (asn1Encodable is Asn1TaggedObject)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)asn1Encodable;
				if (asn1TaggedObject.TagNo == 0)
				{
					this.versionSet = true;
					this.version = DerInteger.GetInstance(asn1TaggedObject, true);
					num++;
				}
				else
				{
					this.version = TbsRequest.V1;
				}
			}
			else
			{
				this.version = TbsRequest.V1;
			}
			if (seq[num] is Asn1TaggedObject)
			{
				this.requestorName = GeneralName.GetInstance((Asn1TaggedObject)seq[num++], true);
			}
			this.requestList = (Asn1Sequence)seq[num++];
			if (seq.Count == num + 1)
			{
				this.requestExtensions = X509Extensions.GetInstance((Asn1TaggedObject)seq[num], true);
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x0001B965 File Offset: 0x0001A965
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x0001B96D File Offset: 0x0001A96D
		public GeneralName RequestorName
		{
			get
			{
				return this.requestorName;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x0001B975 File Offset: 0x0001A975
		public Asn1Sequence RequestList
		{
			get
			{
				return this.requestList;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x0001B97D File Offset: 0x0001A97D
		public X509Extensions RequestExtensions
		{
			get
			{
				return this.requestExtensions;
			}
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0001B988 File Offset: 0x0001A988
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			if (!this.version.Equals(TbsRequest.V1) || this.versionSet)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.version)
				});
			}
			if (this.requestorName != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 1, this.requestorName)
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.requestList
			});
			if (this.requestExtensions != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 2, this.requestExtensions)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000294 RID: 660
		private static readonly DerInteger V1 = new DerInteger(0);

		// Token: 0x04000295 RID: 661
		private readonly DerInteger version;

		// Token: 0x04000296 RID: 662
		private readonly GeneralName requestorName;

		// Token: 0x04000297 RID: 663
		private readonly Asn1Sequence requestList;

		// Token: 0x04000298 RID: 664
		private readonly X509Extensions requestExtensions;

		// Token: 0x04000299 RID: 665
		private bool versionSet;
	}
}
