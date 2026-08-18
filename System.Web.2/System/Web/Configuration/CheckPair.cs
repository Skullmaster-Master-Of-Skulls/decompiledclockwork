using System;
using System.Text.RegularExpressions;

namespace System.Web.Configuration
{
	// Token: 0x020006B7 RID: 1719
	internal class CheckPair
	{
		// Token: 0x0600532A RID: 21290 RVA: 0x0012498C File Offset: 0x00122B8C
		internal CheckPair(string header, string match, bool nonMatch)
		{
			this._header = header;
			this._match = match;
			this._nonMatch = nonMatch;
			Regex regex = new Regex(match);
		}

		// Token: 0x0600532B RID: 21291 RVA: 0x001249BC File Offset: 0x00122BBC
		internal CheckPair(string header, string match)
		{
			this._header = header;
			this._match = match;
			this._nonMatch = false;
			Regex regex = new Regex(match);
		}

		// Token: 0x170017AB RID: 6059
		// (get) Token: 0x0600532C RID: 21292 RVA: 0x001249EB File Offset: 0x00122BEB
		public string Header
		{
			get
			{
				return this._header;
			}
		}

		// Token: 0x170017AC RID: 6060
		// (get) Token: 0x0600532D RID: 21293 RVA: 0x001249F3 File Offset: 0x00122BF3
		public string MatchString
		{
			get
			{
				return this._match;
			}
		}

		// Token: 0x170017AD RID: 6061
		// (get) Token: 0x0600532E RID: 21294 RVA: 0x001249FB File Offset: 0x00122BFB
		public bool NonMatch
		{
			get
			{
				return this._nonMatch;
			}
		}

		// Token: 0x04002BA4 RID: 11172
		private string _header;

		// Token: 0x04002BA5 RID: 11173
		private string _match;

		// Token: 0x04002BA6 RID: 11174
		private bool _nonMatch;
	}
}
