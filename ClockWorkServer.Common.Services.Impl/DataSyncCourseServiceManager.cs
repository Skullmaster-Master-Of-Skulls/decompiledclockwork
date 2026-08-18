using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.Common.Core.DataSync;
using TechnoPro.Common.Core.Mappers.DataSync;
using TechnoPro.Common.ICore.DataSync;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.DataSync;
using TechnoPro.Common.Public.Entities.OperationContexts;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000036 RID: 54
	public class DataSyncCourseServiceManager : IDataSyncCourse, IService
	{
		// Token: 0x0600021E RID: 542 RVA: 0x0000A97C File Offset: 0x00008B7C
		public ParseExternalCourseRowPartsResp ParseExternalCourseRowParts(ParseExternalCourseRowPartsReq Request)
		{
			IDataSyncCourseManager dataSyncCourseManager = new DataSyncCourseManager(Request.GetOperationContext<DataSyncOperationContext>());
			List<DataSyncExternalCourse> list = dataSyncCourseManager.ParseExternalCourseRowParts(Request.ExternalCourseRowParts.ConvertAll<DataSyncExternalCourseRowPart>((DataSyncExternalCourseRowPartDTO f) => f.ToDomainObject()));
			List<DataSyncExternalCourseDTO> list2;
			if (list != null)
			{
				list2 = (from f in list
				select f.ToDTO()).ToList<DataSyncExternalCourseDTO>();
			}
			else
			{
				list2 = null;
			}
			List<DataSyncExternalCourseDTO> externalCourses = list2;
			return new ParseExternalCourseRowPartsResp
			{
				ExternalCourses = externalCourses
			};
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000AA0C File Offset: 0x00008C0C
		public DataSyncCoursesResp DataSyncCourses(DataSyncCoursesReq Request)
		{
			IDataSyncCourseManager dataSyncCourseManager = new DataSyncCourseManager(Request.GetOperationContext<DataSyncOperationContext>());
			IDataSyncCourseManager dataSyncCourseManager2 = dataSyncCourseManager;
			string studentNumber = Request.StudentNumber;
			List<DataSyncExternalCourseDTO> externalCourses = Request.ExternalCourses;
			List<DataSyncExternalCourse> allExternalCourses;
			if (externalCourses == null)
			{
				allExternalCourses = null;
			}
			else
			{
				allExternalCourses = externalCourses.ConvertAll<DataSyncExternalCourse>((DataSyncExternalCourseDTO f) => (f != null) ? f.ToDomainObject() : null);
			}
			List<DataSyncExternalCourseSyncResult> list = dataSyncCourseManager2.DataSyncCourses(studentNumber, allExternalCourses);
			DataSyncCoursesResp dataSyncCoursesResp = new DataSyncCoursesResp();
			IList<DataSyncExternalCourseSyncResultDTO> results;
			if (list == null)
			{
				results = null;
			}
			else
			{
				results = (from f in list
				select (f != null) ? f.ToDTO() : null).ToList<DataSyncExternalCourseSyncResultDTO>();
			}
			dataSyncCoursesResp.Results = results;
			return dataSyncCoursesResp;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000AAA4 File Offset: 0x00008CA4
		public DataSyncLookupCoursesByTableResp DataSyncLookupCoursesByTable(DataSyncLookupCoursesByTableReq request)
		{
			IDataSyncCourseManager dataSyncCourseManager = new DataSyncCourseManager(request.GetOperationContext<DataSyncOperationContext>());
			IList<DataSyncExternalCourseSyncResult> list = dataSyncCourseManager.DataSyncLookupCourses(request.Table);
			DataSyncLookupCoursesByTableResp dataSyncLookupCoursesByTableResp = new DataSyncLookupCoursesByTableResp();
			IList<DataSyncExternalCourseSyncResultDTO> results;
			if (list == null)
			{
				results = null;
			}
			else
			{
				results = (from f in list
				select (f != null) ? f.ToDTO() : null).ToList<DataSyncExternalCourseSyncResultDTO>();
			}
			dataSyncLookupCoursesByTableResp.Results = results;
			return dataSyncLookupCoursesByTableResp;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000AB0C File Offset: 0x00008D0C
		public DataSyncLookupCoursesResp DataSyncLookupCourses(DataSyncLookupCoursesReq request)
		{
			IDataSyncCourseManager dataSyncCourseManager = new DataSyncCourseManager(request.GetOperationContext<DataSyncOperationContext>());
			IDataSyncCourseManager dataSyncCourseManager2 = dataSyncCourseManager;
			IList<DataSyncExternalCourseDTO> allExternalCourses = request.AllExternalCourses;
			IList<DataSyncExternalCourse> allExternalCourses2;
			if (allExternalCourses == null)
			{
				allExternalCourses2 = null;
			}
			else
			{
				allExternalCourses2 = (from f in allExternalCourses
				select (f != null) ? f.ToDomainObject() : null).ToList<DataSyncExternalCourse>();
			}
			IList<DataSyncExternalCourseSyncResult> list = dataSyncCourseManager2.DataSyncLookupCourses(allExternalCourses2);
			DataSyncLookupCoursesResp dataSyncLookupCoursesResp = new DataSyncLookupCoursesResp();
			IList<DataSyncExternalCourseSyncResultDTO> results;
			if (list == null)
			{
				results = null;
			}
			else
			{
				results = (from f in list
				select (f != null) ? f.ToDTO() : null).ToList<DataSyncExternalCourseSyncResultDTO>();
			}
			dataSyncLookupCoursesResp.Results = results;
			return dataSyncLookupCoursesResp;
		}
	}
}
