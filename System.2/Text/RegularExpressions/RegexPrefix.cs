using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000697 RID: 1687
	internal sealed class RegexPrefix
	{
		// Token: 0x06003ECA RID: 16074 RVA: 0x001059B0 File Offset: 0x00103BB0
		internal RegexPrefix(string prefix, bool ci)
		{
			this._prefix = prefix;
			this._caseInsensitive = ci;
		}

		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x06003ECB RID: 16075 RVA: 0x001059C6 File Offset: 0x00103BC6
		internal string Prefix
		{
			get
			{
				return this._prefix;
			}
		}

		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x06003ECC RID: 16076 RVA: 0x001059CE File Offset: 0x00103BCE
		internal bool CaseInsensitive
		{
			get
			{
				return this._caseInsensitive;
			}
		}

		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x06003ECD RID: 16077 RVA: 0x001059D6 File Offset: 0x00103BD6
		internal static RegexPrefix Empty
		{
			get
			{
				return RegexPrefix._empty;
			}
		}

		// Token: 0x04002DDB RID: 11739
		internal string _prefix;

		// Token: 0x04002DDC RID: 11740
		internal bool _caseInsensitive;

		// Token: 0x04002DDD RID: 11741
		internal static RegexPrefix _empty = new RegexPrefix(string.Empty, false);
	}
}
