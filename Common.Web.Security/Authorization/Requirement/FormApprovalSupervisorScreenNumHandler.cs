using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TechnoPro.Common.ICore.DynamicForms.FormApproval;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Extensions;

namespace TechnoPro.Common.Web.Security.Authorization.Requirement
{
	// Token: 0x02000015 RID: 21
	public class FormApprovalSupervisorScreenNumHandler : AuthorizationHandler<FormApprovalSupervisorScreenNumRequirement, int>
	{
		// Token: 0x0600008C RID: 140 RVA: 0x000037B4 File Offset: 0x000019B4
		public FormApprovalSupervisorScreenNumHandler(OperationContext opContext)
		{
			this._opContext = opContext;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000037C4 File Offset: 0x000019C4
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, FormApprovalSupervisorScreenNumRequirement requirement, int screenNum)
		{
			IFormApprovalManager formApprovalManager = ObjectFactory.Resolve<IFormApprovalManager>();
			formApprovalManager.OpContext = (this._opContext ?? context.User.GetOperationContext());
			if (screenNum < 1)
			{
				context.Fail();
			}
			else
			{
				FormApprovalScreenUserOptions formApprovalScreenUserForLoggedInUserOptions = formApprovalManager.GetFormApprovalScreenUserForLoggedInUserOptions(screenNum);
				if (formApprovalScreenUserForLoggedInUserOptions != null && formApprovalScreenUserForLoggedInUserOptions.IsEnabled && formApprovalScreenUserForLoggedInUserOptions.IsSupervisor)
				{
					context.Succeed(requirement);
				}
				else
				{
					context.Fail();
				}
			}
			return Task.CompletedTask;
		}

		// Token: 0x0400001D RID: 29
		private readonly OperationContext _opContext;
	}
}
