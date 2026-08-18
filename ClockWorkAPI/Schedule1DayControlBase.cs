using System;
using System.Windows.Forms;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;

namespace ClockWorkAPI
{
	// Token: 0x02000082 RID: 130
	public class Schedule1DayControlBase : UserControl
	{
		// Token: 0x06000696 RID: 1686 RVA: 0x0002484C File Offset: 0x0002384C
		public virtual void NotifyClients(AccessibleEvents e, AccessibleObject accObj)
		{
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00024850 File Offset: 0x00023850
		public virtual AccessibleObject GetNewAppointmentAccessibleObject(AppointmentDTO app)
		{
			return null;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00024863 File Offset: 0x00023863
		public virtual void Refocus()
		{
		}
	}
}
