using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Tsp
{
	// Token: 0x020001AD RID: 429
	public class MessageImprint : Asn1Encodable
	{
		// Token: 0x06001050 RID: 4176 RVA: 0x0005E06C File Offset: 0x0005D06C
		public static MessageImprint GetInstance(object o)
		{
			if (o == null || o is MessageImprint)
			{
				return (MessageImprint)o;
			}
			if (o is Asn1Sequence)
			{
				return new MessageImprint((Asn1Sequence)o);
			}
			throw new ArgumentException("Unknown object in 'MessageImprint' factory: " + o.GetType().FullName);
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x0005E0BC File Offset: 0x0005D0BC
		private MessageImprint(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.hashAlgorithm = AlgorithmIdentifier.GetInstance(seq[0]);
			this.hashedMessage = Asn1OctetString.GetInstance(seq[1]).GetOctets();
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x0005E111 File Offset: 0x0005D111
		public MessageImprint(AlgorithmIdentifier hashAlgorithm, byte[] hashedMessage)
		{
			this.hashAlgorithm = hashAlgorithm;
			this.hashedMessage = hashedMessage;
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06001053 RID: 4179 RVA: 0x0005E127 File Offset: 0x0005D127
		public AlgorithmIdentifier HashAlgorithm
		{
			get
			{
				return this.hashAlgorithm;
			}
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x0005E12F File Offset: 0x0005D12F
		public byte[] GetHashedMessage()
		{
			return this.hashedMessage;
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x0005E138 File Offset: 0x0005D138
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.hashAlgorithm,
				new DerOctetString(this.hashedMessage)
			});
		}

		// Token: 0x04000C05 RID: 3077
		private readonly AlgorithmIdentifier hashAlgorithm;

		// Token: 0x04000C06 RID: 3078
		private readonly byte[] hashedMessage;
	}
}
