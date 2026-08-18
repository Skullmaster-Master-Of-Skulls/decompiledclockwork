using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.Common.DataStructure;

namespace TechnoPro.ClockWorkWeb.ctrls.Staff.Calendar
{
	// Token: 0x02000141 RID: 321
	public class AppointmentWrapper : WrapperBase<AppointmentDTO>
	{
		// Token: 0x060009C1 RID: 2497 RVA: 0x0004499C File Offset: 0x00042B9C
		public AppointmentWrapper()
		{
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x000449A6 File Offset: 0x00042BA6
		public AppointmentWrapper(AppointmentDTO app) : base(app)
		{
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x000449B1 File Offset: 0x00042BB1
		public int AppointmentId
		{
			get
			{
				AppointmentDTO item = base.Item;
				return (item != null) ? item.AppointmentId : 0;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x000449C8 File Offset: 0x00042BC8
		public string ID
		{
			get
			{
				return this.AppointmentId.ToString();
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x000449E3 File Offset: 0x00042BE3
		public string Subject
		{
			get
			{
				return (base.Item == null) ? "" : base.Item.GetTitleAndSubtitle();
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x00044A00 File Offset: 0x00042C00
		public DateTime? StartDateTime
		{
			get
			{
				AppointmentDTO item = base.Item;
				return (item != null) ? new DateTime?(item.StartDateTime) : null;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x00044A2C File Offset: 0x00042C2C
		public DateTime? EnddateTime
		{
			get
			{
				AppointmentDTO item = base.Item;
				return (item != null) ? new DateTime?(item.EndDateTime) : null;
			}
		}
	}
}
