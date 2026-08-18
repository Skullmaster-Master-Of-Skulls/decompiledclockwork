using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.Veteran;
using TechnoPro.Common.ClientManager.Core.DynamicForms;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.ClientManager.ICore.Veteran;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Veteran
{
	// Token: 0x02000008 RID: 8
	public class VeteranClientManager : IVeteranClientManager, IWebService
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00002E0C File Offset: 0x0000100C
		public bool HasUserCompletedBenefitRequestForm(int Pid, PerDateEntryDTO PerDateEntry, int ScreenNum)
		{
			bool flag = PerDateEntry == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				IDynamicDataClientManager dynamicDataClientManager = new DynamicDataClientManager();
				result = dynamicDataClientManager.DoesAtLeastOneSavedDataItemExist(new DynamicDataContextDTO
				{
					PrimaryId = Pid,
					SecondaryId = PerDateEntry.AppointmentId
				}, eDynamicFormTypeDTO.PerDate, ScreenNum);
			}
			return result;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002E54 File Offset: 0x00001054
		public bool HasUserCompletedAgreementForm(int Pid, PerDateEntryDTO PerDateEntry, int ScreenNum)
		{
			bool flag = PerDateEntry == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				IDynamicDataClientManager dynamicDataClientManager = new DynamicDataClientManager();
				result = dynamicDataClientManager.DoesAtLeastOneSavedDataItemExist(new DynamicDataContextDTO
				{
					PrimaryId = Pid,
					SecondaryId = PerDateEntry.AppointmentId
				}, eDynamicFormTypeDTO.PerDate, ScreenNum);
			}
			return result;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002E9C File Offset: 0x0000109C
		public bool? CounselorResult(int Pid, SessionDTO Session, PerDateEntryDTO PerDateEntry, out string MessageToStudent)
		{
			bool flag = PerDateEntry == null;
			bool? result;
			if (flag)
			{
				MessageToStudent = null;
				result = null;
			}
			else
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				int cidCounselorStatus = webSettingsClientManager.GetSettingValue<int>(Setting.VETERANS_CounselorStatusCid);
				int cidCounselorNote = webSettingsClientManager.GetSettingValue<int>(Setting.VETERANS_CounselorNoteToStudentCid);
				IDynamicDataClientManager dynamicDataClientManager = new DynamicDataClientManager();
				IList<DynamicDataDTO> source = dynamicDataClientManager.LoadDataByFields(new DynamicDataContextDTO
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
				bool flag2 = dynamicDataDTO == null || dynamicDataDTO.ValueId < 1;
				if (flag2)
				{
					MessageToStudent = null;
					result = null;
				}
				else
				{
					string text;
					if (dynamicDataDTO2 == null)
					{
						text = null;
					}
					else
					{
						object value = dynamicDataDTO2.Value;
						text = ((value != null) ? value.ToString() : null);
					}
					MessageToStudent = (text ?? "");
					result = new bool?(dynamicDataDTO.Value.ToString().IndexOf("approved", StringComparison.OrdinalIgnoreCase) >= 0);
				}
			}
			return result;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002FE4 File Offset: 0x000011E4
		public bool? AdministratorResult(int Pid, SessionDTO Session, PerDateEntryDTO PerDateEntry, out string MessageToStudent)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			int cidAdminStatus = webSettingsClientManager.GetSettingValue<int>(Setting.VETERANS_AdminStatusCid);
			int cidAdminNote = webSettingsClientManager.GetSettingValue<int>(Setting.VETERANS_AdminNoteToStudentCid);
			bool flag = PerDateEntry == null;
			bool? result;
			if (flag)
			{
				MessageToStudent = null;
				result = null;
			}
			else
			{
				IDynamicDataClientManager dynamicDataClientManager = new DynamicDataClientManager();
				IList<DynamicDataDTO> source = dynamicDataClientManager.LoadDataByFields(new DynamicDataContextDTO
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
				bool flag2 = dynamicDataDTO == null;
				if (flag2)
				{
					MessageToStudent = null;
					result = null;
				}
				else
				{
					MessageToStudent = ((dynamicDataDTO2 == null || dynamicDataDTO2.Value == null) ? "" : dynamicDataDTO2.Value.ToString());
					result = new bool?(dynamicDataDTO.Value.ToString().IndexOf("approved", StringComparison.OrdinalIgnoreCase) >= 0);
				}
			}
			return result;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000311C File Offset: 0x0000131C
		public IList<ChangeInBenefitRequestDTO> LoadChangeInBenefits(int StudentPersonId, DateTime StartDate, DateTime EndDate)
		{
			LoadChangeInBenefitRequestsReq loadChangeInBenefitRequestsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadChangeInBenefitRequestsReq>();
			loadChangeInBenefitRequestsReq.PersonId = StudentPersonId;
			loadChangeInBenefitRequestsReq.StartDate = StartDate;
			loadChangeInBenefitRequestsReq.EndDate = EndDate;
			return ClientServiceFactory.GetClientInstance<IVeteran>().LoadChangeInBenefitRequests(loadChangeInBenefitRequestsReq).ChangeInBenefitRequests;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003164 File Offset: 0x00001364
		public List<int> GetControlIdsOfPerStudentFilesAlreadyUploaded(int Pid)
		{
			return new List<int>();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000317D File Offset: 0x0000137D
		public void SetPerStudentToSameForPerDateEntry(int Pid, int perDateEntry_appId)
		{
		}
	}
}
