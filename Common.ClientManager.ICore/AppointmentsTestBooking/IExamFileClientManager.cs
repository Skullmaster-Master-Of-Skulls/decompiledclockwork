using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking
{
	// Token: 0x02000086 RID: 134
	public interface IExamFileClientManager : IWebService
	{
		// Token: 0x060003F9 RID: 1017
		IList<ExamFileDTO> LoadExamFilesByExam(int ExamId, bool IncludeDeletedFiles, bool LoadFileData);

		// Token: 0x060003FA RID: 1018
		ExamFileDTO LoadExamFileById(int ExamFileId);

		// Token: 0x060003FB RID: 1019
		int CreateExamFile(ExamFileDTO ExamFile);

		// Token: 0x060003FC RID: 1020
		void DeleteExamFile(int ExamFileId);

		// Token: 0x060003FD RID: 1021
		IList<ExamFileDTO> LoadExamFilesByExamCheckProfAltContactPermissions(int InstructorId, int AltContactId, int ExamId, bool IncludeDeletedFiles, bool LoadFileData);

		// Token: 0x060003FE RID: 1022
		ExamFileDTO LoadExamFileByIdCheckProfAltContactPermissions(int ExamId, int InstructorId, int AltContactId, int ExamFileId);
	}
}
