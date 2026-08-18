using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.AppointmentsTestBooking
{
	// Token: 0x02000079 RID: 121
	public class StudentClassTestInfoRestClientManager : BearerTokenRestProxy<IStudentClassTestInfoClientManager>, IStudentClassTestInfoClientManager, IWebService
	{
		// Token: 0x060004A8 RID: 1192 RVA: 0x0000D37F File Offset: 0x0000B57F
		public StudentClassTestInfoRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0000D389 File Offset: 0x0000B589
		public StudentClassTestInfoRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0000D394 File Offset: 0x0000B594
		public void UpdateExamStatus(int AppointmentId, int NewExamStatusLookupId)
		{
			UpdateExamStatusReq updateExamStatusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateExamStatusReq>();
			updateExamStatusReq.AppointmentId = AppointmentId;
			updateExamStatusReq.NewExamStatusLookupId = NewExamStatusLookupId;
			base.Put<UpdateExamStatusReq>(updateExamStatusReq, "studentclasstestinfo/updateexamstatus");
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0000D3C8 File Offset: 0x0000B5C8
		public void UpdateBookingNote(int AppointmentId, string BookingNote)
		{
			UpdateBookingNoteReq updateBookingNoteReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateBookingNoteReq>();
			updateBookingNoteReq.AppointmentId = AppointmentId;
			updateBookingNoteReq.BookingNote = BookingNote;
			base.Put<UpdateBookingNoteReq>(updateBookingNoteReq, "studentclasstestinfo/updatebookingnote");
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0000D3FC File Offset: 0x0000B5FC
		public void UpdatePrivateNote(int AppointmentId, string PrivateNote)
		{
			UpdatePrivateNoteReq updatePrivateNoteReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdatePrivateNoteReq>();
			updatePrivateNoteReq.AppointmentId = AppointmentId;
			updatePrivateNoteReq.PrivateNote = PrivateNote;
			base.Put<UpdatePrivateNoteReq>(updatePrivateNoteReq, "studentclasstestinfo/updateprivatenote");
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0000D430 File Offset: 0x0000B630
		public void UpdateBookingAndPrivateNote(int AppointmentId, string BookingNote, string PrivateNote)
		{
			UpdateBookingAndPrivateNoteReq updateBookingAndPrivateNoteReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateBookingAndPrivateNoteReq>();
			updateBookingAndPrivateNoteReq.AppointmentId = AppointmentId;
			updateBookingAndPrivateNoteReq.BookingNote = BookingNote;
			updateBookingAndPrivateNoteReq.PrivateNote = PrivateNote;
			base.Put<UpdateBookingAndPrivateNoteReq>(updateBookingAndPrivateNoteReq, "studentclasstestinfo/updatebookingandprivatenote");
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0000D46C File Offset: 0x0000B66C
		public void UpdateStudentReportedClassDateAndTimeToMatchExamClassDateAndTime(int AppointmentId)
		{
			UpdateStudentReportedClassDateAndTimeToMatchExamClassDateAndTimeReq updateStudentReportedClassDateAndTimeToMatchExamClassDateAndTimeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateStudentReportedClassDateAndTimeToMatchExamClassDateAndTimeReq>();
			updateStudentReportedClassDateAndTimeToMatchExamClassDateAndTimeReq.AppointmentId = AppointmentId;
			base.Put<UpdateStudentReportedClassDateAndTimeToMatchExamClassDateAndTimeReq>(updateStudentReportedClassDateAndTimeToMatchExamClassDateAndTimeReq, "studentclasstestinfo/updatestudentreportedclassdateandtimetomatchexam");
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0000D497 File Offset: 0x0000B697
		public StudentClassTestDTO LoadStudentTestInfoByAppointmentId(int AppointmentId)
		{
			return base.Get<StudentClassTestDTO>(string.Format("studentclasstestinfo/appid/{0}", AppointmentId), true);
		}
	}
}
