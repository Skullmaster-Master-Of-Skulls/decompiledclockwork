using System;
using System.Text.RegularExpressions;

namespace System.Web.Util
{
	// Token: 0x0200022E RID: 558
	internal abstract class WildcardPath : Wildcard
	{
		// Token: 0x06001A83 RID: 6787 RVA: 0x0005366B File Offset: 0x0005186B
		internal WildcardPath(string pattern, bool caseInsensitive) : base(pattern, caseInsensitive)
		{
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x00053675 File Offset: 0x00051875
		internal bool IsSuffix(string input)
		{
			this.EnsureSuffix();
			return this._suffix.IsMatch(input);
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x00053689 File Offset: 0x00051889
		protected void EnsureSuffix()
		{
			if (this._suffix != null)
			{
				return;
			}
			this._suffix = this.SuffixFromWildcard(this._pattern, this._caseInsensitive);
		}

		// Token: 0x06001A86 RID: 6790
		protected abstract Regex SuffixFromWildcard(string pattern, bool caseInsensitive);

		// Token: 0x06001A87 RID: 6791
		protected abstract Regex[][] DirsFromWildcard(string pattern);

		// Token: 0x06001A88 RID: 6792
		protected abstract string[] SplitDirs(string input);

		// Token: 0x04001845 RID: 6213
		private Regex _suffix;
	}
}
