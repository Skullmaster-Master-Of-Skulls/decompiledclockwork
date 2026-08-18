using System;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Public.Entities.AlertTrigger.AlertTriggerDefinitions
{
	// Token: 0x020005AF RID: 1455
	[AlertDef(eAlertTriggerType.MissingInfo, "mi_", "AlertTriggerFunctionMissingInfo")]
	[Serializable]
	public class AlertTriggerDefinitionMissingInfo : AlertTriggerDefinitionMissingInfoBase, IAlertTriggerDefinition, IAlertTriggerDefinitionCommon
	{
		// Token: 0x170013C8 RID: 5064
		// (get) Token: 0x06002F1C RID: 12060 RVA: 0x00033B56 File Offset: 0x00031D56
		public override int ControlId
		{
			get
			{
				DynamicFieldWithForm fieldWithForm = this.FieldWithForm;
				return (fieldWithForm != null) ? fieldWithForm.ControlId : 0;
			}
		}

		// Token: 0x170013C9 RID: 5065
		// (get) Token: 0x06002F1D RID: 12061 RVA: 0x00033B6C File Offset: 0x00031D6C
		public override int ScreenNum
		{
			get
			{
				DynamicFieldWithForm fieldWithForm = this.FieldWithForm;
				int? num;
				if (fieldWithForm == null)
				{
					num = null;
				}
				else
				{
					DynamicForm form = fieldWithForm.Form;
					num = ((form != null) ? new int?(form.ScreenNum) : null);
				}
				int? num2 = num;
				return num2.GetValueOrDefault();
			}
		}

		// Token: 0x170013CA RID: 5066
		// (get) Token: 0x06002F1E RID: 12062 RVA: 0x00033BB4 File Offset: 0x00031DB4
		// (set) Token: 0x06002F1F RID: 12063 RVA: 0x00033BBC File Offset: 0x00031DBC
		public DynamicFieldWithForm FieldWithForm { get; set; }
	}
}
