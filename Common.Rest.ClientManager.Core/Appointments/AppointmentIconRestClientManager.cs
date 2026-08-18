using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Appointments;
using TechnoPro.Common.ClientManager.ICore.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.Notifications.AppointmentNotifications;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Appointments
{
	// Token: 0x0200006F RID: 111
	public class AppointmentIconRestClientManager : BearerTokenRestProxy<IAppointmentIconClientManager>, IAppointmentIconClientManager, IWebService
	{
		// Token: 0x0600042D RID: 1069 RVA: 0x0000C643 File Offset: 0x0000A843
		public AppointmentIconRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000C64D File Offset: 0x0000A84D
		public AppointmentIconRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000C658 File Offset: 0x0000A858
		public IList<AppointmentIconDTO> LoadAppointmentIconsByAppointment(int AppointmentId)
		{
			return base.GetMany<AppointmentIconDTO>(string.Format("appointmenticon/appid/{0}", AppointmentId), true);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000C671 File Offset: 0x0000A871
		public AppointmentIconDTO LoadAppointmentIcon(int AppointmentId, int IconNum)
		{
			return base.Get<AppointmentIconDTO>(string.Format("appointmenticon/appid/{0}/iconnum/{1}", AppointmentId, IconNum), true);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000C690 File Offset: 0x0000A890
		public AppointmentIconDTO LoadAppointmentIcon(int IconInfoId)
		{
			return base.Get<AppointmentIconDTO>(string.Format("appointmenticon/iconid/{0}", IconInfoId), true);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000C6A9 File Offset: 0x0000A8A9
		public AppointmentIconDTO LoadAppointmentIconByIconNum(int IconNum)
		{
			return base.Get<AppointmentIconDTO>(string.Format("appointmenticon/iconnum/{0}", IconNum), true);
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000C6C2 File Offset: 0x0000A8C2
		public void DeleteAppointmentIconsNotInList(int AppointmentId, IList<int> IconNums)
		{
			base.Delete(string.Format("appointmenticon/appid/{0}/iconnums/{1}", AppointmentId, IconNums.CommaSeparatedValuesWithoutSpace<int>()));
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000C6E0 File Offset: 0x0000A8E0
		public int InsertOrUpdateAppointmentIcon(int AppointmentId, AppointmentIconDTO icon)
		{
			InsertOrUpdateAppointmentIconReq insertOrUpdateAppointmentIconReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<InsertOrUpdateAppointmentIconReq>();
			insertOrUpdateAppointmentIconReq.AppointmentId = AppointmentId;
			insertOrUpdateAppointmentIconReq.AppIcon = icon;
			int num = base.Post<InsertOrUpdateAppointmentIconReq, int>(insertOrUpdateAppointmentIconReq, "appointmenticon");
			AppointmentDTO appointmentDTO = ObjectFactory.Resolve<IAppointmentClientManager>().LoadAppointment(num);
			if (appointmentDTO != null)
			{
				AppointmentNotificationManager.CurrentInstance.NotifyOthersAppointmentChangedOrDeletedAsync(appointmentDTO);
			}
			return num;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000C72F File Offset: 0x0000A92F
		public void DeleteAppointmentIcon(int AppointmentId, int IconNum)
		{
			base.Delete(string.Format("appointmenticon/appid/{0}/iconnum/{1}", AppointmentId, IconNum));
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000C74D File Offset: 0x0000A94D
		public IList<IconInfoDTO> LoadAllIconInfos()
		{
			return base.GetMany<IconInfoDTO>("appointmenticon/alliconinfos", true);
		}
	}
}
