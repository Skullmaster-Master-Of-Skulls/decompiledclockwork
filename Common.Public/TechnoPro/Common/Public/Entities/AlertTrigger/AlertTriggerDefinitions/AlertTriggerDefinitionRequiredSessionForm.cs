using System;
using TechnoPro.Common.Public.Entities.RequiredSessionForm;

namespace TechnoPro.Common.Public.Entities.AlertTrigger.AlertTriggerDefinitions
{
	// Token: 0x020005B1 RID: 1457
	[AlertDef(eAlertTriggerType.RequiredSessionForm, "rf_", "AlertTriggerFunctionRequiredSessionForm")]
	[Serializable]
	public class AlertTriggerDefinitionRequiredSessionForm : AlertTriggerDefinitionRequiredSessionFormBase, IAlertTriggerDefinition, IAlertTriggerDefinitionCommon
	{
		// Token: 0x170013CC RID: 5068
		// (get) Token: 0x06002F26 RID: 12070 RVA: 0x00033BFC File Offset: 0x00031DFC
		public override string RequiredSessionFormRuleName
		{
			get
			{
				RequiredSessionFormItem requiredSessionFormRule = this.RequiredSessionFormRule;
				return ((requiredSessionFormRule != null) ? requiredSessionFormRule.Name : null) ?? "";
			}
		}

		// Token: 0x170013CD RID: 5069
		// (get) Token: 0x06002F27 RID: 12071 RVA: 0x00033C19 File Offset: 0x00031E19
		// (set) Token: 0x06002F28 RID: 12072 RVA: 0x00033C21 File Offset: 0x00031E21
		public RequiredSessionFormItem RequiredSessionFormRule { get; set; }
	}
}
