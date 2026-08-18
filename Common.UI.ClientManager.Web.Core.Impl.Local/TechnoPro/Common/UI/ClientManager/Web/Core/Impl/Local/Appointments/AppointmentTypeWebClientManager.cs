using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.ClientManager.Core.Appointments;
using TechnoPro.Common.ClientManager.ICore.Appointments;
using TechnoPro.Common.UI.ClientManager.Web.Core.Appointments;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Cache;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Appointments
{
	// Token: 0x02000020 RID: 32
	public class AppointmentTypeWebClientManager : IAppointmentTypeWebClientManager
	{
		// Token: 0x060000BD RID: 189 RVA: 0x00007268 File Offset: 0x00005468
		public IList<AppTypeDTO> LoadAllowedAppTypes()
		{
			IList<AppTypeDTO> list = (IList<AppTypeDTO>)SessionCaching.CurrentInstance["allowedAppTypes"];
			bool flag = list != null;
			IList<AppTypeDTO> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				IAppointmentTypeClientManager appointmentTypeClientManager = new AppointmentTypeClientManager();
				List<AppTypeDTO> list2 = appointmentTypeClientManager.LoadAllAppTypes().ToList<AppTypeDTO>();
				list2.Sort((AppTypeDTO g1, AppTypeDTO g2) => (g1.Description ?? "").CompareTo(g2.Description ?? ""));
				list = list2;
				list.Insert(0, new AppTypeDTO
				{
					Description = "",
					AppTypeId = 0
				});
				SessionCaching.CurrentInstance.Insert("allowedAppTypes", list, TimeSpan.FromMinutes(10.0));
				result = list;
			}
			return result;
		}
	}
}
