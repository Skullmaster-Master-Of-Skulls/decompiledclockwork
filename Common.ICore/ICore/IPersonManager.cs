using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.ICore
{
	// Token: 0x0200000B RID: 11
	public interface IPersonManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600004A RID: 74
		List<Person> GetPersonsByGroup(int groupid);
	}
}
