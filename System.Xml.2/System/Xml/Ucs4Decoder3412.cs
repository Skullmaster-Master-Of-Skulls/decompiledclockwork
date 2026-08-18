using System;

namespace System.Xml
{
	// Token: 0x0200008D RID: 141
	internal class Ucs4Decoder3412 : Ucs4Decoder
	{
		// Token: 0x060004FF RID: 1279 RVA: 0x00012D78 File Offset: 0x00010F78
		internal override int GetFullChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			byteCount += byteIndex;
			int num = byteIndex;
			int num2 = charIndex;
			while (num + 3 < byteCount)
			{
				uint num3 = (uint)((int)bytes[num + 2] << 24 | (int)bytes[num + 3] << 16 | (int)bytes[num] << 8 | (int)bytes[num + 1]);
				if (num3 > 1114111U)
				{
					throw new ArgumentException(Res.GetString("Enc_InvalidByteInEncoding", new object[]
					{
						num
					}), null);
				}
				if (num3 > 65535U)
				{
					base.Ucs4ToUTF16(num3, chars, num2);
					num2++;
				}
				else
				{
					if (XmlCharType.IsSurrogate((int)num3))
					{
						throw new XmlException("Xml_InvalidCharInThisEncoding", string.Empty);
					}
					chars[num2] = (char)num3;
				}
				num2++;
				num += 4;
			}
			return num2 - charIndex;
		}
	}
}
