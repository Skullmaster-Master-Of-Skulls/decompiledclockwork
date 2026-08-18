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
	// Token: 0x02000016 RID: 22
	public class FormApprovalSupervisorIdHandler : AuthorizationHandler<FormApprovalSupervisorIdRequirement, Guid>
	{
		// Token: 0x0600008E RID: 142 RVA: 0x0000382D File Offset: 0x00001A2D
		public FormApprovalSupervisorIdHandler(OperationContext opContext)
		{
			this._opContext = opContext;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000383C File Offset: 0x00001A3C
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, FormApprovalSupervisorIdRequirement requirement, Guid formApprovalId)
		{
			if (formApprovalId == Guid.Empty)
			{
				context.Fail();
			}
			else
			{
				IFormApprovalManager formApprovalManager = ObjectFactory.Resolve<IFormApprovalManager>();
				formApprovalManager.OpContext = (this._opContext ?? context.User.GetOperationContext());
				int screenNumForFormApproval = formApprovalManager.GetScreenNumForFormApproval(formApprovalId);
				if (screenNumForFormApproval < 1)
				{
					context.Fail();
				}
				else
				{
					FormApprovalScreenUserOptions formApprovalScreenUserForLoggedInUserOptions = formApprovalManager.GetFormApprovalScreenUserForLoggedInUserOptions(screenNumForFormApproval);
					if (formApprovalScreenUserForLoggedInUserOptions != null && formApprovalScreenUserForLoggedInUserOptions.IsEnabled && formApprovalScreenUserForLoggedInUserOptions.IsSupervisor)
					{
						context.Succeed(requirement);
					}
					else
					{
						context.Fail();
					}
				}
			}
			return Task.CompletedTask;
		}

		// Token: 0x0400001E RID: 30
		private readonly OperationContext _opContext;
	}
}
