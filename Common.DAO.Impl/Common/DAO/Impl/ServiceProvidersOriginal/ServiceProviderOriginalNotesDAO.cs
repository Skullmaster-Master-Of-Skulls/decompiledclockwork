using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO.ServiceProvidersOriginal;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal.NotesUpload;

namespace TechnoPro.Common.DAO.Impl.ServiceProvidersOriginal
{
	// Token: 0x02000063 RID: 99
	public class ServiceProviderOriginalNotesDAO : IServiceProviderOriginalNotesDAO, IBaseOperationContext<ServiceProvidersOperationContext>
	{
		// Token: 0x06000282 RID: 642 RVA: 0x00015EC9 File Offset: 0x000140C9
		public ServiceProviderOriginalNotesDAO(ServiceProvidersOperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000283 RID: 643 RVA: 0x00015EDB File Offset: 0x000140DB
		// (set) Token: 0x06000284 RID: 644 RVA: 0x00015EE3 File Offset: 0x000140E3
		public ServiceProvidersOperationContext OpContext { get; set; }

		// Token: 0x06000285 RID: 645 RVA: 0x00003998 File Offset: 0x00001B98
		public IList<ServiceProviderWithCourseUploadInfoList> LoadServiceProvidersWithCourseUploadInfosWithNoUploadsOrEmailNoticesInDayCount(int DayCount, params int[] ServiceProviderTypeIds)
		{
			throw new NotImplementedException();
		}
	}
}
