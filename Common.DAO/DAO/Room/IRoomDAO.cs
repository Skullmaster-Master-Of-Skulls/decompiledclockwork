using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Room;

namespace TechnoPro.Common.DAO.Room
{
	// Token: 0x02000039 RID: 57
	public interface IRoomDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060000F3 RID: 243
		IList<Seat> LoadAllSeats();
	}
}
