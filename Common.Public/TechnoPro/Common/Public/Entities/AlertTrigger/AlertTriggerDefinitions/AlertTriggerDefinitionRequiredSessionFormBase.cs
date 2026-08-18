using System;

namespace TechnoPro.Common.Public.Entities.AlertTrigger.AlertTriggerDefinitions
{
	// Token: 0x020005B0 RID: 1456
	[AlertDef(eAlertTriggerType.RequiredSessionForm, "rf_", "AlertTriggerFunctionRequiredSessionForm")]
	[Serializable]
	public class AlertTriggerDefinitionRequiredSessionFormBase : AlertTriggerDefinitionBase
	{
		// Token: 0x06002F21 RID: 12065 RVA: 0x00033768 File Offset: 0x00031968
		public AlertTriggerDefinitionRequiredSessionFormBase()
		{
		}

		// Token: 0x06002F22 RID: 12066 RVA: 0x00033BCE File Offset: 0x00031DCE
		public AlertTriggerDefinitionRequiredSessionFormBase(string requiredSessionFormRuleName)
		{
			this.SetValues(requiredSessionFormRuleName);
		}

		// Token: 0x06002F23 RID: 12067 RVA: 0x00033BE0 File Offset: 0x00031DE0
		public void SetValues(string requiredSessionFormRuleName)
		{
			this.RequiredSessionFormRuleName = requiredSessionFormRuleName;
		}

		// Token: 0x170013CB RID: 5067
		// (get) Token: 0x06002F24 RID: 12068 RVA: 0x00033BEB File Offset: 0x00031DEB
		// (set) Token: 0x06002F25 RID: 12069 RVA: 0x00033BF3 File Offset: 0x00031DF3
		public virtual string RequiredSessionFormRuleName { get; private set; }
	}
}
