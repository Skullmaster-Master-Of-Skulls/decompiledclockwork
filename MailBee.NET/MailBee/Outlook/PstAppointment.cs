using System;
using System.Collections;
using a.b;
using MailBee.Mime;

namespace MailBee.Outlook
{
	// Token: 0x020005AD RID: 1453
	public class PstAppointment : PstMessage
	{
		// Token: 0x060030E2 RID: 12514 RVA: 0x000E4520 File Offset: 0x000E3520
		internal PstAppointment(by A_0) : base(A_0)
		{
			this.c = "X-Appointment-";
			this.b["SendAsIcal"] = A_0.ai();
			this.b["ShowAsBusy"] = A_0.y();
			this.b["Location"] = A_0.i();
			this.b["StartTime"] = A_0.c();
			this.b["EndTime"] = A_0.n();
			this.b["Duration"] = A_0.l();
			this.b["Color"] = A_0.ak();
			this.b["SubType"] = A_0.f();
			this.b["MeetingStatus"] = A_0.q();
			this.b["ResponseStatus"] = A_0.al();
			this.b["RecurrenceBase"] = A_0.r();
			this.b["RecurrenceType"] = A_0.t();
			this.b["RecurrencePattern"] = A_0.v();
			this.b["Timezone"] = A_0.m();
			this.b["AllAttendees"] = A_0.ah();
			this.b["ToAttendees"] = A_0.k();
			this.b["CCAttendees"] = A_0.af();
			this.b["OnlineMeeting"] = A_0.o();
			this.b["NetMeetingType"] = A_0.h();
			this.b["NetMeetingServer"] = A_0.an();
			this.b["NetMeetingOrganizerAlias"] = A_0.u();
			this.b["NetMeetingAutostart"] = A_0.s();
			this.b["ConferenceServerAllowExternal"] = A_0.j();
			this.b["NetMeetingDocumentPathName"] = A_0.p();
			this.b["NetShowURL"] = A_0.w();
			this.b["ConferenceServerPassword"] = A_0.am();
			this.b["AppointmentCounterProposal"] = A_0.ag();
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x060030E3 RID: 12515 RVA: 0x000E47E1 File Offset: 0x000E37E1
		public override PstItemType PstType
		{
			get
			{
				return base.PstType;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x060030E4 RID: 12516 RVA: 0x000E47E9 File Offset: 0x000E37E9
		public override Hashtable PstFields
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x060030E5 RID: 12517 RVA: 0x000E47F4 File Offset: 0x000E37F4
		public override MailMessage GetAsMailMessage()
		{
			MailMessage a_ = base.a((co)this.a, true);
			return base.a(a_);
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x060030E6 RID: 12518 RVA: 0x000E481B File Offset: 0x000E381B
		public override int PstID
		{
			get
			{
				return base.PstID;
			}
		}
	}
}
