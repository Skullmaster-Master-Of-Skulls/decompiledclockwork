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
	// Token: 0x02000019 RID: 25
	public class FormApprovalTraineeScreenNumHandler : AuthorizationHandler<FormApprovalSupervisorScreenNumRequirement, int>
	{
		// Token: 0x06000092 RID: 146 RVA: 0x000038C2 File Offset: 0x00001AC2
		public FormApprovalTraineeScreenNumHandler(OperationContext opContext)
		{
			this._opContext = opContext;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000038D4 File Offset: 0x00001AD4
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, FormApprovalSupervisorScreenNumRequirement requirement, int screenNum)
		{
			IFormApprovalManager formApprovalManager = ObjectFactory.Resolve<IFormApprovalManager>();
			formApprovalManager.OpContext = (this._opContext ?? context.User.GetOperationContext());
			FormApprovalScreenUserOptions formApprovalScreenUserForLoggedInUserOptions = formApprovalManager.GetFormApprovalScreenUserForLoggedInUserOptions(screenNum);
			if (formApprovalScreenUserForLoggedInUserOptions != null && formApprovalScreenUserForLoggedInUserOptions.IsEnabled)
			{
				context.Succeed(requirement);
			}
			else
			{
				context.Fail();
			}
			return Task.CompletedTask;
		}

		// Token: 0x0400001F RID: 31
		private readonly OperationContext _opContext;
	}
}
