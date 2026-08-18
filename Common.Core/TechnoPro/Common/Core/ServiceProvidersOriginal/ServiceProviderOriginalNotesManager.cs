using System;
using System.Collections.Generic;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.DAO.Impl.ServiceProvidersOriginal;
using TechnoPro.Common.DAO.ServiceProvidersOriginal;
using TechnoPro.Common.ICore.ServiceProvidersOriginal;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal.NotesUpload;

namespace TechnoPro.Common.Core.ServiceProvidersOriginal
{
	// Token: 0x02000054 RID: 84
	public class ServiceProviderOriginalNotesManager : IServiceProviderOriginalNotesManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000371 RID: 881 RVA: 0x00011FB8 File Offset: 0x000101B8
		public ServiceProviderOriginalNotesManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00011FCA File Offset: 0x000101CA
		// (set) Token: 0x06000373 RID: 883 RVA: 0x00011FD2 File Offset: 0x000101D2
		public OperationContext OpContext { get; set; }

		// Token: 0x06000374 RID: 884 RVA: 0x00011FDC File Offset: 0x000101DC
		public IList<ServiceProviderWithCourseUploadInfoList> LoadServiceProvidersWithCourseUploadInfosWithNoUploadsOrEmailNoticesInDayCount(int DayCount, params int[] ServiceProviderTypeIds)
		{
			IServiceProviderOriginalNotesDAO serviceProviderOriginalNotesDAO = new ServiceProviderOriginalNotesDAO(this.OpContext.GetProviderTypes());
			return serviceProviderOriginalNotesDAO.LoadServiceProvidersWithCourseUploadInfosWithNoUploadsOrEmailNoticesInDayCount(DayCount, ServiceProviderTypeIds);
		}
	}
}
