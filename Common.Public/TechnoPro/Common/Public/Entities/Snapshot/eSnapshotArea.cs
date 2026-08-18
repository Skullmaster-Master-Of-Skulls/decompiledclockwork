using System;

namespace TechnoPro.Common.Public.Entities.Snapshot
{
	// Token: 0x020001B3 RID: 435
	[Serializable]
	public enum eSnapshotArea
	{
		// Token: 0x04000840 RID: 2112
		[SnapshotArea("GenerateSqlQueriesToReproduceAppointmentTypes")]
		AppointmentTypes,
		// Token: 0x04000841 RID: 2113
		[SnapshotArea("GenerateSqlQueriesToReproduceOldSettingsAndPermissions")]
		OldSettingsAndPermissions,
		// Token: 0x04000842 RID: 2114
		[SnapshotArea("GenerateSqlQueriesToReproducePeopleAndGroups")]
		PeopleAndGroups,
		// Token: 0x04000843 RID: 2115
		[SnapshotArea("GenerateSqlQueriesToReproduceWebSettings")]
		WebSettings,
		// Token: 0x04000844 RID: 2116
		[SnapshotArea("GenerateSqlQueriesToReproduceDynamicControlsAndForms")]
		ControlsAndForms,
		// Token: 0x04000845 RID: 2117
		[SnapshotArea("GenerateSqlQueriesToReproduceReports")]
		Reports,
		// Token: 0x04000846 RID: 2118
		[SnapshotArea("GenerateSqlQueriesToReproduceMailMergeTemplates")]
		MailMergeTemplates
	}
}
