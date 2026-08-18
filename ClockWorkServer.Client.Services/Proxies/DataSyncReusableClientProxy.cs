using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200007C RID: 124
	public class DataSyncReusableClientProxy : WCFTokenBasedReusableClientProxy<IDataSync>, IDataSync, IService
	{
		// Token: 0x06000537 RID: 1335 RVA: 0x0000E9BE File Offset: 0x0000CBBE
		public DataSyncReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0000E9C9 File Offset: 0x0000CBC9
		public DataSyncReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0000E9D8 File Offset: 0x0000CBD8
		public LoadDataSyncInfoResp LoadDataSyncInfo(LoadDataSyncInfoReq Request)
		{
			return this.WrapServiceMethod<LoadDataSyncInfoResp>(() => this.Proxy.LoadDataSyncInfo(Request));
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0000EA10 File Offset: 0x0000CC10
		public PreviewDataSyncDataResp PreviewDataSyncData(PreviewDataSyncDataReq Request)
		{
			return this.WrapServiceMethod<PreviewDataSyncDataResp>(() => this.Proxy.PreviewDataSyncData(Request));
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0000EA48 File Offset: 0x0000CC48
		public RunFullDataSyncForExistingStudentResp RunFullDataSyncForExistingStudent(RunFullDataSyncForExistingStudentReq Request)
		{
			return this.WrapServiceMethod<RunFullDataSyncForExistingStudentResp>(() => this.Proxy.RunFullDataSyncForExistingStudent(Request));
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0000EA80 File Offset: 0x0000CC80
		public RunCourseDataSyncByIdResp RunCourseDataSyncById(RunCourseDataSyncByIdReq Request)
		{
			return this.WrapServiceMethod<RunCourseDataSyncByIdResp>(() => this.Proxy.RunCourseDataSyncById(Request));
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0000EAB8 File Offset: 0x0000CCB8
		public RunCourseDataSyncByStudentNumberResp RunCourseDataSyncByStudentNumber(RunCourseDataSyncByStudentNumberReq Request)
		{
			return this.WrapServiceMethod<RunCourseDataSyncByStudentNumberResp>(() => this.Proxy.RunCourseDataSyncByStudentNumber(Request));
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0000EAF0 File Offset: 0x0000CCF0
		public GetNotetakerPreviewExternalCoursesByStudentNumberResp GetNotetakerPreviewExternalCoursesByStudentNumber(GetNotetakerPreviewExternalCoursesByStudentNumberReq Request)
		{
			return this.WrapServiceMethod<GetNotetakerPreviewExternalCoursesByStudentNumberResp>(() => this.Proxy.GetNotetakerPreviewExternalCoursesByStudentNumber(Request));
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0000EB28 File Offset: 0x0000CD28
		public GetNotetakerPreviewExternalCoursesByUserNameResp GetNotetakerPreviewExternalCoursesByUserName(GetNotetakerPreviewExternalCoursesByUserNameReq Request)
		{
			return this.WrapServiceMethod<GetNotetakerPreviewExternalCoursesByUserNameResp>(() => this.Proxy.GetNotetakerPreviewExternalCoursesByUserName(Request));
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0000EB60 File Offset: 0x0000CD60
		public GetNotetakerPreviewDataByStudentNumberResp GetNotetakerPreviewDataByStudentNumber(GetNotetakerPreviewDataByStudentNumberReq Request)
		{
			return this.WrapServiceMethod<GetNotetakerPreviewDataByStudentNumberResp>(() => this.Proxy.GetNotetakerPreviewDataByStudentNumber(Request));
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0000EB98 File Offset: 0x0000CD98
		public GetNotetakerPreviewDataResp GetNotetakerPreviewData(GetNotetakerPreviewDataReq Request)
		{
			return this.WrapServiceMethod<GetNotetakerPreviewDataResp>(() => this.Proxy.GetNotetakerPreviewData(Request));
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0000EBD0 File Offset: 0x0000CDD0
		public GetStudentPreviewDataByStudentNumberOrUsernameResp GetStudentPreviewDataByStudentNumberOrUsername(GetStudentPreviewDataByStudentNumberOrUsernameReq Request)
		{
			return this.WrapServiceMethod<GetStudentPreviewDataByStudentNumberOrUsernameResp>(() => this.Proxy.GetStudentPreviewDataByStudentNumberOrUsername(Request));
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0000EC08 File Offset: 0x0000CE08
		public LoadCustomTableNamesResp LoadCustomTableNames(LoadCustomTableNamesReq Request)
		{
			return this.WrapServiceMethod<LoadCustomTableNamesResp>(() => this.Proxy.LoadCustomTableNames(Request));
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0000EC40 File Offset: 0x0000CE40
		public LoadCustomExternalColumnNamesResp LoadCustomExternalColumnNames(LoadCustomExternalColumnNamesReq Request)
		{
			return this.WrapServiceMethod<LoadCustomExternalColumnNamesResp>(() => this.Proxy.LoadCustomExternalColumnNames(Request));
		}
	}
}
