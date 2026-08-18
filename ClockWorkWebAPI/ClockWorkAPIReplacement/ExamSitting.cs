using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace ClockWorkWebAPI.ClockWorkAPIReplacement
{
	// Token: 0x0200005C RID: 92
	public class ExamSitting
	{
		// Token: 0x060004AC RID: 1196 RVA: 0x00021104 File Offset: 0x0001F304
		public ExamSitting()
		{
			this.bookings = new List<AppointmentDTO>();
		}

		// Token: 0x04000262 RID: 610
		private int sittingId;

		// Token: 0x04000263 RID: 611
		private string title;

		// Token: 0x04000264 RID: 612
		private DateTime examDate;

		// Token: 0x04000265 RID: 613
		private DateTime dateCreated;

		// Token: 0x04000266 RID: 614
		private PersonBaseDTO whoCreated;

		// Token: 0x04000267 RID: 615
		private PersonBaseDTO invigilator;

		// Token: 0x04000268 RID: 616
		private InvigilatorConfirmation invigilatorConfirmation;

		// Token: 0x04000269 RID: 617
		private double rateOfPay;

		// Token: 0x0400026A RID: 618
		private PayMethod paymentMethod;

		// Token: 0x0400026B RID: 619
		private PersonBaseDTO room;

		// Token: 0x0400026C RID: 620
		private string location;

		// Token: 0x0400026D RID: 621
		private string privateNotes;

		// Token: 0x0400026E RID: 622
		private string invigilatorNotes;

		// Token: 0x0400026F RID: 623
		private DateTime scheduledStartTime;

		// Token: 0x04000270 RID: 624
		private DateTime scheduledEndTime;

		// Token: 0x04000271 RID: 625
		private DateTime actualTimeIn;

		// Token: 0x04000272 RID: 626
		private DateTime actualTimeOut;

		// Token: 0x04000273 RID: 627
		private bool cancelled;

		// Token: 0x04000274 RID: 628
		private DateTime minScheduledStartTime;

		// Token: 0x04000275 RID: 629
		private DateTime maxScheduledEndTime;

		// Token: 0x04000276 RID: 630
		private List<AppointmentDTO> bookings;
	}
}
