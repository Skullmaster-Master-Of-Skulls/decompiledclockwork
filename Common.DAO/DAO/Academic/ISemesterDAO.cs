using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Academic;

namespace TechnoPro.Common.DAO.Academic
{
	// Token: 0x020000D1 RID: 209
	public interface ISemesterDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600061B RID: 1563
		int CreateSemester(Semester semester);

		// Token: 0x0600061C RID: 1564
		void DeleteSemester(int semesterId);

		// Token: 0x0600061D RID: 1565
		void UpdateSemester(Semester semester);

		// Token: 0x0600061E RID: 1566
		Semester LoadCurrentSemester();

		// Token: 0x0600061F RID: 1567
		Semester LoadNextSemester();

		// Token: 0x06000620 RID: 1568
		Semester LoadSemesterById(int semesterId);
	}
}
