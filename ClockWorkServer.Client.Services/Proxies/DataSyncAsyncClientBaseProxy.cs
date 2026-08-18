using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200007B RID: 123
	internal class DataSyncAsyncClientBaseProxy : ClientBase<IDataSyncAsync>, IDataSyncAsync, IDataSync, IService
	{
		// Token: 0x06000525 RID: 1317 RVA: 0x0000E7A8 File Offset: 0x0000C9A8
		public DataSyncAsyncClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0000E7B3 File Offset: 0x0000C9B3
		public DataSyncAsyncClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0000E7C0 File Offset: 0x0000C9C0
		public LoadCustomTableNamesResp LoadCustomTableNames(LoadCustomTableNamesReq Request)
		{
			return base.Channel.LoadCustomTableNames(Request);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0000E7E0 File Offset: 0x0000C9E0
		public LoadCustomExternalColumnNamesResp LoadCustomExternalColumnNames(LoadCustomExternalColumnNamesReq Request)
		{
			return base.Channel.LoadCustomExternalColumnNames(Request);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0000E800 File Offset: 0x0000CA00
		public LoadDataSyncInfoResp LoadDataSyncInfo(LoadDataSyncInfoReq Request)
		{
			return base.Channel.LoadDataSyncInfo(Request);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0000E820 File Offset: 0x0000CA20
		public PreviewDataSyncDataResp PreviewDataSyncData(PreviewDataSyncDataReq Request)
		{
			return base.Channel.PreviewDataSyncData(Request);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0000E840 File Offset: 0x0000CA40
		public RunFullDataSyncForExistingStudentResp RunFullDataSyncForExistingStudent(RunFullDataSyncForExistingStudentReq Request)
		{
			return base.Channel.RunFullDataSyncForExistingStudent(Request);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0000E860 File Offset: 0x0000CA60
		public RunCourseDataSyncByIdResp RunCourseDataSyncById(RunCourseDataSyncByIdReq Request)
		{
			return base.Channel.RunCourseDataSyncById(Request);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0000E880 File Offset: 0x0000CA80
		public RunCourseDataSyncByStudentNumberResp RunCourseDataSyncByStudentNumber(RunCourseDataSyncByStudentNumberReq Request)
		{
			return base.Channel.RunCourseDataSyncByStudentNumber(Request);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0000E8A0 File Offset: 0x0000CAA0
		public GetNotetakerPreviewExternalCoursesByStudentNumberResp GetNotetakerPreviewExternalCoursesByStudentNumber(GetNotetakerPreviewExternalCoursesByStudentNumberReq Request)
		{
			return base.Channel.GetNotetakerPreviewExternalCoursesByStudentNumber(Request);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0000E8C0 File Offset: 0x0000CAC0
		public GetNotetakerPreviewExternalCoursesByUserNameResp GetNotetakerPreviewExternalCoursesByUserName(GetNotetakerPreviewExternalCoursesByUserNameReq Request)
		{
			return base.Channel.GetNotetakerPreviewExternalCoursesByUserName(Request);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0000E8E0 File Offset: 0x0000CAE0
		public GetNotetakerPreviewDataByStudentNumberResp GetNotetakerPreviewDataByStudentNumber(GetNotetakerPreviewDataByStudentNumberReq Request)
		{
			return base.Channel.GetNotetakerPreviewDataByStudentNumber(Request);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0000E900 File Offset: 0x0000CB00
		public GetNotetakerPreviewDataResp GetNotetakerPreviewData(GetNotetakerPreviewDataReq Request)
		{
			return base.Channel.GetNotetakerPreviewData(Request);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0000E920 File Offset: 0x0000CB20
		public GetStudentPreviewDataByStudentNumberOrUsernameResp GetStudentPreviewDataByStudentNumberOrUsername(GetStudentPreviewDataByStudentNumberOrUsernameReq Request)
		{
			return base.Channel.GetStudentPreviewDataByStudentNumberOrUsername(Request);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0000E940 File Offset: 0x0000CB40
		public IAsyncResult BeginRunFullDataSyncForExistingStudent(RunFullDataSyncForExistingStudentReq req, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginRunFullDataSyncForExistingStudent(req, callback, asyncState);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0000E960 File Offset: 0x0000CB60
		public RunFullDataSyncForExistingStudentResp EndRunFullDataSyncForExistingStudent(IAsyncResult result)
		{
			return base.Channel.EndRunFullDataSyncForExistingStudent(result);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0000E980 File Offset: 0x0000CB80
		public IAsyncResult BeginPreviewDataSyncData(PreviewDataSyncDataReq req, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginPreviewDataSyncData(req, callback, asyncState);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0000E9A0 File Offset: 0x0000CBA0
		public PreviewDataSyncDataResp EndPreviewDataSyncData(IAsyncResult result)
		{
			return base.Channel.EndPreviewDataSyncData(result);
		}
	}
}
