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
	// Token: 0x02000042 RID: 66
	public class FormApprovalTraineeServiceManager : IFormApprovalTrainee, IService
	{
		// Token: 0x06000297 RID: 663 RVA: 0x0000D080 File Offset: 0x0000B280
		private bool IsAllowedToAccessTraineeFunctions(OperationContext opContext, int screenNum)
		{
			IFormApprovalManager formApprovalManager = new FormApprovalManager(opContext);
			FormApprovalScreenUserOptions formApprovalScreenUserForLoggedInUserOptions = formApprovalManager.GetFormApprovalScreenUserForLoggedInUserOptions(screenNum);
			return formApprovalScreenUserForLoggedInUserOptions != null && formApprovalScreenUserForLoggedInUserOptions.IsEnabled;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000D0B0 File Offset: 0x0000B2B0
		private bool IsAllowedToAccessTraineeFunctions(OperationContext opContext, Guid formApprovalId)
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
					result = (formApprovalScreenUserForLoggedInUserOptions != null && formApprovalScreenUserForLoggedInUserOptions.IsEnabled);
				}
			}
			return result;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000D10C File Offset: 0x0000B30C
		public LoadFormApprovalForTraineeResp LoadFormApprovalForTrainee(LoadFormApprovalForTraineeReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			bool flag = !this.IsAllowedToAccessTraineeFunctions(operationContext, Request.ScreenNum);
			if (flag)
			{
				throw new PermissionDeniedException("Not allowed to access trainee functions:pid=" + ((operationContext != null) ? operationContext.WhoAmI.ToString() : null) + ":screenNum=" + Request.ScreenNum.ToString());
			}
			IFormApprovalTraineeManager formApprovalTraineeManager = new FormApprovalTraineeManager(operationContext);
			FormApprovalForAppointment formApprovalForAppointment = formApprovalTraineeManager.LoadFormApproval(Request.ScreenNum, Request.StudentPersonId, Request.AppointmentId);
			return new LoadFormApprovalForTraineeResp
			{
				FormApproval = ((formApprovalForAppointment != null) ? formApprovalForAppointment.ToDTO() : null)
			};
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000D1AC File Offset: 0x0000B3AC
		public AddFormApprovalCommentForTraineeResp AddFormApprovalCommentForTrainee(AddFormApprovalCommentForTraineeReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			bool flag = !this.IsAllowedToAccessTraineeFunctions(operationContext, Request.FormApprovalId);
			if (flag)
			{
				throw new PermissionDeniedException("Not allowed to access trainee functions:pid=" + ((operationContext != null) ? operationContext.WhoAmI.ToString() : null) + ":screenNum=" + Request.FormApprovalId.ToString());
			}
			IFormApprovalTraineeManager formApprovalTraineeManager = new FormApprovalTraineeManager(operationContext);
			IFormApprovalTraineeManager formApprovalTraineeManager2 = formApprovalTraineeManager;
			Guid formApprovalId = Request.FormApprovalId;
			FormApprovalCommentTextDTO comment = Request.Comment;
			formApprovalTraineeManager2.AddFormApprovalComment(formApprovalId, (comment != null) ? comment.ToDomainObject() : null);
			return new AddFormApprovalCommentForTraineeResp();
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000D244 File Offset: 0x0000B444
		public ReSubmitFormApprovalFormResp ReSubmitFormApprovalForm(ReSubmitFormApprovalFormReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			bool flag = !this.IsAllowedToAccessTraineeFunctions(operationContext, Request.FormApprovalId);
			if (flag)
			{
				throw new PermissionDeniedException("Not allowed to access trainee functions:pid=" + ((operationContext != null) ? operationContext.WhoAmI.ToString() : null) + ":screenNum=" + Request.FormApprovalId.ToString());
			}
			IFormApprovalTraineeManager formApprovalTraineeManager = new FormApprovalTraineeManager(operationContext);
			IFormApprovalTraineeManager formApprovalTraineeManager2 = formApprovalTraineeManager;
			Guid formApprovalId = Request.FormApprovalId;
			FormApprovalCommentTextDTO comment = Request.Comment;
			formApprovalTraineeManager2.ReSubmitFormApprovalForm(formApprovalId, (comment != null) ? comment.ToDomainObject() : null);
			return new ReSubmitFormApprovalFormResp();
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000D2DC File Offset: 0x0000B4DC
		public CreateFormApprovalFormResp CreateFormApprovalForm(CreateFormApprovalFormReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			bool flag = !this.IsAllowedToAccessTraineeFunctions(operationContext, Request.ScreenNum);
			if (flag)
			{
				throw new PermissionDeniedException("Not allowed to access trainee functions:pid=" + ((operationContext != null) ? operationContext.WhoAmI.ToString() : null) + ":screenNum=" + Request.ScreenNum.ToString());
			}
			IFormApprovalTraineeManager formApprovalTraineeManager = new FormApprovalTraineeManager(operationContext);
			IFormApprovalTraineeManager formApprovalTraineeManager2 = formApprovalTraineeManager;
			int screenNum = Request.ScreenNum;
			int studentPersonId = Request.StudentPersonId;
			int appointmentId = Request.AppointmentId;
			FormApprovalCommentTextDTO comment = Request.Comment;
			FormApprovalCommentText comment2 = (comment != null) ? comment.ToDomainObject() : null;
			FormApprovalSignatureDTO traineeSignature = Request.TraineeSignature;
			Guid formApprovalId = formApprovalTraineeManager2.CreateFormApprovalForm(screenNum, studentPersonId, appointmentId, comment2, (traineeSignature != null) ? traineeSignature.ToDomainObject() : null);
			return new CreateFormApprovalFormResp
			{
				FormApprovalId = formApprovalId
			};
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000D398 File Offset: 0x0000B598
		public LoadTraineeSignatureResp LoadTraineeSignature(LoadTraineeSignatureReq Request)
		{
			IFormApprovalTraineeManager formApprovalTraineeManager = new FormApprovalTraineeManager(Request.GetOperationContext());
			FormApprovalSignature formApprovalSignature = formApprovalTraineeManager.LoadTraineeSignature(Request.FormApprovalId);
			return new LoadTraineeSignatureResp
			{
				Signature = ((formApprovalSignature != null) ? formApprovalSignature.ToDTO() : null)
			};
		}
	}
}
