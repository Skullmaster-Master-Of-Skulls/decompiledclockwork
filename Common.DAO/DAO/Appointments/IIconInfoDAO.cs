using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.DAO.Appointments
{
	// Token: 0x020000AD RID: 173
	public interface IIconInfoDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600049D RID: 1181
		IconInfo LoadIconInfo(int IconInfoId);

		// Token: 0x0600049E RID: 1182
		void DeleteIconInfo(int IconInfoId);

		// Token: 0x0600049F RID: 1183
		int InsertOrUpdateIconInfo(IconInfo IconInfo);

		// Token: 0x060004A0 RID: 1184
		IList<IconInfo> LoadAllIconInfos();
	}
}
