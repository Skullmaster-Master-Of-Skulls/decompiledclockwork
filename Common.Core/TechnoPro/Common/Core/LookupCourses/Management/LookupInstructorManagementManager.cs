using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.DAO.Impl.LookupCourses.Management;
using TechnoPro.Common.DAO.LookupCourses.Management;
using TechnoPro.Common.ICore.LookupCourses.Management;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.LookupCourses.Management;

namespace TechnoPro.Common.Core.LookupCourses.Management
{
	// Token: 0x020000D8 RID: 216
	public class LookupInstructorManagementManager : ILookupInstructorManagementManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x00038603 File Offset: 0x00036803
		// (set) Token: 0x0600084C RID: 2124 RVA: 0x0003860B File Offset: 0x0003680B
		public OperationContext OpContext { get; set; }

		// Token: 0x0600084D RID: 2125 RVA: 0x00038614 File Offset: 0x00036814
		public void DeleteInstructor(int instructorId)
		{
			((ILookupInstructorManagementDAO)new LookupInstructorManagementDAO
			{
				OpContext = this.OpContext
			}).DeleteInstructor(instructorId);
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00038640 File Offset: 0x00036840
		public LookInstructorForManagementList LoadLookupInstructorsForManagement(int startIndex, int count)
		{
			IList<LookupInstructorForManagement> list = ((ILookupInstructorManagementDAO)new LookupInstructorManagementDAO
			{
				OpContext = this.OpContext
			}).LoadAllLookupInstructorsForManagement();
			List<LookupInstructorForManagement> list2 = (list != null) ? list.ToList<LookupInstructorForManagement>() : null;
			bool flag = list2 == null;
			LookInstructorForManagementList result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new LookInstructorForManagementList
				{
					Instructors = list2.GetListRange(startIndex, count),
					StartIndex = startIndex,
					Count = count,
					TotalCount = list2.Count
				};
			}
			return result;
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x000386B8 File Offset: 0x000368B8
		public void MergeInstructors(int instructor1Id, int instructor2Id)
		{
			ILookupInstructorManagementDAO lookupInstructorManagementDAO = new LookupInstructorManagementDAO();
			lookupInstructorManagementDAO.OpContext = this.OpContext;
			lookupInstructorManagementDAO.SwapInstructors(instructor2Id, instructor1Id);
			lookupInstructorManagementDAO.DeleteInstructor(instructor2Id);
		}
	}
}
