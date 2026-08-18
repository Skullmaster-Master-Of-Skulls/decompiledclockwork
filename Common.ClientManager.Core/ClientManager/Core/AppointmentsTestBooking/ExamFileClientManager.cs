using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.AppointmentsTestBooking
{
	// Token: 0x0200008C RID: 140
	public class ExamFileClientManager : IExamFileClientManager, IWebService
	{
		// Token: 0x06000510 RID: 1296 RVA: 0x00016AFC File Offset: 0x00014CFC
		public IList<ExamFileDTO> LoadExamFilesByExam(int ExamId, bool IncludeDeletedFiles, bool LoadFileData)
		{
			LoadExamFilesByExamReq loadExamFilesByExamReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadExamFilesByExamReq>();
			loadExamFilesByExamReq.ExamId = ExamId;
			loadExamFilesByExamReq.IncludeDeletedFiles = IncludeDeletedFiles;
			loadExamFilesByExamReq.LoadFileData = LoadFileData;
			return ClientServiceFactory.GetClientInstance<IExamFile>().LoadExamFilesByExam(loadExamFilesByExamReq).ExamFiles;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00016B44 File Offset: 0x00014D44
		public ExamFileDTO LoadExamFileById(int ExamFileId)
		{
			LoadExamFileByIdReq loadExamFileByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadExamFileByIdReq>();
			loadExamFileByIdReq.ExamFileId = ExamFileId;
			return ClientServiceFactory.GetClientInstance<IExamFile>().LoadExamFileById(loadExamFileByIdReq).ExamFile;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00016B7C File Offset: 0x00014D7C
		public int CreateExamFile(ExamFileDTO ExamFile)
		{
			CreateExamFileReq createExamFileReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateExamFileReq>();
			createExamFileReq.ExamFile = ExamFile;
			return ClientServiceFactory.GetClientInstance<IExamFile>().CreateExamFile(createExamFileReq).ExamFileId;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00016BB4 File Offset: 0x00014DB4
		public void DeleteExamFile(int ExamFileId)
		{
			DeleteExamFileReq deleteExamFileReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteExamFileReq>();
			deleteExamFileReq.ExamFileId = ExamFileId;
			ClientServiceFactory.GetClientInstance<IExamFile>().DeleteExamFile(deleteExamFileReq);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00016BE4 File Offset: 0x00014DE4
		public IList<ExamFileDTO> LoadExamFilesByExamCheckProfAltContactPermissions(int InstructorId, int AltContactId, int ExamId, bool IncludeDeletedFiles, bool LoadFileData)
		{
			LoadExamFilesByExamCheckProfAltContactPermissionsReq loadExamFilesByExamCheckProfAltContactPermissionsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadExamFilesByExamCheckProfAltContactPermissionsReq>();
			loadExamFilesByExamCheckProfAltContactPermissionsReq.ExamId = ExamId;
			loadExamFilesByExamCheckProfAltContactPermissionsReq.IncludeDeletedFiles = IncludeDeletedFiles;
			loadExamFilesByExamCheckProfAltContactPermissionsReq.LoadFileData = LoadFileData;
			return ClientServiceFactory.GetClientInstance<IExamFile>().LoadExamFilesByExamCheckProfAltContactPermissions(loadExamFilesByExamCheckProfAltContactPermissionsReq).ExamFiles;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00016C2C File Offset: 0x00014E2C
		public ExamFileDTO LoadExamFileByIdCheckProfAltContactPermissions(int ExamId, int InstructorId, int AltContactId, int ExamFileId)
		{
			LoadExamFileByIdCheckProfAltContactPermissionsReq loadExamFileByIdCheckProfAltContactPermissionsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadExamFileByIdCheckProfAltContactPermissionsReq>();
			loadExamFileByIdCheckProfAltContactPermissionsReq.ExamId = ExamId;
			loadExamFileByIdCheckProfAltContactPermissionsReq.InstructorId = InstructorId;
			loadExamFileByIdCheckProfAltContactPermissionsReq.AltContactId = AltContactId;
			loadExamFileByIdCheckProfAltContactPermissionsReq.ExamFileId = ExamFileId;
			return ClientServiceFactory.GetClientInstance<IExamFile>().LoadExamFileByIdCheckProfAltContactPermissions(loadExamFileByIdCheckProfAltContactPermissionsReq).ExamFile;
		}
	}
}
