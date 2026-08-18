using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO.Appointments;
using TechnoPro.Common.DAO.Impl.Appointments;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.Appointments.AppointmentHistory;

namespace TechnoPro.Common.Core.Appointments
{
	// Token: 0x0200012C RID: 300
	public class AppointmentHistoryManager : IAppointmentHistoryManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000CB2 RID: 3250 RVA: 0x00058E7A File Offset: 0x0005707A
		public AppointmentHistoryManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x00058E8C File Offset: 0x0005708C
		// (set) Token: 0x06000CB4 RID: 3252 RVA: 0x00058E94 File Offset: 0x00057094
		public OperationContext OpContext { get; set; }

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00058EA0 File Offset: 0x000570A0
		public IList<AppointmentChangeLogEntry> LoadAppointmentChangeLog(int appId)
		{
			IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(this.OpContext);
			BaseExtendedAppointment baseExtendedAppointment = baseAppointmentManager.LoadBaseExtendedAppointmentById<BaseExtendedAppointment>(appId);
			IAppointmentHistoryDAO appointmentHistoryDAO = new AppointmentHistoryDAO(this.OpContext);
			IList<AppointmentRawHistoryItem> list = appointmentHistoryDAO.LoadAppointmentRawHistoryItems(appId);
			bool flag = baseExtendedAppointment != null && list.Count < 1;
			IList<AppointmentChangeLogEntry> result;
			if (flag)
			{
				result = new List<AppointmentChangeLogEntry>
				{
					new AppointmentChangeLogEntry
					{
						CurrentAppointmentInfo = baseExtendedAppointment,
						LogEntryDate = baseExtendedAppointment.DateBooked,
						LogEntryOwner = baseExtendedAppointment.WhoBooked.ToBasicPerson(),
						LogEntryType = eAppointmentChangeLogEntryType.Added
					}
				};
			}
			else
			{
				BaseBasicAppointment currentAppointmentInfo = baseExtendedAppointment;
				List<AppointmentChangeLogEntry> list2 = new List<AppointmentChangeLogEntry>();
				for (int i = list.Count - 1; i >= 0; i--)
				{
					AppointmentRawHistoryItem appointmentRawHistoryItem = list[i];
					list2.Add(new AppointmentChangeLogEntry
					{
						LogEntryType = (appointmentRawHistoryItem.IsDeleted ? eAppointmentChangeLogEntryType.Deleted : eAppointmentChangeLogEntryType.Modified),
						CurrentAppointmentInfo = currentAppointmentInfo,
						LogEntryDate = appointmentRawHistoryItem.AuditDateTime,
						LogEntryOwner = appointmentRawHistoryItem.AuditOwner
					});
					currentAppointmentInfo = appointmentRawHistoryItem.AppointmentBeforeChange;
				}
				result = list2;
			}
			return result;
		}
	}
}
