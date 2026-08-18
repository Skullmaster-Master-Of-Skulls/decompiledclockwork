using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.MergeDuplicates;
using TechnoPro.Common.ClientManager.ICore.MergeDuplicates;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.MergeDuplicates
{
	// Token: 0x0200002E RID: 46
	public class MergeDuplicateStudentRestClientManager : BearerTokenRestProxy<IMergeDuplicateStudentClientManager>, IMergeDuplicateStudentClientManager, IWebService
	{
		// Token: 0x060001A7 RID: 423 RVA: 0x00005F02 File Offset: 0x00004102
		public MergeDuplicateStudentRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00005F0C File Offset: 0x0000410C
		public MergeDuplicateStudentRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00005F17 File Offset: 0x00004117
		public DuplicateStudentSetDTO LoadDuplicateStudentPreviewInfo(DuplicateStudentSetDTO DuplicateSet)
		{
			return base.Post<DuplicateStudentSetDTO, DuplicateStudentSetDTO>(DuplicateSet, "mergeduplicatestudent/loadduplicatestudentpreviewinfo");
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00005F25 File Offset: 0x00004125
		public IList<PotentialDuplicateStudentSetDTO> FindPotentialDuplicateStudents()
		{
			return base.GetMany<PotentialDuplicateStudentSetDTO>("mergeduplicatestudent/findpotentialduplicatestudents", true);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00005F33 File Offset: 0x00004133
		public void MergeDuplicateStudents(DuplicateStudentSetDTO DuplicateStudentSet)
		{
			base.Post<DuplicateStudentSetDTO>(DuplicateStudentSet, "mergeduplicatestudent/mergeduplicatestudents");
		}
	}
}
