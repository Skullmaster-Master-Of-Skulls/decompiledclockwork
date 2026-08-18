using System;
using TechnoPro.Common.DAO.Academic;
using TechnoPro.Common.DAO.Impl.Academic;
using TechnoPro.Common.ICore.Academic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Academic;

namespace TechnoPro.Common.Core.Academic
{
	// Token: 0x0200017A RID: 378
	public class SemesterManager : ISemesterManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06001053 RID: 4179 RVA: 0x00078780 File Offset: 0x00076980
		public SemesterManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x00078792 File Offset: 0x00076992
		// (set) Token: 0x06001055 RID: 4181 RVA: 0x0007879A File Offset: 0x0007699A
		public OperationContext OpContext { get; set; }

		// Token: 0x06001056 RID: 4182 RVA: 0x000787A4 File Offset: 0x000769A4
		public int CreateSemester(Semester semester)
		{
			ISemesterDAO semesterDAO = new SemesterDAO(this.OpContext);
			return semesterDAO.CreateSemester(semester);
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x000787CC File Offset: 0x000769CC
		public void DeleteSemester(int semesterId)
		{
			ISemesterDAO semesterDAO = new SemesterDAO(this.OpContext);
			semesterDAO.DeleteSemester(semesterId);
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x000787F0 File Offset: 0x000769F0
		public void UpdateSemester(Semester semester)
		{
			ISemesterDAO semesterDAO = new SemesterDAO(this.OpContext);
			semesterDAO.UpdateSemester(semester);
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x00078814 File Offset: 0x00076A14
		public Semester LoadCurrentSemester()
		{
			ISemesterDAO semesterDAO = new SemesterDAO(this.OpContext);
			return semesterDAO.LoadCurrentSemester();
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x00078838 File Offset: 0x00076A38
		public Semester LoadNextSemester()
		{
			ISemesterDAO semesterDAO = new SemesterDAO(this.OpContext);
			return semesterDAO.LoadNextSemester();
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0007885C File Offset: 0x00076A5C
		public Semester LoadSemesterById(int semesterId)
		{
			ISemesterDAO semesterDAO = new SemesterDAO(this.OpContext);
			return semesterDAO.LoadSemesterById(semesterId);
		}
	}
}
