using System;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses.Management;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses.Management.Parameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.LookupCourses
{
	// Token: 0x02000041 RID: 65
	public class LookupInstructorManagementClientManager : ILookupInstructorManagementClientManager, IWebService
	{
		// Token: 0x0600026D RID: 621 RVA: 0x0000B6CC File Offset: 0x000098CC
		public LookInstructorForManagementListDTO LoadLookupInstructorsForManagement(int startIndex, int count)
		{
			LoadLookupInstructorsForManagementReq loadLookupInstructorsForManagementReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadLookupInstructorsForManagementReq>();
			loadLookupInstructorsForManagementReq.StartIndex = startIndex;
			loadLookupInstructorsForManagementReq.Count = count;
			LoadLookupInstructorsForManagementResp loadLookupInstructorsForManagementResp = ClientServiceFactory.GetClientInstance<ILookupInstructorManagement>().LoadLookupInstructorsForManagement(loadLookupInstructorsForManagementReq);
			return (loadLookupInstructorsForManagementResp != null) ? loadLookupInstructorsForManagementResp.InstructorList : null;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000B710 File Offset: 0x00009910
		public void DeleteInstructor(int instructorId)
		{
			DeleteInstructorReq deleteInstructorReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteInstructorReq>();
			deleteInstructorReq.InstructorId = instructorId;
			ClientServiceFactory.GetClientInstance<ILookupInstructorManagement>().DeleteInstructor(deleteInstructorReq);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000B740 File Offset: 0x00009940
		public void MergeInstructors(int instructor1Id, int instructor2Id)
		{
			MergeInstructorsReq mergeInstructorsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MergeInstructorsReq>();
			mergeInstructorsReq.InstructorId1 = instructor1Id;
			mergeInstructorsReq.InstructorId2 = instructor2Id;
			ClientServiceFactory.GetClientInstance<ILookupInstructorManagement>().MergeInstructors(mergeInstructorsReq);
		}
	}
}
