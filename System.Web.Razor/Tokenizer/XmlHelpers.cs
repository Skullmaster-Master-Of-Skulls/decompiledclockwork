using System;

namespace System.Web.Razor.Tokenizer
{
	// Token: 0x02000083 RID: 131
	internal static class XmlHelpers
	{
		// Token: 0x0600059B RID: 1435 RVA: 0x000161F8 File Offset: 0x000143F8
		public static bool IsXmlNameStartChar(char chr)
		{
			return char.IsLetter(chr) || chr == ':' || chr == '_' || XmlHelpers.IsInRange(chr, 192, 214) || XmlHelpers.IsInRange(chr, 216, 246) || XmlHelpers.IsInRange(chr, 248, 767) || XmlHelpers.IsInRange(chr, 880, 893) || XmlHelpers.IsInRange(chr, 895, 8191) || XmlHelpers.IsInRange(chr, 8204, 8205) || XmlHelpers.IsInRange(chr, 8304, 8591) || XmlHelpers.IsInRange(chr, 11264, 12271) || XmlHelpers.IsInRange(chr, 12289, 55295) || XmlHelpers.IsInRange(chr, 63744, 64975) || XmlHelpers.IsInRange(chr, 65008, 65533) || XmlHelpers.IsInRange(chr, 65536, 983039);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00016304 File Offset: 0x00014504
		public static bool IsXmlNameChar(char chr)
		{
			return char.IsDigit(chr) || XmlHelpers.IsXmlNameStartChar(chr) || chr == '-' || chr == '.' || chr == '·' || XmlHelpers.IsInRange(chr, 768, 879) || XmlHelpers.IsInRange(chr, 8255, 8256);
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00016357 File Offset: 0x00014557
		public static bool IsInRange(char chr, int low, int high)
		{
			return (int)chr >= low && (int)chr <= high;
		}
	}
}
