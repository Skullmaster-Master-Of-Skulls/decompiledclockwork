using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Academic;

namespace TechnoPro.Common.ICore.Academic
{
	// Token: 0x020000F9 RID: 249
	public interface ISemesterManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000817 RID: 2071
		int CreateSemester(Semester semester);

		// Token: 0x06000818 RID: 2072
		void DeleteSemester(int semesterId);

		// Token: 0x06000819 RID: 2073
		void UpdateSemester(Semester semester);

		// Token: 0x0600081A RID: 2074
		Semester LoadCurrentSemester();

		// Token: 0x0600081B RID: 2075
		Semester LoadNextSemester();

		// Token: 0x0600081C RID: 2076
		Semester LoadSemesterById(int semesterId);
	}
}
