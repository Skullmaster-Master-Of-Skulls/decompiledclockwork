using System;

namespace TechnoPro.Common.Public.Entities.Intake
{
	// Token: 0x02000326 RID: 806
	public class IntakeEntryQueueItem : IntakeEntry
	{
		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x0600191E RID: 6430 RVA: 0x0001DBEB File Offset: 0x0001BDEB
		// (set) Token: 0x0600191F RID: 6431 RVA: 0x0001DBF3 File Offset: 0x0001BDF3
		public int SelectedDepartmentValue { get; set; }

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06001920 RID: 6432 RVA: 0x0001DBFC File Offset: 0x0001BDFC
		// (set) Token: 0x06001921 RID: 6433 RVA: 0x0001DC04 File Offset: 0x0001BE04
		public string SelectedDepartmentTitle { get; set; }
	}
}
