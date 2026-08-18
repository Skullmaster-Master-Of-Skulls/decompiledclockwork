using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.People
{
	// Token: 0x02000029 RID: 41
	public class StaffCommonInfoRestClientManager : BearerTokenRestProxy<IStaffCommonInfoClientManager>, IStaffCommonInfoClientManager, IWebService
	{
		// Token: 0x06000176 RID: 374 RVA: 0x00005972 File Offset: 0x00003B72
		public StaffCommonInfoRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000597C File Offset: 0x00003B7C
		public StaffCommonInfoRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005987 File Offset: 0x00003B87
		public byte[] LoadStaffStoredSignature(int StaffPersonId)
		{
			return base.Get<byte[]>(string.Format("staffcommoninfo/storedsignature/personid/{0}", StaffPersonId), true);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000059A0 File Offset: 0x00003BA0
		public void SaveStaffStoredSignature(int StaffPersonId, byte[] imageBytes)
		{
			SaveStaffStoredSignatureReq saveStaffStoredSignatureReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SaveStaffStoredSignatureReq>();
			saveStaffStoredSignatureReq.StaffPersonId = StaffPersonId;
			saveStaffStoredSignatureReq.SignatureBytes = imageBytes;
			base.Post<SaveStaffStoredSignatureReq>(saveStaffStoredSignatureReq, "staffcommoninfo/savestoredsignature");
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000059D2 File Offset: 0x00003BD2
		public DynamicDataDTO LoadAssignedAdvisorSignatureData(int StudentPersonId)
		{
			return base.Get<DynamicDataDTO>(string.Format("staffcommoninfo/assignedadvisorsignaturedata/personid/{0}", StudentPersonId), true);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000059EC File Offset: 0x00003BEC
		public void SaveAssignedAdvisorStoredSignatureWithImageBytes(int StudentPersonId, byte[] imageBytes)
		{
			SaveAssignedAdvisorStoredSignatureWithImageBytesReq saveAssignedAdvisorStoredSignatureWithImageBytesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SaveAssignedAdvisorStoredSignatureWithImageBytesReq>();
			saveAssignedAdvisorStoredSignatureWithImageBytesReq.StudentPersonId = StudentPersonId;
			saveAssignedAdvisorStoredSignatureWithImageBytesReq.ImageBytes = imageBytes;
			base.Post<SaveAssignedAdvisorStoredSignatureWithImageBytesReq>(saveAssignedAdvisorStoredSignatureWithImageBytesReq, "staffcommoninfo/saveassignedadvisorstoredsignaturebytes");
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00005A20 File Offset: 0x00003C20
		public void SaveAssignedAdvisorStoredSignature(int StudentPersonId, DynamicDataDTO dataItem)
		{
			SaveAssignedAdvisorStoredSignatureReq saveAssignedAdvisorStoredSignatureReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SaveAssignedAdvisorStoredSignatureReq>();
			saveAssignedAdvisorStoredSignatureReq.StudentPersonId = StudentPersonId;
			saveAssignedAdvisorStoredSignatureReq.Data = dataItem;
			base.Post<SaveAssignedAdvisorStoredSignatureReq>(saveAssignedAdvisorStoredSignatureReq, "staffcommoninfo/saveassignedadvisorstoredsignature");
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00005A52 File Offset: 0x00003C52
		public StaffWithCommonInfoDTO LoadStaffWithCommonInfoById(int PersonId)
		{
			return base.Get<StaffWithCommonInfoDTO>(string.Format("staffcommoninfo/personid/{0}", PersonId), true);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00005A6C File Offset: 0x00003C6C
		public void UpdateCommonInfo(int PersonId, StaffCommonInfoDTO CommonInfo, bool JustUpdateEmailAndPhone)
		{
			UpdateCommonInfoReq updateCommonInfoReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateCommonInfoReq>();
			updateCommonInfoReq.CommonInfo = CommonInfo;
			updateCommonInfoReq.JustUpdateEmailAndPhone = JustUpdateEmailAndPhone;
			updateCommonInfoReq.PersonId = PersonId;
			base.Put<UpdateCommonInfoReq>(updateCommonInfoReq, "staffcommoninfo");
		}
	}
}
