using System;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.DataSync;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.DataSync
{
	// Token: 0x0200006E RID: 110
	public class DataSyncCourseClientManager : IDataSyncCourse, IService, IDataSyncCourseClientManager, IWebService
	{
		// Token: 0x0600040B RID: 1035 RVA: 0x00012310 File Offset: 0x00010510
		public DataSyncCoursesResp DataSyncCourses(DataSyncCoursesReq request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<DataSyncCoursesReq>(request);
			ApplicationContext applicationContext = request.ApplicationContext ?? ObjectFactory.Resolve<ApplicationContext>();
			request.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IDataSyncCourse>(true, false).DataSyncCourses(request);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00012360 File Offset: 0x00010560
		public ParseExternalCourseRowPartsResp ParseExternalCourseRowParts(ParseExternalCourseRowPartsReq request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<ParseExternalCourseRowPartsReq>(request);
			ApplicationContext applicationContext = request.ApplicationContext ?? ObjectFactory.Resolve<ApplicationContext>();
			request.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IDataSyncCourse>(true, false).ParseExternalCourseRowParts(request);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x000123B0 File Offset: 0x000105B0
		public DataSyncLookupCoursesByTableResp DataSyncLookupCoursesByTable(DataSyncLookupCoursesByTableReq request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<DataSyncLookupCoursesByTableReq>(request);
			ApplicationContext applicationContext = request.ApplicationContext ?? ObjectFactory.Resolve<ApplicationContext>();
			request.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IDataSyncCourse>(true, false).DataSyncLookupCoursesByTable(request);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00012400 File Offset: 0x00010600
		public DataSyncLookupCoursesResp DataSyncLookupCourses(DataSyncLookupCoursesReq request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<DataSyncLookupCoursesReq>(request);
			ApplicationContext applicationContext = request.ApplicationContext ?? ObjectFactory.Resolve<ApplicationContext>();
			request.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IDataSyncCourse>(true, false).DataSyncLookupCourses(request);
		}
	}
}
