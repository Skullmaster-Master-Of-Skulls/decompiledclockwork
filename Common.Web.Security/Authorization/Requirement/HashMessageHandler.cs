using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TechnoPro.Common.ICore.Membership;
using TechnoPro.Common.Public.Entities.Authentication;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Web.Security.Authorization.Requirement
{
	// Token: 0x0200001C RID: 28
	public class HashMessageHandler : AuthorizationHandler<HashMessageRequirement, ClockWorkHashAuthentication>
	{
		// Token: 0x06000097 RID: 151 RVA: 0x000039B6 File Offset: 0x00001BB6
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HashMessageRequirement requirement, ClockWorkHashAuthentication resource)
		{
			if (ObjectFactory.Resolve<IMembershipManager>().ValidateClockWorkHashingAuthentication(resource))
			{
				context.Succeed(requirement);
			}
			else
			{
				context.Fail();
			}
			return Task.CompletedTask;
		}
	}
}
