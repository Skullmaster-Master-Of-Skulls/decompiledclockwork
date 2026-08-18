using System;
using System.Data;

namespace TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions
{
	// Token: 0x0200024D RID: 589
	[Serializable]
	public enum eOracleQueryType
	{
		// Token: 0x04000FC3 RID: 4035
		[OracleQueryType(CommandType.Text)]
		Query,
		// Token: 0x04000FC4 RID: 4036
		[OracleQueryType(CommandType.StoredProcedure)]
		StoredProcedure,
		// Token: 0x04000FC5 RID: 4037
		[OracleQueryType(CommandType.Text)]
		Function
	}
}
