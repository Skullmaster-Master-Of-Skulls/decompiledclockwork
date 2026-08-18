using System;
using System.IO;

namespace System.Data.SqlClient
{
	// Token: 0x020001EC RID: 492
	internal class TextDataFeed : DataFeed
	{
		// Token: 0x06001E58 RID: 7768 RVA: 0x000D4B08 File Offset: 0x000D3F08
		internal TextDataFeed(TextReader source)
		{
			this._source = source;
		}

		// Token: 0x04001173 RID: 4467
		internal TextReader _source;
	}
}
