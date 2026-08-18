using System;
using System.IO;
using System.Text;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.X509
{
	// Token: 0x0200059A RID: 1434
	internal class PemParser
	{
		// Token: 0x0600312B RID: 12587 RVA: 0x001303F8 File Offset: 0x0012F3F8
		internal PemParser(string type)
		{
			this._header1 = "-----BEGIN " + type + "-----";
			this._header2 = "-----BEGIN X509 " + type + "-----";
			this._footer1 = "-----END " + type + "-----";
			this._footer2 = "-----END X509 " + type + "-----";
		}

		// Token: 0x0600312C RID: 12588 RVA: 0x00130464 File Offset: 0x0012F464
		private string ReadLine(Stream inStream)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num;
			for (;;)
			{
				if ((num = inStream.ReadByte()) == 13 || num == 10 || num < 0)
				{
					if (num < 0 || stringBuilder.Length != 0)
					{
						break;
					}
				}
				else if (num != 13)
				{
					stringBuilder.Append((char)num);
				}
			}
			if (num < 0)
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600312D RID: 12589 RVA: 0x001304B4 File Offset: 0x0012F4B4
		internal Asn1Sequence ReadPemObject(Stream inStream)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text;
			while ((text = this.ReadLine(inStream)) != null)
			{
				if (text.StartsWith(this._header1) || text.StartsWith(this._header2))
				{
					IL_55:
					while ((text = this.ReadLine(inStream)) != null && !text.StartsWith(this._footer1) && !text.StartsWith(this._footer2))
					{
						stringBuilder.Append(text);
					}
					if (stringBuilder.Length == 0)
					{
						return null;
					}
					Asn1Object asn1Object = Asn1Object.FromByteArray(Base64.Decode(stringBuilder.ToString()));
					if (!(asn1Object is Asn1Sequence))
					{
						throw new IOException("malformed PEM data encountered");
					}
					return (Asn1Sequence)asn1Object;
				}
			}
			goto IL_55;
		}

		// Token: 0x040021C9 RID: 8649
		private readonly string _header1;

		// Token: 0x040021CA RID: 8650
		private readonly string _header2;

		// Token: 0x040021CB RID: 8651
		private readonly string _footer1;

		// Token: 0x040021CC RID: 8652
		private readonly string _footer2;
	}
}
