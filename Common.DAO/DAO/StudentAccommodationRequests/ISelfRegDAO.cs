using System;
using System.Collections.Generic;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;

namespace TechnoPro.Common.DAO.StudentAccommodationRequests
{
	// Token: 0x02000029 RID: 41
	public interface ISelfRegDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060000AE RID: 174
		void CopyAccommodationsToCourse(int pid, int lucid, List<StudentCourseAccommodationModificationRequestItem> accommodationModificationRequests, IList<int> cidsToSkip);

		// Token: 0x060000AF RID: 175
		Pair<string, string> GetPersonIdAndLuCourseIdAsLongtermUrlStrings(int pid, int lucid);
	}
}
