using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000033 RID: 51
	internal class StudentClassTestInfoClientBaseProxy : ClientBase<IStudentClassTestInfo>, IStudentClassTestInfo, IService
	{
		// Token: 0x060002A6 RID: 678 RVA: 0x000089C4 File Offset: 0x00006BC4
		public StudentClassTestInfoClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x000089CF File Offset: 0x00006BCF
		public StudentClassTestInfoClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x000089DB File Offset: 0x00006BDB
		public void UpdateExamStatus(UpdateExamStatusReq Request)
		{
			base.Channel.UpdateExamStatus(Request);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x000089EB File Offset: 0x00006BEB
		public void UpdateBookingAndPrivateNote(UpdateBookingAndPrivateNoteReq Request)
		{
			base.Channel.UpdateBookingAndPrivateNote(Request);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x000089FB File Offset: 0x00006BFB
		public void UpdateBookingNote(UpdateBookingNoteReq Request)
		{
			base.Channel.UpdateBookingNote(Request);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00008A0B File Offset: 0x00006C0B
		public void UpdatePrivateNote(UpdatePrivateNoteReq Request)
		{
			base.Channel.UpdatePrivateNote(Request);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00008A1B File Offset: 0x00006C1B
		public void UpdateStudentReportedClassDateAndTimeToMatchExamClassDateAndTime(UpdateStudentReportedClassDateAndTimeToMatchExamClassDateAndTimeReq Request)
		{
			base.Channel.UpdateStudentReportedClassDateAndTimeToMatchExamClassDateAndTime(Request);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00008A2C File Offset: 0x00006C2C
		public LoadClassTestByAppointmentIdResp LoadClassTestByAppointmentId(LoadClassTestByAppointmentIdReq Request)
		{
			return base.Channel.LoadClassTestByAppointmentId(Request);
		}
	}
}
