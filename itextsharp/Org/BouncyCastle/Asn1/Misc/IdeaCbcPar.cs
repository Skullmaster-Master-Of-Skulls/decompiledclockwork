using System;

namespace Org.BouncyCastle.Asn1.Misc
{
	// Token: 0x02000406 RID: 1030
	public class IdeaCbcPar : Asn1Encodable
	{
		// Token: 0x0600231B RID: 8987 RVA: 0x000D8964 File Offset: 0x000D7964
		public static IdeaCbcPar GetInstance(object o)
		{
			if (o is IdeaCbcPar)
			{
				return (IdeaCbcPar)o;
			}
			if (o is Asn1Sequence)
			{
				return new IdeaCbcPar((Asn1Sequence)o);
			}
			throw new ArgumentException("unknown object in IDEACBCPar factory");
		}

		// Token: 0x0600231C RID: 8988 RVA: 0x000D8993 File Offset: 0x000D7993
		public IdeaCbcPar(byte[] iv)
		{
			this.iv = new DerOctetString(iv);
		}

		// Token: 0x0600231D RID: 8989 RVA: 0x000D89A7 File Offset: 0x000D79A7
		private IdeaCbcPar(Asn1Sequence seq)
		{
			if (seq.Count == 1)
			{
				this.iv = (Asn1OctetString)seq[0];
			}
		}

		// Token: 0x0600231E RID: 8990 RVA: 0x000D89CA File Offset: 0x000D79CA
		public byte[] GetIV()
		{
			if (this.iv != null)
			{
				return this.iv.GetOctets();
			}
			return null;
		}

		// Token: 0x0600231F RID: 8991 RVA: 0x000D89E4 File Offset: 0x000D79E4
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			if (this.iv != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.iv
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04001865 RID: 6245
		internal Asn1OctetString iv;
	}
}
