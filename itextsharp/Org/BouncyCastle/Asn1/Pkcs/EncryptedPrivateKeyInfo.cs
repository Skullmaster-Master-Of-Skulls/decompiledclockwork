using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x02000628 RID: 1576
	public class EncryptedPrivateKeyInfo : Asn1Encodable
	{
		// Token: 0x0600357A RID: 13690 RVA: 0x0014BA2C File Offset: 0x0014AA2C
		private EncryptedPrivateKeyInfo(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.algId = AlgorithmIdentifier.GetInstance(seq[0]);
			this.data = Asn1OctetString.GetInstance(seq[1]);
		}

		// Token: 0x0600357B RID: 13691 RVA: 0x0014BA7C File Offset: 0x0014AA7C
		public EncryptedPrivateKeyInfo(AlgorithmIdentifier algId, byte[] encoding)
		{
			this.algId = algId;
			this.data = new DerOctetString(encoding);
		}

		// Token: 0x0600357C RID: 13692 RVA: 0x0014BA98 File Offset: 0x0014AA98
		public static EncryptedPrivateKeyInfo GetInstance(object obj)
		{
			if (obj is EncryptedPrivateKeyInfo)
			{
				return (EncryptedPrivateKeyInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new EncryptedPrivateKeyInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + obj.GetType().FullName, "obj");
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x0600357D RID: 13693 RVA: 0x0014BAE7 File Offset: 0x0014AAE7
		public AlgorithmIdentifier EncryptionAlgorithm
		{
			get
			{
				return this.algId;
			}
		}

		// Token: 0x0600357E RID: 13694 RVA: 0x0014BAEF File Offset: 0x0014AAEF
		public byte[] GetEncryptedData()
		{
			return this.data.GetOctets();
		}

		// Token: 0x0600357F RID: 13695 RVA: 0x0014BAFC File Offset: 0x0014AAFC
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.algId,
				this.data
			});
		}

		// Token: 0x040023C0 RID: 9152
		private readonly AlgorithmIdentifier algId;

		// Token: 0x040023C1 RID: 9153
		private readonly Asn1OctetString data;
	}
}
