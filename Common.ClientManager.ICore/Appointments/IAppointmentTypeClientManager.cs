using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Appointments
{
	// Token: 0x02000080 RID: 128
	public interface IAppointmentTypeClientManager : IWebService
	{
		// Token: 0x060003B0 RID: 944
		IList<int> GetAppointmentTypeAssociatedPerAppScreenNums(int AppTypeId);

		// Token: 0x060003B1 RID: 945
		AppTypeDTO LoadAppTypeById(int AppTypeId);

		// Token: 0x060003B2 RID: 946
		IList<AppTypeDTO> LoadAllAppTypes();

		// Token: 0x060003B3 RID: 947
		IList<AppTypeDTO> LoadAllAppTypes(bool ignoreCache);

		// Token: 0x060003B4 RID: 948
		void UpdateAppType(AppTypeDTO AppType);

		// Token: 0x060003B5 RID: 949
		int CreateAppType(AppTypeDTO AppType);

		// Token: 0x060003B6 RID: 950
		void DeleteAppType(int AppTypeId, int AppTypeIdToReplaceWith);

		// Token: 0x060003B7 RID: 951
		void DisableAppType(int AppTypeId);

		// Token: 0x060003B8 RID: 952
		AppTypeGroupDTO LoadAppTypeGroupById(int AppointmentTypeGroupId);

		// Token: 0x060003B9 RID: 953
		IList<AppTypeGroupWithAppTypesDTO> LoadAllAppTypeGroups();

		// Token: 0x060003BA RID: 954
		AppTypeGroupWithAppTypesDTO LoadAppTypeGroupWithAppTypesById(int AppointmentTypeGroupId);

		// Token: 0x060003BB RID: 955
		void DeleteAppTypeGroup(int AppointmentTypeGroupId);

		// Token: 0x060003BC RID: 956
		int CreateAppTypeGroup(AppTypeGroupDTO AppTypeGroup);

		// Token: 0x060003BD RID: 957
		void UpdateAppTypeGroup(AppTypeGroupDTO AppTypeGroup);

		// Token: 0x060003BE RID: 958
		IList<int> GetAllowedAppTypeIds(int personId);

		// Token: 0x060003BF RID: 959
		IList<AppTypeDTO> LoadAllowedAppTypes();

		// Token: 0x060003C0 RID: 960
		IList<AppTypeDTO> LoadAllowedTestExamRelatedAppTypes();

		// Token: 0x060003C1 RID: 961
		IList<AppTypeDTO> LoadAllowedWorkshopRelatedAppTypes();

		// Token: 0x060003C2 RID: 962
		AppTypeWithExtendedInfoDTO LoadAppTypeWithExtendedInfoIdById(int appTypeId);

		// Token: 0x060003C3 RID: 963
		void UpdateAppTypeWithExtendedInfo(AppTypeWithExtendedInfoDTO AppType);

		// Token: 0x060003C4 RID: 964
		int CreateAppTypeWithExtendedInfo(AppTypeWithExtendedInfoDTO AppType);

		// Token: 0x060003C5 RID: 965
		IList<AppTypeGroupWithAppTypesDTO> LoadAllAppTypesWithGroups(bool ignoreCache);
	}
}
