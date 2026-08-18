using System;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Public.Entities.AlternativeFormat
{
	// Token: 0x02000573 RID: 1395
	[Serializable]
	public class DynamicFormSettingAttribute : Attribute
	{
		// Token: 0x170012D1 RID: 4817
		// (get) Token: 0x06002CE4 RID: 11492 RVA: 0x00031CB4 File Offset: 0x0002FEB4
		// (set) Token: 0x06002CE5 RID: 11493 RVA: 0x00031CBC File Offset: 0x0002FEBC
		public eSettingCode DynamicFormSetting { get; set; }

		// Token: 0x06002CE6 RID: 11494 RVA: 0x00031CC5 File Offset: 0x0002FEC5
		public DynamicFormSettingAttribute(eSettingCode dynamicFormSetting)
		{
			this.DynamicFormSetting = dynamicFormSetting;
		}
	}
}
