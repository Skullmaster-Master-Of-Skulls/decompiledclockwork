using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.DataSync
{
	// Token: 0x02000067 RID: 103
	public interface IDataSyncCourseClientManager : IWebService
	{
		// Token: 0x0600030F RID: 783
		ParseExternalCourseRowPartsResp ParseExternalCourseRowParts(ParseExternalCourseRowPartsReq request);

		// Token: 0x06000310 RID: 784
		DataSyncCoursesResp DataSyncCourses(DataSyncCoursesReq request);

		// Token: 0x06000311 RID: 785
		DataSyncLookupCoursesByTableResp DataSyncLookupCoursesByTable(DataSyncLookupCoursesByTableReq request);

		// Token: 0x06000312 RID: 786
		DataSyncLookupCoursesResp DataSyncLookupCourses(DataSyncLookupCoursesReq request);
	}
}
