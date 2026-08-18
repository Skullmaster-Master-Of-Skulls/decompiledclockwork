using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.MergeDuplicates;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.MergeDuplicates
{
	// Token: 0x02000035 RID: 53
	public interface IMergeDuplicateStudentClientManager : IWebService
	{
		// Token: 0x06000177 RID: 375
		DuplicateStudentSetDTO LoadDuplicateStudentPreviewInfo(DuplicateStudentSetDTO DuplicateSet);

		// Token: 0x06000178 RID: 376
		IList<PotentialDuplicateStudentSetDTO> FindPotentialDuplicateStudents();

		// Token: 0x06000179 RID: 377
		void MergeDuplicateStudents(DuplicateStudentSetDTO DuplicateStudentSet);
	}
}
