using System;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsReminder;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AppointmentsReminder;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.AppointmentsReminder
{
	// Token: 0x02000095 RID: 149
	public class AppointmentsReminderClientManager : IAppointmentsReminderClientManager, IWebService
	{
		// Token: 0x06000567 RID: 1383 RVA: 0x00017CD0 File Offset: 0x00015ED0
		public void AddMeToExclusionList()
		{
			AddMeToExclusionListReq addMeToExclusionListReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AddMeToExclusionListReq>();
			addMeToExclusionListReq.BinPath = ((addMeToExclusionListReq.ApplicationContext != null) ? addMeToExclusionListReq.ApplicationContext.ExecutingPath : null);
			ClientServiceFactory.GetClientInstance<IAppointmentsReminder>().AddMeToExclusionList(addMeToExclusionListReq);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00017D14 File Offset: 0x00015F14
		public void RemoveMeFromExclusionList()
		{
			RemoveMeFromExclusionListReq removeMeFromExclusionListReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<RemoveMeFromExclusionListReq>();
			removeMeFromExclusionListReq.BinPath = ((removeMeFromExclusionListReq.ApplicationContext != null) ? removeMeFromExclusionListReq.ApplicationContext.ExecutingPath : null);
			ClientServiceFactory.GetClientInstance<IAppointmentsReminder>().RemoveMeFromExclusionList(removeMeFromExclusionListReq);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00017D58 File Offset: 0x00015F58
		public bool IsAppointmentsReminderEnable()
		{
			IsAppointmentReminderEnableReq isAppointmentReminderEnableReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<IsAppointmentReminderEnableReq>();
			isAppointmentReminderEnableReq.BinPath = ((isAppointmentReminderEnableReq.ApplicationContext != null) ? isAppointmentReminderEnableReq.ApplicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IAppointmentsReminder>().IsAppointmentsReminderEnable(isAppointmentReminderEnableReq).IsEnable;
		}
	}
}
