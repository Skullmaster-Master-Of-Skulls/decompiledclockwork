using System;
using System.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000038 RID: 56
	public abstract class Asn1Encodable : IAsn1Convertible
	{
		// Token: 0x0600016E RID: 366 RVA: 0x00009064 File Offset: 0x00008064
		public byte[] GetEncoded()
		{
			MemoryStream memoryStream = new MemoryStream();
			Asn1OutputStream asn1OutputStream = new Asn1OutputStream(memoryStream);
			asn1OutputStream.WriteObject(this);
			return memoryStream.ToArray();
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000908C File Offset: 0x0000808C
		public byte[] GetEncoded(string encoding)
		{
			if (encoding.Equals("DER"))
			{
				MemoryStream memoryStream = new MemoryStream();
				DerOutputStream derOutputStream = new DerOutputStream(memoryStream);
				derOutputStream.WriteObject(this);
				return memoryStream.ToArray();
			}
			return this.GetEncoded();
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000090C8 File Offset: 0x000080C8
		public byte[] GetDerEncoded()
		{
			byte[] result;
			try
			{
				result = this.GetEncoded("DER");
			}
			catch (IOException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000090FC File Offset: 0x000080FC
		public sealed override int GetHashCode()
		{
			return this.ToAsn1Object().CallAsn1GetHashCode();
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000910C File Offset: 0x0000810C
		public sealed override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			IAsn1Convertible asn1Convertible = obj as IAsn1Convertible;
			if (asn1Convertible == null)
			{
				return false;
			}
			Asn1Object asn1Object = this.ToAsn1Object();
			Asn1Object asn1Object2 = asn1Convertible.ToAsn1Object();
			return asn1Object == asn1Object2 || asn1Object.CallAsn1Equals(asn1Object2);
		}

		// Token: 0x06000173 RID: 371
		public abstract Asn1Object ToAsn1Object();

		// Token: 0x040000B1 RID: 177
		public const string Der = "DER";

		// Token: 0x040000B2 RID: 178
		public const string Ber = "BER";
	}
}
