using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync.Notetaking;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync.Student;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.DataSync;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.DataSync
{
	// Token: 0x0200005C RID: 92
	public class DataSyncRestClientManager : BearerTokenRestProxy<IDataSyncClientManager>, IDataSyncClientManager, IWebService
	{
		// Token: 0x06000381 RID: 897 RVA: 0x0000AB96 File Offset: 0x00008D96
		public DataSyncRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000ABA0 File Offset: 0x00008DA0
		public DataSyncRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000ABAC File Offset: 0x00008DAC
		public DataSyncResultDTO RunFullDataSyncForExistingStudent(string Student_no, bool DontSyncData, bool DontSyncCourses = false)
		{
			RunFullDataSyncForExistingStudentReq runFullDataSyncForExistingStudentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<RunFullDataSyncForExistingStudentReq>();
			ApplicationContext applicationContext = runFullDataSyncForExistingStudentReq.ApplicationContext ?? ObjectFactory.Resolve<ApplicationContext>();
			runFullDataSyncForExistingStudentReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			runFullDataSyncForExistingStudentReq.Student_no = Student_no;
			runFullDataSyncForExistingStudentReq.DontSyncCourses = DontSyncCourses;
			runFullDataSyncForExistingStudentReq.DontSyncData = DontSyncData;
			return base.Post<RunFullDataSyncForExistingStudentReq, DataSyncResultDTO>(runFullDataSyncForExistingStudentReq, "datasync/runfulldatasync");
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000AC07 File Offset: 0x00008E07
		public DataSyncPreviewResultDTO PreviewDataSyncData(string Student_no)
		{
			return base.Get<DataSyncPreviewResultDTO>(string.Format("datasync/previewdata/studentno/{0}", Student_no), true);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000AC1B File Offset: 0x00008E1B
		public IList<DataSyncExternalCourseDTO> GetNotetakerPreviewExternalCoursesByStudentNumber(string StudentNumber)
		{
			return base.GetMany<DataSyncExternalCourseDTO>(string.Format("datasync/notetakerpreviewexternalcourses/studentno/{0}", StudentNumber), true);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000AC2F File Offset: 0x00008E2F
		public IList<DataSyncExternalCourseDTO> GetNotetakerPreviewExternalCoursesByUserName(string UserName)
		{
			return base.GetMany<DataSyncExternalCourseDTO>(string.Format("datasync/notetakerpreviewexternalcourses/username/{0}", UserName), true);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000AC43 File Offset: 0x00008E43
		public NotetakerWithExternalCoursesDTO GetNotetakerPreviewDataByStudentNumber(string UserName, string StudentNumber)
		{
			return base.Get<NotetakerWithExternalCoursesDTO>(string.Format("datasync/notetakerpreviewdata/username/{0}/studentno/{1}", UserName, StudentNumber), true);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000AC58 File Offset: 0x00008E58
		public NotetakerWithExternalCoursesDTO GetNotetakerPreviewData(string UserName)
		{
			return base.Get<NotetakerWithExternalCoursesDTO>(string.Format("datasync/notetakerpreviewdata/username/{0}", UserName), true);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000AC6C File Offset: 0x00008E6C
		public StudentDataSyncPreviewDataDTO GetStudentPreviewDataByStudentNumberOrUsername(string UserName, string StudentNumber)
		{
			return base.Get<StudentDataSyncPreviewDataDTO>(string.Format("datasync/studentpreviewdata/username/{0}/studentno/{1}", UserName, StudentNumber), true);
		}
	}
}
