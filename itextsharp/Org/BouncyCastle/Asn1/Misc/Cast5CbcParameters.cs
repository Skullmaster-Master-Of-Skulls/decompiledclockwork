using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Misc
{
	// Token: 0x0200020B RID: 523
	public class Cast5CbcParameters : Asn1Encodable
	{
		// Token: 0x06001404 RID: 5124 RVA: 0x00072D1D File Offset: 0x00071D1D
		public static Cast5CbcParameters GetInstance(object o)
		{
			if (o is Cast5CbcParameters)
			{
				return (Cast5CbcParameters)o;
			}
			if (o is Asn1Sequence)
			{
				return new Cast5CbcParameters((Asn1Sequence)o);
			}
			throw new ArgumentException("unknown object in Cast5CbcParameters factory");
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x00072D4C File Offset: 0x00071D4C
		public Cast5CbcParameters(byte[] iv, int keyLength)
		{
			this.iv = new DerOctetString(iv);
			this.keyLength = new DerInteger(keyLength);
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x00072D6C File Offset: 0x00071D6C
		private Cast5CbcParameters(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.iv = (Asn1OctetString)seq[0];
			this.keyLength = (DerInteger)seq[1];
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x00072DBC File Offset: 0x00071DBC
		public byte[] GetIV()
		{
			return Arrays.Clone(this.iv.GetOctets());
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06001408 RID: 5128 RVA: 0x00072DCE File Offset: 0x00071DCE
		public int KeyLength
		{
			get
			{
				return this.keyLength.Value.IntValue;
			}
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x00072DE0 File Offset: 0x00071DE0
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.iv,
				this.keyLength
			});
		}

		// Token: 0x04000DD5 RID: 3541
		private readonly DerInteger keyLength;

		// Token: 0x04000DD6 RID: 3542
		private readonly Asn1OctetString iv;
	}
}
