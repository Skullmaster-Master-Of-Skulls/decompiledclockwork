using System;
using System.Text.RegularExpressions;

namespace System.Web.Util
{
	// Token: 0x0200079D RID: 1949
	internal class WildcardUrl : WildcardPath
	{
		// Token: 0x06005D70 RID: 23920 RVA: 0x0017645C File Offset: 0x0017545C
		internal WildcardUrl(string pattern, bool caseInsensitive) : base(pattern, caseInsensitive)
		{
		}

		// Token: 0x06005D71 RID: 23921 RVA: 0x00176466 File Offset: 0x00175466
		protected override string[] SplitDirs(string input)
		{
			return Wildcard.slashRegex.Split(input);
		}

		// Token: 0x06005D72 RID: 23922 RVA: 0x00176474 File Offset: 0x00175474
		protected override Regex RegexFromWildcard(string pattern, bool caseInsensitive)
		{
			RegexOptions regexOptions;
			if (pattern.Length > 0 && pattern[0] == '*')
			{
				regexOptions = RegexOptions.RightToLeft;
			}
			else
			{
				regexOptions = RegexOptions.None;
			}
			if (caseInsensitive)
			{
				regexOptions |= (RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
			}
			pattern = Wildcard.metaRegex.Replace(pattern, "\\$0");
			pattern = Wildcard.questRegex.Replace(pattern, "[^/]");
			pattern = Wildcard.starRegex.Replace(pattern, "[^/]*");
			pattern = Wildcard.commaRegex.Replace(pattern, "\\z|\\A");
			return new Regex("\\A" + pattern + "\\z", regexOptions);
		}

		// Token: 0x06005D73 RID: 23923 RVA: 0x00176508 File Offset: 0x00175508
		protected override Regex SuffixFromWildcard(string pattern, bool caseInsensitive)
		{
			RegexOptions regexOptions = RegexOptions.RightToLeft;
			if (caseInsensitive)
			{
				regexOptions |= (RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
			}
			pattern = Wildcard.metaRegex.Replace(pattern, "\\$0");
			pattern = Wildcard.questRegex.Replace(pattern, "[^/]");
			pattern = Wildcard.starRegex.Replace(pattern, "[^/]*");
			pattern = Wildcard.commaRegex.Replace(pattern, "\\z|(?:\\A|(?<=/))");
			return new Regex("(?:\\A|(?<=/))" + pattern + "\\z", regexOptions);
		}

		// Token: 0x06005D74 RID: 23924 RVA: 0x00176584 File Offset: 0x00175584
		protected override Regex[][] DirsFromWildcard(string pattern)
		{
			string[] array = Wildcard.commaRegex.Split(pattern);
			Regex[][] array2 = new Regex[array.Length][];
			for (int i = 0; i < array.Length; i++)
			{
				string[] array3 = Wildcard.slashRegex.Split(array[i]);
				Regex[] array4 = new Regex[array3.Length];
				if (array.Length == 1 && array3.Length == 1)
				{
					base.EnsureRegex();
					array4[0] = this._regex;
				}
				else
				{
					for (int j = 0; j < array3.Length; j++)
					{
						array4[j] = this.RegexFromWildcard(array3[j], this._caseInsensitive);
					}
				}
				array2[i] = array4;
			}
			return array2;
		}
	}
}
