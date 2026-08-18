using System;

namespace System.Data.SqlClient
{
	// Token: 0x020001D4 RID: 468
	internal class SessionStateRecord
	{
		// Token: 0x040010CE RID: 4302
		internal bool _recoverable;

		// Token: 0x040010CF RID: 4303
		internal uint _version;

		// Token: 0x040010D0 RID: 4304
		internal int _dataLength;

		// Token: 0x040010D1 RID: 4305
		internal byte[] _data;
	}
}
