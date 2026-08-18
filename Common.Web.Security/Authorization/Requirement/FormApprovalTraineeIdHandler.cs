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
	// Token: 0x0200001A RID: 26
	public class FormApprovalTraineeIdHandler : AuthorizationHandler<FormApprovalSupervisorIdRequirement, Guid>
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00003929 File Offset: 0x00001B29
		public FormApprovalTraineeIdHandler(OperationContext opContext)
		{
			this._opContext = opContext;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003938 File Offset: 0x00001B38
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, FormApprovalSupervisorIdRequirement requirement, Guid formApprovalId)
		{
			if (formApprovalId == Guid.Empty)
			{
				return Task.CompletedTask;
			}
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
				if (formApprovalScreenUserForLoggedInUserOptions != null && formApprovalScreenUserForLoggedInUserOptions.IsEnabled)
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

		// Token: 0x04000020 RID: 32
		private readonly OperationContext _opContext;
	}
}
