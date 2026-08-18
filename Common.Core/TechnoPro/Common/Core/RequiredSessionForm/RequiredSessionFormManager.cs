using System;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.LookupCourses;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.LookupCourses;
using TechnoPro.Common.ICore.RequiredSessionForm;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Core.RequiredSessionForm
{
	// Token: 0x02000059 RID: 89
	public class RequiredSessionFormManager : IRequiredSessionFormManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600039E RID: 926 RVA: 0x0001255E File Offset: 0x0001075E
		public RequiredSessionFormManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600039F RID: 927 RVA: 0x00012570 File Offset: 0x00010770
		// (set) Token: 0x060003A0 RID: 928 RVA: 0x00012578 File Offset: 0x00010778
		public OperationContext OpContext { get; set; }

		// Token: 0x060003A1 RID: 929 RVA: 0x00012584 File Offset: 0x00010784
		public int LoadInfoPmIdForCurrentSession(int StudentPersonId, int ScreenNum)
		{
			return this.LoadInfoPmIdForSession(StudentPersonId, ScreenNum, DateTime.Now);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x000125A4 File Offset: 0x000107A4
		public int LoadInfoPmIdForSession(int StudentPersonId, int ScreenNum, DateTime DateInSession)
		{
			bool flag = StudentPersonId < 1 || ScreenNum < 1;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				ISessionManager sessionManager = new SessionManager(this.OpContext);
				Session session = sessionManager.GetSession(DateInSession);
				IDynamicPerDateDataManager dynamicPerDateDataManager = new DynamicPerDateDataManager(this.OpContext);
				PerDateEntry existingPerDateEntry = dynamicPerDateDataManager.GetExistingPerDateEntry(StudentPersonId, ScreenNum, session);
				result = ((existingPerDateEntry != null) ? existingPerDateEntry.AppointmentId : 0);
			}
			return result;
		}
	}
}
