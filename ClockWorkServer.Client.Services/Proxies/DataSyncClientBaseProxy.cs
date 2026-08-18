using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200007D RID: 125
	internal class DataSyncClientBaseProxy : ClientBase<IDataSync>, IDataSync, IService
	{
		// Token: 0x06000545 RID: 1349 RVA: 0x0000EC78 File Offset: 0x0000CE78
		public DataSyncClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0000EC83 File Offset: 0x0000CE83
		public DataSyncClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0000EC90 File Offset: 0x0000CE90
		public LoadDataSyncInfoResp LoadDataSyncInfo(LoadDataSyncInfoReq Request)
		{
			return base.Channel.LoadDataSyncInfo(Request);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0000ECB0 File Offset: 0x0000CEB0
		public PreviewDataSyncDataResp PreviewDataSyncData(PreviewDataSyncDataReq Request)
		{
			return base.Channel.PreviewDataSyncData(Request);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0000ECD0 File Offset: 0x0000CED0
		public RunFullDataSyncForExistingStudentResp RunFullDataSyncForExistingStudent(RunFullDataSyncForExistingStudentReq Request)
		{
			return base.Channel.RunFullDataSyncForExistingStudent(Request);
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0000ECF0 File Offset: 0x0000CEF0
		public RunCourseDataSyncByIdResp RunCourseDataSyncById(RunCourseDataSyncByIdReq Request)
		{
			return base.Channel.RunCourseDataSyncById(Request);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0000ED10 File Offset: 0x0000CF10
		public RunCourseDataSyncByStudentNumberResp RunCourseDataSyncByStudentNumber(RunCourseDataSyncByStudentNumberReq Request)
		{
			return base.Channel.RunCourseDataSyncByStudentNumber(Request);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0000ED30 File Offset: 0x0000CF30
		public GetNotetakerPreviewExternalCoursesByStudentNumberResp GetNotetakerPreviewExternalCoursesByStudentNumber(GetNotetakerPreviewExternalCoursesByStudentNumberReq Request)
		{
			return base.Channel.GetNotetakerPreviewExternalCoursesByStudentNumber(Request);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0000ED50 File Offset: 0x0000CF50
		public GetNotetakerPreviewExternalCoursesByUserNameResp GetNotetakerPreviewExternalCoursesByUserName(GetNotetakerPreviewExternalCoursesByUserNameReq Request)
		{
			return base.Channel.GetNotetakerPreviewExternalCoursesByUserName(Request);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0000ED70 File Offset: 0x0000CF70
		public GetNotetakerPreviewDataByStudentNumberResp GetNotetakerPreviewDataByStudentNumber(GetNotetakerPreviewDataByStudentNumberReq Request)
		{
			return base.Channel.GetNotetakerPreviewDataByStudentNumber(Request);
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0000ED90 File Offset: 0x0000CF90
		public GetNotetakerPreviewDataResp GetNotetakerPreviewData(GetNotetakerPreviewDataReq Request)
		{
			return base.Channel.GetNotetakerPreviewData(Request);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0000EDB0 File Offset: 0x0000CFB0
		public GetStudentPreviewDataByStudentNumberOrUsernameResp GetStudentPreviewDataByStudentNumberOrUsername(GetStudentPreviewDataByStudentNumberOrUsernameReq Request)
		{
			return base.Channel.GetStudentPreviewDataByStudentNumberOrUsername(Request);
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0000EDD0 File Offset: 0x0000CFD0
		public LoadCustomTableNamesResp LoadCustomTableNames(LoadCustomTableNamesReq Request)
		{
			return base.Channel.LoadCustomTableNames(Request);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0000EDF0 File Offset: 0x0000CFF0
		public LoadCustomExternalColumnNamesResp LoadCustomExternalColumnNames(LoadCustomExternalColumnNamesReq Request)
		{
			return base.Channel.LoadCustomExternalColumnNames(Request);
		}
	}
}
