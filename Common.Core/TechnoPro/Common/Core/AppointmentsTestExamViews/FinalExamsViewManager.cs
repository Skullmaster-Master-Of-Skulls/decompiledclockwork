using System;
using System.Collections.Generic;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.DAO.AppointmentsTestExamViews;
using TechnoPro.Common.DAO.Impl.AppointmentsTestExamViews;
using TechnoPro.Common.ICore.AppointmentsTestExamViews;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestExamViews.FinalExams;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.Core.AppointmentsTestExamViews
{
	// Token: 0x02000138 RID: 312
	public class FinalExamsViewManager : IFinalExamsViewManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000D8B RID: 3467 RVA: 0x0006225A File Offset: 0x0006045A
		public FinalExamsViewManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x0006226C File Offset: 0x0006046C
		// (set) Token: 0x06000D8D RID: 3469 RVA: 0x00062274 File Offset: 0x00060474
		public OperationContext OpContext { get; set; }

		// Token: 0x06000D8E RID: 3470 RVA: 0x00062280 File Offset: 0x00060480
		public IList<FinalExamsViewLight> LoadFinalExamsLight(FinalExamsContext context)
		{
			IFinalExamsViewDAO finalExamsViewDAO = new FinalExamsViewDAO(this.OpContext);
			return finalExamsViewDAO.LoadFinalExamsLight(context);
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x000622A8 File Offset: 0x000604A8
		public IList<PotentialFinalExamBooking> LoadUnbookedFinalExams(DateTime startDate, DateTime endDate, bool requiresApprovedSelfReg, bool requiresUnexpiredAccommodations, bool requiresLoaGeneratedByStaff)
		{
			IWebSettingManager webSettingManager = new WebSettingManager(new SettingsOperationContext(this.OpContext));
			int settingValue = webSettingManager.GetSettingValue<int>(Setting.TESTBOOKING_AccommodationsExpiryDateCid);
			IFinalExamsViewDAO finalExamsViewDAO = new FinalExamsViewDAO(this.OpContext);
			return finalExamsViewDAO.LoadUnbookedFinalExams(startDate, endDate, requiresApprovedSelfReg, requiresUnexpiredAccommodations, requiresLoaGeneratedByStaff, settingValue);
		}
	}
}
