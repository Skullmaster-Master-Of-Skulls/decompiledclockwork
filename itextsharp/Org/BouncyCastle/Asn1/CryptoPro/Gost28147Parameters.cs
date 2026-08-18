using System;

namespace Org.BouncyCastle.Asn1.CryptoPro
{
	// Token: 0x020005C1 RID: 1473
	public class Gost28147Parameters : Asn1Encodable
	{
		// Token: 0x0600329A RID: 12954 RVA: 0x001397A5 File Offset: 0x001387A5
		public static Gost28147Parameters GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return Gost28147Parameters.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x0600329B RID: 12955 RVA: 0x001397B4 File Offset: 0x001387B4
		public static Gost28147Parameters GetInstance(object obj)
		{
			if (obj == null || obj is Gost28147Parameters)
			{
				return (Gost28147Parameters)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new Gost28147Parameters((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid GOST3410Parameter: " + obj.GetType().Name);
		}

		// Token: 0x0600329C RID: 12956 RVA: 0x00139804 File Offset: 0x00138804
		private Gost28147Parameters(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.iv = Asn1OctetString.GetInstance(seq[0]);
			this.paramSet = DerObjectIdentifier.GetInstance(seq[1]);
		}

		// Token: 0x0600329D RID: 12957 RVA: 0x00139854 File Offset: 0x00138854
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.iv,
				this.paramSet
			});
		}

		// Token: 0x04002290 RID: 8848
		private readonly Asn1OctetString iv;

		// Token: 0x04002291 RID: 8849
		private readonly DerObjectIdentifier paramSet;
	}
}
