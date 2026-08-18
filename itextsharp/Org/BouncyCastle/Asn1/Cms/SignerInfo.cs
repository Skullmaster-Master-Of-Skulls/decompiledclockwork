using System;
using System.Collections;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x0200062E RID: 1582
	public class SignerInfo : Asn1Encodable
	{
		// Token: 0x06003593 RID: 13715 RVA: 0x0014BF60 File Offset: 0x0014AF60
		public static SignerInfo GetInstance(object obj)
		{
			if (obj == null || obj is SignerInfo)
			{
				return (SignerInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new SignerInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + obj.GetType().FullName, "obj");
		}

		// Token: 0x06003594 RID: 13716 RVA: 0x0014BFB4 File Offset: 0x0014AFB4
		public SignerInfo(SignerIdentifier sid, AlgorithmIdentifier digAlgorithm, Asn1Set authenticatedAttributes, AlgorithmIdentifier digEncryptionAlgorithm, Asn1OctetString encryptedDigest, Asn1Set unauthenticatedAttributes)
		{
			if (sid.IsTagged)
			{
				this.version = new DerInteger(3);
			}
			else
			{
				this.version = new DerInteger(1);
			}
			this.sid = sid;
			this.digAlgorithm = digAlgorithm;
			this.authenticatedAttributes = authenticatedAttributes;
			this.digEncryptionAlgorithm = digEncryptionAlgorithm;
			this.encryptedDigest = encryptedDigest;
			this.unauthenticatedAttributes = unauthenticatedAttributes;
		}

		// Token: 0x06003595 RID: 13717 RVA: 0x0014C018 File Offset: 0x0014B018
		public SignerInfo(Asn1Sequence seq)
		{
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			this.version = (DerInteger)enumerator.Current;
			enumerator.MoveNext();
			this.sid = SignerIdentifier.GetInstance(enumerator.Current);
			enumerator.MoveNext();
			this.digAlgorithm = AlgorithmIdentifier.GetInstance(enumerator.Current);
			enumerator.MoveNext();
			object obj = enumerator.Current;
			if (obj is Asn1TaggedObject)
			{
				this.authenticatedAttributes = Asn1Set.GetInstance((Asn1TaggedObject)obj, false);
				enumerator.MoveNext();
				this.digEncryptionAlgorithm = AlgorithmIdentifier.GetInstance(enumerator.Current);
			}
			else
			{
				this.authenticatedAttributes = null;
				this.digEncryptionAlgorithm = AlgorithmIdentifier.GetInstance(obj);
			}
			enumerator.MoveNext();
			this.encryptedDigest = Asn1OctetString.GetInstance(enumerator.Current);
			if (enumerator.MoveNext())
			{
				this.unauthenticatedAttributes = Asn1Set.GetInstance((Asn1TaggedObject)enumerator.Current, false);
				return;
			}
			this.unauthenticatedAttributes = null;
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06003596 RID: 13718 RVA: 0x0014C10E File Offset: 0x0014B10E
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06003597 RID: 13719 RVA: 0x0014C116 File Offset: 0x0014B116
		public SignerIdentifier SignerID
		{
			get
			{
				return this.sid;
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06003598 RID: 13720 RVA: 0x0014C11E File Offset: 0x0014B11E
		public Asn1Set AuthenticatedAttributes
		{
			get
			{
				return this.authenticatedAttributes;
			}
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06003599 RID: 13721 RVA: 0x0014C126 File Offset: 0x0014B126
		public AlgorithmIdentifier DigestAlgorithm
		{
			get
			{
				return this.digAlgorithm;
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x0600359A RID: 13722 RVA: 0x0014C12E File Offset: 0x0014B12E
		public Asn1OctetString EncryptedDigest
		{
			get
			{
				return this.encryptedDigest;
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x0600359B RID: 13723 RVA: 0x0014C136 File Offset: 0x0014B136
		public AlgorithmIdentifier DigestEncryptionAlgorithm
		{
			get
			{
				return this.digEncryptionAlgorithm;
			}
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x0600359C RID: 13724 RVA: 0x0014C13E File Offset: 0x0014B13E
		public Asn1Set UnauthenticatedAttributes
		{
			get
			{
				return this.unauthenticatedAttributes;
			}
		}

		// Token: 0x0600359D RID: 13725 RVA: 0x0014C148 File Offset: 0x0014B148
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.sid,
				this.digAlgorithm
			});
			if (this.authenticatedAttributes != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, this.authenticatedAttributes)
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.digEncryptionAlgorithm,
				this.encryptedDigest
			});
			if (this.unauthenticatedAttributes != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, this.unauthenticatedAttributes)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040023D9 RID: 9177
		private DerInteger version;

		// Token: 0x040023DA RID: 9178
		private SignerIdentifier sid;

		// Token: 0x040023DB RID: 9179
		private AlgorithmIdentifier digAlgorithm;

		// Token: 0x040023DC RID: 9180
		private Asn1Set authenticatedAttributes;

		// Token: 0x040023DD RID: 9181
		private AlgorithmIdentifier digEncryptionAlgorithm;

		// Token: 0x040023DE RID: 9182
		private Asn1OctetString encryptedDigest;

		// Token: 0x040023DF RID: 9183
		private Asn1Set unauthenticatedAttributes;
	}
}
