using System;

namespace Microsoft.Practices.Unity.Configuration.ConfigurationHelpers
{
	// Token: 0x0200000F RID: 15
	internal class ParseResult
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00002DC6 File Offset: 0x00000FC6
		public ParseResult(bool matched)
		{
			this.Matched = matched;
			this.MatchedString = string.Empty;
			this.ResultData = null;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002DE7 File Offset: 0x00000FE7
		public ParseResult(string matchedCharacters) : this(matchedCharacters, null)
		{
			this.Matched = true;
			this.MatchedString = (matchedCharacters ?? string.Empty);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002E08 File Offset: 0x00001008
		public ParseResult(string matchedCharacters, object resultData)
		{
			this.Matched = true;
			this.MatchedString = (matchedCharacters ?? string.Empty);
			this.ResultData = resultData;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002E2E File Offset: 0x0000102E
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00002E36 File Offset: 0x00001036
		public bool Matched { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002E3F File Offset: 0x0000103F
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002E47 File Offset: 0x00001047
		public string MatchedString { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002E50 File Offset: 0x00001050
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002E58 File Offset: 0x00001058
		public object ResultData { get; private set; }
	}
}
