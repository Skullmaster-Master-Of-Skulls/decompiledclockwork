using System;
using System.Data;
using System.Data.Common;
using TechnoPro.Common.DAO;
using TechnoPro.Common.ICore;

namespace TechnoPro.Common.Core
{
	// Token: 0x02000022 RID: 34
	public class QueryingManager : IQueryingManager
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000113 RID: 275 RVA: 0x0000671A File Offset: 0x0000491A
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00006722 File Offset: 0x00004922
		public IQueryingDAO QueryingDAO { get; private set; }

		// Token: 0x06000116 RID: 278 RVA: 0x00006738 File Offset: 0x00004938
		public DataTable ExecuteQuery(string query)
		{
			return this.QueryingDAO.ExecuteQuery(query);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00006758 File Offset: 0x00004958
		public int ExecuteNonQuery(string query)
		{
			return this.QueryingDAO.ExecuteNonQuery(query);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00006778 File Offset: 0x00004978
		public DataTable ExecuteQuery(string query, DbParameter[] parameters)
		{
			return this.QueryingDAO.ExecuteQuery(query, parameters);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00006798 File Offset: 0x00004998
		public int ExecuteNonQuery(string query, DbParameter[] parameters)
		{
			return this.QueryingDAO.ExecuteNonQuery(query, parameters);
		}
	}
}
