using System;
using System.Text.RegularExpressions;

namespace System.Web.Util
{
	// Token: 0x0200022D RID: 557
	internal class Wildcard
	{
		// Token: 0x06001A7E RID: 6782 RVA: 0x00053514 File Offset: 0x00051714
		internal Wildcard(string pattern, bool caseInsensitive)
		{
			this._pattern = pattern;
			this._caseInsensitive = caseInsensitive;
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x0005352C File Offset: 0x0005172C
		internal bool IsMatch(string input)
		{
			this.EnsureRegex();
			return this._regex.IsMatch(input);
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x0005354D File Offset: 0x0005174D
		protected void EnsureRegex()
		{
			if (this._regex != null)
			{
				return;
			}
			this._regex = this.RegexFromWildcard(this._pattern, this._caseInsensitive);
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x00053570 File Offset: 0x00051770
		protected virtual Regex RegexFromWildcard(string pattern, bool caseInsensitive)
		{
			RegexOptions regexOptions;
			if (pattern.Length > 0 && pattern[0] == '*')
			{
				regexOptions = (RegexOptions.Singleline | RegexOptions.RightToLeft);
			}
			else
			{
				regexOptions = RegexOptions.Singleline;
			}
			if (caseInsensitive)
			{
				regexOptions |= (RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
			}
			pattern = Wildcard.metaRegex.Replace(pattern, "\\$0");
			pattern = Wildcard.questRegex.Replace(pattern, ".");
			pattern = Wildcard.starRegex.Replace(pattern, ".*");
			pattern = Wildcard.commaRegex.Replace(pattern, "\\z|\\A");
			return new Regex("\\A" + pattern + "\\z", regexOptions);
		}

		// Token: 0x0400183C RID: 6204
		internal string _pattern;

		// Token: 0x0400183D RID: 6205
		internal bool _caseInsensitive;

		// Token: 0x0400183E RID: 6206
		internal Regex _regex;

		// Token: 0x0400183F RID: 6207
		protected static Regex metaRegex = new Regex("[\\+\\{\\\\\\[\\|\\(\\)\\.\\^\\$]");

		// Token: 0x04001840 RID: 6208
		protected static Regex questRegex = new Regex("\\?");

		// Token: 0x04001841 RID: 6209
		protected static Regex starRegex = new Regex("\\*");

		// Token: 0x04001842 RID: 6210
		protected static Regex commaRegex = new Regex(",");

		// Token: 0x04001843 RID: 6211
		protected static Regex slashRegex = new Regex("(?=/)");

		// Token: 0x04001844 RID: 6212
		protected static Regex backslashRegex = new Regex("(?=[\\\\:])");
	}
}
