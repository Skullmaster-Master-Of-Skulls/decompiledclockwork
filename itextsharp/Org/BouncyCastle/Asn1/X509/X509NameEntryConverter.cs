using System;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000035 RID: 53
	public abstract class X509NameEntryConverter
	{
		// Token: 0x06000167 RID: 359 RVA: 0x00008F40 File Offset: 0x00007F40
		protected Asn1Object ConvertHexEncoded(string hexString, int offset)
		{
			string data = hexString.Substring(offset);
			return Asn1Object.FromByteArray(Hex.Decode(data));
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00008F60 File Offset: 0x00007F60
		protected bool CanBePrintable(string str)
		{
			return DerPrintableString.IsPrintableString(str);
		}

		// Token: 0x06000169 RID: 361
		public abstract Asn1Object GetConvertedValue(DerObjectIdentifier oid, string value);
	}
}
