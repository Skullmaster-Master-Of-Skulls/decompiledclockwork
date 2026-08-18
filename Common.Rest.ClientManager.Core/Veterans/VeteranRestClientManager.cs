using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.Veteran;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.ClientManager.ICore.Veteran;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Veterans
{
	// Token: 0x02000004 RID: 4
	public class VeteranRestClientManager : BearerTokenRestProxy<IVeteranClientManager>, IVeteranClientManager, IWebService
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002427 File Offset: 0x00000627
		public VeteranRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002431 File Offset: 0x00000631
		public VeteranRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000243C File Offset: 0x0000063C
		public bool HasUserCompletedBenefitRequestForm(int Pid, PerDateEntryDTO PerDateEntry, int ScreenNum)
		{
			return PerDateEntry != null && ObjectFactory.Resolve<IDynamicDataClientManager>().DoesAtLeastOneSavedDataItemExist(new DynamicDataContextDTO
			{
				PrimaryId = Pid,
				SecondaryId = PerDateEntry.AppointmentId
			}, eDynamicFormTypeDTO.PerDate, ScreenNum);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000243C File Offset: 0x0000063C
		public bool HasUserCompletedAgreementForm(int Pid, PerDateEntryDTO PerDateEntry, int ScreenNum)
		{
			return PerDateEntry != null && ObjectFactory.Resolve<IDynamicDataClientManager>().DoesAtLeastOneSavedDataItemExist(new DynamicDataContextDTO
			{
				PrimaryId = Pid,
				SecondaryId = PerDateEntry.AppointmentId
			}, eDynamicFormTypeDTO.PerDate, ScreenNum);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002468 File Offset: 0x00000668
		public bool? CounselorResult(int Pid, SessionDTO Session, PerDateEntryDTO PerDateEntry, out string MessageToStudent)
		{
			if (PerDateEntry == null)
			{
				MessageToStudent = null;
				return null;
			}
			IWebSettingsClientManager webSettingsClientManager = ObjectFactory.Resolve<IWebSettingsClientManager>();
			int cidCounselorStatus = webSettingsClientManager.GetSettingValue<int>(Setting.VETERANS_CounselorStatusCid);
			int cidCounselorNote = webSettingsClientManager.GetSettingValue<int>(Setting.VETERANS_CounselorNoteToStudentCid);
			IList<DynamicDataDTO> source = ObjectFactory.Resolve<IDynamicDataClientManager>().LoadDataByFields(new DynamicDataContextDTO
			{
				PrimaryId = Pid,
				SecondaryId = PerDateEntry.AppointmentId
			}, new List<int>
			{
				cidCounselorStatus,
				cidCounselorNote
			}, eDynamicFormTypeDTO.PerDate);
			DynamicDataDTO dynamicDataDTO = source.FirstOrDefault((DynamicDataDTO f) => f.Field.ControlId == cidCounselorStatus);
			DynamicDataDTO dynamicDataDTO2 = source.FirstOrDefault((DynamicDataDTO f) => f.Field.ControlId == cidCounselorNote);
			if (dynamicDataDTO == null || dynamicDataDTO.ValueId < 1)
			{
				MessageToStudent = null;
				return null;
			}
			MessageToStudent = ((dynamicDataDTO2 == null || dynamicDataDTO2.Value == null) ? "" : dynamicDataDTO2.Value.ToString());
			return new bool?(dynamicDataDTO.Value.ToString().IndexOf("approved", StringComparison.OrdinalIgnoreCase) >= 0);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002578 File Offset: 0x00000778
		public bool? AdministratorResult(int Pid, SessionDTO Session, PerDateEntryDTO PerDateEntry, out string MessageToStudent)
		{
			IWebSettingsClientManager webSettingsClientManager = ObjectFactory.Resolve<IWebSettingsClientManager>();
			int cidAdminStatus = webSettingsClientManager.GetSettingValue<int>(Setting.VETERANS_AdminStatusCid);
			int cidAdminNote = webSettingsClientManager.GetSettingValue<int>(Setting.VETERANS_AdminNoteToStudentCid);
			if (PerDateEntry == null)
			{
				MessageToStudent = null;
				return null;
			}
			IList<DynamicDataDTO> source = ObjectFactory.Resolve<IDynamicDataClientManager>().LoadDataByFields(new DynamicDataContextDTO
			{
				PrimaryId = Pid,
				SecondaryId = PerDateEntry.AppointmentId
			}, new List<int>
			{
				cidAdminStatus,
				cidAdminNote
			}, eDynamicFormTypeDTO.PerDate);
			DynamicDataDTO dynamicDataDTO = source.FirstOrDefault((DynamicDataDTO f) => f.Field.ControlId == cidAdminStatus);
			DynamicDataDTO dynamicDataDTO2 = source.FirstOrDefault((DynamicDataDTO f) => f.Field.ControlId == cidAdminNote);
			if (dynamicDataDTO == null)
			{
				MessageToStudent = null;
				return null;
			}
			MessageToStudent = ((dynamicDataDTO2 == null || dynamicDataDTO2.Value == null) ? "" : dynamicDataDTO2.Value.ToString());
			return new bool?(dynamicDataDTO.Value.ToString().IndexOf("approved", StringComparison.OrdinalIgnoreCase) >= 0);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000267E File Offset: 0x0000087E
		public IList<ChangeInBenefitRequestDTO> LoadChangeInBenefits(int StudentPersonId, DateTime StartDate, DateTime EndDate)
		{
			return base.GetMany<ChangeInBenefitRequestDTO>(string.Format("veteran/changeinbenefitrequests/personid/{0}/range/{1}/{2}", StudentPersonId, StartDate, EndDate), true);
		}
	}
}
