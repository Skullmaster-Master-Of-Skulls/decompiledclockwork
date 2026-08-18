using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.MergeDuplicates;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.MergeDuplicates;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.MergeDuplicates
{
	// Token: 0x02000039 RID: 57
	public class MergeDuplicateStudentClientManager : IMergeDuplicateStudentClientManager, IWebService
	{
		// Token: 0x0600020C RID: 524 RVA: 0x00009BD8 File Offset: 0x00007DD8
		public DuplicateStudentSetDTO LoadDuplicateStudentPreviewInfo(DuplicateStudentSetDTO DuplicateSet)
		{
			LoadDuplicateStudentPreviewInfoReq loadDuplicateStudentPreviewInfoReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadDuplicateStudentPreviewInfoReq>();
			loadDuplicateStudentPreviewInfoReq.DuplicateStudentSet = DuplicateSet;
			return ClientServiceFactory.GetClientInstance<IMergeDuplicateStudent>().LoadDuplicateStudentPreviewInfo(loadDuplicateStudentPreviewInfoReq).DuplicateStudentSet;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00009C10 File Offset: 0x00007E10
		public IList<PotentialDuplicateStudentSetDTO> FindPotentialDuplicateStudents()
		{
			FindPotentialDuplicateStudentsReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<FindPotentialDuplicateStudentsReq>();
			return ClientServiceFactory.GetClientInstance<IMergeDuplicateStudent>().FindPotentialDuplicateStudents(request).PotentialDuplicateSets;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00009C40 File Offset: 0x00007E40
		public void MergeDuplicateStudents(DuplicateStudentSetDTO DuplicateStudentSet)
		{
			MergeDuplicateStudentsReq mergeDuplicateStudentsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MergeDuplicateStudentsReq>();
			mergeDuplicateStudentsReq.DuplicateStudentSet = DuplicateStudentSet;
			ClientServiceFactory.GetClientInstance<IMergeDuplicateStudent>().MergeDuplicateStudents(mergeDuplicateStudentsReq);
		}
	}
}
