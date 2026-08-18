using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.ICore.Appointments
{
	// Token: 0x020000E5 RID: 229
	public interface IAppointmentTypeManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000714 RID: 1812
		IList<int> GetAppointmentTypeAssociatedPerAppScreenNums(int AppTypeId);

		// Token: 0x06000715 RID: 1813
		AppType LoadAppTypeById(int AppTypeId);

		// Token: 0x06000716 RID: 1814
		List<AppType> LoadAllAppTypes();

		// Token: 0x06000717 RID: 1815
		Task<List<AppType>> LoadAllAppTypesAsync();

		// Token: 0x06000718 RID: 1816
		List<AppType> LoadAllAppTypes(bool ignoreCache);

		// Token: 0x06000719 RID: 1817
		Task<List<AppType>> LoadAllAppTypesAsync(bool ignoreCache);

		// Token: 0x0600071A RID: 1818
		void UpdateAppType(AppType AppType);

		// Token: 0x0600071B RID: 1819
		int CreateAppType(AppType AppType);

		// Token: 0x0600071C RID: 1820
		void DeleteAppType(int AppTypeId, int AppTypeIdToReplaceWith);

		// Token: 0x0600071D RID: 1821
		void DisableAppType(int appTypeId);

		// Token: 0x0600071E RID: 1822
		AppTypeGroup LoadAppTypeGroupById(int AppointmentTypeGroupId);

		// Token: 0x0600071F RID: 1823
		IList<AppTypeGroupWithAppTypes> LoadAllAppTypeGroups();

		// Token: 0x06000720 RID: 1824
		AppTypeGroupWithAppTypes LoadAppTypeGroupWithAppTypesById(int AppointmentTypeGroupId, bool IncludeInactiveAppTypes = false);

		// Token: 0x06000721 RID: 1825
		void DeleteAppTypeGroup(int AppointmentTypeGroupId);

		// Token: 0x06000722 RID: 1826
		int CreateAppTypeGroup(AppTypeGroup AppTypeGroup);

		// Token: 0x06000723 RID: 1827
		void UpdateAppTypeGroup(AppTypeGroup AppTypeGroup);

		// Token: 0x06000724 RID: 1828
		IList<int> GetAllowedAppTypeIds(int personId);

		// Token: 0x06000725 RID: 1829
		Task<IList<int>> GetAllowedAppTypeIdsAsync(int personId);

		// Token: 0x06000726 RID: 1830
		List<AppType> LoadAllInactiveAppTypes();

		// Token: 0x06000727 RID: 1831
		Task<List<AppType>> LoadAllInactiveAppTypesAsync();

		// Token: 0x06000728 RID: 1832
		IList<AppType> LoadAllowedAppTypes();

		// Token: 0x06000729 RID: 1833
		IList<AppType> LoadAllowedTestExamRelatedAppTypes();

		// Token: 0x0600072A RID: 1834
		IList<AppType> LoadAllowedWorkshopRelatedAppTypes();

		// Token: 0x0600072B RID: 1835
		AppTypeWithExtendedInfo LoadAppTypeWithExtendedInfoIdById(int appTypeId);

		// Token: 0x0600072C RID: 1836
		void UpdateAppTypeWithExtendedInfo(AppTypeWithExtendedInfo AppType);

		// Token: 0x0600072D RID: 1837
		int CreateAppTypeWithExtendedInfo(AppTypeWithExtendedInfo AppType);

		// Token: 0x0600072E RID: 1838
		IList<AppTypeGroupWithAppTypes> LoadAllAppTypesWithGroups(bool ignoreCache);

		// Token: 0x0600072F RID: 1839
		AppType LoadAppTypeByAppointmentId(int appointmentId);
	}
}
