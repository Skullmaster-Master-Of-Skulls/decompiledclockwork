using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters;
using TechnoPro.Common.Core.AppointmentsTestBooking;
using TechnoPro.Common.Core.Mappers.AppointmentsTestBooking;
using TechnoPro.Common.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000015 RID: 21
	public class ExamFileServiceManager : IExamFile, IService
	{
		// Token: 0x06000110 RID: 272 RVA: 0x000061A0 File Offset: 0x000043A0
		public LoadExamFilesByExamResp LoadExamFilesByExam(LoadExamFilesByExamReq Request)
		{
			IExamFileManager examFileManager = new ExamFileManager(Request.GetOperationContext());
			IList<ExamFile> list = examFileManager.LoadExamFilesByExam(Request.ExamId, Request.IncludeDeletedFiles, Request.LoadFileData);
			LoadExamFilesByExamResp loadExamFilesByExamResp = new LoadExamFilesByExamResp();
			IList<ExamFileDTO> examFiles;
			if (list != null)
			{
				examFiles = list.ToList<ExamFile>().ConvertAll<ExamFileDTO>((ExamFile g) => g.ToDTO());
			}
			else
			{
				examFiles = null;
			}
			loadExamFilesByExamResp.ExamFiles = examFiles;
			return loadExamFilesByExamResp;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00006214 File Offset: 0x00004414
		public LoadExamFileByIdResp LoadExamFileById(LoadExamFileByIdReq Request)
		{
			IExamFileManager examFileManager = new ExamFileManager(Request.GetOperationContext());
			ExamFile examFile = examFileManager.LoadExamFileById(Request.ExamFileId);
			return new LoadExamFileByIdResp
			{
				ExamFile = ((examFile == null) ? null : examFile.ToDTO())
			};
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00006258 File Offset: 0x00004458
		public CreateExamFileResp CreateExamFile(CreateExamFileReq Request)
		{
			IExamFileManager examFileManager = new ExamFileManager(Request.GetOperationContext());
			return new CreateExamFileResp
			{
				ExamFileId = examFileManager.CreateExamFile(Request.ExamFile.ToDomainObject())
			};
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00006294 File Offset: 0x00004494
		public DeleteExamFileResp DeleteExamFile(DeleteExamFileReq Request)
		{
			IExamFileManager examFileManager = new ExamFileManager(Request.GetOperationContext());
			examFileManager.DeleteExamFile(Request.ExamFileId);
			return new DeleteExamFileResp();
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000062C4 File Offset: 0x000044C4
		public LoadExamFilesByExamCheckProfAltContactPermissionsResp LoadExamFilesByExamCheckProfAltContactPermissions(LoadExamFilesByExamCheckProfAltContactPermissionsReq Request)
		{
			IExamFileManager examFileManager = new ExamFileManager(new OperationContext
			{
				WhoAmI = Request.WhoAmI
			});
			IList<ExamFile> list = examFileManager.LoadExamFilesByExamCheckProfAltContactPermissions(Request.InstructorId, Request.AltContactId, Request.ExamId, Request.IncludeDeletedFiles, Request.LoadFileData);
			LoadExamFilesByExamCheckProfAltContactPermissionsResp loadExamFilesByExamCheckProfAltContactPermissionsResp = new LoadExamFilesByExamCheckProfAltContactPermissionsResp();
			IList<ExamFileDTO> examFiles;
			if (list != null)
			{
				examFiles = (from g in list
				select g.ToDTO()).ToList<ExamFileDTO>();
			}
			else
			{
				examFiles = null;
			}
			loadExamFilesByExamCheckProfAltContactPermissionsResp.ExamFiles = examFiles;
			return loadExamFilesByExamCheckProfAltContactPermissionsResp;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00006350 File Offset: 0x00004550
		public LoadExamFileByIdCheckProfAltContactPermissionsResp LoadExamFileByIdCheckProfAltContactPermissions(LoadExamFileByIdCheckProfAltContactPermissionsReq Request)
		{
			IExamFileManager examFileManager = new ExamFileManager(new OperationContext
			{
				WhoAmI = Request.WhoAmI
			});
			ExamFile examFile = examFileManager.LoadExamFileByIdCheckProfAltContactPermissions(Request.ExamId, Request.InstructorId, Request.AltContactId, Request.ExamFileId);
			return new LoadExamFileByIdCheckProfAltContactPermissionsResp
			{
				ExamFile = ((examFile == null) ? null : examFile.ToDTO())
			};
		}
	}
}
