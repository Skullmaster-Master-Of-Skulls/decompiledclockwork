using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000688 RID: 1672
	internal sealed class CachedCodeEntry
	{
		// Token: 0x06003DE6 RID: 15846 RVA: 0x000FD91C File Offset: 0x000FBB1C
		internal CachedCodeEntry(string key, Hashtable capnames, string[] capslist, RegexCode code, Hashtable caps, int capsize, ExclusiveReference runner, SharedReference repl)
		{
			this._key = key;
			this._capnames = capnames;
			this._capslist = capslist;
			this._code = code;
			this._caps = caps;
			this._capsize = capsize;
			this._runnerref = runner;
			this._replref = repl;
		}

		// Token: 0x06003DE7 RID: 15847 RVA: 0x000FD96C File Offset: 0x000FBB6C
		internal void AddCompiled(RegexRunnerFactory factory)
		{
			this._factory = factory;
			this._code = null;
		}

		// Token: 0x04002CEA RID: 11498
		internal string _key;

		// Token: 0x04002CEB RID: 11499
		internal RegexCode _code;

		// Token: 0x04002CEC RID: 11500
		internal Hashtable _caps;

		// Token: 0x04002CED RID: 11501
		internal Hashtable _capnames;

		// Token: 0x04002CEE RID: 11502
		internal string[] _capslist;

		// Token: 0x04002CEF RID: 11503
		internal int _capsize;

		// Token: 0x04002CF0 RID: 11504
		internal RegexRunnerFactory _factory;

		// Token: 0x04002CF1 RID: 11505
		internal ExclusiveReference _runnerref;

		// Token: 0x04002CF2 RID: 11506
		internal SharedReference _replref;
	}
}
