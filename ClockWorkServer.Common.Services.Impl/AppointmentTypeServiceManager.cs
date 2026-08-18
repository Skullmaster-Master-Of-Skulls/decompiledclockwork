using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters;
using TechnoPro.Common.Core.Appointments;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000021 RID: 33
	public class AppointmentTypeServiceManager : IAppointmentType, IService
	{
		// Token: 0x06000174 RID: 372 RVA: 0x000079CC File Offset: 0x00005BCC
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000079E0 File Offset: 0x00005BE0
		public GetAppointmentTypeAssociatedPerAppScreenNumsResp GetAppointmentTypeAssociatedPerAppScreenNums(GetAppointmentTypeAssociatedPerAppScreenNumsReq Request)
		{
			IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(Request.GetOperationContext());
			IList<int> appointmentTypeAssociatedPerAppScreenNums = appointmentTypeManager.GetAppointmentTypeAssociatedPerAppScreenNums(Request.AppTypeId);
			return new GetAppointmentTypeAssociatedPerAppScreenNumsResp
			{
				PerAppScreenNums = appointmentTypeAssociatedPerAppScreenNums
			};
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00007A18 File Offset: 0x00005C18
		public LoadAppTypeByIdResp LoadAppTypeById(LoadAppTypeByIdReq Request)
		{
			IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(Request.GetOperationContext());
			AppType appType = appointmentTypeManager.LoadAppTypeById(Request.AppTypeId);
			return new LoadAppTypeByIdResp
			{
				AppointmentType = ((appType != null) ? appType.ToDTO() : null)
			};
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00007A5C File Offset: 0x00005C5C
		public void UpdateAppType(UpdateAppTypeReq Request)
		{
			IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(Request.GetOperationContext());
			appointmentTypeManager.UpdateAppType(Request.AppointmentType.ToDomainObject());
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00007A88 File Offset: 0x00005C88
		public CreateAppTypeResp CreateAppType(CreateAppTypeReq Request)
		{
			IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(Request.GetOperationContext());
			int appTypeId = appointmentTypeManager.CreateAppType(Request.AppointmentType.ToDomainObject());
			return new CreateAppTypeResp
			{
				AppTypeId = appTypeId
			};
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00007AC8 File Offset: 0x00005CC8
		public void DeleteAppType(DeleteAppTypeReq Request)
		{
			IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(Request.GetOperationContext());
			bool deleteTheAppTypeInsteadOfDisabling = Request.DeleteTheAppTypeInsteadOfDisabling;
			if (deleteTheAppTypeInsteadOfDisabling)
			{
				appointmentTypeManager.DeleteAppType(Request.AppTypeId, Request.AppTypeIdToReplaceWithInExistingApps);
			}
			else
			{
				appointmentTypeManager.DisableAppType(Request.AppTypeId);
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00007B10 File Offset: 0x00005D10
		public LoadAppTypeGroupByIdResp LoadAppTypeGroupById(LoadAppTypeGroupByIdReq Request)
		{
			IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(Request.GetOperationContext());
			AppTypeGroup appTypeGroup = appointmentTypeManager.LoadAppTypeGroupById(Request.AppointmentTypeGroupId);
			return new LoadAppTypeGroupByIdResp
			{
				AppTypeGroup = ((appTypeGroup != null) ? appTypeGroup.ToDTO() : null)
			};
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00007B54 File Offset: 0x00005D54
		public LoadAllAppTypeGroupsResp LoadAllAppTypeGroups(LoadAllAppTypeGroupsReq Request)
		{
			IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(Request.GetOperationContext());
			IList<AppTypeGroupWithAppTypes> list = appointmentTypeManager.LoadAllAppTypeGroups();
			LoadAllAppTypeGroupsResp loadAllAppTypeGroupsResp = new LoadAllAppTypeGroupsResp();
			IList<AppTypeGroupWithAppTypesDTO> appTypeGroupsWithAppTypes;
			if (list == null)
			{
				appTypeGroupsWithAppTypes = null;
			}
			else
			{
				appTypeGroupsWithAppTypes = list.ToList<AppTypeGroupWithAppTypes>().ConvertAll<AppTypeGroupWithAppTypesDTO>((AppTypeGroupWithAppTypes f) => f.ToDTO());
			}
			loadAllAppTypeGroupsResp.AppTypeGroupsWithAppTypes = appTypeGroupsWithAppTypes;
			return loadAllAppTypeGroupsResp;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00007BB8 File Offset: 0x00005DB8
		public LoadAppTypeGroupWithAppTypesByIdResp LoadAppTypeGroupWithAppTypesById(LoadAppTypeGroupWithAppTypesByIdReq Request)
		{
			IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(Request.GetOperationContext());
			AppTypeGroupWithAppTypes appTypeGroupWithAppTypes = appointmentTypeManager.LoadAppTypeGroupWithAppTypesById(Request.AppointmentTypeGroupId, false);
			return new LoadAppTypeGroupWithAppTypesByIdResp
			{
				AppTypeGroupWithAppTypes = ((appTypeGroupWithAppTypes != null) ? appTypeGroupWithAppTypes.ToDTO() : null)
			};
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00007BFC File Offset: 0x00005DFC
		public void DeleteAppTypeGroup(DeleteAppTypeGroupReq Request)
		{
			IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(Request.GetOperationContext());
			appointmentTypeManager.DeleteAppTypeGroup(Request.AppointmentTypeGroupId);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00007C24 File Offset: 0x00005E24
		public CreateAppTypeGroupResp CreateAppTypeGroup(CreateAppTypeGroupReq Request)
		{
			IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(Request.GetOperationContext());
			int appointmentTypeGroupId = appointmentTypeManager.CreateAppTypeGroup(Request.AppTypeGroup.ToDomainObject());
			return new CreateAppTypeGroupResp
			{
				AppointmentTypeGroupId = appointmentTypeGroupId
			};
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00007C64 File Offset: 0x00005E64
		public void UpdateAppTypeGroup(UpdateAppTypeGroupReq Request)
		{
			IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(Request.GetOperationContext());
			appointmentTypeManager.UpdateAppTypeGroup(Request.AppTypeGroup.ToDomainObject());
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00007C90 File Offset: 0x00005E90
		public LoadAllAppTypesResp LoadAllAppTypes(LoadAllAppTypesReq Request)
		{
			IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(Request.GetOperationContext());
			List<AppType> list = appointmentTypeManager.LoadAllAppTypes(Request.IgnoreCache);
			LoadAllAppTypesResp loadAllAppTypesResp = new LoadAllAppTypesResp();
			loadAllAppTypesResp.AllAppTypes = list.ConvertAll<AppTypeDTO>((AppType a) => a.ToDTO());
			return loadAllAppTypesResp;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00007CEC File Offset: 0x00005EEC
		public GetAllowedAppTypeIdsResp GetAllowedAppTypeIds(GetAllowedAppTypeIdsReq Request)
		{
			IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(Request.GetOperationContext());
			IList<int> allowedAppTypeIds = appointmentTypeManager.GetAllowedAppTypeIds(Request.PersonId);
			return new GetAllowedAppTypeIdsResp
			{
				AppTypeIds = allowedAppTypeIds
			};
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00007D24 File Offset: 0x00005F24
		public LoadAllowedAppTypesResp LoadAllowedAppTypes(LoadAllowedAppTypesReq Request)
		{
			IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(Request.GetOperationContext());
			IList<AppType> list = appointmentTypeManager.LoadAllowedAppTypes();
			LoadAllowedAppTypesResp loadAllowedAppTypesResp = new LoadAllowedAppTypesResp();
			IList<AppTypeDTO> appTypes;
			if (list == null)
			{
				appTypes = null;
			}
			else
			{
				appTypes = list.ToList<AppType>().ConvertAll<AppTypeDTO>((AppType g) => g.ToDTO());
			}
			loadAllowedAppTypesResp.AppTypes = appTypes;
			return loadAllowedAppTypesResp;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00007D88 File Offset: 0x00005F88
		public LoadAllowedTestExamRelatedAppTypesResp LoadAllowedTestExamRelatedAppTypes(LoadAllowedTestExamRelatedAppTypesReq Request)
		{
			IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(Request.GetOperationContext());
			IList<AppType> list = appointmentTypeManager.LoadAllowedTestExamRelatedAppTypes();
			LoadAllowedTestExamRelatedAppTypesResp loadAllowedTestExamRelatedAppTypesResp = new LoadAllowedTestExamRelatedAppTypesResp();
			IList<AppTypeDTO> appTypes;
			if (list == null)
			{
				appTypes = null;
			}
			else
			{
				appTypes = list.ToList<AppType>().ConvertAll<AppTypeDTO>((AppType g) => g.ToDTO());
			}
			loadAllowedTestExamRelatedAppTypesResp.AppTypes = appTypes;
			return loadAllowedTestExamRelatedAppTypesResp;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00007DEC File Offset: 0x00005FEC
		public LoadAllowedWorkshopRelatedAppTypesResp LoadAllowedWorkshopRelatedAppTypes(LoadAllowedWorkshopRelatedAppTypesReq Request)
		{
			IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(Request.GetOperationContext());
			IList<AppType> list = appointmentTypeManager.LoadAllowedWorkshopRelatedAppTypes();
			LoadAllowedWorkshopRelatedAppTypesResp loadAllowedWorkshopRelatedAppTypesResp = new LoadAllowedWorkshopRelatedAppTypesResp();
			IList<AppTypeDTO> appTypes;
			if (list == null)
			{
				appTypes = null;
			}
			else
			{
				appTypes = list.ToList<AppType>().ConvertAll<AppTypeDTO>((AppType g) => g.ToDTO());
			}
			loadAllowedWorkshopRelatedAppTypesResp.AppTypes = appTypes;
			return loadAllowedWorkshopRelatedAppTypesResp;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00007E50 File Offset: 0x00006050
		public LoadAppTypeWithExtendedInfoIdByIdResp LoadAppTypeWithExtendedInfoIdById(LoadAppTypeWithExtendedInfoIdByIdReq Request)
		{
			IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(Request.GetOperationContext());
			LoadAppTypeWithExtendedInfoIdByIdResp loadAppTypeWithExtendedInfoIdByIdResp = new LoadAppTypeWithExtendedInfoIdByIdResp();
			AppTypeWithExtendedInfo appTypeWithExtendedInfo = appointmentTypeManager.LoadAppTypeWithExtendedInfoIdById(Request.AppTypeId);
			loadAppTypeWithExtendedInfoIdByIdResp.AppType = ((appTypeWithExtendedInfo != null) ? appTypeWithExtendedInfo.ToDTO() : null);
			return loadAppTypeWithExtendedInfoIdByIdResp;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00007E94 File Offset: 0x00006094
		public UpdateAppTypeWithExtendedInfoResp UpdateAppTypeWithExtendedInfo(UpdateAppTypeWithExtendedInfoReq Request)
		{
			IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(Request.GetOperationContext());
			appointmentTypeManager.UpdateAppTypeWithExtendedInfo(Request.AppType.ToDomainObject());
			return new UpdateAppTypeWithExtendedInfoResp();
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00007ECC File Offset: 0x000060CC
		public CreateAppTypeWithExtendedInfoResp CreateAppTypeWithExtendedInfo(CreateAppTypeWithExtendedInfoReq Request)
		{
			IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(Request.GetOperationContext());
			return new CreateAppTypeWithExtendedInfoResp
			{
				AppTypeId = appointmentTypeManager.CreateAppTypeWithExtendedInfo(Request.AppType.ToDomainObject())
			};
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00007F08 File Offset: 0x00006108
		public LoadAllAppTypesWithGroupsResp LoadAllAppTypesWithGroups(LoadAllAppTypesWithGroupsReq Request)
		{
			IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(Request.GetOperationContext());
			LoadAllAppTypesWithGroupsResp loadAllAppTypesWithGroupsResp = new LoadAllAppTypesWithGroupsResp();
			IList<AppTypeGroupWithAppTypes> list = appointmentTypeManager.LoadAllAppTypesWithGroups(Request.IgnoreCache);
			IList<AppTypeGroupWithAppTypesDTO> appTypeGroupsWithAppTypes;
			if (list == null)
			{
				appTypeGroupsWithAppTypes = null;
			}
			else
			{
				appTypeGroupsWithAppTypes = (from g in list
				select g.ToDTO()).ToList<AppTypeGroupWithAppTypesDTO>();
			}
			loadAllAppTypesWithGroupsResp.AppTypeGroupsWithAppTypes = appTypeGroupsWithAppTypes;
			return loadAllAppTypesWithGroupsResp;
		}
	}
}
