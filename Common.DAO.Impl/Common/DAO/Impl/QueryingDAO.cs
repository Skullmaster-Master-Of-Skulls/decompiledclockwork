using System;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.Impl
{
	// Token: 0x0200001B RID: 27
	public class QueryingDAO : IQueryingDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000058CC File Offset: 0x00003ACC
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x000058D4 File Offset: 0x00003AD4
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x060000B1 RID: 177 RVA: 0x000058DD File Offset: 0x00003ADD
		public QueryingDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x0000590E File Offset: 0x00003B0E
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00005916 File Offset: 0x00003B16
		public OperationContext OpContext { get; set; }

		// Token: 0x060000B4 RID: 180 RVA: 0x00005920 File Offset: 0x00003B20
		public DataTable ExecuteQuery(string query)
		{
			return this.DatabaseManager.ExecuteQuery(query);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00005940 File Offset: 0x00003B40
		public int ExecuteNonQuery(string query)
		{
			return this.DatabaseManager.ExecuteNonQuery(query);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00005960 File Offset: 0x00003B60
		public DataTable ExecuteQuery(string query, DbParameter[] parameters)
		{
			return this.DatabaseManager.ExecuteQuery(query, parameters);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00005980 File Offset: 0x00003B80
		public int ExecuteNonQuery(string query, DbParameter[] parameters)
		{
			return this.DatabaseManager.ExecuteNonQuery(query, parameters);
		}
	}
}
