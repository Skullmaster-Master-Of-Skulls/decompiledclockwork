using System;
using System.Text.RegularExpressions;

namespace System.Net
{
	// Token: 0x02000186 RID: 390
	[Serializable]
	internal class DelayedRegex
	{
		// Token: 0x06000E86 RID: 3718 RVA: 0x0004BC90 File Offset: 0x00049E90
		internal DelayedRegex(string regexString)
		{
			if (regexString == null)
			{
				throw new ArgumentNullException("regexString");
			}
			this._AsString = regexString;
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x0004BCAD File Offset: 0x00049EAD
		internal DelayedRegex(Regex regex)
		{
			if (regex == null)
			{
				throw new ArgumentNullException("regex");
			}
			this._AsRegex = regex;
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000E88 RID: 3720 RVA: 0x0004BCCA File Offset: 0x00049ECA
		internal Regex AsRegex
		{
			get
			{
				if (this._AsRegex == null)
				{
					this._AsRegex = new Regex(this._AsString + "[/]?", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);
				}
				return this._AsRegex;
			}
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x0004BCFC File Offset: 0x00049EFC
		public override string ToString()
		{
			if (this._AsString == null)
			{
				return this._AsString = this._AsRegex.ToString();
			}
			return this._AsString;
		}

		// Token: 0x0400127B RID: 4731
		private Regex _AsRegex;

		// Token: 0x0400127C RID: 4732
		private string _AsString;
	}
}
