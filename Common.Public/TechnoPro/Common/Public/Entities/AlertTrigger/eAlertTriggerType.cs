using System;
using TechnoPro.Common.Public.Entities.AlertTrigger.AlertTriggerDefinitions;

namespace TechnoPro.Common.Public.Entities.AlertTrigger
{
	// Token: 0x020005A4 RID: 1444
	[Serializable]
	public enum eAlertTriggerType
	{
		// Token: 0x0400209C RID: 8348
		[AlertTriggerType("Unknown", "Not in use", null, null, IsDisabled = true, IsForInternalUseOnly = true)]
		Unknown,
		// Token: 0x0400209D RID: 8349
		[AlertTriggerType("Existing data", "The alert will trigger if the student has data for any one of the specified fields.", typeof(AlertTriggerDefinitionExistingInfo), typeof(AlertTriggerDefinitionExistingInfoBase))]
		ExistingInfo,
		// Token: 0x0400209E RID: 8350
		[AlertTriggerType("Accommodations expiry date", "The alert will trigger if the student's accommodation expiry date is getting close or has passed, and optionally if the expiry date has not been filled in.", typeof(AlertTriggerDefinitionExpiredAccommodations), typeof(AlertTriggerDefinitionExpiredAccommodationsBase))]
		ExpiredAccommodations,
		// Token: 0x0400209F RID: 8351
		[AlertTriggerType("Missing info", "The alert will trigger if the student has at least one field not filled in on the specified form.", typeof(AlertTriggerDefinitionMissingInfo), typeof(AlertTriggerDefinitionMissingInfoBase))]
		MissingInfo,
		// Token: 0x040020A0 RID: 8352
		[AlertTriggerType("Required session form", "The alert will trigger if the student has not filled in one of the required forms in the current session.", typeof(AlertTriggerDefinitionRequiredSessionForm), typeof(AlertTriggerDefinitionRequiredSessionFormBase), IsForInternalUseOnly = true)]
		RequiredSessionForm,
		// Token: 0x040020A1 RID: 8353
		[AlertTriggerType("Invalid student number", "The alert will trigger if the student has an invalid/temporary student number that needs to be corrected.", typeof(AlertTriggerDefinitionTempStudentNumber), typeof(AlertTriggerDefinitionTempStudentNumberBase))]
		TempStudentNumber
	}
}
