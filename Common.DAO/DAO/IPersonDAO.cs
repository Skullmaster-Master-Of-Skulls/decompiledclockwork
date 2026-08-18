using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO
{
	// Token: 0x0200000F RID: 15
	public interface IPersonDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000020 RID: 32
		List<Person> GetPersonsByGroup(int groupid);

		// Token: 0x06000021 RID: 33
		Person GetPerson(int personId);
	}
}
