using System;

namespace System.Data.Odbc
{
	// Token: 0x02000288 RID: 648
	internal sealed class DbSchemaInfo
	{
		// Token: 0x0600270E RID: 9998 RVA: 0x00108800 File Offset: 0x00107C00
		internal DbSchemaInfo()
		{
		}

		// Token: 0x040019F1 RID: 6641
		internal string _name;

		// Token: 0x040019F2 RID: 6642
		internal string _typename;

		// Token: 0x040019F3 RID: 6643
		internal Type _type;

		// Token: 0x040019F4 RID: 6644
		internal ODBC32.SQL_TYPE? _dbtype;

		// Token: 0x040019F5 RID: 6645
		internal object _scale;

		// Token: 0x040019F6 RID: 6646
		internal object _precision;

		// Token: 0x040019F7 RID: 6647
		internal int _columnlength;

		// Token: 0x040019F8 RID: 6648
		internal int _valueOffset;

		// Token: 0x040019F9 RID: 6649
		internal int _lengthOffset;

		// Token: 0x040019FA RID: 6650
		internal ODBC32.SQL_C _sqlctype;

		// Token: 0x040019FB RID: 6651
		internal ODBC32.SQL_TYPE _sql_type;
	}
}
