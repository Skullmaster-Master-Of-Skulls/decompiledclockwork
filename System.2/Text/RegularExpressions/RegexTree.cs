using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x020006A7 RID: 1703
	internal sealed class RegexTree
	{
		// Token: 0x06003FC4 RID: 16324 RVA: 0x0010C0FE File Offset: 0x0010A2FE
		internal RegexTree(RegexNode root, Hashtable caps, int[] capnumlist, int captop, Hashtable capnames, string[] capslist, RegexOptions opts)
		{
			this._root = root;
			this._caps = caps;
			this._capnumlist = capnumlist;
			this._capnames = capnames;
			this._capslist = capslist;
			this._captop = captop;
			this._options = opts;
		}

		// Token: 0x04002E7B RID: 11899
		internal RegexNode _root;

		// Token: 0x04002E7C RID: 11900
		internal Hashtable _caps;

		// Token: 0x04002E7D RID: 11901
		internal int[] _capnumlist;

		// Token: 0x04002E7E RID: 11902
		internal Hashtable _capnames;

		// Token: 0x04002E7F RID: 11903
		internal string[] _capslist;

		// Token: 0x04002E80 RID: 11904
		internal RegexOptions _options;

		// Token: 0x04002E81 RID: 11905
		internal int _captop;
	}
}
