using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000161 RID: 353
	public class PermissionsReusableClientProxy : WCFTokenBasedReusableClientProxy<IPermissions>, IPermissions, IService
	{
		// Token: 0x06000D9D RID: 3485 RVA: 0x00021B26 File Offset: 0x0001FD26
		public PermissionsReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00021B31 File Offset: 0x0001FD31
		public PermissionsReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x00021B40 File Offset: 0x0001FD40
		public LoadUserPermissionIsAllowedSetResp LoadUserPermissionIsAllowedSet(LoadUserPermissionIsAllowedSetReq Request)
		{
			return this.WrapServiceMethod<LoadUserPermissionIsAllowedSetResp>(() => this.Proxy.LoadUserPermissionIsAllowedSet(Request));
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00021B78 File Offset: 0x0001FD78
		public LoadJustUserPermissionsResp LoadJustUserPermissions(LoadJustUserPermissionsReq Request)
		{
			return this.WrapServiceMethod<LoadJustUserPermissionsResp>(() => this.Proxy.LoadJustUserPermissions(Request));
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x00021BB0 File Offset: 0x0001FDB0
		public LoadJustGroupPermissionsResp LoadJustGroupPermissions(LoadJustGroupPermissionsReq Request)
		{
			return this.WrapServiceMethod<LoadJustGroupPermissionsResp>(() => this.Proxy.LoadJustGroupPermissions(Request));
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00021BE8 File Offset: 0x0001FDE8
		public void UpdateJustUserOrGroupPermissions(UpdateJustUserOrGroupPermissionsReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateJustUserOrGroupPermissions(Request);
			});
		}
	}
}
