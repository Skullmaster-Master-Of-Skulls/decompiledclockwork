using System;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.DynamicForms
{
	// Token: 0x0200006A RID: 106
	public class FormApprovalTraineeClientManager : IFormApprovalTraineeClientManager, IWebService
	{
		// Token: 0x060003E2 RID: 994 RVA: 0x00011834 File Offset: 0x0000FA34
		public FormApprovalForAppointmentDTO LoadFormApprovalForTrainee(int screenNum, int studentPersonId, int appId)
		{
			LoadFormApprovalForTraineeReq loadFormApprovalForTraineeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadFormApprovalForTraineeReq>();
			loadFormApprovalForTraineeReq.ScreenNum = screenNum;
			loadFormApprovalForTraineeReq.StudentPersonId = studentPersonId;
			loadFormApprovalForTraineeReq.AppointmentId = appId;
			return ClientServiceFactory.GetClientInstance<IFormApprovalTrainee>().LoadFormApprovalForTrainee(loadFormApprovalForTraineeReq).FormApproval;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0001187C File Offset: 0x0000FA7C
		public void AddFormApprovalCommentForTrainee(Guid formApprovalId, FormApprovalCommentTextDTO comment)
		{
			AddFormApprovalCommentForTraineeReq addFormApprovalCommentForTraineeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AddFormApprovalCommentForTraineeReq>();
			addFormApprovalCommentForTraineeReq.FormApprovalId = formApprovalId;
			addFormApprovalCommentForTraineeReq.Comment = comment;
			ClientServiceFactory.GetClientInstance<IFormApprovalTrainee>().AddFormApprovalCommentForTrainee(addFormApprovalCommentForTraineeReq);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x000118B4 File Offset: 0x0000FAB4
		public void ReSubmitFormApprovalForm(Guid formApprovalId, FormApprovalCommentTextDTO comment)
		{
			ReSubmitFormApprovalFormReq reSubmitFormApprovalFormReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ReSubmitFormApprovalFormReq>();
			reSubmitFormApprovalFormReq.FormApprovalId = formApprovalId;
			reSubmitFormApprovalFormReq.Comment = comment;
			ClientServiceFactory.GetClientInstance<IFormApprovalTrainee>().ReSubmitFormApprovalForm(reSubmitFormApprovalFormReq);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x000118EC File Offset: 0x0000FAEC
		public Guid CreateFormApprovalForm(int screenNum, int studentPersonId, int appId, FormApprovalCommentTextDTO comment, FormApprovalSignatureDTO traineeSignature)
		{
			CreateFormApprovalFormReq createFormApprovalFormReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateFormApprovalFormReq>();
			createFormApprovalFormReq.ScreenNum = screenNum;
			createFormApprovalFormReq.StudentPersonId = studentPersonId;
			createFormApprovalFormReq.AppointmentId = appId;
			createFormApprovalFormReq.Comment = comment;
			createFormApprovalFormReq.TraineeSignature = traineeSignature;
			return ClientServiceFactory.GetClientInstance<IFormApprovalTrainee>().CreateFormApprovalForm(createFormApprovalFormReq).FormApprovalId;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00011944 File Offset: 0x0000FB44
		public FormApprovalSignatureDTO LoadTraineeSignature(Guid formApprovalId)
		{
			LoadTraineeSignatureReq loadTraineeSignatureReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadTraineeSignatureReq>();
			loadTraineeSignatureReq.FormApprovalId = formApprovalId;
			return ClientServiceFactory.GetClientInstance<IFormApprovalTrainee>().LoadTraineeSignature(loadTraineeSignatureReq).Signature;
		}
	}
}
