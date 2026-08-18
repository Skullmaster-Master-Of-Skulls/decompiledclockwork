using System;

namespace TechnoPro.Common.Public.Entities.AppointmentSync
{
	// Token: 0x020004E3 RID: 1251
	[Serializable]
	public class ExternalAttendee : BusinessBase<string>
	{
		// Token: 0x17000FBD RID: 4029
		// (get) Token: 0x060025E3 RID: 9699 RVA: 0x00028871 File Offset: 0x00026A71
		// (set) Token: 0x060025E4 RID: 9700 RVA: 0x00028879 File Offset: 0x00026A79
		public string Name { get; set; }

		// Token: 0x17000FBE RID: 4030
		// (get) Token: 0x060025E5 RID: 9701 RVA: 0x00028884 File Offset: 0x00026A84
		// (set) Token: 0x060025E6 RID: 9702 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public string Username
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000FBF RID: 4031
		// (get) Token: 0x060025E7 RID: 9703 RVA: 0x0002889C File Offset: 0x00026A9C
		// (set) Token: 0x060025E8 RID: 9704 RVA: 0x000288A4 File Offset: 0x00026AA4
		public eAttendeeType AttendeeType { get; set; }

		// Token: 0x17000FC0 RID: 4032
		// (get) Token: 0x060025E9 RID: 9705 RVA: 0x000288AD File Offset: 0x00026AAD
		// (set) Token: 0x060025EA RID: 9706 RVA: 0x000288B5 File Offset: 0x00026AB5
		public eMailboxType MailboxType { get; set; }

		// Token: 0x17000FC1 RID: 4033
		// (get) Token: 0x060025EB RID: 9707 RVA: 0x000288BE File Offset: 0x00026ABE
		// (set) Token: 0x060025EC RID: 9708 RVA: 0x000288C6 File Offset: 0x00026AC6
		public string ResponseStatus { get; set; }

		// Token: 0x17000FC2 RID: 4034
		// (get) Token: 0x060025ED RID: 9709 RVA: 0x000288CF File Offset: 0x00026ACF
		// (set) Token: 0x060025EE RID: 9710 RVA: 0x000288D7 File Offset: 0x00026AD7
		public bool? Self { get; set; }

		// Token: 0x17000FC3 RID: 4035
		// (get) Token: 0x060025EF RID: 9711 RVA: 0x000288E0 File Offset: 0x00026AE0
		// (set) Token: 0x060025F0 RID: 9712 RVA: 0x000288E8 File Offset: 0x00026AE8
		public bool? Organizer { get; set; }

		// Token: 0x17000FC4 RID: 4036
		// (get) Token: 0x060025F1 RID: 9713 RVA: 0x000288F1 File Offset: 0x00026AF1
		// (set) Token: 0x060025F2 RID: 9714 RVA: 0x000288F9 File Offset: 0x00026AF9
		public bool? Optional { get; set; }

		// Token: 0x060025F3 RID: 9715 RVA: 0x00028904 File Offset: 0x00026B04
		public ExternalAttendee()
		{
			this.Name = string.Empty;
			this.AttendeeType = eAttendeeType.EVENT_ATTENDEE;
			this.MailboxType = eMailboxType.Mailbox;
			this.ResponseStatus = "needsAction";
			this.Self = new bool?(false);
			this.Organizer = new bool?(false);
			this.Optional = new bool?(false);
		}
	}
}
