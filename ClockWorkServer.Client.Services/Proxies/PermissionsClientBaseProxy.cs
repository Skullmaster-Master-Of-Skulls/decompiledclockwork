using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000162 RID: 354
	internal class PermissionsClientBaseProxy : ClientBase<IPermissions>, IPermissions, IService
	{
		// Token: 0x06000DA3 RID: 3491 RVA: 0x00021C1D File Offset: 0x0001FE1D
		public PermissionsClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x00021C28 File Offset: 0x0001FE28
		public PermissionsClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x00021C34 File Offset: 0x0001FE34
		public LoadUserPermissionIsAllowedSetResp LoadUserPermissionIsAllowedSet(LoadUserPermissionIsAllowedSetReq Request)
		{
			return base.Channel.LoadUserPermissionIsAllowedSet(Request);
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x00021C54 File Offset: 0x0001FE54
		public LoadJustUserPermissionsResp LoadJustUserPermissions(LoadJustUserPermissionsReq Request)
		{
			return base.Channel.LoadJustUserPermissions(Request);
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00021C74 File Offset: 0x0001FE74
		public LoadJustGroupPermissionsResp LoadJustGroupPermissions(LoadJustGroupPermissionsReq Request)
		{
			return base.Channel.LoadJustGroupPermissions(Request);
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00021C92 File Offset: 0x0001FE92
		public void UpdateJustUserOrGroupPermissions(UpdateJustUserOrGroupPermissionsReq Request)
		{
			base.Channel.UpdateJustUserOrGroupPermissions(Request);
		}
	}
}
