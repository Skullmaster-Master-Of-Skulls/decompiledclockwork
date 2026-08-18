using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Infrastructure.Pluralization
{
	// Token: 0x0200028E RID: 654
	internal static class PluralizationServiceUtil
	{
		// Token: 0x060016FA RID: 5882 RVA: 0x00072B44 File Offset: 0x00070D44
		internal static bool DoesWordContainSuffix(string word, IEnumerable<string> suffixes, CultureInfo culture)
		{
			return suffixes.Any((string s) => word.EndsWith(s, true, culture));
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x00072B94 File Offset: 0x00070D94
		internal static bool TryGetMatchedSuffixForWord(string word, IEnumerable<string> suffixes, CultureInfo culture, out string matchedSuffix)
		{
			matchedSuffix = null;
			if (PluralizationServiceUtil.DoesWordContainSuffix(word, suffixes, culture))
			{
				matchedSuffix = suffixes.First((string s) => word.EndsWith(s, true, culture));
				return true;
			}
			return false;
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x00072BEC File Offset: 0x00070DEC
		internal static bool TryInflectOnSuffixInWord(string word, IEnumerable<string> suffixes, Func<string, string> operationOnWord, CultureInfo culture, out string newWord)
		{
			newWord = null;
			string text;
			if (PluralizationServiceUtil.TryGetMatchedSuffixForWord(word, suffixes, culture, out text))
			{
				newWord = operationOnWord(word);
				return true;
			}
			return false;
		}
	}
}
