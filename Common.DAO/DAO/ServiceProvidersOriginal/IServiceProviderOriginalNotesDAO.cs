using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal.NotesUpload;

namespace TechnoPro.Common.DAO.ServiceProvidersOriginal
{
	// Token: 0x02000038 RID: 56
	public interface IServiceProviderOriginalNotesDAO : IBaseOperationContext<ServiceProvidersOperationContext>
	{
		// Token: 0x060000F2 RID: 242
		IList<ServiceProviderWithCourseUploadInfoList> LoadServiceProvidersWithCourseUploadInfosWithNoUploadsOrEmailNoticesInDayCount(int DayCount, params int[] ServiceProviderTypeIds);
	}
}
