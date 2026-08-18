using System;
using System.Collections;
using System.Globalization;

namespace System.Web.Util
{
	// Token: 0x02000223 RID: 547
	internal class SymbolEqualComparer : IComparer
	{
		// Token: 0x06001A40 RID: 6720 RVA: 0x000030B5 File Offset: 0x000012B5
		internal SymbolEqualComparer()
		{
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x00052478 File Offset: 0x00050678
		int IComparer.Compare(object keyLeft, object keyRight)
		{
			string text = keyLeft as string;
			string text2 = keyRight as string;
			if (text == null)
			{
				throw new ArgumentNullException("keyLeft");
			}
			if (text2 == null)
			{
				throw new ArgumentNullException("keyRight");
			}
			int length = text.Length;
			int length2 = text2.Length;
			if (length != length2)
			{
				return 1;
			}
			for (int i = 0; i < length; i++)
			{
				char c = text[i];
				char c2 = text2[i];
				if (c != c2)
				{
					UnicodeCategory unicodeCategory = char.GetUnicodeCategory(c);
					UnicodeCategory unicodeCategory2 = char.GetUnicodeCategory(c2);
					if (unicodeCategory == UnicodeCategory.UppercaseLetter && unicodeCategory2 == UnicodeCategory.LowercaseLetter)
					{
						if (char.ToLower(c, CultureInfo.InvariantCulture) == c2)
						{
							goto IL_A5;
						}
					}
					else if (unicodeCategory2 == UnicodeCategory.UppercaseLetter && unicodeCategory == UnicodeCategory.LowercaseLetter && char.ToLower(c2, CultureInfo.InvariantCulture) == c)
					{
						goto IL_A5;
					}
					return 1;
				}
				IL_A5:;
			}
			return 0;
		}

		// Token: 0x04001819 RID: 6169
		internal static readonly IComparer Default = new SymbolEqualComparer();
	}
}
