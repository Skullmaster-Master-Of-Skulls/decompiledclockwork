using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync.Notetaking;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync.Student;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.DataSync
{
	// Token: 0x02000066 RID: 102
	public interface IDataSyncClientManager : IWebService
	{
		// Token: 0x06000308 RID: 776
		DataSyncResultDTO RunFullDataSyncForExistingStudent(string Student_no, bool DontSyncData, bool DontSyncCourses = false);

		// Token: 0x06000309 RID: 777
		DataSyncPreviewResultDTO PreviewDataSyncData(string Student_no);

		// Token: 0x0600030A RID: 778
		IList<DataSyncExternalCourseDTO> GetNotetakerPreviewExternalCoursesByStudentNumber(string StudentNumber);

		// Token: 0x0600030B RID: 779
		IList<DataSyncExternalCourseDTO> GetNotetakerPreviewExternalCoursesByUserName(string UserName);

		// Token: 0x0600030C RID: 780
		NotetakerWithExternalCoursesDTO GetNotetakerPreviewDataByStudentNumber(string UserName, string StudentNumber);

		// Token: 0x0600030D RID: 781
		NotetakerWithExternalCoursesDTO GetNotetakerPreviewData(string UserName);

		// Token: 0x0600030E RID: 782
		StudentDataSyncPreviewDataDTO GetStudentPreviewDataByStudentNumberOrUsername(string UserName, string StudentNumber);
	}
}
