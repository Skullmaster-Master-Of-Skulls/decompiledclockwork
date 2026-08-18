using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.AppointmentsTestBooking
{
	// Token: 0x02000076 RID: 118
	public class ExamFileRestClientManager : BearerTokenRestProxy<IExamFileClientManager>, IExamFileClientManager, IWebService
	{
		// Token: 0x0600048D RID: 1165 RVA: 0x0000D0E0 File Offset: 0x0000B2E0
		public ExamFileRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000D0EA File Offset: 0x0000B2EA
		public ExamFileRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000D0F5 File Offset: 0x0000B2F5
		public IList<ExamFileDTO> LoadExamFilesByExam(int ExamId, bool IncludeDeletedFiles, bool LoadFileData)
		{
			return base.GetMany<ExamFileDTO>(string.Format("examfile/examid/{0}?includedeletedfiles={1}&loadfiledata={2}", ExamId, IncludeDeletedFiles, LoadFileData), true);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000D11A File Offset: 0x0000B31A
		public ExamFileDTO LoadExamFileById(int ExamFileId)
		{
			return base.Get<ExamFileDTO>(string.Format("examfile/id/{0}", ExamFileId), true);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000D133 File Offset: 0x0000B333
		public int CreateExamFile(ExamFileDTO ExamFile)
		{
			return base.Post<ExamFileDTO, int>(ExamFile, "examfile");
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0000D141 File Offset: 0x0000B341
		public void DeleteExamFile(int ExamFileId)
		{
			base.Delete(string.Format("examfile/id/{0}", ExamFileId));
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0000D15C File Offset: 0x0000B35C
		public IList<ExamFileDTO> LoadExamFilesByExamCheckProfAltContactPermissions(int InstructorId, int AltContactId, int ExamId, bool IncludeDeletedFiles, bool LoadFileData)
		{
			return base.GetMany<ExamFileDTO>(string.Format("examfile/instructorid/{0}/altcontactid/{1}/examid/{2}?includedeletedfiles={3}&loadfiledata={4}", new object[]
			{
				InstructorId,
				AltContactId,
				ExamId,
				IncludeDeletedFiles,
				LoadFileData
			}), true);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0000D1AF File Offset: 0x0000B3AF
		public ExamFileDTO LoadExamFileByIdCheckProfAltContactPermissions(int ExamId, int InstructorId, int AltContactId, int ExamFileId)
		{
			return base.Get<ExamFileDTO>(string.Format("examfile/instructorid/{0}/altcontactid/{1}/examid/{2}/examfileid/{3}", new object[]
			{
				InstructorId,
				AltContactId,
				ExamId,
				ExamFileId
			}), true);
		}
	}
}
