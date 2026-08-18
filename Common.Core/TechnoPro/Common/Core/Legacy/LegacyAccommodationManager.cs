using System;
using TechnoPro.Common.Core.CourseRegistrations;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.Impl.Legacy;
using TechnoPro.Common.DAO.Legacy;
using TechnoPro.Common.ICore.CourseRegistrations;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.Legacy;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.Core.Legacy
{
	// Token: 0x020000D9 RID: 217
	public class LegacyAccommodationManager : ILegacyAccommodationManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000851 RID: 2129 RVA: 0x000386EA File Offset: 0x000368EA
		public LegacyAccommodationManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x000386FC File Offset: 0x000368FC
		// (set) Token: 0x06000853 RID: 2131 RVA: 0x00038704 File Offset: 0x00036904
		public OperationContext OpContext { get; set; }

		// Token: 0x06000854 RID: 2132 RVA: 0x00038710 File Offset: 0x00036910
		public void LogLoaIssuedDate(int pid, int lucid, string loaString)
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			bool flag = pid <= 0 || lucid <= 0;
			if (!flag)
			{
				ICourseRegistrationManager courseRegistrationManager = new CourseRegistrationManager(this.OpContext);
				IAccommodationsManager accommodationsManager = new AccommodationsManager(this.OpContext);
				accommodationsManager.MarkAccommodationLetterIssued(pid, new int[]
				{
					lucid
				});
				ILegacyAccommodationDAO legacyAccommodationDAO = new LegacyAccommodationDAO(this.OpContext);
				legacyAccommodationDAO.AddAccommodationLoaIssuedRow(pid, lucid, loaString);
			}
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00038784 File Offset: 0x00036984
		public void CreateOrAddAccommodationApprovalNote(int pid, string note)
		{
			ILegacyAccommodationDAO legacyAccommodationDAO = new LegacyAccommodationDAO(this.OpContext);
			legacyAccommodationDAO.CreateOrAddAccommodationApprovalNote(pid, note);
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x000387A8 File Offset: 0x000369A8
		public string GetAccommodationsApprovalSummary(int pid)
		{
			ILegacyAccommodationDAO legacyAccommodationDAO = new LegacyAccommodationDAO(this.OpContext);
			return legacyAccommodationDAO.GetAccommodationsApprovalSummary(pid);
		}
	}
}
