using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.MergeDuplicates;
using TechnoPro.Common.Core.Mappers.MergeDuplicatesStudents;
using TechnoPro.Common.Core.MergeDuplicates;
using TechnoPro.Common.ICore.MergeDuplicates;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.MergeDuplicates.Students;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200006C RID: 108
	public class MergeDuplicateStudentServiceManager : IMergeDuplicateStudent, IService
	{
		// Token: 0x060003FC RID: 1020 RVA: 0x000130F0 File Offset: 0x000112F0
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00013104 File Offset: 0x00011304
		public FindPotentialDuplicateStudentsResp FindPotentialDuplicateStudents(FindPotentialDuplicateStudentsReq Request)
		{
			IMergeDuplicateStudentManager mergeDuplicateStudentManager = new MergeDuplicateStudentManager(Request.GetOperationContext());
			IList<PotentialDuplicateStudentSet> list = mergeDuplicateStudentManager.FindPotentialDuplicateStudents(1);
			FindPotentialDuplicateStudentsResp findPotentialDuplicateStudentsResp = new FindPotentialDuplicateStudentsResp();
			IList<PotentialDuplicateStudentSetDTO> potentialDuplicateSets;
			if (list != null)
			{
				potentialDuplicateSets = list.ToList<PotentialDuplicateStudentSet>().ConvertAll<PotentialDuplicateStudentSetDTO>((PotentialDuplicateStudentSet f) => f.ToDTO());
			}
			else
			{
				potentialDuplicateSets = null;
			}
			findPotentialDuplicateStudentsResp.PotentialDuplicateSets = potentialDuplicateSets;
			return findPotentialDuplicateStudentsResp;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00013168 File Offset: 0x00011368
		public MergeDuplicateStudentsResp MergeDuplicateStudents(MergeDuplicateStudentsReq Request)
		{
			IMergeDuplicateStudentManager mergeDuplicateStudentManager = new MergeDuplicateStudentManager(Request.GetOperationContext());
			mergeDuplicateStudentManager.MergeDuplicateStudents(Request.DuplicateStudentSet.ToDomainObject());
			return new MergeDuplicateStudentsResp();
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x000131A0 File Offset: 0x000113A0
		public LoadDuplicateStudentPreviewInfoResp LoadDuplicateStudentPreviewInfo(LoadDuplicateStudentPreviewInfoReq Request)
		{
			IMergeDuplicateStudentManager mergeDuplicateStudentManager = new MergeDuplicateStudentManager(Request.GetOperationContext());
			DuplicateStudentSet duplicateStudentSet = mergeDuplicateStudentManager.LoadDuplicateStudentPreviewInfo(Request.DuplicateStudentSet.ToDomainObject());
			DuplicateStudentSetDTO duplicateStudentSet2 = (duplicateStudentSet == null) ? null : duplicateStudentSet.ToDTO();
			return new LoadDuplicateStudentPreviewInfoResp
			{
				DuplicateStudentSet = duplicateStudentSet2
			};
		}
	}
}
