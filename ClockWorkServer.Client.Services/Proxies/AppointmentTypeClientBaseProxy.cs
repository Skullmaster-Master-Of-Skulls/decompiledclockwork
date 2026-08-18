using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200004B RID: 75
	internal class AppointmentTypeClientBaseProxy : ClientBase<IAppointmentType>, IAppointmentType, IService
	{
		// Token: 0x060003B4 RID: 948 RVA: 0x0000B0F8 File Offset: 0x000092F8
		public AppointmentTypeClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000B103 File Offset: 0x00009303
		public AppointmentTypeClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000B110 File Offset: 0x00009310
		public CreateAppTypeResp CreateAppType(CreateAppTypeReq Request)
		{
			return base.Channel.CreateAppType(Request);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000B130 File Offset: 0x00009330
		public CreateAppTypeGroupResp CreateAppTypeGroup(CreateAppTypeGroupReq Request)
		{
			return base.Channel.CreateAppTypeGroup(Request);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000B14E File Offset: 0x0000934E
		public void DeleteAppType(DeleteAppTypeReq Request)
		{
			base.Channel.DeleteAppType(Request);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000B15E File Offset: 0x0000935E
		public void DeleteAppTypeGroup(DeleteAppTypeGroupReq Request)
		{
			base.Channel.DeleteAppTypeGroup(Request);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000B170 File Offset: 0x00009370
		public GetAppointmentTypeAssociatedPerAppScreenNumsResp GetAppointmentTypeAssociatedPerAppScreenNums(GetAppointmentTypeAssociatedPerAppScreenNumsReq Request)
		{
			return base.Channel.GetAppointmentTypeAssociatedPerAppScreenNums(Request);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000B190 File Offset: 0x00009390
		public LoadAllAppTypeGroupsResp LoadAllAppTypeGroups(LoadAllAppTypeGroupsReq Request)
		{
			return base.Channel.LoadAllAppTypeGroups(Request);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000B1B0 File Offset: 0x000093B0
		public LoadAllAppTypesResp LoadAllAppTypes(LoadAllAppTypesReq Request)
		{
			return base.Channel.LoadAllAppTypes(Request);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000B1D0 File Offset: 0x000093D0
		public LoadAppTypeByIdResp LoadAppTypeById(LoadAppTypeByIdReq Request)
		{
			return base.Channel.LoadAppTypeById(Request);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000B1F0 File Offset: 0x000093F0
		public LoadAppTypeGroupByIdResp LoadAppTypeGroupById(LoadAppTypeGroupByIdReq Request)
		{
			return base.Channel.LoadAppTypeGroupById(Request);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000B210 File Offset: 0x00009410
		public LoadAppTypeGroupWithAppTypesByIdResp LoadAppTypeGroupWithAppTypesById(LoadAppTypeGroupWithAppTypesByIdReq Request)
		{
			return base.Channel.LoadAppTypeGroupWithAppTypesById(Request);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000B22E File Offset: 0x0000942E
		public void UpdateAppType(UpdateAppTypeReq Request)
		{
			base.Channel.UpdateAppType(Request);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000B23E File Offset: 0x0000943E
		public void UpdateAppTypeGroup(UpdateAppTypeGroupReq Request)
		{
			base.Channel.UpdateAppTypeGroup(Request);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000B250 File Offset: 0x00009450
		public GetAllowedAppTypeIdsResp GetAllowedAppTypeIds(GetAllowedAppTypeIdsReq Request)
		{
			return base.Channel.GetAllowedAppTypeIds(Request);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000B270 File Offset: 0x00009470
		public LoadAllowedAppTypesResp LoadAllowedAppTypes(LoadAllowedAppTypesReq Request)
		{
			return base.Channel.LoadAllowedAppTypes(Request);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000B290 File Offset: 0x00009490
		public LoadAllowedTestExamRelatedAppTypesResp LoadAllowedTestExamRelatedAppTypes(LoadAllowedTestExamRelatedAppTypesReq Request)
		{
			return base.Channel.LoadAllowedTestExamRelatedAppTypes(Request);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000B2B0 File Offset: 0x000094B0
		public LoadAllowedWorkshopRelatedAppTypesResp LoadAllowedWorkshopRelatedAppTypes(LoadAllowedWorkshopRelatedAppTypesReq Request)
		{
			return base.Channel.LoadAllowedWorkshopRelatedAppTypes(Request);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000B2D0 File Offset: 0x000094D0
		public LoadAppTypeWithExtendedInfoIdByIdResp LoadAppTypeWithExtendedInfoIdById(LoadAppTypeWithExtendedInfoIdByIdReq Request)
		{
			return base.Channel.LoadAppTypeWithExtendedInfoIdById(Request);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000B2F0 File Offset: 0x000094F0
		public UpdateAppTypeWithExtendedInfoResp UpdateAppTypeWithExtendedInfo(UpdateAppTypeWithExtendedInfoReq Request)
		{
			return base.Channel.UpdateAppTypeWithExtendedInfo(Request);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000B310 File Offset: 0x00009510
		public CreateAppTypeWithExtendedInfoResp CreateAppTypeWithExtendedInfo(CreateAppTypeWithExtendedInfoReq Request)
		{
			return base.Channel.CreateAppTypeWithExtendedInfo(Request);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000B330 File Offset: 0x00009530
		public LoadAllAppTypesWithGroupsResp LoadAllAppTypesWithGroups(LoadAllAppTypesWithGroupsReq Request)
		{
			return base.Channel.LoadAllAppTypesWithGroups(Request);
		}
	}
}
