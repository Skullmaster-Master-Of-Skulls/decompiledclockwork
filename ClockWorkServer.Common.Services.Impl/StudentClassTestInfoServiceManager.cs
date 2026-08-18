using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters;
using TechnoPro.Common.Core.AppointmentsTestBooking;
using TechnoPro.Common.Core.Mappers.AppointmentsTestBooking;
using TechnoPro.Common.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000018 RID: 24
	public class StudentClassTestInfoServiceManager : IStudentClassTestInfo, IService
	{
		// Token: 0x06000128 RID: 296 RVA: 0x00006794 File Offset: 0x00004994
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000067A8 File Offset: 0x000049A8
		public void UpdateExamStatus(UpdateExamStatusReq Request)
		{
			IStudentClassTestInfoManager studentClassTestInfoManager = new StudentClassTestInfoManager(Request.GetOperationContext());
			studentClassTestInfoManager.UpdateExamStatus(Request.AppointmentId, Request.NewExamStatusLookupId);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000067D8 File Offset: 0x000049D8
		public void UpdateBookingNote(UpdateBookingNoteReq Request)
		{
			IStudentClassTestInfoManager studentClassTestInfoManager = new StudentClassTestInfoManager(Request.GetOperationContext());
			studentClassTestInfoManager.UpdateBookingNote(Request.AppointmentId, Request.BookingNote);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00006808 File Offset: 0x00004A08
		public void UpdatePrivateNote(UpdatePrivateNoteReq Request)
		{
			IStudentClassTestInfoManager studentClassTestInfoManager = new StudentClassTestInfoManager(Request.GetOperationContext());
			studentClassTestInfoManager.UpdatePrivateNote(Request.AppointmentId, Request.PrivateNote);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006838 File Offset: 0x00004A38
		public void UpdateBookingAndPrivateNote(UpdateBookingAndPrivateNoteReq Request)
		{
			IStudentClassTestInfoManager studentClassTestInfoManager = new StudentClassTestInfoManager(Request.GetOperationContext());
			studentClassTestInfoManager.UpdateBookingAndPrivateNote(Request.AppointmentId, Request.BookingNote, Request.PrivateNote);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000686C File Offset: 0x00004A6C
		public void UpdateStudentReportedClassDateAndTimeToMatchExamClassDateAndTime(UpdateStudentReportedClassDateAndTimeToMatchExamClassDateAndTimeReq Request)
		{
			IStudentClassTestInfoManager studentClassTestInfoManager = new StudentClassTestInfoManager(Request.GetOperationContext());
			studentClassTestInfoManager.UpdateStudentReportedClassDateAndTimeToMatchExamClassDateAndTime(Request.AppointmentId);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00006894 File Offset: 0x00004A94
		public LoadClassTestByAppointmentIdResp LoadClassTestByAppointmentId(LoadClassTestByAppointmentIdReq Request)
		{
			IStudentClassTestInfoManager studentClassTestInfoManager = new StudentClassTestInfoManager(Request.GetOperationContext());
			StudentClassTest studentClassTest = studentClassTestInfoManager.LoadClassTestByAppointmentId(Request.AppointmentId);
			return new LoadClassTestByAppointmentIdResp
			{
				StudentTestInfo = ((studentClassTest == null) ? null : studentClassTest.ToDTO())
			};
		}
	}
}
