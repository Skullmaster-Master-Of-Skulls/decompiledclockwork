using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions;
using TechnoPro.Common.ClientManager.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.UserSettingsPermissions
{
	// Token: 0x02000006 RID: 6
	public class PermissionStoreRestClientManager : BearerTokenRestProxy<IPermissionStoreClientManager>, IPermissionStoreClientManager, IWebService
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002732 File Offset: 0x00000932
		public PermissionStoreRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000273C File Offset: 0x0000093C
		public PermissionStoreRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002747 File Offset: 0x00000947
		public UserPermissionIsAllowedSetDTO LoadUserPermissionIsAllowedSet(int pid)
		{
			return base.Get<UserPermissionIsAllowedSetDTO>(string.Format("permissions/userpermissionisallowedset/pid/{0}", pid), true);
		}
	}
}
