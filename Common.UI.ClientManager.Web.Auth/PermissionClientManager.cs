using System;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions;
using TechnoPro.Common.ClientManager.Core.UserSettingsPermissions;
using TechnoPro.Common.ClientManager.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions;
using TechnoPro.Common.UI.ClientManager.Web.Core.Authentication;
using TechnoPro.Common.UI.Web.Entity.AuthenticationAuthorization;

namespace TechnoPro.Common.UI.ClientManager.Web.Auth
{
	// Token: 0x02000003 RID: 3
	public class PermissionClientManager : IPermissionClientManager
	{
		// Token: 0x06000007 RID: 7 RVA: 0x0000216C File Offset: 0x0000036C
		private static UserPermissionIsAllowedSetDTO LoadCurrentUserPermissionSet()
		{
			HttpSessionState session = HttpContext.Current.Session;
			UserPermissionIsAllowedSetDTO userPermissionIsAllowedSetDTO = (UserPermissionIsAllowedSetDTO)session["uPermissions"];
			bool flag = userPermissionIsAllowedSetDTO != null;
			UserPermissionIsAllowedSetDTO result;
			if (flag)
			{
				result = userPermissionIsAllowedSetDTO;
			}
			else
			{
				IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
				ClockWorkIdentity currentClockWorkIdentity = webAuthenticationAuthorizationWebClientManager.GetCurrentClockWorkIdentity(null);
				bool flag2 = currentClockWorkIdentity == null || currentClockWorkIdentity.PersonId < 1;
				if (flag2)
				{
					result = null;
				}
				else
				{
					IPermissionStoreClientManager permissionStoreClientManager = new PermissionStoreClientManager();
					userPermissionIsAllowedSetDTO = permissionStoreClientManager.LoadUserPermissionIsAllowedSet(currentClockWorkIdentity.PersonId);
					session.Add("uPermissions", userPermissionIsAllowedSetDTO);
					result = userPermissionIsAllowedSetDTO;
				}
			}
			return result;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021F8 File Offset: 0x000003F8
		public bool IsPersonAllowed(UserPermissionEnum PermissionCode)
		{
			UserPermissionIsAllowedSetDTO userPermissionIsAllowedSetDTO = PermissionClientManager.LoadCurrentUserPermissionSet();
			bool flag = userPermissionIsAllowedSetDTO == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				UserPermissionIsAllowedDTO userPermissionIsAllowedDTO = userPermissionIsAllowedSetDTO.GeneralPermissionsAllowed.FirstOrDefault((UserPermissionIsAllowedDTO g) => g.Permission == PermissionCode);
				bool flag2 = userPermissionIsAllowedDTO != null;
				if (flag2)
				{
					result = userPermissionIsAllowedDTO.IsAllowed;
				}
				else
				{
					CWLogger.Logger.Warn("PermissionsClientManager:Can'tFindPermission:PermissionCode={0}", PermissionCode.ToString());
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000227C File Offset: 0x0000047C
		public bool IsAllowedToViewScreen(int screenNum)
		{
			UserPermissionIsAllowedSetDTO userPermissionIsAllowedSetDTO = PermissionClientManager.LoadCurrentUserPermissionSet();
			return userPermissionIsAllowedSetDTO != null && userPermissionIsAllowedSetDTO.ScreenNumsAllowedViewScreen.Contains(screenNum);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022A8 File Offset: 0x000004A8
		public bool IsAllowedToModifyScreen(int screenNum)
		{
			UserPermissionIsAllowedSetDTO userPermissionIsAllowedSetDTO = PermissionClientManager.LoadCurrentUserPermissionSet();
			return userPermissionIsAllowedSetDTO != null && userPermissionIsAllowedSetDTO.ScreenNumsAllowedModifyScreen.Contains(screenNum);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022D4 File Offset: 0x000004D4
		public bool IsAllowedToCreateScreen(int screenNum)
		{
			UserPermissionIsAllowedSetDTO userPermissionIsAllowedSetDTO = PermissionClientManager.LoadCurrentUserPermissionSet();
			return userPermissionIsAllowedSetDTO != null && userPermissionIsAllowedSetDTO.ScreenNumsAllowedCreateScreen.Contains(screenNum);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002300 File Offset: 0x00000500
		public bool IsPersonAllowed(UserPermissionEnum PermissionCode, int val)
		{
			switch (PermissionCode)
			{
			case UserPermissionEnum.ViewScreen:
				return this.IsAllowedToViewScreen(val);
			case UserPermissionEnum.ModifyScreen:
				return this.IsAllowedToModifyScreen(val);
			case UserPermissionEnum.CreateScreen:
				return this.IsAllowedToCreateScreen(val);
			}
			return false;
		}

		// Token: 0x04000003 RID: 3
		private const string PermissionSetKey = "uPermissions";
	}
}
