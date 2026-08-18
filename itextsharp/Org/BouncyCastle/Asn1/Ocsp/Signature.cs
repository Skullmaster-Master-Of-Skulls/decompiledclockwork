using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x0200030E RID: 782
	public class Signature : Asn1Encodable
	{
		// Token: 0x06001C97 RID: 7319 RVA: 0x000AB14E File Offset: 0x000AA14E
		public static Signature GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return Signature.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x000AB15C File Offset: 0x000AA15C
		public static Signature GetInstance(object obj)
		{
			if (obj == null || obj is Signature)
			{
				return (Signature)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new Signature((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x000AB1AE File Offset: 0x000AA1AE
		public Signature(AlgorithmIdentifier signatureAlgorithm, DerBitString signatureValue) : this(signatureAlgorithm, signatureValue, null)
		{
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x000AB1B9 File Offset: 0x000AA1B9
		public Signature(AlgorithmIdentifier signatureAlgorithm, DerBitString signatureValue, Asn1Sequence certs)
		{
			if (signatureAlgorithm == null)
			{
				throw new ArgumentException("signatureAlgorithm");
			}
			if (signatureValue == null)
			{
				throw new ArgumentException("signatureValue");
			}
			this.signatureAlgorithm = signatureAlgorithm;
			this.signatureValue = signatureValue;
			this.certs = certs;
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x000AB1F4 File Offset: 0x000AA1F4
		private Signature(Asn1Sequence seq)
		{
			this.signatureAlgorithm = AlgorithmIdentifier.GetInstance(seq[0]);
			this.signatureValue = (DerBitString)seq[1];
			if (seq.Count == 3)
			{
				this.certs = Asn1Sequence.GetInstance((Asn1TaggedObject)seq[2], true);
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001C9C RID: 7324 RVA: 0x000AB24C File Offset: 0x000AA24C
		public AlgorithmIdentifier SignatureAlgorithm
		{
			get
			{
				return this.signatureAlgorithm;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001C9D RID: 7325 RVA: 0x000AB254 File Offset: 0x000AA254
		public DerBitString SignatureValue
		{
			get
			{
				return this.signatureValue;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001C9E RID: 7326 RVA: 0x000AB25C File Offset: 0x000AA25C
		public Asn1Sequence Certs
		{
			get
			{
				return this.certs;
			}
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x000AB264 File Offset: 0x000AA264
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.signatureAlgorithm,
				this.signatureValue
			});
			if (this.certs != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.certs)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040013AF RID: 5039
		internal AlgorithmIdentifier signatureAlgorithm;

		// Token: 0x040013B0 RID: 5040
		internal DerBitString signatureValue;

		// Token: 0x040013B1 RID: 5041
		internal Asn1Sequence certs;
	}
}
