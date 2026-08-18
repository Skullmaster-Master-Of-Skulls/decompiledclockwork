using System;

namespace TechnoPro.Common.Public.Entities.Caching
{
	// Token: 0x0200046F RID: 1135
	[Serializable]
	public enum eServerCacheItemType
	{
		// Token: 0x040019E0 RID: 6624
		Unknown,
		// Token: 0x040019E1 RID: 6625
		allUserObjects,
		// Token: 0x040019E2 RID: 6626
		allGroups,
		// Token: 0x040019E3 RID: 6627
		allRoomGroups,
		// Token: 0x040019E4 RID: 6628
		uAllowedStudentPids,
		// Token: 0x040019E5 RID: 6629
		uAllowedStaffPids,
		// Token: 0x040019E6 RID: 6630
		uAllowedRoomPids,
		// Token: 0x040019E7 RID: 6631
		uAllowedResourcePids,
		// Token: 0x040019E8 RID: 6632
		uDynamicFormFields,
		// Token: 0x040019E9 RID: 6633
		uDynamicFormFieldsTree,
		// Token: 0x040019EA RID: 6634
		uDynamicFieldCid_Email,
		// Token: 0x040019EB RID: 6635
		uDynamicForm_LookupList,
		// Token: 0x040019EC RID: 6636
		uAllowedPidsCombined,
		// Token: 0x040019ED RID: 6637
		uAllowedAppTypeIds,
		// Token: 0x040019EE RID: 6638
		uAllActiveAppTypes,
		// Token: 0x040019EF RID: 6639
		uAllInactiveAppTypes,
		// Token: 0x040019F0 RID: 6640
		uAllUserObjectsBiggestPid,
		// Token: 0x040019F1 RID: 6641
		uWebAuthenticationCustomFieldStaff,
		// Token: 0x040019F2 RID: 6642
		uWebAuthenticationCustomFieldStudent,
		// Token: 0x040019F3 RID: 6643
		uAllAppTypesWithGroups
	}
}
