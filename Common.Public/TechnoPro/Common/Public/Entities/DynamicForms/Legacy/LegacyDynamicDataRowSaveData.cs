using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms.Legacy
{
	// Token: 0x02000377 RID: 887
	public class LegacyDynamicDataRowSaveData
	{
		// Token: 0x06001B74 RID: 7028 RVA: 0x0000D55A File Offset: 0x0000B75A
		public LegacyDynamicDataRowSaveData()
		{
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x0001F550 File Offset: 0x0001D750
		public LegacyDynamicDataRowSaveData(eLegacyDynamicDataRowState rowState, eLegacyDynamicDataType controlValueType)
		{
			this.RowState = rowState;
			this.ControlValueType = controlValueType;
		}

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06001B76 RID: 7030 RVA: 0x0001F56A File Offset: 0x0001D76A
		// (set) Token: 0x06001B77 RID: 7031 RVA: 0x0001F572 File Offset: 0x0001D772
		public eLegacyDynamicDataRowState RowState { get; set; }

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06001B78 RID: 7032 RVA: 0x0001F57B File Offset: 0x0001D77B
		// (set) Token: 0x06001B79 RID: 7033 RVA: 0x0001F583 File Offset: 0x0001D783
		public eLegacyDynamicDataType ControlValueType { get; set; }

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06001B7A RID: 7034 RVA: 0x0001F58C File Offset: 0x0001D78C
		// (set) Token: 0x06001B7B RID: 7035 RVA: 0x0001F594 File Offset: 0x0001D794
		public int ScreenNum { get; set; }

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06001B7C RID: 7036 RVA: 0x0001F59D File Offset: 0x0001D79D
		// (set) Token: 0x06001B7D RID: 7037 RVA: 0x0001F5A5 File Offset: 0x0001D7A5
		public int PersonId { get; set; }

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x06001B7E RID: 7038 RVA: 0x0001F5AE File Offset: 0x0001D7AE
		// (set) Token: 0x06001B7F RID: 7039 RVA: 0x0001F5B6 File Offset: 0x0001D7B6
		public int ControlId { get; set; }

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x06001B80 RID: 7040 RVA: 0x0001F5BF File Offset: 0x0001D7BF
		// (set) Token: 0x06001B81 RID: 7041 RVA: 0x0001F5C7 File Offset: 0x0001D7C7
		public object ControlValue { get; set; }

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x06001B82 RID: 7042 RVA: 0x0001F5D0 File Offset: 0x0001D7D0
		// (set) Token: 0x06001B83 RID: 7043 RVA: 0x0001F5D8 File Offset: 0x0001D7D8
		public int WhoAmI { get; set; }
	}
}
