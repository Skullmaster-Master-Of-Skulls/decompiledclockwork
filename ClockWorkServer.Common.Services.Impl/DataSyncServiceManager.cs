using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.Common.Core.DataSync;
using TechnoPro.Common.Core.Mappers.DataSync;
using TechnoPro.Common.Core.Mappers.DataSync.Notetaking;
using TechnoPro.Common.Core.Mappers.DataSync.Student;
using TechnoPro.Common.ICore.DataSync;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.DataSync;
using TechnoPro.Common.Public.Entities.DataSync.Notetaking;
using TechnoPro.Common.Public.Entities.DataSync.Student;
using TechnoPro.Common.Public.Entities.OperationContexts;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000037 RID: 55
	public class DataSyncServiceManager : IDataSync, IService
	{
		// Token: 0x06000223 RID: 547 RVA: 0x0000ABA4 File Offset: 0x00008DA4
		public LoadDataSyncInfoResp LoadDataSyncInfo(LoadDataSyncInfoReq Request)
		{
			IDataSyncInfoManager dataSyncInfoManager = new DataSyncInfoManager(Request.GetOperationContext());
			return new LoadDataSyncInfoResp
			{
				DataSyncInfo = dataSyncInfoManager.LoadDataSyncInfo().ToDTO()
			};
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000ABDC File Offset: 0x00008DDC
		public RunFullDataSyncForExistingStudentResp RunFullDataSyncForExistingStudent(RunFullDataSyncForExistingStudentReq Request)
		{
			IDataSyncManager dataSyncManager = new DataSyncManager(Request.GetOperationContext<DataSyncOperationContext>());
			DataSyncResult dataSyncResult = dataSyncManager.RunFullDataSyncForExistingStudent(Request.Student_no, Request.DontSyncData, Request.DontSyncCourses);
			return new RunFullDataSyncForExistingStudentResp
			{
				Result = ((dataSyncResult == null) ? null : dataSyncResult.ToDTO())
			};
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000AC2C File Offset: 0x00008E2C
		public PreviewDataSyncDataResp PreviewDataSyncData(PreviewDataSyncDataReq Request)
		{
			IDataSyncManager dataSyncManager = new DataSyncManager(Request.GetOperationContext<DataSyncOperationContext>());
			DataSyncPreviewResult dataSyncPreviewResult = dataSyncManager.PreviewDataSyncData(Request.Student_no);
			return new PreviewDataSyncDataResp
			{
				Result = ((dataSyncPreviewResult == null) ? null : dataSyncPreviewResult.ToDTO())
			};
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000AC70 File Offset: 0x00008E70
		public RunCourseDataSyncByStudentNumberResp RunCourseDataSyncByStudentNumber(RunCourseDataSyncByStudentNumberReq Request)
		{
			IDataSyncManager dataSyncManager = new DataSyncManager(Request.GetOperationContext<DataSyncOperationContext>());
			DataSyncResult dataSyncResult = dataSyncManager.RunCourseDataSyncByStudentNumber(Request.Student_no);
			return new RunCourseDataSyncByStudentNumberResp
			{
				Result = ((dataSyncResult == null) ? null : dataSyncResult.ToDTO())
			};
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000ACB4 File Offset: 0x00008EB4
		public RunCourseDataSyncByIdResp RunCourseDataSyncById(RunCourseDataSyncByIdReq Request)
		{
			IDataSyncManager dataSyncManager = new DataSyncManager(Request.GetOperationContext<DataSyncOperationContext>());
			DataSyncResult dataSyncResult = dataSyncManager.RunCourseDataSyncById(Request.PersonId);
			return new RunCourseDataSyncByIdResp
			{
				Result = ((dataSyncResult == null) ? null : dataSyncResult.ToDTO())
			};
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000ACF8 File Offset: 0x00008EF8
		public GetNotetakerPreviewExternalCoursesByUserNameResp GetNotetakerPreviewExternalCoursesByUserName(GetNotetakerPreviewExternalCoursesByUserNameReq Request)
		{
			IDataSyncManager dataSyncManager = new DataSyncManager(Request.GetOperationContext<DataSyncOperationContext>());
			IList<DataSyncExternalCourse> notetakerPreviewExternalCoursesByUserName = dataSyncManager.GetNotetakerPreviewExternalCoursesByUserName(Request.UserName);
			GetNotetakerPreviewExternalCoursesByUserNameResp getNotetakerPreviewExternalCoursesByUserNameResp = new GetNotetakerPreviewExternalCoursesByUserNameResp();
			IList<DataSyncExternalCourseDTO> externalCourses;
			if (notetakerPreviewExternalCoursesByUserName != null)
			{
				externalCourses = notetakerPreviewExternalCoursesByUserName.ToList<DataSyncExternalCourse>().ConvertAll<DataSyncExternalCourseDTO>((DataSyncExternalCourse g) => g.ToDTO());
			}
			else
			{
				externalCourses = null;
			}
			getNotetakerPreviewExternalCoursesByUserNameResp.ExternalCourses = externalCourses;
			return getNotetakerPreviewExternalCoursesByUserNameResp;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000AD60 File Offset: 0x00008F60
		public GetNotetakerPreviewExternalCoursesByStudentNumberResp GetNotetakerPreviewExternalCoursesByStudentNumber(GetNotetakerPreviewExternalCoursesByStudentNumberReq Request)
		{
			IDataSyncManager dataSyncManager = new DataSyncManager(Request.GetOperationContext<DataSyncOperationContext>());
			IList<DataSyncExternalCourse> notetakerPreviewExternalCoursesByStudentNumber = dataSyncManager.GetNotetakerPreviewExternalCoursesByStudentNumber(Request.StudentNumber);
			GetNotetakerPreviewExternalCoursesByStudentNumberResp getNotetakerPreviewExternalCoursesByStudentNumberResp = new GetNotetakerPreviewExternalCoursesByStudentNumberResp();
			IList<DataSyncExternalCourseDTO> externalCourses;
			if (notetakerPreviewExternalCoursesByStudentNumber != null)
			{
				externalCourses = notetakerPreviewExternalCoursesByStudentNumber.ToList<DataSyncExternalCourse>().ConvertAll<DataSyncExternalCourseDTO>((DataSyncExternalCourse g) => g.ToDTO());
			}
			else
			{
				externalCourses = null;
			}
			getNotetakerPreviewExternalCoursesByStudentNumberResp.ExternalCourses = externalCourses;
			return getNotetakerPreviewExternalCoursesByStudentNumberResp;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000ADC8 File Offset: 0x00008FC8
		public GetNotetakerPreviewDataByStudentNumberResp GetNotetakerPreviewDataByStudentNumber(GetNotetakerPreviewDataByStudentNumberReq Request)
		{
			IDataSyncManager dataSyncManager = new DataSyncManager(Request.GetOperationContext<DataSyncOperationContext>());
			NotetakerWithExternalCourses notetakerPreviewDataByStudentNumber = dataSyncManager.GetNotetakerPreviewDataByStudentNumber(Request.UserName, Request.StudentNumber);
			return new GetNotetakerPreviewDataByStudentNumberResp
			{
				NotetakerWithExternalCourses = ((notetakerPreviewDataByStudentNumber == null) ? null : notetakerPreviewDataByStudentNumber.ToDTO())
			};
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000AE14 File Offset: 0x00009014
		public GetNotetakerPreviewDataResp GetNotetakerPreviewData(GetNotetakerPreviewDataReq Request)
		{
			IDataSyncManager dataSyncManager = new DataSyncManager(Request.GetOperationContext<DataSyncOperationContext>());
			NotetakerWithExternalCourses notetakerPreviewData = dataSyncManager.GetNotetakerPreviewData(Request.UserName);
			bool flag = notetakerPreviewData == null;
			GetNotetakerPreviewDataResp result;
			if (flag)
			{
				result = new GetNotetakerPreviewDataResp
				{
					NotetakerWithExternalCourses = null
				};
			}
			else
			{
				result = new GetNotetakerPreviewDataResp
				{
					NotetakerWithExternalCourses = notetakerPreviewData.ToDTO()
				};
			}
			return result;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000AE6C File Offset: 0x0000906C
		public GetStudentPreviewDataByStudentNumberOrUsernameResp GetStudentPreviewDataByStudentNumberOrUsername(GetStudentPreviewDataByStudentNumberOrUsernameReq Request)
		{
			IDataSyncManager dataSyncManager = new DataSyncManager(Request.GetOperationContext<DataSyncOperationContext>());
			StudentDataSyncPreviewData studentPreviewDataByStudentNumberOrUsername = dataSyncManager.GetStudentPreviewDataByStudentNumberOrUsername(Request.UserName, Request.StudentNumber);
			bool flag = studentPreviewDataByStudentNumberOrUsername == null;
			GetStudentPreviewDataByStudentNumberOrUsernameResp result;
			if (flag)
			{
				result = new GetStudentPreviewDataByStudentNumberOrUsernameResp
				{
					DataSyncPreviewData = null
				};
			}
			else
			{
				result = new GetStudentPreviewDataByStudentNumberOrUsernameResp
				{
					DataSyncPreviewData = studentPreviewDataByStudentNumberOrUsername.ToDTO()
				};
			}
			return result;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000AEC8 File Offset: 0x000090C8
		public LoadCustomTableNamesResp LoadCustomTableNames(LoadCustomTableNamesReq Request)
		{
			IDataSyncManager dataSyncManager = new DataSyncManager(Request.GetOperationContext<DataSyncOperationContext>());
			return new LoadCustomTableNamesResp
			{
				TableNames = dataSyncManager.LoadCustomTableNames()
			};
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000AEF8 File Offset: 0x000090F8
		public LoadCustomExternalColumnNamesResp LoadCustomExternalColumnNames(LoadCustomExternalColumnNamesReq Request)
		{
			IDataSyncManager dataSyncManager = new DataSyncManager(Request.GetOperationContext<DataSyncOperationContext>());
			return new LoadCustomExternalColumnNamesResp
			{
				ExternalColumnNames = dataSyncManager.LoadCustomExternalColumnNames(Request.ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn)
			};
		}
	}
}
