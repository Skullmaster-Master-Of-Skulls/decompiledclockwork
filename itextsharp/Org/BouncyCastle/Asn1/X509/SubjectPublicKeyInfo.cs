using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020005B6 RID: 1462
	public class SubjectPublicKeyInfo : Asn1Encodable
	{
		// Token: 0x06003253 RID: 12883 RVA: 0x0013897F File Offset: 0x0013797F
		public static SubjectPublicKeyInfo GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return SubjectPublicKeyInfo.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06003254 RID: 12884 RVA: 0x00138990 File Offset: 0x00137990
		public static SubjectPublicKeyInfo GetInstance(object obj)
		{
			if (obj is SubjectPublicKeyInfo)
			{
				return (SubjectPublicKeyInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new SubjectPublicKeyInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06003255 RID: 12885 RVA: 0x001389DF File Offset: 0x001379DF
		public SubjectPublicKeyInfo(AlgorithmIdentifier algID, Asn1Encodable publicKey)
		{
			this.keyData = new DerBitString(publicKey);
			this.algID = algID;
		}

		// Token: 0x06003256 RID: 12886 RVA: 0x001389FA File Offset: 0x001379FA
		public SubjectPublicKeyInfo(AlgorithmIdentifier algID, byte[] publicKey)
		{
			this.keyData = new DerBitString(publicKey);
			this.algID = algID;
		}

		// Token: 0x06003257 RID: 12887 RVA: 0x00138A18 File Offset: 0x00137A18
		private SubjectPublicKeyInfo(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.algID = AlgorithmIdentifier.GetInstance(seq[0]);
			this.keyData = DerBitString.GetInstance(seq[1]);
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06003258 RID: 12888 RVA: 0x00138A78 File Offset: 0x00137A78
		public AlgorithmIdentifier AlgorithmID
		{
			get
			{
				return this.algID;
			}
		}

		// Token: 0x06003259 RID: 12889 RVA: 0x00138A80 File Offset: 0x00137A80
		public Asn1Object GetPublicKey()
		{
			return Asn1Object.FromByteArray(this.keyData.GetBytes());
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x0600325A RID: 12890 RVA: 0x00138A92 File Offset: 0x00137A92
		public DerBitString PublicKeyData
		{
			get
			{
				return this.keyData;
			}
		}

		// Token: 0x0600325B RID: 12891 RVA: 0x00138A9C File Offset: 0x00137A9C
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.algID,
				this.keyData
			});
		}

		// Token: 0x0400227B RID: 8827
		private readonly AlgorithmIdentifier algID;

		// Token: 0x0400227C RID: 8828
		private readonly DerBitString keyData;
	}
}
