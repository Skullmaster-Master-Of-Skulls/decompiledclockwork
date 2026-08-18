using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.LookupCourses
{
	// Token: 0x0200005B RID: 91
	public interface ISessionDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600021B RID: 539
		void SetSessionChooserDefaultValue(DateTime DtpNow);

		// Token: 0x0600021C RID: 540
		DateTime? GetSessionChooserDefaultValue();
	}
}
