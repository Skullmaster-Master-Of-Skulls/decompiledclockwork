using System;
using System.Collections.Generic;
using System.Data;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.UnivDataAccess;

namespace TechnoPro.Common.ClientManager.ICore.UnivThroughServer
{
	// Token: 0x02000009 RID: 9
	public interface IUnivThroughServerClientManager : IWebService
	{
		// Token: 0x06000026 RID: 38
		int Fill(ref DataTable t, string sqlCommandText, List<CommonParameter> parameters);

		// Token: 0x06000027 RID: 39
		int FillReturnIdentity(ref DataTable t, string tableName, string AutoIncrementColName, string sqlCommandText, List<CommonParameter> parameters);

		// Token: 0x06000028 RID: 40
		int Fill(ref DataSet ds, string tableName, string sqlCommandText, List<CommonParameter> parameters);

		// Token: 0x06000029 RID: 41
		int Update(ref DataTable t, string sqlCommandText, List<CommonParameter> parameters);

		// Token: 0x0600002A RID: 42
		int ExecuteNonQuery(string sqlCommandText, List<CommonParameter> parameters);

		// Token: 0x0600002B RID: 43
		int ExecuteScalar(string sqlCommandText, List<CommonParameter> parameters);

		// Token: 0x0600002C RID: 44
		IDataReader ExecuteReader(string sqlCommandText, List<CommonParameter> parameters);
	}
}
