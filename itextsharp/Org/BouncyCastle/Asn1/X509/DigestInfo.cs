using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000445 RID: 1093
	public class DigestInfo : Asn1Encodable
	{
		// Token: 0x06002503 RID: 9475 RVA: 0x000E0EF5 File Offset: 0x000DFEF5
		public static DigestInfo GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return DigestInfo.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x000E0F04 File Offset: 0x000DFF04
		public static DigestInfo GetInstance(object obj)
		{
			if (obj is DigestInfo)
			{
				return (DigestInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new DigestInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06002505 RID: 9477 RVA: 0x000E0F53 File Offset: 0x000DFF53
		public DigestInfo(AlgorithmIdentifier algID, byte[] digest)
		{
			this.digest = digest;
			this.algID = algID;
		}

		// Token: 0x06002506 RID: 9478 RVA: 0x000E0F6C File Offset: 0x000DFF6C
		private DigestInfo(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.algID = AlgorithmIdentifier.GetInstance(seq[0]);
			this.digest = Asn1OctetString.GetInstance(seq[1]).GetOctets();
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06002507 RID: 9479 RVA: 0x000E0FC1 File Offset: 0x000DFFC1
		public AlgorithmIdentifier AlgorithmID
		{
			get
			{
				return this.algID;
			}
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x000E0FC9 File Offset: 0x000DFFC9
		public byte[] GetDigest()
		{
			return this.digest;
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x000E0FD4 File Offset: 0x000DFFD4
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.algID,
				new DerOctetString(this.digest)
			});
		}

		// Token: 0x040019D2 RID: 6610
		private readonly byte[] digest;

		// Token: 0x040019D3 RID: 6611
		private readonly AlgorithmIdentifier algID;
	}
}
