using System;
using System.Text.RegularExpressions;

namespace System.Net
{
	// Token: 0x020004A9 RID: 1193
	[Serializable]
	internal class DelayedRegex
	{
		// Token: 0x0600248B RID: 9355 RVA: 0x0008FA58 File Offset: 0x0008EA58
		internal DelayedRegex(string regexString)
		{
			if (regexString == null)
			{
				throw new ArgumentNullException("regexString");
			}
			this._AsString = regexString;
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x0008FA75 File Offset: 0x0008EA75
		internal DelayedRegex(Regex regex)
		{
			if (regex == null)
			{
				throw new ArgumentNullException("regex");
			}
			this._AsRegex = regex;
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x0600248D RID: 9357 RVA: 0x0008FA92 File Offset: 0x0008EA92
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

		// Token: 0x0600248E RID: 9358 RVA: 0x0008FAC4 File Offset: 0x0008EAC4
		public override string ToString()
		{
			if (this._AsString == null)
			{
				return this._AsString = this._AsRegex.ToString();
			}
			return this._AsString;
		}

		// Token: 0x040024CB RID: 9419
		private Regex _AsRegex;

		// Token: 0x040024CC RID: 9420
		private string _AsString;
	}
}
