using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.Common.Core.DynamicForms.FormApproval;
using TechnoPro.Common.Core.Mappers.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.Common.ICore.DynamicForms.FormApproval;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.Common.Public.Exceptions.PermissionDenied;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000041 RID: 65
	public class FormApprovalSupervisorServiceManager : IFormApprovalSupervisor, IService
	{
		// Token: 0x0600028E RID: 654 RVA: 0x0000CC78 File Offset: 0x0000AE78
		private bool IsAllowedToAccessSupervisorFunctions(OperationContext opContext, int screenNum)
		{
			IFormApprovalManager formApprovalManager = new FormApprovalManager(opContext);
			FormApprovalScreenUserOptions formApprovalScreenUserForLoggedInUserOptions = formApprovalManager.GetFormApprovalScreenUserForLoggedInUserOptions(screenNum);
			bool flag = formApprovalScreenUserForLoggedInUserOptions == null || !formApprovalScreenUserForLoggedInUserOptions.IsEnabled;
			return !flag && formApprovalScreenUserForLoggedInUserOptions.IsSupervisor;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000CCB8 File Offset: 0x0000AEB8
		private bool IsAllowedToAccessSupervisorFunctions(OperationContext opContext, Guid formApprovalId)
		{
			bool flag = formApprovalId == Guid.Empty;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				IFormApprovalManager formApprovalManager = new FormApprovalManager(opContext);
				int screenNumForFormApproval = formApprovalManager.GetScreenNumForFormApproval(formApprovalId);
				bool flag2 = screenNumForFormApproval < 1;
				if (flag2)
				{
					result = false;
				}
				else
				{
					FormApprovalScreenUserOptions formApprovalScreenUserForLoggedInUserOptions = formApprovalManager.GetFormApprovalScreenUserForLoggedInUserOptions(screenNumForFormApproval);
					bool flag3 = formApprovalScreenUserForLoggedInUserOptions == null || !formApprovalScreenUserForLoggedInUserOptions.IsEnabled;
					result = (!flag3 && formApprovalScreenUserForLoggedInUserOptions.IsSupervisor);
				}
			}
			return result;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000CD28 File Offset: 0x0000AF28
		public LoadFormApprovalForSupervisorResp LoadFormApprovalForSupervisor(LoadFormApprovalForSupervisorReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			bool flag = !this.IsAllowedToAccessSupervisorFunctions(operationContext, Request.ScreenNum);
			if (flag)
			{
				throw new PermissionDeniedException("Not allowed to access supervisor functions:pid=" + ((operationContext != null) ? operationContext.WhoAmI.ToString() : null) + ":screenNum=" + Request.ScreenNum.ToString());
			}
			IFormApprovalSupervisorManager formApprovalSupervisorManager = new FormApprovalSupervisorManager(operationContext);
			FormApprovalForAppointment formApprovalForAppointment = formApprovalSupervisorManager.LoadFormApproval(Request.ScreenNum, Request.StudentPersonId, Request.AppointmentId);
			return new LoadFormApprovalForSupervisorResp
			{
				FormApproval = ((formApprovalForAppointment != null) ? formApprovalForAppointment.ToDTO() : null)
			};
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000CDC8 File Offset: 0x0000AFC8
		public AddFormApprovalCommentForSupervisorResp AddFormApprovalCommentForSupervisor(AddFormApprovalCommentForSupervisorReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			bool flag = !this.IsAllowedToAccessSupervisorFunctions(operationContext, Request.FormApprovalId);
			if (flag)
			{
				throw new PermissionDeniedException("Not allowed to access supervisor functions:pid=" + ((operationContext != null) ? operationContext.WhoAmI.ToString() : null) + ":formApprovalId=" + Request.FormApprovalId.ToString());
			}
			IFormApprovalSupervisorManager formApprovalSupervisorManager = new FormApprovalSupervisorManager(operationContext);
			IFormApprovalSupervisorManager formApprovalSupervisorManager2 = formApprovalSupervisorManager;
			Guid formApprovalId = Request.FormApprovalId;
			FormApprovalCommentTextDTO comment = Request.Comment;
			formApprovalSupervisorManager2.AddFormApprovalComment(formApprovalId, (comment != null) ? comment.ToDomainObject() : null);
			return new AddFormApprovalCommentForSupervisorResp();
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000CE60 File Offset: 0x0000B060
		public ApproveFormResp ApproveForm(ApproveFormReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			bool flag = !this.IsAllowedToAccessSupervisorFunctions(operationContext, Request.FormApprovalId);
			if (flag)
			{
				throw new PermissionDeniedException("Not allowed to access supervisor functions:pid=" + ((operationContext != null) ? operationContext.WhoAmI.ToString() : null) + ":formApprovalId=" + Request.FormApprovalId.ToString());
			}
			IFormApprovalSupervisorManager formApprovalSupervisorManager = new FormApprovalSupervisorManager(operationContext);
			IFormApprovalSupervisorManager formApprovalSupervisorManager2 = formApprovalSupervisorManager;
			Guid formApprovalId = Request.FormApprovalId;
			FormApprovalCommentTextDTO comment = Request.Comment;
			FormApprovalCommentText comment2 = (comment != null) ? comment.ToDomainObject() : null;
			FormApprovalSignatureDTO supervisorSignature = Request.SupervisorSignature;
			formApprovalSupervisorManager2.ApproveForm(formApprovalId, comment2, (supervisorSignature != null) ? supervisorSignature.ToDomainObject() : null);
			return new ApproveFormResp();
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000CF0C File Offset: 0x0000B10C
		public SendFormBackToTraineeResp SendFormBackToTrainee(SendFormBackToTraineeReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			bool flag = !this.IsAllowedToAccessSupervisorFunctions(operationContext, Request.FormApprovalId);
			if (flag)
			{
				throw new PermissionDeniedException("Not allowed to access supervisor functions:pid=" + ((operationContext != null) ? operationContext.WhoAmI.ToString() : null) + ":formApprovalId=" + Request.FormApprovalId.ToString());
			}
			IFormApprovalSupervisorManager formApprovalSupervisorManager = new FormApprovalSupervisorManager(operationContext);
			IFormApprovalSupervisorManager formApprovalSupervisorManager2 = formApprovalSupervisorManager;
			Guid formApprovalId = Request.FormApprovalId;
			FormApprovalCommentTextDTO comment = Request.Comment;
			formApprovalSupervisorManager2.SendFormBackToTrainee(formApprovalId, (comment != null) ? comment.ToDomainObject() : null);
			return new SendFormBackToTraineeResp();
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000CFA4 File Offset: 0x0000B1A4
		public UnApproveFormApprovalResp UnApproveFormApproval(UnApproveFormApprovalReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			bool flag = !this.IsAllowedToAccessSupervisorFunctions(operationContext, Request.FormApprovalId);
			if (flag)
			{
				throw new PermissionDeniedException("Not allowed to access supervisor functions:pid=" + ((operationContext != null) ? operationContext.WhoAmI.ToString() : null) + ":formApprovalId=" + Request.FormApprovalId.ToString());
			}
			IFormApprovalSupervisorManager formApprovalSupervisorManager = new FormApprovalSupervisorManager(operationContext);
			IFormApprovalSupervisorManager formApprovalSupervisorManager2 = formApprovalSupervisorManager;
			Guid formApprovalId = Request.FormApprovalId;
			FormApprovalCommentTextDTO commentText = Request.CommentText;
			formApprovalSupervisorManager2.UnApproveFormApproval(formApprovalId, (commentText != null) ? commentText.ToDomainObject() : null);
			return new UnApproveFormApprovalResp();
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000D03C File Offset: 0x0000B23C
		public LoadSupervisorSignatureResp LoadSupervisorSignature(LoadSupervisorSignatureReq Request)
		{
			IFormApprovalSupervisorManager formApprovalSupervisorManager = new FormApprovalSupervisorManager(Request.GetOperationContext());
			FormApprovalSignature formApprovalSignature = formApprovalSupervisorManager.LoadSupervisorSignature(Request.FormApprovalId);
			return new LoadSupervisorSignatureResp
			{
				Signature = ((formApprovalSignature != null) ? formApprovalSignature.ToDTO() : null)
			};
		}
	}
}
