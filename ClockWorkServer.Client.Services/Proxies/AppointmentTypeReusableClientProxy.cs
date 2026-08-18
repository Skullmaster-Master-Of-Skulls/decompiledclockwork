using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200004A RID: 74
	public class AppointmentTypeReusableClientProxy : WCFTokenBasedReusableClientProxy<IAppointmentType>, IAppointmentType, IService
	{
		// Token: 0x0600039E RID: 926 RVA: 0x0000AC7E File Offset: 0x00008E7E
		public AppointmentTypeReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000AC89 File Offset: 0x00008E89
		public AppointmentTypeReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000AC98 File Offset: 0x00008E98
		public CreateAppTypeResp CreateAppType(CreateAppTypeReq Request)
		{
			return this.WrapServiceMethod<CreateAppTypeResp>(() => this.Proxy.CreateAppType(Request));
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000ACD0 File Offset: 0x00008ED0
		public CreateAppTypeGroupResp CreateAppTypeGroup(CreateAppTypeGroupReq Request)
		{
			return this.WrapServiceMethod<CreateAppTypeGroupResp>(() => this.Proxy.CreateAppTypeGroup(Request));
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000AD08 File Offset: 0x00008F08
		public void DeleteAppType(DeleteAppTypeReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteAppType(Request);
			});
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000AD40 File Offset: 0x00008F40
		public void DeleteAppTypeGroup(DeleteAppTypeGroupReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteAppTypeGroup(Request);
			});
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000AD78 File Offset: 0x00008F78
		public GetAppointmentTypeAssociatedPerAppScreenNumsResp GetAppointmentTypeAssociatedPerAppScreenNums(GetAppointmentTypeAssociatedPerAppScreenNumsReq Request)
		{
			return this.WrapServiceMethod<GetAppointmentTypeAssociatedPerAppScreenNumsResp>(() => this.Proxy.GetAppointmentTypeAssociatedPerAppScreenNums(Request));
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000ADB0 File Offset: 0x00008FB0
		public LoadAllAppTypeGroupsResp LoadAllAppTypeGroups(LoadAllAppTypeGroupsReq Request)
		{
			return this.WrapServiceMethod<LoadAllAppTypeGroupsResp>(() => this.Proxy.LoadAllAppTypeGroups(Request));
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000ADE8 File Offset: 0x00008FE8
		public LoadAllAppTypesResp LoadAllAppTypes(LoadAllAppTypesReq Request)
		{
			return this.WrapServiceMethod<LoadAllAppTypesResp>(() => this.Proxy.LoadAllAppTypes(Request));
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000AE20 File Offset: 0x00009020
		public LoadAppTypeByIdResp LoadAppTypeById(LoadAppTypeByIdReq Request)
		{
			return this.WrapServiceMethod<LoadAppTypeByIdResp>(() => this.Proxy.LoadAppTypeById(Request));
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000AE58 File Offset: 0x00009058
		public LoadAppTypeGroupByIdResp LoadAppTypeGroupById(LoadAppTypeGroupByIdReq Request)
		{
			return this.WrapServiceMethod<LoadAppTypeGroupByIdResp>(() => this.Proxy.LoadAppTypeGroupById(Request));
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000AE90 File Offset: 0x00009090
		public LoadAppTypeGroupWithAppTypesByIdResp LoadAppTypeGroupWithAppTypesById(LoadAppTypeGroupWithAppTypesByIdReq Request)
		{
			return this.WrapServiceMethod<LoadAppTypeGroupWithAppTypesByIdResp>(() => this.Proxy.LoadAppTypeGroupWithAppTypesById(Request));
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000AEC8 File Offset: 0x000090C8
		public void UpdateAppType(UpdateAppTypeReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateAppType(Request);
			});
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000AF00 File Offset: 0x00009100
		public void UpdateAppTypeGroup(UpdateAppTypeGroupReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateAppTypeGroup(Request);
			});
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000AF38 File Offset: 0x00009138
		public GetAllowedAppTypeIdsResp GetAllowedAppTypeIds(GetAllowedAppTypeIdsReq Request)
		{
			return this.WrapServiceMethod<GetAllowedAppTypeIdsResp>(() => this.Proxy.GetAllowedAppTypeIds(Request));
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000AF70 File Offset: 0x00009170
		public LoadAllowedAppTypesResp LoadAllowedAppTypes(LoadAllowedAppTypesReq Request)
		{
			return this.WrapServiceMethod<LoadAllowedAppTypesResp>(() => this.Proxy.LoadAllowedAppTypes(Request));
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000AFA8 File Offset: 0x000091A8
		public LoadAllowedTestExamRelatedAppTypesResp LoadAllowedTestExamRelatedAppTypes(LoadAllowedTestExamRelatedAppTypesReq Request)
		{
			return this.WrapServiceMethod<LoadAllowedTestExamRelatedAppTypesResp>(() => this.Proxy.LoadAllowedTestExamRelatedAppTypes(Request));
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000AFE0 File Offset: 0x000091E0
		public LoadAllowedWorkshopRelatedAppTypesResp LoadAllowedWorkshopRelatedAppTypes(LoadAllowedWorkshopRelatedAppTypesReq Request)
		{
			return this.WrapServiceMethod<LoadAllowedWorkshopRelatedAppTypesResp>(() => this.Proxy.LoadAllowedWorkshopRelatedAppTypes(Request));
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000B018 File Offset: 0x00009218
		public LoadAppTypeWithExtendedInfoIdByIdResp LoadAppTypeWithExtendedInfoIdById(LoadAppTypeWithExtendedInfoIdByIdReq Request)
		{
			return this.WrapServiceMethod<LoadAppTypeWithExtendedInfoIdByIdResp>(() => this.Proxy.LoadAppTypeWithExtendedInfoIdById(Request));
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000B050 File Offset: 0x00009250
		public UpdateAppTypeWithExtendedInfoResp UpdateAppTypeWithExtendedInfo(UpdateAppTypeWithExtendedInfoReq Request)
		{
			return this.WrapServiceMethod<UpdateAppTypeWithExtendedInfoResp>(() => this.Proxy.UpdateAppTypeWithExtendedInfo(Request));
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000B088 File Offset: 0x00009288
		public CreateAppTypeWithExtendedInfoResp CreateAppTypeWithExtendedInfo(CreateAppTypeWithExtendedInfoReq Request)
		{
			return this.WrapServiceMethod<CreateAppTypeWithExtendedInfoResp>(() => this.Proxy.CreateAppTypeWithExtendedInfo(Request));
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000B0C0 File Offset: 0x000092C0
		public LoadAllAppTypesWithGroupsResp LoadAllAppTypesWithGroups(LoadAllAppTypesWithGroupsReq Request)
		{
			return this.WrapServiceMethod<LoadAllAppTypesWithGroupsResp>(() => this.Proxy.LoadAllAppTypesWithGroups(Request));
		}
	}
}
