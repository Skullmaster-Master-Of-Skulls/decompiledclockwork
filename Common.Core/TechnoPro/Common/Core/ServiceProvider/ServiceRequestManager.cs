using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO.Impl.ServiceProvider;
using TechnoPro.Common.DAO.ServiceProvider;
using TechnoPro.Common.ICore.ServiceProviders;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.Core.ServiceProvider
{
	// Token: 0x0200004E RID: 78
	public class ServiceRequestManager : IServiceRequestManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000331 RID: 817 RVA: 0x00011BEF File Offset: 0x0000FDEF
		public ServiceRequestManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ServiceRequestDAO(opContext);
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000332 RID: 818 RVA: 0x00011C0D File Offset: 0x0000FE0D
		// (set) Token: 0x06000333 RID: 819 RVA: 0x00011C15 File Offset: 0x0000FE15
		public OperationContext OpContext { get; set; }

		// Token: 0x06000334 RID: 820 RVA: 0x00011C20 File Offset: 0x0000FE20
		public SPRequestWithSubItems LoadRequestById(int SPRequestId, bool IncludeSubItems)
		{
			SPRequestWithSubItems result;
			if (IncludeSubItems)
			{
				result = this.dao.LoadRequestWithSubItemsById(SPRequestId);
			}
			else
			{
				SPRequest sprequest = this.dao.LoadRequestById(SPRequestId);
				bool flag = sprequest == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = new SPRequestWithSubItems
					{
						Request = sprequest
					};
				}
			}
			return result;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00011C70 File Offset: 0x0000FE70
		public IList<SPRequest> LoadRequests(DateTime StartDate, DateTime EndDate, bool IncludeAssigned, bool IncludeUnassigned, params int[] SPProviderTypeIds)
		{
			return this.dao.LoadRequests(StartDate, EndDate, IncludeAssigned, IncludeUnassigned, SPProviderTypeIds);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00011C94 File Offset: 0x0000FE94
		public void UpdateRequest(SPRequestWithSubItems RequestWithSubItems, bool UpdateSubItems)
		{
			if (UpdateSubItems)
			{
				this.dao.UpdateRequest(RequestWithSubItems, true);
			}
			else
			{
				this.dao.UpdateRequest(RequestWithSubItems.Request);
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00011CC9 File Offset: 0x0000FEC9
		public void DeleteRequest(int SPRequestId)
		{
			this.dao.DeleteRequest(SPRequestId);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00011CDC File Offset: 0x0000FEDC
		public int CreateRequestCourse(int SPRequestId, SPRequestCourse RequestCourse)
		{
			return this.dao.CreateRequestCourse(SPRequestId, RequestCourse);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00011CFB File Offset: 0x0000FEFB
		public void DeleteRequestCourse(int SPRequestCourseId)
		{
			this.dao.DeleteRequestCourse(SPRequestCourseId);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00011D0B File Offset: 0x0000FF0B
		public void UpdateRequestCourse(SPRequestCourse RequestCourse)
		{
			this.dao.UpdateRequestCourse(RequestCourse);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00011D1B File Offset: 0x0000FF1B
		public void DeleteRequestEvent(int SPRequestEventId)
		{
			this.dao.DeleteRequestEvent(SPRequestEventId);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00011D2B File Offset: 0x0000FF2B
		public void UpdateRequestEvent(SPRequestEvent RequestEvent)
		{
			this.dao.UpdateRequestEvent(RequestEvent);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00011D3C File Offset: 0x0000FF3C
		public int CreateRequestEvent(int SPRequestId, SPRequestEvent RequestEvent)
		{
			return this.dao.CreateRequestEvent(SPRequestId, RequestEvent);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00011D5C File Offset: 0x0000FF5C
		public int CreateRequest(SPRequestWithSubItems RequestWithSubItems, bool CreateSubItems)
		{
			return this.dao.CreateRequest(RequestWithSubItems, CreateSubItems);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00011D7C File Offset: 0x0000FF7C
		public void AssignOrUnassignRequestCourse(int SPRequestCourseId, SPRequestCourseAssignment CourseAssignment)
		{
			bool flag = CourseAssignment == null;
			if (flag)
			{
				this.dao.UnAssignRequestCourse(SPRequestCourseId);
			}
			else
			{
				this.dao.AssignRequestCourse(SPRequestCourseId, CourseAssignment);
			}
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00011DB4 File Offset: 0x0000FFB4
		public void AssignOrUnassignRequestEvent(int SPRequestEventId, SPRequestEventAssignment EventAssignment)
		{
			bool flag = EventAssignment == null;
			if (flag)
			{
				this.dao.UnAssignRequestEvent(SPRequestEventId);
			}
			else
			{
				this.dao.AssignRequestEvent(SPRequestEventId, EventAssignment);
			}
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00011DEB File Offset: 0x0000FFEB
		public void MergeDuplicateRequestsForTwoStudents(int PersonIdNew, int PersonIdOld)
		{
			this.dao.MergeDuplicateRequestsForTwoStudents(PersonIdNew, PersonIdOld);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x000072EA File Offset: 0x000054EA
		public SPRequestWithSubItems LoadRequestByStudentAndProviderType(int PersonId, int SPProviderTypeId, bool IncludeSubItems)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400009B RID: 155
		public IServiceRequestDAO dao;
	}
}
