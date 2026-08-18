using System;
using System.Data.Common;
using System.Data.SqlClient;
using TechnoPro.ClockWorkServer.Contracts.DataContracts;

namespace TechnoPro.ClockWorkServer.Client.Services.Adapters
{
	// Token: 0x02000178 RID: 376
	public static class DbParameterAdapter
	{
		// Token: 0x06000E8A RID: 3722 RVA: 0x00026014 File Offset: 0x00024214
		public static CWDbParameter[] ConvertToCWDbParameter(this DbParameter[] cwDBParameters)
		{
			CWDbParameter[] array = new CWDbParameter[cwDBParameters.Length];
			for (int i = 0; i < cwDBParameters.Length; i++)
			{
				array[i] = (cwDBParameters[i] as SqlParameter).ConvertToCWDbParameter();
			}
			return array;
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x00026054 File Offset: 0x00024254
		public static CWDbParameter ConvertToCWDbParameter(this DbParameter sqlParameter)
		{
			return new CWDbParameter
			{
				ParameterName = sqlParameter.ParameterName,
				DbType = (CWDbType)sqlParameter.DbType,
				Value = sqlParameter.Value
			};
		}
	}
}
