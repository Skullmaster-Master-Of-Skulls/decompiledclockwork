using System;
using System.Text.RegularExpressions;

namespace System.Web.Util
{
	// Token: 0x0200022F RID: 559
	internal class WildcardUrl : WildcardPath
	{
		// Token: 0x06001A89 RID: 6793 RVA: 0x000536AC File Offset: 0x000518AC
		internal WildcardUrl(string pattern, bool caseInsensitive) : base(pattern, caseInsensitive)
		{
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x000536B6 File Offset: 0x000518B6
		protected override string[] SplitDirs(string input)
		{
			return Wildcard.slashRegex.Split(input);
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x000536C4 File Offset: 0x000518C4
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

		// Token: 0x06001A8C RID: 6796 RVA: 0x00053758 File Offset: 0x00051958
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

		// Token: 0x06001A8D RID: 6797 RVA: 0x000537D4 File Offset: 0x000519D4
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
