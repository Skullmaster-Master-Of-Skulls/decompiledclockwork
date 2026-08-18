using System;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.AppointmentsTestBooking
{
	// Token: 0x0200008F RID: 143
	public class StudentClassTestInfoClientManager : IStudentClassTestInfoClientManager, IWebService
	{
		// Token: 0x06000528 RID: 1320 RVA: 0x00016EFC File Offset: 0x000150FC
		public void UpdateExamStatus(int AppointmentId, int NewExamStatusLookupId)
		{
			UpdateExamStatusReq updateExamStatusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateExamStatusReq>();
			updateExamStatusReq.AppointmentId = AppointmentId;
			updateExamStatusReq.NewExamStatusLookupId = NewExamStatusLookupId;
			ClientServiceFactory.GetClientInstance<IStudentClassTestInfo>().UpdateExamStatus(updateExamStatusReq);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00016F34 File Offset: 0x00015134
		public void UpdateBookingNote(int AppointmentId, string BookingNote)
		{
			UpdateBookingNoteReq updateBookingNoteReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateBookingNoteReq>();
			updateBookingNoteReq.AppointmentId = AppointmentId;
			updateBookingNoteReq.BookingNote = BookingNote;
			ClientServiceFactory.GetClientInstance<IStudentClassTestInfo>().UpdateBookingNote(updateBookingNoteReq);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00016F6C File Offset: 0x0001516C
		public void UpdatePrivateNote(int AppointmentId, string PrivateNote)
		{
			UpdatePrivateNoteReq updatePrivateNoteReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdatePrivateNoteReq>();
			updatePrivateNoteReq.AppointmentId = AppointmentId;
			updatePrivateNoteReq.PrivateNote = PrivateNote;
			ClientServiceFactory.GetClientInstance<IStudentClassTestInfo>().UpdatePrivateNote(updatePrivateNoteReq);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00016FA4 File Offset: 0x000151A4
		public void UpdateBookingAndPrivateNote(int AppointmentId, string BookingNote, string PrivateNote)
		{
			UpdateBookingAndPrivateNoteReq updateBookingAndPrivateNoteReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateBookingAndPrivateNoteReq>();
			updateBookingAndPrivateNoteReq.AppointmentId = AppointmentId;
			updateBookingAndPrivateNoteReq.BookingNote = BookingNote;
			updateBookingAndPrivateNoteReq.PrivateNote = PrivateNote;
			ClientServiceFactory.GetClientInstance<IStudentClassTestInfo>().UpdateBookingAndPrivateNote(updateBookingAndPrivateNoteReq);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00016FE4 File Offset: 0x000151E4
		public void UpdateStudentReportedClassDateAndTimeToMatchExamClassDateAndTime(int AppointmentId)
		{
			UpdateStudentReportedClassDateAndTimeToMatchExamClassDateAndTimeReq updateStudentReportedClassDateAndTimeToMatchExamClassDateAndTimeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateStudentReportedClassDateAndTimeToMatchExamClassDateAndTimeReq>();
			updateStudentReportedClassDateAndTimeToMatchExamClassDateAndTimeReq.AppointmentId = AppointmentId;
			ClientServiceFactory.GetClientInstance<IStudentClassTestInfo>().UpdateStudentReportedClassDateAndTimeToMatchExamClassDateAndTime(updateStudentReportedClassDateAndTimeToMatchExamClassDateAndTimeReq);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00017014 File Offset: 0x00015214
		public StudentClassTestDTO LoadStudentTestInfoByAppointmentId(int AppointmentId)
		{
			LoadClassTestByAppointmentIdReq loadClassTestByAppointmentIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadClassTestByAppointmentIdReq>();
			loadClassTestByAppointmentIdReq.AppointmentId = AppointmentId;
			return ClientServiceFactory.GetClientInstance<IStudentClassTestInfo>().LoadClassTestByAppointmentId(loadClassTestByAppointmentIdReq).StudentTestInfo;
		}
	}
}
