using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Appointments;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Appointments
{
	// Token: 0x02000086 RID: 134
	public class AppointmentTypeClientManager : IAppointmentTypeClientManager, IWebService
	{
		// Token: 0x060004C1 RID: 1217 RVA: 0x00015914 File Offset: 0x00013B14
		public IList<int> GetAppointmentTypeAssociatedPerAppScreenNums(int AppTypeId)
		{
			GetAppointmentTypeAssociatedPerAppScreenNumsReq getAppointmentTypeAssociatedPerAppScreenNumsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetAppointmentTypeAssociatedPerAppScreenNumsReq>();
			getAppointmentTypeAssociatedPerAppScreenNumsReq.AppTypeId = AppTypeId;
			return ClientServiceFactory.GetClientInstance<IAppointmentType>().GetAppointmentTypeAssociatedPerAppScreenNums(getAppointmentTypeAssociatedPerAppScreenNumsReq).PerAppScreenNums;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0001594C File Offset: 0x00013B4C
		public AppTypeDTO LoadAppTypeById(int AppTypeId)
		{
			LoadAppTypeByIdReq loadAppTypeByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAppTypeByIdReq>();
			loadAppTypeByIdReq.AppTypeId = AppTypeId;
			return ClientServiceFactory.GetClientInstance<IAppointmentType>().LoadAppTypeById(loadAppTypeByIdReq).AppointmentType;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00015984 File Offset: 0x00013B84
		public IList<AppTypeDTO> LoadAllAppTypes(bool ignoreCache)
		{
			LoadAllAppTypesReq loadAllAppTypesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllAppTypesReq>();
			loadAllAppTypesReq.IgnoreCache = ignoreCache;
			return ClientServiceFactory.GetClientInstance<IAppointmentType>().LoadAllAppTypes(loadAllAppTypesReq).AllAppTypes;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x000159BC File Offset: 0x00013BBC
		public IList<AppTypeDTO> LoadAllAppTypes()
		{
			return this.LoadAllAppTypes(false);
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x000159D8 File Offset: 0x00013BD8
		public void UpdateAppType(AppTypeDTO AppType)
		{
			UpdateAppTypeReq updateAppTypeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateAppTypeReq>();
			updateAppTypeReq.AppointmentType = AppType;
			ClientServiceFactory.GetClientInstance<IAppointmentType>().UpdateAppType(updateAppTypeReq);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00015A08 File Offset: 0x00013C08
		public int CreateAppType(AppTypeDTO AppType)
		{
			CreateAppTypeReq createAppTypeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateAppTypeReq>();
			createAppTypeReq.AppointmentType = AppType;
			return ClientServiceFactory.GetClientInstance<IAppointmentType>().CreateAppType(createAppTypeReq).AppTypeId;
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00015A40 File Offset: 0x00013C40
		public void DeleteAppType(int AppTypeId, int AppTypeIdToReplaceWith)
		{
			DeleteAppTypeReq deleteAppTypeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteAppTypeReq>();
			deleteAppTypeReq.AppTypeId = AppTypeId;
			deleteAppTypeReq.DeleteTheAppTypeInsteadOfDisabling = true;
			deleteAppTypeReq.AppTypeIdToReplaceWithInExistingApps = AppTypeIdToReplaceWith;
			ClientServiceFactory.GetClientInstance<IAppointmentType>().DeleteAppType(deleteAppTypeReq);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00015A80 File Offset: 0x00013C80
		public void DisableAppType(int AppTypeId)
		{
			DeleteAppTypeReq deleteAppTypeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteAppTypeReq>();
			deleteAppTypeReq.AppTypeId = AppTypeId;
			deleteAppTypeReq.DeleteTheAppTypeInsteadOfDisabling = false;
			ClientServiceFactory.GetClientInstance<IAppointmentType>().DeleteAppType(deleteAppTypeReq);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00015AB8 File Offset: 0x00013CB8
		public AppTypeGroupDTO LoadAppTypeGroupById(int AppointmentTypeGroupId)
		{
			LoadAppTypeGroupByIdReq loadAppTypeGroupByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAppTypeGroupByIdReq>();
			loadAppTypeGroupByIdReq.AppointmentTypeGroupId = AppointmentTypeGroupId;
			return ClientServiceFactory.GetClientInstance<IAppointmentType>().LoadAppTypeGroupById(loadAppTypeGroupByIdReq).AppTypeGroup;
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00015AF0 File Offset: 0x00013CF0
		public IList<AppTypeGroupWithAppTypesDTO> LoadAllAppTypeGroups()
		{
			LoadAllAppTypeGroupsReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllAppTypeGroupsReq>();
			return ClientServiceFactory.GetClientInstance<IAppointmentType>().LoadAllAppTypeGroups(request).AppTypeGroupsWithAppTypes;
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00015B20 File Offset: 0x00013D20
		public AppTypeGroupWithAppTypesDTO LoadAppTypeGroupWithAppTypesById(int AppointmentTypeGroupId)
		{
			LoadAppTypeGroupWithAppTypesByIdReq loadAppTypeGroupWithAppTypesByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAppTypeGroupWithAppTypesByIdReq>();
			loadAppTypeGroupWithAppTypesByIdReq.AppointmentTypeGroupId = AppointmentTypeGroupId;
			return ClientServiceFactory.GetClientInstance<IAppointmentType>().LoadAppTypeGroupWithAppTypesById(loadAppTypeGroupWithAppTypesByIdReq).AppTypeGroupWithAppTypes;
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00015B58 File Offset: 0x00013D58
		public void DeleteAppTypeGroup(int AppointmentTypeGroupId)
		{
			DeleteAppTypeGroupReq deleteAppTypeGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteAppTypeGroupReq>();
			deleteAppTypeGroupReq.AppointmentTypeGroupId = AppointmentTypeGroupId;
			ClientServiceFactory.GetClientInstance<IAppointmentType>().DeleteAppTypeGroup(deleteAppTypeGroupReq);
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00015B88 File Offset: 0x00013D88
		public int CreateAppTypeGroup(AppTypeGroupDTO AppTypeGroup)
		{
			CreateAppTypeGroupReq createAppTypeGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateAppTypeGroupReq>();
			createAppTypeGroupReq.AppTypeGroup = AppTypeGroup;
			return ClientServiceFactory.GetClientInstance<IAppointmentType>().CreateAppTypeGroup(createAppTypeGroupReq).AppointmentTypeGroupId;
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00015BC0 File Offset: 0x00013DC0
		public void UpdateAppTypeGroup(AppTypeGroupDTO AppTypeGroup)
		{
			UpdateAppTypeGroupReq updateAppTypeGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateAppTypeGroupReq>();
			updateAppTypeGroupReq.AppTypeGroup = AppTypeGroup;
			ClientServiceFactory.GetClientInstance<IAppointmentType>().UpdateAppTypeGroup(updateAppTypeGroupReq);
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00015BF0 File Offset: 0x00013DF0
		public IList<int> GetAllowedAppTypeIds(int personId)
		{
			GetAllowedAppTypeIdsReq getAllowedAppTypeIdsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetAllowedAppTypeIdsReq>();
			getAllowedAppTypeIdsReq.PersonId = personId;
			return ClientServiceFactory.GetClientInstance<IAppointmentType>().GetAllowedAppTypeIds(getAllowedAppTypeIdsReq).AppTypeIds;
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00015C28 File Offset: 0x00013E28
		public IList<AppTypeDTO> LoadAllowedAppTypes()
		{
			LoadAllowedAppTypesReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllowedAppTypesReq>();
			return ClientServiceFactory.GetClientInstance<IAppointmentType>().LoadAllowedAppTypes(request).AppTypes;
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00015C58 File Offset: 0x00013E58
		public IList<AppTypeDTO> LoadAllowedTestExamRelatedAppTypes()
		{
			LoadAllowedTestExamRelatedAppTypesReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllowedTestExamRelatedAppTypesReq>();
			return ClientServiceFactory.GetClientInstance<IAppointmentType>().LoadAllowedTestExamRelatedAppTypes(request).AppTypes;
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00015C88 File Offset: 0x00013E88
		public IList<AppTypeDTO> LoadAllowedWorkshopRelatedAppTypes()
		{
			LoadAllowedWorkshopRelatedAppTypesReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllowedWorkshopRelatedAppTypesReq>();
			return ClientServiceFactory.GetClientInstance<IAppointmentType>().LoadAllowedWorkshopRelatedAppTypes(request).AppTypes;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00015CB8 File Offset: 0x00013EB8
		public AppTypeWithExtendedInfoDTO LoadAppTypeWithExtendedInfoIdById(int appTypeId)
		{
			LoadAppTypeWithExtendedInfoIdByIdReq loadAppTypeWithExtendedInfoIdByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAppTypeWithExtendedInfoIdByIdReq>();
			loadAppTypeWithExtendedInfoIdByIdReq.AppTypeId = appTypeId;
			return ClientServiceFactory.GetClientInstance<IAppointmentType>().LoadAppTypeWithExtendedInfoIdById(loadAppTypeWithExtendedInfoIdByIdReq).AppType;
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00015CF0 File Offset: 0x00013EF0
		public void UpdateAppTypeWithExtendedInfo(AppTypeWithExtendedInfoDTO AppType)
		{
			UpdateAppTypeWithExtendedInfoReq updateAppTypeWithExtendedInfoReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateAppTypeWithExtendedInfoReq>();
			updateAppTypeWithExtendedInfoReq.AppType = AppType;
			ClientServiceFactory.GetClientInstance<IAppointmentType>().UpdateAppTypeWithExtendedInfo(updateAppTypeWithExtendedInfoReq);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00015D20 File Offset: 0x00013F20
		public int CreateAppTypeWithExtendedInfo(AppTypeWithExtendedInfoDTO AppType)
		{
			CreateAppTypeWithExtendedInfoReq createAppTypeWithExtendedInfoReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateAppTypeWithExtendedInfoReq>();
			createAppTypeWithExtendedInfoReq.AppType = AppType;
			return ClientServiceFactory.GetClientInstance<IAppointmentType>().CreateAppTypeWithExtendedInfo(createAppTypeWithExtendedInfoReq).AppTypeId;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00015D58 File Offset: 0x00013F58
		public IList<AppTypeGroupWithAppTypesDTO> LoadAllAppTypesWithGroups(bool ignoreCache = false)
		{
			LoadAllAppTypesWithGroupsReq loadAllAppTypesWithGroupsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllAppTypesWithGroupsReq>();
			loadAllAppTypesWithGroupsReq.IgnoreCache = ignoreCache;
			return ClientServiceFactory.GetClientInstance<IAppointmentType>().LoadAllAppTypesWithGroups(loadAllAppTypesWithGroupsReq).AppTypeGroupsWithAppTypes;
		}
	}
}
