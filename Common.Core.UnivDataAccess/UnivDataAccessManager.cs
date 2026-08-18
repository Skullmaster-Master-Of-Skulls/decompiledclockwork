using System;
using System.Data.SqlClient;
using TechnoPro.Common.DAO.UnivDataAccess.Impl;
using TechnoPro.Common.ICore.UnivDataAccess;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.UnivDataAccess;
using TechnoPro.Common.Unity.IoC;
using UnivOleDb;

namespace TechnoPro.Common.Core.UnivDataAccess
{
	// Token: 0x02000002 RID: 2
	public class UnivDataAccessManager : IUnivDataAccessManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public UnivDataAccessManager()
		{
			this.dao = new UnivDataAccessDAO(this.OpContext);
			object obj = ObjectFactory.Resolve<ICacheStorageManager>()["da"];
			if (obj != null)
			{
				this.dao.Da = (UnivDataAdapter)obj;
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002098 File Offset: 0x00000298
		public UnivDataAccessManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new UnivDataAccessDAO(opContext);
			object obj = ObjectFactory.Resolve<ICacheStorageManager>()["da"];
			if (obj != null)
			{
				this.dao.Da = (UnivDataAdapter)obj;
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020E2 File Offset: 0x000002E2
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020EA File Offset: 0x000002EA
		public OperationContext OpContext { get; set; }

		// Token: 0x06000005 RID: 5 RVA: 0x000020F3 File Offset: 0x000002F3
		public QueryResult Fill(QueryRequest Query)
		{
			return this.dao.Fill(Query);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002101 File Offset: 0x00000301
		public bool DoesTableExist(string tableName)
		{
			return this.dao.DoesTableExist(tableName);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000210F File Offset: 0x0000030F
		public bool DoesColumnExist(string tableName, string colName)
		{
			return this.dao.DoesColumnExist(tableName, colName);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000211E File Offset: 0x0000031E
		public string GetSQLCommandParametersFilledIn(QueryRequest Query)
		{
			return this.dao.GetSQLCommandParametersFilledIn(Query);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000212C File Offset: 0x0000032C
		public QueryResult FillReturnIdentity(QueryRequest Query, string autoIncrementColName, string tableName)
		{
			return this.dao.FillReturnIdentity(Query, autoIncrementColName, tableName);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000213C File Offset: 0x0000033C
		public QueryResult ExecuteScalar(QueryRequest Query)
		{
			return this.dao.ExecuteScalar(Query);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000214A File Offset: 0x0000034A
		public QueryResult ExecuteNonQuery(QueryRequest Query)
		{
			return this.dao.ExecuteNonQuery(Query);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002158 File Offset: 0x00000358
		public SqlDataReader ExecuteReader(QueryRequest Query)
		{
			return this.dao.ExecuteReader(Query);
		}

		// Token: 0x04000001 RID: 1
		private UnivDataAccessDAO dao;
	}
}
