using System;
using System.Collections.Generic;
using System.Data;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestBookingViews.ViewEntities;

namespace TechnoPro.Common.DAO.AppointmentsTestBooking
{
	// Token: 0x020000BC RID: 188
	public interface ITestExamBookingViewDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600050A RID: 1290
		IList<TestBookingFull> LoadTestsFull(DateTime? StartDate, DateTime? EndDate, bool HideCancelled, int counsellorCid);

		// Token: 0x0600050B RID: 1291
		IList<TestBookingSmall> LoadTestsSmall(DateTime? StartDate, DateTime? EndDate, bool HideCancelled, int counsellorCid);

		// Token: 0x0600050C RID: 1292
		IList<ClassTestDefinitionSmall> LoadClassTestDefinitionsSmall(DateTime? StartDate, DateTime? EndDate);

		// Token: 0x0600050D RID: 1293
		IList<ClassTestDefinitionSmall> LoadClassTestDefinitionsSmallWithExtendedInfo(DateTime? StartDate, DateTime? EndDate, params int[] controlIds);

		// Token: 0x0600050E RID: 1294
		IList<UnbookedStudentsSmall> LoadUnbookedStudentsSmall(bool onlyShowLetterIssued);

		// Token: 0x0600050F RID: 1295
		IList<TestBookingFull> LoadTestsFull(IDataReader reader);

		// Token: 0x06000510 RID: 1296
		IList<TestBookingSmall> LoadTestsSmall(IDataReader reader);

		// Token: 0x06000511 RID: 1297
		IList<ClassTestDefinitionSmall> LoadClassTestDefinitionsSmall(IDataReader reader);

		// Token: 0x06000512 RID: 1298
		IList<UnbookedStudentsSmall> LoadUnbookedStudentsSmall(IDataReader reader);

		// Token: 0x06000513 RID: 1299
		TestBookingFull LoadTestFullByAppId(int appId, int counsellorId);

		// Token: 0x06000514 RID: 1300
		TestBookingSmall LoadTestSmallByAppId(int appId, int counsellorId);

		// Token: 0x06000515 RID: 1301
		ClassTestDefinitionSmall LoadClassTestDefinitionSmallByExamId(int examId);

		// Token: 0x06000516 RID: 1302
		ClassTestDefinitionSmall LoadClassTestDefinitionSmallByExamIdWithExtendedInfo(int examId, params int[] controlIds);

		// Token: 0x06000517 RID: 1303
		IList<UnbookedTestExamStudent> LoadUnbookedTestExamStudents(bool onlyShowLetterIssued, bool ignoreTemplate);
	}
}
