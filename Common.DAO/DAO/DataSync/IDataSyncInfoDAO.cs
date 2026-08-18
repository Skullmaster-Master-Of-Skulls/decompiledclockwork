using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.DataSync
{
	// Token: 0x02000090 RID: 144
	public interface IDataSyncInfoDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060003BA RID: 954
		IList<DynamicData> LoadOnlineIntakeFormData(int ScreenNum, string StudentNumber, out PersonBase StudentInfo);

		// Token: 0x060003BB RID: 955
		void DataSyncIntakeData(int PersonId, string Student_No, int IntakeScreenNum, bool deleteIntakeEntry = true);
	}
}
