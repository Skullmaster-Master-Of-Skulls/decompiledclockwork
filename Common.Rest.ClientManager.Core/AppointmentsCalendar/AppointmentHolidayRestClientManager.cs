using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.ClientManager.ICore.AppointmentsCalendar;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.AppointmentsCalendar
{
	// Token: 0x02000083 RID: 131
	public class AppointmentHolidayRestClientManager : BearerTokenRestProxy<IAppointmentHolidayClientManager>, IAppointmentHolidayClientManager, IWebService
	{
		// Token: 0x06000525 RID: 1317 RVA: 0x0000E729 File Offset: 0x0000C929
		public AppointmentHolidayRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0000E733 File Offset: 0x0000C933
		public AppointmentHolidayRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0000E73E File Offset: 0x0000C93E
		public IList<HolidayDTO> LoadHolidays(DateTime StartDate, DateTime EndDate)
		{
			return base.GetMany<HolidayDTO>(string.Format("appointmentholiday/range/{0}/{1}", StartDate, EndDate), true);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0000E75D File Offset: 0x0000C95D
		public int CreateHoliday(HolidayDTO holiday)
		{
			return base.Post<HolidayDTO, int>(holiday, "appointmentholiday");
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0000E76B File Offset: 0x0000C96B
		public void DeleteHoliday(int HolidayId)
		{
			base.Delete(string.Format("appointmentholiday/id/{0}", HolidayId));
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0000E783 File Offset: 0x0000C983
		public void UpdateHoliday(HolidayDTO Holiday)
		{
			base.Put<HolidayDTO>(Holiday, "appointmentholiday");
		}
	}
}
