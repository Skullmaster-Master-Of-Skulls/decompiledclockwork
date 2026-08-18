using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Core.Mappers.DynamicForms;
using TechnoPro.Common.Core.Mappers.PersonBase;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000078 RID: 120
	public class StaffCommonInfoServiceManager : IStaffCommonInfo, IService
	{
		// Token: 0x0600047A RID: 1146 RVA: 0x00015240 File Offset: 0x00013440
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00015254 File Offset: 0x00013454
		public LoadStaffStoredSignatureResp LoadStaffStoredSignature(LoadStaffStoredSignatureReq Request)
		{
			IStaffCommonInfoManager staffCommonInfoManager = new StaffCommonInfoManager(Request.GetOperationContext());
			byte[] signatureBytes = staffCommonInfoManager.LoadStaffStoredSignature(Request.StaffPersonId);
			return new LoadStaffStoredSignatureResp
			{
				SignatureBytes = signatureBytes
			};
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0001528C File Offset: 0x0001348C
		public SaveStaffStoredSignatureResp SaveStaffStoredSignature(SaveStaffStoredSignatureReq Request)
		{
			IStaffCommonInfoManager staffCommonInfoManager = new StaffCommonInfoManager(Request.GetOperationContext());
			staffCommonInfoManager.SaveStaffStoredSignature(Request.StaffPersonId, Request.SignatureBytes);
			return new SaveStaffStoredSignatureResp();
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x000152C4 File Offset: 0x000134C4
		public LoadStaffSignatureDataResp LoadStaffStoredSignatureData(LoadStaffSignatureDataReq Request)
		{
			IStaffCommonInfoManager staffCommonInfoManager = new StaffCommonInfoManager(Request.GetOperationContext());
			DynamicData dynamicData = staffCommonInfoManager.LoadStaffStoredSignatureData(Request.StaffPersonId);
			return new LoadStaffSignatureDataResp
			{
				Data = ((dynamicData == null) ? null : dynamicData.ToDTO())
			};
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00015308 File Offset: 0x00013508
		public LoadAssignedAdvisorSignatureDataResp LoadAssignedAdvisorSignatureData(LoadAssignedAdvisorSignatureDataReq Request)
		{
			IStaffCommonInfoManager staffCommonInfoManager = new StaffCommonInfoManager(Request.GetOperationContext());
			DynamicData dynamicData = staffCommonInfoManager.LoadAssignedAdvisorSignatureData(Request.StudentPersonId);
			return new LoadAssignedAdvisorSignatureDataResp
			{
				Data = ((dynamicData == null) ? null : dynamicData.ToDTO())
			};
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0001534C File Offset: 0x0001354C
		public SaveAssignedAdvisorStoredSignatureWithImageBytesResp SaveAssignedAdvisorStoredSignatureWithImageBytes(SaveAssignedAdvisorStoredSignatureWithImageBytesReq Request)
		{
			IStaffCommonInfoManager staffCommonInfoManager = new StaffCommonInfoManager(Request.GetOperationContext());
			staffCommonInfoManager.SaveAssignedAdvisorStoredSignatureWithImageBytes(Request.StudentPersonId, Request.ImageBytes);
			return new SaveAssignedAdvisorStoredSignatureWithImageBytesResp();
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00015384 File Offset: 0x00013584
		public SaveAssignedAdvisorStoredSignatureResp SaveAssignedAdvisorStoredSignature(SaveAssignedAdvisorStoredSignatureReq Request)
		{
			IStaffCommonInfoManager staffCommonInfoManager = new StaffCommonInfoManager(Request.GetOperationContext());
			staffCommonInfoManager.SaveAssignedAdvisorStoredSignature(Request.StudentPersonId, Request.Data.ToDomainObject());
			return new SaveAssignedAdvisorStoredSignatureResp();
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x000153C0 File Offset: 0x000135C0
		public LoadStaffWithCommonInfoByIdResp LoadStaffWithCommonInfoById(LoadStaffWithCommonInfoByIdReq Request)
		{
			IStaffCommonInfoManager staffCommonInfoManager = new StaffCommonInfoManager(Request.GetOperationContext());
			StaffWithCommonInfo staffWithCommonInfo = staffCommonInfoManager.LoadStaffWithCommonInfoById(Request.PersonId);
			return new LoadStaffWithCommonInfoByIdResp
			{
				StaffWithCommonInfo = ((staffWithCommonInfo == null) ? null : staffWithCommonInfo.ToDTO())
			};
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00015404 File Offset: 0x00013604
		public void UpdateCommonInfo(UpdateCommonInfoReq Request)
		{
			IStaffCommonInfoManager staffCommonInfoManager = new StaffCommonInfoManager(Request.GetOperationContext());
			staffCommonInfoManager.UpdateCommonInfo(Request.PersonId, Request.CommonInfo.ToDomainObject(), Request.JustUpdateEmailAndPhone);
		}
	}
}
