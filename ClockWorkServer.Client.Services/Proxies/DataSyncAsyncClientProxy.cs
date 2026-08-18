using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200007A RID: 122
	public class DataSyncAsyncClientProxy : WCFTokenBasedAsyncClientProxy<IDataSyncAsync>, IDataSyncAsync, IDataSync, IService
	{
		// Token: 0x06000513 RID: 1299 RVA: 0x0000E3EE File Offset: 0x0000C5EE
		public DataSyncAsyncClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0000E3F9 File Offset: 0x0000C5F9
		public DataSyncAsyncClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0000E408 File Offset: 0x0000C608
		public LoadDataSyncInfoResp LoadDataSyncInfo(LoadDataSyncInfoReq Request)
		{
			return this.WrapServiceMethod<LoadDataSyncInfoResp>(() => this.Proxy.LoadDataSyncInfo(Request));
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0000E440 File Offset: 0x0000C640
		public LoadCustomTableNamesResp LoadCustomTableNames(LoadCustomTableNamesReq Request)
		{
			return this.WrapServiceMethod<LoadCustomTableNamesResp>(() => this.Proxy.LoadCustomTableNames(Request));
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0000E478 File Offset: 0x0000C678
		public LoadCustomExternalColumnNamesResp LoadCustomExternalColumnNames(LoadCustomExternalColumnNamesReq Request)
		{
			return this.WrapServiceMethod<LoadCustomExternalColumnNamesResp>(() => this.Proxy.LoadCustomExternalColumnNames(Request));
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0000E4B0 File Offset: 0x0000C6B0
		public PreviewDataSyncDataResp PreviewDataSyncData(PreviewDataSyncDataReq Request)
		{
			return this.WrapServiceMethod<PreviewDataSyncDataResp>(() => this.Proxy.PreviewDataSyncData(Request));
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0000E4E8 File Offset: 0x0000C6E8
		public RunFullDataSyncForExistingStudentResp RunFullDataSyncForExistingStudent(RunFullDataSyncForExistingStudentReq Request)
		{
			return this.WrapServiceMethod<RunFullDataSyncForExistingStudentResp>(() => this.Proxy.RunFullDataSyncForExistingStudent(Request));
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0000E520 File Offset: 0x0000C720
		public RunCourseDataSyncByIdResp RunCourseDataSyncById(RunCourseDataSyncByIdReq Request)
		{
			return this.WrapServiceMethod<RunCourseDataSyncByIdResp>(() => this.Proxy.RunCourseDataSyncById(Request));
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0000E558 File Offset: 0x0000C758
		public RunCourseDataSyncByStudentNumberResp RunCourseDataSyncByStudentNumber(RunCourseDataSyncByStudentNumberReq Request)
		{
			return this.WrapServiceMethod<RunCourseDataSyncByStudentNumberResp>(() => this.Proxy.RunCourseDataSyncByStudentNumber(Request));
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0000E590 File Offset: 0x0000C790
		public GetNotetakerPreviewExternalCoursesByStudentNumberResp GetNotetakerPreviewExternalCoursesByStudentNumber(GetNotetakerPreviewExternalCoursesByStudentNumberReq Request)
		{
			return this.WrapServiceMethod<GetNotetakerPreviewExternalCoursesByStudentNumberResp>(() => this.Proxy.GetNotetakerPreviewExternalCoursesByStudentNumber(Request));
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0000E5C8 File Offset: 0x0000C7C8
		public GetNotetakerPreviewExternalCoursesByUserNameResp GetNotetakerPreviewExternalCoursesByUserName(GetNotetakerPreviewExternalCoursesByUserNameReq Request)
		{
			return this.WrapServiceMethod<GetNotetakerPreviewExternalCoursesByUserNameResp>(() => this.Proxy.GetNotetakerPreviewExternalCoursesByUserName(Request));
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0000E600 File Offset: 0x0000C800
		public GetNotetakerPreviewDataByStudentNumberResp GetNotetakerPreviewDataByStudentNumber(GetNotetakerPreviewDataByStudentNumberReq Request)
		{
			return this.WrapServiceMethod<GetNotetakerPreviewDataByStudentNumberResp>(() => this.Proxy.GetNotetakerPreviewDataByStudentNumber(Request));
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0000E638 File Offset: 0x0000C838
		public GetNotetakerPreviewDataResp GetNotetakerPreviewData(GetNotetakerPreviewDataReq Request)
		{
			return this.WrapServiceMethod<GetNotetakerPreviewDataResp>(() => this.Proxy.GetNotetakerPreviewData(Request));
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0000E670 File Offset: 0x0000C870
		public GetStudentPreviewDataByStudentNumberOrUsernameResp GetStudentPreviewDataByStudentNumberOrUsername(GetStudentPreviewDataByStudentNumberOrUsernameReq Request)
		{
			return this.WrapServiceMethod<GetStudentPreviewDataByStudentNumberOrUsernameResp>(() => this.Proxy.GetStudentPreviewDataByStudentNumberOrUsername(Request));
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0000E6A8 File Offset: 0x0000C8A8
		public IAsyncResult BeginRunFullDataSyncForExistingStudent(RunFullDataSyncForExistingStudentReq req, AsyncCallback callback, object asyncState)
		{
			return this.WrapServiceMethod<IAsyncResult>(() => this.Proxy.BeginRunFullDataSyncForExistingStudent(req, callback, asyncState));
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0000E6F0 File Offset: 0x0000C8F0
		public RunFullDataSyncForExistingStudentResp EndRunFullDataSyncForExistingStudent(IAsyncResult result)
		{
			return this.WrapServiceMethod<RunFullDataSyncForExistingStudentResp>(() => this.Proxy.EndRunFullDataSyncForExistingStudent(result));
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0000E728 File Offset: 0x0000C928
		public IAsyncResult BeginPreviewDataSyncData(PreviewDataSyncDataReq req, AsyncCallback callback, object asyncState)
		{
			return this.WrapServiceMethod<IAsyncResult>(() => this.Proxy.BeginPreviewDataSyncData(req, callback, asyncState));
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0000E770 File Offset: 0x0000C970
		public PreviewDataSyncDataResp EndPreviewDataSyncData(IAsyncResult result)
		{
			return this.WrapServiceMethod<PreviewDataSyncDataResp>(() => this.Proxy.EndPreviewDataSyncData(result));
		}
	}
}
