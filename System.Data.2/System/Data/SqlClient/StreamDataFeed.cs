using System;
using System.IO;

namespace System.Data.SqlClient
{
	// Token: 0x020001EB RID: 491
	internal class StreamDataFeed : DataFeed
	{
		// Token: 0x06001E57 RID: 7767 RVA: 0x000D4AEC File Offset: 0x000D3EEC
		internal StreamDataFeed(Stream source)
		{
			this._source = source;
		}

		// Token: 0x04001172 RID: 4466
		internal Stream _source;
	}
}
