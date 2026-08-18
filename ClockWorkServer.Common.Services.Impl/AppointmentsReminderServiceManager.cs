using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsReminder;
using TechnoPro.Common.Core.AppointmentsReminder;
using TechnoPro.Common.ICore.AppointmentsReminder;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000013 RID: 19
	public class AppointmentsReminderServiceManager : IAppointmentsReminder, IService
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x00005C58 File Offset: 0x00003E58
		public AddMeToExclusionListResp AddMeToExclusionList(AddMeToExclusionListReq request)
		{
			IAppointmentsReminderManager appointmentsReminderManager = new AppointmentsReminderManager(request.GetOperationContext());
			appointmentsReminderManager.AddPeopleToExclusionList(request.WhoAmI);
			return new AddMeToExclusionListResp();
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005C88 File Offset: 0x00003E88
		public RemoveMeFromExclusionListResp RemoveMeFromExclusionList(RemoveMeFromExclusionListReq request)
		{
			IAppointmentsReminderManager appointmentsReminderManager = new AppointmentsReminderManager(request.GetOperationContext());
			appointmentsReminderManager.RemovePeopleFromExclusionList(request.WhoAmI);
			return new RemoveMeFromExclusionListResp();
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005CB8 File Offset: 0x00003EB8
		public IsAppointmentReminderEnableResp IsAppointmentsReminderEnable(IsAppointmentReminderEnableReq request)
		{
			IAppointmentsReminderManager appointmentsReminderManager = new AppointmentsReminderManager(request.GetOperationContext());
			return new IsAppointmentReminderEnableResp
			{
				IsEnable = appointmentsReminderManager.IsAppointmentsReminderEnable()
			};
		}
	}
}
