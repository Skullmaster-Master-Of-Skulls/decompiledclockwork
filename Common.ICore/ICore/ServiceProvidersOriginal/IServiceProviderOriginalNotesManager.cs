using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal.NotesUpload;

namespace TechnoPro.Common.ICore.ServiceProvidersOriginal
{
	// Token: 0x02000047 RID: 71
	public interface IServiceProviderOriginalNotesManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060001CD RID: 461
		IList<ServiceProviderWithCourseUploadInfoList> LoadServiceProvidersWithCourseUploadInfosWithNoUploadsOrEmailNoticesInDayCount(int DayCount, params int[] ServiceProviderTypeIds);
	}
}
