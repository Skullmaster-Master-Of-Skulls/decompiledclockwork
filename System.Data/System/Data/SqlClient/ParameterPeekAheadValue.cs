using System;
using System.Collections.Generic;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x020002A4 RID: 676
	internal class ParameterPeekAheadValue
	{
		// Token: 0x04001683 RID: 5763
		internal IEnumerator<SqlDataRecord> Enumerator;

		// Token: 0x04001684 RID: 5764
		internal SqlDataRecord FirstRecord;
	}
}
