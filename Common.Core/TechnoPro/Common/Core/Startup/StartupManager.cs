using System;
using System.Collections.Generic;
using TechnoPro.Common.Core.AppointmentsWorkshops;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.ICore.AppointmentsWorkshops;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.Startup;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsWorkshops;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.Startup;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Core.Startup
{
	// Token: 0x02000042 RID: 66
	public class StartupManager : IStartupManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060002AD RID: 685 RVA: 0x000101CF File Offset: 0x0000E3CF
		public StartupManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x000101E4 File Offset: 0x0000E3E4
		public CacheClusterFull LoadCacheClusterFull()
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			IDynamicFormManager dynamicFormManager = new DynamicFormManager(this.OpContext);
			IWorkshopDefinitionManager workshopDefinitionManager = new WorkshopDefinitionManager(this.OpContext);
			List<OldUserSetting> userSettings = oldUserSettingManager.LoadAllUserSettings(this.OpContext.WhoAmI);
			IList<DynamicFormWithExtendedInfo> activeDynamicForms = dynamicFormManager.LoadActiveFormsWithExtendedInfo();
			IList<WorkshopDefinition> workshopDefinitions = workshopDefinitionManager.LoadWorkshopDefinitions();
			return new CacheClusterFull
			{
				PersonId = this.OpContext.WhoAmI,
				UserSettings = userSettings,
				UserPermissions = null,
				ActiveDynamicForms = activeDynamicForms,
				WorkshopDefinitions = workshopDefinitions
			};
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0001027B File Offset: 0x0000E47B
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x00010283 File Offset: 0x0000E483
		public OperationContext OpContext { get; set; }
	}
}
