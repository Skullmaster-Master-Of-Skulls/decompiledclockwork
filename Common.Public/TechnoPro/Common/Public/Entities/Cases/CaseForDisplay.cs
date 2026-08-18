using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Cases
{
	// Token: 0x02000466 RID: 1126
	public class CaseForDisplay : CaseBase
	{
		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x06002246 RID: 8774 RVA: 0x00026416 File Offset: 0x00024616
		// (set) Token: 0x06002247 RID: 8775 RVA: 0x0002641E File Offset: 0x0002461E
		public DateTime DateEntered { get; set; }

		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x06002248 RID: 8776 RVA: 0x00026427 File Offset: 0x00024627
		// (set) Token: 0x06002249 RID: 8777 RVA: 0x0002642F File Offset: 0x0002462F
		public PersonBase WhoEntered { get; set; }

		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x0600224A RID: 8778 RVA: 0x00026438 File Offset: 0x00024638
		// (set) Token: 0x0600224B RID: 8779 RVA: 0x00026440 File Offset: 0x00024640
		public string Status { get; set; }

		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x0600224C RID: 8780 RVA: 0x00026449 File Offset: 0x00024649
		// (set) Token: 0x0600224D RID: 8781 RVA: 0x00026451 File Offset: 0x00024651
		public IList<DynamicData> DynamicFormDataSummary { get; set; }
	}
}
