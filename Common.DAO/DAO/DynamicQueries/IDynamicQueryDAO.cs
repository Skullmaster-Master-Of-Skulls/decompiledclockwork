using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.UnivDataAccess;

namespace TechnoPro.Common.DAO.DynamicQueries
{
	// Token: 0x02000021 RID: 33
	public interface IDynamicQueryDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000070 RID: 112
		int? LoadInt(string sql);

		// Token: 0x06000071 RID: 113
		IList<int> LoadIntList(string sql);

		// Token: 0x06000072 RID: 114
		Task<IList<int>> LoadIntListAsync(string sql);

		// Token: 0x06000073 RID: 115
		QueryResult ExecuteQuery(QueryRequest request);
	}
}
