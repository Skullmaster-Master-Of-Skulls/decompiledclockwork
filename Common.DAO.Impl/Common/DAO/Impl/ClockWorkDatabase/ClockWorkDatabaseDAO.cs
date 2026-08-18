using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Databases;
using TechnoPro.Common.DAO.ClockWorkDatabase;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.Impl.ClockWorkDatabase
{
	// Token: 0x02000111 RID: 273
	public class ClockWorkDatabaseDAO : IClockWorkDatabaseDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060007D4 RID: 2004 RVA: 0x000512C0 File Offset: 0x0004F4C0
		public ClockWorkDatabaseDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x000512D2 File Offset: 0x0004F4D2
		// (set) Token: 0x060007D6 RID: 2006 RVA: 0x000512DA File Offset: 0x0004F4DA
		public OperationContext OpContext { get; set; }

		// Token: 0x060007D7 RID: 2007 RVA: 0x000512E4 File Offset: 0x0004F4E4
		public bool DoesTableExist(string TableName)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@tablename", DbType.String, TableName)
			};
			bool result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT * FROM information_schema.tables WHERE TABLE_NAME=@tablename", parameters))
			{
				result = (dataReader != null && dataReader.Read());
			}
			return result;
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0005135C File Offset: 0x0004F55C
		public string[] LoadAllTableNames()
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			string[] result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select TABLE_NAME from INFORMATION_SCHEMA.TABLES\r\nwhere TABLE_TYPE = 'BASE TABLE'\r\nORDER BY TABLE_NAME"))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<string> list = new List<string>();
					while (dataReader.Read())
					{
						list.Add(dataReader["TABLE_NAME"].ToString().Trim());
					}
					result = (from g in list
					where g.Length > 0
					select g).ToArray<string>();
				}
			}
			return result;
		}
	}
}
