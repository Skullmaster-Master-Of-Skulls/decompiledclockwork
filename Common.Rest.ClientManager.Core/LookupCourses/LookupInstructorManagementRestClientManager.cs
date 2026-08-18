using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses.Management;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses.Management.Parameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.LookupCourses
{
	// Token: 0x02000035 RID: 53
	public class LookupInstructorManagementRestClientManager : BearerTokenRestProxy<ILookupInstructorManagementClientManager>, ILookupInstructorManagementClientManager, IWebService
	{
		// Token: 0x060001EF RID: 495 RVA: 0x00006CB2 File Offset: 0x00004EB2
		public LookupInstructorManagementRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00006CBC File Offset: 0x00004EBC
		public LookupInstructorManagementRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00006CC7 File Offset: 0x00004EC7
		public LookInstructorForManagementListDTO LoadLookupInstructorsForManagement(int startIndex, int count)
		{
			return base.Get<LookInstructorForManagementListDTO>(string.Format("lookupinstructormanagement/startindex/{0}/count/{1}", startIndex, count), true);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00006CE6 File Offset: 0x00004EE6
		public void DeleteInstructor(int instructorId)
		{
			base.Delete(string.Format("lookupinstructormanagement/instructorid/{0}", instructorId));
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00006D00 File Offset: 0x00004F00
		public void MergeInstructors(int instructor1Id, int instructor2Id)
		{
			MergeInstructorsReq mergeInstructorsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MergeInstructorsReq>();
			mergeInstructorsReq.InstructorId1 = instructor1Id;
			mergeInstructorsReq.InstructorId2 = instructor2Id;
			base.Post<MergeInstructorsReq>(mergeInstructorsReq, "lookupinstructormanagement/merge");
		}
	}
}
