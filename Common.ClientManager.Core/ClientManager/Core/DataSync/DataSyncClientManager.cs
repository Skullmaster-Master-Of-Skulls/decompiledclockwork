using System;
using System.Collections.Generic;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Client.Services.Proxies;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync.Notetaking;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync.Student;
using TechnoPro.Common.ClientManager.ClientCaching;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.DataSync;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Exceptions;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.DataSync
{
	// Token: 0x0200006D RID: 109
	public class DataSyncClientManager : IDataSyncClientManager, IWebService, IDataSyncAsync, IDataSync, IService, IDisposable
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x00011A61 File Offset: 0x0000FC61
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x00011A69 File Offset: 0x0000FC69
		private IDataSyncAsync dataSyncAsyncProxy { get; set; }

		// Token: 0x060003EF RID: 1007 RVA: 0x00011A74 File Offset: 0x0000FC74
		public DataSyncClientManager()
		{
			ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
			bool isClockWorkServerEnable = clientCache.IsClockWorkServerEnable;
			bool flag = !isClockWorkServerEnable;
			if (flag)
			{
				this.useService = false;
			}
			bool flag2 = this.useService;
			if (flag2)
			{
				this.dataSyncAsyncProxy = WCFClientProxy<IDataSyncAsync>.GetAsyncInstance();
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00011ACC File Offset: 0x0000FCCC
		public LoadDataSyncInfoResp LoadDataSyncInfo(LoadDataSyncInfoReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<LoadDataSyncInfoReq>(Request);
			IDataSync clientInstance = ClientServiceFactory.GetClientInstance<IDataSync>(this.useService, false);
			ApplicationContext applicationContext = Request.ApplicationContext ?? ObjectFactory.Resolve<ApplicationContext>();
			Request.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			return clientInstance.LoadDataSyncInfo(Request);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00011B24 File Offset: 0x0000FD24
		public PreviewDataSyncDataResp PreviewDataSyncData(PreviewDataSyncDataReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<PreviewDataSyncDataReq>(Request);
			bool flag;
			IDataSync clientInstance = ClientServiceFactory.GetClientInstance<IDataSync>(this.useService, out flag, false);
			CWLogger.Logger.Trace("PreviewDataSyncData:RanThroughServer={0}", flag.ToString());
			ApplicationContext applicationContext = Request.ApplicationContext ?? ObjectFactory.Resolve<ApplicationContext>();
			Request.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			return clientInstance.PreviewDataSyncData(Request);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00011B94 File Offset: 0x0000FD94
		public RunFullDataSyncForExistingStudentResp RunFullDataSyncForExistingStudent(RunFullDataSyncForExistingStudentReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<RunFullDataSyncForExistingStudentReq>(Request);
			IDataSync clientInstance = ClientServiceFactory.GetClientInstance<IDataSync>(this.useService, false);
			ApplicationContext applicationContext = Request.ApplicationContext ?? ObjectFactory.Resolve<ApplicationContext>();
			Request.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			return clientInstance.RunFullDataSyncForExistingStudent(Request);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00011BEC File Offset: 0x0000FDEC
		public RunCourseDataSyncByIdResp RunCourseDataSyncById(RunCourseDataSyncByIdReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<RunCourseDataSyncByIdReq>(Request);
			IDataSync clientInstance = ClientServiceFactory.GetClientInstance<IDataSync>(this.useService, false);
			ApplicationContext applicationContext = Request.ApplicationContext ?? ObjectFactory.Resolve<ApplicationContext>();
			Request.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			return clientInstance.RunCourseDataSyncById(Request);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00011C44 File Offset: 0x0000FE44
		public RunCourseDataSyncByStudentNumberResp RunCourseDataSyncByStudentNumber(RunCourseDataSyncByStudentNumberReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<RunCourseDataSyncByStudentNumberReq>(Request);
			IDataSync clientInstance = ClientServiceFactory.GetClientInstance<IDataSync>(this.useService, false);
			ApplicationContext applicationContext = Request.ApplicationContext ?? ObjectFactory.Resolve<ApplicationContext>();
			Request.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			return clientInstance.RunCourseDataSyncByStudentNumber(Request);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00011C9C File Offset: 0x0000FE9C
		public GetNotetakerPreviewExternalCoursesByStudentNumberResp GetNotetakerPreviewExternalCoursesByStudentNumber(GetNotetakerPreviewExternalCoursesByStudentNumberReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<GetNotetakerPreviewExternalCoursesByStudentNumberReq>(Request);
			IDataSync clientInstance = ClientServiceFactory.GetClientInstance<IDataSync>(this.useService, false);
			ApplicationContext applicationContext = Request.ApplicationContext ?? ObjectFactory.Resolve<ApplicationContext>();
			Request.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			return clientInstance.GetNotetakerPreviewExternalCoursesByStudentNumber(Request);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00011CF4 File Offset: 0x0000FEF4
		public GetNotetakerPreviewDataByStudentNumberResp GetNotetakerPreviewDataByStudentNumber(GetNotetakerPreviewDataByStudentNumberReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<GetNotetakerPreviewDataByStudentNumberReq>(Request);
			IDataSync clientInstance = ClientServiceFactory.GetClientInstance<IDataSync>(this.useService, false);
			ApplicationContext applicationContext = Request.ApplicationContext ?? ObjectFactory.Resolve<ApplicationContext>();
			Request.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			return clientInstance.GetNotetakerPreviewDataByStudentNumber(Request);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00011D4C File Offset: 0x0000FF4C
		public GetNotetakerPreviewExternalCoursesByUserNameResp GetNotetakerPreviewExternalCoursesByUserName(GetNotetakerPreviewExternalCoursesByUserNameReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<GetNotetakerPreviewExternalCoursesByUserNameReq>(Request);
			IDataSync clientInstance = ClientServiceFactory.GetClientInstance<IDataSync>(this.useService, false);
			ApplicationContext applicationContext = Request.ApplicationContext ?? ObjectFactory.Resolve<ApplicationContext>();
			Request.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			return clientInstance.GetNotetakerPreviewExternalCoursesByUserName(Request);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00011DA4 File Offset: 0x0000FFA4
		public GetNotetakerPreviewDataResp GetNotetakerPreviewData(GetNotetakerPreviewDataReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<GetNotetakerPreviewDataReq>(Request);
			ApplicationContext applicationContext = Request.ApplicationContext ?? ObjectFactory.Resolve<ApplicationContext>();
			Request.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			IDataSync clientInstance = ClientServiceFactory.GetClientInstance<IDataSync>(this.useService, false);
			return clientInstance.GetNotetakerPreviewData(Request);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00011DFC File Offset: 0x0000FFFC
		public GetStudentPreviewDataByStudentNumberOrUsernameResp GetStudentPreviewDataByStudentNumberOrUsername(GetStudentPreviewDataByStudentNumberOrUsernameReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<GetStudentPreviewDataByStudentNumberOrUsernameReq>(Request);
			ApplicationContext applicationContext = Request.ApplicationContext ?? ObjectFactory.Resolve<ApplicationContext>();
			Request.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			IDataSync clientInstance = ClientServiceFactory.GetClientInstance<IDataSync>(this.useService, false);
			return clientInstance.GetStudentPreviewDataByStudentNumberOrUsername(Request);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00011E54 File Offset: 0x00010054
		public LoadCustomTableNamesResp LoadCustomTableNames(LoadCustomTableNamesReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<LoadCustomTableNamesReq>(Request);
			ApplicationContext applicationContext = Request.ApplicationContext ?? ObjectFactory.Resolve<ApplicationContext>();
			Request.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			IDataSync clientInstance = ClientServiceFactory.GetClientInstance<IDataSync>(this.useService, false);
			return clientInstance.LoadCustomTableNames(Request);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00011EAC File Offset: 0x000100AC
		public LoadCustomExternalColumnNamesResp LoadCustomExternalColumnNames(LoadCustomExternalColumnNamesReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<LoadCustomExternalColumnNamesReq>(Request);
			ApplicationContext applicationContext = Request.ApplicationContext ?? ObjectFactory.Resolve<ApplicationContext>();
			Request.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			IDataSync clientInstance = ClientServiceFactory.GetClientInstance<IDataSync>(this.useService, false);
			return clientInstance.LoadCustomExternalColumnNames(Request);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00011F04 File Offset: 0x00010104
		public IAsyncResult BeginRunFullDataSyncForExistingStudent(RunFullDataSyncForExistingStudentReq req, AsyncCallback callback, object asyncState)
		{
			bool flag = this.dataSyncAsyncProxy == null;
			if (flag)
			{
				throw new ClockWorkServerNotConnectedException();
			}
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<RunFullDataSyncForExistingStudentReq>(req);
			return this.dataSyncAsyncProxy.BeginRunFullDataSyncForExistingStudent(req, callback, asyncState);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00011F44 File Offset: 0x00010144
		public RunFullDataSyncForExistingStudentResp EndRunFullDataSyncForExistingStudent(IAsyncResult result)
		{
			bool flag = this.dataSyncAsyncProxy == null;
			if (flag)
			{
				throw new ClockWorkServerNotConnectedException();
			}
			return this.dataSyncAsyncProxy.EndRunFullDataSyncForExistingStudent(result);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00011F78 File Offset: 0x00010178
		public IAsyncResult BeginPreviewDataSyncData(PreviewDataSyncDataReq req, AsyncCallback callback, object asyncState)
		{
			bool flag = this.dataSyncAsyncProxy == null;
			if (flag)
			{
				throw new ClockWorkServerNotConnectedException();
			}
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<PreviewDataSyncDataReq>(req);
			return this.dataSyncAsyncProxy.BeginPreviewDataSyncData(req, callback, asyncState);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00011FB8 File Offset: 0x000101B8
		public PreviewDataSyncDataResp EndPreviewDataSyncData(IAsyncResult result)
		{
			bool flag = this.dataSyncAsyncProxy == null;
			if (flag)
			{
				throw new ClockWorkServerNotConnectedException();
			}
			return this.dataSyncAsyncProxy.EndPreviewDataSyncData(result);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00011FE9 File Offset: 0x000101E9
		public void Close()
		{
			this.Dispose();
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00011FF4 File Offset: 0x000101F4
		public DataSyncResultDTO RunFullDataSyncForExistingStudent(string Student_no, bool DontSyncData, bool DontSyncCourses = false)
		{
			RunFullDataSyncForExistingStudentReq runFullDataSyncForExistingStudentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<RunFullDataSyncForExistingStudentReq>();
			ApplicationContext applicationContext = runFullDataSyncForExistingStudentReq.ApplicationContext ?? ObjectFactory.Resolve<ApplicationContext>();
			runFullDataSyncForExistingStudentReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			runFullDataSyncForExistingStudentReq.Student_no = Student_no;
			runFullDataSyncForExistingStudentReq.DontSyncCourses = DontSyncCourses;
			runFullDataSyncForExistingStudentReq.DontSyncData = DontSyncData;
			return ClientServiceFactory.GetClientInstance<IDataSync>().RunFullDataSyncForExistingStudent(runFullDataSyncForExistingStudentReq).Result;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0001205C File Offset: 0x0001025C
		public DataSyncPreviewResultDTO PreviewDataSyncData(string Student_no)
		{
			PreviewDataSyncDataReq previewDataSyncDataReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<PreviewDataSyncDataReq>();
			ApplicationContext applicationContext = previewDataSyncDataReq.ApplicationContext ?? ObjectFactory.Resolve<ApplicationContext>();
			previewDataSyncDataReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			previewDataSyncDataReq.Student_no = Student_no;
			return ClientServiceFactory.GetClientInstance<IDataSync>().PreviewDataSyncData(previewDataSyncDataReq).Result;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x000120B4 File Offset: 0x000102B4
		public IList<DataSyncExternalCourseDTO> GetNotetakerPreviewExternalCoursesByStudentNumber(string StudentNumber)
		{
			GetNotetakerPreviewExternalCoursesByStudentNumberReq getNotetakerPreviewExternalCoursesByStudentNumberReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetNotetakerPreviewExternalCoursesByStudentNumberReq>();
			ApplicationContext applicationContext = getNotetakerPreviewExternalCoursesByStudentNumberReq.ApplicationContext ?? ObjectFactory.Resolve<ApplicationContext>();
			getNotetakerPreviewExternalCoursesByStudentNumberReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			getNotetakerPreviewExternalCoursesByStudentNumberReq.StudentNumber = StudentNumber;
			return ClientServiceFactory.GetClientInstance<IDataSync>().GetNotetakerPreviewExternalCoursesByStudentNumber(getNotetakerPreviewExternalCoursesByStudentNumberReq).ExternalCourses;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0001210C File Offset: 0x0001030C
		public IList<DataSyncExternalCourseDTO> GetNotetakerPreviewExternalCoursesByUserName(string UserName)
		{
			GetNotetakerPreviewExternalCoursesByUserNameReq getNotetakerPreviewExternalCoursesByUserNameReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetNotetakerPreviewExternalCoursesByUserNameReq>();
			ApplicationContext applicationContext = getNotetakerPreviewExternalCoursesByUserNameReq.ApplicationContext ?? ObjectFactory.Resolve<ApplicationContext>();
			getNotetakerPreviewExternalCoursesByUserNameReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			getNotetakerPreviewExternalCoursesByUserNameReq.UserName = UserName;
			return ClientServiceFactory.GetClientInstance<IDataSync>().GetNotetakerPreviewExternalCoursesByUserName(getNotetakerPreviewExternalCoursesByUserNameReq).ExternalCourses;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00012164 File Offset: 0x00010364
		public NotetakerWithExternalCoursesDTO GetNotetakerPreviewDataByStudentNumber(string UserName, string StudentNumber)
		{
			GetNotetakerPreviewDataByStudentNumberReq getNotetakerPreviewDataByStudentNumberReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetNotetakerPreviewDataByStudentNumberReq>();
			ApplicationContext applicationContext = getNotetakerPreviewDataByStudentNumberReq.ApplicationContext ?? ObjectFactory.Resolve<ApplicationContext>();
			getNotetakerPreviewDataByStudentNumberReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			getNotetakerPreviewDataByStudentNumberReq.UserName = UserName;
			getNotetakerPreviewDataByStudentNumberReq.StudentNumber = StudentNumber;
			return ClientServiceFactory.GetClientInstance<IDataSync>().GetNotetakerPreviewDataByStudentNumber(getNotetakerPreviewDataByStudentNumberReq).NotetakerWithExternalCourses;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x000121C4 File Offset: 0x000103C4
		public NotetakerWithExternalCoursesDTO GetNotetakerPreviewData(string UserName)
		{
			GetNotetakerPreviewDataReq getNotetakerPreviewDataReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetNotetakerPreviewDataReq>();
			ApplicationContext applicationContext = getNotetakerPreviewDataReq.ApplicationContext ?? ObjectFactory.Resolve<ApplicationContext>();
			getNotetakerPreviewDataReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			getNotetakerPreviewDataReq.UserName = UserName;
			return ClientServiceFactory.GetClientInstance<IDataSync>().GetNotetakerPreviewData(getNotetakerPreviewDataReq).NotetakerWithExternalCourses;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0001221C File Offset: 0x0001041C
		public StudentDataSyncPreviewDataDTO GetStudentPreviewDataByStudentNumberOrUsername(string UserName, string StudentNumber)
		{
			GetStudentPreviewDataByStudentNumberOrUsernameReq getStudentPreviewDataByStudentNumberOrUsernameReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetStudentPreviewDataByStudentNumberOrUsernameReq>();
			ApplicationContext applicationContext = getStudentPreviewDataByStudentNumberOrUsernameReq.ApplicationContext ?? ObjectFactory.Resolve<ApplicationContext>();
			getStudentPreviewDataByStudentNumberOrUsernameReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			getStudentPreviewDataByStudentNumberOrUsernameReq.UserName = UserName;
			getStudentPreviewDataByStudentNumberOrUsernameReq.StudentNumber = StudentNumber;
			return ClientServiceFactory.GetClientInstance<IDataSync>().GetStudentPreviewDataByStudentNumberOrUsername(getStudentPreviewDataByStudentNumberOrUsernameReq).DataSyncPreviewData;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0001227C File Offset: 0x0001047C
		~DataSyncClientManager()
		{
			this.Dispose(false);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x000122B0 File Offset: 0x000104B0
		protected virtual void Dispose(bool disposing)
		{
			bool flag = !this.disposed;
			if (flag)
			{
				if (disposing)
				{
				}
				bool flag2 = this.dataSyncAsyncProxy != null;
				if (flag2)
				{
					this.dataSyncAsyncProxy.Close();
				}
				this.dataSyncAsyncProxy = null;
				this.disposed = true;
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x000122FC File Offset: 0x000104FC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000012 RID: 18
		private bool useService = true;

		// Token: 0x04000013 RID: 19
		private bool disposed = false;
	}
}
