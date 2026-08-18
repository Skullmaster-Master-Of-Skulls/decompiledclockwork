using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses.Management.Parameters;
using TechnoPro.Common.Core.LookupCourses.Management;
using TechnoPro.Common.Core.Mappers.LookupCourses.Management;
using TechnoPro.Common.ICore.LookupCourses.Management;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.LookupCourses.Management;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000061 RID: 97
	public class LookupInstructorManagementServiceManager : ILookupInstructorManagement, IService
	{
		// Token: 0x06000395 RID: 917 RVA: 0x00010A8C File Offset: 0x0000EC8C
		public LoadLookupInstructorsForManagementResp LoadLookupInstructorsForManagement(LoadLookupInstructorsForManagementReq Request)
		{
			LookInstructorForManagementList lookInstructorForManagementList = ((ILookupInstructorManagementManager)new LookupInstructorManagementManager
			{
				OpContext = Request.GetOperationContext()
			}).LoadLookupInstructorsForManagement(Request.StartIndex, Request.Count);
			return new LoadLookupInstructorsForManagementResp
			{
				InstructorList = ((lookInstructorForManagementList != null) ? lookInstructorForManagementList.ToDTO() : null)
			};
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00010ADC File Offset: 0x0000ECDC
		public DeleteInstructorResp DeleteInstructor(DeleteInstructorReq Request)
		{
			((ILookupInstructorManagementManager)new LookupInstructorManagementManager
			{
				OpContext = Request.GetOperationContext()
			}).DeleteInstructor(Request.InstructorId);
			return new DeleteInstructorResp();
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00010B14 File Offset: 0x0000ED14
		public MergeInstructorsResp MergeInstructors(MergeInstructorsReq Request)
		{
			((ILookupInstructorManagementManager)new LookupInstructorManagementManager
			{
				OpContext = Request.GetOperationContext()
			}).MergeInstructors(Request.InstructorId1, Request.InstructorId2);
			return new MergeInstructorsResp();
		}
	}
}
