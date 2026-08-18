using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.Updates;

namespace TechnoPro.Common.DAO.Updates
{
	// Token: 0x0200001C RID: 28
	public interface IExecuteUpdateDAO
	{
		// Token: 0x06000057 RID: 87
		IList<ExecuteUpdatesResp> ExecuteUpdates();
	}
}
