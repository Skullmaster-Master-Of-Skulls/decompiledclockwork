using System;
using System.IO;

namespace System.Web.Razor.Text
{
	// Token: 0x0200005D RID: 93
	public abstract class LookaheadTextReader : TextReader
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000460 RID: 1120
		public abstract SourceLocation CurrentLocation { get; }

		// Token: 0x06000461 RID: 1121
		public abstract IDisposable BeginLookahead();

		// Token: 0x06000462 RID: 1122
		public abstract void CancelBacktrack();
	}
}
