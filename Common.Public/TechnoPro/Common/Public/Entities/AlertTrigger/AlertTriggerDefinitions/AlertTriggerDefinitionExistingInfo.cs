using System;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Public.Entities.AlertTrigger.AlertTriggerDefinitions
{
	// Token: 0x020005AB RID: 1451
	[AlertDef(eAlertTriggerType.ExistingInfo, "ei_", "AlertTriggerFunctionExistingInfo")]
	[Serializable]
	public class AlertTriggerDefinitionExistingInfo : AlertTriggerDefinitionExistingInfoBase, IAlertTriggerDefinition, IAlertTriggerDefinitionCommon
	{
		// Token: 0x170013C1 RID: 5057
		// (get) Token: 0x06002F0A RID: 12042 RVA: 0x00033A64 File Offset: 0x00031C64
		public override int ControlId
		{
			get
			{
				DynamicFieldWithForm fieldWithForm = this.FieldWithForm;
				return (fieldWithForm != null) ? fieldWithForm.ControlId : 0;
			}
		}

		// Token: 0x170013C2 RID: 5058
		// (get) Token: 0x06002F0B RID: 12043 RVA: 0x00033A78 File Offset: 0x00031C78
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

		// Token: 0x170013C3 RID: 5059
		// (get) Token: 0x06002F0C RID: 12044 RVA: 0x00033AC0 File Offset: 0x00031CC0
		// (set) Token: 0x06002F0D RID: 12045 RVA: 0x00033AC8 File Offset: 0x00031CC8
		public DynamicFieldWithForm FieldWithForm { get; set; }
	}
}
