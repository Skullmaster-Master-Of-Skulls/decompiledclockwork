using System;
using System.Text;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200078A RID: 1930
	internal static class TextEncoderDefaults
	{
		// Token: 0x06004996 RID: 18838 RVA: 0x0010E9F8 File Offset: 0x0010CBF8
		internal static void ValidateEncoding(Encoding encoding)
		{
			string webName = encoding.WebName;
			Encoding[] supportedEncodings = TextEncoderDefaults.SupportedEncodings;
			for (int i = 0; i < supportedEncodings.Length; i++)
			{
				if (webName == supportedEncodings[i].WebName)
				{
					return;
				}
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("MessageTextEncodingNotSupported", new object[]
			{
				webName
			}), "encoding"));
		}

		// Token: 0x06004997 RID: 18839 RVA: 0x0010EA5C File Offset: 0x0010CC5C
		internal static string EncodingToCharSet(Encoding encoding)
		{
			string webName = encoding.WebName;
			TextEncoderDefaults.CharSetEncoding[] charSetEncodings = TextEncoderDefaults.CharSetEncodings;
			for (int i = 0; i < charSetEncodings.Length; i++)
			{
				Encoding encoding2 = charSetEncodings[i].Encoding;
				if (encoding2 != null && encoding2.WebName == webName)
				{
					return charSetEncodings[i].CharSet;
				}
			}
			return null;
		}

		// Token: 0x06004998 RID: 18840 RVA: 0x0010EAA8 File Offset: 0x0010CCA8
		internal static bool TryGetEncoding(string charSet, out Encoding encoding)
		{
			TextEncoderDefaults.CharSetEncoding[] charSetEncodings = TextEncoderDefaults.CharSetEncodings;
			for (int i = 0; i < charSetEncodings.Length; i++)
			{
				if (charSetEncodings[i].CharSet == charSet)
				{
					encoding = charSetEncodings[i].Encoding;
					return true;
				}
			}
			for (int j = 0; j < charSetEncodings.Length; j++)
			{
				string charSet2 = charSetEncodings[j].CharSet;
				if (charSet2 != null && charSet2.Equals(charSet, StringComparison.OrdinalIgnoreCase))
				{
					encoding = charSetEncodings[j].Encoding;
					return true;
				}
			}
			encoding = null;
			return false;
		}

		// Token: 0x04002E50 RID: 11856
		internal static readonly Encoding Encoding = Encoding.GetEncoding("utf-8", new EncoderExceptionFallback(), new DecoderExceptionFallback());

		// Token: 0x04002E51 RID: 11857
		internal const string EncodingString = "utf-8";

		// Token: 0x04002E52 RID: 11858
		internal static readonly Encoding[] SupportedEncodings = new Encoding[]
		{
			Encoding.UTF8,
			Encoding.Unicode,
			Encoding.BigEndianUnicode
		};

		// Token: 0x04002E53 RID: 11859
		internal const string MessageVersionString = "Soap12WSAddressing10";

		// Token: 0x04002E54 RID: 11860
		internal static readonly TextEncoderDefaults.CharSetEncoding[] CharSetEncodings = new TextEncoderDefaults.CharSetEncoding[]
		{
			new TextEncoderDefaults.CharSetEncoding("utf-8", Encoding.UTF8),
			new TextEncoderDefaults.CharSetEncoding("utf-16LE", Encoding.Unicode),
			new TextEncoderDefaults.CharSetEncoding("utf-16BE", Encoding.BigEndianUnicode),
			new TextEncoderDefaults.CharSetEncoding("utf-16", null),
			new TextEncoderDefaults.CharSetEncoding(null, null)
		};

		// Token: 0x02000CEE RID: 3310
		internal class CharSetEncoding
		{
			// Token: 0x06007A6B RID: 31339 RVA: 0x001C8153 File Offset: 0x001C6353
			internal CharSetEncoding(string charSet, Encoding enc)
			{
				this.CharSet = charSet;
				this.Encoding = enc;
			}

			// Token: 0x04004603 RID: 17923
			internal string CharSet;

			// Token: 0x04004604 RID: 17924
			internal Encoding Encoding;
		}
	}
}
