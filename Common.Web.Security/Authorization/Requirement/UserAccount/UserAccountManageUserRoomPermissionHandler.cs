using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Web.Security.Authorization.Requirement.UserAccount
{
	// Token: 0x0200001E RID: 30
	public class UserAccountManageUserRoomPermissionHandler : AuthorizationHandler<UserAccountRequirement>
	{
		// Token: 0x0600009C RID: 156 RVA: 0x000039F2 File Offset: 0x00001BF2
		public UserAccountManageUserRoomPermissionHandler(OperationContext opContext)
		{
			this._opContext = opContext;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003A04 File Offset: 0x00001C04
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserAccountRequirement requirement)
		{
			if (requirement.HasManageUserRoomPermission)
			{
				IPeopleGroupManager peopleGroupManager = ObjectFactory.Resolve<IPeopleGroupManager>();
				peopleGroupManager.OpContext = this._opContext;
				if (peopleGroupManager.HasManageUserRoomPermissions(this._opContext.WhoAmI))
				{
					context.Succeed(requirement);
				}
				else
				{
					context.Fail();
				}
			}
			else
			{
				context.Succeed(requirement);
			}
			return Task.CompletedTask;
		}

		// Token: 0x04000022 RID: 34
		private readonly OperationContext _opContext;
	}
}
