using System;
using TechnoPro.Common.DAO.DynamicForms.FormApproval;
using TechnoPro.Common.DAO.Impl.DynamicForms.FormApproval;
using TechnoPro.Common.ICore.DynamicForms.FormApproval;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.Common.Public.Exceptions.InvalidParameters;

namespace TechnoPro.Common.Core.DynamicForms.FormApproval
{
	// Token: 0x02000104 RID: 260
	public class FormApprovalTraineeManager : IFormApprovalTraineeManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000A9B RID: 2715 RVA: 0x0000672B File Offset: 0x0000492B
		public FormApprovalTraineeManager()
		{
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00044635 File Offset: 0x00042835
		public FormApprovalTraineeManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x00044647 File Offset: 0x00042847
		// (set) Token: 0x06000A9E RID: 2718 RVA: 0x0004464F File Offset: 0x0004284F
		public OperationContext OpContext { get; set; }

		// Token: 0x06000A9F RID: 2719 RVA: 0x00044658 File Offset: 0x00042858
		public FormApprovalSignature LoadTraineeSignature(Guid formApprovalId)
		{
			bool flag = formApprovalId == Guid.Empty;
			if (flag)
			{
				throw new NullOrInvalidIdParameterException("FormApprovalTraineeManager:LoadTraineeSignature:Empty form ApprovalId");
			}
			IFormApprovalTraineeDAO formApprovalTraineeDAO = new FormApprovalTraineeDAO(this.OpContext);
			return formApprovalTraineeDAO.LoadTraineeSignature(formApprovalId);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00044698 File Offset: 0x00042898
		public FormApprovalForAppointment LoadFormApproval(int screenNum, int studentPersonId, int appId)
		{
			bool flag = screenNum < 1 || studentPersonId < 1 || appId < 1;
			if (flag)
			{
				throw new NullOrInvalidIdParameterException(string.Format("FormApprovalTraineeManager:LoadFormApproval:screenNum={0}:pid={1}:appId={2}", screenNum, studentPersonId, appId));
			}
			IFormApprovalSupervisorManager formApprovalSupervisorManager = new FormApprovalSupervisorManager(this.OpContext);
			return formApprovalSupervisorManager.LoadFormApproval(screenNum, studentPersonId, appId);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x000446F4 File Offset: 0x000428F4
		public void AddFormApprovalComment(Guid formApprovalId, FormApprovalCommentText comment)
		{
			bool flag = formApprovalId == Guid.Empty;
			if (flag)
			{
				throw new NullOrInvalidIdParameterException("FormApprovalTraineeManager:AddFormApprovalComment:Empty form ApprovalId");
			}
			IFormApprovalSupervisorManager formApprovalSupervisorManager = new FormApprovalSupervisorManager(this.OpContext);
			formApprovalSupervisorManager.AddFormApprovalComment(formApprovalId, comment);
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00044734 File Offset: 0x00042934
		public void ReSubmitFormApprovalForm(Guid formApprovalId, FormApprovalCommentText comment)
		{
			bool flag = formApprovalId == Guid.Empty;
			if (flag)
			{
				throw new NullOrInvalidIdParameterException("FormApprovalTraineeManager:ReSubmitFormApprovalForm:Empty form ApprovalId");
			}
			IFormApprovalDAO formApprovalDAO = new FormApprovalDAO(this.OpContext);
			formApprovalDAO.UpdateFormApprovalCurrentStatus(formApprovalId, eFormApprovalState.WaitingForSupervisorToApprove);
			string text = (comment != null) ? comment.CommentText : null;
			text = (string.IsNullOrEmpty(text) ? "Re-submitted for approval" : ("[Re-submitted for approval]\r\n" + text));
			formApprovalDAO.AddFormApprovalComment(formApprovalId, text);
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x000447A4 File Offset: 0x000429A4
		public Guid CreateFormApprovalForm(int screenNum, int studentPersonId, int appId, FormApprovalCommentText comment, FormApprovalSignature traineeSignature)
		{
			bool flag = screenNum < 1 || studentPersonId < 1 || appId < 1;
			if (flag)
			{
				throw new NullOrInvalidIdParameterException(string.Format("FormApprovalTraineeManager:CreateFormApprovalForm:screenNum={0}:pid={1}:appId={2}", screenNum, studentPersonId, appId));
			}
			IFormApprovalTraineeDAO formApprovalTraineeDAO = new FormApprovalTraineeDAO(this.OpContext);
			Guid guid = formApprovalTraineeDAO.CreateFormApproval(screenNum, studentPersonId, appId);
			bool flag2 = traineeSignature != null;
			if (flag2)
			{
				formApprovalTraineeDAO.CreateOrUpdateFormApprovalTraineeSignature(guid, traineeSignature);
			}
			string text = (traineeSignature == null) ? "Created approval" : "Submitted for approval";
			string text2 = (comment != null) ? comment.CommentText : null;
			text2 = (string.IsNullOrEmpty(text2) ? text : ("[" + text + "]\r\n" + text2));
			IFormApprovalDAO formApprovalDAO = new FormApprovalDAO(this.OpContext);
			formApprovalDAO.AddFormApprovalComment(guid, text2);
			return guid;
		}
	}
}
