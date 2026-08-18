using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Databases;
using TechnoPro.Common.DAO.DynamicQueries;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.UnivDataAccess;

namespace TechnoPro.Common.DAO.Impl.DynamicQueries
{
	// Token: 0x020000D7 RID: 215
	public class DynamicQueryDAO : IDynamicQueryDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x00036A24 File Offset: 0x00034C24
		// (set) Token: 0x060005CE RID: 1486 RVA: 0x00036A2C File Offset: 0x00034C2C
		public OperationContext OpContext { get; set; }

		// Token: 0x060005CF RID: 1487 RVA: 0x00036A35 File Offset: 0x00034C35
		public DynamicQueryDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00036A68 File Offset: 0x00034C68
		public int? LoadInt(string sql)
		{
			IList<int> list = this.LoadIntList(sql);
			return (list == null || list.Count < 1) ? null : new int?(list[0]);
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00036AA8 File Offset: 0x00034CA8
		public IList<int> LoadIntList(string sql)
		{
			IList<int> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader(sql))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<int> list = new List<int>();
					bool flag2 = dataReader.GetFieldType(0) == typeof(int);
					while (dataReader.Read())
					{
						bool flag3 = !(dataReader[0] is DBNull);
						if (flag3)
						{
							bool flag4 = flag2;
							int item;
							if (flag4)
							{
								item = (int)dataReader[0];
							}
							else
							{
								int.TryParse(dataReader[0].ToString(), out item);
							}
							bool flag5 = !list.Contains(item);
							if (flag5)
							{
								list.Add(item);
							}
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00036B84 File Offset: 0x00034D84
		[DebuggerStepThrough]
		public Task<IList<int>> LoadIntListAsync(string sql)
		{
			DynamicQueryDAO.<LoadIntListAsync>d__8 <LoadIntListAsync>d__ = new DynamicQueryDAO.<LoadIntListAsync>d__8();
			<LoadIntListAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<int>>.Create();
			<LoadIntListAsync>d__.<>4__this = this;
			<LoadIntListAsync>d__.sql = sql;
			<LoadIntListAsync>d__.<>1__state = -1;
			<LoadIntListAsync>d__.<>t__builder.Start<DynamicQueryDAO.<LoadIntListAsync>d__8>(ref <LoadIntListAsync>d__);
			return <LoadIntListAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00036BD0 File Offset: 0x00034DD0
		public QueryResult ExecuteQuery(QueryRequest request)
		{
			bool flag = request.Parameters == null || request.Parameters.Count < 1;
			DataTable dataTable;
			if (flag)
			{
				dataTable = this.DatabaseManager.ExecuteQuery(request.Sql);
			}
			else
			{
				DbParameter[] parameters = (from g in request.Parameters
				select this.DatabaseManager.GetParameter(g.Name, (g.DbType != null) ? g.DbType.Value : DbType.String, g.Value)).ToArray<DbParameter>();
				dataTable = this.DatabaseManager.ExecuteQuery(request.Sql, parameters);
			}
			return new QueryResult
			{
				DataTable = dataTable
			};
		}

		// Token: 0x040002FA RID: 762
		private DatabaseLayer DatabaseManager;
	}
}
