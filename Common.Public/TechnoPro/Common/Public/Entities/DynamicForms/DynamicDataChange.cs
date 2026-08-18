using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.DynamicForms
{
	// Token: 0x0200035C RID: 860
	public class DynamicDataChange : BusinessBase<int>, IDynamicDataHoldingObject
	{
		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06001AC0 RID: 6848 RVA: 0x0001EC2E File Offset: 0x0001CE2E
		// (set) Token: 0x06001AC1 RID: 6849 RVA: 0x0001EC36 File Offset: 0x0001CE36
		public DynamicDataContext Context { get; set; }

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x06001AC2 RID: 6850 RVA: 0x0001EC3F File Offset: 0x0001CE3F
		// (set) Token: 0x06001AC3 RID: 6851 RVA: 0x0001EC47 File Offset: 0x0001CE47
		public DynamicData Data { get; set; }

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06001AC4 RID: 6852 RVA: 0x0001EC50 File Offset: 0x0001CE50
		// (set) Token: 0x06001AC5 RID: 6853 RVA: 0x0001EC58 File Offset: 0x0001CE58
		public object PreviousValue { get; set; }

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x06001AC6 RID: 6854 RVA: 0x0001EC61 File Offset: 0x0001CE61
		// (set) Token: 0x06001AC7 RID: 6855 RVA: 0x0001EC69 File Offset: 0x0001CE69
		public DateTime LastDateOfChange { get; set; }

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x06001AC8 RID: 6856 RVA: 0x0001EC72 File Offset: 0x0001CE72
		// (set) Token: 0x06001AC9 RID: 6857 RVA: 0x0001EC7A File Offset: 0x0001CE7A
		public PersonBase WhoLastChanged { get; set; }

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x06001ACA RID: 6858 RVA: 0x0001EC83 File Offset: 0x0001CE83
		// (set) Token: 0x06001ACB RID: 6859 RVA: 0x0001EC8B File Offset: 0x0001CE8B
		public eDynamicDataChangeAction ChangeAction { get; set; }

		// Token: 0x06001ACC RID: 6860 RVA: 0x0001EC94 File Offset: 0x0001CE94
		public DynamicData GetDynamicData()
		{
			return this.Data;
		}
	}
}
