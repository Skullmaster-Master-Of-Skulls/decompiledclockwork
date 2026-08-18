using System;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.Core.RequiredSessionForm;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.ICore.RequiredSessionForm;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.OperationContexts;

namespace TechnoPro.Common.Core.AlertTrigger
{
	// Token: 0x02000165 RID: 357
	public class AlertTriggerHelperManagers
	{
		// Token: 0x06001010 RID: 4112 RVA: 0x000757C2 File Offset: 0x000739C2
		public AlertTriggerHelperManagers(OperationContext opContext)
		{
			this._opContext = opContext;
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06001011 RID: 4113 RVA: 0x000757D4 File Offset: 0x000739D4
		public IDynamicFieldManager DynamicFieldManager
		{
			get
			{
				IDynamicFieldManager result;
				if ((result = this._dynamicFieldManager) == null)
				{
					result = (this._dynamicFieldManager = new DynamicFieldManager(this._opContext));
				}
				return result;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06001012 RID: 4114 RVA: 0x00075800 File Offset: 0x00073A00
		public IDynamicFormManager DynamicFormManager
		{
			get
			{
				IDynamicFormManager result;
				if ((result = this._dynamicFormManager) == null)
				{
					result = (this._dynamicFormManager = new DynamicFormManager(this._opContext));
				}
				return result;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06001013 RID: 4115 RVA: 0x0007582C File Offset: 0x00073A2C
		public IDynamicDataManager DynamicDataManager
		{
			get
			{
				IDynamicDataManager result;
				if ((result = this._dynamicDataManager) == null)
				{
					result = (this._dynamicDataManager = new DynamicDataManager(this._opContext));
				}
				return result;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06001014 RID: 4116 RVA: 0x00075858 File Offset: 0x00073A58
		public IWebSettingManager WebSettingManager
		{
			get
			{
				IWebSettingManager result;
				if ((result = this._webSettingManager) == null)
				{
					result = (this._webSettingManager = new WebSettingManager(new SettingsOperationContext(this._opContext)));
				}
				return result;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06001015 RID: 4117 RVA: 0x00075888 File Offset: 0x00073A88
		public IAccommodationsManager AccommodationsManager
		{
			get
			{
				IAccommodationsManager result;
				if ((result = this._accommodationsManager) == null)
				{
					result = (this._accommodationsManager = new AccommodationsManager(this._opContext));
				}
				return result;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06001016 RID: 4118 RVA: 0x000758B4 File Offset: 0x00073AB4
		public IStudentManagementManager StudentManagementManager
		{
			get
			{
				IStudentManagementManager result;
				if ((result = this._studentManagementManager) == null)
				{
					result = (this._studentManagementManager = new StudentManagementManager(this._opContext));
				}
				return result;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06001017 RID: 4119 RVA: 0x000758E0 File Offset: 0x00073AE0
		public IRequiredSessionFormManager RequiredSessionFormManager
		{
			get
			{
				IRequiredSessionFormManager result;
				if ((result = this._requiredSessionFormManager) == null)
				{
					result = (this._requiredSessionFormManager = new RequiredSessionFormManager(this._opContext));
				}
				return result;
			}
		}

		// Token: 0x040002D9 RID: 729
		private readonly OperationContext _opContext;

		// Token: 0x040002DA RID: 730
		private IDynamicFieldManager _dynamicFieldManager;

		// Token: 0x040002DB RID: 731
		private IDynamicFormManager _dynamicFormManager;

		// Token: 0x040002DC RID: 732
		private IDynamicDataManager _dynamicDataManager;

		// Token: 0x040002DD RID: 733
		private IWebSettingManager _webSettingManager;

		// Token: 0x040002DE RID: 734
		private IAccommodationsManager _accommodationsManager;

		// Token: 0x040002DF RID: 735
		private IStudentManagementManager _studentManagementManager;

		// Token: 0x040002E0 RID: 736
		private IRequiredSessionFormManager _requiredSessionFormManager;
	}
}
