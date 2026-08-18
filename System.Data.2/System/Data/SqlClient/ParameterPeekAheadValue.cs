using System;
using System.Collections.Generic;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x02000231 RID: 561
	internal class ParameterPeekAheadValue
	{
		// Token: 0x0400152D RID: 5421
		internal IEnumerator<SqlDataRecord> Enumerator;

		// Token: 0x0400152E RID: 5422
		internal SqlDataRecord FirstRecord;
	}
}
