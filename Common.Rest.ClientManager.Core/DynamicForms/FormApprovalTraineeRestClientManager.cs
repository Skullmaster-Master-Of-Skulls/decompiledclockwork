using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.DynamicForms
{
	// Token: 0x02000059 RID: 89
	public class FormApprovalTraineeRestClientManager : BearerTokenRestProxy<IFormApprovalTraineeClientManager>, IFormApprovalTraineeClientManager, IWebService
	{
		// Token: 0x06000370 RID: 880 RVA: 0x0000A9A7 File Offset: 0x00008BA7
		public FormApprovalTraineeRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000A9B1 File Offset: 0x00008BB1
		public FormApprovalTraineeRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000A9BC File Offset: 0x00008BBC
		public FormApprovalForAppointmentDTO LoadFormApprovalForTrainee(int screenNum, int studentPersonId, int appId)
		{
			return base.Get<FormApprovalForAppointmentDTO>(string.Format("fromapprovaltrainee/screennum/{0}/studentpid/{1}/appid/{2}", screenNum, studentPersonId, appId), true);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000A9E4 File Offset: 0x00008BE4
		public void AddFormApprovalCommentForTrainee(Guid formApprovalId, FormApprovalCommentTextDTO comment)
		{
			AddFormApprovalCommentForTraineeReq addFormApprovalCommentForTraineeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AddFormApprovalCommentForTraineeReq>();
			addFormApprovalCommentForTraineeReq.FormApprovalId = formApprovalId;
			addFormApprovalCommentForTraineeReq.Comment = comment;
			base.Post<AddFormApprovalCommentForTraineeReq>(addFormApprovalCommentForTraineeReq, "fromapprovaltrainee/addcomment");
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000AA18 File Offset: 0x00008C18
		public void ReSubmitFormApprovalForm(Guid formApprovalId, FormApprovalCommentTextDTO comment)
		{
			ReSubmitFormApprovalFormReq reSubmitFormApprovalFormReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ReSubmitFormApprovalFormReq>();
			reSubmitFormApprovalFormReq.FormApprovalId = formApprovalId;
			reSubmitFormApprovalFormReq.Comment = comment;
			base.Post<ReSubmitFormApprovalFormReq>(reSubmitFormApprovalFormReq, "fromapprovaltrainee/resubmit");
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000AA4C File Offset: 0x00008C4C
		public Guid CreateFormApprovalForm(int screenNum, int studentPersonId, int appId, FormApprovalCommentTextDTO comment, FormApprovalSignatureDTO traineeSignature)
		{
			CreateFormApprovalFormReq createFormApprovalFormReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateFormApprovalFormReq>();
			createFormApprovalFormReq.ScreenNum = screenNum;
			createFormApprovalFormReq.StudentPersonId = studentPersonId;
			createFormApprovalFormReq.AppointmentId = appId;
			createFormApprovalFormReq.Comment = comment;
			createFormApprovalFormReq.TraineeSignature = traineeSignature;
			return base.Post<CreateFormApprovalFormReq, Guid>(createFormApprovalFormReq, "fromapprovaltrainee/createform");
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000AA95 File Offset: 0x00008C95
		public FormApprovalSignatureDTO LoadTraineeSignature(Guid formApprovalId)
		{
			return base.Get<FormApprovalSignatureDTO>(string.Format("fromapprovaltrainee/id/{0}", formApprovalId), true);
		}
	}
}
