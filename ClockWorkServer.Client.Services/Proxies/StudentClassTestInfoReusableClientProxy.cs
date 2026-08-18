using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000032 RID: 50
	public class StudentClassTestInfoReusableClientProxy : WCFTokenBasedReusableClientProxy<IStudentClassTestInfo>, IStudentClassTestInfo, IService
	{
		// Token: 0x0600029E RID: 670 RVA: 0x0000885A File Offset: 0x00006A5A
		public StudentClassTestInfoReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00008865 File Offset: 0x00006A65
		public StudentClassTestInfoReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00008874 File Offset: 0x00006A74
		public void UpdateExamStatus(UpdateExamStatusReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateExamStatus(Request);
			});
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x000088AC File Offset: 0x00006AAC
		public void UpdateBookingAndPrivateNote(UpdateBookingAndPrivateNoteReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateBookingAndPrivateNote(Request);
			});
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x000088E4 File Offset: 0x00006AE4
		public void UpdateBookingNote(UpdateBookingNoteReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateBookingNote(Request);
			});
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000891C File Offset: 0x00006B1C
		public void UpdatePrivateNote(UpdatePrivateNoteReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdatePrivateNote(Request);
			});
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00008954 File Offset: 0x00006B54
		public void UpdateStudentReportedClassDateAndTimeToMatchExamClassDateAndTime(UpdateStudentReportedClassDateAndTimeToMatchExamClassDateAndTimeReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateStudentReportedClassDateAndTimeToMatchExamClassDateAndTime(Request);
			});
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000898C File Offset: 0x00006B8C
		public LoadClassTestByAppointmentIdResp LoadClassTestByAppointmentId(LoadClassTestByAppointmentIdReq Request)
		{
			return this.WrapServiceMethod<LoadClassTestByAppointmentIdResp>(() => this.Proxy.LoadClassTestByAppointmentId(Request));
		}
	}
}
