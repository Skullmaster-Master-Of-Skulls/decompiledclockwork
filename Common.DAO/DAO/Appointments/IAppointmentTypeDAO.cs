using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.DAO.Appointments
{
	// Token: 0x020000AB RID: 171
	public interface IAppointmentTypeDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000465 RID: 1125
		IList<int> GetAppointmentTypeAssociatedPerAppScreenNums(int AppTypeId);

		// Token: 0x06000466 RID: 1126
		AppType LoadAppTypeById(int AppTypeId);

		// Token: 0x06000467 RID: 1127
		List<AppType> LoadAllAppTypes();

		// Token: 0x06000468 RID: 1128
		Task<List<AppType>> LoadAllAppTypesAsync();

		// Token: 0x06000469 RID: 1129
		void UpdateAppType(AppType AppType);

		// Token: 0x0600046A RID: 1130
		int CreateAppType(AppType AppType);

		// Token: 0x0600046B RID: 1131
		void DeleteAppType(int AppTypeId, int AppTypeIdToReplaceWith);

		// Token: 0x0600046C RID: 1132
		void DisableAppType(int AppTypeId);

		// Token: 0x0600046D RID: 1133
		AppTypeGroup LoadAppTypeGroupById(int AppointmentTypeGroupId);

		// Token: 0x0600046E RID: 1134
		IList<AppTypeGroupWithAppTypes> LoadAllAppTypeGroups(bool includeInactive = false);

		// Token: 0x0600046F RID: 1135
		AppTypeGroupWithAppTypes LoadAppTypeGroupWithAppTypesById(int AppointmentTypeGroupId, bool IncludeInactiveAppTypes = false);

		// Token: 0x06000470 RID: 1136
		void DeleteAppTypeGroup(int AppointmentTypeGroupId);

		// Token: 0x06000471 RID: 1137
		int CreateAppTypeGroup(AppTypeGroup AppTypeGroup);

		// Token: 0x06000472 RID: 1138
		void UpdateAppTypeGroup(AppTypeGroup AppTypeGroup);

		// Token: 0x06000473 RID: 1139
		List<AppType> LoadAllInactiveAppTypes();

		// Token: 0x06000474 RID: 1140
		Task<List<AppType>> LoadAllInactiveAppTypesAsync();

		// Token: 0x06000475 RID: 1141
		AppTypeWithExtendedInfo LoadAppTypeWithExtendedInfoIdById(int appTypeId);

		// Token: 0x06000476 RID: 1142
		void UpdateAppTypeWithExtendedInfo(AppTypeWithExtendedInfo AppType);

		// Token: 0x06000477 RID: 1143
		int CreateAppTypeWithExtendedInfo(AppTypeWithExtendedInfo AppType);

		// Token: 0x06000478 RID: 1144
		IList<AppType> LoadOrphanAppTypes();

		// Token: 0x06000479 RID: 1145
		AppType LoadAppTypeByAppointmentId(int appointmentId);
	}
}
