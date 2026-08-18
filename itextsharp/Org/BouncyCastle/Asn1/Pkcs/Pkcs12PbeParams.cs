using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020002B8 RID: 696
	public class Pkcs12PbeParams : Asn1Encodable
	{
		// Token: 0x06001A44 RID: 6724 RVA: 0x0009B903 File Offset: 0x0009A903
		public Pkcs12PbeParams(byte[] salt, int iterations)
		{
			this.iv = new DerOctetString(salt);
			this.iterations = new DerInteger(iterations);
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x0009B924 File Offset: 0x0009A924
		private Pkcs12PbeParams(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.iv = Asn1OctetString.GetInstance(seq[0]);
			this.iterations = DerInteger.GetInstance(seq[1]);
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x0009B974 File Offset: 0x0009A974
		public static Pkcs12PbeParams GetInstance(object obj)
		{
			if (obj is Pkcs12PbeParams)
			{
				return (Pkcs12PbeParams)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new Pkcs12PbeParams((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + obj.GetType().FullName, "obj");
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001A47 RID: 6727 RVA: 0x0009B9C3 File Offset: 0x0009A9C3
		public BigInteger Iterations
		{
			get
			{
				return this.iterations.Value;
			}
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x0009B9D0 File Offset: 0x0009A9D0
		public byte[] GetIV()
		{
			return this.iv.GetOctets();
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x0009B9E0 File Offset: 0x0009A9E0
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.iv,
				this.iterations
			});
		}

		// Token: 0x04001199 RID: 4505
		private readonly DerInteger iterations;

		// Token: 0x0400119A RID: 4506
		private readonly Asn1OctetString iv;
	}
}
