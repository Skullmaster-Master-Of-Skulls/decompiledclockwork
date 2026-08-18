using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.ClientManager.ICore.Appointments;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Appointments
{
	// Token: 0x02000071 RID: 113
	public class AppointmentTypeRestClientManager : BearerTokenRestProxy<IAppointmentTypeClientManager>, IAppointmentTypeClientManager, IWebService
	{
		// Token: 0x06000440 RID: 1088 RVA: 0x0000C7FC File Offset: 0x0000A9FC
		public AppointmentTypeRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000C806 File Offset: 0x0000AA06
		public AppointmentTypeRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000C811 File Offset: 0x0000AA11
		public IList<int> GetAppointmentTypeAssociatedPerAppScreenNums(int AppTypeId)
		{
			return base.GetMany<int>(string.Format("appointmenttype/associated/apptypeid/{0}", AppTypeId), true);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000C82A File Offset: 0x0000AA2A
		public AppTypeDTO LoadAppTypeById(int AppTypeId)
		{
			return base.Get<AppTypeDTO>(string.Format("appointmenttype/{0}", AppTypeId), true);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000C843 File Offset: 0x0000AA43
		public IList<AppTypeDTO> LoadAllAppTypes()
		{
			return base.GetMany<AppTypeDTO>("appointmenttype?ignorecache=false", true);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000C851 File Offset: 0x0000AA51
		public IList<AppTypeDTO> LoadAllAppTypes(bool ignoreCache)
		{
			return base.GetMany<AppTypeDTO>(string.Format("appointmenttype?ignorecache={0}", ignoreCache), true);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000C86A File Offset: 0x0000AA6A
		public void UpdateAppType(AppTypeDTO AppType)
		{
			base.Put<AppTypeDTO>(AppType, "appointmenttype");
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000C878 File Offset: 0x0000AA78
		public int CreateAppType(AppTypeDTO AppType)
		{
			return base.Post<AppTypeDTO, int>(AppType, "appointmenttype");
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000C886 File Offset: 0x0000AA86
		public void DeleteAppType(int AppTypeId, int AppTypeIdToReplaceWith)
		{
			base.Delete(string.Format("appointmenttype/apptypeid/{0}/apptypeidtoreplacewithinexistingapps/{1}", AppTypeId, AppTypeIdToReplaceWith));
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000C8A4 File Offset: 0x0000AAA4
		public void DisableAppType(int AppTypeId)
		{
			base.Post<int>(AppTypeId, string.Format("appointmenttype/disable/apptypeid/{0}", AppTypeId));
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000C8BD File Offset: 0x0000AABD
		public AppTypeGroupDTO LoadAppTypeGroupById(int AppointmentTypeGroupId)
		{
			return base.Get<AppTypeGroupDTO>(string.Format("appointmenttype/apptypegroup/id/{0}", AppointmentTypeGroupId), true);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000C8D6 File Offset: 0x0000AAD6
		public IList<AppTypeGroupWithAppTypesDTO> LoadAllAppTypeGroups()
		{
			return base.GetMany<AppTypeGroupWithAppTypesDTO>("appointmenttype/apptypegroupwithapptypes", true);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000C8E4 File Offset: 0x0000AAE4
		public AppTypeGroupWithAppTypesDTO LoadAppTypeGroupWithAppTypesById(int AppointmentTypeGroupId)
		{
			return base.Get<AppTypeGroupWithAppTypesDTO>(string.Format("appointmenttype/apptypegroupwithapptypes/apptypegroupid/{0}", AppointmentTypeGroupId), true);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000C8FD File Offset: 0x0000AAFD
		public void DeleteAppTypeGroup(int AppointmentTypeGroupId)
		{
			base.Delete(string.Format("appointmenttype/apptypegroup/id/{0}", AppointmentTypeGroupId));
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000C915 File Offset: 0x0000AB15
		public int CreateAppTypeGroup(AppTypeGroupDTO AppTypeGroup)
		{
			return base.Post<AppTypeGroupDTO, int>(AppTypeGroup, "appointmenttype/apptypegroup");
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0000C923 File Offset: 0x0000AB23
		public void UpdateAppTypeGroup(AppTypeGroupDTO AppTypeGroup)
		{
			base.Put<AppTypeGroupDTO>(AppTypeGroup, "appointmenttype/apptypegroup");
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000C931 File Offset: 0x0000AB31
		public IList<int> GetAllowedAppTypeIds(int personId)
		{
			return base.GetMany<int>(string.Format("appointmenttype/allowapptypeids/pid/{0}", personId), true);
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000C94A File Offset: 0x0000AB4A
		public IList<AppTypeDTO> LoadAllowedAppTypes()
		{
			return base.GetMany<AppTypeDTO>("appointmenttype/allowapptypes", true);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000C958 File Offset: 0x0000AB58
		public IList<AppTypeDTO> LoadAllowedTestExamRelatedAppTypes()
		{
			return base.GetMany<AppTypeDTO>("appointmenttype/allowtestexamapptypes", true);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000C966 File Offset: 0x0000AB66
		public IList<AppTypeDTO> LoadAllowedWorkshopRelatedAppTypes()
		{
			return base.GetMany<AppTypeDTO>("appointmenttype/allowworkshopsapptypes", true);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000C974 File Offset: 0x0000AB74
		public AppTypeWithExtendedInfoDTO LoadAppTypeWithExtendedInfoIdById(int appTypeId)
		{
			return base.Get<AppTypeWithExtendedInfoDTO>(string.Format("appointmenttype/apptypeswithextendedinfo/apptypeid/{0}", appTypeId), true);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000C98D File Offset: 0x0000AB8D
		public void UpdateAppTypeWithExtendedInfo(AppTypeWithExtendedInfoDTO AppType)
		{
			base.Put<AppTypeWithExtendedInfoDTO>(AppType, "appointmenttype/apptypeswithextendedinfo");
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000C99B File Offset: 0x0000AB9B
		public int CreateAppTypeWithExtendedInfo(AppTypeWithExtendedInfoDTO AppType)
		{
			return base.Post<AppTypeWithExtendedInfoDTO, int>(AppType, "appointmenttype/apptypeswithextendedinfo");
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000C9A9 File Offset: 0x0000ABA9
		public IList<AppTypeGroupWithAppTypesDTO> LoadAllAppTypesWithGroups(bool ignoreCache)
		{
			return base.GetMany<AppTypeGroupWithAppTypesDTO>("appointmenttype/apptypegroupwithapptypes", true);
		}
	}
}
