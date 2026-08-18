using System;

namespace System.Net.Http.Formatting
{
	// Token: 0x0200004C RID: 76
	public enum MediaTypeFormatterMatchRanking
	{
		// Token: 0x040000C4 RID: 196
		None,
		// Token: 0x040000C5 RID: 197
		MatchOnCanWriteType,
		// Token: 0x040000C6 RID: 198
		MatchOnRequestAcceptHeaderLiteral,
		// Token: 0x040000C7 RID: 199
		MatchOnRequestAcceptHeaderSubtypeMediaRange,
		// Token: 0x040000C8 RID: 200
		MatchOnRequestAcceptHeaderAllMediaRange,
		// Token: 0x040000C9 RID: 201
		MatchOnRequestWithMediaTypeMapping,
		// Token: 0x040000CA RID: 202
		MatchOnRequestMediaType
	}
}
