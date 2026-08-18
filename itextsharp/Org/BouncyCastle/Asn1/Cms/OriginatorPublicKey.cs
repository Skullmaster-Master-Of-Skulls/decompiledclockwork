using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x020000AE RID: 174
	public class OriginatorPublicKey : Asn1Encodable
	{
		// Token: 0x0600056C RID: 1388 RVA: 0x0001C584 File Offset: 0x0001B584
		public OriginatorPublicKey(AlgorithmIdentifier algorithm, byte[] publicKey)
		{
			this.algorithm = algorithm;
			this.publicKey = new DerBitString(publicKey);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0001C59F File Offset: 0x0001B59F
		public OriginatorPublicKey(Asn1Sequence seq)
		{
			this.algorithm = AlgorithmIdentifier.GetInstance(seq[0]);
			this.publicKey = (DerBitString)seq[1];
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0001C5CB File Offset: 0x0001B5CB
		public static OriginatorPublicKey GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return OriginatorPublicKey.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0001C5DC File Offset: 0x0001B5DC
		public static OriginatorPublicKey GetInstance(object obj)
		{
			if (obj == null || obj is OriginatorPublicKey)
			{
				return (OriginatorPublicKey)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OriginatorPublicKey((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid OriginatorPublicKey: " + obj.GetType().Name);
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x0001C629 File Offset: 0x0001B629
		public AlgorithmIdentifier Algorithm
		{
			get
			{
				return this.algorithm;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x0001C631 File Offset: 0x0001B631
		public DerBitString PublicKey
		{
			get
			{
				return this.publicKey;
			}
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0001C63C File Offset: 0x0001B63C
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.algorithm,
				this.publicKey
			});
		}

		// Token: 0x040002AC RID: 684
		private AlgorithmIdentifier algorithm;

		// Token: 0x040002AD RID: 685
		private DerBitString publicKey;
	}
}
