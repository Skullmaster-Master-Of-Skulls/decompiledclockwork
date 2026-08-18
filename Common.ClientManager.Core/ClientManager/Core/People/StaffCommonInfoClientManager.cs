using System;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.People
{
	// Token: 0x02000031 RID: 49
	public class StaffCommonInfoClientManager : IStaffCommonInfoClientManager, IWebService
	{
		// Token: 0x060001C1 RID: 449 RVA: 0x00008BDC File Offset: 0x00006DDC
		public byte[] LoadStaffStoredSignature(int StaffPersonId)
		{
			LoadStaffStoredSignatureReq loadStaffStoredSignatureReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadStaffStoredSignatureReq>();
			loadStaffStoredSignatureReq.StaffPersonId = StaffPersonId;
			return ClientServiceFactory.GetClientInstance<IStaffCommonInfo>().LoadStaffStoredSignature(loadStaffStoredSignatureReq).SignatureBytes;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00008C14 File Offset: 0x00006E14
		public void SaveStaffStoredSignature(int StaffPersonId, byte[] imageBytes)
		{
			SaveStaffStoredSignatureReq saveStaffStoredSignatureReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SaveStaffStoredSignatureReq>();
			saveStaffStoredSignatureReq.StaffPersonId = StaffPersonId;
			saveStaffStoredSignatureReq.SignatureBytes = imageBytes;
			ClientServiceFactory.GetClientInstance<IStaffCommonInfo>().SaveStaffStoredSignature(saveStaffStoredSignatureReq);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00008C4C File Offset: 0x00006E4C
		public DynamicDataDTO LoadAssignedAdvisorSignatureData(int StudentPersonId)
		{
			LoadAssignedAdvisorSignatureDataReq loadAssignedAdvisorSignatureDataReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAssignedAdvisorSignatureDataReq>();
			loadAssignedAdvisorSignatureDataReq.StudentPersonId = StudentPersonId;
			return ClientServiceFactory.GetClientInstance<IStaffCommonInfo>().LoadAssignedAdvisorSignatureData(loadAssignedAdvisorSignatureDataReq).Data;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00008C84 File Offset: 0x00006E84
		public void SaveAssignedAdvisorStoredSignatureWithImageBytes(int StudentPersonId, byte[] imageBytes)
		{
			SaveAssignedAdvisorStoredSignatureWithImageBytesReq saveAssignedAdvisorStoredSignatureWithImageBytesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SaveAssignedAdvisorStoredSignatureWithImageBytesReq>();
			saveAssignedAdvisorStoredSignatureWithImageBytesReq.StudentPersonId = StudentPersonId;
			saveAssignedAdvisorStoredSignatureWithImageBytesReq.ImageBytes = imageBytes;
			ClientServiceFactory.GetClientInstance<IStaffCommonInfo>().SaveAssignedAdvisorStoredSignatureWithImageBytes(saveAssignedAdvisorStoredSignatureWithImageBytesReq);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00008CBC File Offset: 0x00006EBC
		public void SaveAssignedAdvisorStoredSignature(int StudentPersonId, DynamicDataDTO dataItem)
		{
			SaveAssignedAdvisorStoredSignatureReq saveAssignedAdvisorStoredSignatureReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SaveAssignedAdvisorStoredSignatureReq>();
			saveAssignedAdvisorStoredSignatureReq.StudentPersonId = StudentPersonId;
			saveAssignedAdvisorStoredSignatureReq.Data = dataItem;
			ClientServiceFactory.GetClientInstance<IStaffCommonInfo>().SaveAssignedAdvisorStoredSignature(saveAssignedAdvisorStoredSignatureReq);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00008CF4 File Offset: 0x00006EF4
		public StaffWithCommonInfoDTO LoadStaffWithCommonInfoById(int PersonId)
		{
			LoadStaffWithCommonInfoByIdReq loadStaffWithCommonInfoByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadStaffWithCommonInfoByIdReq>();
			loadStaffWithCommonInfoByIdReq.PersonId = PersonId;
			return ClientServiceFactory.GetClientInstance<IStaffCommonInfo>().LoadStaffWithCommonInfoById(loadStaffWithCommonInfoByIdReq).StaffWithCommonInfo;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00008D2C File Offset: 0x00006F2C
		public void UpdateCommonInfo(int PersonId, StaffCommonInfoDTO CommonInfo, bool JustUpdateEmailAndPhone)
		{
			UpdateCommonInfoReq updateCommonInfoReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateCommonInfoReq>();
			updateCommonInfoReq.CommonInfo = CommonInfo;
			updateCommonInfoReq.JustUpdateEmailAndPhone = JustUpdateEmailAndPhone;
			updateCommonInfoReq.PersonId = PersonId;
			ClientServiceFactory.GetClientInstance<IStaffCommonInfo>().UpdateCommonInfo(updateCommonInfoReq);
		}
	}
}
