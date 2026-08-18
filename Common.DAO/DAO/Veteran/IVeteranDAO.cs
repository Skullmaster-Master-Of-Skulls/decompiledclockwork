using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Veteran;

namespace TechnoPro.Common.DAO.Veteran
{
	// Token: 0x02000016 RID: 22
	public interface IVeteranDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000034 RID: 52
		IList<ChangeInBenefitRequest> LoadBenefitRequests(int PersonId, DateTime StartDate, DateTime EndDate, int ChangeInBenefitRequestScreenNum, int DropListStatusCid);
	}
}
