using System;
using System.Data;

namespace TechnoPro.Common.Public.Entities.DynamicForms.Legacy
{
	// Token: 0x02000372 RID: 882
	[Serializable]
	public enum eLegacyDynamicDataRowState
	{
		// Token: 0x0400161D RID: 5661
		[LegacyDynamicDataRowState(DataRowState.Unchanged)]
		Unchanged,
		// Token: 0x0400161E RID: 5662
		[LegacyDynamicDataRowState(DataRowState.Added)]
		Added,
		// Token: 0x0400161F RID: 5663
		[LegacyDynamicDataRowState(DataRowState.Deleted)]
		Deleted,
		// Token: 0x04001620 RID: 5664
		[LegacyDynamicDataRowState(DataRowState.Modified)]
		Modified,
		// Token: 0x04001621 RID: 5665
		[LegacyDynamicDataRowState(DataRowState.Detached)]
		Detached
	}
}
