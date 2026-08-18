using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AppointmentsCalendar;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.AppointmentsCalendar
{
	// Token: 0x0200009B RID: 155
	public class AppointmentHolidayClientManager : IAppointmentHolidayClientManager, IWebService
	{
		// Token: 0x060005C4 RID: 1476 RVA: 0x0001985C File Offset: 0x00017A5C
		public IList<HolidayDTO> LoadHolidays(DateTime StartDate, DateTime EndDate)
		{
			LoadHolidaysReq loadHolidaysReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadHolidaysReq>();
			loadHolidaysReq.StartDate = StartDate;
			loadHolidaysReq.EndDate = EndDate;
			return ClientServiceFactory.GetClientInstance<IAppointmentHoliday>().LoadHolidays(loadHolidaysReq).Holidays;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0001989C File Offset: 0x00017A9C
		public int CreateHoliday(HolidayDTO holiday)
		{
			CreateHolidayReq createHolidayReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateHolidayReq>();
			createHolidayReq.Holiday = holiday;
			return ClientServiceFactory.GetClientInstance<IAppointmentHoliday>().CreateHoliday(createHolidayReq).HolidayId;
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x000198D4 File Offset: 0x00017AD4
		public void DeleteHoliday(int HolidayId)
		{
			DeleteHolidayReq deleteHolidayReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteHolidayReq>();
			deleteHolidayReq.HolidayId = HolidayId;
			ClientServiceFactory.GetClientInstance<IAppointmentHoliday>().DeleteHoliday(deleteHolidayReq);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00019904 File Offset: 0x00017B04
		public void UpdateHoliday(HolidayDTO Holiday)
		{
			UpdateHolidayReq updateHolidayReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateHolidayReq>();
			updateHolidayReq.Holiday = Holiday;
			ClientServiceFactory.GetClientInstance<IAppointmentHoliday>().UpdateHoliday(updateHolidayReq);
		}
	}
}
