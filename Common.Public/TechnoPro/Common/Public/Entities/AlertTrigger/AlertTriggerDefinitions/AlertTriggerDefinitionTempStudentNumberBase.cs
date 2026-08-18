using System;

namespace TechnoPro.Common.Public.Entities.AlertTrigger.AlertTriggerDefinitions
{
	// Token: 0x020005B2 RID: 1458
	[AlertDef(eAlertTriggerType.TempStudentNumber, "ts_", "AlertTriggerFunctionTempStudentNumber")]
	[Serializable]
	public class AlertTriggerDefinitionTempStudentNumberBase : AlertTriggerDefinitionBase
	{
		// Token: 0x170013CE RID: 5070
		// (get) Token: 0x06002F2A RID: 12074 RVA: 0x00033C33 File Offset: 0x00031E33
		// (set) Token: 0x06002F2B RID: 12075 RVA: 0x00033C3B File Offset: 0x00031E3B
		public int MinNumCharacters { get; set; }

		// Token: 0x170013CF RID: 5071
		// (get) Token: 0x06002F2C RID: 12076 RVA: 0x00033C44 File Offset: 0x00031E44
		// (set) Token: 0x06002F2D RID: 12077 RVA: 0x00033C4C File Offset: 0x00031E4C
		public int MaxNumCharacters { get; set; }

		// Token: 0x170013D0 RID: 5072
		// (get) Token: 0x06002F2E RID: 12078 RVA: 0x00033C55 File Offset: 0x00031E55
		// (set) Token: 0x06002F2F RID: 12079 RVA: 0x00033C5D File Offset: 0x00031E5D
		public bool AllowLettersInStudentNumber { get; set; }
	}
}
