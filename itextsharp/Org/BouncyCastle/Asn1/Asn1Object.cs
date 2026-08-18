using System;
using System.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000040 RID: 64
	public abstract class Asn1Object : Asn1Encodable
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x00009777 File Offset: 0x00008777
		public static Asn1Object FromByteArray(byte[] data)
		{
			return new Asn1InputStream(data).ReadObject();
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00009784 File Offset: 0x00008784
		public static Asn1Object FromStream(Stream inStr)
		{
			return new Asn1InputStream(inStr).ReadObject();
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00009791 File Offset: 0x00008791
		public sealed override Asn1Object ToAsn1Object()
		{
			return this;
		}

		// Token: 0x060001A3 RID: 419
		internal abstract void Encode(DerOutputStream derOut);

		// Token: 0x060001A4 RID: 420
		protected abstract bool Asn1Equals(Asn1Object asn1Object);

		// Token: 0x060001A5 RID: 421
		protected abstract int Asn1GetHashCode();

		// Token: 0x060001A6 RID: 422 RVA: 0x00009794 File Offset: 0x00008794
		internal bool CallAsn1Equals(Asn1Object obj)
		{
			return this.Asn1Equals(obj);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000979D File Offset: 0x0000879D
		internal int CallAsn1GetHashCode()
		{
			return this.Asn1GetHashCode();
		}
	}
}
