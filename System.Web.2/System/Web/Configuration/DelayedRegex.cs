using System;
using System.Text.RegularExpressions;

namespace System.Web.Configuration
{
	// Token: 0x020006D2 RID: 1746
	internal class DelayedRegex
	{
		// Token: 0x0600540D RID: 21517 RVA: 0x00126FA6 File Offset: 0x001251A6
		internal DelayedRegex(string s)
		{
			this._regex = null;
			this._regstring = s;
		}

		// Token: 0x0600540E RID: 21518 RVA: 0x00126FBC File Offset: 0x001251BC
		internal Match Match(string s)
		{
			this.EnsureRegex();
			return this._regex.Match(s);
		}

		// Token: 0x0600540F RID: 21519 RVA: 0x00126FD0 File Offset: 0x001251D0
		internal int GroupNumberFromName(string name)
		{
			this.EnsureRegex();
			return this._regex.GroupNumberFromName(name);
		}

		// Token: 0x06005410 RID: 21520 RVA: 0x00126FE4 File Offset: 0x001251E4
		internal void EnsureRegex()
		{
			string regstring = this._regstring;
			if (this._regex == null)
			{
				this._regex = new Regex(regstring);
				this._regstring = null;
			}
		}

		// Token: 0x04002C38 RID: 11320
		private string _regstring;

		// Token: 0x04002C39 RID: 11321
		private Regex _regex;
	}
}
