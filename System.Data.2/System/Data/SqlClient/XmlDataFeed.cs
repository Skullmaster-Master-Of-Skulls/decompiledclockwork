using System;
using System.Xml;

namespace System.Data.SqlClient
{
	// Token: 0x020001ED RID: 493
	internal class XmlDataFeed : DataFeed
	{
		// Token: 0x06001E59 RID: 7769 RVA: 0x000D4B24 File Offset: 0x000D3F24
		internal XmlDataFeed(XmlReader source)
		{
			this._source = source;
		}

		// Token: 0x04001174 RID: 4468
		internal XmlReader _source;
	}
}
