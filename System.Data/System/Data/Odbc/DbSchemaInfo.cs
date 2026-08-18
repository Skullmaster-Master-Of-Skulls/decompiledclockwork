using System;

namespace System.Data.Odbc
{
	// Token: 0x020001BA RID: 442
	internal sealed class DbSchemaInfo
	{
		// Token: 0x06001940 RID: 6464 RVA: 0x00258CF8 File Offset: 0x002580F8
		internal DbSchemaInfo()
		{
		}

		// Token: 0x04000E39 RID: 3641
		internal string _name;

		// Token: 0x04000E3A RID: 3642
		internal string _typename;

		// Token: 0x04000E3B RID: 3643
		internal Type _type;

		// Token: 0x04000E3C RID: 3644
		internal ODBC32.SQL_TYPE? _dbtype;

		// Token: 0x04000E3D RID: 3645
		internal object _scale;

		// Token: 0x04000E3E RID: 3646
		internal object _precision;

		// Token: 0x04000E3F RID: 3647
		internal int _columnlength;

		// Token: 0x04000E40 RID: 3648
		internal int _valueOffset;

		// Token: 0x04000E41 RID: 3649
		internal int _lengthOffset;

		// Token: 0x04000E42 RID: 3650
		internal ODBC32.SQL_C _sqlctype;

		// Token: 0x04000E43 RID: 3651
		internal ODBC32.SQL_TYPE _sql_type;
	}
}
