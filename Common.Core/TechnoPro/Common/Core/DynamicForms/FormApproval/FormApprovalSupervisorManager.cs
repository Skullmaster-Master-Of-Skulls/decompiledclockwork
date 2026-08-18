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
	// Token: 0x02000103 RID: 259
	public class FormApprovalSupervisorManager : IFormApprovalSupervisorManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000A91 RID: 2705 RVA: 0x0000672B File Offset: 0x0000492B
		public FormApprovalSupervisorManager()
		{
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x000443B0 File Offset: 0x000425B0
		public FormApprovalSupervisorManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x000443C2 File Offset: 0x000425C2
		// (set) Token: 0x06000A94 RID: 2708 RVA: 0x000443CA File Offset: 0x000425CA
		public OperationContext OpContext { get; set; }

		// Token: 0x06000A95 RID: 2709 RVA: 0x000443D4 File Offset: 0x000425D4
		public FormApprovalSignature LoadSupervisorSignature(Guid formApprovalId)
		{
			bool flag = formApprovalId == Guid.Empty;
			if (flag)
			{
				throw new NullOrInvalidIdParameterException("FormApprovalSupervisorManager:LoadSupervisorSignature:Empty form ApprovalId");
			}
			IFormApprovalSupervisorDAO formApprovalSupervisorDAO = new FormApprovalSupervisorDAO(this.OpContext);
			return formApprovalSupervisorDAO.LoadSupervisorSignature(formApprovalId);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00044414 File Offset: 0x00042614
		public FormApprovalForAppointment LoadFormApproval(int screenNum, int studentPersonId, int appId)
		{
			bool flag = screenNum < 1 || studentPersonId < 1 || appId < 1;
			if (flag)
			{
				throw new NullOrInvalidIdParameterException(string.Format("FormApprovalSupervisorManager:LoadFormApproval:screenNum={0}:pid={1}:appId={2}", screenNum, studentPersonId, appId));
			}
			IFormApprovalDAO formApprovalDAO = new FormApprovalDAO(this.OpContext);
			return formApprovalDAO.LoadFormApproval(screenNum, studentPersonId, appId);
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00044470 File Offset: 0x00042670
		public void AddFormApprovalComment(Guid formApprovalId, FormApprovalCommentText comment)
		{
			bool flag = formApprovalId == Guid.Empty;
			if (flag)
			{
				throw new NullOrInvalidIdParameterException("FormApprovalSupervisorManager:AddFormApprovalComment:Empty form ApprovalId");
			}
			IFormApprovalDAO formApprovalDAO = new FormApprovalDAO(this.OpContext);
			formApprovalDAO.AddFormApprovalComment(formApprovalId, comment.CommentText);
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x000444B4 File Offset: 0x000426B4
		public void ApproveForm(Guid formApprovalId, FormApprovalCommentText comment, FormApprovalSignature supervisorSignature)
		{
			bool flag = formApprovalId == Guid.Empty;
			if (flag)
			{
				throw new NullOrInvalidIdParameterException("FormApprovalSupervisorManager:ApproveForm:Empty form ApprovalId");
			}
			bool flag2 = supervisorSignature == null;
			if (flag2)
			{
				throw new NullOrInvalidIdParameterException("FormApprovalSupervisorManager:ApproveForm:Empty supervisor signature");
			}
			IFormApprovalDAO formApprovalDAO = new FormApprovalDAO(this.OpContext);
			formApprovalDAO.UpdateFormApprovalCurrentStatus(formApprovalId, eFormApprovalState.Approved);
			IFormApprovalSupervisorDAO formApprovalSupervisorDAO = new FormApprovalSupervisorDAO(this.OpContext);
			formApprovalSupervisorDAO.CreateOrUpdateFormApprovalSupervisorSignature(formApprovalId, supervisorSignature);
			string text = (comment != null) ? comment.CommentText : null;
			text = (string.IsNullOrEmpty(text) ? "Notes were approved" : ("Notes were approved\r\n" + text));
			formApprovalDAO.AddFormApprovalComment(formApprovalId, text);
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0004454C File Offset: 0x0004274C
		public void SendFormBackToTrainee(Guid formApprovalId, FormApprovalCommentText comment)
		{
			bool flag = formApprovalId == Guid.Empty;
			if (flag)
			{
				throw new NullOrInvalidIdParameterException("FormApprovalSupervisorManager:SendFormBackToTrainee:Empty form ApprovalId");
			}
			IFormApprovalDAO formApprovalDAO = new FormApprovalDAO(this.OpContext);
			formApprovalDAO.UpdateFormApprovalCurrentStatus(formApprovalId, eFormApprovalState.WaitingForTraineeToUpdateNotes);
			string text = (comment != null) ? comment.CommentText : null;
			text = (string.IsNullOrEmpty(text) ? "Notes were sent back" : ("Notes were sent back\r\n" + text));
			formApprovalDAO.AddFormApprovalComment(formApprovalId, text);
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x000445BC File Offset: 0x000427BC
		public void UnApproveFormApproval(Guid formApprovalId, FormApprovalCommentText comment)
		{
			bool flag = formApprovalId == Guid.Empty;
			if (flag)
			{
				throw new NullOrInvalidIdParameterException("FormApprovalSupervisorManager:UnApproveFormApproval:Empty form ApprovalId");
			}
			IFormApprovalSupervisorDAO formApprovalSupervisorDAO = new FormApprovalSupervisorDAO(this.OpContext);
			formApprovalSupervisorDAO.RemoveFormApprovalSupervisorSignature(formApprovalId);
			IFormApprovalDAO formApprovalDAO = new FormApprovalDAO(this.OpContext);
			string text = (comment != null) ? comment.CommentText : null;
			text = (string.IsNullOrEmpty(text) ? "Notes were un-approved" : ("Notes were un-approved\r\n" + text));
			formApprovalDAO.AddFormApprovalComment(formApprovalId, text);
		}
	}
}
