using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020000A1 RID: 161
	public class PbeParameter : Asn1Encodable
	{
		// Token: 0x06000518 RID: 1304 RVA: 0x0001B604 File Offset: 0x0001A604
		public static PbeParameter GetInstance(object obj)
		{
			if (obj is PbeParameter || obj == null)
			{
				return (PbeParameter)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PbeParameter((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + obj.GetType().FullName, "obj");
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0001B658 File Offset: 0x0001A658
		private PbeParameter(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.octStr = Asn1OctetString.GetInstance(seq[0]);
			this.iterationCount = DerInteger.GetInstance(seq[1]);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0001B6A8 File Offset: 0x0001A6A8
		public PbeParameter(byte[] salt, int iterationCount)
		{
			this.octStr = new DerOctetString(salt);
			this.iterationCount = new DerInteger(iterationCount);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0001B6C8 File Offset: 0x0001A6C8
		public byte[] GetSalt()
		{
			return this.octStr.GetOctets();
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x0001B6D5 File Offset: 0x0001A6D5
		public BigInteger IterationCount
		{
			get
			{
				return this.iterationCount.Value;
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0001B6E4 File Offset: 0x0001A6E4
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.octStr,
				this.iterationCount
			});
		}

		// Token: 0x04000290 RID: 656
		private readonly Asn1OctetString octStr;

		// Token: 0x04000291 RID: 657
		private readonly DerInteger iterationCount;
	}
}
