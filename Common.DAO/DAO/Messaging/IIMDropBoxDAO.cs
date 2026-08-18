using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DropBox;

namespace TechnoPro.Common.DAO.Messaging
{
	// Token: 0x0200004B RID: 75
	public interface IIMDropBoxDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060001A6 RID: 422
		void Save(DropBox_IM item);

		// Token: 0x060001A7 RID: 423
		IList<DropBox_IM> GetAllIMs(string username);

		// Token: 0x060001A8 RID: 424
		DropBox_IM GetIM(int id);

		// Token: 0x060001A9 RID: 425
		void Delete(int id);

		// Token: 0x060001AA RID: 426
		int CountIMs(string username);
	}
}
