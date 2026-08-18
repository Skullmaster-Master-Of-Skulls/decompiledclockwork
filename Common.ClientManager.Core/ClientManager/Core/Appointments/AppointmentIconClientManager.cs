using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.Core.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Appointments;
using TechnoPro.Common.ClientManager.ICore.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.Notifications.AppointmentNotifications;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Appointments
{
	// Token: 0x02000084 RID: 132
	public class AppointmentIconClientManager : IAppointmentIconClientManager, IWebService
	{
		// Token: 0x060004B0 RID: 1200 RVA: 0x000155BC File Offset: 0x000137BC
		public IList<AppointmentIconDTO> LoadAppointmentIconsByAppointment(int AppointmentId)
		{
			LoadAppointmentIconsByAppointmentReq loadAppointmentIconsByAppointmentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAppointmentIconsByAppointmentReq>();
			loadAppointmentIconsByAppointmentReq.AppointmentId = AppointmentId;
			return ClientServiceFactory.GetClientInstance<IAppointmentIcon>().LoadAppointmentIconsByAppointment(loadAppointmentIconsByAppointmentReq).Icons;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x000155F4 File Offset: 0x000137F4
		public AppointmentIconDTO LoadAppointmentIcon(int AppointmentId, int IconNum)
		{
			LoadAppointmentIconReq loadAppointmentIconReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAppointmentIconReq>();
			loadAppointmentIconReq.AppointmentId = AppointmentId;
			loadAppointmentIconReq.IconNum = IconNum;
			return ClientServiceFactory.GetClientInstance<IAppointmentIcon>().LoadAppointmentIcon(loadAppointmentIconReq).Icon;
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00015634 File Offset: 0x00013834
		public AppointmentIconDTO LoadAppointmentIcon(int IconInfoId)
		{
			LoadAppointmentIconReq loadAppointmentIconReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAppointmentIconReq>();
			loadAppointmentIconReq.IconNum = IconInfoId;
			return ClientServiceFactory.GetClientInstance<IAppointmentIcon>().LoadAppointmentIcon(loadAppointmentIconReq).Icon;
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0001566C File Offset: 0x0001386C
		public void DeleteAppointmentIconsNotInList(int AppointmentId, IList<int> IconNums)
		{
			DeleteAppointmentIconsNotInListReq deleteAppointmentIconsNotInListReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteAppointmentIconsNotInListReq>();
			deleteAppointmentIconsNotInListReq.AppointmentId = AppointmentId;
			deleteAppointmentIconsNotInListReq.IconNums = IconNums;
			ClientServiceFactory.GetClientInstance<IAppointmentIcon>().DeleteAppointmentIconsNotInList(deleteAppointmentIconsNotInListReq);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x000156A4 File Offset: 0x000138A4
		public int InsertOrUpdateAppointmentIcon(int AppointmentId, AppointmentIconDTO icon)
		{
			InsertOrUpdateAppointmentIconReq insertOrUpdateAppointmentIconReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<InsertOrUpdateAppointmentIconReq>();
			insertOrUpdateAppointmentIconReq.AppointmentId = AppointmentId;
			insertOrUpdateAppointmentIconReq.AppIcon = icon;
			int id = ClientServiceFactory.GetClientInstance<IAppointmentIcon>().InsertOrUpdateAppointmentIcon(insertOrUpdateAppointmentIconReq).Id;
			IAppointmentClientManager appointmentClientManager = new AppointmentClientManager();
			AppointmentDTO appointmentDTO = appointmentClientManager.LoadAppointment(id);
			bool flag = appointmentDTO != null;
			if (flag)
			{
				AppointmentNotificationManager.CurrentInstance.NotifyOthersAppointmentChangedOrDeletedAsync(appointmentDTO);
			}
			return id;
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0001570C File Offset: 0x0001390C
		public void DeleteAppointmentIcon(int AppointmentId, int IconNum)
		{
			DeleteAppointmentIconReq deleteAppointmentIconReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteAppointmentIconReq>();
			deleteAppointmentIconReq.AppointmentId = AppointmentId;
			deleteAppointmentIconReq.IconNum = IconNum;
			ClientServiceFactory.GetClientInstance<IAppointmentIcon>().DeleteAppointmentIcon(deleteAppointmentIconReq);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00015744 File Offset: 0x00013944
		public IList<IconInfoDTO> LoadAllIconInfos()
		{
			LoadAllIconInfosReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllIconInfosReq>();
			return ClientServiceFactory.GetClientInstance<IAppointmentIcon>().LoadAllIconInfos(request).IconInfos;
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00015774 File Offset: 0x00013974
		public AppointmentIconDTO LoadAppointmentIconByIconNum(int IconNum)
		{
			LoadAppointmentIconByIconNumReq loadAppointmentIconByIconNumReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAppointmentIconByIconNumReq>();
			loadAppointmentIconByIconNumReq.IconNum = IconNum;
			return ClientServiceFactory.GetClientInstance<IAppointmentIcon>().LoadAppointmentIconByIconNum(loadAppointmentIconByIconNumReq).Icon;
		}
	}
}
