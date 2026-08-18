using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200069D RID: 1693
	internal class MatchSparse : Match
	{
		// Token: 0x06003F20 RID: 16160 RVA: 0x00107A7E File Offset: 0x00105C7E
		internal MatchSparse(Regex regex, Hashtable caps, int capcount, string text, int begpos, int len, int startpos) : base(regex, capcount, text, begpos, len, startpos)
		{
			this._caps = caps;
		}

		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x06003F21 RID: 16161 RVA: 0x00107A97 File Offset: 0x00105C97
		public override GroupCollection Groups
		{
			get
			{
				if (this._groupcoll == null)
				{
					this._groupcoll = new GroupCollection(this, this._caps);
				}
				return this._groupcoll;
			}
		}

		// Token: 0x04002DFF RID: 11775
		internal new Hashtable _caps;
	}
}
